using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public class ProductFiltiration
{
  public  int? brandId {  get; set; }
  public int? productTypeId {  get; set; }
  public  ProductSortingOptions? sort {  get; set; }

   public string ? Search { get; set; }
}
