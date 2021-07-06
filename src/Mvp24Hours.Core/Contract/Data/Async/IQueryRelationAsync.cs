//=====================================================================================
// Developed by Kallebe Lins (kallebe.santos@outlook.com)
// Teacher, Architect, Consultant and Project Leader
// Virtual Card: https://www.linkedin.com/in/kallebelins
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Core.Contract.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mvp24Hours.Core.Contract.Data
{
    /// <summary>
    /// Class with asynchronous functions to load entities from instances
    /// </summary>
    /// <typeparam name="T">Represents an entity</typeparam>
    public interface IQueryRelationAsync<T>
        where T : IEntityBase
    {
        /// <summary>
        /// Load entity related to model entity
        /// </summary>
        Task LoadRelationAsync<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression)
            where TProperty : class;
        /// <summary>
        /// Load entities related to model entity
        /// </summary>
        Task LoadRelationAsync<TProperty, TKey>(T entity,
            Expression<Func<T, IEnumerable<TProperty>>> propertyExpression,
            Expression<Func<TProperty, bool>> clause = null,
            Expression<Func<TProperty, TKey>> orderKey = null,
            Expression<Func<TProperty, TKey>> orderDescendingKey = null,
            int limit = 0)
            where TProperty : class;
    }
}
