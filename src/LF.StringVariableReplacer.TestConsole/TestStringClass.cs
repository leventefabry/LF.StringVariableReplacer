namespace LF.StringVariableReplacer.TestConsole;

public class TestStringClass
{
    public TestStringClass(string name, string place)
    {
        Name = name;
        Place = place;
    }
    
    public string Name { get; set; }
    public DateTime DateNow { get; set; } = DateTime.Now;
    public string Place { get; set; }
}