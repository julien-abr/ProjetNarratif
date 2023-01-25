using System.Collections.Generic;
using UnityEngine;

namespace Team02
{
    [CreateAssetMenu(fileName = "Custom Hierarchy Rules", menuName = "Hierarchy Rules", order = 0)]
    public class HierarchyRulesSO : ScriptableObject
    {
        [SerializeField]
        private List<CustomHierarchyLines> customHierarchyLines;

        public List<CustomHierarchyLines> CustomHierarchyLines_ => customHierarchyLines;

        [System.Serializable]
        public class CustomHierarchyLines
        {
            public string typo;

            public HierarchyTypos hierarchyTypos;

            public Color fontColor;

            public Color bgColor;

            public TextAnchor alignment;
            public bool upperCase;
            public FontStyle fontStyle;

            public Texture icon;
        }

        public enum HierarchyTypos
        {
            StartWith,
            Contains,
            Tag,
            Layer,
            Component,
        }
    }
}
