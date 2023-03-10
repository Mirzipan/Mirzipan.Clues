using System;

namespace Mirzipan.Definitions.Runtime.Definitions
{
    /// <summary>
    /// An exception fired when something in a definition is wrong, such as wrong value or missing value.
    /// </summary>
    public class DefinitionInitializationException : Exception
    {
        public DefinitionInitializationException() : base()
        {
        }

        public DefinitionInitializationException(string message) : base(message)
        {
        }

        public DefinitionInitializationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}