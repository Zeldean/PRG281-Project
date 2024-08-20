# PRG 281 Project

## Title Page
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
1. [Introduction](#introduction)
2. [Topic Identification](#topic-identification)
3. [Flowchart or Pseudocode](#flowchart-or-pseudocode)
4. [Class Diagram](#class-diagram)
5. [Detailed Project Plan](#detailed-project-plan)
6. [Requirements Analysis](#requirements-analysis)
7. [Risk Analysis](#risk-analysis)
8. [Conclusion](#conclusion)
9. [References](#references)
10. [Appendices](#appendices)

---

## Introduction
### Overview
This project aims to develop a program that tracks prepaid electricity usage, allowing users to manually input readings from their meters and manage their power consumption more effectively.

### Problem Statement
Prepaid electricity users often struggle to keep track of their remaining units and know when to purchase more power. Our solution addresses this by offering a simple interface for inputting meter data and monitoring electricity usage over time.

### Project Objectives
- Track electricity usage by recording meter readings.
- Generate usage reports and alert users when their units are low.
- Provide a user-friendly system for managing prepaid electricity information.

---

## Topic Identification
### Classes and Objects
- **User:** Represents the user’s profile and stores meter information.
- **ElectricityMeter:** Tracks readings and power purchases.
- **UsageRecord:** Logs entries of meter readings and purchases.
- **Report:** Generates summaries and usage analysis.

### Custom Threads
- Threads will handle background tasks like monitoring meter readings for low units and generating alerts.

### Custom Events and Delegates
- Custom events will trigger alerts when predefined conditions, like low units, are met.

### Interfaces (Custom and Built-in)
- **IReportable:** Custom interface for generating various types of usage reports.
- **IDisposable:** Built-in interface to manage file handling.

### Polymorphism
- Different user types (e.g., regular user vs. admin) will inherit from a common base class.

### Custom and Built-in Exceptions
- Implement exceptions for invalid data entries and handle errors related to file operations.

### Security Measures
- Input validation to ensure data integrity.
- Data encryption for sensitive information (if applicable).

---

## Flowchart or Pseudocode
### Flowchart
![Flowchart Image](Project/bin/Debug/net8.0/Media/FlowChart_1.svg)
