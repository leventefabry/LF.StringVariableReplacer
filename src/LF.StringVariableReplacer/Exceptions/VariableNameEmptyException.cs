using System;

namespace LF.StringVariableReplacer.Exceptions
{
    [Serializable]
    public class VariableNameEmptyException : Exception
    {
        public VariableNameEmptyException() : base("Variable name is null or empty. Please provide a variable name") { }
    }
}