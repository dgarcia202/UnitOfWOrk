namespace Infrastructure
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using Infrastructure.Mappings;

    using NHibernate;
    using NHibernate.Tool.hbm2ddl;

    public class OracleSessionFactory
    {
        private static ISessionFactory sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (sessionFactory == null)
                {
                    sessionFactory = Fluently.Configure()
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

                return sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}