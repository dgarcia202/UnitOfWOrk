namespace Entities
{
    using System;

    public class Provider
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Address { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool Active { get; set; } 
    }
}