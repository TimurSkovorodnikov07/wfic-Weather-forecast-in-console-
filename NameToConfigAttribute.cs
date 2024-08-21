public class NameToConfigAttribute : Attribute
{
    public NameToConfigAttribute(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
}