IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'MNCS_VAL_db')
BEGIN
	DROP DATABASE MNCS_VAL_db
	print '' print '*** dropping database MNCS_VAL_db'
END
GO

print '' print '*** creating database MNCS_VAL_db'
GO
CREATE DATABASE MNCS_VAL_db
GO

print '' print '*** using MNCS_VAL_db'
GO
USE [MNCS_VAL_db]
GO


print '' print '*** creating User table'
GO
CREATE TABLE [dbo].[Users] (
	[UserID]		[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NUll,
	[FamilyName]	[nvarchar](100)				NOT NULL,
	[GamerTag] 		[nvarchar](50)				NOT NULL,
	[Phone]			[nvarchar](13)				NOT NULL,
	[Email]			[nvarchar](100)				NOT NULL,
	[PasswordHash]	[nvarchar](100)				NOT NULL  DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]						NOT NULL DEFAULT 1,

	CONSTRAINT [pk_UserID] PRIMARY KEY([UserID]),
	CONSTRAINT [ak_Email] UNIQUE([Email])
)
GO


print '' print '*** inserting User test records'
GO
INSERT INTO [dbo].[Users]
		([GivenName], [FamilyName], [GamerTag], [Phone], [Email])
    VALUES
		('Tyler', 'Hand','MasterMittens', '3195551111', 'tylerh@mncs.com'),
		('Spencer', 'Weyland','Cerspen', '3195552222', 'spencerw@mncs.com'),
		('Alex', 'Finfrock','EvilChaos', '3195553333', 'alexf@mncs.com'),
		('Spencer', 'Dove','DoveStepp','3195554444', 'spencerd@mncs.com'),
		('Harry', 'Majerus','Indexdothtml', '3195556666', 'harrym@mncs.com'),
		('Allan', 'Shark','PopSharky', '3195557777', 'allans@mncs.com'),
		('Avdyl', 'Jasiqi','EcoFriendly', '3195558888', 'avdylj@mncs.com'),
		('Ryan', 'Willson','MechanicalSnail', '3195559999', 'ryanw@mncs.com'),
		('Joshua', 'Phistur','CheemsBurbger', '3196661111', 'joshuap@mncs.com'),
		('Cal', 'Ster','Calster', '3196662222', 'cals@mncs.com')
GO

/* Role table and data */

print '' print '*** creating Role table'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]		[nvarchar](50)			NOT NULL,
	[Description]	[nvarchar](250)			NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY ([RoleID])
)
GO

print '' print '*** inserting sample role records'
GO
INSERT INTO [dbo].[role]
		([RoleID], [Description])
	VALUES
		('Admin', 'administers user accounts and assigns roles'),
		('Commissioner', 'manages the player stats'),
		('Player', 'plays in the league'),
		('Captain', 'Leads team'),
		('General Manager', 'Leads franchise')
GO

/* UserRole join table to join User and ROLES */

print '' print '*** creating UserRole table'
GO
CREATE TABLE [dbo].[UserRole] (
	[UserID]	[int] 					NOT NULL,	
	[RoleID]		[nvarchar](50)			NOT NULL,	
	CONSTRAINT [fk_UserRole_UserID] FOREIGN KEY ([UserID])
		REFERENCES [dbo].[Users]([UserID]),	
	CONSTRAINT [fk_UserRole_RoleID] FOREIGN KEY ([RoleID])
		REFERENCES [dbo].[Role]([RoleID]),
	CONSTRAINT [pk_UserRole] PRIMARY KEY ([UserID], [RoleID])
)
GO

print '' print '*** inserting sample UserRole records'
GO
INSERT INTO [dbo].[UserRole]
		([UserID], [RoleID])
	VALUES
		(100000, 'Admin'),
		(100001, 'Commissioner'),
		(100002, 'Player'),
		(100003, 'Captain'),
		(100004, 'General Manager')
GO
GO

/* login-related stored procedures */

print '' print '*** creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email				[nvarchar](100),
	@PasswordHash		[nvarchar](100)
)
AS
	BEGIN
		SELECT 	COUNT([UserID]) AS 'Authenticated'
		 FROM	[Users]
		WHERE	@Email = [Email]
		  AND	@PasswordHash = [PasswordHash]
		  AND	[Active] = 1
	END
GO

print '' print '*** creating sp_select_user_by_email'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email				[nvarchar](100)			
)
AS
	BEGIN
		SELECT 	[UserID], [GivenName], [FamilyName],[GamerTag], 
				[Phone], [Email], [Active]
		FROM	[Users]
		WHERE	@Email = [Email]
	END
GO

print '' print '*** creating sp_select_roles_by_UserID'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_UserID]
(
	@UserID			[int]			
)
AS
	BEGIN
		SELECT 	[RoleID]
		FROM	[UserRole]
		WHERE	@UserID = [UserID]
	END
GO
print '' print '*** creating sp_select_Player_by_GamerTag'
GO
CREATE PROCEDURE [dbo].[sp_select_Player_by_GamerTag]
(
	@GamerTag			[nvarchar] (50)			
)
AS
	BEGIN
		SELECT 	[GamerTag],[GivenName],[FamilyName]
		FROM	[Users]
		WHERE	@GamerTag = [GamerTag]
	END
GO

print '' print '**creating sp_select_GivenName_by_UserID'
GO
CREATE PROCEDURE [dbo].[sp_select_GivenName_by_UserID]
(
	@GivenName		[nvarchar] (50)
)
AS
	BEGIN
		SELECT[UserID]	
		FROM [Users]
		WHERE @GivenName = [GivenName]	
	END
GO

print '' print '*** creating sp_update_passwordHash'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
(
	@UserID				[int],
	@PasswordHash			[nvarchar](100),
	@OldPasswordHash		[nvarchar](100)
)
AS
	BEGIN
		UPDATE 	[Users]
		  SET 	[PasswordHash] = 	@PasswordHash
		WHERE	@UserID = 		[UserID]
		  AND	@OldPasswordHash = 	[PasswordHash]
		  
		RETURN 	@@ROWCOUNT
	END
GO
print '' print '*** creating Stats table'
GO
CREATE TABLE [dbo].[Stats] (
	[UserID] 		[int] 	NOT NULL,
	[RankID]		[int] 	IDENTITY(100,1)		NOT NULL,
	[Ranks]			[nvarchar] (20) not null,
	[K/DRatio] 		[nvarchar]	(5)	NOT NULL,
	[ACS] 			[nvarchar]	(5)		not null ,
	

	CONSTRAINT [pk_RankID] PRIMARY KEY ([RankID]),
	CONSTRAINT [fk_UserStats_UserID] FOREIGN KEY ([UserID])
		REFERENCES [dbo].[Users]([UserID])	

)
GO


print '' print '*** inserting STATS records'
GO
INSERT INTO [dbo].[Stats]
		([UserID],[Ranks],[K/DRatio], [ACS])
    VALUES
		(100000,'Diamond1','1.45','260'),
		(100001,'Diamond1','1.09 ','160'),
		(100002,'Diamond1','1.65','285'),
		(100003,'Diamond1','1.35','234'),
		(100004,'Diamond1','1.25','254'),
		(100005,'Diamond1','1.15','248'),
		(100006,'Diamond1','1.05','213'),
		(100007,'Diamond1','1.67','135'),
		(100008,'Diamond1','1.23','143'),
		(100009,'Diamond1','1.82','195')
GO


print '' print '**creating sp_select_Stats_by_UserID'
GO
CREATE PROCEDURE [dbo].[sp_select_Stats_by_Rank]
(
	@Ranks	[nvarchar] (50)
)
AS
	BEGIN
		SELECT	[RankID], [Ranks], [K/DRatio], [ACS]	
		FROM [Stats]
		WHERE @Ranks	 = [Ranks]
					
	END
GO




print '' print '*** creating Location table'
GO
CREATE TABLE [dbo].[Location] (
	[UserID]		[int] 			NOT NULL,
	[States] 		[nvarchar](50)	NOT NULL,
	[city] 			[nvarchar](50)	NOT NULL,
	[zipcode] 		[int] 			not null,
	[Active]		[bit]			NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_Location] PRIMARY KEY ([zipcode]),
	CONSTRAINT [fk_location_UserID] FOREIGN KEY ([UserID])
		REFERENCES [dbo].[Users]([UserID]),	
	
)
GO



print '' print '*** inserting Location records'
GO
INSERT INTO [dbo].[Location]
		([UserID],[States],[city],[zipcode])
    VALUES
		(100000,'Iowa','Alburnett',52202),
		(100001,'Iowa','Toddville ',52403),
		(100002,'Iowa','Springville',52103),
		(100003,'Iowa','Cedar Rapids',52402),
		(100004,'Nebraska','Omaha',68138),
		(100005,'Nebraska','Omaha',68137),
		(100006,'Nebraska','Omaha',68132),
		(100007,'minnesota','Minneapolis',55426),
		(100008,'minnesota','Minneapolis',55429),
		(100009,'minnesota','Minneapolis',55435)
GO



print '' print '*** creating Tournament table'
GO
CREATE TABLE [dbo].[Tournament] (
	[UserID]		[int] 			NOT NULL,
	[TournamentID]	[int]			NOT NULL,
	[Teams] 		[nvarchar](50)	NOT NULL,
	[TeamsRanking] 	[int] 			NOT NULL,
	[Active]		[bit]			NOT NULL DEFAULT 1,
	CONSTRAINT [pk_Tournament] PRIMARY KEY ([TournamentID]),
	CONSTRAINT [fk_Tournament_UserID] FOREIGN KEY ([UserID])
		REFERENCES [dbo].[Users]([UserID]),	
	
)
GO
print '' print '*** inserting Tournament records'
GO
INSERT INTO [dbo].[Tournament]
		([UserID],[TournamentID], [Teams], [TeamsRanking])
	VALUES
		(100000,1,'ST. CLOUD FLYERS'   	 , 3 ),
		(100001,2,'ST. PAUL SENATORS'  	 , 1 ),
		(100002,3,'BURNSVILLE INFERNO' 	 , 2),
		(100003,4,'BEMIDJI LUBERJACKS' 	 , 5),
		(100004,5,'BLOOMINGTON MAULERS'	 , 4),
		(100005,6,'BRAINERD VICTORY'   	 , 6),
		(100006,7,'MINNETONKA BARONS'  	 , 10 ),
		(100007,8,'ROCHESTER RHYTHM'   	 , 9),
		(100008,9,'HIBBING RANGERS'      , 7),
		(100009,10,'MINNEAPOLIS MIRACLES', 8)


print '' print '**creating sp_select_tournmants_by_teams'
GO
CREATE PROCEDURE [dbo].[sp_select_tournmants_by_teams]
(
	@Teams	[nvarchar] (50)
)
AS
	BEGIN
		SELECT	[UserID], [TournamentID], [Teams]	
		FROM [Tournament]
		WHERE @Teams	 = [Teams]
					
	END
GO


print '' print '*** creating Game Maps table'
GO
CREATE TABLE [dbo].[GameMaps] (
	[Maps] 			[nvarchar](50)	NOT NULL,
	
	[Description] 	[nvarchar](500)	NOT NULL,
	
	CONSTRAINT [pk_GameMaps] PRIMARY KEY ([Maps])	
	
)
GO

print '' print '*** inserting Maps records'
GO
INSERT INTO [dbo].[GameMaps]
		([Maps],[Description])
	VALUES
		('HAVEN','Beneath a forgotten monastery, a clamour emerges from rival Agents clashing to control three sites. There’s more territory to control, but defenders can use the extra real estate for aggressive pushes.'),
		('ASCENT','An open playground for small wars of position and attrition divide two sites on Ascent. Each site can be fortified by irreversible bomb doors once they’re down you’ll have to destroy them or find another way.'),
		('BIND','Two sites. No middle. Gotta pick left or right. What’s it going to be then? Both offer direct paths for attackers and a pair of one-way teleporters make it easier to flank.'),
		('BREEZE','Take in the sights of historic ruins or seaside caves on this tropical paradise.')
		
GO
print '' print '*** creating Game Maptype table'
GO
CREATE TABLE [dbo].[MapType] (
	[MapType]		[nvarchar](50)	NOT NULL,
	
	CONSTRAINT [pk_MapType] PRIMARY KEY ([MapType])	
	
)
GO
print '' print '*** inserting MapTypes records'
GO
INSERT INTO [dbo].[MapType]
		([MapType])
	VALUES
		('3 Site'),
		('2 Site'),
		('Close Quaters'),
		('Long Range')
		
GO





print '' print '*** creating Games table'
GO
CREATE TABLE [dbo].[Games] (
	[GameID] 		[int]	IDENTITY(10,1) NOT NUll,
	[Maps]			[nvarchar](50)	NOT NULL,
	[MapType] 		[nvarchar](50)	NOT NULL,
	[Score] 		[nvarchar](15) 	NULL,
	[GameTime] 		[nvarchar](15)  NULL ,
	[GameStatusID]	[nvarchar](25)	NOT NULL,
	[Active] 		[bit]	NOT NULL DEFAULT 1,
	
	
	CONSTRAINT [pk_Games] PRIMARY KEY ([GameID]),
	CONSTRAINT [fk_Games_Maps] FOREIGN KEY([Maps])
		REFERENCES [dbo].[GameMaps]([Maps]),
	CONSTRAINT [fk_MapType] FOREIGN KEY([MapType])
		REFERENCES [dbo].[MapType]([MapType])
	
)
GO

print '' print '*** inserting Games records'

GO
INSERT INTO [dbo].[Games]
	([Maps],[MapType],[Score],[GameTime],[GameStatusID])
VALUES
	('HAVEN','3 Site','15-13','35 minutes','Played'),
	('ASCENT','2 Site','13-7','25 minutes','Played'),
	('BIND','Close Quaters','16-17','45 minutes','Played'),
	('BREEZE','Long Range','18-19','55 minutes','Played')
	
GO
print '' print '**creating sp_select_Games_by_Status'
GO
CREATE PROCEDURE [dbo].[sp_select_Games_by_Status]
(
	@GameStatusID			[nvarchar](25)
)
AS
	BEGIN
		SELECT	[GameID], [Maps],[Maptype], [Score], [GameTime],[GameStatusID], [Active]	
		FROM [dbo].[Games]
		WHERE @GameStatusID	 = [GameStatusID]
			AND [Active] = 1		
	END
GO
print '' print '**creating sp_select_Games_by_Maps'
GO
CREATE PROCEDURE [dbo].[sp_select_Games_by_Maps]
(
	@MapType			[nvarchar](50)
)
AS
	BEGIN
		SELECT[Maps]	
		FROM [Games]
		WHERE @MapType	 = [MapType]	
	END
GO





print '' print '*** creating Agents table'
GO
CREATE TABLE [dbo].[Agents] (
	[AgentID]		[int] 	IDENTITY(1000,1)		NOT NULL,
	[AgentName]		[nvarchar](50)	NOT NUll,
	[Description]		[nvarchar](500)	NOT NUll,
	[Active]		[bit]			NOT NULL DEFAULT 1,
	CONSTRAINT [pk_Agents] PRIMARY KEY ([AgentID]),
	
)
GO
print '' print '*** inserting Agents records'

GO
INSERT INTO [dbo].[Agents]
	([AgentName], [Description])
VALUES
	('Omen'     ,'A phantom of a memory, Omen hunts in the shadows. He renders enemies blind, teleports across the field, then lets paranoia take hold as his foe scrambles to learn where he might strike next.'),
	('Chamber'  ,'Well dressed and well armed, French weapons designer Chamber expels aggressors with deadly precision. He leverages his custom arsenal to hold the line and pick off enemies from afar, with a contingency built for every plan.'),
	('Rayna'    ,'Forged in the heart of Mexico, Reyna dominates single combat, popping off with each kill she scores. Her capability is only limited by her raw skill, making her highly dependent on performance.'),
	('Raze'     ,'Raze explodes out of Brazil with her big personality and big guns. With her blunt-force-trauma playstyle, she excels at flushing entrenched enemies and clearing tight spaces with a generous dose of boom.'),
	('Jett'     ,'Representing her home country of South Korea, Jetts agile and evasive fighting style lets her take risks no one else can. She runs circles around every skirmish, cutting enemies before they even know what hit them.'),
	('KillJoy'  ,'The genius of Germany. Killjoy secures the battlefield with ease using her arsenal of inventions. If the damage from her gear doesnt stop her enemies, her robots debuff will help make short work of them.'),
	('Brimstone', 'Joining from the USA, Brimstones orbital arsenal ensures his squad always has the advantage. His ability to deliver utility precisely and from a distance makes him an unmatched boots-on-the-ground commander'),
	('Viper'    ,'The American chemist, Viper deploys an array of poisonous chemical devices to control the battlefield and cripple the enemys vision. If the toxins dont kill her prey, her mind games surely will.'),
	('Fade'     , 'Turkish bounty hunter, Fade, unleashes the power of raw nightmares to seize enemy secrets. Attuned with terror itself, she hunts targets and reveals their deepest fears before crushing them in the dark'),
	('Sage'     ,'The stronghold of China, Sage creates safety for herself and her team wherever she goes. Able to revive fallen friends and stave off aggressive pushes, she provides a calm center to a hellish fight.')
	
GO




print '' print '*** creating Types table'
GO
CREATE TABLE [dbo].[Types] (
	[TypeID]		[int] 			NOT NULL,
	[TypeName]		[nvarchar](50)	NOT NUll,
	[Description] 	[nvarchar](250)	NOT NULL,
	
	CONSTRAINT [pk_Types] PRIMARY KEY ([TypeID]),
	
)
GO




print '' print '*** creating Ablities table'
GO
CREATE TABLE [dbo].[Ablities] (
	[AgentID]			[int] 			NOT NULL,
	[TypeID]			[int] 			NOT NULL,
	
	CONSTRAINT [fk_Agents_AgentID] FOREIGN KEY ([AgentID])
		REFERENCES [dbo].[Agents]([AgentID]),	
	CONSTRAINT [fk_Types_TypeID] FOREIGN KEY ([TypeID])
		REFERENCES [dbo].[Types]([TypeID]),
	CONSTRAINT [pk_Ablities] PRIMARY KEY ([AgentID], [TypeID])
	
)
GO



print '' print '*** creating sp_Agents_by_AgentID'
GO
create procedure [dbo].[sp_Agents_by_AgentID]
(
	@AgentID			[int]
)	
AS
	BEGIN
		select	[AgentName]
		from 	[Agents]
		WHERE	@AgentID = [AgentID]
	END        		
	
GO 




print '' print '**creating sp_select_Users_by_Status'
GO
CREATE PROCEDURE [dbo].[sp_select_Users_by_Status]
(
	@Active			[bit]
)
AS
	BEGIN
		SELECT	[UserId], [GivenName],[FamilyName], [GamerTag],[Active]	
		FROM [Users]
		WHERE @Active	 = [Active]
				
	END
GO

print '' print '**creating sp_select_Teams_by_Status'
GO
CREATE PROCEDURE [dbo].[sp_select_Teams_by_Status]
(
	@Active			[bit]
)
AS
	BEGIN
		SELECT	[Teams],[TeamsRanking],[Active]	
		FROM [Tournament]
		WHERE @Active	 = [Active]
				
	END
GO

print '' print '**creating sp_select_Agents_by_Status'
GO
CREATE PROCEDURE [dbo].[sp_select_Agents_by_Status]
(
	@Active			[bit]
)
AS
	BEGIN
		SELECT [AgentName],[Description],[Active]	
		FROM [Agents]
		WHERE @Active	 = [Active]
				
	END
GO