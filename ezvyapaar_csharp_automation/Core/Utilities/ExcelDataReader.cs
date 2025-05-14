// 6. Excel Data Reader for Data-Driven Testing
namespace ezvyapaar_csharp_automation.core.Utilities
{
    using ezvyapaar_csharp_automation.core.Configuration;
    using ExcelDataReader;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;

    public class ExcelDataReader
    {
        private static readonly ConfigManager _configManager = ConfigManager.Instance;

        static ExcelDataReader()
        {
            // Required for Excel data reader
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static List<Dictionary<string, object>> ReadExcelData(string fileName, string sheetName)
        {
            var result = new List<Dictionary<string, object>>();
            string filePath = Path.Combine(_configManager.TestDataFolder, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Excel file {filePath} not found");
            }

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        });

                        var dataTable = dataSet.Tables[sheetName] ?? dataSet.Tables[0];

                        foreach (DataRow row in dataTable.Rows)
                        {
                            var rowDict = new Dictionary<string, object>();
                            foreach (DataColumn col in dataTable.Columns)
                            {
                                rowDict[col.ColumnName] = row[col];
                            }
                            result.Add(rowDict);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error reading Excel file: {ex.Message}");
                throw;
            }

            return result;
        }
    }
}
