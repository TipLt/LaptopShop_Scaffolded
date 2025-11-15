# LaptopShop Management System

A comprehensive WPF application for managing a laptop retail shop, built with C# and Entity Framework Core following the MVVM pattern.

## 📋 Project Overview

This is a final project for WPF course that implements a complete management system for a laptop shop with role-based access control, database operations, and modern UI design.

## ✅ Requirements Met

### Database Requirements
- ✅ **9 Tables** (exceeds minimum of 6):
  - Users (authentication & roles)
  - Customers
  - Laptops
  - Categories
  - Suppliers
  - Orders
  - OrderDetails (junction table)
  - LaptopCategories (junction table)
  - LaptopSuppliers (junction table)

- ✅ **3 N-N Relationships**:
  1. Laptops ↔ Categories (via LaptopCategories)
  2. Laptops ↔ Suppliers (via LaptopSuppliers)
  3. Orders ↔ Laptops (via OrderDetails)

### Windows (Need 3+, Implemented 8)
1. **LoginWindow** - User authentication
2. **MainWindow** - Role-based dashboard
3. **LaptopManagementWindow** - Full CRUD for laptops
4. **OrderManagementWindow** - View and manage orders
5. **CustomerManagementWindow** - CRUD for customers
6. **CategoryManagementWindow** - View categories
7. **SupplierManagementWindow** - View suppliers
8. **UserManagementWindow** - Admin-only user management

### Design Patterns (Need 3, Implemented 3)
1. **Singleton Pattern** - `SessionManager` for user session management
2. **Repository Pattern** - Generic and specific repositories for data access abstraction
3. **Factory Pattern** - `ViewModelFactory` for creating ViewModels

## 🏗️ Architecture

The application follows **MVVM (Model-View-ViewModel)** architecture:

```
LaptopShop/
├── Model/                      # Entity Framework models (scaffolded)
│   ├── LaptopShopDbContext.cs
│   ├── User.cs
│   ├── Laptop.cs
│   ├── Order.cs
│   └── ...
├── Repositories/               # Repository Pattern implementation
│   ├── IRepository.cs
│   ├── Repository.cs           # Generic repository
│   ├── UserRepository.cs
│   ├── LaptopRepository.cs
│   └── OrderRepository.cs
├── ViewModels/                 # ViewModels with INotifyPropertyChanged
│   ├── BaseViewModel.cs
│   ├── LoginViewModel.cs
│   ├── MainViewModel.cs
│   └── ...
├── Views/                      # XAML Windows
│   ├── LoginWindow.xaml
│   ├── MainWindow.xaml
│   └── ...
├── Services/                   # Business services
│   ├── SessionManager.cs       # Singleton
│   └── ViewModelFactory.cs     # Factory
└── Helpers/                    # Utilities
    ├── RelayCommand.cs
    └── StringToVisibilityConverter.cs
```

## 👥 Role-Based Access Control

### Admin (Full Access)
- ✅ Laptop Management (CRUD)
- ✅ Order Management (CRUD)
- ✅ Customer Management (CRUD)
- ✅ Category Management (View)
- ✅ Supplier Management (View)
- ✅ User Management (CRUD)

### Manager
- ✅ Laptop Management (CRUD)
- ✅ Order Management (View)
- ✅ Category Management (View)
- ✅ Supplier Management (View)

### Sales
- ✅ Laptop Management (View only)
- ✅ Order Management (CRUD)
- ✅ Customer Management (CRUD)

### Warehouse
- ✅ Laptop Management (CRUD - Stock management)
- ✅ Supplier Management (View)

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server 2019 or later
- Visual Studio 2022 (recommended)

### Database Setup
1. Create the database using the script in `Database/CreateDatabase.sql`
2. Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "MyCnn": "server=YOUR_SERVER;database=LaptopShopDB;user=YOUR_USER;password=YOUR_PASSWORD;TrustServerCertificate=true;Encrypt=true"
  }
}
```

### Running the Application
1. Open `PRN212_Project.slnx` in Visual Studio
2. Restore NuGet packages
3. Build and run the project

### Default Login Credentials
- **Admin**: `admin` / `admin123`
- **Manager**: `manager1` / `manager123`
- **Sales**: `sales1` / `sales123`
- **Warehouse**: `warehouse1` / `warehouse123`

## 📦 NuGet Packages
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.22)
- `Microsoft.EntityFrameworkCore.Design` (8.0.22)
- `Microsoft.Extensions.Configuration.Json` (8.0.1)

## 🎨 Features

### Authentication
- Secure login with role-based access
- Session management using Singleton pattern
- Active user validation

### Laptop Management
- Add, Edit, Delete, View laptops
- Track specifications (Brand, Model, Processor, RAM, Storage, GPU)
- Price and stock management
- Many-to-many relationship with categories and suppliers

### Order Management
- View order history
- Customer information tracking
- Order status management (Pending, Processing, Completed, Cancelled)
- Order details with laptop items

### Customer Management
- Add, Edit, Delete customers
- Track contact information
- Link to order history

### Dashboard
- Role-based module visibility
- Modern card-based UI
- Quick access to all modules
- Welcome message with user info

## 🎯 Design Patterns Explained

### 1. Singleton Pattern (SessionManager)
```csharp
public class SessionManager
{
    private static SessionManager? _instance;
    private static readonly object _lock = new object();
    
    public static SessionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionManager();
                    }
                }
            }
            return _instance;
        }
    }
}
```
**Purpose**: Ensures only one instance manages the current user session across the application.

### 2. Repository Pattern
```csharp
public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    int SaveChanges();
}
```
**Purpose**: Abstracts data access logic and provides a clean separation between business logic and data access.

### 3. Factory Pattern (ViewModelFactory)
```csharp
public class ViewModelFactory
{
    public LoginViewModel CreateLoginViewModel()
    {
        return new LoginViewModel(new LaptopShopDbContext());
    }
    // ... other factory methods
}
```
**Purpose**: Centralizes the creation of ViewModels with their dependencies.

## 🔒 Security Notes
- Passwords are stored in plain text (for demonstration only - not production-ready)
- Connection strings should be secured in production
- Implement proper authentication/authorization for production use

## 📝 License
This is an educational project for PRN212 course.

## 👨‍💻 Author
Created as a final project for WPF course with Entity Framework Core and MVVM pattern.