# Public Transport Fleet & Incident Management System

An ASP.NET Core MVC web application designed for public transport dispatchers to manage vehicle fleets, track incidents, and monitor operations.

## Features
- **Fleet & Depot Management**: Hybrid routing and custom route constraints for vehicle side numbers and depot listings.
- **Watchlist**: Temporary session-based vehicle tracking with JSON serialization extensions.
- **Incident Reporting**: Secure asynchronous file uploads with whitelist validation, GUID renaming, and remote AJAX validation.
- **Cross-Cutting Concerns**: Custom action filters for request auditing and execution time metrics.
- **Localization**: Multi-language support utilizing `IViewLocalizer` and `.resx` resource files.
- **Containerization**: Production-ready multi-stage `Dockerfile` built for .NET 8 runtime listening on port 8080.
- **Automated Testing**: Comprehensive unit and integration testing suite using xUnit and `WebApplicationFactory`.

## How to Run

1. **Run the application locally:**
   ```bash
   dotnet run --project TransitDesk.Web
   ```

2. **Run automated tests:**
    ```bash
    dotnet test
    ```

3. **Run with Docker:**
    ```bash
    docker build -t transitdesk .
    docker run -d -p 8080:8080 --name transitdesk-instance transitdesk
    ```

    To access the application enter this address in your web browser.
    - English: http://localhost:8080/?culture=en-US
    - Polish: http://localhost:8080/?culture=pl-PL
