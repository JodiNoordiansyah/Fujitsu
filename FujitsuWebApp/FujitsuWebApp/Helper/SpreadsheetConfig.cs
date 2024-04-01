using System.Linq;
using System.Reflection;

namespace FujitsuWebApp.Helper
{
    public class SpreadsheetConfig
    {
        public string Location { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string SheetName { get; set; } = string.Empty;

        public int StartRow { get; set; } = 0;
        public int StartColumn { get; set; } = 0;

        public string Extension
        {
            get
            {
                var extensionSplit = FileName.Split('.');
                if (extensionSplit.Length > 0)
                    return extensionSplit[^1].ToLower();
                return string.Empty;
            }
        }

        public string ContentType
        {
            get
            {
                return Extension switch
                {
                    "xls" => "application/vnd.ms-excel",
                    "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    _ => string.Empty,
                };
            }
        }

        public static SpreadsheetAttribute GetAttribute(PropertyInfo property)
        {
            var propAttr = property.GetCustomAttributes(false).OfType<SpreadsheetAttribute>();
            if (propAttr != null)
            {
                return propAttr.FirstOrDefault();
            }

            return null;
        }
    }
}
