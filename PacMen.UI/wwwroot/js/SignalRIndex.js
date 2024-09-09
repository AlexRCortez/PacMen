"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pacmenHub").build();
document.getElementById("sendButton").disabled = true;

const sButton = document.getElementById("sendButton");




connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messages").appendChild(li);
    li.textContent = `User: ${user} Says ${message}`;


});

//connection.on("ReceiveMessage", function (user, message) {

    

//    // var li = document.createElement("li");
//    var p = document.createElement("p");
//    var h4 = document.createElement("h4");

//    document.getElementById('uName').appendChild(h4)
//    h4.textContent = `${user}`;

//    document.getElementById('uMessage').appendChild(p)
//    p.textContent = `${message}`;


//    sButton.addEventListener("click", () => {


//    });


//});


connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (error) {
    return console.error(error.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (error) {
        return console.error(error.toString());
    });
    event.preventDefault();
});



