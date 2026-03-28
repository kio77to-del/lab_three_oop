using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Lab3Part1_Circles;

public class CCircle
{
    private double x;
    private double y;
    private const double radius = 30;

    public bool IsSelected { get; set; }

    public CCircle(double x, double y)
    {
        this.x = x;
        this.y = y;
        IsSelected = false;
    }

    public void Draw(DrawingContext context)
    {
        var center = new Point(x, y);

        context.DrawEllipse(
            Brushes.LightBlue,
            new Pen(IsSelected ? Brushes.Red : Brushes.DarkBlue, IsSelected ? 3 : 2),
            center,
            radius,
            radius
        );
    }

    public bool ContainsPoint(double px, double py)
    {
        double dx = px - x;
        double dy = py - y;
        return dx * dx + dy * dy <= radius * radius;
    }
}