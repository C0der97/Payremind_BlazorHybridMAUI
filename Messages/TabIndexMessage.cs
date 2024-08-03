namespace PayRemind.Messages
{
    public class TabIndexMessage
    {
        public int TabIndex { get; }
        public string PhoneNumber { get; }

        public bool CloseCallPage { get; }

        public bool DefaultApp {  get; }


        public TabIndexMessage(int tabIndex, string phoneNumber)
        {
            TabIndex = tabIndex;
            PhoneNumber = phoneNumber;
        }

        public TabIndexMessage(bool closeCallPage)
        {
            CloseCallPage = closeCallPage;
        }

        public TabIndexMessage(bool defaultApp, string default_app)
        {
            DefaultApp = defaultApp;
        }
    }
}
