# Design Patterns Implementation

This document explains the three design patterns implemented in the Laptop Shop Management System.

## 1. Singleton Pattern - Session Management

### Location
`Services/SessionManager.cs`

### Purpose
Ensures only one instance of the session manager exists throughout the application lifetime, managing the currently logged-in user's state globally.

### Implementation
```csharp
public class SessionManager
{
    private static SessionManager? _instance;
    private static readonly object _lock = new object();

    public User? CurrentUser { get; private set; }

    private SessionManager() { }

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

    public void Login(User user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }

    public bool HasRole(string role)
    {
        return CurrentUser?.Role == role;
    }
}
```

### Key Features
- **Thread-Safe**: Double-check locking pattern ensures thread safety
- **Lazy Initialization**: Instance created only when first accessed
- **Global State**: Maintains current user across the entire application
- **Role Checking**: Provides convenient methods to check user permissions

### Usage Example
```csharp
// Login
SessionManager.Instance.Login(user);

// Check permissions
if (SessionManager.Instance.HasRole("Admin"))
{
    // Admin-only functionality
}

// Access current user
var currentUser = SessionManager.Instance.CurrentUser;

// Logout
SessionManager.Instance.Logout();
```

### Benefits
- ✅ Single source of truth for user session
- ✅ Easy to access from anywhere in the application
- ✅ Thread-safe implementation
- ✅ Prevents multiple session instances

---

## 2. Repository Pattern - Data Access Layer

### Location
- `Repositories/IRepository.cs` - Generic interface
- `Repositories/Repository.cs` - Generic implementation
- `Repositories/UserRepository.cs` - Specific repository
- `Repositories/LaptopRepository.cs` - Specific repository
- `Repositories/OrderRepository.cs` - Specific repository

### Purpose
Abstracts data access logic, providing a clean separation between business logic and data persistence. Enables easy testing and maintenance by hiding database implementation details.

### Implementation

#### Generic Interface
```csharp
public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(int id);
    int SaveChanges();
}
```

#### Generic Implementation
```csharp
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly LaptopShopDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(LaptopShopDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    // ... other implementations
}
```

#### Specific Repository (with custom methods)
```csharp
public class LaptopRepository : Repository<Laptop>
{
    public LaptopRepository(LaptopShopDbContext context) : base(context)
    {
    }

    public IEnumerable<Laptop> GetAllWithDetails()
    {
        return _dbSet
            .Include(l => l.Categories)
            .Include(l => l.LaptopSuppliers)
                .ThenInclude(ls => ls.Supplier)
            .ToList();
    }

    public Laptop? GetByIdWithDetails(int id)
    {
        return _dbSet
            .Include(l => l.Categories)
            .Include(l => l.LaptopSuppliers)
                .ThenInclude(ls => ls.Supplier)
            .FirstOrDefault(l => l.LaptopId == id);
    }
}
```

### Key Features
- **Generic Operations**: Common CRUD operations available for all entities
- **Specific Extensions**: Specialized repositories can add custom methods
- **Eager Loading**: Support for loading navigation properties
- **Expression-based Queries**: Flexible querying with LINQ expressions
- **Unit of Work**: SaveChanges method manages transactions

### Usage Example
```csharp
// Generic repository
var categoryRepo = new Repository<Category>(context);
var allCategories = categoryRepo.GetAll();

// Specific repository with custom methods
var laptopRepo = new LaptopRepository(context);
var laptopsWithDetails = laptopRepo.GetAllWithDetails();

// Add new entity
laptopRepo.Add(newLaptop);
laptopRepo.SaveChanges();

// Update entity
laptopRepo.Update(existingLaptop);
laptopRepo.SaveChanges();

// Delete entity
laptopRepo.Delete(laptop);
laptopRepo.SaveChanges();

// Complex queries
var gamingLaptops = laptopRepo.Find(l => l.Price > 30000000);
```

### Benefits
- ✅ Separation of concerns
- ✅ Easy to test (mockable interface)
- ✅ Reduces code duplication
- ✅ Centralized data access logic
- ✅ Easy to maintain and extend
- ✅ Database-agnostic business logic

---

## 3. Factory Pattern - ViewModel Creation

### Location
`Services/ViewModelFactory.cs`

### Purpose
Centralizes the creation of ViewModels, managing their dependencies and providing a single point for ViewModel instantiation. This makes it easier to change constructor parameters and manage dependencies.

### Implementation
```csharp
public class ViewModelFactory
{
    private readonly LaptopShopDbContext _context;

    public ViewModelFactory()
    {
        _context = new LaptopShopDbContext();
    }

    public LoginViewModel CreateLoginViewModel()
    {
        return new LoginViewModel(_context);
    }

    public MainViewModel CreateMainViewModel()
    {
        return new MainViewModel(_context);
    }

    public LaptopManagementViewModel CreateLaptopManagementViewModel()
    {
        return new LaptopManagementViewModel(_context);
    }

    public OrderManagementViewModel CreateOrderManagementViewModel()
    {
        return new OrderManagementViewModel(_context);
    }

    public CustomerManagementViewModel CreateCustomerManagementViewModel()
    {
        return new CustomerManagementViewModel(_context);
    }

    public CategoryManagementViewModel CreateCategoryManagementViewModel()
    {
        return new CategoryManagementViewModel(_context);
    }

    public SupplierManagementViewModel CreateSupplierManagementViewModel()
    {
        return new SupplierManagementViewModel(_context);
    }

    public UserManagementViewModel CreateUserManagementViewModel()
    {
        return new UserManagementViewModel(_context);
    }
}
```

### Key Features
- **Dependency Injection**: Manages DbContext creation and injection
- **Centralized Creation**: Single place to create all ViewModels
- **Easy Maintenance**: Changes to constructor parameters only need updating in one place
- **Testability**: Easy to create mock factories for testing

### Usage Example
```csharp
// In Window code-behind
public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        var factory = new ViewModelFactory();
        DataContext = factory.CreateLoginViewModel();
    }
}

// In ViewModel (when opening new windows)
private void ExecuteManageLaptops(object? parameter)
{
    var window = new Views.LaptopManagementWindow();
    window.ShowDialog();
}
```

### Benefits
- ✅ Single Responsibility: Factory handles creation, not the Views
- ✅ Loose Coupling: Views don't need to know about ViewModel dependencies
- ✅ Easy Testing: Can create test factories with mock dependencies
- ✅ Maintainability: Changes to dependencies in one place
- ✅ Consistency: All ViewModels created the same way

---

## Additional Design Patterns Used

### MVVM Pattern (Architectural Pattern)
While not one of the three required patterns, MVVM is the core architectural pattern:

- **Model**: Entity Framework entities (`Model/` folder)
- **View**: XAML Windows (`Views/` folder)
- **ViewModel**: Logic and data binding (`ViewModels/` folder)

### Command Pattern (Implicit)
Using `ICommand` and `RelayCommand` for user interactions:
```csharp
public ICommand LoginCommand { get; }
LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
```

---

## Design Pattern Benefits Summary

| Pattern | Primary Benefit | Secondary Benefits |
|---------|----------------|-------------------|
| **Singleton** | Ensures single instance | Thread-safe, global access, resource efficiency |
| **Repository** | Data access abstraction | Testability, maintainability, separation of concerns |
| **Factory** | Centralized creation | Dependency management, consistency, easy testing |

## Integration Example

Here's how all three patterns work together:

```csharp
// User logs in
var factory = new ViewModelFactory();
var loginVM = factory.CreateLoginViewModel(); // Factory Pattern
loginVM.Login();

// Login successful
SessionManager.Instance.Login(user); // Singleton Pattern

// Load data using repository
var laptopRepo = new LaptopRepository(context); // Repository Pattern
var laptops = laptopRepo.GetAllWithDetails();

// Check permissions
if (SessionManager.Instance.HasRole("Admin")) // Singleton Pattern
{
    // Show admin features
}
```

This combination provides a robust, maintainable, and testable architecture for the Laptop Shop Management System.
