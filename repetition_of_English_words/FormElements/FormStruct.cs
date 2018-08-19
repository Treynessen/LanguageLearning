using System;
using System.Windows.Forms;
using System.Drawing;
using repetition_of_English_words;

public abstract class FormStruct
{
    protected Button BackButton;
    protected Font button_font;
    protected Font text_font;
    protected FormStruct(Form1 form, Button activate_button)
    {
        button_font = new Font("Consolas", 18, FontStyle.Regular);
        text_font = new Font("Consolas", 16, FontStyle.Regular);
        BackButton = new Button();
        BackButton.Text = "Назад";
        BackButton.Visible = true;
        BackButton.Width = 150;
        BackButton.Height = 50;
        BackButton.Location = new Point(725, 511);
        BackButton.Font = button_font;
        BackButton.Click += form.BackToMainForm;
        BackButton.Click += BackToMainForm;
        activate_button.Click += SetVisible;
        form.Controls.Add(BackButton);
    }
    private void BackToMainForm(object sender, EventArgs e)
    {
        BackButton.Visible = false;
    }

    private void SetVisible(object sender, EventArgs e)
    {
        BackButton.Visible = true;
    }

    public enum Form
    {
        AddTextForm,
        AddWordForm
    }
}
