# Optify Booking Task – ASP.NET Core 9 Web API

## Project Overview
Optify Booking Task is a multi-user booking management API built using ASP.NET Core 9 and Entity Framework Core 9.  
It allows users to manage Trips and Reservations, providing full CRUD operations for reservations with user and trip association.

The project follows Clean Architecture principles and is structured into multiple layers for maintainability, scalability, and testability.

## Technologies Used
- ASP.NET Core 9 Web API
- Entity Framework Core 9 (Code-First)
- SQL Server
- AutoMapper
- Swashbuckle / Swagger
- Repository Pattern + Unit of Work
- Custom Exception Handling
- Dependency Injection
- C# 12 / .NET 9 SDK

## Architecture Overview
The project follows a Clean Architecture approach divided into:
- Domain Layer: Contains entities and core business logic.
- Application Layer: Contains DTOs, interfaces, mapping profiles, and services.
- Infrastructure Layer: Implements data access using EF Core and repositories.
- API Layer: Exposes endpoints, handles requests/responses, and integrates Swagger documentation.

## Database Setup & Seeding
The application uses Code-First Migrations with seeded data for:
- Users
- Trips

### Steps:
1. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "BookingContext": "Server=.;Database=BookingContext;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   }
   ```
2. Run the following commands:
   ```bash
   dotnet ef database update
   ```
3. The database will be created and pre-populated with seed data.

## Running the Project
1. Clone the repository:
   ```bash
   git clone https://github.com/shahdsolliman/OptifyBookingTask.git
   ```
2. Navigate to the project folder:
   ```bash
   cd OptifyBookingTask
   ```
3. Build the solution:
   ```bash
   dotnet build
   ```
4. Run the API:
   ```bash
   dotnet run --project OptifyBookingTask.API
   ```
5. The API will start at:
   ```
   https://localhost:7141
   http://localhost:5151
   ```

## API Endpoints

### Reservations
| Method | Endpoint | Description |
|---------|-----------|-------------|
| GET | /api/reservations | Retrieve all reservations |
| GET | /api/reservations/{id} | Retrieve a specific reservation by ID |
| POST | /api/reservations | Create a new reservation |
| PUT | /api/reservations/{id} | Update an existing reservation |
| DELETE | /api/reservations/{id} | Delete a reservation by ID |

### Example Request (POST)
```json
{
  "customerName": "Ali",
  "tripId": 1,
  "userId": 1,
  "reservationDate": "2025-10-08T10:00:00",
  "notes": "Some notes here"
}
```

### Example Response (201 Created)
```json
{
  "id": 7,
  "customerName": "Ali",
  "tripName": "Saint Catherine Hike",
  "cityName": "Sinai",
  "reservationDate": "2025-10-08T10:00:00",
  "creationDate": "2025-10-08T01:03:59.1896469",
  "notes": "Some notes here",
  "userEmail": "admin@optify.com"
}
```

## Error Handling
Custom error responses are implemented using structured response models:
- ApiResponse
- ApiValidationErrorResponse
- ApiExceptionResponse

Example (404 Not Found):
```json
{
  "statusCode": 404,
  "message": "ReservationToReturnDto with ID 10 was not found."
}
```

Example (400 Validation Error):
```json
{
  "message": "Validation error",
  "errors": [
    {
      "field": "CustomerName",
      "error": ["Customer name is required."]
    }
  ]
}
```

## Swagger Documentation
Swagger UI is integrated and available at:
```
https://localhost:7141/swagger
```
It provides interactive documentation for all endpoints, including example requests and responses.

## Git Commit Structure
Commits follow Conventional Commit naming for clarity:
- feat: New feature
- fix: Bug fix
- refactor: Code improvement
- chore: Maintenance or non-functional changes
- docs: Documentation updates

Example commits:
- feat(reservations): implement full CRUD operations
- refactor(mapping): enhance AutoMapper profiles
- chore(dto): add validation attributes and XML docs

---

## Razor Pages UI (Client)

To complement the Web API, a **Razor Pages UI** project has been added to demonstrate how the API can be consumed from a lightweight frontend interface.  
The UI allows users to **view**, **create**, **update**, and **delete reservations** through an intuitive web interface that communicates directly with the API.

### Features
- Integrated with the existing ASP.NET Core 9 Web API.  
- Displays a list of all reservations fetched from `/api/reservations`.  
- Supports adding, editing, and deleting reservations through Razor Pages forms.  
- Real-time validation and success/error notifications.  
- Fully responsive layout with clean, minimal design.

### Project Structure
```
OptifyBookingTask/
│
├── OptifyBookingTask.API/          --> Web API Project
├── OptifyBookingTask.Application/  --> Application Layer (Services, DTOs, Interfaces)
├── OptifyBookingTask.Domain/       --> Entities and Core Models
├── OptifyBookingTask.Infrastructure/ --> Data Access & EF Core Configurations
└── OptifyBookingTask.UI/           --> Razor Pages Project (Client)
```

### Configuration
1. Navigate to the **UI project folder**:
   ```bash
   cd OptifyBookingTask.UI
   ```
2. In `appsettings.json`, configure the API base URL:
   ```json
   "ApiSettings": {
     "BaseUrl": "https://localhost:7141/api/"
   }
   ```
3. The UI uses **HttpClient** to communicate with the API.  
   Ensure both projects (`API` and `UI`) are running simultaneously.

### Running the Razor Pages UI
1. Build and run the API project first:
   ```bash
   dotnet run --project OptifyBookingTask.API
   ```
2. In a new terminal, run the Razor Pages project:
   ```bash
   dotnet run --project OptifyBookingTask.UI
   ```
3. Open your browser and navigate to:
   ```
   https://localhost:7200
   ```

### UI Preview
**Home Page (Reservations List)**  
Displays all reservations fetched from the API in a responsive table with options to edit or delete.

**Create Reservation Page**  
Form to add a new reservation with client-side validation and server-side integration.

**Edit Reservation Page**  
Allows users to update existing reservations directly via API calls.

---

## Deployment Notes
- Both projects can be hosted independently or as part of the same solution.  
- For production, ensure that:
  - CORS is configured properly in the API.
  - Connection strings and API base URLs are updated in both projects.
  - HTTPS is enforced across all endpoints.
- Recommended hosting setup:
  - **API** on Azure App Service or IIS.
  - **UI** hosted on the same server or separate instance for scalability.

---

## Author
**Shahd Soliman**  
Backend Developer – ASP.NET Core | Entity Framework Core  
Cairo, Egypt  
📧 shahd.soliman2050@gmail.com  
🔗 [GitHub](https://github.com/shahdsolliman) | [LinkedIn](https://www.linkedin.com/in/shahd-soliman-0590872a3)
