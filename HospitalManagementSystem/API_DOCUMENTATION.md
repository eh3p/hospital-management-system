# Hospital Management System API Documentation

## Overview

The Hospital Management System API provides comprehensive endpoints for managing patients, doctors, and appointments. The API is built using ASP.NET Core 9.0 with Entity Framework Core and follows RESTful principles.

## Base URL

- **Development**: `http://localhost:5000/api`
- **Swagger Documentation**: `http://localhost:5000/api-docs`

## Authentication

Currently, the API endpoints are publicly accessible. For production use, implement proper authentication and authorization.

## Data Models

### Patient
```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john.doe@email.com",
  "phoneNumber": "+1234567890",
  "dateOfBirth": "1990-01-01T00:00:00",
  "gender": "Male",
  "address": "123 Main St, City, State",
  "medicalHistory": "No known allergies",
  "registrationDate": "2024-01-01T00:00:00",
  "isActive": true
}
```

### Doctor
```json
{
  "id": 1,
  "name": "Dr. Jane Smith",
  "email": "jane.smith@hospital.com",
  "phoneNumber": "+1234567890",
  "specialization": "Cardiology",
  "licenseNumber": "MD123456",
  "yearsOfExperience": 10,
  "isAvailable": true,
  "hireDate": "2020-01-01T00:00:00",
  "isActive": true
}
```

### Appointment
```json
{
  "id": 1,
  "patientId": 1,
  "patientName": "John Doe",
  "doctorId": 1,
  "doctorName": "Dr. Jane Smith",
  "appointmentDate": "2024-01-15T00:00:00",
  "appointmentTime": "14:30:00",
  "appointmentType": "Consultation",
  "symptoms": "Chest pain",
  "diagnosis": "Angina",
  "prescription": "Nitroglycerin",
  "status": "Scheduled",
  "notes": "Follow up in 2 weeks",
  "createdDate": "2024-01-01T00:00:00"
}
```

## API Endpoints

### Patients

#### Get All Patients
```http
GET /api/patients
```

**Response**: `200 OK`
```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "john.doe@email.com",
    "phoneNumber": "+1234567890",
    "dateOfBirth": "1990-01-01T00:00:00",
    "gender": "Male",
    "address": "123 Main St, City, State",
    "medicalHistory": "No known allergies",
    "registrationDate": "2024-01-01T00:00:00",
    "isActive": true
  }
]
```

#### Get Patient by ID
```http
GET /api/patients/{id}
```

**Response**: `200 OK`
```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john.doe@email.com",
  "phoneNumber": "+1234567890",
  "dateOfBirth": "1990-01-01T00:00:00",
  "gender": "Male",
  "address": "123 Main St, City, State",
  "medicalHistory": "No known allergies",
  "registrationDate": "2024-01-01T00:00:00",
  "isActive": true
}
```

#### Create Patient
```http
POST /api/patients
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john.doe@email.com",
  "phoneNumber": "+1234567890",
  "dateOfBirth": "1990-01-01T00:00:00",
  "gender": "Male",
  "address": "123 Main St, City, State",
  "medicalHistory": "No known allergies"
}
```

**Response**: `201 Created`

#### Update Patient
```http
PUT /api/patients/{id}
Content-Type: application/json

{
  "name": "John Doe Updated",
  "phoneNumber": "+1234567891"
}
```

**Response**: `200 OK`

#### Delete Patient
```http
DELETE /api/patients/{id}
```

**Response**: `204 No Content`

#### Search Patients
```http
GET /api/patients/search?searchTerm=john
```

**Response**: `200 OK`

### Doctors

#### Get All Doctors
```http
GET /api/doctors
```

**Response**: `200 OK`

#### Get Doctor by ID
```http
GET /api/doctors/{id}
```

**Response**: `200 OK`

#### Create Doctor
```http
POST /api/doctors
Content-Type: application/json

{
  "name": "Dr. Jane Smith",
  "email": "jane.smith@hospital.com",
  "phoneNumber": "+1234567890",
  "specialization": "Cardiology",
  "licenseNumber": "MD123456",
  "yearsOfExperience": 10
}
```

**Response**: `201 Created`

#### Update Doctor
```http
PUT /api/doctors/{id}
Content-Type: application/json

{
  "specialization": "Interventional Cardiology",
  "isAvailable": false
}
```

**Response**: `200 OK`

#### Delete Doctor
```http
DELETE /api/doctors/{id}
```

**Response**: `204 No Content`

#### Search Doctors
```http
GET /api/doctors/search?searchTerm=cardiology
```

**Response**: `200 OK`

#### Get Doctors by Specialization
```http
GET /api/doctors/specialization/cardiology
```

**Response**: `200 OK`

### Appointments

#### Get All Appointments
```http
GET /api/appointments
```

**Response**: `200 OK`

#### Get Appointment by ID
```http
GET /api/appointments/{id}
```

**Response**: `200 OK`

#### Create Appointment
```http
POST /api/appointments
Content-Type: application/json

{
  "patientId": 1,
  "doctorId": 1,
  "appointmentDate": "2024-01-15T00:00:00",
  "appointmentTime": "14:30:00",
  "appointmentType": "Consultation",
  "symptoms": "Chest pain",
  "notes": "Patient reports chest pain"
}
```

**Response**: `201 Created`

#### Update Appointment
```http
PUT /api/appointments/{id}
Content-Type: application/json

{
  "status": "Completed",
  "diagnosis": "Angina",
  "prescription": "Nitroglycerin"
}
```

**Response**: `200 OK`

#### Delete Appointment
```http
DELETE /api/appointments/{id}
```

**Response**: `204 No Content`

#### Get Appointments by Patient
```http
GET /api/appointments/patient/{patientId}
```

**Response**: `200 OK`

#### Get Appointments by Doctor
```http
GET /api/appointments/doctor/{doctorId}
```

**Response**: `200 OK`

#### Search Appointments
```http
GET /api/appointments/search?patientId=1&fromDate=2024-01-01&toDate=2024-01-31&status=Scheduled
```

**Query Parameters**:
- `patientId` (optional): Filter by patient ID
- `doctorId` (optional): Filter by doctor ID
- `fromDate` (optional): Filter from date (YYYY-MM-DD)
- `toDate` (optional): Filter to date (YYYY-MM-DD)
- `status` (optional): Filter by status (Scheduled, Completed, Cancelled)
- `appointmentType` (optional): Filter by appointment type

**Response**: `200 OK`

#### Check Time Slot Availability
```http
GET /api/appointments/availability?doctorId=1&appointmentDate=2024-01-15&appointmentTime=14:30:00
```

**Query Parameters**:
- `doctorId` (required): Doctor ID
- `appointmentDate` (required): Appointment date (YYYY-MM-DD)
- `appointmentTime` (required): Appointment time (HH:MM:SS)
- `excludeAppointmentId` (optional): Appointment ID to exclude from check

**Response**: `200 OK`
```json
true
```

## Error Responses

### 400 Bad Request
```json
{
  "message": "A patient with this email already exists."
}
```

### 404 Not Found
```json
{
  "message": "Patient with ID 999 not found"
}
```

### 500 Internal Server Error
```json
{
  "message": "An error occurred while retrieving patients"
}
```

## Business Rules

### Patients
- Email addresses must be unique
- Cannot delete patients with active appointments
- All required fields must be provided when creating a patient

### Doctors
- Email addresses must be unique
- License numbers must be unique
- Cannot delete doctors with active appointments
- All required fields must be provided when creating a doctor

### Appointments
- Appointment date cannot be in the past
- Time slots must be available (no conflicts)
- Patient and doctor must be active
- Doctor must be available
- Cannot delete past or current appointments
- Time slots are 30-minute intervals

## Database Schema

The system uses SQLite with the following main tables:

- **Patients**: Patient information and medical history
- **Doctors**: Doctor information, specialization, and availability
- **Appointments**: Appointment scheduling with patient and doctor relationships
- **AspNetUsers**: User authentication (ASP.NET Core Identity)

## Getting Started

1. **Install Dependencies**:
   ```bash
   dotnet restore
   ```

2. **Run Migrations**:
   ```bash
   dotnet ef database update
   ```

3. **Start the Application**:
   ```bash
   dotnet run
   ```

4. **Access Swagger Documentation**:
   Navigate to `http://localhost:5000/api-docs`

## Testing the API

You can test the API using:

1. **Swagger UI**: Interactive documentation at `/api-docs`
2. **Postman**: Import the endpoints
3. **curl**: Command-line testing
4. **Any HTTP client**: REST API testing

## Sample API Calls

### Create a Patient
```bash
curl -X POST "http://localhost:5000/api/patients" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "email": "john.doe@email.com",
    "phoneNumber": "+1234567890",
    "dateOfBirth": "1990-01-01T00:00:00",
    "gender": "Male",
    "address": "123 Main St, City, State",
    "medicalHistory": "No known allergies"
  }'
```

### Create a Doctor
```bash
curl -X POST "http://localhost:5000/api/doctors" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Dr. Jane Smith",
    "email": "jane.smith@hospital.com",
    "phoneNumber": "+1234567890",
    "specialization": "Cardiology",
    "licenseNumber": "MD123456",
    "yearsOfExperience": 10
  }'
```

### Create an Appointment
```bash
curl -X POST "http://localhost:5000/api/appointments" \
  -H "Content-Type: application/json" \
  -d '{
    "patientId": 1,
    "doctorId": 1,
    "appointmentDate": "2024-01-15T00:00:00",
    "appointmentTime": "14:30:00",
    "appointmentType": "Consultation",
    "symptoms": "Chest pain"
  }'
```

## Support

For API support and questions, please refer to the Swagger documentation or contact the development team. 