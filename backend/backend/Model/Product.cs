// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;
using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace DataModel
{
	[Table("products")]
	public class Product
	{
		[Column("product_id"   , IsPrimaryKey = true , IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public int     ProductId    { get; set; } // integer
		[Column("restaurant_id"                                                                                   )] public int     RestaurantId { get; set; } // integer
		[Column("name"         , CanBeNull    = false                                                             )] public string  Name         { get; set; } = null!; // character varying(100)
		[Column("description"                                                                                     )] public string? Description  { get; set; } // text
		[Column("price"                                                                                           )] public decimal Price        { get; set; } // numeric(10,2)
		[Column("image_url"                                                                                       )] public string? ImageUrl     { get; set; } // character varying(255)
		[Column("category"                                                                                        )] public string? Category     { get; set; } // character varying(100)
		[Column("is_available"                                                                                    )] public bool?   IsAvailable  { get; set; } // boolean

		#region Associations
		/// <summary>
		/// FK_product_id backreference
		/// </summary>
		[Association(ThisKey = nameof(ProductId), OtherKey = nameof(OrderItem.ProductId))]
		public IEnumerable<OrderItem> OrderItems { get; set; } = null!;

		/// <summary>
		/// FK_restaurant_id
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(RestaurantId), OtherKey = nameof(DataModel.Restaurant.RestaurantId))]
		public Restaurant Restaurant { get; set; } = null!;
		#endregion
	}
}
