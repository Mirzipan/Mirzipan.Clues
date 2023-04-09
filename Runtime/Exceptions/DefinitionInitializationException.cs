using System;

namespace Mirzipan.Clues.Exceptions
{
    /// <summary>
    /// An exception fired when something in a definition is wrong, such as wrong value or missing value.
    /// </summary>
    public abstract class DefinitionInitializationException : Exception
    {
        protected DefinitionInitializationException() : base()
        {
        }

        protected DefinitionInitializationException(string message) : base(message)
        {
        }

        protected DefinitionInitializationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}