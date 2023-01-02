namespace LF.StringVariableReplacer.Interfaces
{
    public interface IReplaceWith
    {
        /// <summary>
        /// Defines the value
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns><see cref="IReplaceThis"/></returns>
        IReplaceThis With(string value);
    }
}