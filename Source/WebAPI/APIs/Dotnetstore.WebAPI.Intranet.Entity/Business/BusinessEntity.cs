using Dotnetstore.WebAPI.Nuget.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnetstore.WebAPI.Intranet.Entity.Business;

[Table(name: nameof(BusinessEntity), Schema = "Business")]
[Index(nameof(ID), IsUnique = true)]
public class BusinessEntity : BaseModel
{
}