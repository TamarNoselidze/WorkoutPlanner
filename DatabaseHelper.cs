using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace WorkoutPlanner.Project
{
    /// <summary>
    /// Provides methods to interact with the user database, including loading, saving, registration, and login functionalities.
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        /// The path to the database file that stores user information.
        /// </summary>
        private static readonly string DATABASE_FILE = "database.json";

        /// <summary>
        /// A dictionary storing all registered users with their usernames as keys.
        /// </summary>
        private static Dictionary<string, User> users;

        /// <summary>
        /// Static constructor that initializes the users dictionary by loading data from the database file.
        /// </summary>
        static DatabaseHelper()
        {
            users = LoadUsers();
        }

        /// <summary>
        /// Loads the users from the database file.
        /// </summary>
        /// <returns>A dictionary of users loaded from the database file.</returns>
        private static Dictionary<string, User> LoadUsers()
        {
            try
            {
                using (StreamReader reader = new StreamReader(DATABASE_FILE))
                {
                    string json = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<Dictionary<string, User>>(json) ?? new Dictionary<string, User>();
                }
            }
            catch (Exception)
            {
                return new Dictionary<string, User>();
            }
        }

        /// <summary>
        /// Saves the updated users dictionary to the database file.
        /// </summary>
        /// <param name="users">The dictionary of users to save.</param>
        private static void SaveUsers(Dictionary<string, User> users)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(DATABASE_FILE))
                {
                    string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                    writer.Write(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Updates the information of a given user in the database.
        /// </summary>
        /// <param name="user">The user whose information needs to be updated.</param>
        public static void UpdateUserInfo(User user)
        {
            users[user.Username] = user;
            SaveUsers(users);
        }

        /// <summary>
        /// Handles the user login process.
        /// </summary>
        /// <returns>
        /// The logged-in user if successful; otherwise, returns null after three failed attempts.
        /// </returns>
        public static User LoginUser()
        {
            Console.WriteLine("Login to an existing account");
            Console.Write("Please, enter your username: ");

            int attempts = 0;
            User user;

            while (true)
            {
                string? username = Console.ReadLine()?.Trim();
                if (users.TryGetValue(username, out user))
                {
                    Console.Write("Please, enter your password: ");
                    while (true)
                    {
                        string? password = Console.ReadLine()?.Trim();
                        if (user.Password.Equals(password))
                        {
                            return user;
                        }
                        else
                        {
                            Console.WriteLine("The password is incorrect. Please, try again.");
                        }
                    }
                }
                else
                {
                    attempts++;
                    if (attempts == 3)
                    {
                        Console.WriteLine("You have tried to login with a wrong username 3 times.\nYou will be redirected to the initial menu, so you can try registering instead.\n");
                        return null;
                    }
                    Console.WriteLine("There is no account under this username. Please, try again.");
                }
            }
        }

        /// <summary>
        /// Handles the user registration process.
        /// </summary>
        /// <returns>The newly registered user.</returns>
        public static User RegisterUser()
        {
            Console.WriteLine("\nRegister a new account");
            string username = ObtainUsername();
            string password = ObtainPassword(username);

            User newUser = new User(username, password);
            users[username] = newUser;
            SaveUsers(users);

            return newUser;
        }

        /// <summary>
        /// Prompts the user to enter a valid username.
        /// </summary>
        /// <returns>A valid username entered by the user.</returns>
        private static string ObtainUsername()
        {
            Console.Write("Enter username: ");
            string username;

            while (true)
            {
                username = Console.ReadLine().Trim();
                if (username.Length < 4)
                {
                    Console.Write("The username should have more than 3 characters. Please, enter a different username: ");
                }
                else if (users.ContainsKey(username))
                {
                    Console.Write("This username already exists in the database. Please, enter a different username: ");
                }
                else
                {
                    break;
                }
            }

            return username;
        }

        /// <summary>
        /// Prompts the user to enter a valid password.
        /// </summary>
        /// <param name="username">The username for which the password is being set.</param>
        /// <returns>A valid password entered by the user.</returns>
        private static string ObtainPassword(string username)
        {
            Console.Write("Enter password: ");
            string? password;

            while (true)
            {
                password = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(password)){
                    Console.Write("The password should have at least 6 characters. Please, enter a different password: ");
                }
                else{
                    if (password.Length < 6)
                    {
                        Console.Write("The password should have at least 6 characters. Please, enter a different password: ");
                    }
                    else if (password.Equals(username))
                    {
                        Console.Write("The password cannot be the same as the username. Please, enter a different password: ");
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return password;
        }
    }
}
