# Store API

This is a .NET Core Web API project for an e-commerce application that manages users, products, brands, and orders, with payment processing using Stripe and temporary storage of baskets using Redis.

## Project Overview

This API provides endpoints for:
- Managing users and user authentication
- Retrieving and filtering products by type and brand
- Adding products to a temporary basket
- Creating and confirming orders
- Integrating Stripe for secure payment processing

## Technologies Used

- **ASP.NET Core**: For building the API.
- **Entity Framework Core**: For ORM and database management.
- **SQL Server**: As the primary database to store persistent data.
- **Redis**: For temporary basket storage.
- **Stripe API**: For payment processing with intent-based transactions.
- **AutoMapper**: For mapping database models to data transfer objects (DTOs).

## Project Structure

- **Users**: Stores user data, using Identity for authentication and authorization.
- **Products**: Stores products, with relationships to `Brand` and `Type`.
- **Basket**: Temporary storage of items for checkout using Redis.
- **Orders**: Stores order data and integrates with Stripe for payment intent.

## Database Design

- **User Table**: Stores user information such as email, password, and roles.
- **Product Table**: Each product has a `TypeId` and `BrandId`, referencing the product's type and brand.
- **Brand and Type Tables**: Each brand and type is defined with an ID and name.
- **Basket (in Redis)**: Temporarily stores products added by users until they confirm an order.
- **Order Table**: Stores order details once the basket is confirmed and payment is completed.

## Key Features

### 1. User Management
- **Registration and Authentication**: Users can sign up and sign in using secure authentication.
- **Authorization**: Certain endpoints are protected, accessible only to authenticated users.

### 2. Product Management
- **Retrieve Products**: Users can retrieve products and filter by `Brand` or `Type`.
- **Product Brands and Types**: Provides endpoints to list product brands and types.

### 3. Basket Management
- **Temporary Basket**: Uses Redis to temporarily store basket items before an order is placed.
- **Redis Integration**: Redis cache is configured to persist basket data temporarily for improved performance and reduced database load.

### 4. Order and Payment Processing
- **Order Creation**: Once a user confirms their basket, it’s converted into an order.
- **Stripe Payment**: Payment is handled using Stripe’s PaymentIntent API, allowing secure transactions.
- **Order Confirmation**: If payment is successful, the order is saved in the database and marked as completed.

### 5. Stripe Integration
- **Payment Intent**: The payment flow is handled by creating a `PaymentIntent` in Stripe for each order, ensuring secure payment handling.
- **Testing**: Stripe test cards are used to verify the payment functionality in a development environment.

## Running the Project

### Prerequisites

- .NET Core SDK (6.0 or later)
- SQL Server (or another relational database configured for Entity Framework)
- Redis (for basket caching)
- Stripe API keys (for payment integration)

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/store-api.git
   cd store-api
