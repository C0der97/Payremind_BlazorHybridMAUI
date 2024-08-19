namespace PayRemind.Contracts
{
    public interface IForegroundService
    {
        void StartForegroundService();
        void StopForegroundService();
    }
}
