using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
        public string? CollectionName { get; set; }
        public EntityAttribute(string? collectionName = null)
        {
            CollectionName = collectionName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string? Name { get; set; }

        public ColumnAttribute(string? name = null)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CreatedDateAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class UpdatedDateAttribute : Attribute { }
}
