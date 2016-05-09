﻿namespace Te4Fest.Services.Administration.Contracts
{
    using System.Linq;

    using Te4Fest.Data.Contracts;
    using Te4Fest.Services.Common;

    public interface IAdministrationService<TEntity> : IService
        where TEntity : IEntity
    {
        IQueryable<TEntity> Read();

        TEntity Get(params object[] id);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(params object[] id);

        void Delete(TEntity entity);
    }
}
