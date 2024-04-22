using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Services;

namespace ReadingIsGood.Tests.Tests;

public class StatisticServiceTests : IDisposable
{
    private readonly DbContextOptions<ReadingIsGoodContext> _dbContextOptions;
    private readonly ReadingIsGoodContext _context;

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