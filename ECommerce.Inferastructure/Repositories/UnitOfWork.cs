using ECommerce.Domain.Abstraction;
using ECommerce.Inferastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Inferastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _dbcontext;
    private Dictionary<Type, object> _repsitory;
    public UnitOfWork(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        _repsitory = [];
    }



    public IGenaricRepository<TEntity, Tkey> GetRepo<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
    {
        var key = typeof(TEntity);
        if (!_repsitory.ContainsKey(key))
        {
            _repsitory[key] = new Genaricrepo<TEntity, Tkey>(_dbcontext);
        }

        return (IGenaricRepository<TEntity, Tkey>)_repsitory[key];
       

    }


    public async Task<bool> SaveChangesasync(CancellationToken ct)
    {
       return await _dbcontext.SaveChangesAsync(ct)>0;    
    }
}
