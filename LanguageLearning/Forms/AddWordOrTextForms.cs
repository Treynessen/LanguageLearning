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
    private Button ClearButton;

    public event EventHandler<Changes> AddWordOrTextEvent; // Добавление слова или текста

    private static string word_added_to_dictionary = "Слово добавлено в словарь";
    private static string text_added_to_dictionary = "Предложение добавлено в словарь";
    private static string field_dont_have_word = "Поле для ввода слова не заполнено";
    private static string field_dont_have_text = "Поле для ввода предложения не заполнено";

    int translation_text_boxes_visible = 1;

    public AddWordsOrTextForms(Form1 form, Button activate_form_button, Action create_data, AddWordsOrTextFormsType form_type) : base(form)
    {
        activate_form_button.Click += (sender, e) => SetVisibleFormElements();
        BackToMainFormButton.Click += (sender, e) => BackToMainForm();

        WordOrTextLabel = new Label();
        if (form_type == AddWordsOrTextFormsType.AddWordForm) WordOrTextLabel.Text = word_label;
        else if (form_type == AddWordsOrTextFormsType.AddTextForm) WordOrTextLabel.Text = text_label;
        WordOrTextLabel.Font = text_font;
        WordOrTextLabel.Width = 200;
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
        TranslationLabel.Text = translation_label;
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
                WordsAndTextsData.WordOrText type = form_type == AddWordsOrTextFormsType.AddWordForm ? WordsAndTextsData.WordOrText.Word : WordsAndTextsData.WordOrText.Text;

                // Проверка, содержат ли поля переводы
                int count_text_boxes_with_text = 0;
                for (int i = 0; i < translation_text_boxes_visible; ++i)
                {
                    if (TranslationTextBoxes[i].Text != string.Empty) ++count_text_boxes_with_text;
                }

                bool was_added_new_word_or_text = false;

                if (count_text_boxes_with_text > 0)
                {
                    WordOrTextTextBox.Text = WordOrTextTextBox.Text.Replace('’', '\'');
                    if (form.Data == null) create_data();
                    if (translation_text_boxes_visible == 1)
                        was_added_new_word_or_text = form.Data.AddWordOrText(type, WordOrTextTextBox.Text, TranslationTextBoxes[0].Text);
                    else if (translation_text_boxes_visible == 2)
                        was_added_new_word_or_text = form.Data.AddWordOrText(type, WordOrTextTextBox.Text,
                            TranslationTextBoxes[0].Text, TranslationTextBoxes[1].Text);
                    else if (translation_text_boxes_visible == 3)
                        was_added_new_word_or_text = form.Data.AddWordOrText(type, WordOrTextTextBox.Text,
                            TranslationTextBoxes[0].Text, TranslationTextBoxes[1].Text,
                            TranslationTextBoxes[2].Text);
                    else if (translation_text_boxes_visible == 4)
                        was_added_new_word_or_text = form.Data.AddWordOrText(type, WordOrTextTextBox.Text,
                            TranslationTextBoxes[0].Text, TranslationTextBoxes[1].Text,
                            TranslationTextBoxes[2].Text, TranslationTextBoxes[3].Text);
                    else if (translation_text_boxes_visible == 5)
                        was_added_new_word_or_text = form.Data.AddWordOrText(type, WordOrTextTextBox.Text,
                            TranslationTextBoxes[0].Text, TranslationTextBoxes[1].Text, TranslationTextBoxes[2].Text,
                            TranslationTextBoxes[3].Text, TranslationTextBoxes[4].Text);
                    else
                    {
                        string[] translations = new string[translation_text_boxes_visible];
                        for (int i = 0; i < translation_text_boxes_visible; ++i) translations[i] = TranslationTextBoxes[i].Text;
                        was_added_new_word_or_text = form.Data.AddWordOrText(type, WordOrTextTextBox.Text, translations);
                    }

                    if (was_added_new_word_or_text)
                        AddWordOrTextEvent(this, new Changes(WordOrTextTextBox.Text, type == WordsAndTextsData.WordOrText.Word ? TypeChanges.WordAdded : TypeChanges.TextAdded));

                    // Очистка полей
                    ClearButton.PerformClick();
                    MessageBox.Show(type == WordsAndTextsData.WordOrText.Word ? word_added_to_dictionary : text_added_to_dictionary);
                }

                else
                {
                    MessageBox.Show(translation_text_boxes_visible > 1 ? fields_dont_have_translation : field_dont_have_translation, error);
                }
            }
            else MessageBox.Show(form_type == AddWordsOrTextFormsType.AddWordForm ? field_dont_have_word : field_dont_have_text, error);
        };
        form.Controls.Add(AddWordOrTextButton);

        ClearButton = new Button();
        ClearButton.Font = button_font;
        ClearButton.Width = 150;
        ClearButton.Height = 50;
        ClearButton.Location = new Point(339, 511);
        ClearButton.Text = "Очистить";
        ClearButton.Visible = false;
        ClearButton.Click += (sender, e) =>
        {
            WordOrTextTextBox.Text = string.Empty;
            TranslationTextBoxes[0].Text = string.Empty;
            for (int i = 1; i < translation_text_boxes_visible; ++i)
            {
                TranslationTextBoxes[i].Text = string.Empty;
                TranslationTextBoxes[i].Visible = false;
            }
            translation_text_boxes_visible = 1;

        };
        form.Controls.Add(ClearButton);
    }

    private void SetVisibleFormElements()
    {
        BackToMainFormButton.Visible = true;
        WordOrTextLabel.Visible = true;
        WordOrTextTextBox.Visible = true;
        TranslationPanel.Visible = true;
        AddWordOrTextButton.Visible = true;
        ClearButton.Visible = true;
    }

    private void HideFormElements()
    {
        BackToMainFormButton.Visible = false;
        WordOrTextLabel.Visible = false;
        WordOrTextTextBox.Visible = false;
        TranslationPanel.Visible = false;
        AddWordOrTextButton.Visible = false;
        ClearButton.Visible = false;
    }

    private void BackToMainForm()
    {
        WordOrTextTextBox.Text = string.Empty;
        TranslationTextBoxes[0].Text = string.Empty;
        for (int i = 1; i < TranslationTextBoxes.Count; ++i)
        {
            if (TranslationTextBoxes[i].Visible == false) break;
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