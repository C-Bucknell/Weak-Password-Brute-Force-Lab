// =====================================================================
//  Brute Force lab — STUDENT STARTER (the program you modify)
// =====================================================================
//  Before running this, make sure the TargetWebsite project is already
//  running (start it with Ctrl+F5 so it stays up). It serves on
//  http://localhost:5000
//
//  This starter does ONE thing: it shows you how to send a web request
//  from C# and read what comes back. That is the only building block you
//  need. Turning it into a brute-force attack is YOUR task.
// =====================================================================

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BruteForceClient
{
    class Program
    {
        // The target runs on this address (set in TargetWebsite's launch settings).
        static string baseUrl = "http://localhost:5000";

        static async Task Main()
        {
            using HttpClient client = new HttpClient();

            try
            {
                // --- Building block: fetch a page and read the response ---
                HttpResponseMessage response = await client.GetAsync(baseUrl + "/");
                string body = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Connected to: " + baseUrl);
                Console.WriteLine("Status code : " + (int)response.StatusCode);
                Console.WriteLine("Final URL   : " + response.RequestMessage?.RequestUri);
                Console.WriteLine("Body length : " + body.Length + " characters");
                Console.WriteLine();
                Console.WriteLine("First 200 characters of the page:");
                Console.WriteLine(body.Substring(0, Math.Min(200, body.Length)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not reach the target at " + baseUrl);
                Console.WriteLine("Is the TargetWebsite project running? Start it first with Ctrl+F5.");
                Console.WriteLine("Details: " + ex.Message);
            }

            // =============================================================
            //  YOUR TASK
            // =============================================================
            //  /validate takes a username and a password and decides whether
            //  the login worked. The password for user "bob" is known to be
            //  a number between 1 and 99.
            //
            //  Using only the request building block above, work out:
            //    1. how to send a username and a password guess to /validate
            //    2. how to tell from the response whether a guess was right
            //    3. how to keep trying until you find the correct one
            //
            //  Write that logic yourself below, print the password when you
            //  find it, and stop.
            // =============================================================

            Console.WriteLine();
            Console.WriteLine("Press Enter to close...");
            Console.ReadLine();
        }
    }
}
