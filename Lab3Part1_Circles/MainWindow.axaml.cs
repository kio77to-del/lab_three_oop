using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Lab3Part1_Circles;

public partial class MainWindow : Window
{
    private CircleStorage storage = new CircleStorage();

    public MainWindow()
    {
        InitializeComponent();

        var drawArea = this.FindControl<DrawingArea>("DrawArea");
        drawArea.Storage = storage;

        this.KeyDown += OnKeyDown;
        UpdateStatus();
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var drawArea = this.FindControl<DrawingArea>("DrawArea");
        var point = e.GetPosition(drawArea);

        bool ctrlPressed = e.KeyModifiers.HasFlag(KeyModifiers.Control);

        if (ctrlPressed)
        {
            var clickedCircles = storage.FindAllCirclesAt(point.X, point.Y);

            if (clickedCircles.Count > 0)
            {
                foreach (var circle in clickedCircles)
                {
                    circle.IsSelected = true;
                }
            }
            else
            {
                storage.Add(new CCircle(point.X, point.Y));
            }
        }
        else
        {
            var clickedCircle = storage.FindCircleAt(point.X, point.Y);

            if (clickedCircle != null)
            {
                storage.ClearSelection();
                clickedCircle.IsSelected = true;
            }
            else
            {
                storage.ClearSelection();
                storage.Add(new CCircle(point.X, point.Y));
            }
        }

        drawArea.InvalidateVisual();
        UpdateStatus();
    }

    private void OnDeleteSelectedClick(object? sender, RoutedEventArgs e)
    {
        DeleteSelectedCircles();
    }

    private void OnClearAllClick(object? sender, RoutedEventArgs e)
    {
        storage = new CircleStorage();

        var drawArea = this.FindControl<DrawingArea>("DrawArea");
        drawArea.Storage = storage;
        drawArea.InvalidateVisual();

        UpdateStatus();
    }

    private void DeleteSelectedCircles()
    {
        storage.RemoveSelected();

        var drawArea = this.FindControl<DrawingArea>("DrawArea");
        drawArea.InvalidateVisual();

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        var statusText = this.FindControl<TextBlock>("StatusText");
        int total = storage.GetAll().Count;
        int selected = storage.GetAll().Count(c => c.IsSelected);

        statusText.Text = $"Кругов: {total} | Выделено: {selected}";
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            DeleteSelectedCircles();
        }
    }
}