using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Services;

namespace ReadingIsGood.Tests.ServiceTests;

public class StatisticServiceTests : IDisposable
{
    private readonly ReadingIsGoodContext _context;
    private readonly DbContextOptions<ReadingIsGoodContext> _dbContextOptions;

    public StatisticServiceTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ReadingIsGoodContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        _context = new ReadingIsGoodContext(_dbContextOptions);
        var initializer = new TestDataInitializer();
        initializer.SeedData(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void GetMonthlyOrderStatistics_ShouldReturnCorrectStatistics()
    {
        // Arrange
        var statisticService = new StatisticService(_context);

        // Act
        var statistics = statisticService.GetMonthlyOrderStatistics(1);

        // Assert
        Assert.NotNull(statistics);
        Assert.Equal(3, statistics.Count());
    }
}