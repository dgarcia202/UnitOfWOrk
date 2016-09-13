namespace Infrastructure
{
    using System;
    using System.Data;

    using Application.Infrastructure;

    using NHibernate;

    public sealed class UnitOfWork : IUnitOfWork
    {
        [ThreadStatic]
        private static ISession session;

        public void Commit()
        {
            
        }

        public void RollBack()
        {
            
        }

        internal static ISession CurrentSession
        {
            get
            {
                if (session == null)
                {
                    session = OracleSessionFactory.OpenSession();
                    session.FlushMode = FlushMode.Commit;
                }

                return session;
            }
        }

        public void Dispose()
        {
            if (session != null)
            {
                if (session.IsOpen)
                {
                    session.Close();
                }

                session.Dispose();
                session = null;
            }
        }
    }
}