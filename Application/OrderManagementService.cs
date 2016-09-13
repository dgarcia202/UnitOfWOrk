namespace Application
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Application.Infrastructure;

    using Entities;

    public class OrderManagementService : IOrderManagementService
    {
        private readonly IRepository<Provider> providers;

        public OrderManagementService(IRepository<Provider> providers)
        {
            this.providers = providers;
        }

        public IList<Provider> GetProviders()
        {
            return this.providers.ToList();
        }

        public void AddProviders()
        {
            using (UnitOfWork.Start())
            {
                var person = new Provider { Name = "John Doe", PhoneNumber = "+34 633 732 716" };
                UnitOfWork.CurrentSession.Save(person);
                UnitOfWork.Current.TransactionalFlush();
            }
        }
    }
}