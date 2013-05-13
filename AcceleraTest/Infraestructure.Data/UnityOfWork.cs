using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Domain;
using Domain.Contracts;
using Infraestructure.Data.Contracts;
using Infraestructure.Data.Map;
using Infraestructure.Data.Migrations;

namespace Infraestructure.Data
{
    public class UnityOfWork : DbContext, IEFUnityOfWork
    {
        public UnityOfWork(string connection)
            : base(connection)
        { }

        public UnityOfWork()
            : base("Production")
        { }

        public DbSet<Colaborator> Colaborators { get; set; }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void Rollback()
        {
            ChangeTracker.Entries().ToList().ForEach(e => e.State = EntityState.Unchanged);
        }

        public IDbSet<T> CreateSet<T>() where T : class
        {
            return Set<T>();
        }

        public void Attach<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Unchanged;
        }

        public void SetModifiedState<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<T>(T original, T current) where T : class
        {
            Entry(original).CurrentValues.SetValues(current);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new ColaboratorMapping());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UnityOfWork, Configuration>());
        }

    }
}
