using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using repetition_of_English_words;
using Pair = System.Collections.Generic.KeyValuePair<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.LinkedList<string>>, WordsAndTexts.Type>;

public sealed class StartForm : FormStruct
{
    private Label WordOrTextLabel;
    private TextBox WordOrTextTextBox;
    private Label TranslationLabel;
    private TextBox TranslationTextBox;
    private Button CheckButton;
    private Label TextLabel;
    private Button NextButton;

    private string word;
    private LinkedList<string> translation;

    public string WordOrText =>  WordOrTextTextBox.Text;
    public string Translation => TranslationTextBox.Text.ToLower(); 

    public WordType WordOrTranslation { get; private set; } // в качестве слова используется ключ или значение
    public WordsAndTexts.Type Type { get; private set; } // слово или текст

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
        CheckButton.Location = new Point(409, 511);
        form.Controls.Add(CheckButton);

        TextLabel = new Label();
        TextLabel.Font = text_font;
        TextLabel.Width = 300;
        TextLabel.Location = new Point(160, 390);
        TextLabel.Visible = false;
        form.Controls.Add(TextLabel);

        NextButton = new Button();
        NextButton.Font = button_font;
        NextButton.Text = "Далее";
        NextButton.Visible = false;
        NextButton.Width = 150;
        NextButton.Height = 50;
        NextButton.Location = new Point(567, 511);
        form.Controls.Add(NextButton);
    }

    private void SetVisible(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = true;
        WordOrTextTextBox.Visible = true;
        TranslationLabel.Visible = true;
        TranslationTextBox.Visible = true;
        CheckButton.Visible = true;
        TextLabel.Visible = true;
        NextButton.Visible = true;
    }

    private void BackToMainForm(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = false;
        WordOrTextTextBox.Visible = false;
        CheckButton.Visible = false;
        TranslationLabel.Visible = false;
        TranslationTextBox.Visible = false;
        TextLabel.Visible = false;
        NextButton.Visible = false;
        WordOrTextTextBox.Text = "";
        TranslationTextBox.Text = "";
        TextLabel.Text = "";
    }

    public void CheckEvent(EventHandler e) => CheckButton.Click += e;

    public void SetWordOrText(Pair wt)
    {
        word = wt.Key.Key;
        translation = wt.Key.Value;
        Type = wt.Value;
        if (Type == WordsAndTexts.Type.Word) WordOrTextLabel.Text = "Слово";
        else WordOrTextLabel.Text = "Текст";
        TranslationTextBox.Text = "";

        Random rand = new Random();
        int rand_value = rand.Next(0, 2); 
        if(rand_value == 0)
        {
            WordOrTextTextBox.Text = word;
            WordOrTranslation = WordType.Original;
        }
        else
        {
            rand_value = rand.Next(0, translation.Count);
            int count = 0;
            foreach(var t in translation)
            {
                if (count++ >= rand_value)
                {
                    WordOrTextTextBox.Text = t;
                    WordOrTranslation = WordType.Translation;
                    break;
                }
            }
        }
    }

    public bool Check()
    {
        if (TranslationTextBox.Text == "") return false;
        string str = TranslationTextBox.Text.ToLower();
        if (WordOrTranslation == WordType.Original)
        {
            foreach (var t in translation)
            {
                if (t == str) return true;
            }
        }
        else
        {
            if (word == str) return true;
        }
        return false;
    }

    public void SetText(bool right)
    {
        if (right) TextLabel.Text = "Верно!";
        else TextLabel.Text = "Неверно!";
    }

    public void ClearText() => TextLabel.Text = "";

    public void NextButtonEvent(EventHandler e) => NextButton.Click += e;

    public enum WordType
    {
        Translation,
        Original
    }
}
