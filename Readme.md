# How to start

* Create Azure resources using RTL.TVMaze.AzureResourceGroup (The template doesn't deploy Fucntion app, create it manually.)
* Deploy database using RTL.TVMaze.SqlDatabase
* Deploy RTL.TVMaze.API (ASP.NET Core) and configure connection string
* Deploy RTL.TVMaze.Scraper.Function (Azure Function) and configure app setting 'ConnectionStrings:TVMazeDatabase'
