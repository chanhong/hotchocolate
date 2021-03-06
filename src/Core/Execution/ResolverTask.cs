﻿using System;
using System.Collections.Immutable;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace HotChocolate.Execution
{
    internal class ResolverTask
    {
        public ResolverTask(
            IExecutionContext executionContext,
            ObjectType objectType,
            FieldSelection fieldSelection,
            Path path,
            ImmutableStack<object> source,
            OrderedDictionary result)
        {
            Source = source;
            ObjectType = objectType;
            FieldSelection = fieldSelection;
            FieldType = fieldSelection.Field.Type;
            Path = path;
            Result = result;
            ResolverContext = new ResolverContext(executionContext, this);
        }

        public ImmutableStack<object> Source { get; }

        public ObjectType ObjectType { get; }

        public FieldSelection FieldSelection { get; }

        public IType FieldType { get; }

        public Path Path { get; }

        private OrderedDictionary Result { get; }

        public IResolverContext ResolverContext { get; }

        public object ResolverResult { get; set; }

        public void IntegrateResult(object value)
        {
            Result[FieldSelection.ResponseName] = value;
        }

        public FieldError CreateError(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(
                    "A field error mustn't be null or empty.", 
                    nameof(message));
            }

            return new FieldError(message, FieldSelection.Node);
        }
    }
}
