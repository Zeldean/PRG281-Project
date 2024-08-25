# PRG 281 Project

## Title Page

**Project Title:** Electricity Usage Tracking System for Prepaid Meters**Team Members:**

- **Dean Jacobus Andreas van Zyl (600367)**
- **Hermanus Jacobus Bantjes (601427)**
- **Jan-Paul Seaman (578081)**
- **Stiaan Megit (600819)**

**Course Name:** Programming 281
**Date of Submission:** 27 August 2024

---

## Table of Contents

1. [Introduction](#1-introduction)  
   1.1 [Overview](#overview)  
   1.2 [Problem Statement](#problem-statement)  
   1.3 [Project Objectives](#project-objectives)  
   1.4 [Development Approach](#development-approach)  

2. [Topic Identification](#2-topic-identification)  
   2.1 [Classes and Objects](#classes-and-objects)  
   2.2 [Custom Threads](#custom-threads)  
   2.3 [Custom Events and Delegates](#custom-events-and-delegates)  
   2.4 [Interfaces (Custom and Built-in)](#interfaces-custom-and-built-in)  
   2.5 [Polymorphism](#polymorphism)  
   2.6 [Custom and Built-in Exceptions](#custom-and-built-in-exceptions)  
   2.7 [Security Measures](#security-measures)  

3. [Flowchart](#3-flowchart)  

4. [Class Diagram](#4-class-diagram)  

5. [Detailed Project Plan](#5-detailed-project-plan)  
   5.1 [Project Goals](#project-goals)  
   5.2 [Task Allocation](#task-allocation)

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

![Flow Chart](Project/bin/Debug/net8.0/Media/FlowChart.svg)

---

## 4. Class Diagram

![Class Diagram](Project/bin/Debug/net8.0/Media/ClassDiagram.svg)

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

- **Core Features:**
  - Create users.
  - Log and track electricity usage.
  - Send alerts when units are low.
  - Generate usage reports.
- **Usability:** User-friendly interface for entering meter readings.

### Non-Functional Requirements

- **Performance:** Real-time processing of user input and alerts.
- **Security:** Input validation to prevent invalid data and protect user information.
- **Scalability:** The program should handle multiple users and different electricity meters.

---

## 7. Risk Analysis

### Potential Risks

1. **Time Management:** Balancing project deadlines with other coursework.
2. **Technical Challenges:** Implementing threading, event handling, and file I/O.
3. **Data Integrity:** Ensuring data is saved correctly and remains consistent.

### Mitigation Strategies

- Regular team meetings to track progress.
- Early testing to identify and fix issues before final development.
- Regular backups of user data to prevent loss.

---

## 8. Conclusion

This documentation outlines the key components of our electricity usage tracking system. The next phase involves the implementation stage, where the planned features will be developed and integrated into a cohesive application. Testing and iteration will refine the system to ensure it meets user needs.

---

## 9. Appendices

[**Draw io**](https://app.diagrams.net): Used to create flow chart and class diagram.

[**W3 Schools**](https://www.w3schools.com): Used to find spesific syntax for code.
