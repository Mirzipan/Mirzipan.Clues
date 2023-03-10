using Mirzipan.Bibliotheca.Identifiers;
using UnityEngine;

namespace Mirzipan.Definitions.Runtime.Definitions
{
    [CreateAssetMenu(fileName = "NewVisualDefinition", menuName = "Framed/Definitions/Visual Definition", order = 0)]
    public class VisualDefinition : Definition
    {
        private static readonly QuadByte VisualId = new QuadByte("vis");

        [Header("Visuals")]
        public Sprite Icon;
        public string FullName;
        public string ShortName;
        public string Description;

        public override QuadByte PrimaryId => VisualId;
        public override CompositeId Id => new CompositeId(PrimaryId, _secondaryId);
    }
}