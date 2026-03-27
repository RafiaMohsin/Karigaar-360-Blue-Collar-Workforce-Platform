# Karigaar-360: Blue Collar Workforce Platform

## 📋 Description

Karigaar-360 is a comprehensive web platform built with ASP.NET Core MVC that connects blue-collar workers with customers needing various services. The platform enables:
- **Workers** to create detailed profiles showcasing their skills, experience, and availability
- **Customers** to post job requirements with fair pricing and transparent bidding
- **Rating System** to build trust through verified customer/worker feedback
- **Fair Wage Assurance** through transparent pricing and dispute resolution mechanisms
- **Location-Based Matching** to connect workers and jobs by geographic proximity

---

## 👥 Team Members

| Name | Roll Number |
|------|------------|
| Aliya Rasheed | 24L-2544 |
| Aima Shakeel | 24L-2531 |
| Kashish Fatima | 24L-2605 |
| Rafia Mohsin | 24L-2595 |

---

## 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| **Backend** | ASP.NET Core MVC (C#) |
| **Frontend** | HTML5, CSS3, Bootstrap 5 (Razor Views) |
| **Database** | SQLite (Development) / Microsoft SQL Server (Production) |
| **ORM** | Entity Framework Core |
| **Version Control** | Git & GitHub |

---

## 📁 Project Structure

```
Karigaar-360-Blue-Collar-Workforce-Platform/
├── backend/                          # ASP.NET Core MVC Application
│   ├── Controllers/                  # API & View Controllers
│   ├── Models/                       # Data Models (Customer, Worker, Job)
│   ├── Views/                        # Razor Views (HTML Templates)
│   ├── data/                         # Database Context
│   ├── Migrations/                   # EF Core Migrations
│   ├── wwwroot/                      # Static Files (CSS, JS, Images)
│   ├── appsettings.json              # Configuration
│   └── Program.cs                    # Application Entry Point
│
├── frontend/                         # Static Frontend Files (Optional)
│   ├── index.html
│   ├── login.html
│   ├── register.html
│   └── css/
│
├── database/                         # 📌 Database Schema & Data
│   ├── schema.sql                    # Create TABLE statements
│   ├── seed.sql                      # Sample & Test Data
│   ├── ERD.md                        # Entity Relationship Diagram
│   └── README.md                     # Database Documentation
│
├── docs/                             # Documentation
│   ├── Iteration_1.docx              # Iteration 1 Report
│   └── report.docx                   # Final Project Report
│
├── README.md                         # This File
└── .gitignore                        # Git Ignore Rules
```

---

## 🚀 How to Run the Project

### Prerequisites
- **.NET 9.0 SDK** or later: [Download](https://dotnet.microsoft.com/download)
- **Git**: [Download](https://git-scm.com/downloads)
- **Visual Studio 2022** or **VS Code** (recommended)
- **SQLite** (included with .NET) or **SQL Server Express**

### Step 1: Clone the Repository

```bash
git clone https://github.com/RafiaMohsin/Karigaar-360-Blue-Collar-Workforce-Platform.git
cd Karigaar-360-Blue-Collar-Workforce-Platform
```

### Step 2: Setup Backend

```bash
# Navigate to backend directory
cd backend

# Restore dependencies
dotnet restore

# Apply database migrations (creates database & tables)
dotnet ef database update

# (Optional) Load sample data - see database/README.md
```

### Step 3: Run the Application

#### Option A: Using dotnet CLI
```bash
dotnet run
```
The application will start on: `https://localhost:5001` or `http://localhost:5000`

#### Option B: Using Visual Studio
1. Open `Karigaar360.csproj` in Visual Studio
2. Press `Ctrl+F5` to run without debugging
3. Application launches in default browser

#### Option C: Using Visual Studio Code
```bash
# Install C# extension if not already installed
dotnet run --project backend/Karigaar360.csproj
```

### Step 4: Access the Application

- **Home Page**: [http://localhost:5000/](http://localhost:5000/)
- **Customer Login**: [http://localhost:5000/Customer/Login](http://localhost:5000/Customer/Login)
- **Worker Login**: [http://localhost:5000/Worker/Login](http://localhost:5000/Worker/Login)
- **Post Job** (Customer): [http://localhost:5000/Job/Create](http://localhost:5000/Job/Create)

---

## 🗄️ Database Setup

### Quick Start (Recommended)

The project uses **Entity Framework Core** migrations for automatic database setup:

```bash
cd backend

# Apply migrations - automatically creates database and tables
dotnet ef database update

# (Optional) Drop database if needed
dotnet ef database drop
```

### Manual Database Setup

For detailed manual setup instructions, see [`database/README.md`](database/README.md)

```bash
# SQLite Setup
sqlite3 Karigaar360.db < database/schema.sql

# (Optional) Load sample data for testing
sqlite3 Karigaar360.db < database/seed.sql
```

### Sample Data Included

The `seed.sql` file contains:
- **8 Sample Customers** with contact information
- **15 Sample Workers** with professions and ratings
- **18 Sample Jobs** in various categories (Plumbing, Electrical, Carpentry, etc.)

This allows you to test the application immediately after database setup.

---

## 📊 Database Schema

The project uses three main tables:

### **Customers** Table
Stores information about users who post jobs.
```sql
- Id (Primary Key)
- FullName
- Phone
- Email
- Address
- PasswordHash
```

### **Workers** Table
Stores information about blue-collar professionals.
```sql
- Id (Primary Key)
- FullName
- Phone
- Profession
- ExperienceYears
- Location
- PasswordHash
- Rating
- TotalJobsCompleted
- IsAvailable
```

### **Jobs** Table
Stores job postings created by customers.
```sql
- Id (Primary Key)
- Title
- Description
- Category
- EstimatedHours
- FairPrice
- OfferedPrice
- Location
- CustomerId (Foreign Key → Customers)
- Status (Open, InProgress, Completed)
- CreatedAt
- CompletedAt
```

For detailed schema documentation, see: [`database/ERD.md`](database/ERD.md)

---

## 🎨 Project Design Resources

### Figma Design (Site Map)
View the project design and wireframes:
- [Design Prototype](https://www.figma.com/make/FdDniVVa68M7jm1rKmdaAw/Create-site-map?t=NzlDA6OzxDLXLbzy-1&preview-route=%2Fsitemap)

### Project Documentation
- Iteration 1 Report: [`docs/Iteration_1.docx`](docs/Iteration_1.docx)
- Final Report: [`docs/report.docx`](docs/report.docx)
- Database Documentation: [`database/README.md`](database/README.md)

---

## 🧪 Testing the Application

### Test Accounts (with sample data)

**Sample Customer Account**
```
Email: ahmed.khan@email.com
Phone: 03001234567
Password: [See seed.sql for hash]
```

**Sample Worker Account**
```
Phone: 03001111111
Name: Tariq Ahmed (Plumber)
Profession: Plumbing
Experience: 8 years
```

### Test Scenarios
1. Register a new customer account
2. Register a new worker profile
3. Customer posts a job
4. View available jobs
5. Check worker ratings and profiles
6. Filter jobs by category and location
7. View completed jobs and reviews

---

## 🔐 Security Features

- ✅ Password hashing (bcrypt)
- ✅ SQL injection prevention (parameterized queries via EF Core)
- ✅ CSRF token protection on forms
- ✅ Email & Phone uniqueness constraints
- ✅ User authentication (Session-based)
- ✅ Role-based access (Customer vs Worker)

**Note**: Never commit `.env` or `appsettings.json` with real API keys. Use `.env.example` template.

---

## 📦 Dependencies

### Main NuGet Packages
- `Microsoft.EntityFrameworkCore` - ORM
- `Microsoft.EntityFrameworkCore.Sqlite` - SQLite Provider
- `Microsoft.AspNetCore.Mvc.Razor` - View Engine
- `Bootstrap` - CSS Framework

To view all dependencies:
```bash
dotnet list package
```

---

## 🤝 Contributing

1. **Create a branch** for your feature:
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Commit your changes**:
   ```bash
   git commit -m "Add: your feature description"
   ```

3. **Push to GitHub**:
   ```bash
   git push origin feature/your-feature-name
   ```

4. **Create a Pull Request** with description of changes

---

## 📋 Requirements Compliance

✅ **GitHub Repository**: Public repository with all source code  
✅ **Folder Structure**: Backend, Frontend, Database separated  
✅ **Database Folder**: Contains schema.sql, seed.sql, ERD.md  
✅ **README.md**: Comprehensive setup and project information  
✅ **Documentation**: Iteration reports in docs/ folder  
✅ **No API Keys**: Using .env.example template  
✅ **Team Commits**: All members contribute to repository  

---

## 🐛 Troubleshooting

### Issue: Database not found
**Solution**: Run `dotnet ef database update` in backend directory

### Issue: Port already in use
**Solution**: Change port in `appsettings.json` or use: `dotnet run --urls "http://localhost:5005"`

### Issue: Migrations failed
**Solution**: 
```bash
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Issue: Static files (CSS/JS) not loading
**Solution**: Ensure wwwroot folder is present and configured in Program.cs

For more help, check: [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)

---

## 📞 Contact & Support

For issues, questions, or contributions:
- Create an **Issue** on GitHub
- Comment on a **Pull Request**
- Contact team members directly

---

## 📜 License

This project is part of FAST-NUCES coursework (Spring 2026).  
Educational use only.

---

## 📅 Project Timeline

| Phase | Duration | Status |
|-------|----------|--------|
| **Iteration 1** | Week 1-2 | ✅ Completed |
| **Iteration 2** | Week 3-4 | 🔄 In Progress |
| **Final Submission** | End of Semester | ⏳ Pending |

---

**Last Updated**: March 24, 2026  
**Version**: 1.0  
**Status**: ✅ Active Development
