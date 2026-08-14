using ECommerce.Domain.Entities;
using System.Linq.Expressions;


namespace ECommerce.Domain.Abstraction;

public interface Ispacefications<TEntity , Tkey > where TEntity : BaseEntity<Tkey>
{

    Expression<Func<TEntity, bool>>? Where { get; }

    List<Expression<Func<TEntity,object>>> Includes { get;  }
}
