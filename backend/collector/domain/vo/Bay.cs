
namespace collector.domain.vo;

public class Bay
{
    private readonly string _value;

    public Bay(string value)
    {
        _value = value;
    }

    public string GetValue()
    {
        return _value;
    }
}
