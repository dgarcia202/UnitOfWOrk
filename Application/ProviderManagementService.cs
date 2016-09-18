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
        private readonly IUnitOfWork unitOfWork;

        private readonly IRepository<Provider> providers;

        public OrderManagementService(IUnitOfWork unitOfWork, IRepository<Provider> providers)
        {
            this.providers = providers;
            this.unitOfWork = unitOfWork;
        }

        public IList<Provider> GetProviders()
        {
            return this.providers.ToList();
        }

        public void AddProviders()
        {
            var provider1 = new Provider { Name = "John Doe 777", PhoneNumber = "+34 633 732 716", Address = "C/ Enric Granados"};
            this.providers.Add(provider1);

            var provider2 = new Provider { Name = "John Doe 888", PhoneNumber = "+34 633 732 716", Address = "C/ Enric Granados", Active = true };
            this.providers.Add(provider2);

            var provider3 = new Provider { Name = "John Doe 999", PhoneNumber = "+34 633 732 716", Address = "C/ Enric Granados" };
            this.providers.Add(provider3);

            this.unitOfWork.Commit();
        }
    }
}