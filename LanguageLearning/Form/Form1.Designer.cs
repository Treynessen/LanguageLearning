
partial class Form1
{
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TrainingButton = new System.Windows.Forms.Button();
            this.AddWordsButton = new System.Windows.Forms.Button();
            this.AddTextsButton = new System.Windows.Forms.Button();
            this.DictionaryEditButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TrainingButton
            // 
            this.TrainingButton.Font = new System.Drawing.Font("Consolas", 18F);
            this.TrainingButton.Location = new System.Drawing.Point(262, 120);
            this.TrainingButton.Name = "TrainingButton";
            this.TrainingButton.Size = new System.Drawing.Size(360, 78);
            this.TrainingButton.TabIndex = 0;
            this.TrainingButton.Text = "Тренировка";
            this.TrainingButton.UseVisualStyleBackColor = true;
            this.TrainingButton.Click += new System.EventHandler(this.TrainingButton_Click);
            // 
            // AddWordsButton
            // 
            this.AddWordsButton.Font = new System.Drawing.Font("Consolas", 18F);
            this.AddWordsButton.Location = new System.Drawing.Point(120, 246);
            this.AddWordsButton.Name = "AddWordsButton";
            this.AddWordsButton.Size = new System.Drawing.Size(282, 78);
            this.AddWordsButton.TabIndex = 1;
            this.AddWordsButton.Text = "Добавить слова";
            this.AddWordsButton.UseVisualStyleBackColor = true;
            this.AddWordsButton.Click += new System.EventHandler(this.AddWordsButton_Click);
            // 
            // AddTextsButton
            // 
            this.AddTextsButton.Font = new System.Drawing.Font("Consolas", 18F);
            this.AddTextsButton.Location = new System.Drawing.Point(482, 246);
            this.AddTextsButton.Name = "AddTextsButton";
            this.AddTextsButton.Size = new System.Drawing.Size(282, 78);
            this.AddTextsButton.TabIndex = 2;
            this.AddTextsButton.Text = "Добавить предложения";
            this.AddTextsButton.UseVisualStyleBackColor = true;
            this.AddTextsButton.Click += new System.EventHandler(this.AddTextsButton_Click);
            // 
            // DictionaryEditButton
            // 
            this.DictionaryEditButton.Font = new System.Drawing.Font("Consolas", 18F);
            this.DictionaryEditButton.Location = new System.Drawing.Point(262, 372);
            this.DictionaryEditButton.Name = "DictionaryEditButton";
            this.DictionaryEditButton.Size = new System.Drawing.Size(360, 78);
            this.DictionaryEditButton.TabIndex = 3;
            this.DictionaryEditButton.Text = "Редактировать словарь";
            this.DictionaryEditButton.UseVisualStyleBackColor = true;
            this.DictionaryEditButton.Click += new System.EventHandler(this.DictionaryEditButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 570);
            this.Controls.Add(this.DictionaryEditButton);
            this.Controls.Add(this.AddTextsButton);
            this.Controls.Add(this.AddWordsButton);
            this.Controls.Add(this.TrainingButton);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(900, 609);
            this.MinimumSize = new System.Drawing.Size(900, 609);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Language Learning";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button TrainingButton;
    private System.Windows.Forms.Button AddWordsButton;
    private System.Windows.Forms.Button AddTextsButton;
    private System.Windows.Forms.Button DictionaryEditButton;
}

