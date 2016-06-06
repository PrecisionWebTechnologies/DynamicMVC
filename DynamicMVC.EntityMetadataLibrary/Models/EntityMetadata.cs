using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DynamicMVC.Annotations;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.EntityMetadataLibrary.Models
{
    /// <summary>
    /// Holds Metadata for a DynamicEntity as desired by the developer.  This can be configured to be different than the default behavior or from what exists in the client application
    /// </summary>
    public class EntityMetadata
    {
        public EntityMetadata()
        {
            EntityAttributes = new List<Attribute>();
            var entityPropertyMetadata = new ObservableCollection<EntityPropertyMetadata>();
            entityPropertyMetadata.CollectionChanged += EntityPropertyMetadataOnCollectionChanged;
            EntityPropertyMetadata = entityPropertyMetadata;
        }

        private void EntityPropertyMetadataOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in notifyCollectionChangedEventArgs.NewItems)
                {
                    var entityMetadataProperty = (EntityPropertyMetadata) item;
                    entityMetadataProperty.EntityMetadata = this;
                }
            }
        }

        public EntityMetadata(string typeName)
            : this()
        {
            TypeName = typeName;
        }

        public string TypeName { get; set; }
        public List<Attribute> EntityAttributes { get; set; }
        public ICollection<EntityPropertyMetadata> EntityPropertyMetadata { get; set; }
        public Func<object> CreateNewObject { get; set; }
        public Type EntityType { get; set; }
        public IReflectedClass ReflectedClass { get; set; }

        public override string ToString()
        {
            return EntityType.Name;
        }

        public bool ControllerExists { get; set; }

        public DynamicEntityAttribute DynamicEntityAttribute()
        {
            return (DynamicEntityAttribute)EntityAttributes.Single(x => x is DynamicEntityAttribute);
        }
    }
}
