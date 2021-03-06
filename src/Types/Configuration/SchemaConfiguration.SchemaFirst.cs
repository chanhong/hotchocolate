using System;
using HotChocolate.Resolvers;

namespace HotChocolate.Configuration
{
    internal partial class SchemaConfiguration
    {
        public IBindResolverDelegate BindResolver(
            AsyncFieldResolverDelegate fieldResolver)
        {
            if (fieldResolver == null)
            {
                throw new ArgumentNullException(nameof(fieldResolver));
            }

            var bindingInfo =
                new ResolverDelegateBindingInfo
                {
                    AsyncFieldResolver = fieldResolver
                };
            _resolverBindings.Add(bindingInfo);
            return new BindResolverDelegate(bindingInfo);
        }

        public IBindResolverDelegate BindResolver(
            FieldResolverDelegate fieldResolver)
        {
            if (fieldResolver == null)
            {
                throw new ArgumentNullException(nameof(fieldResolver));
            }

            var bindingInfo =
                new ResolverDelegateBindingInfo
                {
                    FieldResolver = fieldResolver
                };
            _resolverBindings.Add(bindingInfo);
            return new BindResolverDelegate(bindingInfo);
        }

        public IBindResolver<TResolver> BindResolver<TResolver>(
            BindingBehavior bindingBehavior)
            where TResolver : class
        {
            var bindingInfo =
                new ResolverCollectionBindingInfo
                {
                    Behavior = bindingBehavior,
                    ResolverType = typeof(TResolver)
                };
            _resolverBindings.Add(bindingInfo);
            return new BindResolver<TResolver>(bindingInfo);
        }

        public IBindType<T> BindType<T>(
            BindingBehavior bindingBehavior)
            where T : class
        {
            var bindingInfo = new TypeBindingInfo
            {
                Behavior = bindingBehavior,
                Type = typeof(T)
            };
            _typeBindings.Add(bindingInfo);
            return new BindType<T>(bindingInfo);
        }
    }
}
