namespace ReadingIsGood.Models.ResponseModels;

public class StatisticResponseModel
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int TotalOrderCount { get; set; }
    public decimal TotalPurchasedAmount { get; set; }
    public int TotalBookCount { get; set; }
}