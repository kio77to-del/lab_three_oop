using System.Collections.Generic;

namespace Lab3Part1_Circles;

public class CircleStorage
{
    private List<CCircle> circles = new List<CCircle>();

    public void Add(CCircle circle)
    {
        circles.Add(circle);
    }

    public List<CCircle> GetAll()
    {
        return circles;
    }

    public void ClearSelection()
    {
        foreach (var circle in circles)
        {
            circle.IsSelected = false;
        }
    }

    public void RemoveSelected()
    {
        circles.RemoveAll(circle => circle.IsSelected);
    }

    public CCircle? FindCircleAt(double x, double y)
    {
        for (int i = circles.Count - 1; i >= 0; i--)
        {
            if (circles[i].ContainsPoint(x, y))
            {
                return circles[i];
            }
        }

        return null;
    }
}