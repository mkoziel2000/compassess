# PFL Coding Assessment

## Contents
**PflApp** Folder contains the main application
**PflAppTests** contains unit and integration tests for the various components

## Notes on the code
The project was built on .NET Core.  jQuery and Bootstrap where used for the UI.

Much of the code is .NET Core scaffolding and was not modified from its original state.  The code modules to focus on for assessment are anything that has "Pfl" in its filename/foldername. 

DataEncryption utility module was created to represent configuration encryption

Index.cshtml represents the page that drives the UI.

OrdersController and HomeController where added/modified to handle product display and Ordering capabilities

The actual PFL Api integration is encapsulated within a service called PflApiSvc.

Models where created to represent the various payloads.  Payload objects represent the response uses a schema-less approach that allows for class specialization (JsonExtensionData).  I felt this was a way to cut down on the number of individual response objects needed to be created in order to support the current response payload design of the API.

Dependency Injection is used to allow modules to easily support test mocking as well as making configuration, logging, and service objects accessible across all controllers and services.

## Security
DataEncryption Utility module was created to aid in securing credentials to the PFL Api within the JSON configuration file of appsettings.json.  The security is based on a self-signed cert "pfl.pfx" and employs AES-512

## Current Capabilities

  * Ability to register an account into the system.  This will be used as the Customer Account for ordering.
  * Login
  * Selection of a Product
  * Basic ordering of the product and ability to adjust quantity.
  * Security of credentials

  
## How To Run
This is a .NET Core project so either one of the following can be done:

### Option 1. Build

  * Install .NET Core 2.0 SDK
  * Restore using "dotnet restore" command
  * Building using "dotnet build" command
  * Run using "dotnet run" command
  * Commandline window will have the URL address to hit in order to work with the application
  
### Option 2. Install
 
   * Unzip pflapp-runtime.zip
   * Execute PflApp.exe (Windows7 x84 or later OS's)
   * Commandline window will have the URL address to hit in order to work with the application
   
  