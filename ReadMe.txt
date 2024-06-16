
Add your Sql datasource to the appsettings.json file in the ProfDieHetChecked variable.

Exicute the 'Update-Database' command in the package manager console on the 'DataLayer' layer.

Start the application, the database will automaticly be seeded with a User.

{
  "userName": "thibeault.admin",
  "passWord": "Thibeault123!"
}

Use this to login into the API, everything else is locked behind Authorize.

Except the none existing UI and the OrderHeader controller function, everything works.
The create orderHeader does work, but only in the database and I can't a proper OrderHeader object 
to show in the api out of the database.

They don't exist or work because I made 2 big mistakes early on and now its to late to refactor for the deadline.

	1. I build the database code first and my objects contain the other dataobjects instead of their ID
	   which is causing problem every time I try to add or update a dataobject.

	2. I immediately stated using DTO's while my API is going to connect my UI to the backend.
	   Those DTO's don't pass on the relevant information.