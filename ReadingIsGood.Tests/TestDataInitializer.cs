using ReadingIsGood.Models.DbModels;

namespace ReadingIsGood.Tests;

public class TestDataInitializer
{
    public void SeedData(ReadingIsGoodContext context)
    {
        var book1 = new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", Price = 10.99m };
        var book2 = new Book { Id = 2, Title = "To Kill a Mockingbird", Price = 7.99m };
        var book3 = new Book { Id = 3, Title = "Titanic", Price = 5.99m };
        var book4 = new Book { Id = 4, Title = "The Women", Price = 14.99m };

        context.Books.AddRange(book1, book2, book3, book4);

        var customer1 = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "ksH/TlPQHTdVm0ffal8rNrgUoj7YJ5PKjlZZKWtB3FG5RoWk"
        };

        context.Customers.AddRange(customer1);

        var order1 = new Order { Id = 1, CustomerId = customer1.Id, OrderDate = DateTime.UtcNow.AddMonths(-1) };
        var order2 = new Order { Id = 2, CustomerId = customer1.Id, OrderDate = DateTime.UtcNow.AddMonths(-1) };
        var order3 = new Order { Id = 3, CustomerId = customer1.Id, OrderDate = DateTime.UtcNow.AddMonths(-2) };
        var order4 = new Order { Id = 4, CustomerId = customer1.Id, OrderDate = DateTime.UtcNow.AddMonths(-3) };

        context.Orders.AddRange(order1, order2, order3, order4);

        var stock1 = new Stock
            { Id = 1, BookId = book1.Id, Sold = false };
        var stock2 = new Stock
            { Id = 2, BookId = book1.Id, Sold = false };
        var stock3 = new Stock
            { Id = 3, BookId = book1.Id, Sold = false };
        var stock4 = new Stock
            { Id = 4, BookId = book1.Id, Sold = false };
        var stock5 = new Stock
            { Id = 5, BookId = book1.Id, Sold = false };
        var stock6 = new Stock
            { Id = 6, BookId = book2.Id, Sold = false };
        var stock7 = new Stock
            { Id = 7, BookId = book2.Id, Sold = false };
        var stock8 = new Stock
            { Id = 8, BookId = book2.Id, Sold = false };
        var stock9 = new Stock
            { Id = 9, BookId = book2.Id, OrderId = order4.Id, Sold = true, SoldDate = order4.OrderDate };
        var stock10 = new Stock
            { Id = 10, BookId = book2.Id, OrderId = order4.Id, Sold = true, SoldDate = order4.OrderDate };
        var stock11 = new Stock
            { Id = 11, BookId = book3.Id, Sold = false };
        var stock12 = new Stock
            { Id = 12, BookId = book4.Id, Sold = false };
        var stock13 = new Stock
            { Id = 13, BookId = book3.Id, Sold = false };
        var stock14 = new Stock
            { Id = 14, BookId = book4.Id, Sold = false };
        var stock15 = new Stock
            { Id = 15, BookId = book2.Id, OrderId = order3.Id, Sold = true, SoldDate = order3.OrderDate };
        var stock16 = new Stock
            { Id = 16, BookId = book3.Id, OrderId = order3.Id, Sold = true, SoldDate = order3.OrderDate };
        var stock17 = new Stock
            { Id = 17, BookId = book2.Id, OrderId = order1.Id, Sold = true, SoldDate = order1.OrderDate };
        var stock18 = new Stock
            { Id = 18, BookId = book4.Id, OrderId = order2.Id, Sold = true, SoldDate = order2.OrderDate };

        context.Stocks.AddRange(stock1, stock2, stock3, stock4, stock5, stock6, stock7, stock8, stock9,
            stock10, stock11, stock12, stock13, stock14, stock15, stock16, stock17, stock18);
        context.SaveChanges();
    }
}