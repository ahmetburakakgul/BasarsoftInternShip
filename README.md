# BasarsoftInternShip

This project belongs to my internship at Başarsoft. It is a news site with CRUD operations.There are buttons on the Home Page where we can register and login,
and we can also list according to categories on this page. There are three ways to enter the site. these entries are made from the same page. Entries are as Admin Author and Member
On the admin page, we can approve the comments made on the news content on our site, at the same time we can delete and edit the categories on our site, edit the news content in the broadcast 
or delete the news. in the same way we can reduce the authority of our writers.

# Backend

This application was made with .Net 5 MVC in visual studio. I used MSSQL for database and code first approach. Entity Framework used to access MSSQL.
The program is abstracted in itself. The project has Entity layer for entities, Data layer to access our data and bussiness layer where I write my rules. 
I used all of these layers on the UI side. For this reason, the program is abstracted in itself and from Loose coupling. So when we change something, 
other objects will not be affected much. I used OOP, Design Patterns and SOLID principles in this project.
