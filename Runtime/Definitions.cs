﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mirzipan.Bibliotheca.Identifiers;
using Mirzipan.Clues.Exceptions;
using Mirzipan.Clues.Meta;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace Mirzipan.Clues
{
    public class Definitions : IDefinitions, IDisposable
    {
        private readonly Dictionary<Type, Dictionary<ulong, ADefinition>> _data;
        private readonly Dictionary<Type, ADefinition> _defaults;

        #region Lifecycle

        public Definitions()
        {
            _data = new Dictionary<Type, Dictionary<ulong, ADefinition>>();
            _defaults = new Dictionary<Type, ADefinition>();
        }

        public void Dispose()
        {
            UnloadDefinitions();
        }

        #endregion Lifecycle

        #region Manipulation

        public void LoadAtPath(string path)
        {
            LoadDefinitionsAtPath(path);
        }
        
        /// <summary>
        /// Adds the specified definition. In case there already is one with the same indexed type and id, this will overwrite it
        /// </summary>
        /// <param name="definition"></param>
        public void Add(ADefinition definition)
        {
            Type type = definition.GetType();
            AddDefinition(definition, type, true);
                
            var attributes = type.GetCustomAttributes<DefinitionTypeAttribute>();
            foreach (var attribute in attributes)
            {
                if (attribute.IndexedType != null)
                {
                    AddDefinition(definition, attribute.IndexedType, false);
                }
            }
        }

        /// <summary>
        /// Makes the specified definition the default one for its type.
        /// </summary>
        /// <param name="definition"></param>
        public void MakeDefault(ADefinition definition)
        {
            Type type = definition.GetType();
            RemoveDefault(type);
            
            definition.SetDefault(true);
            _defaults[type] = definition;
        }

        /// <summary>
        /// Removes the specified definition.
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public bool Remove(ADefinition definition)
        {
            Type type = definition.GetType();
            bool result = RemoveDefinition(definition, type);
            
            var attributes = type.GetCustomAttributes<DefinitionTypeAttribute>();
            foreach (var attribute in attributes)
            {
                if (attribute.IndexedType != null)
                {
                    result |= RemoveDefinition(definition, attribute.IndexedType);
                }
            }

            return result;
        }

        /// <summary>
        /// Removes the default definition of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool RemoveDefault<T>()
        {
            Type type = typeof(T);
            return RemoveDefault(type);
        }

        /// <summary>
        /// Removes the default definition of the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool RemoveDefault(Type type)
        {
            if (!_defaults.TryGetValue(type, out var previousDefault))
            {
                return false;
            }

            previousDefault.SetDefault(false);
            return _defaults.Remove(type);

        }

        #endregion Manipulation

        #region Queries

        /// <summary>
        /// Returns all definitions of a given type.
        /// </summary>
        /// <typeparam name="T">Definition type</typeparam>
        /// <returns>Definition of type, if found, null otherwise</returns>
        public IEnumerable<T> GetAll<T>() where T : ADefinition
        {
            if (!_data.TryGetValue(typeof(T), out var innerDefinition))
            {
                yield break;
            }

            if (innerDefinition == null)
            {
                yield break;
            }

            foreach (var definition in innerDefinition)
            {
                yield return (T) definition.Value;
            }
        }

        /// <summary>
        /// Returns a definition with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T">Definition type</typeparam>
        /// <returns>Definition of type and id, if found, null otherwise</returns>
        public T Get<T>(CompositeId id) where T : ADefinition
        {
            Type type = typeof(T);
            if (!_data.TryGetValue(type, out var innerDefinition))
            {
                return null;
            }

            ADefinition result;
            if (innerDefinition == null || !innerDefinition.TryGetValue(id.Value, out result))
            {
                return null;
            }

            return (T) result;
        }

        /// <summary>
        /// Returns the default definition for the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Default<T>() where T : ADefinition
        {
            Type type = typeof(T);
            return _defaults.TryGetValue(type, out var result) ? (T)result : null;
        }

        #endregion Queries

        #region Private

        private void LoadDefinitionsAtPath(string path)
        {
            var assets = Resources.LoadAll(path);

            foreach (var asset in assets)
            {
                var type = asset.GetType();
                if (!typeof(ADefinition).IsAssignableFrom(type))
                {
                    continue;
                }

                var definition = asset as ADefinition;
                if (definition == null || !definition.IsEnabled)
                {
                    continue;
                }
                
                try
                {
                    definition.Init();
                    Debug.AssertFormat(definition.Id.Value != 0u, "Definition {0} is missing an id.", definition);
                }
                catch (DefinitionInitializationException initializationException)
                {
                    LogDefinitionInitError(definition, asset, initializationException);
                    continue;
                }
                catch (Exception e)
                {
                    LogDefinitionInitError(definition, asset, e);
                    continue;
                }

                Add(definition);
            }
        }
        
        private void UnloadDefinitions()
        {
            var definitionsByType = _data.Values.ToList();
            foreach (var definitionMap in definitionsByType)
            {
                definitionMap.Clear();
            }

            _data.Clear();
        }
        
        private void AddDefinition(ADefinition definition, Type type, bool allowAsDefault)
        {
            if (!_data.TryGetValue(type, out var innerDefinitions))
            {
                innerDefinitions = new Dictionary<ulong, ADefinition>();
                _data[type] = innerDefinitions;
            }

            innerDefinitions[definition.Id.Value] = definition;

            if (allowAsDefault && definition.IsDefault)
            {
                _defaults[type] = definition;
            }
        }

        private bool RemoveDefinition(ADefinition definition, Type type)
        {
            if (!_data.TryGetValue(type, out var innerDefinitions))
            {
                return false;
            }

            return innerDefinitions.Remove(definition.Id.Value);
        }

        private static void LogDefinitionInitError(ADefinition definition, Object asset, Exception exception)
        {
#if UNITY_EDITOR
            Debug.LogErrorFormat("Error while processing {0} in {1}", definition.Id, AssetDatabase.GetAssetPath(asset));
#else
            Debug.LogErrorFormat("Error while processing {0} in {1}", definition.Id, asset.name);
#endif
            Debug.LogError(exception);
        }

        #endregion Private
    }
}