namespace Application
{
    using System.Collections.Generic;

    using Entities;

    public interface IOrderManagementService
    {
        IList<Provider> GetProviders();
    }
}