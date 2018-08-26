using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using IncomprehensiblePair = System.Collections.Generic.KeyValuePair<string, WordsAndTextsData.WordOrText>;

public partial class Form1 : Form
{
    private FileStream data_file;
    private FormStruct training_form, add_word_form, add_text_form, dictionary_edit_form;
    public WordsAndTextsData Data { get; private set; }
    public LinkedList<IncomprehensiblePair> Incomprehensible { get; private set; }

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        training_form = new TrainingForm(this, TrainingButton, AddIncomprehensible);
        add_word_form = new AddWordsOrTextForms(this, AddWordsButton, CreateData, AddWordsOrTextForms.AddWordsOrTextFormsType.AddWordForm);
        add_text_form = new AddWordsOrTextForms(this, AddTextsButton, CreateData, AddWordsOrTextForms.AddWordsOrTextFormsType.AddTextForm);
        dictionary_edit_form = new DictionaryEditForm(this, DictionaryEditButton);

        training_form.BackToMainFormEvent += SetVisibleMainFormElements;
        add_word_form.BackToMainFormEvent += SetVisibleMainFormElements;
        add_text_form.BackToMainFormEvent += SetVisibleMainFormElements;
        dictionary_edit_form.BackToMainFormEvent += SetVisibleMainFormElements;

        try
        {
            data_file = new FileStream("words.data", FileMode.Open);
        }
        catch (FileNotFoundException)
        {
            DictionaryEditButton.Enabled = false;
            TrainingButton.Enabled = false;
            MessageBox.Show("Файл со словарем не найден");
            return;
        }

        Container data = Serialize.DesirializeFromFile(data_file);
        if (data == null)
        {
            DictionaryEditButton.Enabled = false;
            TrainingButton.Enabled = false;
            data_file.Close();
            return;
        }
        Data = new WordsAndTextsData(data.Words, data.Texts);
        Incomprehensible = data.Incomprehensible;
    }

    /*==================== Кнопки ====================*/

    private void TrainingButton_Click(object sender, EventArgs e)
    {
        HideMainFormElements();
    }

    private void AddWordsButton_Click(object sender, EventArgs e)
    {
        HideMainFormElements();
    }

    private void AddTextsButton_Click(object sender, EventArgs e)
    {
        HideMainFormElements();
    }

    private void DictionaryEditButton_Click(object sender, EventArgs e)
    {
        HideMainFormElements();
    }

    /*================================================*/

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (Data != null)
        {
            if (data_file == null) File.Create("words.data").Close();
            else data_file.Close();
            using (data_file = new FileStream("words.data", FileMode.Truncate))
                Serialize.SerializeToFile(data_file, new Container(Data.Words, Data.Texts, Incomprehensible));
        }
    }

    /*==================== Методы ====================*/

    private void SetVisibleMainFormElements()
    {
        TrainingButton.Visible = true;
        AddWordsButton.Visible = true;
        AddTextsButton.Visible = true;
        DictionaryEditButton.Visible = true;
    }

    private void HideMainFormElements()
    {
        TrainingButton.Visible = false;
        AddWordsButton.Visible = false;
        AddTextsButton.Visible = false;
        DictionaryEditButton.Visible = false;
    }

    private void CreateData()
    {
        DictionaryEditButton.Enabled = true;
        TrainingButton.Enabled = true;
        Data = new WordsAndTextsData();
    }

    private void AddIncomprehensible(IncomprehensiblePair pair)
    {
        if (Incomprehensible == null) Incomprehensible = new LinkedList<IncomprehensiblePair>();
        Incomprehensible.AddLast(pair);
    }

    /*================================================*/
}
