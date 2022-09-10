using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnetstore.WebAPI.Nuget.Core.Models;

public abstract class Company : BaseInfoModel
{
    [Column(TypeName = "nvarchar(50)")]
    [StringLength(50, MinimumLength = 1)]
    [MinLength(1)]
    [MaxLength(50)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Text)]
    public string? CorporateID { get; set; }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            CorporateID = null;
        }

        base.DisposeManaged();
    }
}