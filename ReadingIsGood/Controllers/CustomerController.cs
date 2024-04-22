using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;

namespace ReadingIsGood.Controllers;

public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService;
    private readonly AppSettings _settings;

    public CustomerController(ICustomerService customerService, AppSettings settings)
    {
        _customerService = customerService;
        _settings = settings;
    }

    /// <summary>
    ///     Creates a new customer.
    /// </summary>
    /// <param name="customer">The details of the customer to create.</param>
    /// <returns>The response containing the result of the operation.</returns>
    [AllowAnonymous]
    [Route("api/customer")]
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CustomerRequestModel customer)
    {
        var response = _customerService.CreateCustomer(customer);
        return Ok(response);
    }

    /// <summary>
    ///     Authenticates a customer.
    /// </summary>
    /// <param name="customer">The credentials of the customer to authenticate.</param>
    /// <returns>The response containing the authentication token or an error message.</returns>
    [AllowAnonymous]
    [Route("api/login")]
    [HttpPost]
    public IActionResult Login([FromBody] CustomerLoginRequestModel customer)
    {
        var userId = _customerService.ValidateUser(customer);
        if (userId == 0) return Ok(new ResponseModel<string>("", "UserName or Password is incorrect"));

        var token = GenerateToken(userId);

        return Ok(new ResponseModel<string>(token));
    }

    /// <summary>
    ///     Retrieves orders for the authenticated customer.
    /// </summary>
    /// <returns>The response containing the customer's orders.</returns>
    [Authorize]
    [Route("api/customer/orders")]
    [HttpGet]
    public IActionResult GetCustomerOrders()
    {
        var customerId = GetCustomerId();
        var response = _customerService.GetCustomerOrders(customerId);
        return Ok(response);
    }

    private string GenerateToken(int id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.JwtSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}