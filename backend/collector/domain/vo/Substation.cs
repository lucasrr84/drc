
namespace collector.domain.vo;

public class Substation
{
    private readonly string _value;

    public Substation(string value)
    {
        _value = value;
    }

    public string GetValue()
    {
        return _value;
    }
}
