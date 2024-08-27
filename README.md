# PRG 281 Project

## Milestone 1

**Project Title:** Electricity Usage Tracking System for Prepaid Meters  
**Team Members:**

- **Dean Jacobus Andreas van Zyl (600367)**
- **Hermanus Jacobus Bantjes (601427)**
- **Jan-Paul Seaman (578081)**
- **Stiaan Megit (600819)**

**Course Name:** Programming 281  
**Date of Submission:** 27 August 2024

---

## Table of Contents

1. [Introduction](#1-introduction)
   - [Overview](#overview)
   - [Problem Statement](#problem-statement)
   - [Project Objectives](#project-objectives)
   - [Development Approach](#development-approach)

2. [Topic Identification](#2-topic-identification)
   - [Classes and Objects](#classes-and-objects)
   - [Custom Threads](#custom-threads)
   - [Custom Events and Delegates](#custom-events-and-delegates)
   - [Interfaces (Custom and Built-in)](#interfaces)
   - [Polymorphism](#polymorphism)
   - [Custom and Built-in Exceptions](#exceptions)
   - [Security Measures](#security-measures)

3. [Flowchart](#3-flowchart)

4. [Class Diagram](#4-class-diagram)

5. [Detailed Project Plan](#5-detailed-project-plan)
   - [Project Goals](#project-goals)
   - [Task Allocation](#task-allocation)

6. [Requirements Analysis](#6-requirements-analysis)
   - [Functional Requirements](#functional-requirements)
   - [Non-Functional Requirements](#non-functional-requirements)

7. [Risk Analysis](#7-risk-analysis)
   - [Potential Risks](#potential-risks)
   - [Mitigation Strategies](#mitigation-strategies)

8. [Conclusion](#8-conclusion)

9. [Appendices](#9-appendices)

---

## 1. Introduction

### Overview

The objective of this project is to create a software application that enables users to monitor their usage of prepaid electricity. The program will allow users to enter meter readings manually and gain better control over their electricity consumption.

### Problem Statement

Prepaid electricity users often struggle to keep track of remaining units and knowing when to purchase more power. Our solution aims to address this challenge by providing a user-friendly interface for inputting meter data and effectively monitoring electricity usage trends over time.

### Project Objectives

- Track electricity usage by recording meter readings.
- Generate usage reports and alert users when their units are low.
- Provide a user-friendly system for managing prepaid electricity information.

### Development Approach

We are utilizing an iterative methodology involving continuous feedback and testing at each development stage. After completing each goal, we will incorporate the features and have them evaluated by other team members. This allows us to make necessary adjustments and refinements. Feedback will be documented, logged, and assigned to team members for further review and action.

---

## 2. Topic Identification

### Classes and Objects

**Classes** are templates that act as placeholders or references used when creating and inserting objects. On their own, classes don’t hold any information until they are populated with objects. Classes define properties such as a person's ID, name, age, and more.

**Objects** are instances of classes that hold specific data. They are created when data is provided for the properties defined in a class, allowing us to reuse the class structure without needing to redefine it every time.

- **Why:** Classes and objects simplify the process of defining, creating, storing, and retrieving information. By defining a class once, we can create multiple instances (objects) that hold user data, power usage information, and perform related calculations.
- **How:** We will create different classes to manage user data, power usage, menu items, and calculations. Objects will be inserted into these classes, allowing us to track and manage multiple instances of user information and power usage without needing to recreate instances every time.

### Custom Threads

Threads allow programs to perform multiple tasks concurrently, which can improve speed and efficiency. Custom threads run independently from the main thread, allowing certain tasks to execute without affecting the main program flow.

- **Why:** Custom threads enable tasks to run in parallel, preventing the interface from freezing while waiting for other processes to complete.
- **How:** We will create threads for tasks like retrieving text files containing program data, ensuring the program doesn't slow down while waiting for this information. Threads will also be used to append messages to events and trigger certain events without impacting the main thread.

### Custom Events and Delegates

Events are actions triggered by specific conditions within the program, while delegates define what actions should be performed when those events occur. Events monitor conditions and, when met, execute the delegate’s instructions (e.g., displaying a message).

- **Why:** Using events and delegates reduces code redundancy. Instead of repeatedly checking for conditions manually, events trigger automatically and execute the appropriate delegate, leading to more efficient code execution.
- **How:** We will implement events such as alerts when power consumption is high or when a mid-year report is due. Delegates will point to methods like `Message`, which will display relevant notifications based on the event’s condition.

### Interfaces

Custom interfaces define a contract for methods and properties that can be inherited and implemented by classes. Interfaces allow us to separate method definitions from implementation, providing flexibility and the ability to implement multiple interfaces in a single class.

- **Why:** Interfaces improve code organization by allowing methods to be inherited into multiple classes, making changes and updates easier to manage.
- **How:** We will create interfaces for managing messages and notifications. These interfaces will allow us to reuse and easily call different messages from a centralized location, keeping the code clean and modular.

### Built-In Interfaces

- **Built-In Interfaces:** These include interfaces like `IComparable` for sorting objects and `IEnumerable` for iterating through collections.

- **Why:** Built-in interfaces provide standard features that align with .NET framework and language-specific practices, ensuring compatibility and ease of integration.
- **How:** We will use `IComparer<T>` in the `EntryCompare` class to sort entries by date, leveraging its `Compare` method for efficient data management.

### Polymorphism

- **Polymorphism:** This concept allows objects to be treated as instances of their parent class, enabling method overriding and overloading.

- **Why:** Polymorphism enhances code reusability and flexibility, allowing different classes to be managed through a common interface.
- **How:** We will use method overloading in the `Navigation` class to handle different parameters for menu items and allow method overriding in the `EntryList` and `Calculations` classes for specific functionality.

### Exceptions

- **Exceptions:** Used for handling errors and exceptional situations, with built-in exceptions like `ArgumentOutOfRangeException` and custom exceptions for application-specific scenarios.

- **Why:** Proper exception handling improves application robustness and error management.
- **How:** We will implement basic error-checking mechanisms in the `Calculations` class and use built-in exceptions in the `Navigation` class to handle specific errors related to menu selections.

### Security Measures

- **Security Measures:** Techniques to protect the application and its data from unauthorized access, data breaches, and attacks. This includes data validation, access control, and encryption.

- **Why:** Security measures safeguard user data and maintain application integrity and trustworthiness.
- **How:** We will use encapsulation to protect data within classes, validate user input to prevent invalid data, and implement basic validation in the `CreateUser` method to ensure data integrity and user experience.

---

## 3. Flowchart

![Flow Chart](https://raw.githubusercontent.com/Zeldean/PRG281-Project/e71a74e29e885a1cc4fe8a9c575d921bda88fe03/Project/bin/Debug/net8.0/Media/FlowChart.svg)

This flowchart outlines the user interaction and data management processes within the application. It begins by checking for existing users and loading or creating user data as needed. The user can then interact with the main menu, which offers options for creating new entries, viewing history, generating reports, and configuring settings. Each option triggers a specific process, such as data input, retrieval, or calculation. The flowchart also includes error handling and back options, ensuring a smooth and user-friendly experience. The structure is designed to facilitate efficient data management and user interaction within the application.

---

## 4. Class Diagram

![Class Diagram](https://raw.githubusercontent.com/Zeldean/PRG281-Project/e71a74e29e885a1cc4fe8a9c575d921bda88fe03/Project/bin/Debug/net8.0/Media/ClassDiagram.svg)

This class diagram represents the structure of the application, focusing on user management, data entry, calculations, notifications, and menu operations. The `User` class handles user-specific data and file management, while the `Entry` and `Entry-List` classes manage individual entries and their collections. The `Calculation` class provides methods for calculating usage statistics. Notifications are managed through the `Notification` class, which interacts with the `I-Notification.Methods` interface. The `Menu` and `Menu-Item` classes define the structure of the application's user interface, allowing dynamic interaction. Overall, this diagram provides a clear depiction of how various components of the application interact to manage and process user data effectively.

---

## 5. Detailed Project Plan

### Project Goals

1. **Rough Documentation:** Create the preliminary version of documentation.
2. **Core Functionality:** Implement core functionality.
3. **Program Navigation:** Implement overall program navigation.
4. **User File Management:** Implement file creation, data storage, and reading methods.
5. **Power Usage Calculations:** Implement average usage calculation methods.
6. **Report Generation:** Implement report generation methods.
7. **Generate Test Data:** Create entries for testing.
8. **Testing:** Test the program thoroughly.
9. **Final Documentation:** Finalize documentation.

### Task Allocation

- **Dean:**
  - Design and implement core classes like `User`, `Entry`, `Navigation`
  - Implement methods for data management like `ReadUserData`, `ImportUserData`, `ExportUserData`, `ClearData`.
  - Implement methods for file management like `FileExists`, `CreateUserFile`.
  - Design and implement console app navigation class with the ability to `create menus`, `add menu items`, `exicute code when menu item is selected`, `console disply and clear`.

- **Hermanus:**
  - Implement `EntryList` class: Manages methods that retrieve information from a text file and display it.
  - Implement `ReadEntries` method: Retrieves entries from the text file, adds them to a list, and allows them to be displayed and modified.
  - Implement Inheritance: Allows classes to inherit from the `EntryList` class and use its methods.

- **Jan-Paul Seaman:**  
  - Implement `Calculation` class: Inherits from `User` class and manages data calculations.
  - Implement `TotalUsage` method: Generate reports for different periods (Weekly, Monthly, Yearly).
  - Implement `CalculateTotal` method: Handle complex calculation logic for "Type1" and "Type2" entries.
  - Handle entry processing: Includes filtering, grouping by month, and predicting usage based on previous data.
  - Implement `EntryCompare` class: Inherits the built-in interface `IComparable` to sort entries for the yearly report, placing the newest month at the top of the list.
  - **Additional Details:**
    - **Constructor:** Initializes the class, uses the `ReadUserData` method from the `EntryList` class, and calculates total usage based on the report type.
    - `EntryCompare` class: Sorts entries in descending order based on the date.

- **Stiaan:**
  - Implement `Notification` class: Manages the methods that provide notification messages.
  - Implement `GeneratingNote` method: Reads through the text file and returns the units from the last entry.
  - Implement `Respons` method: Uses the units returned from the `GeneratingNote` method to check if the units are less than 50 units; if true, it generates a notification message.
  - Implement `Message` method: Stores the response from the `Respons` method.
  - Implement `MultiThreading & EventHandling`: The `GeneratingNote` method is passed to `Thread1`, which runs and retrieves the units from the last entry. When `Thread1` finishes, `Thread2` runs. `Thread2` contains the event that points to the `Note` delegate, which uses the `Message` method.
  - Implement `CreateEntry` method: Takes input from the user and inserts the input as a string into the text file at the bottom of the page. Takes current units input and purchased units input.
  - Class Diagram

---

## 6. Requirements Analysis

### Functional Requirements

1. **Meter Data Management:** The system should allow users to enter and store electricity meter readings.
2. **Usage Tracking:** The system should track electricity usage over time and alert users when their units are low.
3. **Report Generation:** The system should generate usage reports.
4. **User Management:** The system should manage multiple user profiles.

### Non-Functional Requirements

1. **Usability:** The system should be user-friendly and intuitive.
2. **Performance:** The system should handle data input and retrieval efficiently.
3. **Reliability:** The system should be reliable and provide accurate tracking and alerts.
4. **Security:** The system should securely manage user data.

---

## 7. Risk Analysis

### Potential Risks

1. **Inaccurate Meter Readings:** Users may enter incorrect meter readings, leading to inaccurate tracking.
2. **System Crashes:** The system may experience crashes or bugs that impact performance.
3. **Data Security:** Unauthorized access to user data could lead to privacy concerns.
4. **User Errors:** Users may struggle to navigate the system, leading to incorrect usage.

### Mitigation Strategies

1. **Data Validation:** Implement validation checks for meter readings to ensure data accuracy.
2. **Error Handling:** Implement robust error handling and logging to minimize crashes and bugs.
3. **Access Control:** Implement secure login and access control measures to protect user data.
4. **User Training:** Provide clear instructions and user guides to help users navigate the system.

---

## 8. Conclusion

This project aims to develop an effective electricity usage tracking system for prepaid meters. By addressing the problem of manual tracking and introducing features like alerts and reports, the system will provide users with better control over their electricity consumption. The project plan outlines the development approach, including task allocation, risk management, and the use of various programming concepts to ensure a successful outcome.

---

## 9. Appendices

### Appendix A: Data Definitions

- **Meter Reading:** The value recorded from the electricity meter.
- **Unit:** A measure of electricity consumption.
- **User Profile:** A set of data specific to an individual user, including meter readings and usage history.

### Appendix B: Test Plan

- **Functional Tests:** Test meter data entry, usage tracking, and report generation.
- **Performance Tests:** Test system responsiveness and efficiency.
- **Security Tests:** Test access control and data protection measures.

---
