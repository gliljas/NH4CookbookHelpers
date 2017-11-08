using System;

namespace MappingRecipes.CalculatedProperties
{
    public class Invoice
    {
        public virtual Guid Id { get; protected set; }
        public virtual decimal Amount { get; set; }
        public virtual string Customer { get; set; }
        public virtual int InvoicesOnCustomer { get; protected set; }
    }
}