using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private readonly IList<Item> items;
        protected Bag(int capacity)
        {
            Capacity = capacity;
            items = new List<Item>();
            Items = new ReadOnlyCollection<Item>(items);
        }
        public int Capacity { get; set; }

        public int Load => Items.Sum(i=>i.Weight);

        public IReadOnlyCollection<Item> Items { get; }

        public void AddItem(Item item)
        {
            if (Load+item.Weight>Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }
            items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (items.Count==0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            Item resultItem = Items.FirstOrDefault(i=>i.GetType().Name==name);

            if (resultItem==null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag,name));
            }

            items.Remove(resultItem);
            return resultItem;
        }
    }
}
