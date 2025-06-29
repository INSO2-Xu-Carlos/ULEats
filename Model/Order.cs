// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace DataModel
{
	[Table("orders")]
	public class Order
	{
		[Column("order_id"               , IsPrimaryKey = true , IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public int             OrderId               { get; set; } // integer
		[Column("restaurant_id"                                                                                             )] public int             RestaurantId          { get; set; } // integer
		[Column("delivery_id"                                                                                               )] public int?            DeliveryId            { get; set; } // integer
		[Column("order_date"                                                                                                )] public DateTimeOffset? OrderDate             { get; set; } // timestamp (6) with time zone
		[Column("status"                 , CanBeNull    = false                                                             )] public string          Status                { get; set; } = null!; // character varying(20)
		[Column("delivery_address"       , CanBeNull    = false                                                             )] public string          DeliveryAddress       { get; set; } = null!; // text
		[Column("subtotal"                                                                                                  )] public decimal         Subtotal              { get; set; } // numeric(10,2)
		[Column("delivery_fee"                                                                                              )] public decimal         DeliveryFee           { get; set; } // numeric(10,2)
		[Column("total_amount"                                                                                              )] public decimal         TotalAmount           { get; set; } // numeric(10,2)
		[Column("estimated_delivery_time"                                                                                   )] public DateTimeOffset? EstimatedDeliveryTime { get; set; } // timestamp (6) with time zone
		[Column("actual_delivey_time"                                                                                       )] public DateTimeOffset? ActualDeliveyTime     { get; set; } // timestamp (6) with time zone
		[Column("special_instructions"                                                                                      )] public string?         SpecialInstructions   { get; set; } // text

		#region Associations
		/// <summary>
		/// FK_delivery_id
		/// </summary>
		[Association(ThisKey = nameof(DeliveryId), OtherKey = nameof(DataModel.Delivery.DeliveryId))]
		public Delivery? Delivery { get; set; }

		/// <summary>
		/// FK_order_id backreference
		/// </summary>
		[Association(ThisKey = nameof(OrderId), OtherKey = nameof(Payment.OrderId))]
		public IEnumerable<Payment> Payments { get; set; } = null!;

		/// <summary>
		/// FK_order_id backreference
		/// </summary>
		[Association(ThisKey = nameof(OrderId), OtherKey = nameof(OrderTracking.OrderId))]
		public IEnumerable<OrderTracking> OrderTrackings { get; set; } = null!;

		/// <summary>
		/// FK_restaurant_id
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(RestaurantId), OtherKey = nameof(DataModel.Restaurant.RestaurantId))]
		public Restaurant Restaurant { get; set; } = null!;
		#endregion
	}
}
