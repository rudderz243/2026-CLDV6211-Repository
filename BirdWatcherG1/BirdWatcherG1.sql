CREATE DATABASE BirdWatcherG1;

USE BirdWatcherG1;

CREATE TABLE Spottings (
    SpottingId INT NOT NULL PRIMARY KEY IDENTITY,
    SpottingDescription nvarchar(200) NOT NULL,
    TimeSeen datetime NULL DEFAULT ((getdate())),
    ImageURL nvarchar(200) NOT NULL,
);