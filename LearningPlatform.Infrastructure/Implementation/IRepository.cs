﻿namespace LearningPlatform.Infrastructure.Implementation;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    TEntity FindById(object id);
}