# Student Planner

A C# desktop application that helps students organize courses, assignments, availability, and commitments while automatically generating personalized study schedules based on deadlines, priorities, and available time.

## Overview

Student Planner is a Windows Forms application developed in C# with SQLite for persistent data storage.

The application allows users to manage their courses and tasks, define available study periods and existing commitments, and generate a personalized schedule based on their available time and task priorities.

The project is organized into separate Core, Data, and UI projects to separate business logic, data access, and presentation.

## Features

- Course management
- Task and assignment management
- Availability management
- Commitment management
- Automatic schedule generation
- Task prioritization based on deadlines and priority
- Schedule blocks based on available time
- SQLite data persistence

## Architecture

The application uses a layered structure with separate projects for core logic, data access, and the user interface.

```text
StudentPlanner.UI
        │
        ▼
StudentPlanner.Core
   ┌────┴────┐
   ▼         ▼
Repositories Scheduler
   │
   ▼
StudentPlanner.Data
   │
   ▼
SQLite Database
```

## StudentPlanner.Core

Contains the application's domain models, repository interfaces, and scheduling logic.

The scheduling component generates study blocks by considering task deadlines, task priority, availability, and existing commitments.

## StudentPlanner.Data

Contains the concrete repository implementations and SQLite database access.

Separate repositories are used for courses, tasks, availability, commitments, and schedule blocks.

## StudentPlanner.UI

Contains the Windows Forms user interface through which users manage their information and interact with the scheduling system.

## Scheduling

The scheduling system uses a greedy scheduling approach.

Tasks are ordered primarily by deadline and secondarily by priority. The scheduler then generates available time slots while accounting for existing commitments and assigns task work to those slots.

When insufficient available time exists to schedule all tasks, the application identifies the remaining unscheduled work.

## Technologies

- **Language:** C#
- **Framework:** Windows Forms
- **Database:** SQLite
- **Architecture:** Layered architecture, Repository Pattern
- **Development Environment:** Visual Studio

## How to Run
- Clone the repository.
- Open StudentPlanner.sln in Visual Studio.
- Build the solution.
- Run the StudentPlanner.UI project.
- Use the application to create courses, tasks, availability, and commitments.

## Future Improvements

Potential improvements include:

- More advanced scheduling and optimization strategies
- Additional scheduling constraints
- Improved schedule visualization
- Expanded testing
- Additional customization options
