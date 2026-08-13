using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstraction;

public interface IUnitOfWork
{
    public Task<bool> SaveChangesasync(CancellationToken ct);


    // need to greate obj From Genaricrepo of <Tentity>

    IGenaricRepository<TEntity ,Tkey> GetRepo<TEntity  ,Tkey>() where TEntity :BaseEntity<Tkey> ;
}
