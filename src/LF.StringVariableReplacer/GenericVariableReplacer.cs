using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LF.StringVariableReplacer.Exceptions;

namespace LF.StringVariableReplacer
{
    public class GenericVariableReplacer<T> where T : class
    {
        #region Fields

        private readonly Dictionary<string, string> _replaceValues = new Dictionary<string, string>();
        private readonly T _obj;
        private string _stringToReplace;

        private readonly string _openingChars = "{{";
        private readonly string _closingChars = "}}";

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericVariableReplacer{T}"/> class.
        /// The default opening characters are "{{" and the closing are "}}".
        /// </summary>
        /// <param name="stringToReplace">String to replace</param>
        /// <param name="obj">The object that holds the values or variable names</param>
        public GenericVariableReplacer(string stringToReplace, T obj)
        {
            _stringToReplace = stringToReplace;
            _obj = obj;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericVariableReplacer{T}"/> class.
        /// </summary>
        /// <param name="stringToReplace">String to replace</param>
        /// <param name="obj">The object that holds the values or variable names</param>
        /// <param name="openingChars">The opening characters</param>
        /// <param name="closingChars">The closing characters</param>
        public GenericVariableReplacer(string stringToReplace, T obj, string openingChars, string closingChars) : this(stringToReplace, obj)
        {
            _openingChars = openingChars;
            _closingChars = closingChars;
        }

        #endregion

        #region AddReplaceValue

        /// <summary>
        /// Adds replace value. The value and variable name will be bind from the expression.
        /// </summary>
        /// <param name="expression">The expression based on the given class</param>
        /// <returns>The instance of <see cref="GenericVariableReplacer{T}"/> class</returns>
        /// <exception cref="ReplaceWithException">Throws if can't get the variable name or value</exception>
        public GenericVariableReplacer<T> AddReplaceValue(Expression<Func<T, string>> expression)
        {
            try
            {
                var variableName = GetCorrectPropertyName(expression);
                var value = GetValueFromExpression(expression);

                _replaceValues.Add(variableName, value);
                return this;
            }
            catch (Exception)
            {
                throw new ReplaceWithException();
            }
        }

        /// <summary>
        /// Adds replace value. The variable name will be bind from the expression.
        /// </summary>
        /// <param name="variableNameExpression">The expression based on the given class</param>
        /// <param name="value">The manually given value</param>
        /// <returns>The instance of <see cref="GenericVariableReplacer{T}"/> class</returns>
        /// <exception cref="ReplaceWithException">Throws if can't get the variable name or value</exception>
        public GenericVariableReplacer<T> AddReplaceValue(Expression<Func<T, string>> variableNameExpression, string value)
        {
            try
            {
                var variableName = GetCorrectPropertyName(variableNameExpression);
                _replaceValues.Add(variableName, value);
                return this;
            }
            catch (Exception)
            {
                throw new ReplaceWithException();
            }
        }

        /// <summary>
        /// Adds replace value. The value will be bind from the expression.
        /// </summary>
        /// <param name="variableName">The manually given variable name</param>
        /// <param name="valueExpression">The expression based on the given class</param>
        /// <returns>The instance of <see cref="GenericVariableReplacer{T}"/> class</returns>
        /// <exception cref="ReplaceWithException">Throws if can't get the variable name or value</exception>
        public GenericVariableReplacer<T> AddReplaceValue(string variableName, Expression<Func<T, string>> valueExpression)
        {
            try
            {
                var value = GetValueFromExpression(valueExpression);
                _replaceValues.Add(variableName, value);

                return this;
            }
            catch (Exception)
            {
                throw new ReplaceWithException();
            }
        }

        #endregion

        /// <summary>
        /// Replaces all variables in the given string.
        /// </summary>
        /// <returns>The processed string</returns>
        public string Replace()
        {
            foreach (var replaceValue in _replaceValues)
            {
                _stringToReplace = _stringToReplace.Replace($"{_openingChars}{replaceValue.Key}{_closingChars}", replaceValue.Value);
            }

            return _stringToReplace;
        }

        private string GetValueFromExpression(Expression<Func<T, string>> expression)
        {
            var compiledFunction = expression.Compile();
            return compiledFunction.Invoke(_obj);
        }

        private string GetCorrectPropertyName(Expression<Func<T, string>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            var op = ((UnaryExpression)expression.Body).Operand;
            return ((MemberExpression)op).Member.Name;
        }
    }
}