namespace Infrastructure
{
    using System;

    using Application.Infrastructure;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using Infrastructure.Mappings;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static ISession currentSession;

        public IUnitOfWork Create()
        {
            ISession session = OracleSessionFactory.OpenSession();
            session.FlushMode = FlushMode.Commit;
            currentSession = session;
            return new UnitOfWork(this, session);
        }
    }
}