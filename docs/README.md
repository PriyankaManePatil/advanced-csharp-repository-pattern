# Repository Pattern Learning Guide

This folder explains the code as an architectural reference. Read the documents in order:

1. [Fundamentals](01-fundamentals.md) — purpose, boundaries and request flow.
2. [Pattern catalogue](02-pattern-catalogue.md) — variants, implementation status and selection guidance.
3. [Code walkthrough](03-code-walkthrough.md) — why each project, interface and class exists.
4. [Decision guide](04-decision-guide.md) — choose a pattern based on the problem.
5. [Testing strategy](05-testing-strategy.md) — what to mock and what to integrate.
6. [Pitfalls](06-pitfalls-and-anti-patterns.md) — common repository mistakes.

The sample is intentionally executable enough for CI verification, but its purpose is education. EF Core
In-Memory is not a relational database and must not be used to claim production database correctness.
