﻿using UnityEngine;

namespace Mirzipan.Clues.Definition
{
    [CreateAssetMenu(fileName = "NewVisualDefinition", menuName = "Clues/Visual Definition", order = 0)]
    public class VisualDefinition : ADefinition
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