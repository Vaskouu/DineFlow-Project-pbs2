DineFlow Restaurant Ordering API
📌 Overview

DineFlow is an ASP.NET Core Web API project for restaurant management.
The system manages establishments, dining zones, tables, menus, menu items, orders, payments, and real-time notifications.

The project includes:

REST API
GraphQL API
SignalR notifications
Entity Framework Core
Unit & Integration Tests
Clean multilayer architecture

🚀 Main Features
Establishments & Tables
Create/edit/deactivate establishments
Manage dining zones and tables
Track free and occupied tables
Menus & Items
Manage menus, categories, and menu items
Set prices and availability
Filter menu items
Orders
Create and manage orders
Add/remove items
Change order status
Track active orders
Payments
Create payments
Calculate total order amount
Complete orders after payment
Real-Time Notifications

SignalR notifications for:

New orders
Status changes
Payments
Table updates
Item availability

🧠 Business Rules
Orders cannot be created for inactive establishments or tables
Unavailable items cannot be ordered
Quantity must be greater than 0
Completed/cancelled orders cannot be edited
Order statuses follow valid transitions
Payments must be valid and positive

🧪 Testing

The project includes:

Minimum 12 unit tests
Minimum 8 integration tests

📄 Documentation

The project contains:

XML comments
Swagger documentation
README instructions
Git commit history
