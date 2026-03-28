using System;
using System.IO;

namespace Lab3Part2_MVC;

public class NumberModel
{
    private int a;
    private int b;
    private int c;

    private readonly string filePath = "numbers.txt";

    public event Action? ModelChanged;

    public int A => a;
    public int B => b;
    public int C => c;

    public int MinValue => 0;
    public int MaxValue => 100;

    public NumberModel()
    {
        a = 20;
        b = 50;
        c = 80;

        Load();
    }

    public void SetA(int value)
    {
        value = Clamp(value);

        int newA = value;
        int newB = b;
        int newC = c;

        if (newA > newB)
            newB = newA;

        if (newB > newC)
            newC = newB;

        ApplyValues(newA, newB, newC);
    }

    public void SetB(int value)
    {
        value = Clamp(value);

        if (value < a || value > c)
            return;

        ApplyValues(a, value, c);
    }

    public void SetC(int value)
    {
        value = Clamp(value);

        int newA = a;
        int newB = b;
        int newC = value;

        if (newC < newB)
            newB = newC;

        if (newB < newA)
            newA = newB;

        ApplyValues(newA, newB, newC);
    }

    public void Reset()
    {
        ApplyValues(20, 50, 80);
    }

    private int Clamp(int value)
    {
        if (value < MinValue)
            return MinValue;

        if (value > MaxValue)
            return MaxValue;

        return value;
    }

    private void ApplyValues(int newA, int newB, int newC)
    {
        if (a == newA && b == newB && c == newC)
            return;

        a = newA;
        b = newB;
        c = newC;

        Save();
        ModelChanged?.Invoke();
    }

    public void Load()
    {
        if (!File.Exists(filePath))
            return;

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length < 3)
                return;

            int loadedA = Clamp(int.Parse(lines[0]));
            int loadedB = Clamp(int.Parse(lines[1]));
            int loadedC = Clamp(int.Parse(lines[2]));

            if (loadedA > loadedB)
                loadedB = loadedA;

            if (loadedB > loadedC)
                loadedC = loadedB;

            a = loadedA;
            b = loadedB;
            c = loadedC;
        }
        catch
        {
        }
    }

    public void Save()
    {
        try
        {
            File.WriteAllLines(filePath, new string[]
            {
                a.ToString(),
                b.ToString(),
                c.ToString()
            });
        }
        catch
        {
        }
    }
}