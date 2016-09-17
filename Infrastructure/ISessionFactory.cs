namespace Infrastructure
{
    using NHibernate;

    public interface ISessionFactory
    {
        ISession OpenSession();
    }
}