using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities;

public abstract class BaseEntity <Tkey> 
{
    public Tkey Id { get; protected set; } = default!;

    public DateTime CreatedAt {  get; protected set; }  = DateTime.Now;
    public DateTime ? UpdatedAt {  get; protected set; } 

    public bool IsDeleted { get;  set; }   


}
