"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("requestButton").disabled = true;

connection.on("ReceiveMessage", function () {
  location.reload();
});

connection.start().then(function() {
  document.getElementById("requestButton").disabled = false;
}).catch(function (err) {
  return console.error(err.toString());
});

document.getElementById("requestButton").addEventListener("click", function(event) {
  connection.invoke("SendMessage").catch(function (err) {
    return console.error(err.toString());
  });
  event.preventDefault();
});
