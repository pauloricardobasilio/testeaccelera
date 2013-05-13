using System.Linq;
using Domain;
using Domain.Contracts;
using Infraestructure.Data.Contracts;

namespace Infraestructure.Data.Repositories
{
    public class ColaboratorRepository : Repository<Colaborator>, IColaboratorRepository
    {
        public ColaboratorRepository(IEFUnityOfWork unityOfWork)
            : base(unityOfWork)
        { }

        public Colaborator GetByRegistry(string registry)
        {
            var uow = UnityOfWork as UnityOfWork;

            var set = uow.CreateSet<Colaborator>();

            return set.FirstOrDefault(x => x.Registry.ToLower() == registry.ToLower());
        }

        public bool Exists(string registry)
        {
            var uow = UnityOfWork as UnityOfWork;

            var dbSet = uow.CreateSet<Colaborator>();

            return dbSet.Any(x => x.Registry.ToLower() == registry.ToLower());
        }

        public bool ExistsAnother(string registry, int idToExcludeFromQuery)
        {
            var uow = UnityOfWork as UnityOfWork;
            var dbSet = uow.CreateSet<Colaborator>();

            return dbSet.Any(x => x.Registry.ToLower() == registry.ToLower() && x.Id != idToExcludeFromQuery);
        }

        public bool Exists(Colaborator colaborator)
        {
            var uow = UnityOfWork as UnityOfWork;
            var dbSet = uow.CreateSet<Colaborator>();

            return dbSet.Any(x => x.Registry.ToLower() == colaborator.Registry.ToLower() && x.Id != colaborator.Id);
        }
    }
}
