using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using LinkedListWithTranslations = System.Collections.Generic.LinkedList<string>;
using IncomprehensiblePair = System.Collections.Generic.KeyValuePair<string, WordsAndTextsData.WordOrText>;

public sealed class TrainingForm : FormStruct
{
    private Label WordOrTextLabel;
    private TextBox WordOrTextTextBox;
    private Label TranslationLabel;
    private TextBox TranslationTextBox;
    private Button CheckButton;
    private Label ResultLabel;
    private Button NextWordOrTextButton;

    private WordsAndTextsData.WordOrText type;
    private Language language; // Язык слова или предложения в WordOrTextTextBox
    private string right = "Правильно!";
    private string wrong = "Неправильно!";
    private string if_russian_word_or_text = null; // Если вопрос на русском, то сюда записывается английский перевод
    private int[] words_fill_cells;
    private int[] texts_fill_cells;
    int words_was_count = 0;
    int texts_was_count = 0;
    LinkedList<string> words_was;
    LinkedList<string> texts_was;

    public TrainingForm(Form1 form, Button activate_form_button, Action<IncomprehensiblePair> add_incomprehensible) : base(form)
    {
        activate_form_button.Click += (sender, e) =>
        {
            FirstTryAfterEntry(form);
            NextWordOrTextButton.Enabled = false;
            CheckButton.Enabled = false;
            SetVisibleFormElements();
            if (words_fill_cells.Length > 0 || texts_fill_cells.Length > 0)
            {
                NextWordOrTextButton.Enabled = true;
                CheckButton.Enabled = true;
                NextWordOrTextButton.PerformClick();
            }
        };
        BackToMainFormButton.Click += (sender, e) => BackToMainForm();

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
        CheckButton.Click += (sender, e) =>
        {
            if (WordOrTextTextBox.Text != string.Empty && TranslationTextBox.Text != string.Empty)
            {
                KeyValuePair<string, LinkedListWithTranslations> pair = new KeyValuePair<string, LinkedListWithTranslations>();
                string word_or_text = null;
                if (type == WordsAndTextsData.WordOrText.Word)
                    word_or_text = language == Language.English ? WordOrTextTextBox.Text : TranslationTextBox.Text.ToLower();
                else
                    word_or_text = language == Language.English ? WordOrTextTextBox.Text : TranslationTextBox.Text.ToLower();
                int index = form.Data.GetHash(word_or_text, type);
                // Получение пары слово - переводы
                if (type == WordsAndTextsData.WordOrText.Word && form.Data.Words[index] != null)
                {
                    foreach (var _pair in form.Data.Words[index])
                    {
                        if (_pair.Key == word_or_text)
                        {
                            pair = _pair;
                            break;
                        }
                    }
                }
                else if (type == WordsAndTextsData.WordOrText.Text && form.Data.Texts[index] != null)
                {
                    foreach (var _pair in form.Data.Texts[index])
                    {
                        if (_pair.Key == word_or_text)
                        {
                            pair = _pair;
                            break;
                        }
                    }
                }
                // Если пара существует
                if (pair.Key != null && pair.Key != string.Empty)
                {

                    word_or_text = language == Language.English ? TranslationTextBox.Text.ToLower() : WordOrTextTextBox.Text;
                    ResultLabel.Text = wrong;
                    foreach (var str in pair.Value)
                    {
                        if (str == word_or_text)
                        {
                            ResultLabel.Text = right;
                            break;
                        }
                    }
                }
                else ResultLabel.Text = wrong;

                // Если дан неправильный ответ, то добавляем слово в список
                if (ResultLabel.Text == wrong)
                {
                    bool have = false;
                    if (form.Incomprehensible != null)
                    {
                        foreach (var _pair in form.Incomprehensible)
                        {
                            if ((language == Language.English ? WordOrTextTextBox.Text : if_russian_word_or_text) == _pair.Key)
                            {
                                have = true;
                                break;
                            }
                        }
                    }
                    if (!have || form.Incomprehensible == null) add_incomprehensible(new IncomprehensiblePair(language == Language.English ? WordOrTextTextBox.Text : if_russian_word_or_text, type));
                }
                // Если дан правильный ответ, то удаляем слово из списка
                else
                {
                    if (form.Incomprehensible != null)
                    {
                        foreach (var _pair in form.Incomprehensible)
                        {
                            if ((language == Language.English && _pair.Key == WordOrTextTextBox.Text)
                            || (language == Language.Russian && _pair.Key == if_russian_word_or_text))
                            {
                                form.Incomprehensible.Remove(_pair);
                                break;
                            }
                        }
                    }
                }
            }
            else ResultLabel.Text = wrong;
            CheckButton.Enabled = false;
        };
        form.Controls.Add(CheckButton);

        ResultLabel = new Label();
        ResultLabel.Font = text_font;
        ResultLabel.Width = 300;
        ResultLabel.Location = new Point(160, 390);
        ResultLabel.Visible = false;
        form.Controls.Add(ResultLabel);

        NextWordOrTextButton = new Button();
        NextWordOrTextButton.Font = button_font;
        NextWordOrTextButton.Text = "Далее";
        NextWordOrTextButton.Visible = false;
        NextWordOrTextButton.Width = 150;
        NextWordOrTextButton.Height = 50;
        NextWordOrTextButton.Location = new Point(567, 511);
        NextWordOrTextButton.Click += (sender, e) =>
        {
            if (words_fill_cells.Length == 0 && texts_fill_cells.Length == 0) return;

            if_russian_word_or_text = null;

            ResultLabel.Text = string.Empty;
            TranslationTextBox.Text = string.Empty;
            CheckButton.Enabled = true;

            Random rand = new Random();
            if (words_fill_cells.Length != 0 || texts_fill_cells.Length != 0 || form.Incomprehensible != null)
            {
                int rand_num = -1;
                if (words_fill_cells.Length != 0 && texts_fill_cells.Length != 0 && form.Incomprehensible == null) rand_num = rand.Next(0, 2);
                else if (words_fill_cells.Length != 0 && texts_fill_cells.Length != 0 && form.Incomprehensible != null && form.Incomprehensible.Count > 0) rand_num = rand.Next(0, 4); // повышенный шанс
                else if (form.Incomprehensible != null && form.Incomprehensible.Count > 0) rand_num = 2;
                else if (words_fill_cells.Length != 0) rand_num = 0;
                else if (texts_fill_cells.Length != 0) rand_num = 1;
                // Выбор слова
                if (rand_num == 0)
                {
                    type = WordsAndTextsData.WordOrText.Word;
                    int index = -1;
                    do
                    {
                        index = rand.Next(0, words_fill_cells.Length);
                        index = words_fill_cells[index];
                    } while (form.Data.Words[index] == null || form.Data.Words[index].Count == 0);

                    int list_index = rand.Next(0, form.Data.Words[index].Count);
                    // Проверка, было ли это слово
                    if (words_was.Count > 0)
                    {
                        foreach (var str in words_was)
                        {
                            if (form.Data.Words[index].ElementAt(list_index).Key == str)
                            {
                                NextWordOrTextButton.PerformClick();
                                return;
                            }
                        }
                    }
                    // Устанавливает в текст бокс английское слово
                    if (rand.Next(0, 2) == 0)
                    {
                        language = Language.English;
                        WordOrTextTextBox.Text = form.Data.Words[index].ElementAt(list_index).Key;
                    }
                    // Устанавливает в текст бокс перевод
                    else
                    {
                        language = Language.Russian;
                        int translation_index = rand.Next(0, form.Data.Words[index].ElementAt(list_index).Value.Count);
                        WordOrTextTextBox.Text = form.Data.Words[index].ElementAt(list_index).Value.ElementAt(translation_index);
                        if_russian_word_or_text = form.Data.Words[index].ElementAt(list_index).Key;
                    }

                    if (words_was.Count > 0)
                    {
                        // Если не заполнен, то продолжаем заполнение
                        if (words_was.Count < words_was_count) words_was.AddLast(form.Data.Words[index].ElementAt(list_index).Key);
                        // Иначе удаляем первое значение из очереди и добавляем слово в конец списка
                        else
                        {
                            words_was.RemoveFirst();
                            words_was.AddLast(form.Data.Words[index].ElementAt(list_index).Key);
                        }
                    }
                }
                // Выбор предложения
                else if (rand_num == 1)
                {
                    type = WordsAndTextsData.WordOrText.Text;
                    int index = -1;
                    do
                    {
                        index = rand.Next(0, texts_fill_cells.Length);
                        index = texts_fill_cells[index];
                    } while (form.Data.Texts[index] == null || form.Data.Texts[index].Count == 0);

                    int list_index = rand.Next(0, form.Data.Texts[index].Count);
                    // Проверка, был ли этот текст
                    if (texts_was.Count > 0)
                    {
                        foreach (var str in texts_was)
                        {
                            if (form.Data.Texts[index].ElementAt(list_index).Key == str)
                            {
                                NextWordOrTextButton.PerformClick();
                                return;
                            }
                        }
                    }
                    // Устанавливает в текст бокс английский текст
                    if (rand.Next(0, 2) == 0)
                    {
                        language = Language.English;
                        WordOrTextTextBox.Text = form.Data.Texts[index].ElementAt(list_index).Key;
                    }
                    // Устанавливает в текст бокс перевод
                    else
                    {
                        language = Language.Russian;
                        int translation_index = rand.Next(0, form.Data.Texts[index].ElementAt(list_index).Value.Count);
                        WordOrTextTextBox.Text = form.Data.Texts[index].ElementAt(list_index).Value.ElementAt(translation_index);
                        if_russian_word_or_text = form.Data.Texts[index].ElementAt(list_index).Key;
                    }

                    if (texts_was.Count > 0)
                    {
                        // Если не заполнен, то продолжаем заполнение
                        if (texts_was.Count < texts_was_count) texts_was.AddLast(form.Data.Texts[index].ElementAt(list_index).Key);
                        // Иначе удаляем первое значение из очереди и добавляем текст в конец списка
                        else
                        {
                            texts_was.RemoveFirst();
                            texts_was.AddLast(form.Data.Texts[index].ElementAt(list_index).Key);
                        }
                    }
                }
                // Выбор из списка плохо усвоенных слов или предложений
                else if (rand_num > 1)
                {
                    // Устанавливаем в текст бокс английское слово или английский текст
                    if (rand.Next(0, 2) == 0)
                    {
                        WordOrTextTextBox.Text = form.Incomprehensible.First.Value.Key;
                        type = form.Incomprehensible.First.Value.Value;
                        language = Language.English;
                    }
                    // Устанавливаем в текст бокс русское слово или русский текст
                    else
                    {
                        if_russian_word_or_text = form.Incomprehensible.First.Value.Key;
                        type = form.Incomprehensible.First.Value.Value;
                        language = Language.Russian;
                        int index = form.Data.GetHash(if_russian_word_or_text, type);
                        KeyValuePair<string, LinkedListWithTranslations> pair = new KeyValuePair<string, LinkedListWithTranslations>();
                        if (type == WordsAndTextsData.WordOrText.Word)
                        {
                            foreach (var _pair in form.Data.Words[index])
                            {
                                if (if_russian_word_or_text == _pair.Key)
                                {
                                    pair = _pair;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var _pair in form.Data.Texts[index])
                            {
                                if (if_russian_word_or_text == _pair.Key)
                                {
                                    pair = _pair;
                                    break;
                                }
                            }
                        }
                        WordOrTextTextBox.Text = pair.Value.ElementAt(rand.Next(0, pair.Value.Count));
                    }
                }
            }
        };
        form.Controls.Add(NextWordOrTextButton);
    }

    private void SetVisibleFormElements()
    {
        BackToMainFormButton.Visible = true;
        WordOrTextLabel.Visible = true;
        WordOrTextTextBox.Visible = true;
        TranslationLabel.Visible = true;
        TranslationTextBox.Visible = true;
        CheckButton.Visible = true;
        ResultLabel.Visible = true;
        NextWordOrTextButton.Visible = true;
    }

    private void HideFormElements()
    {
        BackToMainFormButton.Visible = false;
        WordOrTextLabel.Visible = false;
        WordOrTextTextBox.Visible = false;
        TranslationLabel.Visible = false;
        TranslationTextBox.Visible = false;
        CheckButton.Visible = false;
        ResultLabel.Visible = false;
        NextWordOrTextButton.Visible = false;
    }

    private void BackToMainForm()
    {
        NextWordOrTextButton.Enabled = false;
        WordOrTextTextBox.Text = string.Empty;
        TranslationTextBox.Text = string.Empty;
        HideFormElements();
    }

    private void FirstTryAfterEntry(Form1 form)
    {
        // Нужно переделать реализацию! Возможно можно как-то связать остальные формы, чтобы они уведомляли эту форму,
        // когда происходит добавление или удаление элемента из словаря.
        int words_fill_cells_count = 0;
        int texts_fill_cells_count = 0;
        for (int i = 0; i < (form.Data.Words.Length > form.Data.Texts.Length ? form.Data.Words.Length : form.Data.Texts.Length); ++i)
        {
            if (i < form.Data.Words.Length && form.Data.Words[i] != null && form.Data.Words[i].Count > 0) ++words_fill_cells_count;
            if (i < form.Data.Texts.Length && form.Data.Texts[i] != null && form.Data.Texts[i].Count > 0) ++texts_fill_cells_count;
        }
        words_fill_cells = new int[words_fill_cells_count];
        texts_fill_cells = new int[texts_fill_cells_count];
        if (words_fill_cells_count == 0 && texts_fill_cells_count == 0) return;
        // Заполнение массивов номерами не пустых ячеек
        for (int i = 0, w = 0, t = 0; i < (form.Data.Words.Length > form.Data.Texts.Length ? form.Data.Words.Length : form.Data.Texts.Length); ++i)
        {
            if (i < form.Data.Words.Length && form.Data.Words[i] != null && form.Data.Words[i].Count > 0) words_fill_cells[w++] = i;
            if (i < form.Data.Texts.Length && form.Data.Texts[i] != null && form.Data.Texts[i].Count > 0) texts_fill_cells[t++] = i;
        }
        words_was_count = words_fill_cells_count / 2;
        texts_was_count = texts_fill_cells_count / 2;
        words_was = new LinkedList<string>();
        texts_was = new LinkedList<string>();

    }

    private enum Language
    {
        English,
        Russian
    }
}
