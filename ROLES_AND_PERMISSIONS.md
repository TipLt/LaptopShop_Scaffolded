# Role-Based Access Control (RBAC)

This document details the role-based access control system implemented in the Laptop Shop Management System.

## Overview

The system implements four distinct roles, each with specific permissions tailored to their responsibilities in a laptop shop environment.

## Roles and Responsibilities

### 1. Admin (Administrator)
**Full System Access**

The Admin role has complete access to all system features and is responsible for:
- Overall system management
- User account management
- Full access to all modules

#### Permissions
| Module | Create | Read | Update | Delete |
|--------|--------|------|--------|--------|
| Users | ✅ | ✅ | ✅ | ✅ |
| Laptops | ✅ | ✅ | ✅ | ✅ |
| Orders | ✅ | ✅ | ✅ | ✅ |
| Customers | ✅ | ✅ | ✅ | ✅ |
| Categories | ✅ | ✅ | ✅ | ✅ |
| Suppliers | ✅ | ✅ | ✅ | ✅ |

#### Default Login
- **Username**: `admin`
- **Password**: `admin123`

---

### 2. Manager
**Product & Inventory Management**

The Manager role focuses on product management, inventory oversight, and business operations:
- Managing laptop catalog
- Overseeing categories and suppliers
- Monitoring orders (read-only)

#### Permissions
| Module | Create | Read | Update | Delete |
|--------|--------|------|--------|--------|
| Users | ❌ | ❌ | ❌ | ❌ |
| Laptops | ✅ | ✅ | ✅ | ✅ |
| Orders | ❌ | ✅ | ❌ | ❌ |
| Customers | ❌ | ❌ | ❌ | ❌ |
| Categories | ❌ | ✅ | ❌ | ❌ |
| Suppliers | ❌ | ✅ | ❌ | ❌ |

#### Typical Tasks
- Add new laptop models
- Update laptop specifications and pricing
- Remove discontinued products
- Monitor inventory levels
- Review supplier information
- Track order history

#### Default Login
- **Username**: `manager1`
- **Password**: `manager123`

---

### 3. Sales
**Customer & Order Management**

The Sales role focuses on customer relationships and order processing:
- Managing customer information
- Processing orders
- Viewing product catalog

#### Permissions
| Module | Create | Read | Update | Delete |
|--------|--------|------|--------|--------|
| Users | ❌ | ❌ | ❌ | ❌ |
| Laptops | ❌ | ✅ | ❌ | ❌ |
| Orders | ✅ | ✅ | ✅ | ✅ |
| Customers | ✅ | ✅ | ✅ | ✅ |
| Categories | ❌ | ❌ | ❌ | ❌ |
| Suppliers | ❌ | ❌ | ❌ | ❌ |

#### Typical Tasks
- Add new customers
- Update customer information
- Create new orders
- Process order status changes
- View laptop specifications and prices
- Handle customer inquiries

#### Default Login
- **Username**: `sales1`
- **Password**: `sales123`

---

### 4. Warehouse
**Stock & Inventory Management**

The Warehouse role manages physical inventory and stock levels:
- Managing laptop stock quantities
- Tracking inventory levels
- Coordinating with suppliers

#### Permissions
| Module | Create | Read | Update | Delete |
|--------|--------|------|--------|--------|
| Users | ❌ | ❌ | ❌ | ❌ |
| Laptops | ✅ | ✅ | ✅ | ❌ |
| Orders | ❌ | ❌ | ❌ | ❌ |
| Customers | ❌ | ❌ | ❌ | ❌ |
| Categories | ❌ | ❌ | ❌ | ❌ |
| Suppliers | ❌ | ✅ | ❌ | ❌ |

#### Typical Tasks
- Update laptop stock quantities
- Add new inventory items
- Modify laptop details
- Check supplier information
- Monitor stock levels
- Coordinate restocking

#### Default Login
- **Username**: `warehouse1`
- **Password**: `warehouse123`

---

## Role Comparison Matrix

### Quick Reference

| Feature | Admin | Manager | Sales | Warehouse |
|---------|-------|---------|-------|-----------|
| User Management | ✅ Full | ❌ None | ❌ None | ❌ None |
| Laptop Management | ✅ Full | ✅ Full | 👁️ View Only | ✅ Stock Only |
| Order Management | ✅ Full | 👁️ View Only | ✅ Full | ❌ None |
| Customer Management | ✅ Full | ❌ None | ✅ Full | ❌ None |
| Category Management | ✅ Full | 👁️ View Only | ❌ None | ❌ None |
| Supplier Management | ✅ Full | 👁️ View Only | ❌ None | 👁️ View Only |

### Dashboard Access

The main dashboard dynamically shows only the modules accessible to the current user's role:

```
Admin Dashboard:
├── 💻 Laptop Management
├── 📦 Order Management
├── 👥 Customer Management
├── 📑 Category Management
├── 🏭 Supplier Management
└── ⚙️ User Management

Manager Dashboard:
├── 💻 Laptop Management
├── 📦 Order Management (View)
├── 📑 Category Management (View)
└── 🏭 Supplier Management (View)

Sales Dashboard:
├── 💻 Laptop Management (View)
├── 📦 Order Management
└── 👥 Customer Management

Warehouse Dashboard:
├── 💻 Laptop Management (Stock)
└── 🏭 Supplier Management (View)
```

---

## Implementation Details

### Session Management
```csharp
// Current user stored in Singleton
SessionManager.Instance.CurrentUser

// Role checking
SessionManager.Instance.HasRole("Admin")
SessionManager.Instance.HasAnyRole("Admin", "Manager")
```

### View Visibility
```csharp
// In MainViewModel
public bool CanAccessLaptops => 
    SessionManager.Instance.HasAnyRole("Admin", "Manager", "Warehouse", "Sales");

public bool CanAccessUsers => 
    SessionManager.Instance.HasRole("Admin");
```

### XAML Binding
```xml
<Button Command="{Binding ManageUsersCommand}"
        Visibility="{Binding CanAccessUsers, 
                    Converter={StaticResource BooleanToVisibilityConverter}}"/>
```

---

## Security Considerations

### Current Implementation
- ✅ Role-based UI visibility
- ✅ Session management
- ✅ Active user validation

### For Production (Recommended Enhancements)
- ⚠️ Implement password hashing (currently plain text)
- ⚠️ Add server-side authorization checks
- ⚠️ Implement audit logging
- ⚠️ Add password complexity requirements
- ⚠️ Implement session timeout
- ⚠️ Add two-factor authentication for Admin

---

## Role Assignment

### Database Schema
```sql
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Sales', 'Manager', 'Warehouse')),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);
```

### Adding New Users
Admins can add new users through the User Management module with role selection:
- Username (unique)
- Password
- Role (dropdown: Admin, Manager, Sales, Warehouse)
- Full Name
- Email
- Active status

---

## Business Workflow Examples

### Example 1: New Laptop Stock Arrival
1. **Warehouse** receives new laptops
2. **Warehouse** updates stock quantity in Laptop Management
3. **Manager** reviews inventory levels
4. **Sales** sees updated stock and can now sell

### Example 2: Customer Purchase
1. **Sales** adds/updates customer information
2. **Sales** creates new order with laptop selection
3. **Sales** processes payment and updates order status
4. **Manager** monitors order completion
5. **Admin** can review entire transaction

### Example 3: Product Management
1. **Manager** adds new laptop model
2. **Manager** sets categories and specifications
3. **Manager** links suppliers
4. **Warehouse** updates initial stock
5. **Sales** can now view and sell the new product

---

## Testing Role-Based Access

To test each role, log in with the respective credentials and verify:

1. **Dashboard Modules**: Only permitted modules should be visible
2. **CRUD Operations**: Buttons should be enabled/disabled based on permissions
3. **Navigation**: Restricted modules should not be accessible
4. **Data Display**: Each role sees appropriate data

---

## Extending the System

To add a new role:

1. Update database constraint:
```sql
ALTER TABLE Users DROP CONSTRAINT [constraint_name];
ALTER TABLE Users ADD CONSTRAINT [constraint_name] 
    CHECK (Role IN ('Admin', 'Sales', 'Manager', 'Warehouse', 'NewRole'));
```

2. Update `SessionManager` with role checking methods
3. Add role permissions in ViewModels
4. Update dashboard visibility in `MainViewModel`
5. Test all access scenarios

---

## Summary

The role-based access control system provides:
- ✅ Clear separation of responsibilities
- ✅ Appropriate access levels for each role
- ✅ Secure session management
- ✅ Dynamic UI adaptation
- ✅ Scalable permission system
- ✅ Business workflow support

This RBAC implementation ensures that users can only access features relevant to their job function, maintaining data security and operational clarity.
