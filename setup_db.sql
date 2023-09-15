USE master
GO

IF DB_ID('MotionPictures') IS NOT NULL
BEGIN
	ALTER DATABASE MotionPictures SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE MotionPictures;
END

CREATE DATABASE MotionPictures
GO

USE MotionPictures
GO

CREATE TABLE [dbo].[MotionPictures](
	[ID] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Release Year] [int] NOT NULL,
	[AcademyAward] [bit] NOT NULL,
	[DirectorId] [int] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MotionPictureDirectors](
	[DirectorId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](500) NULL,
	[BirthYear] [int] NOT NULL,
) ON [PRIMARY]
GO

INSERT INTO [dbo].[MotionPictureDirectors]
           ([DirectorId]
           ,[FirstName]
           ,[LastName]
           ,[BirthYear])
     VALUES
           (1
           ,'James'
           ,'Lexington'
           ,1962)
GO

INSERT INTO [dbo].[MotionPictureDirectors]
           ([DirectorId]
           ,[FirstName]
           ,[LastName]
           ,[BirthYear])
     VALUES
           (2
           ,'Margaret'
           ,'Blackburn'
           ,1975)
GO

INSERT INTO [dbo].[MotionPictureDirectors]
           ([DirectorId]
           ,[FirstName]
           ,[LastName]
           ,[BirthYear])
     VALUES
           (3
           ,'Jackson'
           ,'Smith'
           ,1993)
GO

INSERT INTO [dbo].[MotionPictures]
            ([Name]
           ,[Description]
           ,[Release Year]
		   ,[AcademyAward]
		   ,[DirectorId])
     VALUES
           ('The Matrix'
           ,'When a beautiful stranger leads computer hacker Neo to a forbidding underworld, he discovers the shocking truth--the life he knows is the elaborate deception of an evil cyber-intelligence.'
           ,1999
		   ,1
		   ,1)

INSERT INTO [dbo].[MotionPictures]
           ([Name]
           ,[Description]
           ,[Release Year]
		   ,[AcademyAward]
		   ,[DirectorId])
     VALUES
           ('Snowpiercer'
           ,'In a future where a failed climate change experiment has killed all life except for the survivors who boarded the Snowpiercer (a train that travels around the globe), a new class system emerges.'
           ,2013
		   ,0
		   ,null)

INSERT INTO [dbo].[MotionPictures]
           ([Name]
           ,[Description]
           ,[Release Year]
		   ,[AcademyAward]
		   ,[DirectorId])
     VALUES
           ('Spider-Man: No Way Home'
           ,'With Spider-Man''s identity now revealed, Peter asks Doctor Strange for help. When a spell goes wrong, dangerous foes from other worlds start to appear, forcing Peter to discover what it truly means to be Spider-Man.'
           ,2021
		   ,0
		   ,3)

INSERT INTO [dbo].[MotionPictures]
          ([Name]
           ,[Description]
           ,[Release Year]
		   ,[AcademyAward]
		   ,[DirectorId])
     VALUES
           ('Big Fish'
           ,'A frustrated son tries to determine the fact from fiction in his dying father''s life.'
           ,2003
		   ,0
		   ,1)

INSERT INTO [dbo].[MotionPictures]
           ([Name]
           ,[Description]
           ,[Release Year]
		   ,[AcademyAward]
		   ,[DirectorId])
     VALUES
           ('The Sandlot'
           ,'In the summer of 1962, a new kid in town is taken under the wing of a young baseball prodigy and his rowdy team, resulting in many adventures.'
           ,1993
		   ,0
		   ,2)

INSERT INTO [dbo].[MotionPictures]
          ([Name]
           ,[Description]
           ,[Release Year]
		   ,[AcademyAward]
		   ,[DirectorId])
     VALUES
           ('Back to the Future'
           ,'Marty McFly, a 17-year-old high school student, is accidentally sent 30 years into the past in a time-traveling DeLorean invented by his close friend, the maverick scientist Doc Brown.'
           ,1985
		   ,1
		   ,3)