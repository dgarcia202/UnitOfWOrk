namespace Infrastructure
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using Infrastructure.Mappings;

    using NHibernate;
    using NHibernate.Tool.hbm2ddl;

    public class OracleSessionFactory : ISessionFactory
    {
        private NHibernate.ISessionFactory sessionFactory;

        private NHibernate.ISessionFactory SessionFactory
        {
            get
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (this.sessionFactory == null)
                {
                    this.sessionFactory = Fluently.Configure()
                    .Database(OracleClientConfiguration.Oracle10
                    .ConnectionString("DATA SOURCE=XE;USER ID=PROVIDERS;PASSWORD=1234;")
                    .Driver<NHibernate.Driver.OracleClientDriver>())
                    .Mappings(m => m.FluentMappings.Add<ProviderMap>())
                    .ExposeConfiguration(config =>
                    {
                        SchemaExport schemaExport = new SchemaExport(config);
                    })
                    .BuildSessionFactory();
                }

                return this.sessionFactory;
            }
        }

        public ISession OpenSession()
        {
            return this.SessionFactory.OpenSession();
        }
    }
}