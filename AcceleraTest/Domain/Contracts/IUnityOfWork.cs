using System;

namespace Domain.Contracts
{
    public interface IUnityOfWork : IDisposable
    {
        void Commit();

        void Rollback();
    }
}