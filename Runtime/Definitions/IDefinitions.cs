using System.Collections.Generic;
using Mirzipan.Bibliotheca.Identifiers;

namespace Mirzipan.Definitions.Runtime.Definitions
{
    public interface IDefinitions
    {
        /// <summary>
        /// Returns all definitions of a given type.
        /// </summary>
        /// <typeparam name="T">Definition type</typeparam>
        /// <returns>Definition of type, if found, null otherwise</returns>
        IEnumerable<T> GetAll<T>() where T : Definition;

        /// <summary>
        /// Returns a definition with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T">Definition type</typeparam>
        /// <returns>Definition of type and id, if found, null otherwise</returns>
        T Get<T>(CompositeId id) where T : Definition;

        /// <summary>
        /// Returns the default definition for the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Default<T>() where T : Definition;
    }
}