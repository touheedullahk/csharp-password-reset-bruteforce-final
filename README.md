# C# Password Reset Brute Force Application

## Project Objective

This project is a C# Windows Forms application that demonstrates a local password reset simulation using brute-force password recovery.

The application generates a random password, hashes it using SHA256 with a constant static salt, and then attempts to recover the password using both single-threaded and multi-threaded brute-force methods.

This project is created for educational purposes only. It is designed to demonstrate hashing, brute-force search, multithreading, progress tracking, performance logging, and GitHub-based version control.

---

## Technologies Used

- C#
- Windows Forms
- .NET 8
- SHA256 hashing
- Static salt
- Task-based multithreading
- CancellationTokenSource
- GitHub version control
- Draw.io / UML class diagram

---

## Main Features

- Random password generation
- Password length randomly generated between `[4-6)`, meaning 4 or 5 characters
- SHA256 hashing with a constant static salt
- Start and stop brute-force attack
- Single-thread brute-force mode
- Multi-thread brute-force mode
- Maximum worker threads limited to CPU cores minus one
- Brute-force search starts from length 1 and continues up to length 6
- Brute-force algorithm does not know the password length in advance
- Separate brute-force generator and password validator classes
- Live progress display
- Live elapsed time display
- Attempts checked display
- Threads used display
- Found password output
- Performance logging for single-thread and multi-thread attacks
- UML class diagram
- Development notes for final report preparation

---

## Project Structure

```text
PasswordBruteForceApp/
│
├── Models/
│   ├── AttackResult.cs
│   └── ProgressInfo.cs
│
├── Services/
│   ├── AppSettings.cs
│   ├── PasswordGenerator.cs
│   ├── PasswordHasher.cs
│   ├── PasswordValidator.cs
│   ├── BruteForceGenerator.cs
│   ├── SingleThreadBruteForcer.cs
│   ├── MultiThreadBruteForcer.cs
│   └── PerformanceLogger.cs
│
├── Logs/
│   └── performance-log.txt
│
├── Docs/
│   ├── uml-class-diagram.puml
│   └── password_bruteforce_uml_clean_fixed.drawio
│
├── MainForm.cs
├── MainForm.Designer.cs
├── Program.cs
├── README.md
└── development-notes.md
```
