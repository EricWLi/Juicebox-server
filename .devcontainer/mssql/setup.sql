
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'JuiceboxDb_Dev')
    CREATE DATABASE JuiceboxDb_Dev;
GO

USE JuiceboxDb_Dev;

DROP TABLE IF EXISTS dbo.Guest;
DROP TABLE IF EXISTS dbo.Party;
DROP TABLE IF EXISTS dbo.UserAccount;

CREATE TABLE dbo.UserAccount (
    UserID INT NOT NULL,
    Username VARCHAR(15) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    DateCreated DATE NOT NULL,
    DateModified DATE NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY(UserID)
);

CREATE TABLE dbo.Guest (
    GuestID INT NOT NULL,
    DisplayName VARCHAR(15) NOT NULL,
    DateCreated DATE NOT NULL,
    LastActive DATE NOT NULL,
    CONSTRAINT PK_Guest PRIMARY KEY(GuestID)
);

DROP TABLE IF EXISTS dbo.Party;
CREATE TABLE dbo.Party (
    PartyID INT NOT NULL,
    Name VARCHAR(255) NOT NULL,
    HostUserID INT NOT NULL,
    CONSTRAINT PK_Party PRIMARY KEY(PartyID),
    CONSTRAINT FK_Party_UserAccount FOREIGN KEY(HostUserID)
        REFERENCES dbo.UserAccount(UserID)
);

GO