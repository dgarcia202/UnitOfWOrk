namespace Infrastructure
{
    using System;
    using System.Data;

    using Application.Infrastructure;

    using NHibernate;

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory innerSessionFactory;

        private ISession innerSession;

        private bool isCorrupted;

        public UnitOfWork(ISessionFactory innerSessionFactory)
        {
            this.innerSessionFactory = innerSessionFactory;
        }

        internal ISession Session
        {
            get
            {
                this.CheckCorrupted();
                if (this.innerSession == null)
                {
                    this.innerSession = this.innerSessionFactory.OpenSession();
                    this.innerSession.FlushMode = FlushMode.Commit;
                }

                return this.innerSession;
            }
        }

        public void Commit()
        {
            this.CheckCorrupted();
            using (var t = this.innerSession.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    // forces session flush
                    t.Commit();
                }
                catch (Exception e)
                {
                    this.isCorrupted = true;
                    t.Rollback();
                }
            }
        }

        public void Dispose()
        {
            if (this.innerSession != null)
            {
                if (this.innerSession.IsOpen)
                {
                    this.innerSession.Close();
                }

                this.innerSession.Dispose();
            }
        }

        private void CheckCorrupted()
        {
            if (this.isCorrupted)
            {
                throw new InvalidOperationException("This unit of work was rendered unusable by a previous error");
            }
        }
    }
}