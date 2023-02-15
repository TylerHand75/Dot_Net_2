IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'cabin_db_am')
BEGIN
	DROP DATABASE cabin_db_am
	print '' print '*** dropping database cabin_db_am'
END
GO

print '' print '*** creating database cabin_db_am'
GO
CREATE DATABASE cabin_db_am
GO

print '' print '*** using cabin_db_am'
GO
USE [cabin_db_am]
GO

/* Employee table */
print '' print '*** creating employee table'
GO
CREATE TABLE [dbo].[Employee] (
	[EmployeeID]	[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](100)				NOT NULL,
	[Phone]			[nvarchar](13)				NOT NULL,
	[Email]			[nvarchar](100)				NOT NULL,
	[PasswordHash]	[nvarchar](100)				NOT NULL DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID]),
	CONSTRAINT [ak_Email] UNIQUE([Email])
)
GO

/* Employee test records */

print '' print '*** inserting employee test records'
GO
INSERT INTO [dbo].[Employee]
	([GivenName], [FamilyName], [Phone], [Email])
	VALUES
		('Joanne', 'Smith', '3195551111', 'joanne@company.com'),
		('Martin', 'Jones', '3195552222', 'martin@company.com'),
		('Leo', 'Williams', '3195553333', 'leo@company.com'),
		('Maria', 'Perez', '3195554444', 'maria@company.com'),
		('Ahmed', 'Rawl', '3195555555', 'ahmed@company.com')
GO

/* Role table */
print '' print '*** createing Role table'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]		[nvarchar](50)				NOT NULL,
	[Description]	[nvarchar](250)				NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY ([RoleID])
)
GO

/* Role sample records */
print '' print '*** inserting sample role records'
GO
INSERT INTO [dbo].[Role]
	([RoleId], [Description])
	VALUES
		('Admin', 'administratiors user accounts and assigns roles'),
		('Manager', 'manages the inventory of cabins'),
		('Rental', 'runs the rental desk'),
		('CheckIn', 'checks in guest and shows them to cabins'),
		('CheckOut', 'checks guest out and settles their account'),
		('Inspection', 'inspects cabin for missing items and damage'),
		('Prep', 'cleans cabins and prepairs them for rental'),
		('Maintenance', 'performs maintenance and repairs on cabins')
GO

/* EmployeeRole join table to join Employee and Roles */
print '' print '*** creating EmployeeRole table'
GO
CREATE TABLE [dbo].[EmployeeRole] (
	[EmployeeID]	[int]						NOT NULL,
	[RoleID]		[nvarchar](50)				NOT NULL,
	CONSTRAINT [fk_EmployeeRole_EmployeeID]		FOREIGN KEY ([EmployeeID])
		REFERENCES [dbo].[Employee] ([EmployeeID]),
	CONSTRAINT [fk_EmployeeRole_RoleID]		FOREIGN KEY ([RoleID])
		REFERENCES [dbo].[Role] ([RoleID]),
	CONSTRAINT [pk_EmployeeRole] PRIMARY KEY ([EmployeeID], [RoleID])
)
GO

print '' print '*** inserting sample EmployeeRole table'
GO
INSERT INTO [dbo].[EmployeeRole]
	([EmployeeID], [RoleID])
	VALUES
		(100000, 'Admin'),
		(100000, 'Manager'),
		(100001, 'Rental'),
		(100002, 'CheckIn'),
		(100002, 'CheckOut'),
		(100003, 'Inspection'),
		(100003, 'Prep'),
		(100004, 'Maintenance')
GO

/* login related stored procedures */
print '' print '*** creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
)
AS
	BEGIN
		SELECT COUNT([EmployeeID]) AS 'Authenticated'
		FROM		[Employee]
		WHERE		@Email = [Email]
			AND		@PasswordHash = [PasswordHash]
			AND		[Active] = 1
	END
GO

print '' print '*** creating sp_select_employee_by_email'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_email]
(
	@Email 			[nvarchar](100)
)
AS	
	BEGIN
		SELECT [EmployeeID], [GivenName], [FamilyName], 
				[Phone], [Email], [Active]
		FROM	[Employee]
		WHERE	@Email = [Email]
	END     
GO            

print '' print '*** creating sp_select_roles_by_employeeID'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_employeeID]
(
	@EmployeeID			[int]
)
AS
	BEGIN
		SELECT 	[RoleID]
		FROM	[EmployeeRole]
		WHERE	@EmployeeID = [EmployeeID]
	END
GO

print '' print '**creating sp_update_passwordHash'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
(
	@EmployeeID				[int],
	@PasswordHash			[nvarchar](100),
	@OldPasswordHash		[nvarchar](100)
)
AS
	BEGIN
		UPDATE	[Employee]
		  SET	[PasswordHash] = 	@PasswordHash
		WHERE	@EmployeeID = 		[EmployeeID]
		  AND	@OldPasswordHash = 	[PasswordHash]
		  
		RETURN 	@@ROWCOUNT
	END
GO

/* Cabin stuff */

/* CabinType table */
print '' print '*** creating CabinType table'
GO
CREATE TABLE [dbo].[CabinType] (
	[CabinTypeID]	[nvarchar](25)				NOT NULL,
	[Description]	[nvarchar](1000)			NOT NULL,
	CONSTRAINT [pk_CabinTypeID] PRIMARY KEY([CabinTypeID])
)
GO

/* CabinType sample records */
print '' print '*** inserting sample CabinType records'
GO
INSERT INTO [dbo].[CabinType]
	([CabinTypeID], [Description])
	VALUES
		('Rustic', 'Essentially a Baba yaga hut, but without legs.'),
		('Family', 'Two bedrooms with bunk beds for kids.'),
		('Standard', 'Double bed for one or two campers.')
GO

/* Amenities table */
print '' print '*** creating Amenities table'
GO
CREATE TABLE [dbo].[Amenities] (
	[AmenitiesID]	[int] IDENTITY(100000,1)	NOT NULL,
	[CabinTypeID]	[nvarchar](25)				NOT NULL,
	[Description]	[nvarchar](100)				NULL,
	CONSTRAINT [pk_AmenitiesID] PRIMARY KEY([AmenitiesID]),
	CONSTRAINT [fk_CabinTypeID_Amenities] FOREIGN KEY([CabinTypeID])
		REFERENCES [CabinType]([CabinTypeID])
)
GO

/* Amentites sample records */
print '' print '*** inserting sample Amenities records'
GO
INSERT INTO [dbo].[Amenities]
	([CabinTypeID], [Description])
	VALUES
		('Rustic', 'Natural earth floor and open sky view.'),
		('Family', 'Lake View.'),
		('Family', 'Fireplace.'),
		('Family', 'Picnic Table.'),
		('Family', 'Charcoal grill.'),
		('Standard', 'Fire Pit.'),
		('Standard', 'Log benches and table.')
GO



/* CabinStatus table */
print '' print '*** creating cabinStatus table'
GO
CREATE TABLE [dbo].[CabinStatus] (
	[CabinStatusID]	[nvarchar](25)				NOT NULL,
	[Description]	[nvarchar](100)				NULL,
	CONSTRAINT [pk_CabinStatusId] PRIMARY KEY([CabinStatusID])
)
GO

/* CabinStatus sample records */
print '' print '*** inserting sample CabinStatus records'
GO
INSERT INTO [dbo].[CabinStatus]
	([CabinStatusID], [Description])
	VALUES
		('Available', 'Ready to Rent.'),
		('CheckIn', 'Rented, not checked in.'),
		('Occupied', 'Occupied.'),
		('Needs Prep', 'Vacated, needs cleaning.'),
		('Needs Maintenance', 'something is wrong.')
GO




/* Cabin table */
print '' print '*** creating cabin table'
GO
CREATE TABLE [dbo].[Cabin] (
	[CabinID]		[int] IDENTITY(100000,1)	NOT NULL,
	[Trail]			[nvarchar](25)					NOT NULL,
	[Site]			[nvarchar](10)				NOT NULL,
	[CabinTypeID]	[nvarchar](25)				NOT NULL,
	[CabinStatusID]	[nvarchar](25)				NOT NULL,
	[Active]		[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_CabinID] PRIMARY KEY([CabinID]),
	CONSTRAINT [fk_CabinStatusID] FOREIGN KEY([CabinStatusID])
		REFERENCES [CabinStatus]([CabinStatusID]),
	CONSTRAINT [fk_CabinTypeID] FOREIGN KEY([CabinTypeID])
		REFERENCES [CabinType]([CabinTypeID])
)
GO

/* CabinStatus sample records */
print '' print '*** inserting sample Cabin records'
GO
INSERT INTO [dbo].[Cabin]
	([Trail], [Site],[CabinTypeID],[CabinStatusID])
	VALUES
		('Hopalong Trail', 'H-1', 'Standard', 'Available'),
		('Hopalong Trail', 'H-2', 'Family', 'Available'),
		('The Last Roundup', 'L-1', 'Rustic', 'Available'),
		('The Last Roundup', 'H-2', 'Rustic', 'Available'),
		('Drooplong Trail', 'D-1', 'Family', 'Available'),
		('Drooplong Trail', 'D-2', 'Family', 'Available')
GO



print '' print '**creating sp_select_cabins_by_cabinstatusid'
GO
CREATE PROCEDURE [dbo].[sp_select_cabins_by_cabinstatusid]
(
	@CabinStatusID				[nvarchar](25)
)
AS
	BEGIN
		SELECT	[CabinID], [Trail], [Site], [CabinTypeID], [CabinStatusID], [Active]		
		FROM [dbo].[Cabin]
		WHERE @CabinStatusID = [CabinStatusID]	
			AND [Active] = 1
	END
GO

print '' print '*** creating sp_amenities_by_cabintypeid'
GO
create procedure [dbo].[sp_amenities_by_cabintypeid]
(
	@cabintypeid			[nvarchar](25)
)	
AS
	BEGIN
		select	[Description]
		from 	[Amenities]
		WHERE	@cabintypeid = [CabinTypeID]
	END        		
	
GO 