﻿using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls.Shapes;

namespace PayRemind.Pages.Custom
{
    public class ShowcaseView : ContentView
    {
        private AbsoluteLayout _layout;
        private BoxView _backgroundBox;
        private Border _highlightBorder;
        private ObservableCollection<ShowcaseItem> _showcaseItems;
        private int _currentIndex = 0;
        private CancellationTokenSource _autoAdvanceCts;

        public event EventHandler Dismissed;

        public ShowcaseView()
        {
            _showcaseItems = new ObservableCollection<ShowcaseItem>();
            SetupShowcase();
        }

        public void AddShowcaseItem(View targetView, string message)
        {
            _showcaseItems.Add(new ShowcaseItem(targetView, message));
        }

        private void SetupShowcase()
        {
            _layout = new AbsoluteLayout();

            _backgroundBox = new BoxView
            {
                Color = Colors.Black.WithAlpha(0.1f),
                InputTransparent = true
            };
            AbsoluteLayout.SetLayoutBounds(_backgroundBox, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(_backgroundBox, AbsoluteLayoutFlags.All);

            _highlightBorder = new Border
            {
                Stroke = Colors.Yellow,
                StrokeThickness = 2,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(8)
                },
                BackgroundColor = Colors.Transparent,
                InputTransparent = true,
                Opacity = 0
            };

            _layout.Children.Add(_backgroundBox);
            _layout.Children.Add(_highlightBorder);

            Content = _layout;

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += OnTapped;
            _layout.GestureRecognizers.Add(tapGesture);
        }

        private void OnTapped(object sender, EventArgs e)
        {
            _autoAdvanceCts?.Cancel();
            AdvanceShowcase();
        }

        public async Task Show()
        {
            if (_showcaseItems.Count == 0) return;

            this.IsVisible = true;
            _currentIndex = 0;
            await ShowCurrentItem();
        }

        private async Task ShowCurrentItem()
        {
            var item = _showcaseItems[_currentIndex];
            var targetPosition = GetAbsolutePosition(item.TargetView);

            System.Diagnostics.Debug.WriteLine($"Showing message: {item.Message}");

            _highlightBorder.Opacity = 0;

            double padding = 8; // Padding around the target view
            AbsoluteLayout.SetLayoutBounds(_highlightBorder, new Rect(
                targetPosition.X - padding,
                targetPosition.Y - padding,
                item.TargetView.Width + (padding * 2),
                item.TargetView.Height + (padding * 2)));

            await AnimateOpacity(_highlightBorder, 0, 1, 300);

            _autoAdvanceCts = new CancellationTokenSource();

            ISnackbar snackbar = Snackbar.Make(item.Message,
                action: () =>
                {
                    _autoAdvanceCts.Cancel(); // Cancel the auto-advance timer
                    AdvanceShowcase();
                },
                actionButtonText: "OK",
                duration: TimeSpan.FromSeconds(60),
                visualOptions: new SnackbarOptions
                {
                    BackgroundColor = Colors.DarkViolet,
                    TextColor = Colors.Yellow,
                    ActionButtonTextColor = Colors.Yellow
                });

            // Start the auto-advance timer
            _ = AutoAdvanceAfterDelay(_autoAdvanceCts.Token);

            await snackbar.Show();
        }

        private async Task AutoAdvanceAfterDelay(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(60), cancellationToken);
                if (!cancellationToken.IsCancellationRequested)
                {
                    MainThread.BeginInvokeOnMainThread(AdvanceShowcase);
                }
            }
            catch (TaskCanceledException)
            {
                // The task was cancelled, do nothing
            }
        }

        private void AdvanceShowcase()
        {
            if (_currentIndex < _showcaseItems.Count - 1)
            {
                _currentIndex++;
                MainThread.BeginInvokeOnMainThread(async () => await ShowCurrentItem());
            }
            else
            {
                Dismiss();
            }
        }

        private async Task AnimateOpacity(VisualElement view, double start, double end, uint length)
        {
            uint steps = 30;
            double stepValue = (end - start) / steps;
            uint delay = length / steps;

            for (int i = 0; i <= steps; i++)
            {
                view.Opacity = start + (stepValue * i);
                await Task.Delay((int)delay);
            }
        }

        public void Dismiss()
        {
            this.IsVisible = false;
            Dismissed?.Invoke(this, EventArgs.Empty);
        }

        private Point GetAbsolutePosition(View view)
        {
            var element = view;
            var x = element.X;
            var y = element.Y;

            while (element.Parent != null)
            {
                if (element.Parent is View parentView)
                {
                    x += parentView.X;
                    y += parentView.Y;
                    element = parentView;
                }
                else
                {
                    break;
                }
            }

            return new Point(x, y);
        }
    }

    public class ShowcaseItem
    {
        public View TargetView { get; set; }
        public string Message { get; set; }

        public ShowcaseItem(View targetView, string message)
        {
            TargetView = targetView;
            Message = message;
        }
    }
}