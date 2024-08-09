# WorkoutPlanner

## Introduction
The Workout Planner is a Java-based console application designed to help users create, customize, and manage their workout routines and nutritional information. 
It features user authentication, allowing users to create accounts and log in to personalize their fitness plans. 
The application integrates with external APIs to fetch general and tailored exercises based on muscle groups, equipment, and intensity levels. 
Users can also retrieve and save detailed nutritional information, including BMI, caloric needs, and macronutrient recommendations. 
The program stores user data locally in a JSON file, ensuring personalized and persistent user experiences. 
It provides an intuitive interface for viewing saved exercises and nutritional information, making it a comprehensive tool for fitness enthusiasts. 
Future enhancements may include a graphical user interface and additional features for tracking progress and goals.

# Features

- User Authentication: Users can create accounts or log in with existing credentials to personalize their workout routines.
- Exercise Database: A local database of exercises, including exercise name, muscle groups targeted and instructions.
- Workout Routine Creation: Users can design their workout routines by selecting exercises, specifying sets, reps, and rest intervals.
- Workout Suggestions: The system provides workout suggestions based on users' fitness goals.
- Nutritional Information: Users can fetch and save detailed nutritional information, including BMI, caloric needs, macronutrients, minerals, and vitamins.


## Getting Started

# Requirements

- .NET SDK 6.0 or higher
- Internet connection for API access


# Setup and Installation

1. Clone the Repository:

    ```
    git clone https://github.com/TamarNoselidze/WorkoutPlanner.git
    cd WorkoutPlanner
    ```

2. Download Dependencies:
    Ensure you have all required libraries. This project uses Newtonsoft.Json for JSON parsing.

3. Set Up the Database:
    Create a `database.json` file in the root directory of the project. This file will store user data (a sample one is provided, feel free to overwrite).

4. Compile the Code:
    Open the project in Visual Studio and build the solution.


# Execution

In the root directory, run the application:
```
dotnet run
```


## Usage

## Main Menu

- **Register**: Create a new account by entering a username and password.
- **Login**: Log in with an existing account by entering your username and password.
- **Exit**: Exit the application.

## Logged In Menu

- **Access Existing Data**: View your saved exercises and nutritional information.
- **Make a New Request**: Fetch new exercises or nutritional information.
- **Exit**: Log out and return to the main menu.

## Fetch Exercises

1. **General Exercises**: Fetch exercises based on the target muscle.
2. **Tailored Exercises**: Fetch exercises based on specific muscle, equipment, and intensity level.

## Get Nutritional Information

Enter your measurement units, sex, age, height, weight, and activity level to fetch and save nutritional information.


# API Integration
The application integrates with the following APIs:

- [ExerciseDB API](https://rapidapi.com/justin-WFnsXH_t6/api/exercisedb): Fetches general exercises based on the target muscle.
- [WorkoutDB API](https://rapidapi.com/naeimsalib/api/work-out-api1): Fetches tailored exercises based on specific criteria.
- [Nutrition Calculator API](https://rapidapi.com/sprestrelski/api/nutrition-calculator): Fetches detailed nutritional information.
