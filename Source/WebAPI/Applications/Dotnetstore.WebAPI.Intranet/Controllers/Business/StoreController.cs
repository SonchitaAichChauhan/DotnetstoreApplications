using Dotnetstore.UnitOfWorks.Intranet.Interfaces.Businesses;
using Dotnetstore.WebAPI.Nuget.Intranet.Dto.Businesses.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Dotnetstore.WebAPI.Intranet.Controllers.Business;

[ApiController]
[Route("api/[controller]/[action]")]
public class StoreController : ControllerBase
{
    private readonly IStoreWrapper _storeWrapper;

    public StoreController(
        IStoreWrapper storeWrapper)
    {
        _storeWrapper = storeWrapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetQuantityAllActiveAsync(RequestQuantityAllActiveStoresDto requestQuantityAllActiveStoresDto)
    {
        var result = await _storeWrapper.GetQuantityAllActiveAsync(requestQuantityAllActiveStoresDto);

        return result.Quantity switch
        {
            0 => NoContent(),
            < 0 => BadRequest(),
            _ => Ok(result)
        };
    }
}