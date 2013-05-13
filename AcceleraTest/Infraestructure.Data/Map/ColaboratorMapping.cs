using System.Data.Entity.ModelConfiguration;
using Domain;

namespace Infraestructure.Data.Map
{
    public class ColaboratorMapping : EntityTypeConfiguration<Colaborator>
    {
        public ColaboratorMapping()
        {
            HasKey(x => x.Id);
        }
    }
}
