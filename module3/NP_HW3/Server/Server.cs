 
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server
{

    class Program
    {
        

        private const int PORT = 8080;
        

        private static Dictionary<string, string[]> recipes = new Dictionary<string, string[]>
        {
            { "pizza", new string[] { "flour", "tomato sauce", "cheese", "toppings" } },
            { "pasta", new string[] { "pasta", "tomato sauce", "olive oil", "garlic" } },
            { "stir fry", new string[] { "vegetables", "protein", "soy sauce", "oil" } }
        };




        static void Main(string[] args)
        {
            
            UdpClient server = new UdpClient(PORT);
            Console.WriteLine("Server started. Waiting for client requests...");

            while (true)
            {
                var clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                var requestBytes = server.Receive(ref clientEndPoint);
                var request = System.Text.Encoding.UTF8.GetString(requestBytes);
                Console.WriteLine($"Received request from {clientEndPoint.Address}:{clientEndPoint.Port}: {request}");

                var response = GetRecipes(request.Split(','));
                var responseBytes = System.Text.Encoding.UTF8.GetBytes(response);
                server.Send(responseBytes, responseBytes.Length, clientEndPoint);
            }
            


        
        }

         

            private static string GetRecipes(string[] ingredients)
            {
                var matchedRecipes = new List<string>();
                foreach (var recipe in recipes)
                {
                    var match = true;
                    foreach (var ingredient in ingredients)
                    {
                        if (!recipe.Value.Contains(ingredient))
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        matchedRecipes.Add(recipe.Key);
                    }
                }
                return string.Join(", ", matchedRecipes);
            }

             
    }
}
