using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities;

public abstract class BaseEntity
{
    public Guid  id { get; protected set; }

    public DateTime CreatedAt {  get; protected set; }  
    public DateTime UpdatedAt {  get; protected set; } 


}
