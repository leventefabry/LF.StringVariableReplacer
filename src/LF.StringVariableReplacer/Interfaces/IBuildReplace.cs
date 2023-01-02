namespace LF.StringVariableReplacer.Interfaces
{
    public interface IBuildReplace
    {
        /// <summary>
        /// Replaces all variables in the given string.
        /// </summary>
        /// <returns>The processed string</returns>
        string Replace();
    }
}