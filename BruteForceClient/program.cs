// =====================================================================
//  Brute Force lab — STUDENT STARTER (the program you modify)
// =====================================================================
//  Before running this, make sure the TargetWebsite project is already
//  running (start it with Ctrl+F5 so it stays up). It serves on
//  http://localhost:5000
//
//  When you run this you get a menu with three options:
//    1. Show the web page  - the program acts like a web browser and
//                            simply asks the site for a page.
//    2. Do a login         - you type a username and password and the
//                            program sends that one attempt to the site.
//    3. Run my own code    - an empty method (StudentTask) where YOU write
//                            the code for the task.
// =====================================================================

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BruteForceClient
{
    class Program
    {
        // The target runs on this address (set in TargetWebsite's launch settings).
        static string baseUrl = "http://localhost:5000";

        // Re-use one HttpClient for every request (the recommended way in .NET).
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("================================================");
                Console.WriteLine(" Brute Force Lab - Student Starter");
                Console.WriteLine(" Target: " + baseUrl);
                Console.WriteLine("================================================");
                Console.WriteLine(" 1. Show the web page (act like a web browser)");
                Console.WriteLine(" 2. Do a login (you type the username/password)");
                Console.WriteLine(" 3. Run my own code (your task)");
                Console.WriteLine(" 0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        await ShowWebPage();
                        break;
                    case "2":
                        await DoLogin();
                        break;
                    case "3":
                        await StudentTask();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Please type 1, 2, 3 or 0.");
                        break;
                }
            }

            Console.WriteLine("\nGoodbye. Press Enter to close...");
            Console.ReadLine();
        }

        // --- Option 1 ------------------------------------------------------
        // Acts like a web browser: asks the site for a page and shows what it
        // sends back. No username or password is involved here.
        static async Task ShowWebPage()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + "/");
                string body = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Status code : " + (int)response.StatusCode);
                Console.WriteLine("Page address: " + response.RequestMessage?.RequestUri);
                Console.WriteLine("---- page returned ----");
                Console.WriteLine(body);
                Console.WriteLine("-----------------------");
            }
            catch (Exception ex)
            {
                ShowConnectionError(ex);
            }
        }

        // --- Option 2 ------------------------------------------------------
        // Asks you to type a username and password, then sends that single
        // attempt to the site and shows the response.
        static async Task DoLogin()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.WriteLine();

            await SendLoginAttempt(username, password);
        }

        // --- Option 3 ------------------------------------------------------
        static async Task StudentTask()
        {
            // === YOUR TASK =================================================
            //  The password for user "bob" is a whole number between 1 and 99.
            //  Using SendLoginAttempt(...) as your building block, write code
            //  that:
            //    1. tries each possible password in turn
            //    2. works out, in code, when an attempt has succeeded
            //       (hint: compare the response with what you saw in option 2)
            //    3. stops and reports the password once it is found
            //
            //  Write your code below this comment.
            // ===============================================================

            Console.WriteLine("This option is empty - add your own code in the StudentTask() method.");
            await Task.CompletedTask; // you can remove this once your code uses await
        }

        // --- Shared helper -------------------------------------------------
        // Sends a single username/password attempt to the site and prints the
        // response. Used by option 2, and the building block for option 3.
        static async Task SendLoginAttempt(string username, string password)
        {
            try
            {
                var formData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                HttpResponseMessage response =
                    await client.PostAsync($"{baseUrl}/validate", formData);

                string body = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Sent       : username={username}, password={password}");
                Console.WriteLine($"Status code: {(int)response.StatusCode}");
                Console.WriteLine($"Ended up at: {response.RequestMessage?.RequestUri}");
                Console.WriteLine("---- page returned ----");
                Console.WriteLine(body);
                Console.WriteLine("-----------------------");
            }
            catch (Exception ex)
            {
                ShowConnectionError(ex);
            }
        }

        static void ShowConnectionError(Exception ex)
        {
            Console.WriteLine("Could not reach the target at " + baseUrl);
            Console.WriteLine("Is the TargetWebsite project running? Start it with Ctrl+F5.");
            Console.WriteLine("Details: " + ex.Message);
        }
    }
}
