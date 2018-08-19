using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using repetition_of_English_words;

public sealed class FormAddWordsOrText : FormStruct
{
    private Label WordOrTextLabel;
    private TextBox WordOrTextTextBox;
    private Label TranslationLabel;
    private LinkedList<TextBox> TranslationTextBoxs;
    private Button AddTranslation;
    private Panel TranslationPanel;
    private Button AddWord;

    public string WordOrText
    {
        get { return WordOrTextTextBox.Text; }
    }
    public string[] Translations
    {
        get
        {
            string[] translations = new string[TranslationTextBoxs.Count];
            int it = 0;
            foreach (var tb in TranslationTextBoxs) translations[it++] = tb.Text;
            return translations;
        }
    }

    public FormAddWordsOrText(Form1 form, Button activate_button, Form f) : base(form, activate_button)
    {
        activate_button.Click += SetVisible;

        WordOrTextLabel = new Label();
        if (f == Form.AddWordForm) WordOrTextLabel.Text = "Слово";
        else if (f == Form.AddTextForm) WordOrTextLabel.Text = "Текст";
        WordOrTextLabel.Font = text_font;
        WordOrTextLabel.Location = new Point(161, 132);
        WordOrTextLabel.Visible = false;
        form.Controls.Add(WordOrTextLabel);

        WordOrTextTextBox = new TextBox();
        WordOrTextTextBox.Font = text_font;
        WordOrTextTextBox.Width = 550;
        WordOrTextTextBox.Location = new Point(167, 162);
        WordOrTextTextBox.Visible = false;
        form.Controls.Add(WordOrTextTextBox);

        //////////////////////////////////////////////////////

        TranslationPanel = new Panel();
        TranslationPanel.Height = 226;
        TranslationPanel.Width = 885;
        TranslationPanel.Location = new Point(0, 284);
        TranslationPanel.AutoScroll = true;
        TranslationPanel.Visible = false;
        form.Controls.Add(TranslationPanel);

        TranslationLabel = new Label();
        TranslationLabel.Text = "Перевод";
        TranslationLabel.Font = text_font;
        TranslationLabel.Location = new Point(161, 0);
        TranslationPanel.Controls.Add(TranslationLabel);

        TranslationTextBoxs = new LinkedList<TextBox>();
        TranslationTextBoxs.AddLast(new TextBox());
        TranslationTextBoxs.Last.Value.Font = text_font;
        TranslationTextBoxs.Last.Value.Width = 550;
        TranslationTextBoxs.Last.Value.Location = new Point(167, 30);
        TranslationPanel.Controls.Add(TranslationTextBoxs.Last.Value);

        AddTranslation = new Button();
        AddTranslation.Font = text_font;
        AddTranslation.Text = "+";
        AddTranslation.Width = 34;
        AddTranslation.Height = 34;
        AddTranslation.Location = new Point(726, 29);
        AddTranslation.Click += (sender, e) =>
        {
            int text_box_y_pos = TranslationTextBoxs.Last.Value.Location.Y;
            TranslationTextBoxs.AddLast(new TextBox());
            TranslationTextBoxs.Last.Value.Font = text_font;
            TranslationTextBoxs.Last.Value.Width = 550;
            TranslationTextBoxs.Last.Value.Location = new Point(167, text_box_y_pos + 42);
            TranslationPanel.Controls.Add(TranslationTextBoxs.Last.Value);
        };
        TranslationPanel.Controls.Add(AddTranslation);

        //////////////////////////////////////////////////////

        AddWord = new Button();
        AddWord.Font = button_font;
        AddWord.Width = 220;
        AddWord.Height = 50;
        AddWord.Location = new Point(485, 511);
        if (f == Form.AddWordForm) AddWord.Text = "Добавить слово";
        else if (f == Form.AddTextForm) AddWord.Text = "Добавить текст";
        AddWord.Visible = false;
        form.Controls.Add(AddWord);

        BackButton.Visible = false;
        BackButton.Click += BackToMainForm;
    }

    private void BackToMainForm(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = false;
        WordOrTextTextBox.Visible = false;
        WordOrTextTextBox.Text = "";
        TranslationPanel.Visible = false;
        while (TranslationTextBoxs.Count > 1)
        {
            TranslationPanel.Controls.Remove(TranslationTextBoxs.Last.Value);
            TranslationTextBoxs.Remove(TranslationTextBoxs.Last);
        }
        TranslationTextBoxs.Last.Value.Text = "";
        AddWord.Visible = false;
    }

    private void SetVisible(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = true;
        WordOrTextTextBox.Visible = true;
        TranslationPanel.Visible = true;
        AddWord.Visible = true;
    }

    public void AddWordOrTextClickEvent(EventHandler e) => AddWord.Click += e;
}