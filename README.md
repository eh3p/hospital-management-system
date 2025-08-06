# 🏥 Hospital Management System

A comprehensive ASP.NET Core Razor Pages application with Identity authentication, featuring a complete hospital management system with CRUD operations, ModelState validation, TempData, and Session support.

## 👨‍💻 **Creator**
**Ehab Ashraf Mourad**

## 🚀 **Live Demo**
The application is running at: `http://localhost:5000`

## ✨ **Features Implemented**

### ✅ Identity Integration
- ASP.NET Core Identity with Individual User Accounts
- User authentication and authorization
- Role-based access control

### ✅ Hospital Management Entities
- **Patient Management**: Complete CRUD operations with validation
- **Doctor Management**: Complete CRUD operations with validation  
- **Appointment Management**: Entity relationships between patients and doctors

### ✅ ModelState Validation
- Comprehensive validation attributes on all models
- Client-side and server-side validation
- Custom validation messages
- Email uniqueness validation
- Phone number format validation
- Date and time validation

### ✅ TempData Implementation
- Success messages after CRUD operations
- Status messages across page redirects
- Dashboard welcome messages
- Form submission confirmations

### ✅ Session Support
- User session tracking
- Page access logging
- Form interaction tracking
- Session-based data persistence
- Session information display on dashboard

### ✅ CRUD Operations (Razor Pages)
- **Create**: Add new patients and doctors with validation
- **Read**: List and view details of patients and doctors
- **Update**: Edit existing records with validation
- **Delete**: Soft delete with relationship checks

### ✅ Authorization Examples
- `[Authorize]`: Requires any authenticated user
- `[Authorize(Roles = "Admin")]`: Role-based access control
- `[AllowAnonymous]`: Public access pages

## 📁 **Project Structure**

```
HospitalManagementSystem/
├── Models/
│   ├── Patient.cs          # Patient entity with validation
│   ├── Doctor.cs           # Doctor entity with validation
│   └── Appointment.cs      # Appointment entity with relationships
├── Data/
│   └── ApplicationDbContext.cs  # EF Core context with hospital entities
├── Pages/
│   ├── Hospital/
│   │   ├── Dashboard.cshtml     # Main dashboard with statistics
│   │   ├── Patients/            # Patient CRUD pages
│   │   │   ├── Index.cshtml     # List patients
│   │   │   ├── Create.cshtml    # Add new patient
│   │   │   ├── Edit.cshtml      # Edit patient
│   │   │   ├── Delete.cshtml    # Delete patient
│   │   │   └── Details.cshtml   # View patient details
│   │   ├── Doctors/             # Doctor CRUD pages
│   │   │   └── Index.cshtml     # List doctors
│   │   ├── Admin/               # Admin-only pages
│   │   │   └── AdminPanel.cshtml # Role-based access example
│   │   └── Public/              # Public pages
│   │       └── About.cshtml     # Anonymous access example
│   └── Shared/
│       └── _Layout.cshtml       # Navigation with FontAwesome
└── Program.cs                   # Session configuration
```

## 🔧 **Key Features Demonstrated**

### 1. ModelState Validation
```csharp
[Required(ErrorMessage = "Name is required")]
[StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
[Display(Name = "Full Name")]
public string Name { get; set; } = string.Empty;
```

### 2. TempData Usage
```csharp
[TempData]
public string? StatusMessage { get; set; }

// In action method
TempData["StatusMessage"] = $"Patient '{Patient.Name}' has been created successfully!";
```

### 3. Session Management
```csharp
// Store session data
HttpContext.Session.SetString("CurrentUser", User.Identity?.Name ?? "Unknown");

// Retrieve session data
var currentUser = HttpContext.Session.GetString("CurrentUser");
```

### 4. Authorization Attributes
```csharp
[Authorize]                    // Requires authentication
[Authorize(Roles = "Admin")]   // Requires Admin role
[AllowAnonymous]               // Allows anonymous access
```

## 🗄️ **Database Schema**

### Patients Table
- Id (Primary Key)
- Name (Required, Max 100 chars)
- Email (Required, Unique, Email format)
- PhoneNumber (Required, Phone format)
- DateOfBirth (Required, Date)
- Gender (Required)
- Address (Required, Max 200 chars)
- MedicalHistory (Optional)
- RegistrationDate (Auto-set)
- IsActive (Boolean)

### Doctors Table
- Id (Primary Key)
- Name (Required, Max 100 chars)
- Email (Required, Email format)
- PhoneNumber (Required, Phone format)
- Specialization (Required, Max 100 chars)
- LicenseNumber (Required, Max 50 chars)
- YearsOfExperience (Required, Range 0-50)
- IsAvailable (Boolean)
- HireDate (Auto-set)
- IsActive (Boolean)

### Appointments Table
- Id (Primary Key)
- PatientId (Foreign Key)
- DoctorId (Foreign Key)
- AppointmentDate (Required, Date)
- AppointmentTime (Required, Time)
- AppointmentType (Required)
- Symptoms (Optional)
- Diagnosis (Optional)
- Prescription (Optional)
- Status (Required, Default: "Scheduled")
- Notes (Optional)
- CreatedDate (Auto-set)

## 🚀 **How to Run**

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Installation Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/hospital-management-system.git
   cd hospital-management-system
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Run database migrations:**
   ```bash
   dotnet ef database update
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```

5. **Access the application:**
   - Open browser and go to `http://localhost:5000`
   - Register a new user account
   - Explore the hospital management features

## 🧪 **Features to Test**

### 1. Authentication & Authorization
- Register a new user account
- Login with credentials
- Access protected pages (Dashboard, Patients, Doctors)
- Try accessing Admin Panel (requires Admin role)
- Access public About page (no authentication required)

### 2. CRUD Operations
- **Create**: Add new patients and doctors
- **Read**: View lists and details
- **Update**: Edit existing records
- **Delete**: Remove records (soft delete)

### 3. Validation
- Try submitting forms with invalid data
- Test email uniqueness validation
- Test required field validation
- Test format validation (phone, email, dates)

### 4. TempData & Session
- Create/Edit/Delete records and see success messages
- Check session information on dashboard
- Navigate between pages and observe session persistence

### 5. Navigation
- Use the responsive navigation menu
- Test dropdown menus
- Navigate between different sections

## 🛠️ **Technology Stack**

- **Framework**: ASP.NET Core 9.0
- **UI**: Razor Pages with Bootstrap 5
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Icons**: FontAwesome 6.0
- **Validation**: Data Annotations with ModelState
- **State Management**: TempData and Session

## 🔒 **Security Features**

- **Authentication**: User registration and login
- **Authorization**: Role-based access control
- **Validation**: Server-side and client-side validation
- **Session Security**: HttpOnly cookies, secure session management
- **Data Protection**: Soft deletes, relationship integrity checks

## 📊 **Screenshots**

### Dashboard
![Dashboard](https://via.placeholder.com/800x400/007bff/ffffff?text=Hospital+Dashboard)

### Patient Management
![Patient Management](https://via.placeholder.com/800x400/28a745/ffffff?text=Patient+Management)

### Doctor Management
![Doctor Management](https://via.placeholder.com/800x400/ffc107/000000?text=Doctor+Management)

## 🔮 **Future Enhancements**

- Appointment scheduling system
- Doctor availability management
- Patient medical history tracking
- Reporting and analytics
- Email notifications
- Advanced search and filtering
- API endpoints for mobile apps
- Multi-tenant support

## 🤝 **Contributing**

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 **License**

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 **Contact**

**Ehab Ashraf Mourad**
- GitHub: [@yourusername](https://github.com/yourusername)
- Email: your.email@example.com

## 🙏 **Acknowledgments**

- ASP.NET Core team for the excellent framework
- Bootstrap team for the responsive UI components
- FontAwesome for the beautiful icons
- Entity Framework team for the ORM

---

**Note**: This project demonstrates best practices for ASP.NET Core development including proper separation of concerns, validation, security, and user experience design.

**Created with ❤️ by Ehab Ashraf Mourad** 