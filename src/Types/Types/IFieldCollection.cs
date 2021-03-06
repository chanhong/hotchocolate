﻿using System.Collections.Generic;

namespace HotChocolate.Types
{
    public interface IFieldCollection<out T>
        : IEnumerable<T>
        where T : IField
    {
        T this[string fieldName] { get; }

        bool ContainsField(string fieldName);
    }

    public static class FieldCollectionExtensions
    {
        public static bool TryGetField<T>(
            this IFieldCollection<T> collection,
            string fieldName,
            out T field)
            where T : IField
        {
            if(collection.ContainsField(fieldName))
            {
                field = collection[fieldName];
                return true;
            }

            field = default;
            return false;
        }
    }
}
