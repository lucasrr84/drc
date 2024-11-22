
namespace collector.domain.vo;

public class TcpPort
{
    private readonly int _value;

    public TcpPort(int? value)
    {
        if (value == null) _value = 102;
    }

    public int GetValue()
    {
        return _value;
    }

}
