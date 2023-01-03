using LF.StringVariableReplacer.Exceptions;

namespace LF.StringVariableReplacer.UnitTest;

public class VariableReplacerTest
{
    private const string TestString = "Hello {{Name}}, Today is {{DateTime}}. Welcome to {{Place}}.";
    
    [Fact]
    public void VariableReplacer_ReplaceString_ShouldWork()
    {
        var exceptedString = "Hello Bob, Today is 2023-01-01. Welcome to the Market.";

        var name = "Bob";
        var dateTime = new DateTime(2023, 01, 01);
        var place = "the Market";

        var replacedString = new VariableReplacer(TestString)
            .ReplaceThis("Name").With(name)
            .ReplaceThis("DateTime").With(dateTime.ToShortDateString())
            .ReplaceThis("Place").With(place)
            .Replace();
        
        Assert.Equal(exceptedString, replacedString);
    }
    
    [Fact]
    public void VariableReplacer_Incomplete_ShouldWork()
    {
        var exceptedString = "Hello {{Name}}, Today is 2023-01-01. Welcome to the Market.";

        var dateTime = new DateTime(2023, 01, 01);
        var place = "the Market";

        var replacedString = new VariableReplacer(TestString)
            .ReplaceThis("DateTime").With(dateTime.ToShortDateString())
            .ReplaceThis("Place").With(place)
            .Replace();
        
        Assert.Equal(exceptedString, replacedString);
    }
    
    [Fact]
    public void VariableReplacer_BadVariableName_ShouldWork()
    {
        var exceptedString = "Hello {{Name}}, Today is 2023-01-01. Welcome to the Market.";

        var name = "Bob";
        var dateTime = new DateTime(2023, 01, 01);
        var place = "the Market";

        var replacedString = new VariableReplacer(TestString)
            .ReplaceThis("Nam").With(name)
            .ReplaceThis("DateTime").With(dateTime.ToShortDateString())
            .ReplaceThis("Place").With(place)
            .Replace();
        
        Assert.Equal(exceptedString, replacedString);
    }

    [Fact]
    public void VariableReplacer_JustReplace_ShouldWork()
    {
        var replacedString = new VariableReplacer(TestString).Replace();
        
        Assert.Equal(TestString, replacedString);
    }
    
    [Fact]
    public void VariableReplacer_CallTheSameVariableNameMultiple_ShouldWork()
    {
        var exceptedString = "Hello John, Today is 2023-01-01. Welcome to the Market.";

        var name = "Bob";
        var dateTime = new DateTime(2023, 01, 01);
        var place = "the Market";

        var replacer = new VariableReplacer(TestString)
            .ReplaceThis("Name").With(name)
            .ReplaceThis("DateTime").With(dateTime.ToShortDateString())
            .ReplaceThis("Place").With(place);

        var replacedString = replacer.ReplaceThis("Name").With("John").Replace();

        Assert.Equal(exceptedString, replacedString);
    }
    
    [Fact]
    public void VariableReplacer_EmptyVariableName_ShouldThrowVariableNameEmptyException()
    {
        var name = "Bob";
        var dateTime = new DateTime(2023, 01, 01);
        var place = "the Market";

        Assert.Throws<VariableNameEmptyException>(() =>
        {
            new VariableReplacer(TestString)
                .ReplaceThis(string.Empty).With(name)
                .ReplaceThis("DateTime").With(dateTime.ToShortDateString())
                .ReplaceThis("Place").With(place)
                .Replace();
        });
    }
    
    [Fact]
    public void VariableReplacer_StartWithWith_ShouldThrowVariableNameEmptyException()
    {
        Assert.Throws<VariableNameEmptyException>(() =>
        {
            new VariableReplacer(TestString).With("Bob").Replace();
        });
    }
}