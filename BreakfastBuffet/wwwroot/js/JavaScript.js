"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/KitchenHub").build();

connection.on("RecieveMessage", function (numAdult, numChild, date) {
  var encodedMsg = "Adults: " + numAdult + " ,Children: " + numChild + " ,Total: " + (numChild + numAdult) + " ,Date: " + timestamp;
  var li = document.createElement("li");
  li.textContent = encodedMsg;
  document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
}).catch(function (err) {
  return console.error(err.toString());
});