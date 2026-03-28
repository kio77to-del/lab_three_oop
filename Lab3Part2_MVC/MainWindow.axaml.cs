using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Lab3Part2_MVC;

public partial class MainWindow : Window
{
    private NumberModel model = new NumberModel();
    private bool isUpdating = false;
    private int updateCount = 0;

    public MainWindow()
    {
        InitializeComponent();

        model.ModelChanged += OnModelChanged;

        BindControls();
        UpdateFromModel();
        UpdateStatus("Статус: значения загружены из модели");
    }

    private void BindControls()
    {
        var aTextBox = this.FindControl<TextBox>("ATextBox");
        var bTextBox = this.FindControl<TextBox>("BTextBox");
        var cTextBox = this.FindControl<TextBox>("CTextBox");

        var aNumeric = this.FindControl<NumericUpDown>("ANumeric");
        var bNumeric = this.FindControl<NumericUpDown>("BNumeric");
        var cNumeric = this.FindControl<NumericUpDown>("CNumeric");

        var aSlider = this.FindControl<Slider>("ASlider");
        var bSlider = this.FindControl<Slider>("BSlider");
        var cSlider = this.FindControl<Slider>("CSlider");

        if (aTextBox != null) aTextBox.LostFocus += OnATextBoxLostFocus;
        if (bTextBox != null) bTextBox.LostFocus += OnBTextBoxLostFocus;
        if (cTextBox != null) cTextBox.LostFocus += OnCTextBoxLostFocus;

        if (aNumeric != null) aNumeric.ValueChanged += OnANumericChanged;
        if (bNumeric != null) bNumeric.ValueChanged += OnBNumericChanged;
        if (cNumeric != null) cNumeric.ValueChanged += OnCNumericChanged;

        if (aSlider != null) aSlider.PropertyChanged += OnASliderChanged;
        if (bSlider != null) bSlider.PropertyChanged += OnBSliderChanged;
        if (cSlider != null) cSlider.PropertyChanged += OnCSliderChanged;
    }

    private void OnModelChanged()
    {
        UpdateFromModel();
    }

    private void UpdateFromModel()
    {
        isUpdating = true;

        var aTextBox = this.FindControl<TextBox>("ATextBox");
        var bTextBox = this.FindControl<TextBox>("BTextBox");
        var cTextBox = this.FindControl<TextBox>("CTextBox");

        var aNumeric = this.FindControl<NumericUpDown>("ANumeric");
        var bNumeric = this.FindControl<NumericUpDown>("BNumeric");
        var cNumeric = this.FindControl<NumericUpDown>("CNumeric");

        var aSlider = this.FindControl<Slider>("ASlider");
        var bSlider = this.FindControl<Slider>("BSlider");
        var cSlider = this.FindControl<Slider>("CSlider");

        if (aTextBox != null) aTextBox.Text = model.A.ToString();
        if (bTextBox != null) bTextBox.Text = model.B.ToString();
        if (cTextBox != null) cTextBox.Text = model.C.ToString();

        if (aNumeric != null) aNumeric.Value = model.A;
        if (bNumeric != null) bNumeric.Value = model.B;
        if (cNumeric != null) cNumeric.Value = model.C;

        if (aSlider != null) aSlider.Value = model.A;
        if (bSlider != null) bSlider.Value = model.B;
        if (cSlider != null) cSlider.Value = model.C;

        updateCount++;
        UpdateStatus($"Статус: модель обновила форму ({updateCount})");

        isUpdating = false;
    }

    private void OnATextBoxLostFocus(object? sender, RoutedEventArgs e)
    {
        if (isUpdating) return;

        var textBox = this.FindControl<TextBox>("ATextBox");
        if (textBox == null) return;

        if (int.TryParse(textBox.Text, out int value))
        {
            model.SetA(value);
        }
        else
        {
            UpdateFromModel();
            UpdateStatus("Статус: для A введено неверное значение");
        }
    }

    private void OnBTextBoxLostFocus(object? sender, RoutedEventArgs e)
    {
        if (isUpdating) return;

        var textBox = this.FindControl<TextBox>("BTextBox");
        if (textBox == null) return;

        if (int.TryParse(textBox.Text, out int value))
        {
            model.SetB(value);
            UpdateFromModel();
        }
        else
        {
            UpdateFromModel();
            UpdateStatus("Статус: для B введено неверное значение");
        }
    }

    private void OnCTextBoxLostFocus(object? sender, RoutedEventArgs e)
    {
        if (isUpdating) return;

        var textBox = this.FindControl<TextBox>("CTextBox");
        if (textBox == null) return;

        if (int.TryParse(textBox.Text, out int value))
        {
            model.SetC(value);
        }
        else
        {
            UpdateFromModel();
            UpdateStatus("Статус: для C введено неверное значение");
        }
    }

    private void OnANumericChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (isUpdating || e.NewValue == null) return;
        model.SetA((int)e.NewValue.Value);
    }

    private void OnBNumericChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (isUpdating || e.NewValue == null) return;
        model.SetB((int)e.NewValue.Value);
        UpdateFromModel();
    }

    private void OnCNumericChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (isUpdating || e.NewValue == null) return;
        model.SetC((int)e.NewValue.Value);
    }

    private void OnASliderChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (isUpdating) return;
        if (e.Property.Name != "Value") return;

        var slider = this.FindControl<Slider>("ASlider");
        if (slider == null) return;

        model.SetA((int)Math.Round(slider.Value));
    }

    private void OnBSliderChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (isUpdating) return;
        if (e.Property.Name != "Value") return;

        var slider = this.FindControl<Slider>("BSlider");
        if (slider == null) return;

        model.SetB((int)Math.Round(slider.Value));
        UpdateFromModel();
    }

    private void OnCSliderChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (isUpdating) return;
        if (e.Property.Name != "Value") return;

        var slider = this.FindControl<Slider>("CSlider");
        if (slider == null) return;

        model.SetC((int)Math.Round(slider.Value));
    }

    private void OnResetClick(object? sender, RoutedEventArgs e)
    {
        model.Reset();
        UpdateStatus("Статус: значения сброшены");
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        model.Save();
        UpdateStatus("Статус: значения сохранены");
    }

    private void UpdateStatus(string text)
    {
        var statusText = this.FindControl<TextBlock>("StatusText");
        if (statusText != null)
        {
            statusText.Text = text;
        }
    }
}