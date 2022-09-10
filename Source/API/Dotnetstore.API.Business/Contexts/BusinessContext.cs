using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Intranet.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.API.Business.Contexts;

public class BusinessContext : DbContext, IBusinessEntityContextService
{
    public BusinessContext(DbContextOptions<BusinessContext> options) : base(options)
    {
    }

    public DbSet<BusinessEntity> BusinessEntities => Set<BusinessEntity>();

    public DbSet<Store> Stores => Set<Store>();
}