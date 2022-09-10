using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnetstore.WebAPI.Nuget.Core.Models;

public abstract class Person : BaseModel
{
    [Column(TypeName = "nvarchar(50)")]
    [StringLength(50, MinimumLength = 1)]
    [MinLength(1)]
    [MaxLength(50)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Text)]
    public string? EnglishName { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    [StringLength(50, MinimumLength = 1)]
    [MinLength(1)]
    [MaxLength(50)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Text)]
    public string? FirstName { get; set; }

    [Required]
    [ScaffoldColumn(true)]
    public bool? IsMale { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    [StringLength(50, MinimumLength = 1)]
    [MinLength(1)]
    [MaxLength(50)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Text)]
    public string? LastName { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    [StringLength(50, MinimumLength = 1)]
    [MinLength(1)]
    [MaxLength(50)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Text)]
    public string? MiddleName { get; set; }

    [Column(TypeName = "varchar(30)")]
    [StringLength(30, MinimumLength = 10)]
    [MinLength(10)]
    [MaxLength(30)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Text)]
    public string? SocialSecurityNumber { get; set; }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            LastName = null;
            FirstName = null;
            MiddleName = null;
            EnglishName = null;
            SocialSecurityNumber = null;
            IsMale = null;
        }

        base.DisposeManaged();
    }
}