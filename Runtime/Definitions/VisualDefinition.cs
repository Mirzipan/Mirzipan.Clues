using UnityEngine;

namespace Mirzipan.Definitions.Runtime.Definitions
{
    [CreateAssetMenu(fileName = "NewVisualDefinition", menuName = "Framed/Definitions/Visual Definition", order = 0)]
    public class VisualDefinition : Definition
    {
        [Header("Visuals")]
        public Sprite Icon;
        public string FullName;
        public string ShortName;
        public string Description;
    }
}