using System;
using System.Windows.Forms;
using System.IO;

namespace repetition_of_English_words
{
    public partial class Form1 : Form
    {
        FileStream data_file = null;
        WordsAndTexts data;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            data = serialize.WordsAndText_DATA;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {

        }

        private void AddWordsButton_Click(object sender, EventArgs e)
        {

        }

        private void AddTextsButton_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (data != null && data_file != null) Serialize.SerializeToFile(data_file, new Serialize(data));
            if (data_file != null) data_file.Close();
        }
    }
}
