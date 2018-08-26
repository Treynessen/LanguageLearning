using System.Drawing;
using System.Windows.Forms;

public sealed class TrainingForm : FormStruct
{
    private Label WordOrTextLabel;
    private TextBox WordOrTextTextBox;
    private Label TranslationLabel;
    private TextBox TranslationTextBox;
    private Button CheckButton;
    private Label ResultLabel;
    private Button NextWordOrTextButton;

    public TrainingForm(Form1 form, Button activate_form_button) : base(form)
    {
        activate_form_button.Click += (sender, e) => SetVisibleFormElements();
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
        NextWordOrTextButton.Visible = false;
    }

    private void BackToMainForm()
    {

        HideFormElements();
    }
}
