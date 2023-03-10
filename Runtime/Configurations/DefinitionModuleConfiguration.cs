using Mirzipan.Definitions.Runtime.Definitions;
using Mirzipan.Framed.Configurations;
using Mirzipan.Framed.Modules;
using UnityEngine;

namespace Mirzipan.Definitions.Runtime.Configurations
{
    [CreateAssetMenu(fileName = "DefinitionsConfiguration", menuName = "Framed/Definitions Configuration", order = 1100)]
    public class DefinitionModuleConfiguration: ConfigurationScriptableObject
    {
        [SerializeField]
        [Tooltip("Path relative to a Resources folder.")]
        private string _pathToLoadFrom = "Data";
        
        public override void AddBindings()
        {
            var module = new DefinitionsModule(_pathToLoadFrom);
            Container.Bind<IDefinitions>(module);
            Container.Bind(module);
            Container.Bind<CoreModule>(module, "definitions");
        }
    }
}