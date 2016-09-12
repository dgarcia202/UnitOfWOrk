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
    }
}