using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnetstore.WebAPI.Nuget.Core.Models;

public abstract class BaseInfoModel : BaseModel
{
    [Required]
    [ScaffoldColumn(true)]
    [Column(TypeName = "nvarchar(100)")]
    [StringLength(100, MinimumLength = 3)]
    [MinLength(3)]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string? Name { get; set; }

    [Required]
    [ScaffoldColumn(true)]
    [Column(TypeName = "nvarchar(2000)")]
    [StringLength(2000, MinimumLength = 3)]
    [MinLength(3)]
    [MaxLength(2000)]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }


    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            Name = null;
            Description = null;
        }

        base.DisposeManaged();
    }
}