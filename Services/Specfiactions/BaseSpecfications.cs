using ECommerce.Domain.Abstraction;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfiactions;

public abstract class BaseSpecfications<TEntity, Tkey> : Ispacefications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
{
     public Expression<Func<TEntity, bool>> ? Where { get; private set; }

    public   List<Expression<Func<TEntity, object>>> Includes { get; } = new();


    protected BaseSpecfications(Expression<Func<TEntity, bool>>? where)
    {
        Where = where;  
    }


    public void  AddInclude(Expression<Func<TEntity, object>> includes)
    {
        Includes.Add(includes);
    }

}


