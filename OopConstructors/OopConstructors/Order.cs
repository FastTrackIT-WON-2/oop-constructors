using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopConstructors
{
	public class Order
	{
		private const int MaxItemsCount = 100;

		private readonly OrderItem[] orderItems = new OrderItem[Order.MaxItemsCount];

		private readonly OrderStatistics statistics = new OrderStatistics();

		public int ItemsCount { get; private set; } = 0;

		public decimal Total
		{
			get
			{
				decimal total = 0;
				for (int i = 0; i < this.ItemsCount; i++)
				{
					total += this.orderItems[i].Price * this.orderItems[i].Quantity;
				}

				return total;
			}
		}

		public void AddOrUpdateItem(string name, int quantity, decimal price = 0M)
		{
			if (this.ItemsCount < this.orderItems.Length - 1)
			{
				OrderItem newItem = new OrderItem
				{
					Name = name,
					Quantity = quantity,
					Price = price
				};

				OrderItem.OrderItemLookup alreadyOrderedResult = newItem.CheckIsAlreadyOrdered(this);

				if (!alreadyOrderedResult.IsAlreadyOrdered)
				{
					// new product, add it to array
					this.orderItems[this.ItemsCount] = newItem;
					this.statistics.UpdateStatistics(newItem);
					this.ItemsCount++;
					Console.WriteLine($"Adding new item '{name}' to order, with quantity={quantity} and price={price}");
				}
				else
				{
					// existing product, update its quantity
					OrderItem updatedItem = new OrderItem
					{
						Name = this.orderItems[alreadyOrderedResult.Index].Name,
						Quantity = this.orderItems[alreadyOrderedResult.Index].Quantity + quantity,
						Price = this.orderItems[alreadyOrderedResult.Index].Price
					};

					this.orderItems[alreadyOrderedResult.Index] = updatedItem;
					this.statistics.UpdateStatistics(updatedItem);
					Console.WriteLine($"Updating item '{updatedItem.Name}' quantity to {updatedItem.Quantity} and keeping price={updatedItem.Price}");
				}
			}
			else
			{
				Console.WriteLine($"Cannot order more than {Order.MaxItemsCount} items");
			}
		}

		public void PrintOrder()
		{
			string noCrtHeading = "#".PadLeft(this.ItemsCount.ToString().Length, ' ');
			string itemNameHeading = "Item".PadRight(this.statistics.MaxItemNameCharacters + 5, ' ');
			string itemQuantityHeading = "Qty".PadLeft(this.statistics.MaxQuantityDigits + 5, ' ');
			string itemPriceHeading = "Price".PadLeft(this.statistics.MaxPriceDigits + 5, ' ');
			string itemTotalHeading = "Total".PadLeft(this.statistics.MaxItemTotalDigits + 5, ' ');

			string fullHeading = $"{noCrtHeading}  {itemNameHeading}{itemQuantityHeading}{itemPriceHeading}{itemTotalHeading}";

			Console.WriteLine(new string('=', fullHeading.Length));
			Console.WriteLine(fullHeading);
			Console.WriteLine(new string('-', fullHeading.Length));

			for (int i = 0; i < this.ItemsCount; i++)
			{
				string noCrt = (i + 1).ToString().PadLeft(this.ItemsCount.ToString().Length, ' ');
				string itemName = this.orderItems[i].Name.PadRight(this.statistics.MaxItemNameCharacters + 5, ' ');
				string itemQuantity = this.orderItems[i].Quantity.ToString().PadLeft(this.statistics.MaxQuantityDigits + 5, ' ');
				string itemPrice = this.orderItems[i].Price.ToString().PadLeft(this.statistics.MaxPriceDigits + 5, ' ');
				string itemTotal = this.orderItems[i].ItemTotal.ToString().PadLeft(this.statistics.MaxItemTotalDigits + 5, ' ');

				Console.WriteLine($"{noCrt}) {itemName}{itemQuantity}{itemPrice}{itemTotal}");
			}

			Console.WriteLine(new string('-', fullHeading.Length));

			string grandTotalFooter = $"TOTAL = {this.Total.ToString().PadLeft(this.Total.ToString().Length + 5, ' ')}".PadLeft(fullHeading.Length, ' ');
			Console.WriteLine(grandTotalFooter);

			Console.WriteLine(new string('=', fullHeading.Length));
		}

		private class OrderItem
		{
			public string Name { get; set; } = string.Empty;

			public int Quantity { get; set; } = 0;

			public decimal Price { get; set; } = 0M;

			public decimal ItemTotal { get { return this.Quantity * this.Price; } }

			public OrderItemLookup CheckIsAlreadyOrdered(Order o)
			{
				for (int i = 0; i < (o?.ItemsCount ?? 0); i++)
				{
					if (string.Equals(o?.orderItems?[i]?.Name, this.Name, StringComparison.OrdinalIgnoreCase))
					{
						return new OrderItemLookup(true, i);
					}
				}

				return new OrderItemLookup(false, -1);
			}

			public class OrderItemLookup
			{
				public OrderItemLookup(bool isAlreadyOrdered, int index)
				{
					this.IsAlreadyOrdered = isAlreadyOrdered;
					this.Index = index;
				}

				public bool IsAlreadyOrdered
				{
					get;
				}

				public int Index
				{
					get;
				}
			}
		}

		private class OrderStatistics
		{
			public int MaxItemNameCharacters { get; set; } = 0;

			public int MaxQuantityDigits { get; set; } = 0;

			public int MaxPriceDigits { get; set; } = 0;

			public int MaxItemTotalDigits { get; set; } = 0;

			public void UpdateStatistics(OrderItem orderItem)
			{
				if (orderItem is null)
				{
					return;
				}

				if (orderItem.Name?.Length > this.MaxItemNameCharacters)
				{
					this.MaxItemNameCharacters = orderItem.Name?.Length ?? 0;
				}

				if (orderItem.Quantity.ToString().Length > this.MaxQuantityDigits)
				{
					this.MaxQuantityDigits = orderItem.Quantity.ToString().Length;
				}

				if (orderItem.Price.ToString().Length > this.MaxPriceDigits)
				{
					this.MaxPriceDigits = orderItem.Price.ToString().Length;
				}

				if (orderItem.ItemTotal.ToString().Length > this.MaxItemTotalDigits)
				{
					this.MaxItemTotalDigits = orderItem.ItemTotal.ToString().Length;
				}
			}
		}
	}
}
