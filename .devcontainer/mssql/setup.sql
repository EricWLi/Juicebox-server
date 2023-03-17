
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'JuiceboxDb_Dev')
    CREATE DATABASE JuiceboxDb_Dev;
GO

USE JuiceboxDb_Dev;

DROP TABLE IF EXISTS dbo.Guest;
DROP TABLE IF EXISTS dbo.Party;
DROP TABLE IF EXISTS dbo.UserAccount;

CREATE TABLE dbo.UserAccount (
    ID INT NOT NULL,
    Username VARCHAR(15) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    DateCreated DATE NOT NULL,
    LastActive DATE NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY(ID)
);

CREATE TABLE dbo.Guest (
    ID INT NOT NULL,
    DisplayName VARCHAR(15) NOT NULL,
    DateCreated DATE NOT NULL,
    LastActive DATE NOT NULL,
    CONSTRAINT PK_Guest PRIMARY KEY(ID)
);

DROP TABLE IF EXISTS dbo.Party;
CREATE TABLE dbo.Party (
    ID INT NOT NULL,
    Name VARCHAR(255) NULL,
    HostUserID INT NOT NULL,
    CONSTRAINT PK_Party PRIMARY KEY(ID),
    CONSTRAINT FK_Party_UserAccount FOREIGN KEY(HostUserID)
        REFERENCES dbo.UserAccount(ID)
);

DROP TABLE IF EXISTS dbo.PartyMember;
CREATE TABLE dbo.PartyMember (
    ID INT NOT NULL,
    PartyID INT NOT NULL,
    UserID INT NULL,
    GuestID INT NULL,
    DateCreated DATE NOT NULL,
    CONSTRAINT PK_PartyMember PRIMARY KEY(ID),
    CONSTRAINT FK_PartyMember_Party FOREIGN KEY(PartyID)
        REFERENCES dbo.Party(ID),
    CONSTRAINT FK_PartyMember_User FOREIGN KEY(UserID)
        REFERENCES dbo.UserAccount(ID),
    CONSTRAINT FK_PartyMember_Guest FOREIGN KEY(GuestID)
        REFERENCES dbo.Guest(ID)
);

DROP TABLE IF EXISTS dbo.QueueItem;
CREATE TABLE dbo.QueueItem (
    ID INT NOT NULL,
    PartyID INT NOT NULL,
    SpotifyUri VARCHAR(64) NOT NULL,
    SongTitle NVARCHAR(255) NOT NULL,
    SongArtist NVARCHAR(255) NOT NULL,
    SongDuration INT NOT NULL,
    CONSTRAINT PK_Queue PRIMARY KEY(ID),
    CONSTRAINT FK_Queue_Party FOREIGN KEY(PartyID)
        REFERENCES dbo.Party(ID)
);

GO