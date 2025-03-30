# JET Tech Challenge
### AccountService
This class was refactored to handle exceptions and notify users why specific requests failed.


This is an example of a successful request displayed using Postman:
![image](https://github.com/user-attachments/assets/f417dbbc-afad-42c0-80c9-05d9501106cf)

Below are examples of failed requests displayed using Postman:
![image](https://github.com/user-attachments/assets/3d88abf4-5f2e-4634-991a-0ba18485b383)
![image](https://github.com/user-attachments/assets/1df267c8-3134-4e83-8b5d-25b28e698868)
![image](https://github.com/user-attachments/assets/83c5052f-f7b4-41a6-b0c2-a6c6007e0d09)

### Database Integration
Using the Entity Framework and AccountDbContext class, the code was connected to SQL Server. The tables were created, and data was seeded.
![image](https://github.com/user-attachments/assets/17e0891d-566c-423d-82ff-9c50cb6473b8)

### Docker
The code is dockerized by adding a Dockerfile. The image was created. To be able to fully use the code with SQL Server, a docker compose was added to create the connection between SQL Server container and program docker image.
![image](https://github.com/user-attachments/assets/fe4511e4-82e9-4dd5-a805-033f90be2882)
