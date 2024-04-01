using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FujitsuWebApp.Helper
{
    public static class SpreadsheetProcessor<T> where T : class, new()
    {
        public static IEnumerable<T> Read(byte[] data, string extension, bool withAttribute = false, int sheetIndex = 0, bool firstRowAsHeader = true)
        {
            var workbook = GetWorkbook(data, extension);

            var sheet = workbook.GetSheetAt(sheetIndex);
            int startIndex = firstRowAsHeader ? 1 : 0;

            return ProcessData(sheet, startIndex, withAttribute);
        }

        private static IWorkbook GetWorkbook(byte[] data, string extension)
        {
            using MemoryStream ms = new MemoryStream(data);
            return extension switch
            {
                "xlsx" => new XSSFWorkbook(ms),
                "xls" => new HSSFWorkbook(ms),
                _ => throw new ArgumentException("extension must between", "extension")
            };
        }

        private static List<T> ProcessData(ISheet sheet, int startIndex, bool withAttribute)
        {
            List<T> results = new List<T>();
            DataFormatter formatter = new DataFormatter(System.Globalization.CultureInfo.InvariantCulture);
            for (int rowIndex = startIndex; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                var sheetRow = sheet.GetRow(rowIndex);
                if (sheetRow != null)
                {
                    if (sheetRow.Cells.All(d => d.CellType == CellType.Blank)) continue;

                    int cellIndex = 0;
                    var rowData = new T();
                    try
                    {
                        foreach (var property in typeof(T).GetProperties())
                        {
                            if (!withAttribute || withAttribute && SpreadsheetConfig.GetAttribute(property) != null)
                            {
                                var typeCode = Type.GetTypeCode(property.PropertyType);
                                var cellValue = formatter.FormatCellValue(sheetRow.GetCell(cellIndex++));

                                cellValue = CleanAscii(cellValue);

                                switch (typeCode)
                                {
                                    case TypeCode.String:
                                        property.SetValue(rowData, cellValue);
                                        break;
                                    case TypeCode.Boolean:
                                        bool boolResult;
                                        if (bool.TryParse(cellValue, out boolResult))
                                        {
                                            property.SetValue(rowData, boolResult);
                                        }
                                        break;
                                    case TypeCode.DateTime:
                                        DateTime dateTimeResult;
                                        if (DateTime.TryParse(cellValue, out dateTimeResult))
                                        {
                                            property.SetValue(rowData, dateTimeResult);
                                        }
                                        break;
                                    case TypeCode.Int32:
                                        int intResult;
                                        if (int.TryParse(cellValue, out intResult))
                                        {
                                            property.SetValue(rowData, intResult);
                                        }
                                        break;
                                    case TypeCode.Decimal:
                                        decimal decimalResult;
                                        if (decimal.TryParse(cellValue, out decimalResult))
                                        {
                                            property.SetValue(rowData, decimalResult);
                                        }
                                        break;
                                    case TypeCode.Double:
                                        double doubleResult;
                                        if (double.TryParse(cellValue, out doubleResult))
                                        {
                                            property.SetValue(rowData, doubleResult);
                                        }
                                        break;
                                }
                            }
                        }
                        results.Add(rowData);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"{ex.Message}. Cell Index {cellIndex}, Row Index {rowIndex}");
                    }
                }
            }
            return results;
        }

        public static string CleanAscii(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);

            foreach (char c in s)
            {
                if (c > 127) // you probably don't want 127 either
                    continue;
                if (c < 32)  // I bet you don't want control characters 
                    continue;
                if (c == '%')
                    continue;
                if (c == '?')
                    continue;
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
