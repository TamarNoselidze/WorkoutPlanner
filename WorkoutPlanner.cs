using System;
using System.Collections.Generic;

namespace WorkoutPlanner.Project
{
    /// <summary>
    /// The main class for the Workout Planner application.
    /// Handles user registration, login, and requests for exercises and nutritional information.
    /// </summary>
    class WorkoutPlanner
    {
        /// <summary>
        /// The main method to start the Workout Planner application.
        /// </summary>
        /// <param name="args">Command-line arguments</param>
        static void Main(string[] args)
        {
            var planner = new WorkoutPlanner();
            planner.Run();
        }


        /// <summary>
        /// Runs the Workout Planner application, displaying the main menu and handling user input.
        /// </summary>
        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                DisplayMainMenu();
                string? option = Console.ReadLine()?.Trim();
                switch (option)
                {
                    case "1":
                        HandleRegistration();
                        break;
                    case "2":
                        HandleLogin();
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Thank you for using Workout Planner.");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the main menu options to the user.
        /// </summary>
        private void DisplayMainMenu()
        {
            Console.WriteLine("Welcome to Workout Planner!");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("\nChoose an option: ");
        }

        /// <summary>
        /// Handles the registration option, prompting the user to register and then handle new requests.
        /// </summary>
        private void HandleRegistration()
        {
            User currentUser = DatabaseHelper.RegisterUser();
            HandleNewRequest(currentUser);
        }

        /// <summary>
        /// Handles the login option, prompting the user to log in and then displaying the logged-in menu.
        /// </summary>
        private void HandleLogin()
        {
            User loggedInUser = DatabaseHelper.LoginUser();
            if (loggedInUser != null)
            {
                ShowLoggedInMenu(loggedInUser);
            }
        }


        /// <summary>
        /// Displays the logged-in user menu and handles user options.
        /// </summary>
        /// <param name="user">The logged-in user.</param>
        private static void ShowLoggedInMenu(User user)
        {
            bool exit = false;
            Console.WriteLine($"\nWelcome back, {user.Username}!");

            while (!exit)
            {
                Console.WriteLine("How can I help you today?");
                Console.WriteLine("1. Access your existing, personalized data");
                Console.WriteLine("2. Make a new request");
                Console.WriteLine("3. Exit");

                string? option = Console.ReadLine()?.Trim();
                switch (option)
                {
                    case "1": // Fetch and display the user's stored data
                        user.DisplayUserData();
                        Console.WriteLine("\nIf you want to see details about a specific exercise, please, respond with the corresponding number.");
                        Console.WriteLine("If not, please, respond with '0'");
                        while (true)
                        {
                            string? response = Console.ReadLine()?.Trim();
                            if (int.TryParse(response, out int exNumber))
                            {
                                if (exNumber > 0 && exNumber <= user.NumberOfExercises())
                                {
                                    Exercise currExercise = user.GetExercise(exNumber - 1);
                                    if (string.IsNullOrEmpty(currExercise.Name))
                                    {
                                        currExercise.DisplayWorkoutInfo();
                                    }
                                    else
                                    {
                                        currExercise.DisplayExerciseInfoGeneral();
                                    }
                                    Console.WriteLine();
                                    break;
                                }
                                else if (exNumber == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Not a valid response, please try again.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Not a valid response, please try again.");
                            }
                        }

                        if (user.HasNutritionalInfo())
                        {
                            Console.WriteLine("You also have a detailed nutritional information saved. Would you like to display it? ");
                            Console.WriteLine(" 1. Yes \n 2. No");
                            string? response = Console.ReadLine()?.Trim();
                            while (response != "1" && response != "2")
                            {
                                Console.WriteLine("\nInvalid response. Please choose 1 or 2.");
                                response = Console.ReadLine()?.Trim();
                            }
                            if (response == "1")
                            {
                                NutritionalInfo info = user.GetNutritionalInfo();
                                info.DisplayNutritionalInfo();
                            }
                        }

                        Console.WriteLine("\nOkay, now redirecting back to the main menu.");
                        break;
                    case "2":  // Make a new request
                        HandleNewRequest(user);
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please, try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Handles new requests from the user.
        /// </summary>
        /// <param name="currentUser">The current logged-in user.</param>
        private static void HandleNewRequest(User currentUser)
        {
            bool done = false;
            Console.WriteLine("\nWhat type of request would you like to make?");

            while (!done)
            {
                Console.WriteLine("1. Fetch exercises");
                Console.WriteLine("2. Create workout routine");
                Console.WriteLine("3. Get nutritional information");
                Console.WriteLine("4. Return");

                string? requestType = Console.ReadLine()?.Trim();
                switch (requestType)
                {
                    case "1":
                        string exerciseType = "1";
                        HandleFetching(exerciseType, currentUser);
                        break;
                    case "2":
                        exerciseType = "2";
                        HandleFetching(exerciseType, currentUser);
                        break;
                    case "3":
                        GetNutritionalInfo(currentUser);
                        break;
                    case "4":
                        Console.WriteLine();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please, try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Handles fetching exercises or workout routines based on user input.
        /// </summary>
        /// <param name="exerciseType">The type of exercise to fetch.</param>
        /// <param name="currentUser">The current logged-in user.</param>
        private static void HandleFetching(string exerciseType, User currentUser)
        {
            if (exerciseType == "1")
            {
                Console.WriteLine("\nWhat body part would you like to fetch exercises for?");
                string? targetMuscle = Console.ReadLine()?.Trim().ToLower();
                string mappedBodyPart = Exercise.GetMappedBodyPart(targetMuscle);
                while (true)
                {
                    if (mappedBodyPart == null)
                    {
                        Console.WriteLine("We don't have exercises for this body part. Would you maybe like to try 'upper arms' or 'lower legs'?\nPlease, enter another body part name: ");
                        targetMuscle = Console.ReadLine()?.Trim().ToLower();
                        mappedBodyPart = Exercise.GetMappedBodyPart(targetMuscle);
                    }
                    else
                    {
                        List<Exercise> exercises = ExerciseDB.FetchExercises(targetMuscle, mappedBodyPart);
                        if (exercises.Count > 0)
                        {
                            DisplayFoundExercises(exercises, currentUser, false);
                            break;
                        }
                    }
                }
            }
            else
            {
                // type == 2
                Console.WriteLine("\nWhat specific muscle would you like to fetch exercises for?");
                string? targetMuscle = Console.ReadLine()?.Trim().ToLower();
                while (!Exercise.IsTargetValid(targetMuscle))
                {
                    Console.WriteLine($"\nCould not find information for the muscle '{targetMuscle}'.");
                    Exercise.DisplayMuscleList();
                    targetMuscle = Console.ReadLine()?.Trim().ToLower();
                }
                Console.WriteLine($"\nNoted the target muscle: {targetMuscle}");

                Console.WriteLine("\nWould you like to specify equipment? (Press enter to skip)");
                string? equipment = Console.ReadLine();
                if (!string.IsNullOrEmpty(equipment))
                {
                    while (!Exercise.IsEquipmentValid(equipment))
                    {
                        Console.WriteLine($"\nCould not find the equipment '{equipment}'.");
                        Exercise.DisplayEquipmentList();
                        equipment = Console.ReadLine()?.Trim().ToLower();
                    }
                    Console.WriteLine("\nNoted.");
                }

                Console.WriteLine("\nWould you like to specify the intensity level? \nYou can choose from the options below or press enter to skip.");
                Console.WriteLine("\n  1. Beginner\n  2. Intermediate\n  3. Expert");
                string? intensityOption = Console.ReadLine();
                string intensityLevel = "";
                if (!string.IsNullOrEmpty(intensityOption))
                {
                    while (intensityOption != "1" && intensityOption != "2" && intensityOption != "3")
                    {
                        Console.WriteLine("\nInvalid option: the value should be an integer corresponding to one of the options. Please, try again.");
                        intensityOption = Console.ReadLine();
                    }
                    intensityLevel = intensityOption switch
                    {
                        "1" => "beginner",
                        "2" => "intermediate",
                        "3" => "expert",
                        _ => intensityLevel
                    };
                }

                List<Exercise> exercises = WorkoutDB.FetchWorkouts(targetMuscle, equipment, intensityLevel);
                if (exercises.Count > 0)
                {
                    DisplayFoundExercises(exercises, currentUser, true);
                }
                else
                {
                    Console.WriteLine("Could not find any exercise with these specifications.\n");
                }
            }
        }

        /// <summary>
        /// Displays the list of found exercises and handles user interaction for saving exercises.
        /// </summary>
        /// <param name="exercises">The list of exercises to display.</param>
        /// <param name="currentUser">The current logged-in user.</param>
        /// <param name="workout">Indicates if the exercises are workout routines.</param>
        static void DisplayFoundExercises(List<Exercise> exercises, User currentUser, bool workout)
        {
            Console.WriteLine($"{exercises.Count} exercises found:");
            for (int i = 0; i < exercises.Count; i++)
            {
                Exercise ex = exercises[i];
                Console.WriteLine($"{i + 1}. {(workout ? ex.Workout : ex.Name)}");
            }

            Console.WriteLine("\nWould you like to get further information about any of these exercises?");
            Console.WriteLine(" - If yes, please choose a corresponding number, otherwise reply with '0'.");

            while (true)
            {
                string? response = Console.ReadLine()?.Trim();

                if (int.TryParse(response, out int number))
                {
                    if (number == 0)
                    {
                        Console.WriteLine("Okay, going back to the main menu.");
                        break;
                    }
                    else if (number > 0 && number <= exercises.Count)
                    {
                        Exercise currExercise = exercises[number - 1];
                        if (workout)
                        {
                            currExercise.DisplayWorkoutInfo();
                        }
                        else
                        {
                            currExercise.DisplayExerciseInfoGeneral();
                        }
                        Console.WriteLine();

                        SaveExerciseMenu(currentUser, currExercise, workout);
                        Console.WriteLine("\nOkay, going back to the main menu.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid option. Please, choose an integer in the correct range.");
                    }
                }
            }
        }

        /// <summary>
        /// Handles details about a specific exercise, including the option to save it to user data.
        /// </summary>
        /// <param name="user">The current logged-in user.</param>
        static void SaveExerciseMenu(User currentUser, Exercise currExercise, bool workout)
        {
            Console.WriteLine("Would you like to save this exercise?");
            Console.WriteLine(" 1. Yes");
            Console.WriteLine(" 2. No");
            string? response = Console.ReadLine()?.Trim();
            while (response != "1" && response != "2")
            {
                Console.WriteLine("\nInvalid number. Please, choose from the two options: either '1' or '2'. ");
                response = Console.ReadLine()?.Trim();
            }
            if (response == "1")
            {
                currentUser.SaveExercise(currExercise, workout);
                DatabaseHelper.UpdateUserInfo(currentUser); // Save the updated user data
            }
        }

        /// <summary>
        /// Fetches nutritional information for the user.
        /// </summary>
        /// <param name="user">The current logged-in user.</param>
        private static void GetNutritionalInfo(User currUser)
        {
            Console.WriteLine("Enter measurement units: 'std' for standard, 'met' for metric: ");
            string? measurementUnits = Console.ReadLine()?.Trim();
            while (measurementUnits != "std" && measurementUnits != "met")
            {
                Console.WriteLine("\nInvalid option, please try again.");
                measurementUnits = Console.ReadLine()?.Trim();
            }

            Console.Write("Enter sex (male/female): ");
            string? sex = Console.ReadLine()?.Trim();
            while (sex != "male" && sex != "female")
            {
                Console.WriteLine("\nInvalid option, please try again.");
                sex = Console.ReadLine()?.Trim();
            }

            Console.Write("Enter age: ");
            int ageValue;
            while (!int.TryParse(Console.ReadLine(), out ageValue))
            {
                Console.WriteLine("Age should be an integer. Try again.");
            }

            int feet = 0, inches = 0, lbs = 0, cm = 0, kilos = 0;

            if (measurementUnits == "std")
            {
                while (true)
                {
                    try
                    {
                        Console.Write("Enter height in feet: ");
                        feet = int.Parse(Console.ReadLine());

                        Console.Write("Enter height in inches: ");
                        inches = int.Parse(Console.ReadLine());

                        Console.Write("Enter weight in lbs: ");
                        lbs = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Something went wrong. Measurement units should have integer values. Please, try again.");
                    }
                }
            }
            else
            {
                // "met"
                while (true)
                {
                    try
                    {
                        Console.Write("Enter height in cm: ");
                        cm = int.Parse(Console.ReadLine());
                        Console.Write("Enter weight in kilos: ");
                        kilos = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Something went wrong. Measurement units should have integer values. Please, try again.");
                    }
                }
            }

            Console.WriteLine("Choose activity level from the options below: ");
            Console.WriteLine("  1. Inactive \n  2. Low Active \n  3. Active \n  4. Very Active");
            string? activityOption = Console.ReadLine()?.Trim();
            while (activityOption != "1" && activityOption != "2" && activityOption != "3" && activityOption != "4")
            {
                Console.WriteLine("\nInvalid option. Try again.");
                activityOption = Console.ReadLine()?.Trim();
            }
            string activityLevel = activityOption switch
            {
                "1" => "Inactive",
                "2" => "Low%20Active",
                "3" => "Active",
                "4" => "Very%20Active",
                _ => ""
            };

            NutritionalInfo nutritionalInfo = NutritionalDB.FetchNutritionalInfo(measurementUnits, sex, ageValue, feet, inches, lbs, cm, kilos, activityLevel);
            if (nutritionalInfo != null)
            {
                nutritionalInfo.DisplayNutritionalInfo();
                Console.WriteLine("\nWould you like to save this information to your account? (yes/no)");
                string? response = Console.ReadLine()?.Trim().ToLower();
                while (response != "yes" && response != "no")
                {
                    Console.WriteLine("\nInvalid option. Please choose 'yes' or 'no'");
                    response = Console.ReadLine()?.Trim().ToLower();
                }
                if (response == "yes")
                {
                    currUser.SaveNutritionalInfo(nutritionalInfo);
                    DatabaseHelper.UpdateUserInfo(currUser); // Save the updated user data
                }
            }
            else
            {
                Console.WriteLine("Failed to fetch nutritional info.");
            }
        }
    }
}
