using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using repetition_of_English_words;

public sealed class StartForm : FormStruct
{
    private Label WordOrTextLabel;
    private TextBox WordOrTextTextBox;
    private Label TranslationLabel;
    private TextBox TranslationTextBox;
    private Button CheckButton;
    
    public string WordOrText { get { return WordOrTextTextBox.Text.ToLower(); } }
    public string Translation { get { return TranslationTextBox.Text.ToLower(); } }
    public WordsAndTexts.Type Type { get; private set; }

    public StartForm(Form1 form, Button activate_button, EventHandler back_main_form) : base(form, activate_button, back_main_form)
    {
        activate_button.Click += SetVisible;
        BackButton.Click += BackToMainForm;

        WordOrTextLabel = new Label();
        WordOrTextLabel.Text = "Слово";
        WordOrTextLabel.Font = text_font;
        WordOrTextLabel.Location = new Point(161, 132);
        WordOrTextLabel.Visible = false;
        form.Controls.Add(WordOrTextLabel);

        WordOrTextTextBox = new TextBox();
        WordOrTextTextBox.Font = text_font;
        WordOrTextTextBox.Width = 550;
        WordOrTextTextBox.Location = new Point(167, 162);
        WordOrTextTextBox.Visible = false;
        WordOrTextTextBox.Enabled = false;
        form.Controls.Add(WordOrTextTextBox);

        TranslationLabel = new Label();
        TranslationLabel.Text = "Перевод";
        TranslationLabel.Font = text_font;
        TranslationLabel.Location = new Point(161, 284);
        TranslationLabel.Visible = false;
        form.Controls.Add(TranslationLabel);

        TranslationTextBox = new TextBox();
        TranslationTextBox.Font = text_font;
        TranslationTextBox.Width = 550;
        TranslationTextBox.Location = new Point(167, 314);
        TranslationTextBox.Visible = false;
        form.Controls.Add(TranslationTextBox);

        CheckButton = new Button();
        CheckButton.Font = button_font;
        CheckButton.Text = "Проверить";
        CheckButton.Visible = false;
        CheckButton.Width = 150;
        CheckButton.Height = 50;
        CheckButton.Location = new Point(567, 511);
        form.Controls.Add(CheckButton);
    }

    private void SetVisible(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = true;
        WordOrTextTextBox.Visible = true;
        TranslationLabel.Visible = true;
        TranslationTextBox.Visible = true;
        CheckButton.Visible = true;
    }

    private void BackToMainForm(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = false;
        WordOrTextTextBox.Visible = false;
        CheckButton.Visible = false;
        TranslationLabel.Visible = false;
        TranslationTextBox.Visible = false;
        WordOrTextTextBox.Text = "";
        TranslationTextBox.Text = "";
    }

    public void CheckEvent(EventHandler e) => CheckButton.Click += e;

    public void SetWordOrText(KeyValuePair<string, WordsAndTexts.Type> wt)
    {
        WordOrTextTextBox.Text = wt.Key;
        Type = wt.Value;
        if (Type == WordsAndTexts.Type.Word) WordOrTextLabel.Text = "Слово";
        else WordOrTextLabel.Text = "Текст";
        TranslationTextBox.Text = "";
    }
}
