using System;
using HotChocolate.Internal;
using HotChocolate.Types;

namespace HotChocolate.Configuration
{
    internal partial class SchemaConfiguration
    {
        #region RegisterType - Type

        public void RegisterType<T>()
            where T : class, INamedType
        {
            CreateAndRegisterType(typeof(T));
        }

        public void RegisterQueryType<T>()
            where T : class
        {
            INamedType type = RegisterObjectType<T>();
            Options.QueryTypeName = type.Name;
        }

        public void RegisterMutationType<T>()
            where T : class
        {
            INamedType type = RegisterObjectType<T>();
            Options.MutationTypeName = type.Name;
        }

        public void RegisterSubscriptionType<T>()
            where T : class
        {
            INamedType type = RegisterObjectType<T>();
            Options.SubscriptionTypeName = type.Name;
        }

        public INamedType RegisterObjectType<T>()
            where T : class
        {
            return typeof(ObjectType).IsAssignableFrom(typeof(T))
                ? CreateAndRegisterType(typeof(T))
                : CreateAndRegisterType(typeof(ObjectType<T>));
        }

        private INamedType CreateAndRegisterType(Type type)
        {
            if (BaseTypes.IsNonGenericBaseType(type))
            {
                throw new SchemaException(new SchemaError(
                    "You cannot add a type without specifing its " +
                    "name and attributes."));
            }

            TypeReference typeReference = new TypeReference(type);
            _typeRegistry.RegisterType(typeReference);
            return _typeRegistry.GetType<INamedType>(typeReference);
        }

        #endregion

        #region RegisterType - Instance

        public void RegisterType<T>(T namedType)
            where T : class, INamedType
        {
            _typeRegistry.RegisterType(namedType);
        }

        public void RegisterQueryType<T>(T objectType) where T : ObjectType
        {
            Options.QueryTypeName = objectType.Name;
            _typeRegistry.RegisterType(objectType);
        }

        public void RegisterMutationType<T>(T objectType) where T : ObjectType
        {
            Options.MutationTypeName = objectType.Name;
            _typeRegistry.RegisterType(objectType);
        }

        public void RegisterSubscriptionType<T>(T objectType) where T : ObjectType
        {
            Options.SubscriptionTypeName = objectType.Name;
            _typeRegistry.RegisterType(objectType);
        }

        #endregion

        #region Directives

        public void RegisterDirective<T>() where T : Directive
        {
            throw new NotImplementedException();
        }

        public void RegisterDirective<T>(T directive) where T : Directive
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
