namespace Infrastructure
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Application.Infrastructure;

    using NHibernate;
    using NHibernate.Linq;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UnitOfWork unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork as UnitOfWork;

            if (this.unitOfWork == null)
            {
                throw new InvalidOperationException("Repository received an incorrect implementation of UnitOfWork.");
            }
        }

        public Type ElementType => this.unitOfWork.Session.Query<T>().ElementType;

        public Expression Expression => this.unitOfWork.Session.Query<T>().Expression;

        public IQueryProvider Provider => this.unitOfWork.Session.Query<T>().Provider;

        public void Add(T entity)
        {
            this.unitOfWork.Session.Save(entity);
        }

        public T Get(Guid id)
        {
            return this.unitOfWork.Session.Get<T>(id);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.unitOfWork.Session.Query<T>().GetEnumerator();
        }

        public void Remove(T entity)
        {
            this.unitOfWork.Session.Delete(entity);
        }
    }
}