using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;

namespace Lab3Part1_Circles;

public partial class MainWindow : Window
{
    private CircleStorage storage = new CircleStorage();

    public MainWindow()
    {
        InitializeComponent();

        var drawArea = this.FindControl<DrawingArea>("DrawArea");
        if (drawArea != null)
        {
            drawArea.Storage = storage;
        }

        UpdateStatus();
        Focus();
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var drawArea = this.FindControl<DrawingArea>("DrawArea");
        if (drawArea == null)
            return;

        var position = e.GetPosition(drawArea);
        bool ctrlPressed = e.KeyModifiers.HasFlag(KeyModifiers.Control);

        var clickedCircle = storage.FindCircleAt(position.X, position.Y);

        if (clickedCircle == null)
        {
            if (!ctrlPressed)
            {
                storage.ClearSelection();
            }

            storage.Add(new CCircle(position.X, position.Y));
        }
        else
        {
            if (!ctrlPressed)
            {
                storage.ClearSelection();
                clickedCircle.IsSelected = true;
            }
            else
            {
                clickedCircle.IsSelected = !clickedCircle.IsSelected;
            }
        }

        drawArea.InvalidateVisual();
        UpdateStatus();
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            DeleteSelectedCircles();
        }
    }

    private void OnDeleteSelectedClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        DeleteSelectedCircles();
    }

    private void OnClearAllClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        storage = new CircleStorage();

        var drawArea = this.FindControl<DrawingArea>("DrawArea");
        if (drawArea != null)
        {
            drawArea.Storage = storage;
            drawArea.InvalidateVisual();
        }

        UpdateStatus();
    }

    private void DeleteSelectedCircles()
    {
        storage.RemoveSelected();

        var drawArea = this.FindControl<DrawingArea>("DrawArea");
        drawArea?.InvalidateVisual();

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        var statusText = this.FindControl<TextBlock>("StatusText");
        if (statusText == null)
            return;

        int totalCount = storage.GetAll().Count;
        int selectedCount = storage.GetAll().Count(circle => circle.IsSelected);

        statusText.Text = $"Кругов: {totalCount} | Выделено: {selectedCount}";
    }
}