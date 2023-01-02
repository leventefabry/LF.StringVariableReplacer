using LF.StringVariableReplacer;
using LF.StringVariableReplacer.TestConsole;

var testString = "Hello {{Name}}, Today is {{DateTime}}. Welcome to {{Place}}.";
Console.WriteLine($"Original text: {testString}");

var testClass = new TestStringClass("Bob", "the Buckingham Palace");

var genericReplacedString = new GenericVariableReplacer<TestStringClass>(testString, testClass)
    .AddReplaceValue(x => x.Name)
    .AddReplaceValue("DateTime", x => x.DateNow.ToString("MM/dd/yyyy"))
    .AddReplaceValue(x => x.Place, "some Random Place")
    .Replace();

Console.WriteLine($"Replaced text: {genericReplacedString}");

var replacedString = new VariableReplacer(testString)
    .ReplaceThis("Name").With("Bob")
    .ReplaceThis("DateTime").With(DateTime.Now.ToShortDateString())
    .ReplaceThis("Place").With("the Buckingham Palace")
    .Replace();

Console.WriteLine($"Replaced text: {replacedString}");