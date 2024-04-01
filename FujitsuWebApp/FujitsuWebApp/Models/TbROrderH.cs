using System;
using System.Collections.Generic;

namespace FujitsuWebApp.Models;

public partial class TbROrderH
{
    public string OrderNo { get; set; } = null!;

    public DateOnly? OrderDate { get; set; }

    public string? SupplierCode { get; set; }

    public decimal? Amount { get; set; }
}
