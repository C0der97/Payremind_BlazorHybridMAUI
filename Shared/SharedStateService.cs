namespace PayRemind.Shared
{
    public class SharedStateService
    {
        public event Func<Task> OnChange;

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
