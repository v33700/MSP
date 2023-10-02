using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;

namespace DraftGotoGro
{
    internal class CSVGEN
    {
        private List<Dictionary<string, object>> data;

        public CSVGEN(string jsonData)
        {
            data = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonData);

        }

        public void ToCsv(string filename)
        {
            if (data == null || !data.Any())
            {
                System.Console.WriteLine("No data provided!");
                return;
            }

            StringBuilder csvContent = new StringBuilder();

            // Extract the header from the keys of the first dictionary
            var header = data[0].Keys;
            csvContent.AppendLine(string.Join(",", header));

            foreach (var record in data)
            {
                var line = string.Join(",", record.Values.Select(v => v.ToString()));
                csvContent.AppendLine(line);
            }

            File.WriteAllText(filename, csvContent.ToString());
        }

    }
}
