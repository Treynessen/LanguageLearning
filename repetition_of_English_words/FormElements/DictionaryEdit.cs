using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using repetition_of_English_words;
using StringList = System.Collections.Generic.LinkedList<string>;

public sealed class DictionaryEdit : FormStruct
{
    private Label WordsOrTextsLabel;
    private ComboBox WordsOrTextsComboBox;
    private CheckBox WordsCheckBox;
    private CheckBox TextsCheckBox;
    private Label TranslationLabel;
    private List<TextBox> TranslationTextBoxs;
    private Button AddTranslation;
    private Panel TranslationPanel;
    private Button AddWordButton;

    public DictionaryEdit(Form1 form, Button activate_button, EventHandler back_main_form) 
        : base(form, activate_button, back_main_form)
    {
        activate_button.Click += SetVisible;

        WordsOrTextsLabel = new Label();
        WordsOrTextsLabel.Text = "Слова";
        WordsOrTextsLabel.Font = text_font;
        WordsOrTextsLabel.Location = new Point(161, 132);
        WordsOrTextsLabel.Width = 200;
        WordsOrTextsLabel.Visible = false;
        form.Controls.Add(WordsOrTextsLabel);

        WordsCheckBox = new CheckBox();
        WordsCheckBox.Text = "Слова";
        WordsCheckBox.Font = new Font("Consolas", 12, FontStyle.Regular);
        WordsCheckBox.Location = new Point(515, 132);
        WordsCheckBox.Checked = true;
        WordsCheckBox.Enabled = false;
        WordsCheckBox.CheckedChanged += (sender, e) =>
        {
            if (WordsCheckBox.Checked)
            {
                WordsCheckBox.Enabled = false;
                TextsCheckBox.Enabled = true;
                TextsCheckBox.Checked = false;
                WordsOrTextsLabel.Text = "Слова";
            }
        };
        WordsCheckBox.Width = 80;
        WordsCheckBox.Visible = false;
        form.Controls.Add(WordsCheckBox);

        TextsCheckBox = new CheckBox();
        TextsCheckBox.Text = "Предложения";
        TextsCheckBox.Font = new Font("Consolas", 12, FontStyle.Regular);
        TextsCheckBox.Location = new Point(598, 132);
        TextsCheckBox.Checked = false;
        TextsCheckBox.Enabled = true;
        TextsCheckBox.CheckedChanged += (sender, e) =>
        {
            if (TextsCheckBox.Checked)
            {
                TextsCheckBox.Enabled = false;
                WordsCheckBox.Enabled = true;
                WordsCheckBox.Checked = false;
                WordsOrTextsLabel.Text = "Предложения";
            }
        };
        TextsCheckBox.Width = 130;
        TextsCheckBox.Visible = false;
        form.Controls.Add(TextsCheckBox);

        WordsOrTextsComboBox = new ComboBox();
        WordsOrTextsComboBox.Font = text_font;
        WordsOrTextsComboBox.Width = 550;
        WordsOrTextsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        WordsOrTextsComboBox.Location = new Point(167, 162);
        WordsOrTextsComboBox.Visible = false;
        form.Controls.Add(WordsOrTextsComboBox);

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
        AddWordButton.Text = "Добавить слово";
        AddWordButton.Visible = false;
        form.Controls.Add(AddWordButton);

        BackButton.Visible = false;
        BackButton.Click += BackToMainForm;
    }

    public void WordsAndTextsData(LinkedList<KeyValuePair<string, StringList>>[] words,
        LinkedList<KeyValuePair<string, StringList>>[] texts,
        int[] words_filled_cells, int[] texts_filled_cells)
    {
        
        
    }

    private void BackToMainForm(object sender, EventArgs e)
    {
        WordsOrTextsLabel.Visible = false;
        WordsOrTextsComboBox.Visible = false;
        WordsCheckBox.Visible = false;
        TextsCheckBox.Visible = false;
        TranslationPanel.Visible = false;
        for (int i = 1; i < TranslationTextBoxs.Count; ++i)
        {
            TranslationPanel.Controls.Remove(TranslationTextBoxs[i]);
        }
        TranslationTextBoxs.RemoveRange(1, TranslationTextBoxs.Count - 1);
        TranslationTextBoxs[0].Text = "";
        AddWordButton.Visible = false;
    }

    private void SetVisible(object sender, EventArgs e)
    {
        WordsOrTextsLabel.Visible = true;
        WordsOrTextsComboBox.Visible = true;
        WordsCheckBox.Visible = true;
        TextsCheckBox.Visible = true;
        TranslationPanel.Visible = true;
        AddWordButton.Visible = true;
    }
}