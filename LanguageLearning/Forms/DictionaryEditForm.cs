using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using LinkedListWithTranslations = System.Collections.Generic.LinkedList<string>;

public sealed class DictionaryEditForm : FormStruct
{
    private Label WordsOrTextsLabel;
    private ComboBox WordsOrTextsComboBox;
    private CheckBox WordsCheckBox;
    private CheckBox TextsCheckBox;
    private Label TranslationLabel;
    private Panel TranslationPanel;
    private Button SaveChangesButton;
    private Button DeleteWordButton;
    private Button AddTranslationButton;
    private List<TextBox> TranslationTextBoxes;
    private List<Button> DeleteTranslationButton;

    public event EventHandler<Changes> WordOrTextDeleteEvent; // Удаление слова или предложения в WordsOrTextsComboBox

    private static string minus = "-";
    private static string changes_saved = "Изменения сохранены";
    private static string word_was_not_selected = "Не выбрано слово";
    private static string text_was_not_selected = "Не выбрано предложение";
    private static string word_delited = "Слово удалено";
    private static string translation_delited = "Перевод удален";
    private static string translation_delited_error = "Ошибка при удалении перевода";

    private List<string> words;
    private List<string> texts;
    private bool was_changes_in_words = true;
    private bool was_changes_in_texts = true;

    int translation_text_boxes_visible = 1;

    public DictionaryEditForm(Form1 form, Button activate_form_button) : base(form)
    {
        activate_form_button.Click += (sender, e) =>
        {
            GetWordsAndTexts(form);
            SetVisibleFormElements();

            WordsOrTextsComboBox.Items.Clear();
            WordsCheckBox.Checked = true;
        };
        BackToMainFormButton.Click += (sender, e) => BackToMainForm();

        WordsOrTextsLabel = new Label();
        WordsOrTextsLabel.Text = word_label;
        WordsOrTextsLabel.Font = text_font;
        WordsOrTextsLabel.Location = new Point(161, 132);
        WordsOrTextsLabel.Width = 200;
        WordsOrTextsLabel.Visible = false;
        form.Controls.Add(WordsOrTextsLabel);

        WordsCheckBox = new CheckBox();
        WordsCheckBox.Text = word_label;
        WordsCheckBox.Font = new Font("Consolas", 12, FontStyle.Regular);
        WordsCheckBox.Location = new Point(515, 132);
        WordsCheckBox.Checked = false;
        WordsCheckBox.Enabled = false;
        WordsCheckBox.CheckedChanged += (sender, e) =>
        {
            if (WordsCheckBox.Checked)
            {
                WordsCheckBox.Enabled = false;
                TextsCheckBox.Enabled = true;
                TextsCheckBox.Checked = false;
                WordsOrTextsLabel.Text = word_label;
                WordsOrTextsComboBox.Items.Clear();
                foreach (var word in words)
                {
                    WordsOrTextsComboBox.Items.Add(word);
                }
                while (translation_text_boxes_visible > 1)
                {
                    --translation_text_boxes_visible;
                    TranslationTextBoxes[translation_text_boxes_visible].Text = string.Empty;
                    TranslationTextBoxes[translation_text_boxes_visible].Visible = false;
                    DeleteTranslationButton[translation_text_boxes_visible - 1].Visible = false;
                }
                TranslationTextBoxes[0].Text = string.Empty;
            }
        };
        WordsCheckBox.Width = 80;
        WordsCheckBox.Visible = false;
        form.Controls.Add(WordsCheckBox);

        TextsCheckBox = new CheckBox();
        TextsCheckBox.Text = text_label;
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
                WordsOrTextsLabel.Text = text_label;
                WordsOrTextsComboBox.Items.Clear();
                foreach (var text in texts)
                {
                    WordsOrTextsComboBox.Items.Add(text);
                }
                while (translation_text_boxes_visible > 1)
                {
                    --translation_text_boxes_visible;
                    TranslationTextBoxes[translation_text_boxes_visible].Text = string.Empty;
                    TranslationTextBoxes[translation_text_boxes_visible].Visible = false;
                    DeleteTranslationButton[translation_text_boxes_visible - 1].Visible = false;
                }
                TranslationTextBoxes[0].Text = string.Empty;
            }
        };
        TextsCheckBox.Width = 130;
        TextsCheckBox.Visible = false;
        form.Controls.Add(TextsCheckBox);

        WordsOrTextsComboBox = new ComboBox();
        WordsOrTextsComboBox.Font = text_font;
        WordsOrTextsComboBox.Width = 550;
        WordsOrTextsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        WordsOrTextsComboBox.IntegralHeight = false;
        WordsOrTextsComboBox.MaxDropDownItems = 15;
        WordsOrTextsComboBox.Location = new Point(167, 162);
        WordsOrTextsComboBox.Visible = false;
        WordsOrTextsComboBox.SelectedIndexChanged += (sender, e) => FillTrasnslationBoxes(form.Data);
        form.Controls.Add(WordsOrTextsComboBox);

        DeleteWordButton = new Button();
        DeleteWordButton.Font = text_font;
        DeleteWordButton.Text = minus;
        DeleteWordButton.Width = 34;
        DeleteWordButton.Height = 34;
        DeleteWordButton.Location = new Point(726, 161);
        DeleteWordButton.Visible = false;
        DeleteWordButton.Click += (sender, e) =>
        {
            if (WordsOrTextsComboBox.Text != string.Empty)
            {
                LinkedList<KeyValuePair<string, LinkedListWithTranslations>> list;
                // Поиск ячейки, в которой находится текст или слово
                if (WordsCheckBox.Checked)
                {
                    int index = form.Data.GetHash(WordsOrTextsComboBox.Text, WordsAndTextsData.WordOrText.Word);
                    list = form.Data.Words[index];
                }
                else
                {
                    int index = form.Data.GetHash(WordsOrTextsComboBox.Text, WordsAndTextsData.WordOrText.Text);
                    list = form.Data.Texts[index];
                }
                foreach (var pair in list)
                {
                    if (pair.Key == WordsOrTextsComboBox.Text)
                    {
                        if (list.Remove(pair))
                            WordOrTextDeleteEvent(this, new Changes(pair.Key, WordsCheckBox.Checked ? TypeChanges.WordDeleted : TypeChanges.TextDeleted));
                        break;
                    }
                }

                // Если слово или предложение было добавлено в Incomprehensible, то удаляем его 
                if (form.Incomprehensible != null)
                {
                    foreach (var pair in form.Incomprehensible)
                    {
                        if (pair.Key == WordsOrTextsComboBox.Text)
                        {
                            form.Incomprehensible.Remove(pair);
                            break;
                        }
                    }
                }

                // Удаление слова или предложения из выпадающего списка и из массива
                if (WordsCheckBox.Checked) words.RemoveAt(WordsOrTextsComboBox.SelectedIndex);
                else texts.RemoveAt(WordsOrTextsComboBox.SelectedIndex);
                WordsOrTextsComboBox.Items.Remove(WordsOrTextsComboBox.Text);
                // Если у слова было несколько вариантов перевода, то закрываем поля
                while (translation_text_boxes_visible > 1)
                {
                    --translation_text_boxes_visible;
                    TranslationTextBoxes[translation_text_boxes_visible].Visible = false;
                    if (translation_text_boxes_visible > 0) DeleteTranslationButton[translation_text_boxes_visible - 1].Visible = false;
                }
                TranslationTextBoxes[0].Text = string.Empty;
                MessageBox.Show(word_delited);
            }
        };
        form.Controls.Add(DeleteWordButton);

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

        AddTranslationButton = new Button();
        AddTranslationButton.Font = text_font;
        AddTranslationButton.Width = 34;
        AddTranslationButton.Height = 34;
        AddTranslationButton.Text = "+";
        AddTranslationButton.Click += (sender, e) =>
        {
            // Если поле уже существует, то делаем его видимым
            if (TranslationTextBoxes.Count > translation_text_boxes_visible)
            {
                if (translation_text_boxes_visible > 0) DeleteTranslationButton[translation_text_boxes_visible - 1].Visible = true;
                TranslationTextBoxes[translation_text_boxes_visible].Visible = true;
                TranslationTextBoxes[translation_text_boxes_visible++].Text = string.Empty;
            }
            // Если полей нет, то добавляем
            else
            {
                AddTranslationTextBox(form.Data);
                if (translation_text_boxes_visible > 0) DeleteTranslationButton[translation_text_boxes_visible - 1].Visible = true;
                TranslationTextBoxes[translation_text_boxes_visible++].Visible = true;
            }
        };
        AddTranslationButton.Location = new Point(726, 29);
        TranslationPanel.Controls.Add(AddTranslationButton);

        /*===================================================================*/

        SaveChangesButton = new Button();
        SaveChangesButton.Font = button_font;
        SaveChangesButton.Width = 280;
        SaveChangesButton.Height = 50;
        SaveChangesButton.Location = new Point(437, 511);
        SaveChangesButton.Text = "Сохранить изменения";
        SaveChangesButton.Visible = false;
        SaveChangesButton.Click += (sender, e) =>
        {
            if (WordsOrTextsComboBox.Text != string.Empty && translation_text_boxes_visible > 0)
            {

                int count_text_boxes_with_text = 0;
                for (int i = 0; i < translation_text_boxes_visible; ++i)
                {
                    if (TranslationTextBoxes[i].Text != string.Empty) ++count_text_boxes_with_text;
                }

                if (count_text_boxes_with_text == 0)
                {
                    MessageBox.Show(translation_text_boxes_visible > 1 ? fields_dont_have_translation : field_dont_have_translation, error);
                    return;
                }

                // Получение узла
                LinkedListNode<string> current_node = null;
                if (WordsCheckBox.Checked) current_node = form.Data.GetWordOrTextTranslation(WordsOrTextsComboBox.Text, WordsAndTextsData.WordOrText.Word).First;
                else current_node = form.Data.GetWordOrTextTranslation(WordsOrTextsComboBox.Text, WordsAndTextsData.WordOrText.Text).First;

                // Если добавлены новые варианты перевода
                if (current_node != null && current_node.List.Count < translation_text_boxes_visible)
                {
                    LinkedListNode<string> temp = current_node;
                    bool have = false;
                    for (int i = current_node.List.Count; i < translation_text_boxes_visible; ++i)
                    {
                        if (TranslationTextBoxes[i].Text == string.Empty) continue;
                        while (temp != null)
                        {
                            if (temp.Value.Equals(TranslationTextBoxes[i].Text, System.StringComparison.CurrentCultureIgnoreCase))
                            {
                                have = true;
                                break;
                            }
                            temp = temp.Next;
                        }
                        temp = current_node;
                        if (!have) current_node.List.AddLast(TranslationTextBoxes[i].Text);
                        have = false;
                    }
                }

                // Запись изменений, если они были
                LinkedListNode<string> first_node = current_node;
                for (int i = 0; i < translation_text_boxes_visible; ++i)
                {
                    if (TranslationTextBoxes[i].Text == string.Empty) continue;
                    if (current_node != null && !current_node.Value.Equals(TranslationTextBoxes[i].Text, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        LinkedListNode<string> temp_node = first_node;
                        bool have = false;
                        while (temp_node != null)
                        {
                            if (temp_node.Value.Equals(TranslationTextBoxes[i].Text, System.StringComparison.CurrentCultureIgnoreCase))
                            {
                                have = true;
                                break;
                            }
                            temp_node = temp_node.Next;
                        }
                        if (!have) current_node.Value = TranslationTextBoxes[i].Text;
                    }
                    if (current_node != null) current_node = current_node.Next;
                }
                FillTrasnslationBoxes(form.Data);
                MessageBox.Show(changes_saved);
            }
            else MessageBox.Show(WordsCheckBox.Checked ? word_was_not_selected : text_was_not_selected, error);
        };
        form.Controls.Add(SaveChangesButton);

        form.DictionaryChangesObserver.WordOrTextAddedEvent += (sender, e) =>
        {
            if (e.Type == TypeChanges.WordAdded) was_changes_in_words = true;
            else was_changes_in_texts = true;
        };
    }

    private void GetWordsAndTexts(Form1 form)
    {
        if (was_changes_in_words && form.Data.Words != null)
        {
            words = (from dictonary_list in form.Data.Words
                     where dictonary_list != null
                     from word in dictonary_list
                     orderby word.Key
                     select word.Key).ToList();
            was_changes_in_words = false;
        }
        if (was_changes_in_texts && form.Data.Texts != null)
        {
            texts = (from dictonary_list in form.Data.Texts
                     where dictonary_list != null
                     from text in dictonary_list
                     orderby text.Key
                     select text.Key).ToList();
            was_changes_in_texts = false;
        }
    }

    private void SetVisibleFormElements()
    {
        BackToMainFormButton.Visible = true;
        WordsOrTextsLabel.Visible = true;
        WordsCheckBox.Visible = true;
        TextsCheckBox.Visible = true;
        WordsOrTextsComboBox.Visible = true;
        DeleteWordButton.Visible = true;
        TranslationPanel.Visible = true;
        SaveChangesButton.Visible = true;
    }

    private void HideFormElements()
    {
        BackToMainFormButton.Visible = false;
        WordsOrTextsLabel.Visible = false;
        WordsCheckBox.Visible = false;
        TextsCheckBox.Visible = false;
        WordsOrTextsComboBox.Visible = false;
        DeleteWordButton.Visible = false;
        TranslationPanel.Visible = false;
        SaveChangesButton.Visible = false;
    }

    private void BackToMainForm()
    {
        for (int i = 1; i < translation_text_boxes_visible; ++i)
        {
            TranslationTextBoxes[i].Visible = false;
            DeleteTranslationButton[i - 1].Visible = false;
        }
        TranslationTextBoxes[0].Text = string.Empty;
        translation_text_boxes_visible = 1;
        WordsCheckBox.Checked = false;
        HideFormElements();
    }

    private void AddTranslationTextBox(WordsAndTextsData data)
    {
        int text_box_y_pos = TranslationTextBoxes[TranslationTextBoxes.Count - 1].Location.Y;
        TranslationTextBoxes.Add(new TextBox());
        TranslationTextBoxes[TranslationTextBoxes.Count - 1].Font = text_font;
        TranslationTextBoxes[TranslationTextBoxes.Count - 1].Width = 550;
        TranslationTextBoxes[TranslationTextBoxes.Count - 1].Location = new Point(167, text_box_y_pos + 42);
        TranslationTextBoxes[TranslationTextBoxes.Count - 1].Visible = false;
        TranslationPanel.Controls.Add(TranslationTextBoxes[TranslationTextBoxes.Count - 1]);

        if (DeleteTranslationButton == null) DeleteTranslationButton = new List<Button>();
        DeleteTranslationButton.Add(new Button());
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Font = button_font;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Text = minus;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Width = 34;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Height = 34;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Location = new Point(726, text_box_y_pos + 41);
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Visible = false;
        int text_box_index = TranslationTextBoxes.Count - 1;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Click += (sender, e) =>
        {
            bool success = false;
            if (WordsOrTextsComboBox.Text != string.Empty && TranslationTextBoxes[text_box_index].Text != string.Empty)
            {
                var translation_list = data.GetWordOrTextTranslation(WordsOrTextsComboBox.Text, WordsCheckBox.Checked ? WordsAndTextsData.WordOrText.Word : WordsAndTextsData.WordOrText.Text);
                if (translation_list != null && translation_list.Count > 1)
                {
                    foreach (var translation in translation_list)
                    {
                        if (translation.Equals(TranslationTextBoxes[text_box_index].Text, System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            success = translation_list.Remove(translation);
                            break;
                        }
                    }
                }
                if (success) MessageBox.Show(translation_delited);
                else MessageBox.Show(translation_delited_error, error);
            }
            else if (WordsOrTextsComboBox.Text == string.Empty) MessageBox.Show(WordsCheckBox.Checked ? word_was_not_selected : text_was_not_selected, error);
            else if (TranslationTextBoxes[text_box_index].Text == string.Empty) MessageBox.Show(field_dont_have_translation, error);
            FillTrasnslationBoxes(data);
        };
        TranslationPanel.Controls.Add(DeleteTranslationButton[DeleteTranslationButton.Count - 1]);

    }

    private void HideEmptyBoxes()
    {
        if (TranslationTextBoxes.Count > 1)
        {
            for (int i = 1; i < TranslationTextBoxes.Count; ++i)
            {
                // Если нет видимых полей, то заканчиваем итерирование
                if (!TranslationTextBoxes[i].Visible) break;
                // Если видимое поле пустое, то проверяем следующие поля на наличие данных, если данные есть, то делаем свап
                if (TranslationTextBoxes[i].Text == string.Empty)
                {
                    for (int j = i; j < TranslationTextBoxes.Count; ++j)
                    {
                        if (TranslationTextBoxes[j].Text != string.Empty)
                        {
                            TranslationTextBoxes[i].Text = TranslationTextBoxes[j].Text;
                            TranslationTextBoxes[j].Text = string.Empty;
                        }
                    }
                    // Если все видимые поля пустые, то прячем их
                    if (TranslationTextBoxes[i].Text == string.Empty)
                    {
                        for (int j = i; j < TranslationTextBoxes.Count && TranslationTextBoxes[j].Visible; ++j)
                        {
                            TranslationTextBoxes[j].Visible = false;
                            DeleteTranslationButton[j - 1].Visible = false;
                            --translation_text_boxes_visible;
                        }
                        break;
                    }
                }
            }
        }
    }

    private void FillTrasnslationBoxes(WordsAndTextsData data)
    {
        LinkedListWithTranslations translations;
        // Получение списка переводов
        if (WordsCheckBox.Checked) translations = data.GetWordOrTextTranslation(WordsOrTextsComboBox.Text, WordsAndTextsData.WordOrText.Word);
        else translations = data.GetWordOrTextTranslation(WordsOrTextsComboBox.Text, WordsAndTextsData.WordOrText.Text);
        // Если нет поля для перевода, то добавляем его
        if (TranslationTextBoxes.Count < translations.Count)
        {
            while (TranslationTextBoxes.Count < translations.Count) AddTranslationTextBox(data);
        }
        // Если поле есть, но оно невидимо, то делаем его видимым
        if (translation_text_boxes_visible < translations.Count)
        {
            while (translation_text_boxes_visible < translations.Count)
            {
                TranslationTextBoxes[translation_text_boxes_visible].Visible = true;
                DeleteTranslationButton[translation_text_boxes_visible - 1].Visible = true;
                ++translation_text_boxes_visible;
            }
        }
        // Если полей слишком много, то делаем их невидимыми
        else if (translation_text_boxes_visible > translations.Count)
        {
            while (translation_text_boxes_visible > translations.Count)
            {
                --translation_text_boxes_visible;
                TranslationTextBoxes[translation_text_boxes_visible].Visible = false;
                if (translation_text_boxes_visible > 0) DeleteTranslationButton[translation_text_boxes_visible - 1].Visible = false;
            }
        }
        int it = 0;
        // Заполняем поля с переводом
        foreach (var translation in translations)
        {
            TranslationTextBoxes[it++].Text = translation;
        }
    }
}