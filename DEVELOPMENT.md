# Workout Planner Development Documentation

## Project Overview
The Workout Planner is a C# console application designed to help users create, customize, and manage their workout routines and nutritional information. 
It features user authentication and integrates with external APIs to provide exercise and nutritional data.


# Setup Instructions

## Prerequisites
- .NET SDK 6.0 or higher
- Visual Studio 2022 or another compatible IDE

## Cloning the Repository
```
git clone https://github.com/TamarNoselidze/WorkoutPlanner.git
cd WorkoutPlanner
```

Ensure you have all required libraries, such as Newtonsoft.Json for JSON parsing.

Create a `database.json` file in the root directory of the project. 
This file will store user data (a sample one is provided, feel free to overwrite).



## Building the Project

Project structure: 
```
/WorkoutPlanner
├── /bin                   # Compiled binaries
├── /obj                   # Temporary files
├── WorkoutPlanner.cs      # Main entry point and application logic
├── DatabaseHelper.cs      # Database helper class
├── Exercise.cs            # Exercise helper class
├── ExerciseDB.cs          # Database exercises are fetched from 
├── NutritionalInfo.cs     # Nutritional information helper class
├── NutritionalDB.cs       # Database nutritional info is fetched from
├── WorkoutDB.cs           # Database workouts are fetched from
├── User.cs                # User model
├── WorkoutPlanner.sln     # Visual Studio solution file
   . . .
└── README.md              # Project README file
```


# Code Documentation

## Classes

### WorkoutPlanner
The main class for the Workout Planner application.
Handles user registration, login, and requests for exercises and nutritional information.

### DatabaseHelper
Provides methods to interact with the user database, including loading, saving, registration, and login functionalities.

### Exercise
Represents an exercise with properties such as name, targeted body part, equipment, and instructions.

### ExerciseDB
Provides methods for interacting with the ExerciseDB API to fetch exercise data.

### NutritionalInfo
Holds the nutritional information data.

### NutritionalDB
This class handles fetching nutritional information.

### User
Represents a user in the workout planner system, including their saved exercises and nutritional information.

### WorkoutDB
Handles fetching workout exercises from the Workout API.


__For more detailed information see the documented code, or an XML documentation at `bin/debug/net5.0/WorkoutPlanner.xml`__