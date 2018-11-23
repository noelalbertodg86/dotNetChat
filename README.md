# dotNetChat

For the development of the application several technologies were taken into account, all on .net with the IDE VISUAL STUDIO and several solutions.

1- the database connection and management was done using the EntityFramework library and a n-layer design pattern ["Entity", "Data", "BusinessClass" solution].
In the application you can see the structure and relationships between databases, always trying to maintain a simple system that meets the requirements.
All sent messages are stored in the database, as well as user profiles.


2 - The development of the Robot was made in a console project ["Bot" solution] that must be started from the execution of the .exe or installed as a windows service, at this moment a cyclic server is going to start listening for the queue of the rabbitMQ by default the queue name is bot. Once invoked, the stock quote is downloaded from the internet and returned to the user. The robot is listening to any new request. The singleton pattern was applied to the execution of the robot.

3 - For the development of chatRoom, the technology based on websockes SignalR was used, creating an API .NET CORE project, where signalR  and HUB controllers interact to provide all the necessary functionalities to the system. Login, management, persistence and dissemination of messages to connected users. ["Server" solution]

4 - For the development of the client part, a webbrowser with windows forms was generated, which invokes a previously designed and tested html page that provides the visual functionality of Login and ChatRoom.
["Client" solution]

5 - In addition, utilitarian solutions for the handling of RabbitMQ queues were created without duplicating code [solution "RabbitManager"] and [solution "Encrypt"] to avoid the flat management of passwords.

6 - A test project [solution "ChatUnitTestProyect"] is created as a solution for test management, examples of how to administer the tests are included in the same.


To start the project
1- Start the Robot ["Bot"]
2- Start the solution ["Server"]
3- Use the browser of the solution ["Client"] or the browser Crome of the solution ["Server"]

Nuget package used:
EntityFramework
Microsoft.AspNetCore.App
Microsoft.Diagnostics.Tracing.EventSource.Redist
Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.Json
Microsoft.NETCore.App
MSTest.TestAdapter
Newtonsoft.Json
RabbitMQ.Client
System.Configuration.ConfigurationManager


Thanks for the opportunity !!!!!