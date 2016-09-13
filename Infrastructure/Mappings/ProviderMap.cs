namespace Infrastructure.Mappings
{
    using Entities;

    using FluentNHibernate.Mapping;

    public class ProviderMap : ClassMap<Provider>
    {
        public ProviderMap()
        {
            this.Table("PROVIDER");

            this.Id(x => x.Id)
                .GeneratedBy.Sequence("PROVIDER_SEQ")
                .Not.Nullable();

            this.Map(x => x.Name)
                .Column("NAME")
                .Length(255)
                .Not.Nullable();

            this.Map(x => x.Address)
                .Column("ADDRESS")
                .Length(255)
                .Not.Nullable();

            this.Map(x => x.PhoneNumber)
                .Column("PHONE")
                .Length(1)
                .Not.Nullable();
        }
    }
}