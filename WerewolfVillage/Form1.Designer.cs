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
            this.Startbutton = new System.Windows.Forms.Button();
            this.numvillager = new System.Windows.Forms.Label();
            this.ResultBox = new System.Windows.Forms.TextBox();
            this.numvillagecombo = new System.Windows.Forms.ComboBox();
            this.ReadFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AgentName1 = new System.Windows.Forms.ComboBox();
            this.AgentRole1 = new System.Windows.Forms.ComboBox();
            this.AgentName2 = new System.Windows.Forms.ComboBox();
            this.AgentName3 = new System.Windows.Forms.ComboBox();
            this.AgentName4 = new System.Windows.Forms.ComboBox();
            this.AgentName5 = new System.Windows.Forms.ComboBox();
            this.AgentName6 = new System.Windows.Forms.ComboBox();
            this.AgentName7 = new System.Windows.Forms.ComboBox();
            this.AgentName8 = new System.Windows.Forms.ComboBox();
            this.AgentName9 = new System.Windows.Forms.ComboBox();
            this.AgentName10 = new System.Windows.Forms.ComboBox();
            this.AgentName11 = new System.Windows.Forms.ComboBox();
            this.AgentName12 = new System.Windows.Forms.ComboBox();
            this.AgentName13 = new System.Windows.Forms.ComboBox();
            this.AgentName14 = new System.Windows.Forms.ComboBox();
            this.AgentName15 = new System.Windows.Forms.ComboBox();
            this.AgentRole2 = new System.Windows.Forms.ComboBox();
            this.AgentRole3 = new System.Windows.Forms.ComboBox();
            this.AgentRole4 = new System.Windows.Forms.ComboBox();
            this.AgentRole5 = new System.Windows.Forms.ComboBox();
            this.AgentRole6 = new System.Windows.Forms.ComboBox();
            this.AgentRole7 = new System.Windows.Forms.ComboBox();
            this.AgentRole8 = new System.Windows.Forms.ComboBox();
            this.AgentRole9 = new System.Windows.Forms.ComboBox();
            this.AgentRole10 = new System.Windows.Forms.ComboBox();
            this.AgentRole11 = new System.Windows.Forms.ComboBox();
            this.AgentRole12 = new System.Windows.Forms.ComboBox();
            this.AgentRole13 = new System.Windows.Forms.ComboBox();
            this.AgentRole14 = new System.Windows.Forms.ComboBox();
            this.AgentRole15 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.GerdButton = new System.Windows.Forms.RadioButton();
            this.nextDayButton = new System.Windows.Forms.Button();
            this.StartAutoGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Startbutton
            // 
            this.Startbutton.Location = new System.Drawing.Point(768, 92);
            this.Startbutton.Name = "Startbutton";
            this.Startbutton.Size = new System.Drawing.Size(167, 32);
            this.Startbutton.TabIndex = 0;
            this.Startbutton.Text = "START";
            this.Startbutton.UseVisualStyleBackColor = true;
            this.Startbutton.Click += new System.EventHandler(this.startbutton_Click);
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
            this.ResultBox.Location = new System.Drawing.Point(524, 332);
            this.ResultBox.Multiline = true;
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultBox.Size = new System.Drawing.Size(411, 307);
            this.ResultBox.TabIndex = 4;
            this.ResultBox.TextChanged += new System.EventHandler(this.ResultBox_TextChanged);
            // 
            // numvillagecombo
            // 
            this.numvillagecombo.FormattingEnabled = true;
            this.numvillagecombo.Items.AddRange(new object[] {
            "16",
            "5",
            "6",
            "7",
            "8",
            "9",
            "15"});
            this.numvillagecombo.Location = new System.Drawing.Point(219, 49);
            this.numvillagecombo.Name = "numvillagecombo";
            this.numvillagecombo.Size = new System.Drawing.Size(121, 26);
            this.numvillagecombo.TabIndex = 5;
            this.numvillagecombo.Text = "16";
            this.numvillagecombo.SelectedIndexChanged += new System.EventHandler(this.numvillagecombo_SelectedIndexChanged);
            // 
            // ReadFile
            // 
            this.ReadFile.Location = new System.Drawing.Point(629, 50);
            this.ReadFile.Name = "ReadFile";
            this.ReadFile.Size = new System.Drawing.Size(306, 25);
            this.ReadFile.TabIndex = 6;
            this.ReadFile.Text = "village_g632.csv";
            this.ReadFile.TextChanged += new System.EventHandler(this.ReadFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(444, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "読み込むcsvファイル";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "エージェント１：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "２：";
            // 
            // AgentName1
            // 
            this.AgentName1.FormattingEnabled = true;
            this.AgentName1.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName1.Location = new System.Drawing.Point(184, 124);
            this.AgentName1.MaxDropDownItems = 21;
            this.AgentName1.Name = "AgentName1";
            this.AgentName1.Size = new System.Drawing.Size(174, 26);
            this.AgentName1.TabIndex = 10;
            this.AgentName1.Text = "少女リーザ";
            this.AgentName1.SelectedIndexChanged += new System.EventHandler(this.AgentName1_SelectedIndexChanged);
            // 
            // AgentRole1
            // 
            this.AgentRole1.FormattingEnabled = true;
            this.AgentRole1.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole1.Location = new System.Drawing.Point(379, 124);
            this.AgentRole1.MaxDropDownItems = 6;
            this.AgentRole1.Name = "AgentRole1";
            this.AgentRole1.Size = new System.Drawing.Size(99, 26);
            this.AgentRole1.TabIndex = 11;
            this.AgentRole1.Text = "狂人";
            // 
            // AgentName2
            // 
            this.AgentName2.FormattingEnabled = true;
            this.AgentName2.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName2.Location = new System.Drawing.Point(184, 156);
            this.AgentName2.MaxDropDownItems = 21;
            this.AgentName2.Name = "AgentName2";
            this.AgentName2.Size = new System.Drawing.Size(174, 26);
            this.AgentName2.TabIndex = 12;
            this.AgentName2.Text = "ならず者ディーター";
            // 
            // AgentName3
            // 
            this.AgentName3.FormattingEnabled = true;
            this.AgentName3.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName3.Location = new System.Drawing.Point(184, 188);
            this.AgentName3.MaxDropDownItems = 21;
            this.AgentName3.Name = "AgentName3";
            this.AgentName3.Size = new System.Drawing.Size(174, 26);
            this.AgentName3.TabIndex = 13;
            this.AgentName3.Text = "木こりトーマス";
            // 
            // AgentName4
            // 
            this.AgentName4.FormattingEnabled = true;
            this.AgentName4.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName4.Location = new System.Drawing.Point(184, 220);
            this.AgentName4.MaxDropDownItems = 21;
            this.AgentName4.Name = "AgentName4";
            this.AgentName4.Size = new System.Drawing.Size(174, 26);
            this.AgentName4.TabIndex = 14;
            this.AgentName4.Text = "羊飼いカタリナ";
            // 
            // AgentName5
            // 
            this.AgentName5.FormattingEnabled = true;
            this.AgentName5.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName5.Location = new System.Drawing.Point(184, 252);
            this.AgentName5.MaxDropDownItems = 21;
            this.AgentName5.Name = "AgentName5";
            this.AgentName5.Size = new System.Drawing.Size(174, 26);
            this.AgentName5.TabIndex = 15;
            this.AgentName5.Text = "少年ペーター";
            // 
            // AgentName6
            // 
            this.AgentName6.FormattingEnabled = true;
            this.AgentName6.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName6.Location = new System.Drawing.Point(184, 284);
            this.AgentName6.MaxDropDownItems = 21;
            this.AgentName6.Name = "AgentName6";
            this.AgentName6.Size = new System.Drawing.Size(174, 26);
            this.AgentName6.TabIndex = 16;
            this.AgentName6.Text = "負傷兵シモン";
            // 
            // AgentName7
            // 
            this.AgentName7.FormattingEnabled = true;
            this.AgentName7.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName7.Location = new System.Drawing.Point(184, 316);
            this.AgentName7.MaxDropDownItems = 21;
            this.AgentName7.Name = "AgentName7";
            this.AgentName7.Size = new System.Drawing.Size(174, 26);
            this.AgentName7.TabIndex = 17;
            this.AgentName7.Text = "神父ジムゾン";
            // 
            // AgentName8
            // 
            this.AgentName8.FormattingEnabled = true;
            this.AgentName8.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName8.Location = new System.Drawing.Point(184, 348);
            this.AgentName8.MaxDropDownItems = 21;
            this.AgentName8.Name = "AgentName8";
            this.AgentName8.Size = new System.Drawing.Size(174, 26);
            this.AgentName8.TabIndex = 18;
            this.AgentName8.Text = "司書クララ";
            // 
            // AgentName9
            // 
            this.AgentName9.FormattingEnabled = true;
            this.AgentName9.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName9.Location = new System.Drawing.Point(184, 380);
            this.AgentName9.MaxDropDownItems = 21;
            this.AgentName9.Name = "AgentName9";
            this.AgentName9.Size = new System.Drawing.Size(174, 26);
            this.AgentName9.TabIndex = 19;
            this.AgentName9.Text = "宿屋の女主人レジーナ";
            // 
            // AgentName10
            // 
            this.AgentName10.FormattingEnabled = true;
            this.AgentName10.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName10.Location = new System.Drawing.Point(184, 412);
            this.AgentName10.MaxDropDownItems = 21;
            this.AgentName10.Name = "AgentName10";
            this.AgentName10.Size = new System.Drawing.Size(174, 26);
            this.AgentName10.TabIndex = 20;
            this.AgentName10.Text = "青年ヨアヒム";
            // 
            // AgentName11
            // 
            this.AgentName11.FormattingEnabled = true;
            this.AgentName11.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName11.Location = new System.Drawing.Point(184, 444);
            this.AgentName11.MaxDropDownItems = 21;
            this.AgentName11.Name = "AgentName11";
            this.AgentName11.Size = new System.Drawing.Size(174, 26);
            this.AgentName11.TabIndex = 21;
            this.AgentName11.Text = "パン屋オットー";
            // 
            // AgentName12
            // 
            this.AgentName12.FormattingEnabled = true;
            this.AgentName12.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName12.Location = new System.Drawing.Point(184, 476);
            this.AgentName12.MaxDropDownItems = 21;
            this.AgentName12.Name = "AgentName12";
            this.AgentName12.Size = new System.Drawing.Size(174, 26);
            this.AgentName12.TabIndex = 22;
            this.AgentName12.Text = "行商人アルビン";
            // 
            // AgentName13
            // 
            this.AgentName13.FormattingEnabled = true;
            this.AgentName13.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName13.Location = new System.Drawing.Point(184, 508);
            this.AgentName13.MaxDropDownItems = 21;
            this.AgentName13.Name = "AgentName13";
            this.AgentName13.Size = new System.Drawing.Size(174, 26);
            this.AgentName13.TabIndex = 23;
            this.AgentName13.Text = "旅人ニコラス";
            // 
            // AgentName14
            // 
            this.AgentName14.FormattingEnabled = true;
            this.AgentName14.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName14.Location = new System.Drawing.Point(184, 540);
            this.AgentName14.MaxDropDownItems = 21;
            this.AgentName14.Name = "AgentName14";
            this.AgentName14.Size = new System.Drawing.Size(174, 26);
            this.AgentName14.TabIndex = 24;
            this.AgentName14.Text = "老人モーリッツ";
            // 
            // AgentName15
            // 
            this.AgentName15.FormattingEnabled = true;
            this.AgentName15.Items.AddRange(new object[] {
            "村長ヴァルター",
            "老人モーリッツ",
            "神父ジムゾン",
            "木こりトーマス",
            "旅人ニコラス",
            "ならず者ディーター",
            "少年ペーター",
            "少女リーザ",
            "行商人アルビン",
            "羊飼いカタリナ",
            "パン屋オットー",
            "青年ヨアヒム",
            "村娘パメラ",
            "農夫ヤコブ",
            "宿屋の女主人レジーナ",
            "シスターフリーデル",
            "仕立て屋エルナ",
            "司書クララ",
            "負傷兵シモン"});
            this.AgentName15.Location = new System.Drawing.Point(184, 572);
            this.AgentName15.MaxDropDownItems = 21;
            this.AgentName15.Name = "AgentName15";
            this.AgentName15.Size = new System.Drawing.Size(174, 26);
            this.AgentName15.TabIndex = 25;
            this.AgentName15.Text = "シスターフリーデル";
            this.AgentName15.UseWaitCursor = true;
            // 
            // AgentRole2
            // 
            this.AgentRole2.FormattingEnabled = true;
            this.AgentRole2.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole2.Location = new System.Drawing.Point(379, 156);
            this.AgentRole2.MaxDropDownItems = 6;
            this.AgentRole2.Name = "AgentRole2";
            this.AgentRole2.Size = new System.Drawing.Size(99, 26);
            this.AgentRole2.TabIndex = 26;
            this.AgentRole2.Text = "村人";
            // 
            // AgentRole3
            // 
            this.AgentRole3.FormattingEnabled = true;
            this.AgentRole3.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole3.Location = new System.Drawing.Point(379, 188);
            this.AgentRole3.MaxDropDownItems = 6;
            this.AgentRole3.Name = "AgentRole3";
            this.AgentRole3.Size = new System.Drawing.Size(99, 26);
            this.AgentRole3.TabIndex = 27;
            this.AgentRole3.Text = "村人";
            // 
            // AgentRole4
            // 
            this.AgentRole4.FormattingEnabled = true;
            this.AgentRole4.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole4.Location = new System.Drawing.Point(379, 220);
            this.AgentRole4.MaxDropDownItems = 6;
            this.AgentRole4.Name = "AgentRole4";
            this.AgentRole4.Size = new System.Drawing.Size(99, 26);
            this.AgentRole4.TabIndex = 28;
            this.AgentRole4.Text = "村人";
            // 
            // AgentRole5
            // 
            this.AgentRole5.FormattingEnabled = true;
            this.AgentRole5.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole5.Location = new System.Drawing.Point(379, 252);
            this.AgentRole5.MaxDropDownItems = 6;
            this.AgentRole5.Name = "AgentRole5";
            this.AgentRole5.Size = new System.Drawing.Size(99, 26);
            this.AgentRole5.TabIndex = 29;
            this.AgentRole5.Text = "人狼";
            // 
            // AgentRole6
            // 
            this.AgentRole6.FormattingEnabled = true;
            this.AgentRole6.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole6.Location = new System.Drawing.Point(379, 284);
            this.AgentRole6.MaxDropDownItems = 6;
            this.AgentRole6.Name = "AgentRole6";
            this.AgentRole6.Size = new System.Drawing.Size(99, 26);
            this.AgentRole6.TabIndex = 30;
            this.AgentRole6.Text = "村人";
            // 
            // AgentRole7
            // 
            this.AgentRole7.FormattingEnabled = true;
            this.AgentRole7.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole7.Location = new System.Drawing.Point(379, 316);
            this.AgentRole7.MaxDropDownItems = 6;
            this.AgentRole7.Name = "AgentRole7";
            this.AgentRole7.Size = new System.Drawing.Size(99, 26);
            this.AgentRole7.TabIndex = 31;
            this.AgentRole7.Text = "人狼";
            // 
            // AgentRole8
            // 
            this.AgentRole8.FormattingEnabled = true;
            this.AgentRole8.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole8.Location = new System.Drawing.Point(379, 348);
            this.AgentRole8.MaxDropDownItems = 6;
            this.AgentRole8.Name = "AgentRole8";
            this.AgentRole8.Size = new System.Drawing.Size(99, 26);
            this.AgentRole8.TabIndex = 32;
            this.AgentRole8.Text = "村人";
            // 
            // AgentRole9
            // 
            this.AgentRole9.FormattingEnabled = true;
            this.AgentRole9.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole9.Location = new System.Drawing.Point(379, 380);
            this.AgentRole9.MaxDropDownItems = 6;
            this.AgentRole9.Name = "AgentRole9";
            this.AgentRole9.Size = new System.Drawing.Size(99, 26);
            this.AgentRole9.TabIndex = 33;
            this.AgentRole9.Text = "村人";
            // 
            // AgentRole10
            // 
            this.AgentRole10.FormattingEnabled = true;
            this.AgentRole10.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole10.Location = new System.Drawing.Point(379, 412);
            this.AgentRole10.MaxDropDownItems = 6;
            this.AgentRole10.Name = "AgentRole10";
            this.AgentRole10.Size = new System.Drawing.Size(99, 26);
            this.AgentRole10.TabIndex = 33;
            this.AgentRole10.Text = "占い師";
            // 
            // AgentRole11
            // 
            this.AgentRole11.FormattingEnabled = true;
            this.AgentRole11.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole11.Location = new System.Drawing.Point(379, 444);
            this.AgentRole11.MaxDropDownItems = 6;
            this.AgentRole11.Name = "AgentRole11";
            this.AgentRole11.Size = new System.Drawing.Size(99, 26);
            this.AgentRole11.TabIndex = 34;
            this.AgentRole11.Text = "霊能者";
            // 
            // AgentRole12
            // 
            this.AgentRole12.FormattingEnabled = true;
            this.AgentRole12.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole12.Location = new System.Drawing.Point(379, 476);
            this.AgentRole12.MaxDropDownItems = 6;
            this.AgentRole12.Name = "AgentRole12";
            this.AgentRole12.Size = new System.Drawing.Size(99, 26);
            this.AgentRole12.TabIndex = 35;
            this.AgentRole12.Text = "村人";
            // 
            // AgentRole13
            // 
            this.AgentRole13.FormattingEnabled = true;
            this.AgentRole13.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole13.Location = new System.Drawing.Point(379, 508);
            this.AgentRole13.MaxDropDownItems = 6;
            this.AgentRole13.Name = "AgentRole13";
            this.AgentRole13.Size = new System.Drawing.Size(99, 26);
            this.AgentRole13.TabIndex = 36;
            this.AgentRole13.Text = "村人";
            // 
            // AgentRole14
            // 
            this.AgentRole14.FormattingEnabled = true;
            this.AgentRole14.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole14.Location = new System.Drawing.Point(379, 540);
            this.AgentRole14.MaxDropDownItems = 6;
            this.AgentRole14.Name = "AgentRole14";
            this.AgentRole14.Size = new System.Drawing.Size(99, 26);
            this.AgentRole14.TabIndex = 37;
            this.AgentRole14.Text = "人狼";
            // 
            // AgentRole15
            // 
            this.AgentRole15.FormattingEnabled = true;
            this.AgentRole15.Items.AddRange(new object[] {
            "村人",
            "占い師",
            "霊能者",
            "狩人",
            "狂人",
            "人狼"});
            this.AgentRole15.Location = new System.Drawing.Point(379, 572);
            this.AgentRole15.MaxDropDownItems = 6;
            this.AgentRole15.Name = "AgentRole15";
            this.AgentRole15.Size = new System.Drawing.Size(99, 26);
            this.AgentRole15.TabIndex = 38;
            this.AgentRole15.Text = "狩人";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 18);
            this.label4.TabIndex = 39;
            this.label4.Text = "３：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 18);
            this.label5.TabIndex = 40;
            this.label5.Text = "４：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 18);
            this.label6.TabIndex = 41;
            this.label6.Text = "５：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(137, 287);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 18);
            this.label7.TabIndex = 42;
            this.label7.Text = "６：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(137, 319);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 18);
            this.label8.TabIndex = 43;
            this.label8.Text = "７：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(137, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 18);
            this.label9.TabIndex = 44;
            this.label9.Text = "８：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(137, 383);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 18);
            this.label10.TabIndex = 45;
            this.label10.Text = "９：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(125, 415);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 18);
            this.label11.TabIndex = 46;
            this.label11.Text = "１０：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(125, 447);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 18);
            this.label12.TabIndex = 47;
            this.label12.Text = "１１：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(125, 479);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 18);
            this.label13.TabIndex = 48;
            this.label13.Text = "１２：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(125, 511);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 18);
            this.label14.TabIndex = 49;
            this.label14.Text = "１３：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(125, 543);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 18);
            this.label15.TabIndex = 50;
            this.label15.Text = "１４：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(125, 575);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 18);
            this.label16.TabIndex = 51;
            this.label16.Text = "１５：";
            // 
            // GerdButton
            // 
            this.GerdButton.AutoSize = true;
            this.GerdButton.Location = new System.Drawing.Point(140, 617);
            this.GerdButton.Name = "GerdButton";
            this.GerdButton.Size = new System.Drawing.Size(184, 22);
            this.GerdButton.TabIndex = 52;
            this.GerdButton.Text = "楽天家ゲルト（村人）";
            this.GerdButton.UseVisualStyleBackColor = true;
            this.GerdButton.CheckedChanged += new System.EventHandler(this.GerdButton_CheckedChanged);
            // 
            // nextDayButton
            // 
            this.nextDayButton.Location = new System.Drawing.Point(768, 130);
            this.nextDayButton.Name = "nextDayButton";
            this.nextDayButton.Size = new System.Drawing.Size(167, 32);
            this.nextDayButton.TabIndex = 53;
            this.nextDayButton.Text = "Next Day";
            this.nextDayButton.UseVisualStyleBackColor = true;
            this.nextDayButton.Click += new System.EventHandler(this.nextDayButton_Click);
            // 
            // StartAutoGame
            // 
            this.StartAutoGame.Location = new System.Drawing.Point(768, 284);
            this.StartAutoGame.Name = "StartAutoGame";
            this.StartAutoGame.Size = new System.Drawing.Size(167, 32);
            this.StartAutoGame.TabIndex = 54;
            this.StartAutoGame.Text = "StartAutoGame";
            this.StartAutoGame.UseVisualStyleBackColor = true;
            this.StartAutoGame.Click += new System.EventHandler(this.StartAutoGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 670);
            this.Controls.Add(this.StartAutoGame);
            this.Controls.Add(this.nextDayButton);
            this.Controls.Add(this.GerdButton);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AgentRole15);
            this.Controls.Add(this.AgentRole14);
            this.Controls.Add(this.AgentRole13);
            this.Controls.Add(this.AgentRole12);
            this.Controls.Add(this.AgentRole11);
            this.Controls.Add(this.AgentRole10);
            this.Controls.Add(this.AgentRole9);
            this.Controls.Add(this.AgentRole8);
            this.Controls.Add(this.AgentRole7);
            this.Controls.Add(this.AgentRole6);
            this.Controls.Add(this.AgentRole5);
            this.Controls.Add(this.AgentRole4);
            this.Controls.Add(this.AgentRole3);
            this.Controls.Add(this.AgentRole2);
            this.Controls.Add(this.AgentName15);
            this.Controls.Add(this.AgentName14);
            this.Controls.Add(this.AgentName13);
            this.Controls.Add(this.AgentName12);
            this.Controls.Add(this.AgentName11);
            this.Controls.Add(this.AgentName10);
            this.Controls.Add(this.AgentName9);
            this.Controls.Add(this.AgentName8);
            this.Controls.Add(this.AgentName7);
            this.Controls.Add(this.AgentName6);
            this.Controls.Add(this.AgentName5);
            this.Controls.Add(this.AgentName4);
            this.Controls.Add(this.AgentName3);
            this.Controls.Add(this.AgentName2);
            this.Controls.Add(this.AgentRole1);
            this.Controls.Add(this.AgentName1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReadFile);
            this.Controls.Add(this.numvillagecombo);
            this.Controls.Add(this.ResultBox);
            this.Controls.Add(this.numvillager);
            this.Controls.Add(this.Startbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Startbutton;
        private System.Windows.Forms.Label numvillager;
        private System.Windows.Forms.TextBox ResultBox;
        private System.Windows.Forms.ComboBox numvillagecombo;
        private System.Windows.Forms.TextBox ReadFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox AgentName1;
        private System.Windows.Forms.ComboBox AgentRole1;
        private System.Windows.Forms.ComboBox AgentName2;
        private System.Windows.Forms.ComboBox AgentName3;
        private System.Windows.Forms.ComboBox AgentName4;
        private System.Windows.Forms.ComboBox AgentName5;
        private System.Windows.Forms.ComboBox AgentName6;
        private System.Windows.Forms.ComboBox AgentName7;
        private System.Windows.Forms.ComboBox AgentName8;
        private System.Windows.Forms.ComboBox AgentName9;
        private System.Windows.Forms.ComboBox AgentName10;
        private System.Windows.Forms.ComboBox AgentName11;
        private System.Windows.Forms.ComboBox AgentName12;
        private System.Windows.Forms.ComboBox AgentName13;
        private System.Windows.Forms.ComboBox AgentName14;
        private System.Windows.Forms.ComboBox AgentName15;
        private System.Windows.Forms.ComboBox AgentRole2;
        private System.Windows.Forms.ComboBox AgentRole3;
        private System.Windows.Forms.ComboBox AgentRole4;
        private System.Windows.Forms.ComboBox AgentRole5;
        private System.Windows.Forms.ComboBox AgentRole6;
        private System.Windows.Forms.ComboBox AgentRole7;
        private System.Windows.Forms.ComboBox AgentRole8;
        private System.Windows.Forms.ComboBox AgentRole9;
        private System.Windows.Forms.ComboBox AgentRole10;
        private System.Windows.Forms.ComboBox AgentRole11;
        private System.Windows.Forms.ComboBox AgentRole12;
        private System.Windows.Forms.ComboBox AgentRole13;
        private System.Windows.Forms.ComboBox AgentRole14;
        private System.Windows.Forms.ComboBox AgentRole15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton GerdButton;
        private System.Windows.Forms.Button nextDayButton;
        private System.Windows.Forms.Button StartAutoGame;
    }
}

