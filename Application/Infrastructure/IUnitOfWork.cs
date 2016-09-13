namespace Application.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();

        void RollBack();
    }
}