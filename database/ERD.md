# Entity Relationship Diagram (ERD)
## Karigaar-360 Blue Collar Workforce Platform

```
┌─────────────────────────────────────────────────────────────────────────┐
│                         DATABASE ENTITIES                               │
└─────────────────────────────────────────────────────────────────────────┘

    ┌──────────────────────────┐
    │      CUSTOMERS           │
    ├──────────────────────────┤
    │ PK  Id (INT)             │
    │     FullName (TEXT)      │
    │     Phone (TEXT)         │
    │     Email (TEXT)         │
    │     Address (TEXT)       │
    │     PasswordHash (TEXT)  │
    │     CreatedAt (DATETIME) │
    │     UpdatedAt (DATETIME) │
    └────────────┬─────────────┘
                 │
                 │ (1:N) One Customer posts many Jobs
                 │
                 ▼
    ┌──────────────────────────────┐
    │         JOBS                 │
    ├──────────────────────────────┤
    │ PK  Id (INT)                 │
    │ FK  CustomerId (INT)◄────────┼─── Relationship: JOBS → CUSTOMERS
    │     Title (TEXT)             │
    │     Description (TEXT)       │
    │     Category (TEXT)          │
    │     EstimatedHours (INT)     │
    │     FairPrice (DECIMAL)      │
    │     OfferedPrice (DECIMAL)   │
    │     Location (TEXT)          │
    │     CustomerName (TEXT)      │
    │     Status (TEXT)            │
    │     CreatedAt (DATETIME)     │
    │     UpdatedAt (DATETIME)     │
    │     CompletedAt (DATETIME)   │
    └──────────────────────────────┘


    ┌──────────────────────────────────┐
    │         WORKERS                  │
    ├──────────────────────────────────┤
    │ PK  Id (INT)                     │
    │     FullName (TEXT)              │
    │     Phone (TEXT)                 │
    │     Profession (TEXT)            │
    │     ExperienceYears (INT)        │
    │     Location (TEXT)              │
    │     PasswordHash (TEXT)          │
    │     Rating (DECIMAL)             │
    │     TotalJobsCompleted (INT)     │
    │     IsAvailable (INT/BOOLEAN)    │
    │     CreatedAt (DATETIME)         │
    │     UpdatedAt (DATETIME)         │
    └──────────────────────────────────┘


┌─────────────────────────────────────────────────────────────────────────┐
│                        RELATIONSHIP DETAILS                             │
└─────────────────────────────────────────────────────────────────────────┘

Relationship: CUSTOMERS → JOBS
- Type: One-to-Many (1:N)
- Foreign Key: Jobs.CustomerId references Customers.Id
- Cascade Delete: When customer is deleted, all their jobs are deleted
- Cardinality: One customer can post multiple jobs
- Constraint: NOT NULL on Jobs.CustomerId


┌─────────────────────────────────────────────────────────────────────────┐
│                       KEY INDEXES FOR PERFORMANCE                       │
└─────────────────────────────────────────────────────────────────────────┘

Customers Table:
  - idx_customers_email (UNIQUE) → for login queries
  - idx_customers_phone (UNIQUE) → for phone verification

Workers Table:
  - idx_workers_phone (UNIQUE) → for login queries
  - idx_workers_profession → for job matching by profession
  - idx_workers_location → for location-based queries
  - idx_workers_is_available → for availability filtering

Jobs Table:
  - idx_jobs_customer_id → for customer's job list
  - idx_jobs_status → for filtering (Open, InProgress, Completed)
  - idx_jobs_category → for category-based search
  - idx_jobs_location → for location-based search
  - idx_jobs_created_at → for sorting by newest jobs


┌─────────────────────────────────────────────────────────────────────────┐
│                    EXAMPLE USE CASES & QUERIES                          │
└─────────────────────────────────────────────────────────────────────────┘

1. Find all open jobs for a specific location:
   SELECT * FROM Jobs WHERE Status = 'Open' AND Location = 'Karachi'

2. Get all jobs posted by a specific customer:
   SELECT * FROM Jobs WHERE CustomerId = 1 ORDER BY CreatedAt DESC

3. Find available workers in a specific profession:
   SELECT * FROM Workers WHERE Profession = 'Plumber' AND IsAvailable = 1

4. Get completed jobs for a customer with worker statistics:
   SELECT j.*, COUNT(j.Id) as TotalJobsPosted 
   FROM Jobs j 
   WHERE j.CustomerId = 1 AND j.Status = 'Completed'
   GROUP BY j.CustomerId

5. Find highest-rated workers in a location:
   SELECT * FROM Workers 
   WHERE Location = 'Lahore' 
   ORDER BY Rating DESC LIMIT 10
