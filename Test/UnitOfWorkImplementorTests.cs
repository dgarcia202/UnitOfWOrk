namespace Test
{
    using Application.Infrastructure;

    using Infrastructure;

    using NHibernate;

    using NUnit.Framework;

    using Rhino.Mocks;

    [TestFixture]
    public class UnitOfWorkImplementorTests
    {
        private readonly MockRepository mocks = new MockRepository();
        private IUnitOfWorkFactory factory;
        private ISession session;
        private UnitOfWorkImplementor uow;

        [SetUp]
        public void SetupContext()
        {
            this.factory = this.mocks.DynamicMock<IUnitOfWorkFactory>();
            this.session = this.mocks.DynamicMock<ISession>();
        }

        [Test]
        public void CanDisposeUnitOfWorkImplementor()
        {
            using (mocks.Record())
            {
                Expect.Call(() => factory.DisposeUnitOfWork(null)).IgnoreArguments();
                Expect.Call(session.Dispose);
            }
            using (mocks.Playback())
            {
                uow = new UnitOfWorkImplementor(factory, session);
                uow.Dispose();
            }
        }

        [Test]
        public void Can_Dispose_UnitOfWorkImplementor()
        {
            using (mocks.Record())
            {
                Expect.Call(() => factory.DisposeUnitOfWork(null)).IgnoreArguments();
                Expect.Call(session.Dispose);
            }
            using (mocks.Playback())
            {
                uow = new UnitOfWorkImplementor(factory, session);
                uow.Dispose();
            }
        }

        [Test]
        public void Can_BeginTransaction()
        {
            using (mocks.Record())
            {
                Expect.Call(session.BeginTransaction()).Return(null);
            }
            using (mocks.Playback())
            {
                uow = new UnitOfWorkImplementor(factory, session);
                var transaction = uow.BeginTransaction();
                Assert.IsNotNull(transaction);
            }
        }

        [Test]
        public void Can_BeginTransaction_specifying_isolation_level()
        {
            var isolationLevel = IsolationLevel.Serializable;
            using (mocks.Record())
            {
                Expect.Call(session.BeginTransaction(isolationLevel)).Return(null);
            }
            using (mocks.Playback())
            {
                uow = new UnitOfWorkImplementor(factory, session);
                var transaction = uow.BeginTransaction(isolationLevel);
                Assert.IsNotNull(transaction);
            }
        }

        [Test]
        public void Can_execute_TransactionalFlush()
        {
            var tx = mocks.CreateMock<ITransaction>();
            var session = mocks.DynamicMock<ISession>();
            SetupResult.For(session.BeginTransaction(IsolationLevel.ReadCommitted)).Return(tx);

            uow = mocks.PartialMock<UnitOfWorkImplementor>(factory, session);

            using (mocks.Record())
            {
                Expect.Call(tx.Commit);
                Expect.Call(tx.Dispose);
            }
            using (mocks.Playback())
            {
                uow = new UnitOfWorkImplementor(factory, session);
                uow.TransactionalFlush();
            }
        }

        [Test]
        public void Can_execute_TransactionalFlush_specifying_isolation_level()
        {
            var tx = mocks.CreateMock<ITransaction>();
            var session = mocks.DynamicMock<ISession>();
            SetupResult.For(session.BeginTransaction(IsolationLevel.Serializable)).Return(tx);

            uow = mocks.PartialMock<UnitOfWorkImplementor>(factory, session);

            using (mocks.Record())
            {
                Expect.Call(tx.Commit);
                Expect.Call(tx.Dispose);
            }
            using (mocks.Playback())
            {
                uow.TransactionalFlush(IsolationLevel.Serializable);
            }
        }
    }
}