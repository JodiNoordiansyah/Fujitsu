using FujitsuWebApp.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FujitsuWebApp.Models;

public partial class TbMSupplier
{
    [Spreadsheet(ColumnWidth = 6000, DisplayName = "Supplier Code", Order = 1)]
    [Required]
    public string SupplierCode { get; set; } = null!;
    [Spreadsheet(ColumnWidth = 6000, DisplayName = "Supplier Name", Order = 2)]
    [Required]
    public string? SupplierName { get; set; }
    [Spreadsheet(ColumnWidth = 6000, DisplayName = "Address", Order = 3)]
    [Required]
    public string? Address { get; set; }
    [Spreadsheet(ColumnWidth = 6000, DisplayName = "Province", Order = 4)]
    [Required]
    public string? Province { get; set; }
    [Spreadsheet(ColumnWidth = 6000, DisplayName = "City", Order = 5)]
    [Required]
    public string? City { get; set; }
    [Spreadsheet(ColumnWidth = 6000, DisplayName = "Pic", Order = 6)]
    [Required]
    public string? Pic { get; set; }
}

public class SupplierVm : TbMSupplier
{
    public List<DropDownViewModel>? DropDownViewModel { get; set; }
}

public class SearchParam
{
    public string? SupplierCode { get; set; }
    public string? Province { get; set; }
    public string? City { get; set; }
}
