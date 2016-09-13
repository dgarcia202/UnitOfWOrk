namespace Test
{
    using System.Reflection;

    using Application.Infrastructure;

    using Infrastructure;

    using NUnit.Framework;

    using Rhino.Mocks;

    [TestFixture]
    public class UnitOfWorkTests
    {
        private readonly MockRepository mocks = new MockRepository();

        [Test]
        public void CanStartUnitOfWork()
        {
            var factory = this.mocks.DynamicMock<IUnitOfWorkFactory>();
            var unitOfWork = this.mocks.DynamicMock<IUnitOfWork>();

            // brute force attack to set my own factory via reflection
            var fieldInfo = typeof(UnitOfWork).GetField("unitOfWorkFactory",
                BindingFlags.Static | BindingFlags.SetField | BindingFlags.NonPublic);
            fieldInfo.SetValue(null, factory);

            using (this.mocks.Record())
            {
                Expect.Call(factory.Create()).Return(unitOfWork);
            }
            using (this.mocks.Playback())
            {
                var uow = UnitOfWork.Start();
            }
        }
    }
}