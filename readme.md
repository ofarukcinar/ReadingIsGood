# ReadingIsGood Project Documentation

## API Documentation

### Overview

The ReadingIsGood project provides an API for managing book orders. This API allows users to list books, place orders,
view their orders, and check stock availability.

### Endpoints

#### 1. Books

- **GET /api/books**: Lists all books.
- **GET /api/books/{id}/stock**: Retrieves the stock status and count of a specific book.
- **POST /api/book**: Adds a new book.
- **PUT /api/books/stock**: Updates the stock status of a book.

#### 2. Orders

- **POST /api/order**: Creates a new order.
- **GET /api/orders/by-date**: Lists orders within a specified date range.

#### 3. Customers

- **POST /api/customer**: Adds a new customer.
- **POST /api/login**: Performs customer login.
- **GET /api/customer/orders**: Lists the customer's orders.

#### 4. Statistics

- **GET /api/order-monthly-statistics**: Retrieves monthly order statistics.

### Usage

You can use any HTTP client to interact with the API. Make sure to include the authorization header with your requests.
To obtain a username and password for customer login, use the `/api/login` endpoint.

---

## Project General Information

### Purpose

The purpose of the ReadingIsGood project is to provide a platform for managing book orders. Users can list books, place
orders, and view their order history.

### Technologies

- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- Dependency Injection
- Unit Test (xUnit)
- Moq (Mocking)

### Installation

1. Clone the project: `git clone https://github.com/username/ReadingIsGood.git`
2. Navigate to the project directory in the terminal or in Visual Studio.
3. Alternatively, Dockerize the project and run it using Docker Compose:

```bash
docker-compose up --build -d
```

**UserName:** john.doe@example.com

**Password:** xayTZL21
