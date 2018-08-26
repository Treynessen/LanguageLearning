using System;
using System.Windows.Forms;
using System.Drawing;

// Общая структура для остальных форм
public abstract class FormStruct
{
    public event Action BackToMainFormEvent;

    protected Button BackToMainFormButton;
    protected Font button_font;
    protected Font text_font;

    protected FormStruct(Form1 form)
    {
        button_font = new Font("Consolas", 18, FontStyle.Regular);
        text_font = new Font("Consolas", 16, FontStyle.Regular);
        
        BackToMainFormButton = new Button();
        BackToMainFormButton.Text = "Назад";
        BackToMainFormButton.Visible = false;
        BackToMainFormButton.Width = 150;
        BackToMainFormButton.Height = 50;
        BackToMainFormButton.Location = new Point(725, 511);
        BackToMainFormButton.Font = button_font;
        BackToMainFormButton.Click += (sender, e) => BackToMainFormEvent();
        form.Controls.Add(BackToMainFormButton);
    }
}
