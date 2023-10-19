using DraftGotoGro;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

internal class CSVGEN
{
    private List<CombinedData> combinedData;
    private List<Sale> salesData;

    private class CombinedData
    {
        public Member Member { get; set; }
        public List<Sale> Sales { get; set; }
    }

    // Constructor for combined data
    public CSVGEN(string jsonData, bool isCombined)
    {
        if (isCombined)
        {
            combinedData = JsonSerializer.Deserialize<List<CombinedData>>(jsonData) ?? new List<CombinedData>();
        }
        else
        {
            salesData = JsonSerializer.Deserialize<List<Sale>>(jsonData) ?? new List<Sale>();
        }
    }

    public void ToCsv(string filename)
    {
        StringBuilder csvContent = new StringBuilder();

        if (combinedData != null && combinedData.Any()) // Handling combined data
        {
            var memberHeaders = new List<string> { "_id", "Name", "PhoneNumber", "Address" };
            var saleHeader = "Sales";
            csvContent.AppendLine(string.Join(",", memberHeaders) + "," + saleHeader);

            foreach (var record in combinedData)
            {
                var member = record.Member;
                var memberValues = memberHeaders.Select(h => member.GetType().GetProperty(h)?.GetValue(member)?.ToString() ?? "").ToList();
                var salesValue = JsonSerializer.Serialize(record.Sales);
                csvContent.AppendLine(string.Join(",", memberValues) + "," + salesValue);
            }
        }
        else if (salesData != null && salesData.Any()) // Handling only sales data
        {
            var saleHeaders = new List<string> { "MemberID", "OrderNumber", "Items", "SaleDate" };
            csvContent.AppendLine(string.Join(",", saleHeaders));

            foreach (var sale in salesData)
            {
                var values = saleHeaders.Select(h =>
                {
                    if (h == "Items")
                    {
                        return JsonSerializer.Serialize(sale.Items);
                    }
                    return sale.GetType().GetProperty(h)?.GetValue(sale)?.ToString() ?? "";
                }).ToList();
                csvContent.AppendLine(string.Join(",", values));
            }
        }
        else
        {
            System.Console.WriteLine("No data provided!");
            return;
        }

        File.WriteAllText(filename, csvContent.ToString(), Encoding.UTF8);
    }
}
