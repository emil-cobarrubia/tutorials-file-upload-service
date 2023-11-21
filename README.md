# PatientManager-API-BackEnd-Eval

## Disclaimer
This application is intended to be for evaluation and educational purposes only.  This should not be used for processing or holding real data.

## Set the Database connection
In the `PatientManager-API-Backend-Eval\Contexts\PrototypesDevContext.cs`, update the connection strings to a database you have access to.

## Create the Patients Table
This solution makes use of only one table, PATIENTS.
Create the PATIENTS table, using the file, `PatientManager-API-Backend-Eval\Contexts\SQL-CreateTable-Patients.sql`.

## Run the Solution; test the API in Swagger
Once a Database has been connected and the PATIENTS table created, the API can now run locally.

## Make note of the API URL
You'll need to keep track of two URLs for two different API controllers.
1. Patients API URL
2. FileUplaod API URL

These URLs will need to be updated in the front-end Angular solution so the application knows where it'll be sending requests to.

An example of the API URLs will look something like this.
1. Patients API URL - `https://localhost:7191/api/Patients`
2. FileUplaod API URL - `https://localhost:7191/api/FileUpload`

The URLs and ports may be different on your side once it's running.  Once you have the URLs handy, you'll need to upodate the 
service classes in the Angular solution:
1. `angular-patientmanager-frontend-eval\src\app\services\patient.service.ts`
2. `angular-patientmanager-frontend-eval\src\app\services\fileupload.service.ts`

Each service has a variable for the URL above the constructor that will need to be updated.
