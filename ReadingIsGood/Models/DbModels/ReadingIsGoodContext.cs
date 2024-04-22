using Microsoft.EntityFrameworkCore;

namespace ReadingIsGood.Models;

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

        modelBuilder.Entity<Book>().HasData(book1, book2);

        var customer1 = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "ksH/TlPQHTdVm0ffal8rNrgUoj7YJ5PKjlZZKWtB3FG5RoWk"
        };

        modelBuilder.Entity<Customer>().HasData(customer1);

        var order1 = new Order { Id = 1, CustomerId = customer1.Id, OrderDate = DateTime.Now };

        modelBuilder.Entity<Order>().HasData(order1);

        var stock1 = new Stock
            { Id = 1, BookId = book1.Id, Sold = false, SoldDate = DateTime.MinValue };
        var stock2 = new Stock
            { Id = 2, BookId = book1.Id, Sold = false, SoldDate = DateTime.MinValue };
        var stock3 = new Stock
            { Id = 3, BookId = book1.Id, Sold = false, SoldDate = DateTime.MinValue };
        var stock4 = new Stock
            { Id = 4, BookId = book1.Id, Sold = false, SoldDate = DateTime.MinValue };
        var stock5 = new Stock
            { Id = 5, BookId = book1.Id, Sold = false, SoldDate = DateTime.MinValue };
        var stock6 = new Stock
            { Id = 6, BookId = book2.Id, Sold = false, SoldDate = DateTime.MinValue };
        var stock7 = new Stock
            { Id = 7, BookId = book2.Id, Sold = false, SoldDate = DateTime.MinValue };
        var stock8 = new Stock
            { Id = 8, BookId = book2.Id, Sold = false, SoldDate = DateTime.MinValue };
        var stock9 = new Stock
            { Id = 9, BookId = book2.Id, OrderId = order1.Id, Sold = true, SoldDate = DateTime.Now };
        var stock10 = new Stock
            { Id = 10, BookId = book2.Id, OrderId = order1.Id, Sold = true, SoldDate = DateTime.Now };

        modelBuilder.Entity<Stock>().HasData(stock1, stock2, stock3, stock4, stock5, stock6, stock7, stock8, stock9,
            stock10);
    }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public ICollection<Stock> Stocks { get; set; }
}

public class Stock
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
    public bool Sold { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime SoldDate { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }

    public ICollection<Stock> Stocks { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public ICollection<Order> Orders { get; set; }
}