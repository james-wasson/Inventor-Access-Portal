using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace InventorAccessPortal.EntityFramework.Repositories
{
    public abstract class InventorAccessPortalRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<InventorAccessPortalDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected InventorAccessPortalRepositoryBase(IDbContextProvider<InventorAccessPortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class InventorAccessPortalRepositoryBase<TEntity> : InventorAccessPortalRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected InventorAccessPortalRepositoryBase(IDbContextProvider<InventorAccessPortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
