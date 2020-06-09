namespace WerewolfVillage
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.staetbutton = new System.Windows.Forms.Button();
            this.numvillager = new System.Windows.Forms.Label();
            this.ResultBox = new System.Windows.Forms.TextBox();
            this.numvillagecombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // staetbutton
            // 
            this.staetbutton.Location = new System.Drawing.Point(768, 267);
            this.staetbutton.Name = "staetbutton";
            this.staetbutton.Size = new System.Drawing.Size(167, 32);
            this.staetbutton.TabIndex = 0;
            this.staetbutton.Text = "START";
            this.staetbutton.UseVisualStyleBackColor = true;
            this.staetbutton.Click += new System.EventHandler(this.startbutton_Click);
            // 
            // numvillager
            // 
            this.numvillager.AutoSize = true;
            this.numvillager.Location = new System.Drawing.Point(56, 49);
            this.numvillager.Name = "numvillager";
            this.numvillager.Size = new System.Drawing.Size(142, 18);
            this.numvillager.TabIndex = 2;
            this.numvillager.Text = "生成する村人の数";
            // 
            // ResultBox
            // 
            this.ResultBox.Location = new System.Drawing.Point(39, 332);
            this.ResultBox.Multiline = true;
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultBox.Size = new System.Drawing.Size(896, 307);
            this.ResultBox.TabIndex = 4;
            this.ResultBox.TextChanged += new System.EventHandler(this.ResultBox_TextChanged);
            // 
            // numvillagecombo
            // 
            this.numvillagecombo.FormattingEnabled = true;
            this.numvillagecombo.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.numvillagecombo.Location = new System.Drawing.Point(219, 49);
            this.numvillagecombo.Name = "numvillagecombo";
            this.numvillagecombo.Size = new System.Drawing.Size(121, 26);
            this.numvillagecombo.TabIndex = 5;
            this.numvillagecombo.Text = "5";
            this.numvillagecombo.SelectedIndexChanged += new System.EventHandler(this.numvillagecombo_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 670);
            this.Controls.Add(this.numvillagecombo);
            this.Controls.Add(this.ResultBox);
            this.Controls.Add(this.numvillager);
            this.Controls.Add(this.staetbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button staetbutton;
        private System.Windows.Forms.Label numvillager;
        private System.Windows.Forms.TextBox ResultBox;
        private System.Windows.Forms.ComboBox numvillagecombo;
    }
}

