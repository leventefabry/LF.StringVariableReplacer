namespace LF.StringVariableReplacer.Interfaces
{
    public interface IReplaceThis : IBuildReplace
    {
        /// <summary>
        /// Defines a variable name
        /// </summary>
        /// <param name="variableName">The name of the variable</param>
        /// <returns><see cref="IReplaceWith"/></returns>
        IReplaceWith ReplaceThis(string variableName);
    }
}