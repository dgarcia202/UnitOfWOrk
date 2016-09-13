namespace Test
{
    using System;

    using Application.Infrastructure;

    using Infrastructure;

    using NHibernate;

    using NUnit.Framework;

    [TestFixture]
    public class UnitOfWorkFactoryTests
    {
        private IUnitOfWorkFactory factory;

        [SetUp]
        public void SetupContext()
        {
            this.factory = (IUnitOfWorkFactory)Activator.CreateInstance(typeof(UnitOfWorkFactory), true);
        }

        [Test]
        public void CanCreateUnitOfWork()
        {
            IUnitOfWork implementor = this.factory.Create();
            Assert.IsNotNull(implementor);
            Assert.IsNotNull(this.factory.CurrentSession);
            Assert.AreEqual(FlushMode.Commit, this.factory.CurrentSession.FlushMode);
        }

        [Test]
        public void CanCreateAndAccessSessionFactory()
        {
            var sessionFactory = this.factory.SessionFactory;
            Assert.IsNotNull(sessionFactory);
        }

        [Test]
        public void AccessingCurrentSessionWhenNoSessionOpenThrows()
        {
            try
            {
                var session = this.factory.CurrentSession;
            }
            catch (InvalidOperationException)
            { }
        }
    }
}