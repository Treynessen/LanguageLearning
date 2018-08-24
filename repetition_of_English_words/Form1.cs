using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using KeyListPair = System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.LinkedList<string>>;
using Pair = System.Collections.Generic.KeyValuePair<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.LinkedList<string>>, WordsAndTexts.Type>;
using StringList = System.Collections.Generic.LinkedList<string>;

namespace repetition_of_English_words
{
    public partial class Form1 : Form
    {
        FileStream data_file = null;
        WordsAndTexts data;
        LinkedList<Pair> incomprehensible; // Плохо усвоенные слова
        LinkedList<Pair> words_and_texts_was; //
        int[] words_filled_cells; // Номера заполненных ячеек в хэш-таблице Words
        int[] texts_filled_cells; // Номера заполненных ячеек в хэш-таблице Texts
        FormAddWordsOrText add_word_form, add_text_form; // Формы добавления слов и текста
        StartForm start_form; // Форма, открывающаяся по нажатию кнопки "Начать"
        DictionaryEdit dictionary_edit; // Форма редактирования словаря

        // Возвращает слово или текст из хэш-таблицы
        public Pair WordOrText
        {
            get
            {
                Random rand = new Random();
                int rand_num = 0;
                if (words_filled_cells.Length == 0 && texts_filled_cells.Length == 0) return new Pair(new KeyListPair("", null), WordsAndTexts.Type.None);
                else if (words_filled_cells.Length == 0) rand_num = 1;
                else if (texts_filled_cells.Length == 0) rand_num = 0;
                else if (incomprehensible.Count > 0) rand_num = rand.Next(0, 4);
                else rand_num = rand.Next(0, 2);
                if (rand_num == 0)
                {
                    int index = rand.Next(0, words_filled_cells.Length);
                    index = words_filled_cells[index];
                    rand_num = rand.Next(0, data.Words[index].Count);
                    int count = 0;
                    foreach (var w in data.Words[index]) if (count++ >= rand_num) return new Pair(w, WordsAndTexts.Type.Word);
                }
                else if (rand_num == 1)
                {
                    int index = rand.Next(0, texts_filled_cells.Length);
                    index = texts_filled_cells[index];
                    rand_num = rand.Next(0, data.Texts[index].Count);
                    int count = 0;
                    foreach (var w in data.Texts[index]) if (count++ >= rand_num) return new Pair(w, WordsAndTexts.Type.Text);
                }
                else
                {
                    return incomprehensible.First.Value;
                }
                return new Pair(data.Words[words_filled_cells[0]].First.Value, WordsAndTexts.Type.Word);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            add_word_form = new FormAddWordsOrText(this, AddWordsButton, BackToMainForm, FormStruct.Form.AddWordForm);
            add_text_form = new FormAddWordsOrText(this, AddTextsButton, BackToMainForm, FormStruct.Form.AddTextForm);
            start_form = new StartForm(this, StartButton, BackToMainForm);
            dictionary_edit = new DictionaryEdit(this, DictionaryEdit, BackToMainForm, DeleteWordOrText);

            // События для кнопок добавить слово/текст
            add_word_form.AddWordOrTextEvent((_sender, _e) =>
            {
                string word = add_word_form.WordOrText;
                if (word != "")
                {
                    if (data_file == null) data_file = new FileStream("words.data", FileMode.Create);
                    if (data == null) data = new WordsAndTexts();
                    if (incomprehensible == null) incomprehensible = new LinkedList<Pair>();

                    string[] translations = add_word_form.Translations;
                    if (translations.Length == 1) data.AddWord(word, translations[0]);
                    else if (translations.Length == 2) data.AddWord(word, translations[0], translations[1]);
                    else if (translations.Length == 3) data.AddWord(word, translations[0], translations[1], translations[2]);
                    else if (translations.Length == 4) data.AddWord(word, translations[0], translations[1], translations[2], translations[3]);
                    else if (translations.Length == 5) data.AddWord(word, translations[0], translations[1], translations[2], translations[3], translations[4]);
                    else if (translations.Length > 5) data.AddWord(word, translations);
                    StartButton.Enabled = true;
                    DictionaryEdit.Enabled = true;
                    add_word_form.Clear();
                    GetFilledCells();
                    MessageBox.Show("Добавлено в словарь");
                }
            });
            add_text_form.AddWordOrTextEvent((_sender, _e) =>
            {
                string text = add_text_form.WordOrText;
                if (text != "")
                {
                    if (data_file == null) data_file = new FileStream("words.data", FileMode.Create);
                    if (data == null) data = new WordsAndTexts();
                    if (incomprehensible == null) incomprehensible = new LinkedList<Pair>();

                    string[] translations = add_text_form.Translations;
                    if (translations.Length == 1) data.AddText(text, translations[0]);
                    else if (translations.Length == 2) data.AddText(text, translations[0], translations[1]);
                    else if (translations.Length == 3) data.AddText(text, translations[0], translations[1], translations[2]);
                    else if (translations.Length == 4) data.AddText(text, translations[0], translations[1], translations[2], translations[3]);
                    else if (translations.Length == 5) data.AddText(text, translations[0], translations[1], translations[2], translations[3], translations[4]);
                    else if (translations.Length > 5) data.AddText(text, translations);
                    StartButton.Enabled = true;
                    DictionaryEdit.Enabled = true;
                    add_text_form.Clear();
                    GetFilledCells();
                    MessageBox.Show("Добавлено в словарь");
                }
            });
            
            // Событие для кнопки "Проверить"
            start_form.CheckEvent((_sender, _e) =>
            {
                string word_or_text = start_form.WordOrText;
                if (word_or_text != "")
                {
                    bool right = start_form.Check();
                    if (right) start_form.SetText(true);
                    else
                    {
                        if (start_form.WordOrTranslation == StartForm.WordType.Original) start_form.SetText(false);
                        else
                        {
                            string translation = start_form.Translation;
                            LinkedList<KeyValuePair<string, StringList>> list = null;
                            if (start_form.Type == WordsAndTexts.Type.Text) list = data.Texts[data.GetIndex(translation, data.Texts.Length)];
                            else list = data.Words[data.GetIndex(translation, data.Words.Length)];
                            if (list != null)
                            {
                                string word = start_form.WordOrText;
                                bool find = false;
                                foreach (var l in list)
                                {
                                    if (l.Key == translation)
                                    {
                                        foreach (var v in l.Value)
                                        {
                                            if (v == word)
                                            {
                                                start_form.SetText(true);
                                                find = true;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                if (!find) start_form.SetText(false);
                            }
                            else start_form.SetText(false);
                        }
                    }
                }
                else MessageBox.Show("Словарь не заполнен", "Ошибка");
            });
            
            // Событие для кнопки "Далее"
            start_form.NextButtonEvent((_sender, _e) =>
            {
                start_form.SetWordOrText(WordOrText);
                start_form.ClearText();
            });
            
            // Проверка наличия файла со словарем
            try
            {
                data_file = new FileStream("words.data", FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл со словарем не найден");
                StartButton.Enabled = false;
                DictionaryEdit.Enabled = false;
                return;
            }
            Container serialize = Serialize.DesirializeFromFile(data_file);
            if (serialize == null)
            {
                data_file.Close();
                File.Delete("words.data");
                StartButton.Enabled = false;
                return;
            }

            data = new WordsAndTexts(serialize.Words, serialize.Texts);
            incomprehensible = serialize.Incomprehensible;
            GetFilledCells(); // Заполнение массивов, хранящих количество заполненных ячеек в хэш-таблицах
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            LeaveMainForm();
            start_form.SetWordOrText(WordOrText); // Слово для тестирования
        }

        private void AddWordsButton_Click(object sender, EventArgs e)
        {
            LeaveMainForm();
        }

        private void AddTextsButton_Click(object sender, EventArgs e)
        {
            LeaveMainForm();
        }

        private void DictionaryEdit_Click(object sender, EventArgs e)
        {
            LeaveMainForm();
            dictionary_edit.WordsAndTextsData(data.Words, data.Texts);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (data_file != null) data_file.Close();
            if (data != null && incomprehensible != null)
            {
                using (data_file = new FileStream("words.data", FileMode.Truncate))
                    Serialize.SerializeToFile(data_file, new Container(data.Words, data.Texts, incomprehensible));
            }

        }

        // Заполнение массивов, хранящих количество заполненных ячеек в хэш-таблицах
        private void GetFilledCells()
        {
            int count_words = 0, count_texts = 0;
            for (int i = 0; i < (data.Words.Length > data.Texts.Length ? data.Words.Length : data.Texts.Length); ++i)
            {
                if (data.Words.Length > i && data.Words[i] != null) ++count_words;
                if (data.Texts.Length > i && data.Texts[i] != null) ++count_texts;
            }
            words_filled_cells = new int[count_words];
            texts_filled_cells = new int[count_texts];
            for (int i = 0, w = 0, t = 0; i < (data.Words.Length > data.Texts.Length ? data.Words.Length : data.Texts.Length); ++i)
            {
                if (data.Words.Length > i && data.Words[i] != null) words_filled_cells[w++] = i;
                if (data.Texts.Length > i && data.Texts[i] != null) texts_filled_cells[t++] = i;
            }

        }

        private void LeaveMainForm()
        {
            StartButton.Visible = false;
            AddWordsButton.Visible = false;
            AddTextsButton.Visible = false;
            DictionaryEdit.Visible = false;
        }

        private void BackToMainForm(object sender, EventArgs e)
        {
            StartButton.Visible = true;
            AddWordsButton.Visible = true;
            AddTextsButton.Visible = true;
            DictionaryEdit.Visible = true;
        }

        private void DeleteWordOrText(string key, WordsAndTexts.Type type)
        {
            if(type == WordsAndTexts.Type.Text)
            {
                var pairs = data.Texts[data.GetIndex(key, data.Texts.Length)];
                foreach(var pair in pairs)
                {
                    if (pair.Key == key)
                    {
                        pairs.Remove(pair);
                        break;
                    }
                }
            }
            else
            {
                var pairs = data.Words[data.GetIndex(key, data.Words.Length)];
                foreach (var pair in pairs)
                {
                    if (pair.Key == key)
                    {
                        pairs.Remove(pair);
                        break;
                    }
                }
            }
        }
    }
}