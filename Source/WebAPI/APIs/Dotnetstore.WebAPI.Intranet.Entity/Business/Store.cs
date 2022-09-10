using Dotnetstore.WebAPI.Nuget.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnetstore.WebAPI.Intranet.Entity.Business;

[Table(name: nameof(Store), Schema = "Business")]
[Index(nameof(ID), IsUnique = true)]
[Index(nameof(Name), IsUnique = false)]
public class Store : Company
{
    [Required]
    public bool? IsOwnStore { get; set; }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            IsOwnStore = null;
        }

        base.DisposeManaged();
    }
}