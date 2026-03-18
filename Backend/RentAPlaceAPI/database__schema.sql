
-- RentAPlace - Database Schema
-- Project: Capstone Project RentAPlace
-- Database: RentAPlaceDB
-- ORM: Entity Framework Core


CREATE DATABASE RentAPlaceDB;
GO

USE RentAPlaceDB;
GO



CREATE TABLE Users (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(100)  NOT NULL,
    Email       NVARCHAR(200)  NOT NULL UNIQUE,
    Password    NVARCHAR(255)  NOT NULL,
    Role        NVARCHAR(50)   NOT NULL DEFAULT 'User'
    -- Role: 'User' (Renter) or 'Owner'
);
GO



CREATE TABLE Properties (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    Title        NVARCHAR(200)     NOT NULL,
    Location     NVARCHAR(300)     NOT NULL,
    Price        DECIMAL(10, 2)    NOT NULL,
    PropertyType NVARCHAR(100)     NOT NULL,
    Description  NVARCHAR(MAX)     NULL,
    ImagePath    NVARCHAR(500)     NOT NULL DEFAULT '',
    OwnerId      INT               NOT NULL,
    CONSTRAINT FK_Properties_Owner
        FOREIGN KEY (OwnerId) REFERENCES Users(Id)
        ON DELETE CASCADE
);
GO



CREATE TABLE Reservations (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    PropertyId  INT            NOT NULL,
    UserId      INT            NOT NULL,
    CheckIn     DATETIME       NOT NULL,
    CheckOut    DATETIME       NOT NULL,
    Status      NVARCHAR(50)   NOT NULL DEFAULT 'Pending',
    -- Status: 'Pending', 'Confirmed', 'Cancelled'
    CONSTRAINT FK_Reservations_Property
        FOREIGN KEY (PropertyId) REFERENCES Properties(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_Reservations_User
        FOREIGN KEY (UserId) REFERENCES Users(Id)
        ON DELETE NO ACTION
);
GO



CREATE TABLE Messages (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    SenderId    INT            NOT NULL,
    ReceiverId  INT            NOT NULL,
    Content     NVARCHAR(MAX)  NOT NULL,
    SentAt      DATETIME       NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_Messages_Sender
        FOREIGN KEY (SenderId) REFERENCES Users(Id)
        ON DELETE NO ACTION,
    CONSTRAINT FK_Messages_Receiver
        FOREIGN KEY (ReceiverId) REFERENCES Users(Id)
        ON DELETE NO ACTION
);
GO




CREATE TABLE Reviews (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    PropertyId  INT            NOT NULL,
    UserId      INT            NOT NULL,
    Rating      INT            NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
    Comment     NVARCHAR(500)  NULL,
    CreatedAt   DATETIME       NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_Reviews_Property
        FOREIGN KEY (PropertyId) REFERENCES Properties(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_Reviews_User
        FOREIGN KEY (UserId) REFERENCES Users(Id)
        ON DELETE NO ACTION
);
GO
