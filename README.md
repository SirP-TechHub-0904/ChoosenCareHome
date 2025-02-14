# Chosen Health Care UK - Care Home Automation System

## Overview
[Chosen Health Care UK](https://chosenhealthcare.co.uk/) provides **personalized home care services**, assisting individuals in managing their health and daily activities. To streamline operations, improve caregiver coordination, and enhance patient care, a **custom automation system** was developed.

The **Chosen Health Care Automation System** is a **digital platform** that enables automated **shift management, appointment scheduling, caregiver-patient tracking, and communication** between caregivers and administrators. 

---

## Tech Stack
- **Backend:** ASP.NET Core (C#), Entity Framework Core, MSSQL  
- **Frontend:** Razor Pages / MVC, HTML, CSS, JavaScript  
- **Database:** Microsoft SQL Server  
- **Authentication:** ASP.NET Identity, Role-Based Access Control 
- **Scheduling:** Cron Jobs for shift allocations and reminders  

---

## Features & Modules

### 1. **Shift & Rota Management**
- Automated **shift allocation** based on caregiver **availability, location, and preferences**  
- Real-time rota updates with **dynamic scheduling adjustments**  
- **Shift reminders & alerts** for caregivers and administrators  

### 2. **User & Appointment Management**
- **Caregiver-patient assignment module** for better tracking  
- **Real-time duty tracking** with status updates  
- **Automated appointment scheduling** to optimize service delivery  

### 3. **Notifications & Messaging System**
- **Instant notifications** for shift changes, appointment updates, and admin messages  
- **Direct messaging feature** for real-time communication between caregivers and administrators  
- **Emergency alerts** for urgent tasks and patient requests  

### 4. **Healthcare Blog & News Section**
- Integrated **CMS (Content Management System)** for admins to post updates  
- **Health-related news and policies** shared with caregivers and patients  
- **Searchable archive** for easy access to important healthcare information  

### 5. **Admin Dashboard & Reporting**
- **Data-driven insights** on caregiver schedules, patient interactions, and compliance  
- **Exportable reports** for performance monitoring and workforce management  
- **Audit logs** to track shift allocations and notifications  

---

## Installation & Setup

### **1. Clone the repository**
```sh
git clone https://github.com/SirP-TechHub-0904/ChoosenCareHome.git
cd automation-system
