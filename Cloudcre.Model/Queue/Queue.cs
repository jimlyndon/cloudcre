using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloudcre.Model.Core;

namespace Cloudcre.Model.Queue
{
    public class Queue : EntityBase<Guid>, IAggregateRoot
    {
        private readonly IList<QueuedItem> _items;

        public Queue()
        {
            _items = new List<QueuedItem>();
        }

        public Guid Id { get; set; }

        public void Add(MultipleFamily property)
        {
            if (!QueueContainsAnItemFor(property))
            {
                _items.Add(QueuedItemFactory.CreateItemFor(property, this));
            }
        }

        public QueuedItem GetItemFor(MultipleFamily property)
        {
            return _items.Where(i => i.Contains(property)).FirstOrDefault();
        }

        public void Remove(MultipleFamily property)
        {
            if(QueueContainsAnItemFor(property))
            {
                _items.Remove(GetItemFor(property));
            }
        }

        public bool QueueContainsAnItemFor(MultipleFamily property)
        {
            return _items.Any(i => i.Contains(property));
        }

        public IEnumerable<QueuedItem> Items()
        {
            return _items;
        }

        protected override void Validate()
        {
            foreach (var queuedItem in _items)
            {
                if(queuedItem.GetBrokenRules().Count() > 0)
                    base.AddBrokenRule(QueueBusinessRules.ItemInvalid);
            }
        }
    }
}