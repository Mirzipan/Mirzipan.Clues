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
        protected QuadByte _secondaryId;
    
        public bool IsEnabled => _isEnabled;
        public bool IsDefault => _isDefault;
        public QuadByte SecondaryId => _secondaryId;
        public abstract QuadByte PrimaryId { get; }
        public abstract CompositeId Id { get; }
        
        /// <summary>
        /// Initialize any things necessary.
        /// </summary>
        public virtual void Init()
        {
            
        }
    }
}