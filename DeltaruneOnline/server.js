const WebSocket = require('ws');

const ws = new WebSocket.Server({ port: 1273 });

console.log("Server Started");


ws.on('connection', (wss) => {

  console.log("A new Client Connected")

  wss.send('Welcome to the Server!');


  wss.on('message', (message) => {

    console.log(`Server Received: ${message}`);


    wss.send('Got your Message: ' + message);

  });
});
