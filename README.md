# 🏥 Hospital Management System API

[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue.svg)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-9.0-blue.svg)](https://docs.microsoft.com/en-us/ef/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![GitHub](https://img.shields.io/badge/GitHub-eh3p-green.svg)](https://github.com/eh3p)

A comprehensive REST API for managing hospital operations including patients, doctors, and appointments. Built with ASP.NET Core 9.0, Entity Framework Core, and SQLite database.

## 👨‍💻 **Creator**
**Ehab Ashraf Mourad**
- GitHub: [eh3p](https://github.com/eh3p)
- Email: ehabashraf1667@gmail.com

## 🚀 Features

- **Patient Management**: CRUD operations for patient records
- **Doctor Management**: CRUD operations for doctor profiles with specializations
- **Appointment Management**: Schedule and manage appointments with validation
- **Search Functionality**: Search patients, doctors, and appointments
- **API Documentation**: Swagger/OpenAPI documentation
- **Code-First Database**: Entity Framework Core with SQLite
- **AutoMapper**: Clean mapping between entities and DTOs
- **Service Layer**: Business logic separation
- **Error Handling**: Comprehensive error handling and logging

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 9.0
- **Database**: SQLite with Entity Framework Core
- **ORM**: Entity Framework Core (Code-First)
- **API Documentation**: Swagger/OpenAPI
- **Mapping**: AutoMapper
- **Authentication**: ASP.NET Core Identity (ready for implementation)
- **Logging**: Built-in ASP.NET Core logging

## 📋 Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code
- Git

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/eh3p/hospital-management-system.git
cd hospital-management-system/HospitalManagementSystem
```

### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Build the Project

```bash
dotnet build
```

### 4. Run Database Migrations

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The API will be available at:
- **API Base URL**: `http://localhost:5000/api`
- **Swagger Documentation**: `http://localhost:5000/swagger`

## 📚 API Endpoints

### Patients
- `GET /api/patients` - Get all patients
- `GET /api/patients/{id}` - Get patient by ID
- `POST /api/patients` - Create new patient
- `PUT /api/patients/{id}` - Update patient
- `DELETE /api/patients/{id}` - Delete patient
- `GET /api/patients/search?searchTerm={term}` - Search patients

### Doctors
- `GET /api/doctors` - Get all doctors
- `GET /api/doctors/{id}` - Get doctor by ID
- `POST /api/doctors` - Create new doctor
- `PUT /api/doctors/{id}` - Update doctor
- `DELETE /api/doctors/{id}` - Delete doctor
- `GET /api/doctors/search?searchTerm={term}` - Search doctors
- `GET /api/doctors/specialization/{specialization}` - Get doctors by specialization

### Appointments
- `GET /api/appointments` - Get all appointments
- `GET /api/appointments/{id}` - Get appointment by ID
- `POST /api/appointments` - Create new appointment
- `PUT /api/appointments/{id}` - Update appointment
- `DELETE /api/appointments/{id}` - Delete appointment
- `GET /api/appointments/patient/{patientId}` - Get appointments by patient
- `GET /api/appointments/doctor/{doctorId}` - Get appointments by doctor
- `POST /api/appointments/search` - Search appointments

## 📊 Database Schema

### Patient
- Id (Primary Key)
- Name
- Email
- PhoneNumber
- DateOfBirth
- Gender
- Address
- MedicalHistory
- RegistrationDate
- IsActive

### Doctor
- Id (Primary Key)
- Name
- Email
- PhoneNumber
- Specialization
- LicenseNumber
- YearsOfExperience
- IsAvailable
- HireDate
- IsActive

### Appointment
- Id (Primary Key)
- PatientId (Foreign Key)
- DoctorId (Foreign Key)
- AppointmentDate
- AppointmentTime
- AppointmentType
- Symptoms
- Diagnosis
- Prescription
- Status
- Notes
- CreatedDate

## 🔧 Configuration

The application uses `appsettings.json` for configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=app.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 🧪 Testing the API

### Using Swagger UI

1. Navigate to `http://localhost:5000/swagger`
2. Explore available endpoints
3. Test API calls directly from the browser

### Using curl

```bash
# Get all patients
curl -X GET "http://localhost:5000/api/patients"

# Create a new patient
curl -X POST "http://localhost:5000/api/patients" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Ehab",
    "email": "Ehab@email.com",
    "phoneNumber": "01029236732",
    "dateOfBirth": "1990-01-01T00:00:00",
    "gender": "Male",
    "address": "Egypt"
  }'
```

## 📁 Project Structure

```
HospitalManagementSystem/
├── Controllers/          # API Controllers
├── Data/                # Database context and migrations
├── DTOs/                # Data Transfer Objects
├── Mapping/             # AutoMapper profiles
├── Models/              # Entity models
├── Services/            # Business logic services
├── Program.cs           # Application entry point
├── appsettings.json     # Configuration
└── API_DOCUMENTATION.md # Detailed API documentation
```

## 🔒 Security Considerations

- Currently, the API is publicly accessible
- For production use, implement proper authentication and authorization
- Consider using JWT tokens or API keys
- Implement rate limiting
- Use HTTPS in production

## 🚀 Deployment

### Local Development
```bash
dotnet run
```

### Production
```bash
dotnet publish -c Release
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Contact

**Ehab Ashraf Mourad**
- GitHub: [eh3p](https://github.com/eh3p)
- Email: ehabashraf1667@gmail.com

## 🙏 Acknowledgments

- ASP.NET Core team for the excellent framework
- Entity Framework team for the ORM
- Swagger team for the API documentation tools
- AutoMapper team for the mapping library

---

**Note**: This is a development version. For production use, ensure proper security measures are implemented.

**Created with ❤️ by Ehab Ashraf Mourad** 