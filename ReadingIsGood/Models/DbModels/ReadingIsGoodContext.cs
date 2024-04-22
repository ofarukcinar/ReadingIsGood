using Microsoft.EntityFrameworkCore;

namespace ReadingIsGood.Models.DbModels;

public class ReadingIsGoodContext : DbContext
{
    public ReadingIsGoodContext(DbContextOptions<ReadingIsGoodContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Book)
            .WithMany(b => b.Stocks)
            .HasForeignKey(s => s.BookId);

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Order)
            .WithMany(o => o.Stocks)
            .HasForeignKey(s => s.OrderId);

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var book1 = new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", Price = 10.99m };
        var book2 = new Book { Id = 2, Title = "To Kill a Mockingbird", Price = 7.99m };
        var book3 = new Book { Id = 3, Title = "Titanic", Price = 5.99m };
        var book4 = new Book { Id = 4, Title = "The Women", Price = 14.99m };

        modelBuilder.Entity<Book>().HasData(book1, book2, book3, book4);

        var customer1 = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "ksH/TlPQHTdVm0ffal8rNrgUoj7YJ5PKjlZZKWtB3FG5RoWk"
        };

        modelBuilder.Entity<Customer>().HasData(customer1);

        var order1 = new Order { Id = 1, CustomerId = customer1.Id, OrderDate = DateTime.UtcNow.AddMonths(-1) };
        var order2 = new Order { Id = 2, CustomerId = customer1.Id, OrderDate = DateTime.UtcNow.AddMonths(-1) };
        var order3 = new Order { Id = 3, CustomerId = customer1.Id, OrderDate = DateTime.UtcNow.AddMonths(-2) };
        var order4 = new Order { Id = 4, CustomerId = customer1.Id, OrderDate = DateTime.UtcNow.AddMonths(-3) };

        modelBuilder.Entity<Order>().HasData(order1, order2, order3, order4);

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

        modelBuilder.Entity<Stock>().HasData(stock1, stock2, stock3, stock4, stock5, stock6, stock7, stock8, stock9,
            stock10, stock11, stock12, stock13, stock14, stock15, stock16, stock17, stock18);
    }
}