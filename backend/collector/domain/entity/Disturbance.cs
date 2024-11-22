using collector.domain.vo;

namespace collector.domain.entity;

public class Disturbance
{
    public UUID DisturbanceId { get; private set; }
    public DateTime CollectorDateTime { get; private set; }
    public DateTime TriggerDateTime { get; private set; }
    public UUID IedId { get; private set; }

    public Disturbance(string disturbanceId, DateTime triggerDateTime, string iedId)
    {
        DisturbanceId = new UUID(disturbanceId);
        CollectorDateTime = DateTime.Now;
        TriggerDateTime = triggerDateTime;
        IedId = new UUID(iedId);
    }

    public static Disturbance Create(DateTime triggerDateTime, string iedId)
    {
        var uuid = UUID.Create();
        return new Disturbance(uuid.GetValue(), triggerDateTime, iedId);
    }

    public void SetTriggerDateTime(DateTime datetime)
    {
        TriggerDateTime = datetime;
    }
}