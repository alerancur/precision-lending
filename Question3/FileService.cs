using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Question3
{
    public class FileService
    {
        public async Task<SearchResultSummary> SearchInFilesAsync(string folderPath, string search, string destinationFilename)
        {
            var tasks = Directory.EnumerateFiles(folderPath).Select(file => SearchInFile(file, search)).ToList();
            await Task.WhenAll(tasks);

            File.WriteAllLines(destinationFilename, tasks.SelectMany (t => t.Result.FullLinesWithSearchParam));

            return new SearchResultSummary
            {
                FilesProcessed = tasks.Count(t=>t.IsCompletedSuccessfully),
                LinesWithSearchParam = tasks.Sum(t=>t.Result.FullLinesWithSearchParam.Count()),
                SearchParamOccurrences = tasks.Sum(t => t.Result.NumberOfOccurences)
            };
        }

        private async Task<SearchResult> SearchInFile(string file, string search)
        {
            var lines = await File.ReadAllLinesAsync(file);
            return new SearchResult
            {
                FullLinesWithSearchParam = lines.Where(line => line.Contains(search)),
                NumberOfOccurences = lines.Sum(line => Occurrences(line, search))
            };
        }

        private int Occurrences(string searchTarget, string searchParam)
        {
            int result = 0, index = 0;

            while ((index=searchTarget.IndexOf(searchParam, index))>-1)
            {
                result++;
                index += searchParam.Length;
            }

            return result;
        }
    }
}
