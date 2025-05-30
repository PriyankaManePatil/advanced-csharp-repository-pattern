# Advanced C# Repository Pattern with Unit Testing (Reference Project)

![Reference Only](https://img.shields.io/badge/Project-Reference%20Only-lightgrey?style=flat-square&color=blue)

This repository demonstrates a **clean, production-grade reference** implementation of the Repository Pattern in C#. It follows **SOLID principles**, enforces **modular structure**, and includes **100% unit test coverage** using both xUnit and NUnit.

🛑 **Note:** This project is for **architectural reference only**. It is **not intended to be run** in production environments.

---

## 📁 Folder Structure

```
src/
  Core/           → Interfaces and Domain Entities
  Infrastructure/ → Repository Implementation and DbContext
  Shared/         → Logging Interface
  WebApi/         → Entry Point (Minimal API for simulation)

tests/
  RepositoryTests/ → Unit tests using xUnit and NUnit

docs/             → Architecture Diagrams (optional visual aid)
```

---

## 🔧 Tech Stack
- .NET 7 / .NET Core
- Entity Framework Core (In-Memory DB)
- xUnit & NUnit for testing
- Minimal API (for structure only)
- Clean folder separation

---

## ✅ Highlights

- ✅ One class per file
- ✅ Clear function and variable naming
- ✅ SOLID principles
- ✅ Dependency Injection
- ✅ Repository Pattern
- ✅ 100% test coverage (Add, Get, Update, Delete)
- ✅ Fully commented, production-style code

---

## 🧪 Testing Frameworks

| Framework | When to Prefer |
|-----------|----------------|
| **xUnit** | Preferred for CI/CD (GitHub Actions, .NET CLI) |
| **NUnit** | Common in legacy/enterprise test environments     |

You can run tests like this (if running locally):
```bash
dotnet test
```

---

## 🖼 Architecture Diagram

> Add this image to `docs/architecture-diagram.png` to visually represent your layers and relationships.

---

## 📜 License

This project is licensed under the MIT License. Feel free to fork and adapt.

---

## 🧠 Why This Project Exists
This repository exists to showcase how a senior developer or solution architect structures maintainable C# applications with full test coverage and scalable folder structure — even when not deployed.

Great for interview prep, mentoring, and architectural discussions.
