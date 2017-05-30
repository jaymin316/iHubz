using System;

namespace iHubz.Domain.Core
{
    public class ImmutableCollectionForAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
