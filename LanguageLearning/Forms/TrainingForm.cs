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

    private static string right = "Правильно!";
    private static string wrong = "Неправильно!";

    private string word_or_text_if_use_russian = null;  // Если вопрос на русском, то сюда записывается английский перевод. 
                                                        // Если дан неправильный ответ, то слово из этого поля записывается в 
                                                        // массив Incomprehensible
    private int[] words_fill_cells;
    private int[] texts_fill_cells;
    private bool was_changes_in_words = true;
    private bool was_changes_in_texts = true;

    int words_was_max_count = 0;
    int texts_was_max_count = 0;
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
        WordOrTextLabel.Text = word_label;
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
        WordOrTextTextBox.Enabled = false;
        form.Controls.Add(WordOrTextTextBox);

        TranslationLabel = new Label();
        TranslationLabel.Text = translation_label;
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
                    word_or_text = language == Language.English ? WordOrTextTextBox.Text : TranslationTextBox.Text;
                else
                    word_or_text = language == Language.English ? WordOrTextTextBox.Text : TranslationTextBox.Text;
                int index = form.Data.GetHash(word_or_text, type);
                // Получение пары слово - переводы
                if (type == WordsAndTextsData.WordOrText.Word && form.Data.Words[index] != null)
                {
                    foreach (var _pair in form.Data.Words[index])
                    {
                        if (_pair.Key.Equals(word_or_text, StringComparison.CurrentCultureIgnoreCase))
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
                        if (_pair.Key.Equals(word_or_text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            pair = _pair;
                            break;
                        }
                    }
                }
                // Если пара существует
                if (pair.Key != null && pair.Key != string.Empty)
                {

                    word_or_text = language == Language.English ? TranslationTextBox.Text : WordOrTextTextBox.Text;
                    ResultLabel.Text = wrong;
                    foreach (var str in pair.Value)
                    {
                        if (str.Equals(word_or_text, StringComparison.CurrentCultureIgnoreCase))
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
                    if (form.Incomprehensible != null && form.Incomprehensible.Count > 0)
                    {
                        foreach (var _pair in form.Incomprehensible)
                        {
                            if ((language == Language.English ? WordOrTextTextBox.Text : word_or_text_if_use_russian) == _pair.Key)
                            {
                                have = true;
                                break;
                            }
                        }
                    }
                    if (!have || form.Incomprehensible == null && form.Incomprehensible.Count < 1) add_incomprehensible(new IncomprehensiblePair(language == Language.English ? WordOrTextTextBox.Text : word_or_text_if_use_russian, type));
                }
                // Если дан правильный ответ, то удаляем слово из списка
                else
                {
                    if (form.Incomprehensible != null)
                    {
                        foreach (var _pair in form.Incomprehensible)
                        {
                            if ((language == Language.English && _pair.Key == WordOrTextTextBox.Text)
                            || (language == Language.Russian && _pair.Key == word_or_text_if_use_russian))
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
            Random rand = new Random();
            word_or_text_if_use_russian = null;

            ResultLabel.Text = string.Empty;
            TranslationTextBox.Text = string.Empty;
            CheckButton.Enabled = true;

            if (words_fill_cells.Length != 0 || texts_fill_cells.Length != 0 || form.Incomprehensible != null)
            {
                int rand_num = -1;
                if (words_fill_cells.Length != 0 && texts_fill_cells.Length != 0 && (form.Incomprehensible == null || form.Incomprehensible.Count < 1)) rand_num = rand.Next(0, 2);
                else if (words_fill_cells.Length != 0 && texts_fill_cells.Length != 0 && form.Incomprehensible != null && form.Incomprehensible.Count > 0) rand_num = rand.Next(0, 4); // повышенный шанс
                else if (form.Incomprehensible != null && form.Incomprehensible.Count > 0) rand_num = 2;
                else if (words_fill_cells.Length != 0) rand_num = 0;
                else if (texts_fill_cells.Length != 0) rand_num = 1;
                // Выбор слова или предложения
                if (rand_num < 2)
                {
                    LinkedList<KeyValuePair<string, LinkedListWithTranslations>> pair_list = null;
                    if (rand_num == 0)
                    {
                        WordOrTextLabel.Text = word_label;
                        type = WordsAndTextsData.WordOrText.Word;
                        pair_list = form.Data.Words[words_fill_cells[rand.Next(0, words_fill_cells.Length)]];
                    }
                    else
                    {
                        WordOrTextLabel.Text = text_label;
                        type = WordsAndTextsData.WordOrText.Text;
                        pair_list = form.Data.Texts[texts_fill_cells[rand.Next(0, texts_fill_cells.Length)]];
                    }
                    KeyValuePair<string, LinkedListWithTranslations> pair = pair_list.ElementAt(rand.Next(0, pair_list.Count));

                    // Проверка было ли это слово/предложение ранее
                    if ((rand_num == 0 ? words_was.Count : texts_was.Count) > 0)
                    {
                        foreach (var str in rand_num == 0 ? words_was : texts_was)
                        {
                            if (pair.Key.Equals(str, StringComparison.CurrentCultureIgnoreCase))
                            {
                                NextWordOrTextButton.PerformClick();
                                return;
                            }
                        }
                    }

                    // Устанавливаем в WordOrTextTextBox английское слово/предложение
                    if (rand.Next(0, 2) == 0)
                    {
                        language = Language.English;
                        WordOrTextTextBox.Text = pair.Key;
                    }
                    // Устанавливаем в WordOrTextTextBox русское слово/предложение
                    else
                    {
                        language = Language.Russian;
                        word_or_text_if_use_russian = pair.Key;
                        WordOrTextTextBox.Text = pair.Value.ElementAt(rand.Next(0, pair.Value.Count));
                    }
                    // Добавление слова/предложения в список исключений
                    if ((rand_num == 0 ? words_was_max_count : texts_was_max_count) > 0)
                    {
                        if (rand_num == 0)
                        {
                            // Если не заполнен, то продолжаем заполнение
                            if (words_was.Count < words_was_max_count)
                                words_was.AddLast(pair.Key);
                            // Иначе удаляем первое значение из очереди и добавляем слово в конец списка
                            else
                            {
                                words_was.RemoveFirst();
                                words_was.AddLast(pair.Key);
                            }
                        }
                        else
                        {
                            // Если не заполнен, то продолжаем заполнение
                            if (texts_was.Count < texts_was_max_count)
                                texts_was.AddLast(pair.Key);
                            // Иначе удаляем первое значение из очереди и добавляем слово в конец списка
                            else
                            {
                                texts_was.RemoveFirst();
                                texts_was.AddLast(pair.Key);
                            }
                        }
                    }
                }

                // Выбор из списка плохо усвоенных слов или предложений
                else
                {
                    type = form.Incomprehensible.First.Value.Value;
                    WordOrTextLabel.Text = type == WordsAndTextsData.WordOrText.Word ? word_label : text_label;
                    // Устанавливаем в WordOrTextTextBox английское слово/предложение
                    if (rand.Next(0, 2) == 0)
                    {
                        language = Language.English;
                        WordOrTextTextBox.Text = form.Incomprehensible.First.Value.Key;
                    }
                    // Устанавливаем в WordOrTextTextBox русское слово/предложение
                    else
                    {
                        language = Language.Russian;
                        word_or_text_if_use_russian = form.Incomprehensible.First.Value.Key;
                        LinkedListWithTranslations translations_list = form.Data.GetWordOrTextTranslation(word_or_text_if_use_russian, form.Incomprehensible.First.Value.Value);
                        WordOrTextTextBox.Text = translations_list.ElementAt(rand.Next(0, translations_list.Count));
                    }
                }
            }
        };
        form.Controls.Add(NextWordOrTextButton);

        form.DictionaryChangesObserver.WordOrTextAddedEvent += (sender, e) =>
        {
            if (e.Type == TypeChanges.WordAdded) was_changes_in_words = true;
            if (e.Type == TypeChanges.TextAdded) was_changes_in_texts = true;
        };

        form.DictionaryChangesObserver.WordOrTextDeletedEvent += (sender, e) =>
        {
            if (e.Type == TypeChanges.WordDeleted) was_changes_in_words = true;
            if (e.Type == TypeChanges.TextDeleted) was_changes_in_texts = true;
        };
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
        LinkedList<int> words_fill_cells_nums = null;
        if (was_changes_in_words) words_fill_cells_nums = new LinkedList<int>();
        LinkedList<int> texts_fill_cells_nums = null;
        if (was_changes_in_texts) texts_fill_cells_nums = new LinkedList<int>();

        for (int i = 0; i < (form.Data.Words.Length > form.Data.Texts.Length ? form.Data.Words.Length : form.Data.Texts.Length); ++i)
        {
            if (was_changes_in_words && i < form.Data.Words.Length && form.Data.Words[i] != null && form.Data.Words[i].Count > 0) words_fill_cells_nums.AddLast(i);
            if (was_changes_in_texts && i < form.Data.Texts.Length && form.Data.Texts[i] != null && form.Data.Texts[i].Count > 0) texts_fill_cells_nums.AddLast(i);
        }
        if (was_changes_in_words) words_fill_cells = new int[words_fill_cells_nums.Count];
        if (was_changes_in_texts) texts_fill_cells = new int[texts_fill_cells_nums.Count];
        if ((words_fill_cells_nums == null || words_fill_cells_nums.Count == 0)
            && (texts_fill_cells_nums == null || texts_fill_cells_nums.Count == 0)) return;
        // Заполнение массивов номерами не пустых ячеек
        {
            // wn - words_node
            // tn - texts_node
            // w - переменная для итерирования по массиву с адресами ячеек со словами
            // t - переменная для итерирования по массиву с адресами ячеек с текстом
            LinkedListNode<int> wn = was_changes_in_words ? words_fill_cells_nums.First : null;
            LinkedListNode<int> tn = was_changes_in_texts ? texts_fill_cells_nums.First : null;
            for (int w = 0, t = 0; wn != null || tn != null; wn = wn != null ? wn.Next : null, tn = tn != null ? tn.Next : null)
            {
                if (wn != null) words_fill_cells[w++] = wn.Value;
                if (tn != null) texts_fill_cells[t++] = tn.Value;
            }
        }
        if (was_changes_in_words)
        {
            words_was_max_count = words_fill_cells_nums.Count / 2;
            words_was = new LinkedList<string>();
        }
        if (was_changes_in_texts)
        {
            texts_was_max_count = texts_fill_cells_nums.Count / 2;
            texts_was = new LinkedList<string>();
        }
        was_changes_in_words = false;
        was_changes_in_texts = false;
    }

    private enum Language
    {
        English,
        Russian
    }
}
