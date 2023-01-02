using System;

namespace LF.StringVariableReplacer.Exceptions
{
    [Serializable]
    public class ReplaceWithException : Exception
    {
        public ReplaceWithException() : base("Error during getting value of field member") { }
    }
}