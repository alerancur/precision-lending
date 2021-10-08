using System.Collections.Generic;

namespace Question2
{
    public class RateProviderB : IRateProvider
    {
        public string Name { get => "RateProviderB"; }

        public IEnumerable<Rate> GetRates() => new List<Rate>
            {
                new()
                {
                    ProviderName = Name,
                    Value = 0.2541M,
                    Term = 1
                }
            };
    }
}
