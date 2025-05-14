using System.Collections.Generic;
using System.IO;

namespace ezvyapaar_csharp_automation.core.Utilities
{
    public static class TestDataReader
    {
        public static IEnumerable<string[]> ReadCsv(string filePath)
        {
            foreach (var line in File.ReadAllLines(filePath))
            {
                yield return line.Split(',');
            }
        }
    }
}
