namespace PayRemind.Pages;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System.Collections.Generic;

public partial class Guided : ContentPage
{


    private List<View> steps;
    private int currentStep = 0;
    private Frame tooltipFrame;


    public Guided()
	{
		InitializeComponent();
    }



    private void SetupGuide()
    {

        ToolTipProperties.SetText(button1, "Click to Save your data");


        steps = new List<View>
            {
                button1,
                entry1,
                label1
                // Add more controls as needed
            };

        tooltipFrame = new Frame
        {
            BackgroundColor = Colors.Black,
            Opacity = 0.8,
            Padding = new Thickness(10),
            HasShadow = true,
            IsVisible = false
        };

        var tooltipLabel = new Label
        {
            TextColor = Colors.White,
            FontSize = 16
        };

        var nextButton = new Button
        {
            Text = "Next",
            BackgroundColor = Colors.White,
            TextColor = Colors.Black
        };
        nextButton.Clicked += OnNextClicked;

        var tooltipStack = new StackLayout
        {
            Children = { tooltipLabel, nextButton }
        };

        tooltipFrame.Content = tooltipStack;

        // Asegurarse de que tooltipFrame no se añade más de una vez
        if (!absoluteLayout.Children.Contains(tooltipFrame))
        {
            AbsoluteLayout.SetLayoutFlags(tooltipFrame, AbsoluteLayoutFlags.PositionProportional);
            absoluteLayout.Children.Add(tooltipFrame);
        }

        StartGuide();
    }

    private void StartGuide()
    {
        ShowTooltip(0, "This is the first step of the guide.");
    }

    private void ShowTooltip(int stepIndex, string message)
    {
        if (stepIndex >= steps.Count) return;

        var targetElement = steps[stepIndex];
        var targetBounds = targetElement.Bounds;

        tooltipFrame.IsVisible = true;

        AbsoluteLayout.SetLayoutBounds(tooltipFrame, new Rect(
            targetBounds.Left,
            targetBounds.Bottom,
            AbsoluteLayout.AutoSize,
            AbsoluteLayout.AutoSize));

        ((Label)((StackLayout)tooltipFrame.Content).Children[0]).Text = message;
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        currentStep++;
        if (currentStep < steps.Count)
        {
            ShowTooltip(currentStep, $"This is step {currentStep + 1} of the guide.");
        }
        else
        {
            tooltipFrame.IsVisible = false;
            DisplayAlert("Tour Complete", "You've completed the guide!", "OK");
        }
    }

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    this.Loaded += (s, e) =>
    //    {
    //        // Espera un momento para asegurar que la UI está completamente cargada
    //        Dispatcher.Dispatch(() => SetupGuide());
    //    };
    //}

    private void OnStartGuideClicked(object sender, EventArgs e)
    {
        DisplayAlert("Guide Started", "The guide would start here.", "OK");

        SetupGuide();
    }
}