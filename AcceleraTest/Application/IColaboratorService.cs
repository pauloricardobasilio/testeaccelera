using System;
using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface IColaboratorService
    {
        void AddColaborator(Colaborator colaborator);

        void UpdateColaborator(Colaborator colaborator);

        List<Colaborator> FindColaborators();

        List<Colaborator> FindColaborators(Func<Colaborator, bool> filter);

        Colaborator FindColaboratorById(int id);

        Colaborator FindColaboratorByRegistry(string registry);

        void RemoveColaborator(int id);
    }
}
