

-- Drop existing tables if they exist (for clean migrations)
DROP TABLE IF EXISTS Jobs;
DROP TABLE IF EXISTS Customers;
DROP TABLE IF EXISTS Workers;


CREATE TABLE Customers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Phone TEXT NOT NULL,
    Email TEXT,
    Address TEXT,
    PasswordHash TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);


CREATE UNIQUE INDEX idx_customers_email ON Customers(Email);
CREATE UNIQUE INDEX idx_customers_phone ON Customers(Phone);


CREATE TABLE Workers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Phone TEXT NOT NULL,
    Profession TEXT NOT NULL,
    ExperienceYears INTEGER NOT NULL,
    Location TEXT NOT NULL,
    PasswordHash TEXT NOT NULL,
    Rating DECIMAL(3,2) DEFAULT 0,
    TotalJobsCompleted INTEGER DEFAULT 0,
    IsAvailable INTEGER DEFAULT 1,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX idx_workers_phone ON Workers(Phone);
CREATE INDEX idx_workers_profession ON Workers(Profession);
CREATE INDEX idx_workers_location ON Workers(Location);
CREATE INDEX idx_workers_is_available ON Workers(IsAvailable);


CREATE TABLE Jobs (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL CHECK(length(Title) <= 100),
    Description TEXT NOT NULL,
    Category TEXT NOT NULL CHECK(length(Category) <= 50),
    EstimatedHours INTEGER NOT NULL CHECK(EstimatedHours > 0 AND EstimatedHours <= 1000),
    FairPrice DECIMAL(10,2) NOT NULL,
    OfferedPrice DECIMAL(10,2) NOT NULL,
    Location TEXT NOT NULL,
    CustomerId INTEGER NOT NULL,
    CustomerName TEXT NOT NULL CHECK(length(CustomerName) <= 100),
    Status TEXT NOT NULL DEFAULT 'Open' CHECK(length(Status) <= 20),
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CompletedAt DATETIME,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
);

CREATE INDEX idx_jobs_customer_id ON Jobs(CustomerId);
CREATE INDEX idx_jobs_status ON Jobs(Status);
CREATE INDEX idx_jobs_category ON Jobs(Category);
CREATE INDEX idx_jobs_location ON Jobs(Location);
CREATE INDEX idx_jobs_created_at ON Jobs(CreatedAt);
