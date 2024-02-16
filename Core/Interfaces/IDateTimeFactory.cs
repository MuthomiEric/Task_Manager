namespace Core.Interfaces
{
    public interface IDateTimeFactory
    {
        DateTime Now();
        DateTime UtcToLocal(DateTime utcTime);
        DateTime? UtcToLocal(DateTime? utcTime);
        DateTime LocalToUtc(DateTime localTime);
        DateTime? LocalToUtc(DateTime? localTime);
    }
}
