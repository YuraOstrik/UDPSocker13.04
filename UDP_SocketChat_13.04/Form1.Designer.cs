namespace UDP_SocketChat_13._04
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            listBox1 = new ListBox();
            InfoT = new TextBox();
            listBox2 = new ListBox();
            ipAd = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(84, 88);
            button1.Name = "button1";
            button1.Size = new Size(224, 52);
            button1.TabIndex = 0;
            button1.Text = "Send your message";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(84, 155);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(381, 224);
            listBox1.TabIndex = 1;
            // 
            // InfoT
            // 
            InfoT.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            InfoT.Location = new Point(84, 399);
            InfoT.Name = "InfoT";
            InfoT.Size = new Size(381, 27);
            InfoT.TabIndex = 2;
            //InfoT.TextChanged += textBox1_TextChanged;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(471, 174);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(202, 184);
            listBox2.TabIndex = 3;
            // 
            // ipAd
            // 
            ipAd.Location = new Point(329, 101);
            ipAd.Name = "ipAd";
            ipAd.Size = new Size(125, 27);
            ipAd.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(702, 484);
            Controls.Add(ipAd);
            Controls.Add(listBox2);
            Controls.Add(InfoT);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ListBox listBox1;
        private TextBox InfoT;
        private ListBox listBox2;
        private TextBox ipAd;
    }
}
