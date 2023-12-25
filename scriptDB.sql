USE master
GO

IF DB_ID('snidentitydb') IS NOT NULL
    BEGIN
		PRINT('TABLE IDENTITY EXISTS; DELETING');
        use master;
		DROP DATABASE snidentitydb;
    END
GO
IF DB_ID('sndb') IS NOT NULL
    BEGIN
		PRINT('TABLE SNDB EXISTS; DELETING');
        use master;
		DROP DATABASE sndb;
    END
GO
PRINT('TABLE SNDB DOESNT EXIST; CREATING');
USE master;
CREATE DATABASE sndb;
GO
PRINT('TABLE IDENTITY DOESNT EXIST; CREATING');
USE master;
CREATE DATABASE snidentitydb;
GO
USE sndb;
GO

/*CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    StrName VARCHAR(100) NOT NULL,
    StrEmail VARCHAR(255) NOT NULL,
    HsPassword VARCHAR(255) NOT NULL,
    StrRecoverMail VARCHAR(255) NOT NULL,
    StrPhoneNumber VARCHAR(100) NOT NULL,
    StrState VARCHAR(20) NOT NULL,
    FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
    FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    HashURL VARCHAR(255)
);

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    StrName VARCHAR(100) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
    FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
    StrState VARCHAR(20) NOT NULL,
    HashURL VARCHAR(255),
);

ALTER TABLE Users
	ADD FkIdRole INT FOREIGN KEY REFERENCES Roles(Id);

CREATE TABLE Actions (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    StrName VARCHAR(128) NOT NULL,
    StrController VARCHAR(128) NOT NULL,
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
    FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
    HashURL VARCHAR(255)
);

CREATE TABLE Permissions (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    FkIdRoleAllowed INT FOREIGN KEY REFERENCES Roles(Id),
    FkIdAction INT FOREIGN KEY REFERENCES Actions(Id),
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
    FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
    HashURL VARCHAR(255)
);*/

CREATE TABLE Review(
    Id INT PRIMARY KEY IDENTITY (1, 1),
    FkIdMovie INT NOT NULL,
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
)

CREATE TABLE Administrator (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    StrName VARCHAR(128) NOT NULL,
    StrMail VARCHAR(128) NOT NULL,
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    HashURL VARCHAR(255)
);

CREATE TABLE Professor (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    StrName VARCHAR(128) NOT NULL,
    StrMail VARCHAR(128) NOT NULL,
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    HashURL VARCHAR(255)
);

CREATE TABLE Student (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    StrName VARCHAR(128) NOT NULL,
    StrMail VARCHAR(128) NOT NULL,
    StrPhoneNumber VARCHAR(20) NOT NULL,
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    HashURL VARCHAR(255)
);

CREATE TABLE Comment (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    StrName VARCHAR(128) NOT NULL,
    FkIdComment INT FOREIGN KEY REFERENCES Comment(Id), 
    TextComment VARCHAR(255) NOT NULL,
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    HashURL VARCHAR(255)
);

CREATE TABLE UserList (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    StrName VARCHAR(128) NOT NULL,
    FkIdMovieList INT FOREIGN KEY REFERENCES Comment(Id), 
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    HashURL VARCHAR(255)
);

CREATE TABLE MovieList (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    FkIdList INT FOREIGN KEY REFERENCES UserList(Id),
    FkIdMovie INT NOT NULL,
    StrState VARCHAR(20) NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateLastChanged DATETIME NOT NULL,
    HashURL VARCHAR(255)
);
