using System;
using System.Collections.Generic;
using System.Text;

namespace FujitsuWebApp.Helper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SpreadsheetAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public int Order { get; set; }
        public int ColumnWidth { get; set; } = 5000;
        public string MoneyCurrency { get; set; } = "";
    }
}
