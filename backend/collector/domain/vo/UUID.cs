
namespace collector.domain.vo;

public class UUID
{
    private readonly string _value;

    public UUID(string value)
    {
        _value = value;
    }

    public static UUID Create()
    {
        var uuid = Guid.NewGuid().ToString();
        return new UUID(uuid);
    }

    public string GetValue()
    {
        return _value;
    }
}
