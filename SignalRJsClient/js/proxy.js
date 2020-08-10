var connection =  new signalR.HubConnectionBuilder()
                    .withUrl("http://localhost:5003/filemonitorhub")
                    .build();

connection.on("OnFileChanged", function(message){
    console.log(message);
    var eventData = JSON.parse(message);
    let menu = document.getElementById('eventLogs');
    let li = document.createElement('li');
    li.textContent = "(Data: " + eventData.FileName + ", " + eventData.DateTime + ", " + eventData.EventType +")";
    menu.appendChild(li);
});

connection.start().catch(function(err) {
    return console.error(err.toString());
});

function appendClick() {
    var fileNameOrContent = document.getElementById("fileNameOrContent").value;
    var method = "ManageFile";
    connection.invoke(method, fileNameOrContent, 0).catch(function(err) {
        console.log(err);
    });
}

function createClick() {
    var fileNameOrContent = document.getElementById("fileNameOrContent").value;
    var method = "ManageFile";
    connection.invoke(method, fileNameOrContent, 1).catch(function(err) {
        console.log(err);
    });
}

function deleteClick() {
    var fileNameOrContent = document.getElementById("fileNameOrContent").value;
    var method = "ManageFile";
    connection.invoke(method, fileNameOrContent, 2).catch(function(err) {
        console.log(err);
    });
}

function renameClick() {
    var fileNameOrContent = document.getElementById("fileNameOrContent").value;
    var method = "ManageFile";
    connection.invoke(method, fileNameOrContent, 3).catch(function(err) {
        console.log(err);
    });
}