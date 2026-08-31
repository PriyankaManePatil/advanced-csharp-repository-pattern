# Repository Pattern Learning Guide

This folder explains the code as an architectural reference. Read the documents in order:

1. [Fundamentals](01-fundamentals.md) — purpose, boundaries and request flow.
2. [Pattern catalogue](02-pattern-catalogue.md) — variants, implementation status and selection guidance.
3. [Code walkthrough](03-code-walkthrough.md) — why each project, interface and class exists.
4. [Decision guide](04-decision-guide.md) — choose a pattern based on the problem.
5. [Testing strategy](05-testing-strategy.md) — what to mock and what to integrate.
6. [Pitfalls](06-pitfalls-and-anti-patterns.md) — common repository mistakes.
7. [Interview preparation](07-interview-preparation.md) — concise answers and discussion prompts.
8. [Glossary](08-glossary.md) — important Repository Pattern, EF Core and architecture terms.
9. [Hands-on exercises](09-hands-on-exercises.md) — progressive practice tasks with expected outcomes.
10. [Request lifecycle](10-request-lifecycle.md) — trace one HTTP request through every layer.
11. [File-by-file map](11-file-by-file-reference.md) — what to read, why it exists and what to notice.

The sample is intentionally executable enough for CI verification, but its purpose is education. EF Core
In-Memory is not a relational database and must not be used to claim production database correctness.
