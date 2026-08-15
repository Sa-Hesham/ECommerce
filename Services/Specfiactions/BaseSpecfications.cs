using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities;
using System.Linq.Expressions;


namespace Services.Specfiactions;

public abstract class BaseSpecfications<TEntity, Tkey> : Ispacefications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
{
     public Expression<Func<TEntity, bool>> ? Where { get; private set; }

    public   List<Expression<Func<TEntity, object>>> Includes { get; } = new();
   

    protected BaseSpecfications(Expression<Func<TEntity, bool>>? where)
    {
        Where = where;  
    }


  protected void  AddInclude(Expression<Func<TEntity, object>> includes)
    {
        Includes.Add(includes);
    }
    public  Expression<Func<TEntity, object>>? OrderBy { get;  private set; }
    public  Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    protected void orderby(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;  
    }

    protected  void orderByDescending (Expression<Func<TEntity, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

}


