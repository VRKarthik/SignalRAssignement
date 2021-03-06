# SignalRAssignement
## Requirements
Create a .Net Core 3.1 server console app and a simple client (a simple HTML page with Javascript)
Server and client have to communicate via ASP.NET Core SignalR.

### Server
.Net Core 3.1 Console App (Server)
- Track one directory via FileSystemWatcher and push all file events Changed, Created, Deleted, Renamed to all connected SignalR clients
    * Contained data: Filename, DateTime, EventType
- Implement a SignalR Hub class which support following methods:
    * Change(string text) => Append text to first file in the server directory
    * Create(string filename) => Create or recreate a file with filename and current DateTime as file content in the server directory
    * Delete() => Delete first file in the server directory
    * Rename(string newFilename) => Rename the first file in the server directory to newFilename

### Client
SignalR Client (.Net or JavaScript)
- Every WebClient (n-possible) show all file events in a simple list as text (Data: Filename, DateTime, EventType)
- Add 1 Textbox + 4 Buttons:
    * If user clicks on 1. Button: Append the text from the textbox to first file in server directory
    * If user clicks on 2. Button: Create or recreate file with filename (from textbox) in server directory and current DateTime as file content
    * If user clicks on 3. Button: Delete first file in server directory
    * If user clicks on 4. Button: Rename first file in server directory to text from the textbox

## Project structure
### FileMonitor.DataContracts
Contains FileEventData class, which will be shared among the server and clients.
### FileMonitor.Service
This is a repository or library which will provide all file related methods.
### SignalRFileMonitor
This is the .Net Core Console app, which will host the SignalR Hub.
### SignalRJsClient
This is a simple website, which will acts as SignalRClient.

### Running the project
- Server directory path is configured in the App.config file in the SignalRFileMonitor folder, change the directory path if needed.
- Run the SignalRConsoleHost project.
- Open the index.html from SignalRJsClient using a web browser.
