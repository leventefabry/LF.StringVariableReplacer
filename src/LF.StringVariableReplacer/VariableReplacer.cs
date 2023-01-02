using System.Collections.Generic;
using LF.StringVariableReplacer.Exceptions;
using LF.StringVariableReplacer.Interfaces;

namespace LF.StringVariableReplacer
{
    public class VariableReplacer : IReplaceThis, IReplaceWith
    {
        #region Fields

        private string _variableName;
        private string _stringToReplace;
        private readonly Dictionary<string, string> _replaceValues = new Dictionary<string, string>();

        private readonly string _openingChars = "{{";
        private readonly string _closingChars = "}}";

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableReplacer"/> class.
        /// The default opening characters are "{{" and the closing are "}}".
        /// </summary>
        /// <param name="stringToReplace">String to replace</param>
        public VariableReplacer(string stringToReplace)
        {
            _stringToReplace = stringToReplace;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableReplacer"/> class.
        /// </summary>
        /// <param name="stringToReplace">String to replace</param>
        /// <param name="openingChars">The opening characters</param>
        /// <param name="closingChars">The closing characters</param>
        public VariableReplacer(string stringToReplace, string openingChars, string closingChars) : this(stringToReplace)
        {
            _openingChars = openingChars;
            _closingChars = closingChars;
        }

        #endregion

        public IReplaceWith ReplaceThis(string variableName)
        {
            _variableName = variableName;
            return this;
        }

        public IReplaceThis With(string value)
        {
            if (string.IsNullOrEmpty(_variableName)) throw new VariableNameEmptyException();
            
            _replaceValues.Add(_variableName, value);
            return this;
        }
        
        public string Replace()
        {
            foreach (var replaceValue in _replaceValues)
            {
                _stringToReplace = _stringToReplace.Replace($"{_openingChars}{replaceValue.Key}{_closingChars}", replaceValue.Value);
            }

            return _stringToReplace;
        }
    }
}