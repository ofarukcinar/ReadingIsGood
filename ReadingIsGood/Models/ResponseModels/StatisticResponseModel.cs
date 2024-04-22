namespace ReadingIsGood.Models.ResponseModels;

public class StatisticResponseModel
{
    public string Month { get; set; }
    public int TotalOrderCount { get; set; }
    public decimal TotalPurchasedAmount { get; set; }
    public int TotalBookCount { get; set; }
}