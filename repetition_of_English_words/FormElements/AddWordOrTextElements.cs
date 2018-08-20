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
    private List<TextBox> TranslationTextBoxs;
    private Button AddTranslation;
    private Panel TranslationPanel;
    private Button AddWordButton;
    private Button ClearButton;

    public string[] Translations
    {
        get
        {
            int count = 0;
            for(int i=0;i< TranslationTextBoxs.Count; ++i)
            {
                if (TranslationTextBoxs[i].Text != "") ++count;
            }
            string[] strs = new string[count];
            for(int i = 0, j=0;i< TranslationTextBoxs.Count; ++i)
            {
                if (TranslationTextBoxs[i].Text != "") strs[j++] = TranslationTextBoxs[i].Text.ToLower();
            }
            return strs;
        }
    }
    public string WordOrText { get { return WordOrTextTextBox.Text.ToLower(); } }

    public FormAddWordsOrText(Form1 form, Button activate_button, EventHandler back_main_form, Form f)
        : base(form, activate_button, back_main_form)
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

        TranslationTextBoxs = new List<TextBox>();
        TranslationTextBoxs.Add(new TextBox());
        TranslationTextBoxs[0].Font = text_font;
        TranslationTextBoxs[0].Width = 550;
        TranslationTextBoxs[0].Location = new Point(167, 30);
        TranslationPanel.Controls.Add(TranslationTextBoxs[0]);

        AddTranslation = new Button();
        AddTranslation.Font = text_font;
        AddTranslation.Text = "+";
        AddTranslation.Width = 34;
        AddTranslation.Height = 34;
        AddTranslation.Location = new Point(726, 29);
        AddTranslation.Click += (sender, e) =>
        {
            int text_box_y_pos = TranslationTextBoxs[TranslationTextBoxs.Count - 1].Location.Y;
            TranslationTextBoxs.Add(new TextBox());
            TranslationTextBoxs[TranslationTextBoxs.Count - 1].Font = text_font;
            TranslationTextBoxs[TranslationTextBoxs.Count - 1].Width = 550;
            TranslationTextBoxs[TranslationTextBoxs.Count - 1].Location = new Point(167, text_box_y_pos + 42);
            TranslationPanel.Controls.Add(TranslationTextBoxs[TranslationTextBoxs.Count - 1]);
        };
        TranslationPanel.Controls.Add(AddTranslation);

        //////////////////////////////////////////////////////

        AddWordButton = new Button();
        AddWordButton.Font = button_font;
        AddWordButton.Width = 220;
        AddWordButton.Height = 50;
        AddWordButton.Location = new Point(497, 511);
        if (f == Form.AddWordForm) AddWordButton.Text = "Добавить слово";
        else if (f == Form.AddTextForm) AddWordButton.Text = "Добавить текст";
        AddWordButton.Visible = false;
        form.Controls.Add(AddWordButton);

        ClearButton = new Button();
        ClearButton.Font = button_font;
        ClearButton.Width = 180;
        ClearButton.Height = 50;
        ClearButton.Location = new Point(309, 511);
        ClearButton.Text = "Очистить";
        ClearButton.Visible = false;
        ClearButton.Click += (sender, e) =>
        {
            if (TranslationTextBoxs.Count > 1)
            {
                for (int i = 1; i < TranslationTextBoxs.Count; ++i)
                {
                    TranslationPanel.Controls.Remove(TranslationTextBoxs[i]);
                }
                TranslationTextBoxs.RemoveRange(1, TranslationTextBoxs.Count - 1);
            }
            WordOrTextTextBox.Text = "";
            TranslationTextBoxs[0].Text = "";

        };
        form.Controls.Add(ClearButton);

        BackButton.Visible = false;
        BackButton.Click += BackToMainForm;
    }

    private void BackToMainForm(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = false;
        WordOrTextTextBox.Visible = false;
        WordOrTextTextBox.Text = "";
        TranslationPanel.Visible = false;
        for (int i = 1; i < TranslationTextBoxs.Count; ++i)
        {
            TranslationPanel.Controls.Remove(TranslationTextBoxs[i]);
        }
        TranslationTextBoxs.RemoveRange(1, TranslationTextBoxs.Count - 1);
        TranslationTextBoxs[0].Text = "";
        AddWordButton.Visible = false;
        ClearButton.Visible = false;
    }

    private void SetVisible(object sender, EventArgs e)
    {
        WordOrTextLabel.Visible = true;
        WordOrTextTextBox.Visible = true;
        TranslationPanel.Visible = true;
        AddWordButton.Visible = true;
        ClearButton.Visible = true;
    }

    public void Clear() => ClearButton.PerformClick();

    public void AddWordOrTextEvent(EventHandler e) => AddWordButton.Click += e;
}