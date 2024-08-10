namespace PayRemind.Messages
{
    public class HideFloatButton
    {

        public bool _HideFloatButton { get; set; } = false;

        public HideFloatButton(bool hideFloatButton)
        {
            _HideFloatButton = hideFloatButton;
        }
    }
}
