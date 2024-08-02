namespace PayRemind.Contracts
{
    public interface ICallHandler
    {
        void AnswerCall();
        void RejectCall();
    }
}
