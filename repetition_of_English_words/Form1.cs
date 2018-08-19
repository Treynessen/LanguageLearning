using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using Pair = System.Collections.Generic.KeyValuePair<string, WordsAndTexts.Type>;

namespace repetition_of_English_words
{
    public partial class Form1 : Form
    {
        FileStream data_file = null;
        WordsAndTexts data;
        LinkedList<Pair> incomprehensible;
        LinkedList<Pair> words_and_texts_was;
        int[] words_filled_cells;
        int[] texts_filled_cells;
        FormAddWordsOrText add_word_form, add_text_form;
        StartForm start_form;

        public Pair WordOrText
        {
            get
            {
                Random rand = new Random();
                int rand_num;
                if (words_filled_cells.Length == 0 && texts_filled_cells.Length == 0) return new Pair("", WordsAndTexts.Type.None);
                else if (words_filled_cells.Length == 0) rand_num = 1;
                else if (texts_filled_cells.Length == 0) rand_num = 0;
                else if (incomprehensible.Count > 0) rand_num = rand.Next(0, 3);
                else rand_num = rand.Next(0, 2);
                if (rand_num == 0)
                {
                    int index = rand.Next(0, words_filled_cells.Length);
                    index = words_filled_cells[index];
                    rand_num = rand.Next(0, data.Words[index].Count);
                    int count = 0;
                    foreach (var w in data.Words[index]) if (count++ >= rand_num) return new Pair(w.Key, WordsAndTexts.Type.Word);
                }
                else if (rand_num == 1)
                {
                    int index = rand.Next(0, texts_filled_cells.Length);
                    index = texts_filled_cells[index];
                    rand_num = rand.Next(0, data.Texts[index].Count);
                    int count = 0;
                    foreach (var w in data.Texts[index]) if (count++ >= rand_num) return new Pair(w.Key, WordsAndTexts.Type.Text);
                }
                else
                {
                    return incomprehensible.First.Value;
                }
                return new Pair(data.Words[words_filled_cells[0]].First.Value.Key, WordsAndTexts.Type.Word);
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
            add_word_form.AddWordOrTextEvent((_sender, _e) =>
            {
                string word = add_word_form.WordOrText;
                if (word != "")
                {
                    string[] translations = add_word_form.Translations;
                    if (translations.Length == 1) data.AddWord(word, translations[0]);
                    else if (translations.Length == 2) data.AddWord(word, translations[0], translations[1]);
                    else if (translations.Length == 3) data.AddWord(word, translations[0], translations[1], translations[2]);
                    else if (translations.Length == 4) data.AddWord(word, translations[0], translations[1], translations[2], translations[3]);
                    else if (translations.Length == 5) data.AddWord(word, translations[0], translations[1], translations[2], translations[3], translations[4]);
                    else if (translations.Length > 5) data.AddWord(word, translations);
                    StartButton.Enabled = true;
                    GetFilledCells();
                }
            });
            add_text_form.AddWordOrTextEvent((_sender, _e) =>
            {
                string text = add_text_form.WordOrText;
                if (text != "")
                {
                    string[] translations = add_text_form.Translations;
                    if (translations.Length == 1) data.AddText(text, translations[0]);
                    else if (translations.Length == 2) data.AddText(text, translations[0], translations[1]);
                    else if (translations.Length == 3) data.AddText(text, translations[0], translations[1], translations[2]);
                    else if (translations.Length == 4) data.AddText(text, translations[0], translations[1], translations[2], translations[3]);
                    else if (translations.Length == 5) data.AddText(text, translations[0], translations[1], translations[2], translations[3], translations[4]);
                    else if (translations.Length > 5) data.AddText(text, translations);
                    StartButton.Enabled = true;
                    GetFilledCells();
                }
            });
            start_form.CheckEvent((_sender, _e) =>
            {
                string word_or_text = start_form.WordOrText;
                string translate = start_form.Translation;
                if (start_form.Type == WordsAndTexts.Type.Text)
                {
                    int index = data.GetIndex(word_or_text, data.Texts.Length);
                    LinkedList<string> translates = null;
                    foreach (var kv in data.Texts[index])
                    {
                        if (kv.Key == word_or_text)
                        {
                            translates = kv.Value;
                            break;
                        }
                    }
                    if (translates != null)
                    {
                        if (translates.Find(translate) != null)
                        {
                            start_form.SetWordOrText(WordOrText);
                        }
                        else
                        {

                        }
                    }
                }
                else if (start_form.Type == WordsAndTexts.Type.Word)
                {
                    int index = data.GetIndex(word_or_text, data.Words.Length);
                    LinkedList<string> translates = null;
                    foreach (var kv in data.Words[index])
                    {
                        if (kv.Key == word_or_text)
                        {
                            translates = kv.Value;
                            break;
                        }
                    }
                    if (translates != null)
                    {
                        if (translates.Find(translate) != null)
                        {
                            start_form.SetWordOrText(WordOrText);
                        }
                        else
                        {

                        }
                    }
                }
            });
            try
            {
                data_file = new FileStream("words.data", FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл со словарем не найден");
                StartButton.Enabled = false;
                return;
            }
            Serialize serialize = Serialize.DesirializeFromFile(data_file);
            if (serialize == null)
            {
                data_file.Close();
                File.Delete("words.data");
                StartButton.Enabled = false;
                return;
            }
            data = serialize.WordsAndText_DATA;
            incomprehensible = serialize.Incomprehensible;
            GetFilledCells();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Visible = false;
            AddWordsButton.Visible = false;
            AddTextsButton.Visible = false;
            start_form.SetWordOrText(WordOrText);
        }

        private void AddWordsButton_Click(object sender, EventArgs e)
        {
            StartButton.Visible = false;
            AddWordsButton.Visible = false;
            AddTextsButton.Visible = false;
            if (data_file == null) data_file = new FileStream("words.data", FileMode.Create);
            if (data == null) data = new WordsAndTexts();
            if (incomprehensible == null) incomprehensible = new LinkedList<Pair>();
        }

        private void AddTextsButton_Click(object sender, EventArgs e)
        {
            StartButton.Visible = false;
            AddWordsButton.Visible = false;
            AddTextsButton.Visible = false;
            if (data_file == null) data_file = new FileStream("words.data", FileMode.Create);
            if (data == null) data = new WordsAndTexts();
            if (incomprehensible == null) incomprehensible = new LinkedList<Pair>();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (data_file != null) data_file.Close();
            if (data != null && incomprehensible != null)
            {
                using (data_file = new FileStream("words.data", FileMode.Truncate))
                    Serialize.SerializeToFile(data_file, new Serialize(data, incomprehensible));
            }

        }

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

        private void BackToMainForm(object sender, EventArgs e)
        {
            StartButton.Visible = true;
            AddWordsButton.Visible = true;
            AddTextsButton.Visible = true;
        }
    }
}
