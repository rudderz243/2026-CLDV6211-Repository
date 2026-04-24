CREATE DATABASE CarRentalExampleG1;

USE CarRentalExampleG1;

CREATE TABLE [Cars] (
    [CarID] int NOT NULL IDENTITY,
    [CarMake] nvarchar(50) NOT NULL,
    [CarModel] nvarchar(50) NOT NULL,
    [CarYear] nvarchar(4) NOT NULL,
    [CarColourway] nvarchar(50) NOT NULL,
    [CarVIN] nvarchar(17) NOT NULL,
    CONSTRAINT [PK__Cars__68A0340EF4BB7B28] PRIMARY KEY ([CarID])
);
GO


CREATE TABLE [Customers] (
    [CustomerID] int NOT NULL IDENTITY,
    [CustomerName] nvarchar(50) NOT NULL,
    [CustomerEmail] nvarchar(150) NOT NULL,
    [CustomerPhone] nvarchar(20) NOT NULL,
    [CustomerAddress] nvarchar(50) NOT NULL,
    CONSTRAINT [PK__Customer__A4AE64B817F8EEE7] PRIMARY KEY ([CustomerID])
);
GO


CREATE TABLE [Bookings] (
    [BookingID] int NOT NULL IDENTITY,
    [CarID] int NOT NULL,
    [CustomerID] int NOT NULL,
    [StartDate] datetime NOT NULL,
    [EndDate] datetime NOT NULL,
    [BookingStatus] nvarchar(20) NOT NULL CONSTRAINT [DF_Bookings_Status] DEFAULT N'Pending',
    [PaymentType] nvarchar(20) NOT NULL CONSTRAINT [DF_Payment_Type] DEFAULT N'Not Paid',
    CONSTRAINT [PK__Bookings__73951ACD1F8638C9] PRIMARY KEY ([BookingID]),
    CONSTRAINT [FK_Car_Bookings] FOREIGN KEY ([CarID]) REFERENCES [Cars] ([CarID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Customer_Bookings] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerID]) ON DELETE CASCADE
);
GO