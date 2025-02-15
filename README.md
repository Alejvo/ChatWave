# API Endpoints
| Method | Endpoints | Action | Source |Params | Body |
| --- | --- | --- | --- | --- | --- |
| GET | /api/users | Get all users | FromQuery | searchTerm (optional) <br> sortColumn (optional) <br> sortOrder (optional) <br> page <br> pageSize ||
| GET | /api/users/id | Get an user by id | FromQuery | id ||
| PUT | /api/users | Update an user | FromForm || {<br> &nbsp;&nbsp;"Id" : "string",<br> &nbsp;&nbsp;"FirstName" : "string",<br> &nbsp;&nbsp;"LastName" : "string",<br> &nbsp;&nbsp;"Email" : "string",<br> &nbsp;&nbsp;"Password" : "string",<br> &nbsp;&nbsp;"Username" : "string",<br> &nbsp;&nbsp;"Birthday" : "string (date)",<br> &nbsp;&nbsp;"ProfileImage" : "string (binary)" <br>} |
| Delete | /api/users/id | Delete an existing user by id | FromQuery | id ||
| POST | /api/users | Create an user | FromForm ||{<br> &nbsp;&nbsp;"FirstName" : "string",<br> &nbsp;&nbsp;"LastName" : "string",<br> &nbsp;&nbsp;"Email" : "string",<br> &nbsp;&nbsp;"Password" : "string",<br> &nbsp;&nbsp;"Username" : "string",<br> &nbsp;&nbsp;"Birthday" : "string (date)",<br> &nbsp;&nbsp;"ProfileImage" : "string (binary)" <br>}|
| POST | /api/users/login | Allow user login | FromBody || {<br> &nbsp;&nbsp;"email" : "string","password" : "string"} |
| GET | /api/groups | Get all groups | FromQuery | searchTerm (optional) <br> sortColumn (optional) <br> sortOrder (optional) <br> page <br> pageSize||
| GET | /api/groups/id | Get a group by id | FromQuery | id ||
| POST | /api/groups | Create a group | FromForm || {<br> &nbsp;&nbsp;"Name" : "string",<br> &nbsp;&nbsp;"Description" : "string",<br> &nbsp;&nbsp;"Image" : "string (binary)"<br>}|
| POST | /api/groups/join | Allow an user to join a group | FromBody || {<br> &nbsp;&nbsp;"groupId" : "string",<br> &nbsp;&nbsp;"userId" : "string"<br>} |
| POST | /api/groups/leave | Allow an user to leave a group | FromBody || {<br> &nbsp;&nbsp;"groupId" : "string",<br> &nbsp;&nbsp;"userId" : "string"<br>}|
| PUT | /api/groups | Update a group | FromForm || {<br> &nbsp;&nbsp;"Id" : "string",&nbsp;&nbsp;"Name" : "string",<br> &nbsp;&nbsp;"Description" : "string",<br> &nbsp;&nbsp;"Image" : "string (binary)"<br>} |
| DELETE | /api/groups/id | Delete an existing group by id | FromQuery | id ||
| POST | /api/messages/user | Send a message to an user | FromBody || { <br> &nbsp;&nbsp;"text" :  "string", "originId" : "string", "destinyId" : "string", "sentAt" : "date" <br>} |
| POST | /api/messages/group | Send a message to an existing group | FromBody || { <br> &nbsp;&nbsp;"text" :  "string", "userId" : "string", "groupId" : "string", "sentAt" : "date" <br>} |
| GET | /api/messages/user | Get the messages between two users | FromBody || receiverId <br> senderId |
| GET | /api/messages/group | Get the messages from an existing group | FromBody || receiverId <br> senderId |
| GET | /api/friends/request | Get all the friend request from a user | FromQuery | userId ||
| POST | /api/friends/request | Send a friend request | FromBody || { <br> &nbsp;&nbsp; "userId" : "string", <br> &nbsp;&nbsp; "friendId : string" <br> } |
| POST | /api/friends/add | Add an user to another user's friend list | FromBody || { <br> &nbsp;&nbsp; "userId" : "string", <br> &nbsp;&nbsp; "friendId : "string" <br> } |
| POST | /api/friends/remove | Remove an user to another user's friend list | FromBody || { <br> &nbsp;&nbsp; "userId" : "string", <br> &nbsp;&nbsp; "friendId : "string" <br>} |