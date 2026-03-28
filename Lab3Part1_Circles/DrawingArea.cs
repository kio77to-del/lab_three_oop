using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Lab3Part1_Circles;

public class DrawingArea : Control
{
    public CircleStorage? Storage { get; set; }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        context.FillRectangle(Brushes.White, new Rect(Bounds.Size));

        if (Storage == null)
            return;

        foreach (var circle in Storage.GetAll())
        {
            circle.Draw(context);
        }
    }
}