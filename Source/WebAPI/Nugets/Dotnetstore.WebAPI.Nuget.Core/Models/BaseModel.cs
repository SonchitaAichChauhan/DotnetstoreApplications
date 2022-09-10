using Dotnetstore.WebAPI.Nuget.Core.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnetstore.WebAPI.Nuget.Core.Models;

public abstract class BaseModel : Disposable
{
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid? ID { get; set; }

    [ScaffoldColumn(false)]
    public Guid? CreatedByID { get; set; }

    [Required]
    [ScaffoldColumn(false)]
    public DateTimeOffset? CreatedTime { get; set; } = DateTimeOffset.Now;

    [Required]
    [ScaffoldColumn(false)]
    public bool? IsDeleted { get; set; } = false;

    [ScaffoldColumn(false)]
    public Guid? DeletedByID { get; set; }

    [ScaffoldColumn(false)]
    public DateTimeOffset? DeletedTime { get; set; }

    [Required]
    [ScaffoldColumn(true)]
    public bool? IsDead { get; set; } = false;

    [Required]
    [ScaffoldColumn(true)]
    public bool? IsGDPR { get; set; } = false;

    [Required]
    [ScaffoldColumn(false)]
    public bool? IsSystem { get; set; } = false;

    [Required]
    [ScaffoldColumn(true)]
    public bool? IsPublic { get; set; } = true;

    [Required]
    [ScaffoldColumn(false)]
    public bool? IsUpdated { get; set; } = false;

    [ScaffoldColumn(false)]
    public Guid? LastUpdatedByID { get; set; }

    [ScaffoldColumn(false)]
    public DateTimeOffset? LastUpdatedTime { get; set; }

    [ScaffoldColumn(false)]
    public bool? IsVisible { get; set; } = false;

    [Timestamp]
    public byte[]? RowVersion { get; set; }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            CreatedByID = null;
            DeletedByID = null;
            DeletedTime = null;
            LastUpdatedByID = null;
            LastUpdatedTime = null;
            RowVersion = null;
            IsVisible = null;
            IsUpdated = null;
            IsPublic = null;
            IsSystem = null;
            IsGDPR = null;
            IsDead = null;
            IsDeleted = null;
            CreatedTime = null;
            ID = null;
        }

        base.DisposeManaged();
    }
}