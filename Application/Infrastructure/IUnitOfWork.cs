namespace Application.Infrastructure
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void RollBack();
    }
}