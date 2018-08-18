namespace repetition_of_English_words
{
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
            this.StartButton = new System.Windows.Forms.Button();
            this.AddWordsButton = new System.Windows.Forms.Button();
            this.AddTextsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartButton.Location = new System.Drawing.Point(262, 159);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(360, 78);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Начать";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // AddWordsButton
            // 
            this.AddWordsButton.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddWordsButton.Location = new System.Drawing.Point(120, 333);
            this.AddWordsButton.Name = "AddWordsButton";
            this.AddWordsButton.Size = new System.Drawing.Size(282, 78);
            this.AddWordsButton.TabIndex = 1;
            this.AddWordsButton.Text = "Добавить слова";
            this.AddWordsButton.UseVisualStyleBackColor = true;
            this.AddWordsButton.Click += new System.EventHandler(this.AddWordsButton_Click);
            // 
            // AddTextsButton
            // 
            this.AddTextsButton.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddTextsButton.Location = new System.Drawing.Point(482, 333);
            this.AddTextsButton.Name = "AddTextsButton";
            this.AddTextsButton.Size = new System.Drawing.Size(282, 78);
            this.AddTextsButton.TabIndex = 2;
            this.AddTextsButton.Text = "Добавить предложения";
            this.AddTextsButton.UseVisualStyleBackColor = true;
            this.AddTextsButton.Click += new System.EventHandler(this.AddTextsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 570);
            this.Controls.Add(this.AddTextsButton);
            this.Controls.Add(this.AddWordsButton);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "English Words";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button AddWordsButton;
        private System.Windows.Forms.Button AddTextsButton;
    }
}

