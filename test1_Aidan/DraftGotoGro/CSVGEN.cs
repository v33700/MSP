using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using ThirdParty.Json.LitJson;

namespace DraftGotoGro
{
    internal class CSVGEN
    {
        private List<Dictionary<string, object>> data;

        public CSVGEN(string jsonData)
        {
            data = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonData) ?? new List<Dictionary<string, object>>();
        }

        public void ToCsv(string filename)
        {
            if (!data.Any())
            {
                System.Console.WriteLine("No data provided!");
                return;
            }

            StringBuilder csvContent = new StringBuilder();

            // Extract the header from the keys of the first dictionary
            var firstRecord = data.FirstOrDefault(d => d != null);
            if (firstRecord == null)
            {
                System.Console.WriteLine("No valid records provided!");
                return;
            }

            var header = firstRecord.Keys.Where(k => k != null);
            csvContent.AppendLine(string.Join(",", header.Select(Escape)));

            foreach (var record in data)
            {
                if (record == null) continue;
                var line = string.Join(",", record.Values.Where(v => v != null).Select(v => Escape(v.ToString())));
                csvContent.AppendLine(line);
            }

            File.WriteAllText(filename, csvContent.ToString(), Encoding.UTF8);
        }

        private string Escape(string s)
        {
            if (s.Contains(',') || s.Contains('"') || s.Contains('\n'))
            {
                return $"\"{s.Replace("\"", "\"\"")}\"";
            }
            return s;
        }

    }
}
