namespace PayRemind.Pages.Custom
{
    public class ShowcaseSequence
    {
        private Queue<ShowcaseView> _showcases = new Queue<ShowcaseView>();

        public ShowcaseSequence AddShowcase(ShowcaseView showcase)
        {
            _showcases.Enqueue(showcase);
            return this;
        }

        public void Start()
        {
            ShowNext();
        }

        private void ShowNext()
        {
            if (_showcases.Any())
            {
                var showcase = _showcases.Dequeue();
                showcase.Show();
                showcase.Dismissed += (s, e) => ShowNext();
            }
        }
    }

}
