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

    protected static string error = "Ошибка";
    protected static string word_label = "Слово";
    protected static string words_label = "Слова";
    protected static string text_label = "Предложение";
    protected static string texts_label = "Предложения";
    protected static string translation_label = "Перевод";
    protected static string field_dont_have_translation = "Поле не содержит перевод";
    protected static string fields_dont_have_translation = "Поля не содержит перевод";

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
