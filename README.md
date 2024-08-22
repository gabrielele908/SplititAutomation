
Explanation of the tests UI and API:
This C# program automates the process of testing an online transaction platform. It uses Selenium to log into a web application, retrieve an access token, and then perform API testing with that token. The program first navigates the website, interacts with the login form, and extracts the access token from the browser's local storage or cookies. Once the token is retrieved, it makes a series of API calls, simulating different transaction scenarios by varying parameters like email, transaction amount, and payment details. The program logs responses to ensure the platform handles various cases correctly, allowing for thorough testing of the system's functionality.
The project was implemented in two ways:
First implementation everything was written under a main plan in a synchronized way.
A second implementation was written by using POM (page model) and best practice in C#. 
The Project Structure by POM:
 
![image](https://github.com/user-attachments/assets/0a48e30d-f3b2-45b4-80f5-01216249846c)

Requirement packages:
Selenium.WebDriver
Selenium.WebDriver.ChromeDriver
Selenium.Support
System.Net.Http.Json
NUnit
NUnit3TestAdapter
NUnit.ConsoleRunner
NunitLite
