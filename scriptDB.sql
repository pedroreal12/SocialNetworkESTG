USE master
GO
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'sndb')
    BEGIN
        CREATE DATABASE sndb;
    END;
    GO

USE sndb
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='sndb')
    BEGIN
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
        CREATE TABLE Users (
            Id INT PRIMARY KEY IDENTITY (1, 1),
            StrName VARCHAR(100) NOT NULL,
            StrEmail VARCHAR(255) NOT NULL,
            HsPassword VARCHAR(255) NOT NULL,
            StrRecoverMail VARCHAR(255) NOT NULL,
            StrPhoneNumber VARCHAR(100) NOT NULL,
            FkIdRole INT FOREIGN KEY REFERENCES Roles(Id),
            StrState VARCHAR(20) NOT NULL,
            FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
            FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
            DateCreated DATETIME NOT NULL,
            DateLastChanged DATETIME NOT NULL,
            HashURL VARCHAR(255)
        );
        CREATE TABLE Permissions (
            Id INT PRIMARY KEY IDENTITY (1, 1),
            FkIdRoleAllowed INT FOREIGN KEY REFERENCES Roles(Id)
            FkIdAction INT FOREIGN KEY REFERENCES Actions(Id),
            StrState VARCHAR(20) NOT NULL,
            DateCreated DATETIME NOT NULL,
            DateLastChanged DATETIME NOT NULL,
            FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
            FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
            HashURL VARCHAR(255)
        );
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
        CREATE TABLE Review(
            Id INT PRIMARY KEY IDENTITY (1, 1),
            FkIdMovie INT FOREIGN KEY,
            FkIdUser INT FOREIGN KEY REFERENCES Users(Id),
            StrState VARCHAR(20) NOT NULL,
            DateCreated DATETIME NOT NULL,
            DateLastChanged DATETIME NOT NULL,
            FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
            FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
        )
        CREATE TABLE Administrator (
            Id INT PRIMARY KEY IDENTITY (1, 1),
            StrName VARCHAR(128) NOT NULL,
            StrMail VARCHAR(128) NOT NULL,
            StrState VARCHAR(20) NOT NULL,
            FkIdUser INT FOREIGN KEY REFERENCES Users(Id),
            DateCreated DATETIME NOT NULL,
            DateLastChanged DATETIME NOT NULL,
            FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
            FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
            HashURL VARCHAR(255)
        );
        CREATE TABLE Professor (
            Id INT PRIMARY KEY IDENTITY (1, 1),
            StrName VARCHAR(128) NOT NULL,
            FkIdUser INT FOREIGN KEY REFERENCES Users(Id),
            StrMail VARCHAR(128) NOT NULL,
            StrState VARCHAR(20) NOT NULL,
            FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
            FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
            DateCreated DATETIME NOT NULL,
            DateLastChanged DATETIME NOT NULL,
            HashURL VARCHAR(255)
        );
        CREATE TABLE Student (
            Id INT PRIMARY KEY IDENTITY (1, 1),
            StrName VARCHAR(128) NOT NULL,
            FkIdUser INT FOREIGN KEY REFERENCES Users(Id),
            StrMail VARCHAR(128) NOT NULL,
            StrPhoneNumber VARCHAR(20) NOT NULL,
            StrState VARCHAR(20) NOT NULL,
            DateCreated DATETIME NOT NULL,
            DateLastChanged DATETIME NOT NULL,
            FkIdUserCreated INT FOREIGN KEY REFERENCES Users(Id),
            FkIdUserLastChanged INT FOREIGN KEY REFERENCES Users(Id),
            HashURL VARCHAR(255)
        );
    END
GO
