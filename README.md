# sbox-persistence-demo
A demo for persistence in s&box using WebSockets.
This repository contains the s&box gamemode code
for the persistence demo.

### What?
The 'game' demo I have here simply receives data
from a WebSocket server and updates a label with
a click count. Every time you click the button
on the UI, the click count will increment on the
WebSocket server application. If there are two
players in separate game sessions clicking the
button, the click count will update accordingly
for both of the players.

### Why?
Sometimes you want to save information between
game sessions, or sync information across
different running game lobbies. s&box does not
have an embedded database like Garry's Mod, and
servers are supposed to be easily pick-up-and-play,
so WebSockets are a high-performance and smart
solution for data persistence.

I anticipate s&box will have a stronger emphasis
on *games* rather than *game modes*, and I think
there are many genres of games that benefit
dramatically from use of WebSockets for persistence
or advanced networking.

### Ok, but why does your code suck?
I don't know how to program.
