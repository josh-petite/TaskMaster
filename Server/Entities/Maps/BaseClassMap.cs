using FluentNHibernate.Mapping;

namespace Server.Entities.Maps
{
    public abstract class BaseClassMap<T> : ClassMap<T> where T : BaseEntity
    {
        protected BaseClassMap()
        {
            Map(o => o.CreatedBy);
            Map(o => o.CreatedAt);
        }
    }
}