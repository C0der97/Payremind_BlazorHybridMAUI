namespace PayRemind.Messages
{
    public class TabIndexMessage
    {
        public int TabIndex { get; }
        public string PhoneNumber { get; }


        public TabIndexMessage(int tabIndex, string phoneNumber)
        {
            TabIndex = tabIndex;
            PhoneNumber = phoneNumber;
        }
    }
}
