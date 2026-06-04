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
- PlantUML
- Draw.io / diagrams.net

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

---

## Class Responsibilities

### MainForm

The main graphical interface of the application. It handles button clicks, displays password and hash values, starts and stops brute-force attacks, updates progress, and displays results.

### AppSettings

Stores application-wide constants such as the static salt, character set, password length rules, brute-force length rules, and maximum worker thread count.

### PasswordGenerator

Generates a random password using the character set and password length range defined in `AppSettings`.

### PasswordHasher

Hashes passwords using SHA256 with the static salt defined in `AppSettings`.

### PasswordValidator

Checks whether a candidate password matches the target SHA256 hash.

### BruteForceGenerator

Generates all possible password combinations from length 1 up to length 6.

### SingleThreadBruteForcer

Runs the brute-force attack using one thread and records the result.

### MultiThreadBruteForcer

Runs the brute-force attack using multiple tasks. The number of worker tasks is limited to CPU cores minus one.

### PerformanceLogger

Writes attack performance results to a log file for comparison between single-thread and multi-thread brute-force methods.

### AttackResult

Stores the result of a brute-force attack, including success status, found password, attempts checked, elapsed time, and threads used.

### ProgressInfo

Stores progress data such as attempts checked, total combinations, current password length, and progress percentage.

---

## How the Application Works

1. The user clicks **Create Password**.
2. The application randomly generates a password with length 4 or 5.
3. The password is hashed using SHA256 with a static salt.
4. The user can start either:
   - Single-thread brute-force attack
   - Multi-thread brute-force attack
5. The brute-force algorithm starts from length 1 and continues up to length 6.
6. Candidate passwords are generated and validated against the target hash.
7. When the password is found, the result is displayed in the GUI.
8. The application logs the performance result into the `Logs` folder.

---

## Multithreading Explanation

The multi-thread brute-force attack uses Task-based parallel execution.

The maximum number of worker tasks is calculated as:

```text
CPU cores - 1
```

This keeps one CPU core free so the system remains responsive while the brute-force attack is running.

Each worker searches a different part of the password space. This avoids duplicate work and demonstrates real parallel execution.

When one worker finds the password, cancellation is requested and the other workers stop.

---

## Performance Logging

The application logs performance results for both single-thread and multi-thread brute-force attacks.

Each log entry includes:

- Date and time
- Attack type
- Generated password
- Target SHA256 hash
- Success status
- Found password
- Attempts checked
- Elapsed time
- Threads used

The log file is stored in:

```text
Logs/performance-log.txt
```

---

## UML Diagram

The UML class diagram is stored in the `Docs` folder.

Files included:

```text
Docs/uml-class-diagram.puml
Docs/password_bruteforce_uml_clean_fixed.drawio
```

The `.puml` file is the PlantUML source version of the diagram.

The `.drawio` file is the visual Draw.io version used for presentation and final report preparation.

---

## Version History

### Version 1 - Initial Project Setup

- Created Windows Forms project.
- Added basic folder structure.
- Added README file.
- Prepared project for future implementation.

### Version 2 - Application Settings and Constants

- Added `AppSettings` class.
- Defined static salt.
- Defined character set.
- Defined generated password length range `[4-6)`.
- Defined brute-force search range from length 1 to 6.
- Added maximum worker thread calculation using CPU cores minus one.

### Version 3 - SHA256 Password Hashing Service

- Added `PasswordHasher` class.
- Implemented SHA256 password hashing.
- Added static salt usage from `AppSettings`.
- Converted hash byte output into hexadecimal string format.

### Version 4 - Random Password Generator

- Added `PasswordGenerator` class.
- Implemented random password generation.
- Used password length range `[4-6)`.

### Version 5 - Password Validator Service

- Added `PasswordValidator` class.
- Implemented candidate password validation against a target SHA256 hash.
- Kept validation separate from brute-force generation.

### Version 6 - Brute-Force Combination Generator

- Added `BruteForceGenerator` class.
- Implemented generation of all possible combinations.
- Generator starts from password length 1.
- Generator continues up to maximum length 6.

### Version 7 - Single-Thread Brute-Force Attack

- Added `AttackResult` model.
- Added `SingleThreadBruteForcer` class.
- Connected brute-force generator with password validator.
- Added elapsed time measurement and attempt counting.

### Version 8 - Multi-Thread Brute-Force Attack

- Added `MultiThreadBruteForcer` class.
- Implemented Task-based parallel brute-force search.
- Used CPU cores minus one rule.
- Added shared cancellation after password is found.

### Version 9 - Graphical Interface Controls

- Added main GUI controls.
- Added password creation button.
- Added single-thread and multi-thread attack buttons.
- Added stop button, progress bar, elapsed time display, and result output.

### Version 10 - Connect Password Creation to GUI

- Connected the Create Password button to the password generator.
- Displayed generated password and SHA256 hash in the GUI.

### Version 11 - Connect Single-Thread Attack to GUI

- Connected single-thread brute-force attack to the GUI.
- Used `Task.Run` to keep the GUI responsive.
- Added stop support using `CancellationTokenSource`.

### Version 12 - Connect Multi-Thread Attack to GUI

- Connected multi-thread brute-force attack to the GUI.
- Displayed threads used, attempts checked, elapsed time, and found password.

### Version 13 - Performance Logging

- Added `PerformanceLogger` class.
- Logged single-thread and multi-thread attack results.
- Created performance log file in the `Logs` folder.

### Version 14 - Progress and Status Display Improvements

- Added `ProgressInfo` model.
- Added live progress updates.
- Added live elapsed time updates.
- Displayed current password length and attempts checked.

### Version 15/16 - Graphical Interface Layout Polish

- Improved Windows Forms layout.
- Added section headings.
- Added dedicated status label.
- Improved spacing and presentation quality.

### Version 17 - UML Class Diagram

- Added UML class diagram source file.
- Added Draw.io UML diagram.
- Documented class relationships.

### Version 18-19 - README Documentation Polish

- Updated project documentation.
- Added full project structure.
- Added class responsibility explanations.
- Added application workflow explanation.
- Added multithreading explanation.
- Added performance logging explanation.

---

## How to Run the Project

1. Clone or download the repository.
2. Open the solution in Visual Studio.
3. Make sure the project is set to Debug mode.
4. Build the solution.
5. Run the application.
6. Click **Create Password**.
7. Run either the single-thread or multi-thread brute-force attack.
8. Check the result output and performance log.

---

## Testing Notes

During testing, the application should show:

- Generated password
- SHA256 hash
- Progress percentage
- Elapsed time
- Attempts checked
- Number of threads used
- Found password
- Performance log entry

The found password should match the generated password.

---

## Educational Purpose Notice

This application is for educational use only. It demonstrates brute-force password recovery against a locally generated hash inside the application. It must not be used against real systems, real accounts, websites, or any password data that does not belong to the user.
