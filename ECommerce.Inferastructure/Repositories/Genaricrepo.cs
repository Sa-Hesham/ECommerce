using ECommerce.Domain.Abstraction;
using ECommerce.Inferastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Inferastructure.Repositories;

public class Genaricrepo<TEntity, Tkey> : IGenaricRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
{
    private readonly AppDbContext _dbContext;

    public Genaricrepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Createasync(TEntity entity, CancellationToken ct = default)
    {
       await _dbContext.Set<TEntity>().AddAsync(entity, ct);   
    }

    public void ToggeToggleStatus(TEntity entity)
    {
        entity.IsDeleted = !entity.IsDeleted;
         
    }

    public async Task<IEnumerable<TEntity>> Getallasync(CancellationToken ct = default)
    {
        return await  _dbContext.Set<TEntity>().
            AsNoTracking().
            ToListAsync(ct);   
    } 

    public async Task<TEntity?> GettByIdAsync(Tkey id, CancellationToken ct = default)
    {
       return await _dbContext.Set<TEntity>().AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id!.Equals(id) , ct);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;  
       
        
    }

    public async Task<IEnumerable<TEntity>> Getallasync(Ispacefications<TEntity, Tkey> Spacefication, CancellationToken ct = default)
    {

       var query =  SpecficationEvaluator.CreateQuery<TEntity, Tkey>(_dbContext.Set<TEntity>(), Spacefication);

        return await query.ToListAsync(ct) ;


    }

    public async Task<TEntity?> GettByIdAsync(Ispacefications<TEntity, Tkey> Spacefication, CancellationToken ct = default)
    {
      var query = SpecficationEvaluator.CreateQuery<TEntity, Tkey>(_dbContext.Set<TEntity>(), Spacefication);
        return await query.FirstOrDefaultAsync(ct);
    }
    public async Task<int> totalCountasync(Ispacefications<TEntity, Tkey> Spacefication, CancellationToken ct = default)
    {
        return await SpecficationEvaluator.CreateQuery<TEntity, Tkey>(_dbContext.Set<TEntity>(), Spacefication).CountAsync(ct);
    }
    #region Spacefications

    #endregion
}
