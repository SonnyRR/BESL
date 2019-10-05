namespace BESL.Application.Infrastructure.AutoMapper
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using global::AutoMapper;
    using global::AutoMapper.QueryableExtensions;

    public static class QueryableMappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            IConfigurationProvider configurationProvider,
            params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo(configurationProvider, membersToExpand);
        }

        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            IConfigurationProvider configurationProvider,
            object parameters)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo<TDestination>(configurationProvider, parameters);
        }
    }
}
