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


	private int?pageIndex ;
	public int? PageIndex
	{
		get { return pageIndex; }
		set { pageIndex = value.HasValue ? Math.Max(value.Value,1) : null; }
	}


	private int ?pageSize;	
	public int ?PageSize { 
		get { return pageSize; }
		set { pageSize = value.HasValue ?Math.Clamp(value.Value, 5, 10) :null; }
	}   

   
}
