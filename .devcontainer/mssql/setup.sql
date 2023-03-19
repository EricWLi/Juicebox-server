
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'JuiceboxDb_Dev')
    CREATE DATABASE JuiceboxDb_Dev;
GO