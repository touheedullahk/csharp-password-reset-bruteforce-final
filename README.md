# C# Password Reset Brute Force Application

## Project Objective

This project is a C# Windows Forms application that demonstrates a local password reset simulation using brute-force password recovery.

The application will generate a random password, hash it using SHA256 with a static salt, and then attempt to recover the password using both single-threaded and multi-threaded brute-force methods.

This project is created for educational purposes only.

## Technologies Used

- C#
- Windows Forms
- .NET 8
- SHA256 hashing
- Task-based multithreading
- GitHub version control

## Planned Features

- Random password creation
- SHA256 hashing with static salt
- Start and stop brute-force attack
- Single-thread brute-force mode
- Multi-thread brute-force mode
- Progress display
- Elapsed time display
- Found password output
- Performance logging
- UML class diagram
- Final test report

## Project Structure

```text
PasswordBruteForceApp/
│
├── Models/
│   └── Model classes will be added here
│
├── Services/
│   └── Main functionality classes will be added here
│
├── Logs/
│   └── Performance log files will be stored here
│
├── Docs/
│   └── UML diagram and report materials will be stored here
│
├── Program.cs
└── Main Windows Forms files