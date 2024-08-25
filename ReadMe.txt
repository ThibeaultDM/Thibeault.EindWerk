
Add your Sql datasource to the appsettings.json file in the ProfDieHetChecked variable.

Exicute the 'Update-Database' command in the package manager console on the 'DataLayer' layer.

Start the application, the database will automaticly be seeded with a User.

{
  "userName": "thibeault.admin",
  "passWord": "Thibeault123!"
}

Use this to login into the API, everything else is locked behind Authorize.

Except the none existing UI and the OrderHeader controller, everything works.
The create orderHeader sort of works, but only in the database and I can't show a proper OrderHeader object 
to in the api from the database. I havn't yet found how to stop the circular reference, but there should be a
way to do it in the api.

My UI doesn't exist because I made 2 big mistakes early on and now its to late to refactor for the application.

	1. I build the database code first and my objects contain the other dataobjects instead of their ID
	   which is causing problem every time I try to add or update a dataobject.

	2. I immediately stated using DTO's while my API is going to connect my UI to the backend.
	   Those DTO's don't pass on information that the UI needs.