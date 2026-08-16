using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.Inferastructure;

public static  class SpecficationEvaluator
{
    public static IQueryable<TEntity> CreateQuery <TEntity ,Tkey> (IQueryable<TEntity> inputQuery , 
        Ispacefications<TEntity,Tkey> specfication ) where TEntity : BaseEntity<Tkey>
    {
        var query = inputQuery;
        if (specfication.Where != null)
            query = query.Where(specfication.Where);
        if (specfication.Includes.Count > 0)
        {
            foreach (var space in specfication.Includes)
            {
                query = query.Include(space);
            }
        }

        if(specfication.OrderBy != null)
        {
            query = query.OrderBy(specfication.OrderBy);    
        }
        else if(specfication.OrderByDescending != null)
        {
            query = query.OrderByDescending(specfication.OrderByDescending);
        }

        if(specfication.IsBaginated )
        {
            query = query
                .Skip(specfication.Skip)
                .Take(specfication.Take);   
        }


        return query;
    }
}
