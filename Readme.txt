Overview of the overall structure of the application: this application is a basic Inventory management system 
which can be used to create, modify, delete and browse Products along with Category and Unit of measurement. 

This application contains 3 separate components as –
	1.	Database Scripts: to create database (sql server 2012 or higher)
	2.	InventoryServices: it consist of web apis for database layer.
	3.	InventoryUi: it consists of Ui part using ASP.Net web forms.
These 3 components can be installed on the same or different machines.

Software required:
	1.	Visual Studio 2017 or higher.
	2.	Sql server Management studio 2012 or higher.
	3.	Chrome / Firefox

Steps to create the database (database name ‘Inventory’) (Sql server 2012 or higher)-
(Note: Sometimes the database creation through script may fail due to following reasons-
	a. if proper path for database files is not given in the script.
	b. Permission issue.
So, a better approack can be to create database in Sql server Management studio through right click the Databases 
node in the left pane and use New Database menu option.)

	1.	Download the project from https://github.com/manishankarghoshkatni/Derivco_01.git as a zip archive.
	2.	Open the archive and navigate to Db_Scripts folder under the root folder. It contains 2 script files-
		a.	Db_Create.sql – it contains database creation scripts.
		b.	Obj_Create.sql – it contains Table and other objects creation scripts like stored procedures along with 
			record insert commands.
	3.	Open Sql server Management studio –
		a.	Open Db_Create.sql using File -> Open menu command. It contains a database creation command like as 
			follows –

		CREATE DATABASE [Inventory]
		 CONTAINMENT = NONE
		 ON  PRIMARY 
		( NAME = N'Inventory', FILENAME = N'<PATH TO DATABASE LOCATION>\Inventory.mdf' , SIZE = 5120KB , 
			MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
		 LOG ON 
		( NAME = N'Inventory_log', FILENAME = N' <PATH TO DATABASE LOCATION >\Inventory_log.ldf' , SIZE = 2048KB , 
			MAXSIZE = 2048GB , FILEGROWTH = 10%)
		GO

		b.	Replace < PATH TO DATABASE LOCATION> with a proper file location where the current sql server instance 
			has the rights to create database.
	4.	Execute the script.
	5.	Right click the Databases node in the left pane in Sql server Management studio click Refresh menu option to 
		ensure that the database has been created successfully with database name as ‘Inventory’.

Steps to create other database objects like table, stored proc etc –
	1.	Open Obj_Create.sql script file using File -> Open menu command in Sql server Management studio.
	2.	Execute the script.
	3.	Right click the database node ‘Inventory’ in the left pane in Sql server Management studio and click Refresh 
		menu option to see the newly created database objects.

Start the web api application:
	1.	Navigate to ‘Assignment\ Api\InventoryServices’ folder.
	2.	Double click the InventoryServices.sln file to open the project in visual studio 2017 or higher.
	3.	Open the web.config from the root folder.
	4.	Locate the connection string with the name as ‘InventoryEntities’ and find the ‘data source’ option in it. Replace it with a proper value. 
		It takes the format as ‘Database host\sql server instance name’ like mydbhost\sqlexpress 
	5.	If the sql server is using Sql authentication, then do the following-
		a.	Remove integrated security=True from connection string.
		b.	Use uid=<user id for sql authentication>
		c.	Use pwd=<password>
	6.	Save it.
	7.	Run the project. It will open a browser window and display ‘Product Service running …’ once started.

Start the Ui application:
	1.	Navigate to ‘Assignment\UI\InventoryUi’ folder.
	2.	Double click the InventoryUi.sln file to open the project in visual studio 2017 or higher.
	3.	If the web api and UI applications are running on the same machine, then steps through 4 to 7 can be skipped.
	4.	Open the web.config from the root folder.
	5.	Locate the following line under  appSettings as ‘<add key="WebApiRootUrl" value="http://localhost:49732/" />’
	6.	Change ‘localhost’ with the target machine name or ip of the machine where the web api service is running.
	7.	Save the file.
	8.	Run the project. It will open in browser and display the UI. It contains 3 main menu options-
		a.	Product-
			i.	Add: to add new Product.
			ii.	Search / Modify/Delete: to Search / Modify/Delete existing Product.
		b.	Category-
			i.	Add: to add new Category.
			ii.	Search / Modify/Delete: to Search / Modify/Delete existing Category.
		c.	Unit-
			i.	Add: to add new Unit.
			ii.	Search / Modify/Delete: to Search / Modify/Delete existing Unit.

			
			Filter Options:- Id,Name and Category. If any of these is blank it will populate all the records.
			Please make sure that the Unit and Category are added first before creating a new product.
			Price field will round off upto 2 digits after decimals.
			

Log file path-
Log file is created at \Logs\InventoryServicesLogs.txt


