using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAS_Query_APP.Models;

public class CAS
{
    [Key]
    public int ID { get; set; } // primary key

    [Required]
    [StringLength(12)]
    public string CASRN { get; set; } = null!;

    [Required]
    public string casregno { get; set; }

    [Required]
    [StringLength(500)]
    public string ChemName { get; set; }

    [StringLength(16)]
    public string FLAG { get; set; }

    [Required]
    [StringLength(8)]
    public string ACTIVITY { get; set; }
}