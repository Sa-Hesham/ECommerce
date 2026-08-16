using ECommerce.Domain.Entities;
using System.Linq.Expressions;


namespace ECommerce.Domain.Abstraction;

public interface Ispacefications<TEntity , Tkey > where TEntity : BaseEntity<Tkey>
{

    Expression<Func<TEntity, bool>>? Where { get; }

    List<Expression<Func<TEntity,object>>> Includes { get;  }


    #region Sorting

    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDescending { get; }

    #endregion


    #region Pagination
    public int Take { get;}
    public int Skip {  get;}

    public bool IsBaginated { get;}     

    #endregion


}
