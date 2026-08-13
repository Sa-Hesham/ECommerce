using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstraction;

public interface IGenaricRepository<TEntity ,Tkey> where TEntity : BaseEntity<Tkey>
{
    public Task<IEnumerable<TEntity> > Getallasync(CancellationToken ct = default);


    public Task<TEntity?> GettByIdAsync( Tkey id , CancellationToken ct = default);



    public Task Createasync(TEntity entity , CancellationToken ct = default);




    public void Update(TEntity entity ); 



    public void ToggeToggleStatus(TEntity entity );    
}
