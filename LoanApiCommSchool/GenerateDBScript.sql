CREATE TABLE [User] (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Age INT NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    MonthlyIncome DECIMAL(18, 2) NOT NULL,
    IsBlocked BIT DEFAULT 0 NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL
);

CREATE TABLE LoanType (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(50) UNIQUE NOT NULL -- For the Normalized Entity Types should be stored separately
	;

CREATE TABLE Currency (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CurrencyCode NVARCHAR(10) UNIQUE NOT NULL -- it's always a good idea to store currencies separately for dictionary purposes
);


CREATE TABLE Role (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) UNIQUE NOT NULL -- RoleInformation for the Normalized DB
);

CREATE TABLE Loan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    LoanTypeID INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    CurrencyID INT NOT NULL,
    Period INT NOT NULL, -- Loan period in months
    Status NVARCHAR(50) NOT NULL, -- 'Processing', 'Approved', 'Rejected'
    UserID INT NOT NULL,
    FOREIGN KEY (LoanTypeID) REFERENCES LoanType(ID),
    FOREIGN KEY (CurrencyID) REFERENCES Currency(ID),
    FOREIGN KEY (UserID) REFERENCES [User](ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

CREATE TABLE UserRole (
    UserID INT NOT NULL,
    RoleID INT NOT NULL,
    PRIMARY KEY (UserID, RoleID),
    FOREIGN KEY (UserID) REFERENCES [User](ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    FOREIGN KEY (RoleID) REFERENCES Role(ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

-------------
-- Inserts --
-------------

INSERT INTO [User] (FirstName, LastName, Username, Age, Email, MonthlyIncome, IsBlocked, PasswordHash)
VALUES
('Artur', 'Zakharyan', 'arturz', 30, 'artur.zakharyan@example.com', 5000, 0, 'hashedpassword1'),
('Anahit', 'Sargsyan', 'anahits', 28, 'anahit.sargsyan@example.com', 4500, 0, 'hashedpassword2'),
('Armine', 'Mkrtchyan', 'arminem', 35, 'armine.mkrtchyan@example.com', 6000, 1, 'hashedpassword3'),
('Gevorg', 'Hakobyan', 'gevorgh', 40, 'gevorg.hakobyan@example.com', 7000, 0, 'hashedpassword4'),
('Jorik', 'Kapanyan', 'jorikk', 33, 'jorik.kapanyan@example.com', 5200, 0, 'hashedpassword5'),
('Sashik', 'Maisuryan', 'sashikm', 45, 'sashik.maisuryan@example.com', 7500, 0, 'hashedpassword6');

INSERT INTO Role (Name)
VALUES
('User'),
('Accountant');

INSERT INTO UserRole (UserID, RoleID)
VALUES
(1, 1), -- John is a User
(2, 1), -- Jane is a User
(3, 1), -- Alice is a User
(4, 2); -- Bob is an Accountant


INSERT INTO LoanType (TypeName)
VALUES
('Quick Loan'),
('Auto Loan'),
('Installment');

INSERT INTO Currency (CurrencyCode)
VALUES
('USD'),
('EUR'),
('GBP');

INSERT INTO Loan (LoanTypeID, Amount, CurrencyID, Period, Status, UserID)
VALUES
(1, 1500, 1, 12, 'Processing', 1), -- Quick Loan for John
(2, 20000, 2, 36, 'Approved', 2), -- Auto Loan for Jane
(3, 5000, 3, 24, 'Rejected', 3), -- Installment Loan for Alice
(1, 3000, 1, 18, 'Processing', 1), -- Quick Loan for John
(2, 15000, 2, 48, 'Approved', 4), -- Auto Loan for Bob
(3, 8000, 3, 12, 'Processing', 2); -- Installment Loan for Jane
