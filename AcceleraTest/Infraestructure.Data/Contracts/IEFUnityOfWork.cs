using System.Data.Entity;
using Domain.Contracts;

namespace Infraestructure.Data.Contracts
{
    public interface IEFUnityOfWork : IUnityOfWork
    {
        IDbSet<T> CreateSet<T>() where T : class;

        void Attach<T>(T entity) where T : class;

        void SetModifiedState<T>(T entity) where T : class;

        void ApplyCurrentValues<T>(T original, T current) where T : class;
    }
}
