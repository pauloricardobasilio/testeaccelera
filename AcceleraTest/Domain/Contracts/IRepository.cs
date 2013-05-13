using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IRepository<T> where T : class
    {
        IUnityOfWork UnityOfWork { get; }

        void Add(T entity);

        void Remove(T entity);

        void Modify(T entity);

        void Track(T entity);

        void Merge(T persisted, T current);

        T Get(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetByFilter(Func<T, bool> filter);
    }
}
