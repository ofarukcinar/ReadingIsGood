using Mapster;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Models;

public class MapsterConfig
{
    public static void Init()
    {
        TypeAdapterConfig<Order, OrderVM>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Books,
                src => src.Stocks.ToList().Select(x => new BookVM
                    { Title = x.Book.Title, Id = x.Book.Id, Price = x.Book.Price }))
            .Map(dest => dest.OrderDate, src => src.OrderDate);
    }
}