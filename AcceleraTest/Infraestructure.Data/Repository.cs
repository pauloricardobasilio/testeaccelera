using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Contracts;
using Infraestructure.Data.Contracts;

namespace Infraestructure.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IEFUnityOfWork _unityOfWork;

        public IUnityOfWork UnityOfWork { get { return _unityOfWork; } }

        public Repository(IEFUnityOfWork unityOfWork)
        {
            if (unityOfWork == null)
                throw new ArgumentNullException();

            _unityOfWork = unityOfWork;
        }

        public void Add(T entity)
        {
            if (entity != null)
                GetSet().Add(entity);
        }

        public void Remove(T entity)
        {
            if (entity == null) return;
            _unityOfWork.Attach(entity);
            GetSet().Remove(entity);
        }

        public void Modify(T entity)
        {
            if (entity == null) return;
            _unityOfWork.SetModifiedState(entity);
        }

        public void Track(T entity)
        {
            if (entity == null) return;
            _unityOfWork.Attach(entity);
        }

        public void Merge(T persisted, T current)
        {
            _unityOfWork.ApplyCurrentValues(persisted, current);
        }

        public T Get(int id)
        {
            return GetSet().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return GetSet();
        }

        public IEnumerable<T> GetByFilter(Func<T, bool> filter)
        {
            return GetSet().Where(filter);
        }

        IDbSet<T> GetSet()
        {
            return _unityOfWork.CreateSet<T>();
        }

    }
}
