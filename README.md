# 🏥 Hospital Management System API

[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0) 
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue.svg)](https://dotnet.microsoft.com/apps/aspnet) 
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-9.0-blue.svg)](https://docs.microsoft.com/en-us/ef/) 
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 
[![GitHub](https://img.shields.io/badge/GitHub-eh3p-green.svg)](https://github.com/eh3p)

## 👨‍💻 Creator
**Ehab Ashraf Mourad**  
📧 Email: ehabashraf1667@gmail.com  
💻 GitHub: [eh3p](https://github.com/eh3p)

## 🚀 Features
- **Patient Management**: Full CRUD operations with medical history
- **Doctor Management**: Specialization tracking with availability
- **Appointment System**: Intelligent scheduling with validation
- **Search Functionality**: Across all entities with filters
- **API Documentation**: Swagger/OpenAPI integration
- **SQL Server**: Robust database backend
- **AutoMapper**: Clean DTO transformations

## 🛠️ Technology Stack
| Component | Technology |
|-----------|------------|
| Framework | ASP.NET Core 9.0 |
| Database | SQL Server 2022 |
| ORM | Entity Framework Core 9.0 |
| API Docs | Swagger/OpenAPI |
| Mapping | AutoMapper |
| Auth | ASP.NET Core Identity |

## 📋 Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code
- [Git](https://git-scm.com/)

## 🚀 Quick Start
```bash
# Clone repository
git clone https://github.com/eh3p/hospital-management-system.git
cd hospital-management-system/HospitalManagementSystem

# Install dependencies
dotnet restore

# Build project
dotnet build

# Setup database
dotnet ef database update

# Run application
dotnet run

# After running the application, open your browser and go to
http://localhost:5000
