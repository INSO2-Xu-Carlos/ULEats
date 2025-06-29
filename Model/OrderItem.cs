// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;

#pragma warning disable 1573, 1591
#nullable enable

namespace DataModel
{
	[Table("order_items")]
	public class OrderItem
	{
		[Column("order_item_id", IsPrimaryKey = true, IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public int     OrderItemId { get; set; } // integer
		[Column("product_id"                                                                                     )] public int     ProductId   { get; set; } // integer
		[Column("quantity"                                                                                       )] public int     Quantity    { get; set; } // integer
		[Column("unit_price"                                                                                     )] public decimal UnitPrice   { get; set; } // numeric(10,2)
		[Column("customer_id"                                                                                    )] public int     CustomerId  { get; set; } // integer

		#region Associations
		/// <summary>
		/// FK_customer_id
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(CustomerId), OtherKey = nameof(DataModel.Customer.CustomerId))]
		public Customer Customer { get; set; } = null!;

		/// <summary>
		/// FK_product_id
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(ProductId), OtherKey = nameof(DataModel.Product.ProductId))]
		public Product Product { get; set; } = null!;
		#endregion
	}
}
