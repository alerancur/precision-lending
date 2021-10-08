using System.Collections.Generic;

namespace Question3
{
    public class SearchResult
    {
        public IEnumerable<string> FullLinesWithSearchParam { get; set; }
        public int NumberOfOccurences { get; set; }
    }
}