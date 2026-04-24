CREATE DATABASE CarRentalExampleG2;

USE CarRentalExampleG2;

CREATE TABLE [Cars] (
    [CarID] int NOT NULL IDENTITY,
    [CarMake] nvarchar(50) NOT NULL,
    [CarModel] nvarchar(50) NOT NULL,
    [CarYear] nvarchar(4) NOT NULL,
    [CarColourway] nvarchar(50) NOT NULL,
    [CarVIN] nvarchar(17) NOT NULL,
    CONSTRAINT [PK__Cars__68A0340EA14D11DB] PRIMARY KEY ([CarID])
);
GO


CREATE TABLE [Customers] (
    [CustomerID] int NOT NULL IDENTITY,
    [CustomerName] nvarchar(50) NOT NULL,
    [CustomerEmail] nvarchar(150) NOT NULL,
    [CustomerPhone] nvarchar(20) NOT NULL,
    [CustomerAddress] nvarchar(250) NOT NULL,
    CONSTRAINT [PK__Customer__A4AE64B86715060D] PRIMARY KEY ([CustomerID])
);
GO


CREATE TABLE [Bookings] (
    [BookingID] int NOT NULL IDENTITY,
    [CarID] int NOT NULL,
    [CustomerID] int NOT NULL,
    [StartDate] datetime NOT NULL,
    [EndDate] datetime NOT NULL,
    [BookingStatus] nvarchar(20) NOT NULL CONSTRAINT [DF_Bookings_Status] DEFAULT N'Pending',
    [PaymentType] nvarchar(20) NOT NULL CONSTRAINT [DF_Bookings_PaymentType] DEFAULT N'Not Paid',
    CONSTRAINT [PK__Bookings__73951ACD90087244] PRIMARY KEY ([BookingID]),
    CONSTRAINT [FK_Bookings_Cars] FOREIGN KEY ([CarID]) REFERENCES [Cars] ([CarID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerID]) ON DELETE CASCADE
);
GO
