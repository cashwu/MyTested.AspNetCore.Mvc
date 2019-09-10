﻿namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data;
    using Builders.Base;
    using System.Collections.Generic;

    /// <summary>
    /// Contains <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderCachingExtensions
    {
        /// <summary>
        /// Sets initial values to the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="memoryCacheBuilder">Action setting the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> values by using <see cref="IWithMemoryCacheBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithMemoryCacheBuilder> memoryCacheBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            memoryCacheBuilder(new MemoryCacheWithBuilder(actualBuilder.TestContext.HttpContext.RequestServices));

            return actualBuilder.Builder;
        }

        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutMemoryCache(cache => cache.ClearCache());

        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder, 
            object key)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutMemoryCache(cache => cache.WithoutEntry(key));

        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<object> keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutMemoryCache(cache => cache.WithoutEntries(keys));

        public static TBuilder WithoutMemoryCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutMemoryCacheBuilder> memoryCacheBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            memoryCacheBuilder(new MemoryCacheWithoutBuilder(actualBuilder.TestContext.HttpContext.RequestServices));

            return actualBuilder.Builder;
        }
    }
}
