using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.WebAPI.Intranet.Entity.Interfaces;

public interface IBusinessEntityContextService
{
    DbSet<BusinessEntity> BusinessEntities { get; }

    DbSet<Store> Stores { get; }
}