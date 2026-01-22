# 📌 BlazorWithIMS

A learning-focused, demo-oriented **Blazor Server** based Interactive Management System (IMS) project.

BlazorWithIMS is designed to demonstrate **component-based architecture**, **CRUD operations**, **Entity Framework integration**, and **Clean Architecture principles** in a real-world structured project.

---

## 📋 Table of Contents

- Project Overview
- Features
- Technologies
- Prerequisites
- Installation
- Configuration
- Architecture
- Application Capabilities
- Screenshots
- Project Structure
- Database
- Security

---

## 📌 Project Overview

**BlazorWithIMS** is an interactive management system developed for learning and demonstration purposes using **Blazor Server**.

The project showcases:
- Modular component-based UI design
- CRUD operations for multiple entities
- Data persistence with Entity Framework Core
- Clean Architecture for maintainability
- Identity-based authentication and authorization

---

## ✨ Features

### 🧩 Architecture & Structure
- Component-based page and UI design
- Clean Architecture layers
- Maintainable and scalable project structure

### ⚡ Core Functionalities
- Full CRUD operations for multiple entities
- Interactive UI using SignalR WebSockets
- Entity Framework Core for data persistence

### 🔐 Security
- ASP.NET Core Identity integration
- Authentication system
- Authorization with roles

---

## 🛠️ Technologies

| Technology | Purpose |
|------|--------|
| **.NET 8.0** | Application Framework |
| **Blazor Server** | Interactive UI |
| **Clean Architecture** | Project Structure |
| **Entity Framework Core** | ORM |
| **ASP.NET Core Identity** | Authentication & Authorization |

---

## 📦 Prerequisites

- .NET 8.0 SDK
- SQL Server or LocalDB
- Visual Studio 2022+
- Git

---

## 🚀 Installation

### 1. Clone the repository

```bash
git clone https://github.com/onuracarsoy/BlazorWithIMS.git
cd BlazorWithIMS
```

### 2. Open the solution

Open the solution file using **Visual Studio**.

### 3. Restore NuGet packages

```bash
dotnet restore
```

### 4. Configure database (if required)

Update connection string in `appsettings.json`.

### 5. Run the project

```bash
dotnet run
```

or press **F5** in Visual Studio.

---

## ⚙️ Configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=BlazorWithIMSDb;Trusted_Connection=True;"
  }
}
```

---

## 🧱 Architecture

This project follows **Clean Architecture** principles:

- **Presentation Layer** → Blazor UI
- **Application Layer** → Business logic
- **Domain Layer** → Entities & core rules
- **Infrastructure Layer** → EF Core, Identity, persistence

---

## 🎯 Application Capabilities

### Management System Features
- Entity management panels
- CRUD dashboards
- Role-based access
- User management
- Secure authentication

---

## 🖼️ Screenshots


![BLAZORIMS1](https://github.com/user-attachments/assets/a7bc06ce-919e-4a5e-bf90-f4452b2b3d50)

![BLAZORIMS2](https://github.com/user-attachments/assets/f18e8114-edfc-4b61-af46-f51a019d0800)

![BLAZORIMS3](https://github.com/user-attachments/assets/c14ff36a-ecb3-47e5-b1d4-c05ebd9c9cd0)

---

## 📁 Project Structure

```
BlazorWithIMS/
├── Core/              # Domain layer
├── Application/       # Business logic
├── Infrastructure/    # EF Core, Identity
├── Presentation/      # Blazor Server UI
└── Shared/            # Shared models/components
```

---

## 🗄️ Database

```bash
# Create migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

---

## 🔒 Security

- Identity authentication
- Role-based authorization
- Secure data access
- Layered architecture security

---


**Built for learning, demo, and architectural practice using Blazor Server & Clean Architecture** 🚀





