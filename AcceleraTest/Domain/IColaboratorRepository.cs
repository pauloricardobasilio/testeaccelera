using Domain.Contracts;

namespace Domain
{
    public interface IColaboratorRepository : IRepository<Colaborator>
    {
        Colaborator GetByRegistry(string registry);

        bool Exists(string registry);

        bool ExistsAnother(string registry, int idToExcludeFromQuery);
    }
}
