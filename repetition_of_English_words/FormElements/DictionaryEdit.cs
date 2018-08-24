﻿using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using repetition_of_English_words;
using StringList = System.Collections.Generic.LinkedList<string>;

public delegate void WordsOrTextDeliter(string key, WordsAndTexts.Type type);

public sealed class DictionaryEdit : FormStruct
{
    private Label WordsOrTextsLabel;
    private ComboBox WordsOrTextsComboBox;
    private CheckBox WordsCheckBox;
    private CheckBox TextsCheckBox;
    private Label TranslationLabel;
    private List<TextBox> TranslationTextBoxs;
    private Panel TranslationPanel;
    private Button SaveChangesButton;
    private Button DeleteWordButton;
    private List<Button> DeleteTranslationButton;

    int translation_text_boxes_visible = 1;

    private IEnumerable<KeyValuePair<string, StringList>> words_with_translate;
    private IEnumerable<KeyValuePair<string, StringList>> texts_with_translate;
    private string[] words;
    private string[] texts;

    public DictionaryEdit(Form1 form, Button activate_button, EventHandler back_main_form, WordsOrTextDeliter words_or_text_deliter)
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
                WordsOrTextsComboBox.Items.Clear();
                WordsOrTextsComboBox.Items.AddRange(words);
                while (translation_text_boxes_visible > 1) TranslationTextBoxs[--translation_text_boxes_visible].Visible = false;
                TranslationTextBoxs[0].Text = "";
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
                WordsOrTextsComboBox.Items.Clear();
                WordsOrTextsComboBox.Items.AddRange(texts);
                while (translation_text_boxes_visible > 1) TranslationTextBoxs[--translation_text_boxes_visible].Visible = false;
                TranslationTextBoxs[0].Text = "";
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
        WordsOrTextsComboBox.SelectedIndexChanged += (sender, e) =>
        {
            StringList translations;
            // Получение списка переводов
            if (WordsCheckBox.Checked) translations = words_with_translate.ElementAt(WordsOrTextsComboBox.SelectedIndex).Value;
            else translations = texts_with_translate.ElementAt(WordsOrTextsComboBox.SelectedIndex).Value;
            // Если нет поля для перевода, то добавляем его
            if (TranslationTextBoxs.Count < translations.Count)
            {
                while (TranslationTextBoxs.Count < translations.Count) AddTranslationTextBox();
            }
            // Если поле есть, но оно невидимо, то делаем его видимым
            if (translation_text_boxes_visible < translations.Count)
            {
                while (translation_text_boxes_visible < translations.Count)
                {
                    TranslationTextBoxs[translation_text_boxes_visible].Visible = true;
                    DeleteTranslationButton[translation_text_boxes_visible].Visible = true;
                    ++translation_text_boxes_visible;
                }
            }
            // Если полей слишком много, то делаем их невидимыми
            else if (translation_text_boxes_visible > translations.Count)
            {
                while (translation_text_boxes_visible > translations.Count)
                {
                    --translation_text_boxes_visible;
                    TranslationTextBoxs[translation_text_boxes_visible].Visible = false;
                    DeleteTranslationButton[translation_text_boxes_visible].Visible = false;
                }
            }
            int it = 0;
            // Заполняем поля с переводом
            foreach (var translation in translations)
            {
                TranslationTextBoxs[it++].Text = translation;
            }
        };
        form.Controls.Add(WordsOrTextsComboBox);

        DeleteWordButton = new Button();
        DeleteWordButton.Font = text_font;
        DeleteWordButton.Text = "-";
        DeleteWordButton.Width = 34;
        DeleteWordButton.Height = 34;
        DeleteWordButton.Location = new Point(726, 161);
        DeleteWordButton.Visible = false;
        DeleteWordButton.Click += (sender, e) =>
        {
            if (WordsOrTextsComboBox.Text != "")
            {
                words_or_text_deliter(WordsOrTextsComboBox.Text, TextsCheckBox.Checked ? WordsAndTexts.Type.Text : WordsAndTexts.Type.Word);
                WordsOrTextsComboBox.Items.Remove(WordsOrTextsComboBox.Text);
                // Если у слова было несколько вариантов перевода, то закрываем поля
                while (translation_text_boxes_visible > 1)
                {
                    --translation_text_boxes_visible;
                    TranslationTextBoxs[translation_text_boxes_visible].Visible = false;
                    DeleteTranslationButton[translation_text_boxes_visible].Visible = false;
                }
                TranslationTextBoxs[0].Text = "";
            }
        };
        form.Controls.Add(DeleteWordButton);

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

        // Переделать название на AddButton!!!!!!!!!
        DeleteTranslationButton = new List<Button>();
        DeleteTranslationButton.Add(new Button());
        DeleteTranslationButton[0].Font = text_font;
        DeleteTranslationButton[0].Width = 34;
        DeleteTranslationButton[0].Height = 34;
        DeleteTranslationButton[0].Text = "+";
        DeleteTranslationButton[0].Click += (sender, e) =>
        {
            // Если поле уже существует, то делаем его видимым
            if (TranslationTextBoxs.Count > translation_text_boxes_visible)
            {
                DeleteTranslationButton[translation_text_boxes_visible].Visible = true;
                TranslationTextBoxs[translation_text_boxes_visible].Visible = true;
                TranslationTextBoxs[translation_text_boxes_visible++].Text = "";
            }
            // Если полей нет, то добавляем
            else
            {
                AddTranslationTextBox();
                DeleteTranslationButton[translation_text_boxes_visible].Visible = true;
                TranslationTextBoxs[translation_text_boxes_visible++].Visible = true;
            }
        };
        DeleteTranslationButton[0].Location = new Point(726, 29);
        TranslationPanel.Controls.Add(DeleteTranslationButton[0]);

        //////////////////////////////////////////////////////

        SaveChangesButton = new Button();
        SaveChangesButton.Font = button_font;
        SaveChangesButton.Width = 280;
        SaveChangesButton.Height = 50;
        SaveChangesButton.Location = new Point(437, 511);
        SaveChangesButton.Text = "Сохранить изменения";
        SaveChangesButton.Visible = false;
        SaveChangesButton.Click += (sender, e) =>
        {
            if (WordsOrTextsComboBox.Text != "")
            {
                LinkedListNode<string> current_node;
                // Получение узла
                if (WordsCheckBox.Checked) current_node = words_with_translate.ElementAt(WordsOrTextsComboBox.SelectedIndex).Value.First;
                else current_node = texts_with_translate.ElementAt(WordsOrTextsComboBox.SelectedIndex).Value.First;

                // Если добавлены новые варианты перевода
                if (current_node.List.Count < translation_text_boxes_visible)
                {
                    LinkedListNode<string> temp = current_node;
                    bool have = false;
                    for (int i = current_node.List.Count; i < translation_text_boxes_visible; ++i)
                    {
                        if (TranslationTextBoxs[i].Text == "") continue;
                        while (temp != null)
                        {
                            if (temp.Value == TranslationTextBoxs[i].Text.ToLower())
                            {
                                have = true;
                                break;
                            }
                            temp = temp.Next;
                        }
                        temp = current_node;
                        if (!have) current_node.List.AddLast(TranslationTextBoxs[i].Text.ToLower());
                        have = false;
                    }
                }

                // Запись изменений, если они были
                for (int i = 0; i < translation_text_boxes_visible; ++i)
                {
                    if (TranslationTextBoxs[i].Text == "") continue;
                    if (current_node != null && current_node.Value != TranslationTextBoxs[i].Text.ToLower()) current_node.Value = TranslationTextBoxs[i].Text.ToLower();
                    if (current_node != null) current_node = current_node.Next;
                }
                MessageBox.Show("Изменения сохранены");
            }
        };
        form.Controls.Add(SaveChangesButton);

        BackButton.Visible = false;
        BackButton.Click += BackToMainForm;
    }

    public void WordsAndTextsData(LinkedList<KeyValuePair<string, StringList>>[] words,
        LinkedList<KeyValuePair<string, StringList>>[] texts)
    {
        words_with_translate = from n in words
                               where n != null
                               select n into ws
                               from result in ws
                               orderby result.Key
                               select result;
        this.words = new string[words_with_translate.Count()];
        int it = 0;
        foreach (var word in words_with_translate)
        {
            this.words[it++] = word.Key;
        }
        it = 0;
        WordsOrTextsComboBox.Items.AddRange(this.words);

        texts_with_translate = from n in texts
                               where n != null
                               select n into ts
                               from result in ts
                               orderby result.Key
                               select result;
        this.texts = new string[texts_with_translate.Count()];
        foreach (var text in texts_with_translate)
        {
            this.texts[it++] = text.Key;
        }
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
            TranslationPanel.Controls.Remove(DeleteTranslationButton[i]);
        }
        TranslationTextBoxs.RemoveRange(1, TranslationTextBoxs.Count - 1);
        DeleteTranslationButton.RemoveRange(1, DeleteTranslationButton.Count - 1);
        TranslationTextBoxs[0].Text = "";
        WordsOrTextsComboBox.Items.Clear();
        SaveChangesButton.Visible = false;
        DeleteWordButton.Visible = false;
    }

    private void SetVisible(object sender, EventArgs e)
    {
        WordsOrTextsLabel.Visible = true;
        WordsOrTextsComboBox.Visible = true;
        WordsCheckBox.Visible = true;
        TextsCheckBox.Visible = true;
        TranslationPanel.Visible = true;
        SaveChangesButton.Visible = true;
        DeleteWordButton.Visible = true;
        WordsCheckBox.Checked = true;
        translation_text_boxes_visible = 1;
    }

    private void AddTranslationTextBox()
    {
        int text_box_y_pos = TranslationTextBoxs[TranslationTextBoxs.Count - 1].Location.Y;
        TranslationTextBoxs.Add(new TextBox());
        TranslationTextBoxs[TranslationTextBoxs.Count - 1].Font = text_font;
        TranslationTextBoxs[TranslationTextBoxs.Count - 1].Width = 550;
        TranslationTextBoxs[TranslationTextBoxs.Count - 1].Location = new Point(167, text_box_y_pos + 42);
        TranslationTextBoxs[TranslationTextBoxs.Count - 1].Visible = false;
        TranslationPanel.Controls.Add(TranslationTextBoxs[TranslationTextBoxs.Count - 1]);

        DeleteTranslationButton.Add(new Button());
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Font = button_font;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Text = "-";
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Width = 34;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Height = 34;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Location = new Point(726, text_box_y_pos + 41);
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Visible = false;
        int index = TranslationTextBoxs.Count - 1;
        DeleteTranslationButton[DeleteTranslationButton.Count - 1].Click += (sender, e) =>
        {
            if (TranslationTextBoxs[index].Text != "")
            {
                if (TextsCheckBox.Checked)
                {
                    var text_translates = texts_with_translate.ElementAt(WordsOrTextsComboBox.SelectedIndex).Value;
                    if (text_translates.Remove(TranslationTextBoxs[index].Text))
                        MessageBox.Show("Текст удален");
                }
                else
                {
                    var word_translates = words_with_translate.ElementAt(WordsOrTextsComboBox.SelectedIndex).Value;
                    if (word_translates.Remove(TranslationTextBoxs[index].Text))
                        MessageBox.Show("Текст удален");
                }
            }
            TranslationTextBoxs[index].Text = "";
            HideEmptyBoxes();
        };
        TranslationPanel.Controls.Add(DeleteTranslationButton[DeleteTranslationButton.Count - 1]);
    }

    private void HideEmptyBoxes()
    {
        if (TranslationTextBoxs.Count > 1)
        {
            for (int i = 1; i < TranslationTextBoxs.Count; ++i)
            {
                // Если нет видимых полей, то заканчиваем итерирование
                if (!TranslationTextBoxs[i].Visible) break;
                // Если видимое поле пустое, то проверяем следующие поля на наличие данных, если данные есть, то делаем свап
                if (TranslationTextBoxs[i].Text == "")
                {
                    for (int j = i; j < TranslationTextBoxs.Count; ++j)
                    {
                        if (TranslationTextBoxs[j].Text != "")
                        {
                            TranslationTextBoxs[i].Text = TranslationTextBoxs[j].Text;
                            TranslationTextBoxs[j].Text = "";
                        }
                    }
                    // Если все видимые поля пустые, то прячем их
                    if (TranslationTextBoxs[i].Text == "")
                    {
                        for (int j = i; j < TranslationTextBoxs.Count && TranslationTextBoxs[j].Visible; ++j)
                        {
                            TranslationTextBoxs[j].Visible = false;
                            DeleteTranslationButton[j].Visible = false;
                            --translation_text_boxes_visible;
                        }
                        break;
                    }
                }
            }
        }
    }
}