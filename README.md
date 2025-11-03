+====================================================================+
|             LeaseLink – Property Management System (web)           |
+====================================================================+


LeaseLink is a comprehensive Property Management System designed to connect Admins, Property Managers, Tenants, and Prospective Tenants through a single, integrated platform.  
It includes a Web Application, which is built using ASP.NET Core MVC. The system enables real-time management of property listings, rental applications, payments, and maintenance requests to improve transparency and efficiency for all stakeholders.


Purpose of the Project:
**********************

This project was developed as part of the Work Integrated Learning (WIL) module.  
Its purpose is to demonstrate team collaboration, software engineering practice, and full-stack development by applying academic theory to a real-world application.


Minimum System Requirements:
****************************

- Operating System: Windows 10 or 11.
- Visual Studio 2022. 
- .NET SDK 8.0 or later  
- Firebase Account (for Database, Authentication & Storage).  


Minimum Hardware Requirements:
******************************

- CPU/PROCESSOR --> 1.8 GHz or faster processor. 
- RAM --> Minimum 4 GB (8 GB recommended).  
- HARD DISK DRIVE (HDD) STORAGE --> Minimum 5 GB of available space.  
- SOLID STATE DRIVE (SSD) STORAGE --> Recommended for faster performance in Visual Studio. 
- DISPLAY RESOLUTION --> Minimum 1280x720 (Full HD 1920x1080 recommended).  


/***************************************************************
        --- LEASELINK PROPERTY MANAGEMENT SYSTEM(Web) ---
 ***************************************************************/


How To Run The Application:
***************************
1. Ensure you have the following installed:
   - Visual Studio 2019/2022.
   - .NET Framework 8.0 or later.
2. Open the solution file **LeaseLink.sln** in Visual Studio 2022.  
3. Update the `appsettings.json` file with Firebase credentials (API key, database URL, storage bucket).  
4. Build the solution (**Ctrl + Shift + B**).  
5. Run the application (**F5** or Start Debugging).  
6. The homepage will display a login screen for Admin, Property Manager, or Tenant access.  


Functionality and Features:
***************************

Admin Dashboard:
- Manage all system users (Tenants, Property Managers, and Admins).  
- Broadcast important announcements.  
- View dashboards showing rent arrears, active leases, and maintenance tasks.  
- Generate and download monthly financial reports.  

Property Manager Dashboard:
- Add, update, and manage property listings.  
- Upload property images to Firebase Storage.  
- Approve or reject rental applications.  
- Manage maintenance requests and assign technicians.  
- Receive reminders for leases nearing expiration.  

:Tenant Dashboard:
- View current lease details and rent payment history.  
- Submit maintenance requests with photo uploads.  
- Receive notifications for upcoming rent payments.  
- Navigate easily through a clean and personalized dashboard.  

Prospective Tenant Portal:
- Browse and filter available properties.  
- Submit rental applications with supporting documents.  
- View property images, pricing, and manager contact details.  

Additional Features:
- Secure Firebase Authentication. 
- Responsive design with consistent Navy Blue and gold colour scheme.  
- Real-time synchronization. 

