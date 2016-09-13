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

    public class Repository<T> : IRepository<T>
    {
        private readonly ISession session;

        public Repository()
        {
            this.session = OracleSessionFactory.OpenSession();
        }

        public Type ElementType => this.session.Query<T>().ElementType;

        public Expression Expression => this.session.Query<T>().Expression;

        public IQueryProvider Provider => this.session.Query<T>().Provider;

        public void Add(T entity)
        {
            this.session.Save(entity);
        }

        public T Get(Guid id)
        {
            return this.session.Get<T>(id);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.session.Query<T>().GetEnumerator();
        }

        public void Remove(T entity)
        {
            this.session.Delete(entity);
        }
    }
}