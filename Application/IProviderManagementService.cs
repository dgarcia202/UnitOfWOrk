namespace Application
{
    using System.Collections.Generic;

    using Entities;

    public interface IProviderManagementService
    {
        IList<Provider> GetProviders();

        void AddProviders();
    }
}