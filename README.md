# File Processing API

The File Processing API is a secure, RESTful web service built with ASP.NET Core that processes uploaded files in either CSV or JSON format. It includes API Key-based authentication, file processing (average calculation for CSV, filtering for JSON), and basic logging of file processing events.

## Features

- **API Key Authentication**: Ensures secure access to the file upload endpoint.
- **File Processing**: 
  - **CSV**: Calculate the average of a specified column (e.g., Value).
  - **JSON**: Filter data based on a specific condition (e.g., City == "New York").
- **File Tracking and Reporting**: Logs processed files and provides a report on processed files.

---

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop) (if you plan to use Docker for containerization)
- A tool like [Postman](https://www.postman.com/) or `curl` for testing the API endpoints

---

## Build the Project

To build the project, you need the .NET SDK installed.

### Steps:

1. **Clone the repository**:
   ```bash
   git clone  https://github.com/villamuchocarlo/VILLAMUCHO_09272024.git
   cd FileProcessingAPI

1. **Build the project**:
   ```bash
   dotnet build

---

## Run the Project

There are two ways to run the application: locally or using Docker.

### Run Locally

1. **Restore dependencies (if needed)**:
   ```bash
   dotnet restore

2. **Run the API**:
   ```bash
   dotnet run

3. **The API will be running at**:
   ```bash
   http://localhost:5000

### Run with Docker

1. **Build the Docker image**:
   ```bash
   docker build -t file-processing-api .

2. **Run the Docker container**:
   ```bash
   docker run -d -p 8080:80 file-processing-api

3. **The API will be available at**:
   ```bash
   http://localhost:8080

---

## Using the API

You can use Postman, cURL, or any API testing tool to test the API endpoints.

### API Endpoints
  | Method | Endpoint                       | Description                                        |
  |--------|--------------------------------|----------------------------------------------------|
  | POST   | /api/fileupload/upload         | Upload and process a CSV or JSON file              |
  | GET    | /api/fileupload/report         | Get a report of the files that have been processed |

### Request Headers

All requests to the API must include the following header for API Key authentication:
   - **Headers**:
      - `X-API-KEY: <your-api-key>`

### Sample Requests
   
1. **Upload a CSV File (Calculate Average)**:
   - Sample File (.csv):
        ```sql
        ID,Value,Description
        1,10,First item
        2,20,Second item
        3,30,Third item
        4,40,Fourth item
        5,50,Fifth item
   - cURL Example:
        ```bash
        curl -H "X-API-KEY: your-api-key-here" -F "file=@path/to/data.csv" https://localhost:5000/api/fileupload/upload
   - Postman Example:
      - Set POST request URL to `https://localhost:5001/api/fileupload/upload`
      - Add header: `X-API-KEY: your-api-key-here`
      - Upload the CSV file under the Body > form-data section

2. **Upload a JSON File (Filter Data)**:
   - Sample JSON File (.json):
        ```json
        [
          { "ID": 1, "Name": "John", "Age": 28, "City": "New York" },
          { "ID": 2, "Name": "Jane", "Age": 32, "City": "Los Angeles" },
          { "ID": 3, "Name": "Mike", "Age": 40, "City": "Chicago" },
          { "ID": 4, "Name": "Sara", "Age": 25, "City": "New York" }
        ]
   - cURL Example:
        ```bash
        curl -H "X-API-KEY: your-api-key-here" -F "file=@path/to/data.json" https://localhost:5000/api/fileupload/upload
   - Postman Example:
      - Set POST request URL to `https://localhost:5001/api/fileupload/upload`
      - Add header: `X-API-KEY: your-api-key-here`
      - Upload the JSON  file under the Body > form-data section
 
3. **Get File Processing Report**:
   - cURL Example:
        ```bash
        curl -X GET -H "X-API-KEY: your-api-key-here" https://localhost:5000/api/fileupload/report
   - Postman Example:
      - Set GET request URL to `https://localhost:5000/api/fileupload/report`
      - Add header: `X-API-KEY: your-api-key-here`

### Sample Requests
   - **CSV Processing:**:
        ```json
        {
          "Result": "CSV file processed",
          "Average": 30
        }
   - **JSON Processing**:
        ```json
        {
          "Result": "JSON file processed",
          "Data": [
            { "ID": 1, "Name": "John", "Age": 28, "City": "New York" },
            { "ID": 4, "Name": "Sara", "Age": 25, "City": "New York" }
          ]
        }
---

## Unit Tests

Unit tests are included to validate file processing logic and API key authentication.

### Run Unit Tests

1. **Navigate to the test project**:
   ```bash
   cd FileProcessingAPI.Tests

2. **Run the tests**:
   ```bash
   dotnet test

You should see output indicating which tests have passed or failed.

---

## Docker Cleanup (Optional)

To stop and remove the Docker container when you're finished:

1. List running containers to find the container ID:
   ```bash
   docker ps

2. Stop the container:
   ```bash
   docker stop <container-id>

2. Remove the container:
   ```bash
   docker rm <container-id>

---

## File Tracking Feature

The API tracks all processed files and provides a report. You can access the report through the /api/fileupload/report endpoint. The report includes:
- File Name
- File Type (CSV or JSON)
- Processing Result (e.g., average value or filtered data)
- Timestamp of when the file was processed

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.

---

## Contact

For any issues or questions, please reach out at villamuchocarlo@gmail.com.
