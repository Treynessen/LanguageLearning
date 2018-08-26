using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public sealed class AddWordsOrTextForms : FormStruct
{
    private Label WordOrTextLabel;
    private TextBox WordOrTextTextBox;
    private Panel TranslationPanel;
    private Label TranslationLabel;
    private List<TextBox> TranslationTextBoxes;
    private Button AddTranslation;
    private Button AddWordOrTextButton;

    int translation_text_boxes_visible = 1;

    public AddWordsOrTextForms(Form1 form, Button activate_form_button, Action create_data, AddWordsOrTextFormsType form_type) : base(form)
    {
        activate_form_button.Click += (sender, e) => SetVisibleFormElements();
        BackToMainFormButton.Click += (sender, e) => BackToMainForm();

        WordOrTextLabel = new Label();
        if (form_type == AddWordsOrTextFormsType.AddWordForm) WordOrTextLabel.Text = "Слово";
        else if (form_type == AddWordsOrTextFormsType.AddTextForm) WordOrTextLabel.Text = "Текст";
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

        /*==================== TranslationPanel Elements ====================*/

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

        TranslationTextBoxes = new List<TextBox>();
        TranslationTextBoxes.Add(new TextBox());
        TranslationTextBoxes[0].Font = text_font;
        TranslationTextBoxes[0].Width = 550;
        TranslationTextBoxes[0].Location = new Point(167, 30);
        TranslationPanel.Controls.Add(TranslationTextBoxes[0]);

        AddTranslation = new Button();
        AddTranslation.Font = text_font;
        AddTranslation.Text = "+";
        AddTranslation.Width = 34;
        AddTranslation.Height = 34;
        AddTranslation.Location = new Point(726, 29);
        AddTranslation.Click += (sender, e) =>
        {
            // Если свободных полей нет, то создаем новое
            if (translation_text_boxes_visible == TranslationTextBoxes.Count)
            {
                int text_box_y_pos = TranslationTextBoxes[translation_text_boxes_visible - 1].Location.Y;
                TranslationTextBoxes.Add(new TextBox());
                TranslationTextBoxes[translation_text_boxes_visible].Font = text_font;
                TranslationTextBoxes[translation_text_boxes_visible].Width = 550;
                TranslationTextBoxes[translation_text_boxes_visible].Location = new Point(167, text_box_y_pos + 42);
                TranslationPanel.Controls.Add(TranslationTextBoxes[translation_text_boxes_visible]);
                ++translation_text_boxes_visible;
            }
            // Если имеются свободные поля, но они невидимы, то делаем их видимыми
            else if (translation_text_boxes_visible < TranslationTextBoxes.Count)
            {
                TranslationTextBoxes[translation_text_boxes_visible++].Visible = true;
            }
        };
        TranslationPanel.Controls.Add(AddTranslation);

        /*===================================================================*/

        AddWordOrTextButton = new Button();
        AddWordOrTextButton.Font = button_font;
        AddWordOrTextButton.Width = 220;
        AddWordOrTextButton.Height = 50;
        AddWordOrTextButton.Location = new Point(497, 511);
        if (form_type == AddWordsOrTextFormsType.AddWordForm) AddWordOrTextButton.Text = "Добавить слово";
        else if (form_type == AddWordsOrTextFormsType.AddTextForm) AddWordOrTextButton.Text = "Добавить текст";
        AddWordOrTextButton.Visible = false;
        AddWordOrTextButton.Click += (sender, e) =>
        {
            if (WordOrTextTextBox.Text != string.Empty)
            {
                if (form.Data == null) create_data();
                WordsAndTextsData.WordOrText type = form_type == AddWordsOrTextFormsType.AddWordForm ? WordsAndTextsData.WordOrText.Word : WordsAndTextsData.WordOrText.Text;
                if (translation_text_boxes_visible == 1)
                    form.Data.AddWordOrText(type, WordOrTextTextBox.Text.ToLower(), TranslationTextBoxes[0].Text.ToLower());
                if (translation_text_boxes_visible == 2)
                    form.Data.AddWordOrText(type, WordOrTextTextBox.Text.ToLower(),
                        TranslationTextBoxes[0].Text.ToLower(), TranslationTextBoxes[1].Text.ToLower());
                if (translation_text_boxes_visible == 3)
                    form.Data.AddWordOrText(type, WordOrTextTextBox.Text.ToLower(),
                        TranslationTextBoxes[0].Text.ToLower(), TranslationTextBoxes[1].Text.ToLower(),
                        TranslationTextBoxes[2].Text.ToLower());
                if (translation_text_boxes_visible == 4)
                    form.Data.AddWordOrText(type, WordOrTextTextBox.Text.ToLower(),
                        TranslationTextBoxes[0].Text.ToLower(), TranslationTextBoxes[1].Text.ToLower(),
                        TranslationTextBoxes[2].Text.ToLower(), TranslationTextBoxes[3].Text.ToLower());
                if (translation_text_boxes_visible == 5)
                    form.Data.AddWordOrText(type, WordOrTextTextBox.Text.ToLower(),
                        TranslationTextBoxes[0].Text.ToLower(), TranslationTextBoxes[1].Text.ToLower(), TranslationTextBoxes[2].Text.ToLower(),
                        TranslationTextBoxes[3].Text.ToLower(), TranslationTextBoxes[4].Text.ToLower());
                else
                {
                    string[] translations = new string[translation_text_boxes_visible];
                    for (int i = 0; i < translation_text_boxes_visible; ++i) translations[i] = TranslationTextBoxes[i].Text.ToLower();
                    form.Data.AddWordOrText(type, WordOrTextTextBox.Text.ToLower(), translations);
                }
                // Очистка полей
                WordOrTextTextBox.Text = string.Empty;
                TranslationTextBoxes[0].Text = string.Empty;
                for (int i = 1; i < translation_text_boxes_visible; ++i)
                {
                    TranslationTextBoxes[i].Text = string.Empty;
                    TranslationTextBoxes[i].Visible = false;
                }
                translation_text_boxes_visible = 1;
                MessageBox.Show(type == WordsAndTextsData.WordOrText.Word ? "Слово добавлено в словарь" : "Текст добавлен в словарь");
            }
        };
        form.Controls.Add(AddWordOrTextButton);
    }

    private void SetVisibleFormElements()
    {
        BackToMainFormButton.Visible = true;
        WordOrTextLabel.Visible = true;
        WordOrTextTextBox.Visible = true;
        TranslationPanel.Visible = true;
        AddWordOrTextButton.Visible = true;
    }

    private void HideFormElements()
    {
        BackToMainFormButton.Visible = false;
        WordOrTextLabel.Visible = false;
        WordOrTextTextBox.Visible = false;
        TranslationPanel.Visible = false;
        AddWordOrTextButton.Visible = false;
    }

    private void BackToMainForm()
    {
        WordOrTextTextBox.Text = string.Empty;
        TranslationTextBoxes[0].Text = string.Empty;
        for (int i = 1; i < TranslationTextBoxes.Count; ++i)
        {
            TranslationTextBoxes[i].Text = string.Empty;
            TranslationTextBoxes[i].Visible = false;
            --translation_text_boxes_visible;
        }
        HideFormElements();
    }

    public enum AddWordsOrTextFormsType
    {
        AddWordForm,
        AddTextForm
    }
}