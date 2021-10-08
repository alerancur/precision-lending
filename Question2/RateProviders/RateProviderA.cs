using System.Collections.Generic;

namespace Question2
{
    public class RateProviderA : IRateProvider
    {
        public string Name { get => "RateProviderA"; }

        public IEnumerable<Rate> GetRates() => 
            new List<Rate>
            {
                new()
                {
                    ProviderName = Name,
                    Value = 0.1141M,
                    Term = 12
                }
            };
    }
}
