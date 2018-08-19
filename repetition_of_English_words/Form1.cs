using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace repetition_of_English_words
{
    public partial class Form1 : Form
    {
        FileStream data_file = null;
        WordsAndTexts data;
        LinkedList<string> incomprehensible;
        FormAddWordsOrText add_word_form, add_text_form;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            add_word_form = new FormAddWordsOrText(this, AddWordsButton, BackToMainForm, FormStruct.Form.AddWordForm);
            add_text_form = new FormAddWordsOrText(this, AddTextsButton, BackToMainForm, FormStruct.Form.AddTextForm);
            add_word_form.AddWordOrTextEvent((_sender, _e) =>
            {
                string[] translations = add_word_form.Translations;
                if (translations.Length == 1) data.AddWord(add_word_form.WordOrText, translations[0]);
                else if (translations.Length == 2) data.AddWord(add_word_form.WordOrText, translations[0], translations[1]);
                else if (translations.Length == 3) data.AddWord(add_word_form.WordOrText, translations[0], translations[1], translations[2]);
                else if (translations.Length == 4) data.AddWord(add_word_form.WordOrText, translations[0], translations[1], translations[2], translations[3]);
                else if (translations.Length == 5) data.AddWord(add_word_form.WordOrText, translations[0], translations[1], translations[2], translations[3], translations[4]);
                else if (translations.Length > 5) data.AddWord(add_word_form.WordOrText, translations);
            });
            add_text_form.AddWordOrTextEvent((_sender, _e) =>
            {
                string[] translations = add_text_form.Translations;
                if (translations.Length == 1) data.AddWord(add_text_form.WordOrText, translations[0]);
                else if (translations.Length == 2) data.AddText(add_text_form.WordOrText, translations[0], translations[1]);
                else if (translations.Length == 3) data.AddText(add_text_form.WordOrText, translations[0], translations[1], translations[2]);
                else if (translations.Length == 4) data.AddText(add_text_form.WordOrText, translations[0], translations[1], translations[2], translations[3]);
                else if (translations.Length == 5) data.AddText(add_text_form.WordOrText, translations[0], translations[1], translations[2], translations[3], translations[4]);
                else if (translations.Length > 5) data.AddText(add_text_form.WordOrText, translations);
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
            data_file.Close();
            data = serialize.WordsAndText_DATA;
            incomprehensible = serialize.Incomprehensible;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {

        }

        private void AddWordsButton_Click(object sender, EventArgs e)
        {
            StartButton.Visible = false;
            AddWordsButton.Visible = false;
            AddTextsButton.Visible = false;
            if (data_file == null) data_file = new FileStream("words.data", FileMode.Create);
            if (data == null) data = new WordsAndTexts();
            if (incomprehensible == null) incomprehensible = new LinkedList<string>();
        }

        private void AddTextsButton_Click(object sender, EventArgs e)
        {
            StartButton.Visible = false;
            AddWordsButton.Visible = false;
            AddTextsButton.Visible = false;
            if (data_file == null) data_file = new FileStream("words.data", FileMode.Create);
            if (data == null) data = new WordsAndTexts();
            if (incomprehensible == null) incomprehensible = new LinkedList<string>();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (data != null && incomprehensible != null)
            {
                using (data_file = new FileStream("words.data", FileMode.Truncate))
                    Serialize.SerializeToFile(data_file, new Serialize(data, incomprehensible));
            }
            if (data_file != null) data_file.Close();
        }

        private void BackToMainForm(object sender, EventArgs e)
        {
            StartButton.Visible = true;
            AddWordsButton.Visible = true;
            AddTextsButton.Visible = true;
        }
    }
}
