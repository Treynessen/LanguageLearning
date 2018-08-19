using System;
using System.Windows.Forms;
using System.Drawing;
using repetition_of_English_words;

public sealed class FormAddWordsOrText : FormStruct
{
    private Label WordOrTextLabel;
    private TextBox WordOrTextTextBox;
    private Label TranslationLabel;
    private TextBox TranslationTextBox;
    private Button AddTranslation;

    public FormAddWordsOrText(Form1 form, Button activate_button, Form f) : base(form, activate_button)
    {
        activate_button.Click += SetVisible;

        WordOrTextLabel = new Label();
        if (f == Form.AddWordForm) WordOrTextLabel.Text = "Слово";
        else if (f == Form.AddTextForm) WordOrTextLabel.Text = "Текст";
        WordOrTextLabel.Font = text_font;
        WordOrTextLabel.Location = new Point(4, 13);
        WordOrTextLabel.Visible = false;
        form.Controls.Add(WordOrTextLabel);

        WordOrTextTextBox = new TextBox();
        WordOrTextTextBox.Font = text_font;
        WordOrTextTextBox.Width = 698;
        WordOrTextTextBox.Location = new Point(120, 10);
        WordOrTextTextBox.Visible = false;
        form.Controls.Add(WordOrTextTextBox);

        TranslationLabel = new Label();
        TranslationLabel.Text = "Перевод";
        TranslationLabel.Font = text_font;
        TranslationLabel.Location = new Point(4, 83);
        TranslationLabel.Visible = false;
        form.Controls.Add(TranslationLabel);

        TranslationTextBox = new TextBox();
        TranslationTextBox.Font = text_font;
        TranslationTextBox.Width = 698;
        TranslationTextBox.Location = new Point(120, 80);
        TranslationTextBox.Visible = false;
        form.Controls.Add(TranslationTextBox);

        AddTranslation = new Button();
        AddTranslation.Font = text_font;
        AddTranslation.Text = "+";
        AddTranslation.Width = 34;
        AddTranslation.Height = 34;
        AddTranslation.Visible = false;
        AddTranslation.Location = new Point(834, 79);
        form.Controls.Add(AddTranslation);

        BackButton.Visible = false;
        BackButton.Click += BackToMainForm;
    }

    private void BackToMainForm(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = false;
        TranslationLabel.Visible = false;
        WordOrTextTextBox.Visible = false;
        WordOrTextTextBox.Text = "";
        TranslationTextBox.Visible = false;
        TranslationTextBox.Text = "";
        AddTranslation.Visible = false;
    }

    private void SetVisible(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = true;
        TranslationLabel.Visible = true;
        WordOrTextTextBox.Visible = true;
        TranslationTextBox.Visible = true;
        AddTranslation.Visible = true;
    }
}