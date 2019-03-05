using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace microservice1
{
    public class EmployeeMapping: ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            Table("Employee");

            Id(x => x.Id, m =>
            {
                m.Column("Id");
                m.Generator(Generators.Identity);
            });

            Property(x => x.Name);
        }
    }
}
