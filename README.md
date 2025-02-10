# WebHookTimer


Features:
Create a timer with a webhook URL for event notification.
Check how much time is left for an active timer.
Uses DB to store timer data.

Technologies:
Language: C#
Framework: .NET Core 6 or higher
Database: SQL Server

Edit appsettings.json and update the connection string:
"ConnectionStrings": {
    "myDbConn": "Server=ServerName;Database=TimersDB;Trusted_Connection=True;"
  }

Restore dependencies:
dotnet restore

Run the application:
dotnet run

API Endpoints:
Set Timer:
http://localhost:5180/WebHook/SetTimer
request body, for example:
{
  "hours": 0,
  "minutes": 1,
  "seconds": 30,
  "webhookUrl": "http/s://{domain}/api/timers/outpost"
}

Get Timer Status:
http://localhost:5180/WebHook/GetTimerStatus?id="Your Id"
