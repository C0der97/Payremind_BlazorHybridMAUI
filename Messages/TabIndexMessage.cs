using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PayRemind.Messages
{
    public class TabIndexMessage : ValueChangedMessage<int>
    {
        public int TabIndex { get; }

        public TabIndexMessage(int tabIndex) : base(tabIndex)
        {
            TabIndex = tabIndex;
        }
    }
}
