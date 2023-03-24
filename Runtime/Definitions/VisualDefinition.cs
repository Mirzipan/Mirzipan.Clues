using UnityEngine;

namespace Mirzipan.Definitions.Runtime.Definitions
{
    [CreateAssetMenu(fileName = "NewVisualDefinition", menuName = "Framed/Definitions/Visual Definition", order = 0)]
    public class VisualDefinition : Definition
    {
        [Header("Visuals")]
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private Color32 _color;
        [SerializeField]
        private string _fullName;
        [SerializeField]
        private string _shortName;
        [SerializeField]
        private string _description;
        
        public Sprite Icon => _icon;
        public Color32 Color => _color;
        public string FullName => _fullName;
        public string ShortName => _shortName;
        public string Description => _description;
    }
}