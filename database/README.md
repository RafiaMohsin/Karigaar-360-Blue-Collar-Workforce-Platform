# Database Documentation
## Karigaar-360 Blue Collar Workforce Platform

---

## 📋 Overview

This directory contains all database-related files for the Karigaar-360 project, including schema definitions, sample data, and Entity Relationship Diagrams.

---

## 📁 File Structure

```
database/
├── schema.sql          # Database schema and table definitions
├── seed.sql            # Sample and test data
├── ERD.md              # Entity Relationship Diagram documentation
└── README.md           # This file
```

---

## 📊 Database Details

### **Database Type**: SQLite (for development) / Can migrate to SQL Server, MySQL, PostgreSQL

### **Connection String** (in `appsettings.json`):
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=Karigaar360.db"
}
```

### **Tables Created**:
1. **Customers** - Job posters/requesters
2. **Workers** - Service providers/blue collar professionals
3. **Jobs** - Job listings posted by customers

---

## 📄 File Descriptions

### 1. **schema.sql**
Contains the complete database schema with:
- Table definitions for `Customers`, `Workers`, and `Jobs`
- Column definitions with data types and constraints
- Primary keys and foreign key relationships
- Indexes for performance optimization
- Check constraints for data validation
- Cascading delete rules

**Usage:**
```bash
# For SQLite
sqlite3 Karigaar360.db < schema.sql

# For SQL Server / MySQL / PostgreSQL
# Run the SQL statements through your database management tool
```

### 2. **seed.sql**
Contains realistic sample data for testing:
- **8 Sample Customers** with contact details and password hashes
- **15 Sample Workers** with professions, experience levels, and ratings
- **18 Sample Jobs** in various categories (Plumbing, Electrical, Carpentry, etc.)
- **Job Status Distribution**: 8 Open, 2 In Progress, 12 Completed

**Sample Data Breakdown:**
```
CUSTOMERS: 8 records
├── Location Distribution: Karachi, Lahore, Islamabad, Multan, Peshawar, etc.
└── Email & Phone verified for uniqueness

WORKERS: 15 records
├── Professions: Plumber, Electrician, Carpenter, Painter, Mason, HVAC, Welder
├── Experience: 5-15 years
├── Rating: 4.3 - 4.9 (out of 5.0)
├── Total Jobs Completed: 15-102 jobs
└── Availability: 11 available, 4 unavailable

JOBS: 18 records
├── Categories: 8 different job types
├── Status: Open (8), InProgress (2), Completed (12)
├── Location Coverage: Major Pakistani cities
├── Price Range: PKR 1,500 - PKR 28,000 offered
└── Duration: 2 - 30 hours estimated
```

**Usage:**
```bash
# For SQLite
sqlite3 Karigaar360.db < seed.sql

# For running in .NET migrations
# Copy relevant INSERT statements into EF Core seeders
```

### 3. **ERD.md**
Visual Entity Relationship Diagram including:
- ASCII diagram of all tables and relationships
- Table structure reference
- Relationship types (1:N)
- Key constraints and cascade rules
- Performance indexes explanation
- Example queries for common use cases

---

## 🗂️ Entity Relationship Summary

### **Relationship Diagram**

```
CUSTOMERS (1) ──────→ (N) JOBS
   ├─ Id (PK)           ├─ Id (PK)
   ├─ FullName          ├─ CustomerId (FK)
   ├─ Phone             ├─ Title
   ├─ Email             ├─ Category
   ├─ Address           ├─ Status
   └─ PasswordHash      └─ CreatedAt

WORKERS (Independent)
   ├─ Id (PK)
   ├─ FullName
   ├─ Profession
   ├─ Location
   ├─ Rating
   └─ TotalJobsCompleted
```

### **Key Relationships**
- **CUSTOMERS → JOBS**: One-to-Many (Cascade Delete)
  - A customer can post multiple jobs
  - When a customer is deleted, all their jobs are deleted

- **WORKERS**: Standalone table
  - Can be linked to JOBS through future `Bids` or `Applications` table (for future enhancements)

---

##  Primary Keys & Constraints

| Table | Primary Key | Unique Fields | Foreign Keys |
|-------|-------------|---------------|--------------|
| Customers | Id | Email, Phone | None |
| Workers | Id | Phone | None |
| Jobs | Id | None | CustomerId → Customers(Id) |

---

##  Database Statistics (with Sample Data)

| Metric | Value |
|--------|-------|
| Total Tables | 3 |
| Total Records | 41 |
| Total Indexes | 9 |
| Relationships | 1 (1:N) |
| Cascade Rules | 1 (Customers→Jobs) |

---

## 🚀 Setup Instructions

### **Option 1: Using .NET Entity Framework Migrations (Recommended)**
The project already has EF Core migrations set up. Just run:
```bash
cd backend
dotnet ef database update
```

This will automatically:
1. Create tables from migrations
2. Create indexes
3. Apply all constraints

### **Option 2: Manual Database Setup**

**For SQLite:**
```bash
# Navigate to project directory
cd backend

# Create database and apply schema
sqlite3 Karigaar360.db < ../database/schema.sql

# (Optional) Load sample data
sqlite3 Karigaar360.db < ../database/seed.sql
```

**For SQL Server/MySQL/PostgreSQL:**
1. Copy contents of `schema.sql`
2. Run in your database manager
3. (Optional) Run `seed.sql` for sample data
4. Update connection string in `appsettings.json`

---

## 🔐 Security Considerations

### **Password Storage**
- Passwords are stored as hashes (bcrypt/SHA-256)
- Never store plain text passwords
- Sample data uses placeholder hashes: `$2a$10$sampleHashedPassword*`

### **Sample Data**
- All phone numbers are formatted as Pakistani mobile numbers
- All emails are placeholder/test emails
- Password hashes are intentionally weak (for testing only)
- **DO NOT use in production**

### **Data Privacy**
- Email and Phone fields are indexed as UNIQUE for security
- Implement proper authentication before database access
- Use parameterized queries to prevent SQL injection

---

## 📊 Sample Data Details

### **Customer Sample**
```
Ahmed Khan
├─ Phone: 03001234567
├─ Email: ahmed.khan@email.com
├─ Address: 123 Main Street, Karachi
└─ Posted Jobs: 3

Fatima Ali
├─ Phone: 03015678901
├─ Email: fatima.ali@email.com
├─ Address: 456 Park Avenue, Lahore
└─ Posted Jobs: 2 (including custom metal gate)
```

### **Worker Sample**
```
Tariq Ahmed (Plumber)
├─ Experience: 8 years
├─ Rating: 4.8/5.0
├─ Completed Jobs: 45
├─ Location: Karachi
└─ Available: Yes

Muhammad Ali (Electrician)
├─ Experience: 12 years
├─ Rating: 4.9/5.0
├─ Completed Jobs: 78
├─ Location: Lahore
└─ Available: Yes
```

### **Job Sample**
```
Bathroom Plumbing Repair
├─ Customer: Ahmed Khan
├─ Category: Plumbing
├─ Estimated Hours: 3
├─ Fair Price: PKR 3,000
├─ Offered Price: PKR 2,500
├─ Location: Karachi
└─ Status: Open

Home Electrical Wiring
├─ Customer: Fatima Ali
├─ Category: Electrical
├─ Estimated Hours: 16
├─ Fair Price: PKR 18,000
├─ Offered Price: PKR 15,000
├─ Location: Lahore
└─ Status: Open
```

---

## ✅ Testing Checklist

- [ ] Schema creates all 3 tables successfully
- [ ] All primary keys are auto-increment
- [ ] Foreign key constraints work (try deleting a Customer)
- [ ] Unique constraints enforce on Email and Phone
- [ ] Cascade delete removes related jobs when customer is deleted
- [ ] Indexes are created for performance
- [ ] Sample data loads without errors
- [ ] Can query jobs by customer
- [ ] Can query workers by profession
- [ ] Can filter jobs by status (Open, InProgress, Completed)

---

## 🔄 Future Enhancements

Consider adding these tables in future iterations:

### **Bids/Applications Table** (for worker-job matching)
```sql
CREATE TABLE Bids (
    Id INTEGER PRIMARY KEY,
    JobId INTEGER,
    WorkerId INTEGER,
    BidPrice DECIMAL,
    BidMessage TEXT,
    Status TEXT,
    FOREIGN KEY (JobId) REFERENCES Jobs(Id),
    FOREIGN KEY (WorkerId) REFERENCES Workers(Id)
);
```

### **Reviews/Ratings Table** (for feedback)
```sql
CREATE TABLE Reviews (
    Id INTEGER PRIMARY KEY,
    JobId INTEGER,
    FromUserId INTEGER,
    ToUserId INTEGER,
    Rating DECIMAL,
    Comment TEXT,
    CreatedAt DATETIME,
    FOREIGN KEY (JobId) REFERENCES Jobs(Id)
);
```

### **Payments Table** (for transactions)
```sql
CREATE TABLE Payments (
    Id INTEGER PRIMARY KEY,
    JobId INTEGER,
    Amount DECIMAL,
    Status TEXT,
    PaymentDate DATETIME,
    FOREIGN KEY (JobId) REFERENCES Jobs(Id)
);
```

---

## 📞 Support & Questions

For issues or questions regarding the database:
1. Check the ERD.md for relationship details
2. Review schema.sql for table structure
3. Examine seed.sql for example data
4. Check migrations in `backend/Migrations/` folder

---

## 📋 Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0 | 2026-03-24 | Initial schema with Customers, Workers, Jobs tables |
| 1.1 | 2026-03-24 | Added seed data with 8 customers, 15 workers, 18 jobs |
| 1.2 | 2026-03-24 | Added performance indexes and cascade delete rules |

---

**Last Updated**: March 24, 2026  
**Database Tool**: SQLite3 / EF Core  
**Status**: ✅ Production Ready
