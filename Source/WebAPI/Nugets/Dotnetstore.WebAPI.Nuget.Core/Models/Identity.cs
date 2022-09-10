using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnetstore.WebAPI.Nuget.Core.Models;

public abstract class Identity : Person
{
    [ScaffoldColumn(false)]
    [DataType(DataType.DateTime)]
    public DateTimeOffset? ActivatedDate { get; set; }

    [Column(TypeName = "varchar(100)")]
    [StringLength(100, MinimumLength = 50)]
    [MinLength(50)]
    [MaxLength(100)]
    [ScaffoldColumn(false)]
    [DataType(DataType.Text)]
    public string? ActivationCode { get; set; }

    [ScaffoldColumn(false)]
    public Guid? BlockedByID { get; set; }

    [ScaffoldColumn(false)]
    [DataType(DataType.DateTime)]
    public DateTimeOffset? BlockedDate { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 20)]
    [Column(TypeName = "varchar(500)")]
    [MinLength(20)]
    [MaxLength(500)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    [Column(TypeName = "bit")]
    [ScaffoldColumn(true)]
    public bool? EnablePasswordReset { get; set; }

    [Required]
    [ScaffoldColumn(false)]
    public byte? InvalidLoginAttempts { get; set; } = 0;

    [Required]
    [ScaffoldColumn(false)]
    public bool? IsActivated { get; set; } = false;

    [Required]
    [ScaffoldColumn(true)]
    public bool? IsBlocked { get; set; } = false;

    [Required]
    [ScaffoldColumn(true)]
    [Range(1, 10)]
    public int? MaxInvalidLoginAttempts { get; set; } = 3;

    [Required]
    [StringLength(500, MinimumLength = 20)]
    [Column(TypeName = "varchar(500)")]
    [MinLength(20)]
    [MaxLength(500)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    [StringLength(100, MinimumLength = 20)]
    [MinLength(50)]
    [MaxLength(100)]
    [ScaffoldColumn(false)]
    [DataType(DataType.Text)]
    public string? Salt1 { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    [StringLength(100, MinimumLength = 20)]
    [MinLength(50)]
    [MaxLength(100)]
    [ScaffoldColumn(false)]
    [DataType(DataType.Text)]
    public string? Salt2 { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    [StringLength(100, MinimumLength = 20)]
    [MinLength(50)]
    [MaxLength(100)]
    [ScaffoldColumn(false)]
    [DataType(DataType.Text)]
    public string? Salt3 { get; set; }

    [Required]
    [Column(TypeName = "varchar(500)")]
    [StringLength(500, MinimumLength = 8)]
    [MinLength(8)]
    [MaxLength(500)]
    [ScaffoldColumn(true)]
    [DataType(DataType.Text)]
    public string? Username { get; set; }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            Username = null;
            Password = null;
            ConfirmPassword = null;
            Salt1 = null;
            Salt2 = null;
            Salt3 = null;
            ActivationCode = null;
            ActivatedDate = null;
            BlockedDate = null;
            BlockedByID = null;
            MaxInvalidLoginAttempts = null;
            ActivatedDate = null;
            EnablePasswordReset = null;
            InvalidLoginAttempts = null;
            IsActivated = null;
            IsBlocked = null;
        }

        base.DisposeManaged();
    }
}