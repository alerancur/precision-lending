using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace Question2
{
    class Program
    {
        static void Main(string[] args)
        {
            var rateProviderImplementations = (from t in Assembly.GetAssembly(typeof(IRateProvider)).GetTypes()
                        where t.IsClass && typeof(IRateProvider).IsAssignableFrom(t)
                        select t)
                        .ToList();

            Console.WriteLine("Please select one of the following rate providers to obtain rates:");
            Console.WriteLine(string.Join(" | ", rateProviderImplementations.Select((t,i)=>$"{t.Name} [{i}]")));
            var key = Console.ReadKey(true);
            if (!int.TryParse(key.KeyChar.ToString(), out var selectedProvider) || selectedProvider >= rateProviderImplementations.Count)
                Console.WriteLine("Invalid Option selected, please try again");
            else
            {
                var implementation = (IRateProvider)Activator.CreateInstance(rateProviderImplementations[selectedProvider]);
                Console.WriteLine(JsonConvert.SerializeObject(implementation.GetRates(), Formatting.Indented));
            }
        }
    }
}
