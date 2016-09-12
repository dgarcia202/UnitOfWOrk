namespace Entities
{
    public class Provider
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Address { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool Active { get; set; } 
    }
}