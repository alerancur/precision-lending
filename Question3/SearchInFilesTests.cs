using System;
using System.Threading.Tasks;
using Xunit;

namespace Question3
{
    public class SearchInFilesTests
    {    
        [Fact]
        public async Task Test1()
        {
            var fs = new FileService();
            var result = await fs.SearchInFilesAsync(@"./Resources", "search", @"./Resources/Output/output.txt");
            Assert.Equal(84, result.FilesProcessed);
            Assert.Equal(168, result.LinesWithSearchParam);
            Assert.Equal(196, result.SearchParamOccurrences);
        }
    }
}
