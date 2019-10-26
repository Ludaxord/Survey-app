# THIS PROJECT IS REUPLOAD FROM SEPTEMBER 2018. IT HAS BEEN DISCONTINUED

# Survey App

Simple survey application that contains two section:

1. front-end - created with **React + Redux**, main npm packages that are included in project:
* React
* Redux
* Redux-Form
* Redux-Thunk
* Axios
* Lodash
* Bootstrap
2. back-end - created with **.NET Core**

 **Front-end**:

 Contains client application with 5 main sections:
 When user turn on app. Home Screen show up. Than user can navigate to Login and Register screen. Other screen are disabled.
 When User Register account confirm mail should be sent to them. After that user can login into app and fill questionaire.

 **Back-end**:

 Contains all app logic and also connection to database (SQL Server). Backend is connected with frontend and all data that user provides to app like answers, logins, registers are collected, sorted and stored here.

 **Tests**

 To run backend app in test environment you should have dotnet installed (I used visual studio for mac and terminal to run app).
 Then just simply open terminal in ./SurveyAPI directory and type:

 **dotnet run**

 then server should start running. Please Remember to create database before.

 To run frontend app in test environment you should have node.js (npm) installed.
 Then open terminal in .frontend/survey-front-end directory and type:

 **npm install**
 **npm run start**

 then front-server should start running.

 frontend server running on:

 **http://localhost:3000**

 backend server running on:

 **https://localhost:5001**

 **USAGE**

 * When you successfully run application please go to Register tab and fill form. After send data please check Your email. Please on register type valid email because you will receive email.
