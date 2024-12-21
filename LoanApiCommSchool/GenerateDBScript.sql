-------------
-- Inserts --
-------------

INSERT INTO [User] (FirstName, LastName, Username, Age, Email, MonthlyIncome, IsBlocked, Password)
VALUES
('Artur', 'Zakharyan', 'arturz', 30, 'artur.zakharyan@example.com', 5000, 0, 'hashedpassword1'),
('Anahit', 'Sargsyan', 'anahits', 28, 'anahit.sargsyan@example.com', 4500, 0, 'hashedpassword2'),
('Armine', 'Mkrtchyan', 'arminem', 35, 'armine.mkrtchyan@example.com', 6000, 1, 'hashedpassword3'),
('Gevorg', 'Hakobyan', 'gevorgh', 40, 'gevorg.hakobyan@example.com', 7000, 0, 'hashedpassword4'),
('Jorik', 'Kapanyan', 'jorikk', 33, 'jorik.kapanyan@example.com', 5200, 0, 'hashedpassword5'),
('Sashik', 'Maisuryan', 'sashikm', 45, 'sashik.maisuryan@example.com', 7500, 0, 'hashedpassword6');


INSERT INTO Loan (LoanType, Amount, Currency, Period, Status, UserID)
VALUES
('Installment', 2500, 'USD', 12, 'Processing', 1), -- Installment Loan for User 1
('Auto', 30000, 'EUR', 60, 'Approved', 2), -- Auto Loan for User 2
('Quick', 1000, 'USD', 6, 'Rejected', 3), -- Quick Loan for User 3
('Installment', 5000, 'GEL', 24, 'Processing', 1), -- Installment Loan for User 1
('Auto', 20000, 'USD', 48, 'Approved', 4), -- Auto Loan for User 4
('Installment', 12000, 'GEL', 18, 'Processing', 2); -- Installment Loan for User 2



INSERT INTO [Accountant] (FirstName, LastName)
VALUES
('Artur', 'Zakharyan'),
('Sashik', 'Maisuryan')