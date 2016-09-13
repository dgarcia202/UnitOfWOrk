namespace Test
{
    using System;
    using System.Reflection;

    using Application.Infrastructure;

    using Infrastructure;

    using NHibernate;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class UnitOfWorkWithFactoryTests
    {
        private readonly MockRepository mocks = new MockRepository();
        private IUnitOfWorkFactory factory;
        private IUnitOfWork unitOfWork;

        private ISession session;

        [SetUp]
        public void SetupContext()
        {
            this.factory = this.mocks.DynamicMock<IUnitOfWorkFactory>();
            this.unitOfWork = this.mocks.DynamicMock<IUnitOfWork>();
            session = this.mocks.DynamicMock<ISession>();

            // brute force attack to set my own factory via reflection
            var fieldInfo = typeof(UnitOfWork).GetField("unitOfWorkFactory",
                BindingFlags.Static | BindingFlags.SetField | BindingFlags.NonPublic);
            fieldInfo.SetValue(null, this.factory);

            this.mocks.BackToRecordAll();
            SetupResult.For(this.factory.Create()).Return(this.unitOfWork);
            this.mocks.ReplayAll();
        }

        [TearDown]
        public void TearDownContext()
        {
            this.mocks.VerifyAll();

            // assert that the UnitOfWork is reset
            var fieldInfo = typeof(UnitOfWork).GetField("innerUnitOfWork",
                BindingFlags.Static | BindingFlags.SetField | BindingFlags.NonPublic);
            fieldInfo.SetValue(null, null);
        }

        [Test]
        public void StartingUnitOfWorkIfAlreadyStartedThrows()
        {
            UnitOfWork.Start();

            Assert.Throws<InvalidOperationException>(
                () =>
                    {
                        UnitOfWork.Start();
                    });
        }

        [Test]
        public void CanAccessCurrentUnitOfWork()
        {
            IUnitOfWork uow = UnitOfWork.Start();
            var current = UnitOfWork.Current;
            Assert.AreSame(uow, current);
        }

        [Test]
        public void AccessingCurrentUnitOfWorkIfNotStartedThrows()
        {
            Assert.Throws<InvalidOperationException>(
                () =>
                    {
                        var current = UnitOfWork.Current;
                    });
        }

        [Test]
        public void CanTestIfUnitOfWorkIsStarted()
        {
            Assert.IsFalse(UnitOfWork.IsStarted);

            IUnitOfWork uow = UnitOfWork.Start();
            Assert.IsTrue(UnitOfWork.IsStarted);
        }

        [Test]
        public void CanGetValidCurrentSessionIfUoWIsStarted()
        {
            using (UnitOfWork.Start())
            {
                ISession session = UnitOfWork.CurrentSession;
                Assert.IsNotNull(session);
            }
        }
    }
}