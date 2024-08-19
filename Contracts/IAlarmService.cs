namespace PayRemind.Contracts
{
    public interface IAlarmService
    {
        void SetAlarm(DateTime alarmTime, string name_reminder, string tittle);
    }
}
