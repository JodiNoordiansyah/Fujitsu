using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FujitsuWebApp.Models;
[Table("TB_M_CITY")]
public partial class TbMCity
{
    [Key]
    public long Id { get; set; }
    public string? Province { get; set; }
    public string? City { get; set; }
}
