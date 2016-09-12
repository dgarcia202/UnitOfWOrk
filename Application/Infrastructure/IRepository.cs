namespace Application.Infrastructure
{
    using System;
    using System.Linq;

    public interface IRepository<T> : IQueryable<T>
    {
        void Add(T entity);

        T Get(Guid id);

        void Remove(T entity);
    }
}