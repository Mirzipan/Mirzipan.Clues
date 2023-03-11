using Mirzipan.Bibliotheca.Identifiers;
using UnityEngine;

namespace Mirzipan.Definitions.Runtime.Definitions
{
    public abstract class Definition: ScriptableObject
    {
        [Header("Meta")]
        [SerializeField]
        protected bool _isEnabled;
        [SerializeField]
        protected bool _isDefault;
        [SerializeField]
        protected CompositeId _id;
        public bool IsEnabled => _isEnabled;
        public bool IsDefault => _isDefault;
        public CompositeId Id => _id;
        
        /// <summary>
        /// Initialize any things necessary.
        /// </summary>
        public virtual void Init()
        {
            
        }

        internal void SetDefault(bool isDefault) => _isDefault = isDefault;
    }
}