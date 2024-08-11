namespace PayRemind.Contracts
{
    public interface IViewConverterService
    {
        Android.Views.View GetNativeView(Microsoft.Maui.Controls.View view);
    }
}
