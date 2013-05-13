using Domain;
using Domain.Contracts;
using Infraestructure.Data;
using Infraestructure.Data.Repositories;

namespace Application
{
    public static class ColaboratorServiceFactory
    {
        public static IColaboratorService Create()
        {
            IUnityOfWork unityOfWork = new UnityOfWork();
            IColaboratorRepository colaboratorRepository = new ColaboratorRepository(unityOfWork as UnityOfWork);

            return new ColaboratorService(colaboratorRepository);
        }
    }
}
