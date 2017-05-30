using System;
using System.Linq;
using System.Linq.Expressions;

namespace iHubz.Domain.Core
{
    public static class TypeAnalyser
    {
        public static string GetPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return GetPropertyName(me);
        }

        public static string ToPropertyName<T>(this Expression<Func<T, object>> selector)
        {
            var me = selector.Body as MemberExpression;
            if (me == null)
            {
                throw new ArgumentException("MemberException expected.");
            }

            return GetPropertyName(me);
        }

        private static string GetPropertyName(MemberExpression me)
        {
            // if it is the public immutable IEnumerable then need to get the name from the custom attribute
            var attr = me.Member.GetCustomAttributes(typeof(ImmutableCollectionForAttribute), false).FirstOrDefault();
            var immutableCollectionForAttribute = attr as ImmutableCollectionForAttribute;
            if (immutableCollectionForAttribute == null)
            {
                // otherwise just return the property name
                var path = me.ToString();
                var separatorIndex = path.IndexOf('.');
                return separatorIndex > 0 ? path.Substring(separatorIndex + 1) : path;
            }

            return immutableCollectionForAttribute.Name;
        }
    }
}
