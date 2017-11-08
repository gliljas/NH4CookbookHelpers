using System;
using System.Collections.Generic;

namespace MappingRecipes.Bidirectional
{
    public class Project
    {
        private readonly ISet<Order> _orders;

        public Project()
        {
            _orders = new HashSet<Order>();
        }

        public virtual Guid Id { get; protected set; }

        public virtual IEnumerable<Order> Orders => _orders;

        public virtual bool ConnectOrder(Order order)
        {
            if (order != null && _orders.Add(order))
            {
                order.ConnectProject(this);
                return true;
            }
            return false;
        }

        public virtual bool DisconnectOrder(Order order)
        {
            if (order != null && _orders.Contains(order))
            {
                _orders.Remove(order);
                order.DisconnectProject(this);
                return true;
            }
            return false;
        }
    }
}