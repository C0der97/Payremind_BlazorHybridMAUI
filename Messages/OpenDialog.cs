namespace PayRemind.Messages
{
    public class OpenDialog
    {
        public bool _OpenDialog { get; set; } = false;

        public OpenDialog(bool openDialog)
        {
            _OpenDialog = openDialog;
        }
    }
}
