CREATE DATABASE BirdWatcherG2;

USE BirdWatcherG2;

CREATE TABLE Spottings (
    SpottingId INT NOT NULL IDENTITY PRIMARY KEY,
    BirdSeenDescription nvarchar(200) NOT NULL,
    TimeSeen datetime NULL DEFAULT ((getdate())),
    ImageURL nvarchar(200) NOT NULL,
);