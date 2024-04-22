using ReadingIsGood.Models.ResponseModels;

namespace ReadingIsGood.Interfaces;

public interface IStatisticService
{
    List<StatisticResponseModel> GetMonthlyOrderStatistics(int customerId);
}