// =====================================================================
//  Brute Force lab — STUDENT STARTER (the program you modify)
// =====================================================================
//  Before running this, make sure the TargetWebsite project is already
//  running (start it with Ctrl+F5 so it stays up). It serves on
//  http://localhost:5000
//
//  This starter gives you ONE building block: a method that sends a
//  single username + password attempt to the site and prints what comes
//  back. Turning that into a brute-force attack is YOUR task.
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
            Console.WriteLine("Brute force lab - student starter");
            Console.WriteLine("Target: " + baseUrl);
            Console.WriteLine();

            // --- Building block ------------------------------------------------
            // Send ONE login attempt so you can see how it works. Start with an
            // obviously-wrong guess and look carefully at what comes back - that
            // tells you what a FAILED attempt looks like. (The real password is a
            // number, so "wrong" is guaranteed to fail.)
            await SendLoginAttempt("bob", "wrong");

            // === YOUR TASK =====================================================
            //  The password for user "bob" is a whole number between 1 and 99.
            //  Using SendLoginAttempt as your building block, work out:
            //    1. how to call it with each possible password in turn
            //    2. how to tell, in code, whether an attempt succeeded
            //       (hint: compare what comes back with what you saw above)
            //    3. how to stop and report the password once you find it
            //
            //  Write your code below.
            // ===================================================================

            Console.WriteLine("\nPress Enter to close...");
            Console.ReadLine();
        }

        // Sends a single username/password attempt to the site and prints the
        // response. This is the building block for your task.
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
                Console.WriteLine("Could not reach the target at " + baseUrl);
                Console.WriteLine("Is the TargetWebsite project running? Start it with Ctrl+F5.");
                Console.WriteLine("Details: " + ex.Message);
            }
        }
    }
}
