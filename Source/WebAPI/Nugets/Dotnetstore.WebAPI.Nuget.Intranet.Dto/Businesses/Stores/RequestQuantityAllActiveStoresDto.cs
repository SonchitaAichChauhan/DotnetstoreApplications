using System.ComponentModel.DataAnnotations;

namespace Dotnetstore.WebAPI.Nuget.Intranet.Dto.Businesses.Stores;

public class RequestQuantityAllActiveStoresDto
{
    [Required]
    public bool IsOwnStore { get; set; }
}