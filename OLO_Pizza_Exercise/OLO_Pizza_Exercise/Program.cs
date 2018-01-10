using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OLO_Pizza_Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read in JSON string
            var json = new System.Net.WebClient().DownloadString("http://files.olo.com/pizzas.json");

            // Create a list of pizzas from JSON string. 
            //      - Convert JSON string to lower. 
            //      - Toppings property is a SortedSet so that we can use the LINQ GroupBy.
            List<pizza> pizzas = JsonConvert.DeserializeObject<List<pizza>>(json.ToLower());

            // Use LINQ to group, order, and select the top 20 pizzas
            var results = pizzas.GroupBy(x => string.Join("", x.toppings))
                                    .OrderByDescending(x => x.Count())
                                    .Take(20);

            // Print results
            int ctr = 1;
            foreach (var p in results)
            {
                Console.WriteLine(string.Format("Rank=> {0}  Orders=> {1}  Toppings=> {2}", ctr.ToString().PadLeft(2, ' '), p.Count().ToString().PadLeft(5, ' '), string.Join(", ", p.First().toppings)));
                ctr++;
            }

            Console.ReadKey();
        }
    }
    public class pizza
    {
        public SortedSet<string> toppings { get; set; }
    }
}

