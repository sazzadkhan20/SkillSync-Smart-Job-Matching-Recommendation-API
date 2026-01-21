# SkillSync – Smart Job Matching & Recommendation System (ASP.NET Core Web API)

SkillSync is a **Smart Job Matching and Recommendation System** built using **ASP.NET Core Web API** that automates job–candidate matching based on skill compatibility, experience level, and application status.  
The system follows a **clean 4-Layer Architecture**, supports **automated candidate notifications**, and is designed to scale for real-world recruitment platforms.

> ⚡ This project is **fully developed and maintained by a single contributor**.

---

## 📌 Key Features

- 🔍 **Smart Job Matching**
  - Matches candidates to jobs using skill match percentage
  - Experience level and job requirements considered
- 🤖 **Automated Recommendation Engine**
  - Shortlists candidates automatically
  - Supports Interview & Hired decision flows
- 📩 **Bulk Email Notification System**
  - Sends automated emails to selected candidates
  - Async & non-blocking email delivery
- 📊 **Advanced Search, Filter & Sort**
  - Filter by skills, experience, location, date, status
  - Sort by posted date (ASC / DESC)
- 🧠 **Domain-Driven Business Logic**
  - Clean separation of business rules
- ⚙️ **RESTful API Design**
  - Fully async endpoints
  - Clean DTO mapping
- 🏗 **4-Layer Architecture**
  - Maintainable, scalable, testable structure

---

## 🏛 Architecture Overview (4-Layer)

```

SkillSync.API
│
├── AppLayerAPI (Presentation Layer)
│   └── Controllers
│
├── BLL (Business Logic Layer)
│   ├── Services
│   ├── DTOs
│   ├── DomainLogic
│   └── Enums
│
├── DAL (Data Access Layer)
│   ├── EF
│   ├── Repositories
│   ├── Interfaces
│   └── DbContext
│
└── Shared / Utilities

````

### 🔹 Layer Responsibilities

| Layer | Responsibility |
|------|---------------|
| Presentation | Handles HTTP requests & responses |
| Business Logic | Core application rules & workflows |
| Data Access | Database operations (EF Core) |
| Utilities | Helpers, Email Service, Mappers |

---

## 🛠 Technologies Used

- **ASP.NET Core 8 Web API**
- **Entity Framework Core**
- **AutoMapper**
- **SQL Server**
- **SMTP Email (Async)**
- **LINQ & Expression Filtering**
- **Dependency Injection**
- **Task / async-await**
- **REST API Best Practices**

---

## 🧩 Core Modules

### 👤 Candidate Management
- Candidate profiles
- Skills & experience tracking

### 💼 Job Post Management
- Job creation & publishing
- Skill requirement definition

### 📑 Job Application Module
- Apply to jobs
- Track application status
- Skill match calculation

### 🧠 Skill Match Engine
- Calculates compatibility percentage
- Domain-based logic (clean & testable)

### 📩 Notification Engine
- Interview / Hired email automation
- Bulk email sending
- Enum-based decision handling

---

## 📬 Automated Email Workflow

1. API receives jobId + minimum skill match
2. System filters eligible candidates
3. Decision type (Interview / Hired) applied
4. Bulk async emails sent
5. Invalid or null emails skipped safely

---

## 🌐 Example API Endpoints

### 🔍 Search & Filter Jobs
```http
GET /api/jobs/search?skills=C%23,SQL&location=Dhaka&experience=2&sort=desc
````

### 📩 Notify Candidates

```http
POST /api/jobs/{jobId}/notify
```

```json
{
  "minimumSkillMatch": 70,
  "decisionType": "Interview"
}
```

---

## 🔐 Security & Best Practices

* Async everywhere (`Task`, `await`)
* DTO-based data exposure
* Enum validation
* Null-safe operations
* SMTP authentication via App Passwords
* Clean exception handling

---

## 🧪 Error Handling Strategy

* Domain validation errors
* Graceful API responses
* No application crashes on invalid data
* Logging-ready structure

---

## 🚀 How to Run the Project

1. Clone the repository

```bash
git clone https://github.com/yourusername/SkillSync.git
```

2. Configure database connection in `appsettings.json`

3. Run migrations

```bash
dotnet ef database update
```

4. Run the API

```bash
dotnet run
```

---

## 👨‍💻 Contribution

> This project is **solely developed and maintained by me** as a full-stack backend system focusing on:

* Clean architecture
* Real-world recruitment logic
* Scalable enterprise design

---

## 📌 Future Enhancements

* AI-based skill matching
* Background email queues
* Role-based authentication (JWT)
* Admin dashboard
* Analytics & reports

---

## 📄 License

This project is for **educational and portfolio purposes**.
All rights reserved by the author.

---

## ⭐ Final Note

SkillSync demonstrates **enterprise-level backend engineering**, clean architecture, and real-world automation logic using **ASP.NET Core Web API**.

If you find this project useful, give it a ⭐!

``
