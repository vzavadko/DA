namespace XRay.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.створитиСправуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.відкритиСправуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.закритиСправуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.допомогаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.інструкціяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.attachXRayImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPatientCard = new System.Windows.Forms.ToolStripButton();
            this.tsbRentgenAnalysis = new System.Windows.Forms.ToolStripButton();
            this.tsbOutputInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.processImageButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.userControl11 = new WpfControlLibrary.UserControl1();
            this.panel4 = new System.Windows.Forms.Panel();
            this.elementHost_bottom = new System.Windows.Forms.Integration.ElementHost();
            this.toothHelper1 = new WpfControlLibrary.ToothHelper();
            this.tcPatientCard = new System.Windows.Forms.TabControl();
            this.tp1_GeneralInfo = new System.Windows.Forms.TabPage();
            this.tp1_DentalStatus = new System.Windows.Forms.TabPage();
            this.tp2_Lines = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpDS_General = new System.Windows.Forms.TabPage();
            this.tpDS_Pathology = new System.Windows.Forms.TabPage();
            this.tp2_AnatomicCharacteristics = new System.Windows.Forms.TabPage();
            this.tp2_RetensionCharacteristics = new System.Windows.Forms.TabPage();
            this.tp3_SurgeryCharacteristics = new System.Windows.Forms.TabPage();
            this.tp3_Recommendations = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tcPatientCard.SuspendLayout();
            this.tp1_DentalStatus.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.ToolStripMenuItem,
            this.допомогаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1041, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.створитиСправуToolStripMenuItem,
            this.відкритиСправуToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.закритиСправуToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // створитиСправуToolStripMenuItem
            // 
            this.створитиСправуToolStripMenuItem.Name = "створитиСправуToolStripMenuItem";
            this.створитиСправуToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.створитиСправуToolStripMenuItem.Text = "Новий пацієнт";
            this.створитиСправуToolStripMenuItem.Click += new System.EventHandler(this.CreatePatientInfo);
            // 
            // відкритиСправуToolStripMenuItem
            // 
            this.відкритиСправуToolStripMenuItem.Name = "відкритиСправуToolStripMenuItem";
            this.відкритиСправуToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.відкритиСправуToolStripMenuItem.Text = "Відкрити";
            this.відкритиСправуToolStripMenuItem.Click += new System.EventHandler(this.OpenPatientInfo);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem1.Text = "Зберегти";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.SavePatientInfo);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem2.Text = "Зберегти як ...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.SaveAs);
            // 
            // закритиСправуToolStripMenuItem
            // 
            this.закритиСправуToolStripMenuItem.Name = "закритиСправуToolStripMenuItem";
            this.закритиСправуToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.закритиСправуToolStripMenuItem.Text = "Закрити";
            this.закритиСправуToolStripMenuItem.Click += new System.EventHandler(this.ClosePatientInfo);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.ToolStripMenuItem.Text = "Дії";
            // 
            // допомогаToolStripMenuItem
            // 
            this.допомогаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.інструкціяToolStripMenuItem});
            this.допомогаToolStripMenuItem.Name = "допомогаToolStripMenuItem";
            this.допомогаToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.допомогаToolStripMenuItem.Text = "Допомога";
            // 
            // інструкціяToolStripMenuItem
            // 
            this.інструкціяToolStripMenuItem.Name = "інструкціяToolStripMenuItem";
            this.інструкціяToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.інструкціяToolStripMenuItem.Text = "Інструкція";
            this.інструкціяToolStripMenuItem.Click += new System.EventHandler(this.інструкціяToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // attachXRayImageDialog
            // 
            this.attachXRayImageDialog.FileName = "openFileDialog1";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 662F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1041, 662);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 393F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tcPatientCard, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1035, 656);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton6,
            this.toolStripSeparator1,
            this.tsbPatientCard,
            this.tsbRentgenAnalysis,
            this.tsbOutputInfo,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1035, 55);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton1.Text = "Створити картку";
            this.toolStripButton1.Click += new System.EventHandler(this.CreatePatientInfo);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton2.Text = "Відкрити картку";
            this.toolStripButton2.Click += new System.EventHandler(this.OpenPatientInfo);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton3.Text = "Зберегти картку";
            this.toolStripButton3.Click += new System.EventHandler(this.SavePatientInfo);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton6.Text = "Друкувати";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // tsbPatientCard
            // 
            this.tsbPatientCard.CheckOnClick = true;
            this.tsbPatientCard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPatientCard.Image = ((System.Drawing.Image)(resources.GetObject("tsbPatientCard.Image")));
            this.tsbPatientCard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPatientCard.Name = "tsbPatientCard";
            this.tsbPatientCard.Size = new System.Drawing.Size(52, 52);
            this.tsbPatientCard.Text = "toolStripButton7";
            this.tsbPatientCard.Click += new System.EventHandler(this.tsbButtonGroup_Click);
            // 
            // tsbRentgenAnalysis
            // 
            this.tsbRentgenAnalysis.CheckOnClick = true;
            this.tsbRentgenAnalysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRentgenAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("tsbRentgenAnalysis.Image")));
            this.tsbRentgenAnalysis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRentgenAnalysis.Name = "tsbRentgenAnalysis";
            this.tsbRentgenAnalysis.Size = new System.Drawing.Size(52, 52);
            this.tsbRentgenAnalysis.Text = "toolStripButton8";
            this.tsbRentgenAnalysis.Click += new System.EventHandler(this.tsbButtonGroup_Click);
            // 
            // tsbOutputInfo
            // 
            this.tsbOutputInfo.CheckOnClick = true;
            this.tsbOutputInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOutputInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsbOutputInfo.Image")));
            this.tsbOutputInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOutputInfo.Name = "tsbOutputInfo";
            this.tsbOutputInfo.Size = new System.Drawing.Size(52, 52);
            this.tsbOutputInfo.Text = "toolStripButton9";
            this.tsbOutputInfo.Click += new System.EventHandler(this.tsbButtonGroup_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1035, 20);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 15);
            this.toolStripStatusLabel1.Text = "Готове";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.toolStrip2, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 61);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(387, 572);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip2.Location = new System.Drawing.Point(0, 486);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(387, 39);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.Visible = false;
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(381, 566);
            this.panel3.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.processImageButton);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.elementHost);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(381, 378);
            this.panel5.TabIndex = 1;
            // 
            // processImageButton
            // 
            this.processImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processImageButton.Enabled = false;
            this.processImageButton.Image = ((System.Drawing.Image)(resources.GetObject("processImageButton.Image")));
            this.processImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.processImageButton.Location = new System.Drawing.Point(28, 223);
            this.processImageButton.Name = "processImageButton";
            this.processImageButton.Size = new System.Drawing.Size(359, 50);
            this.processImageButton.TabIndex = 24;
            this.processImageButton.Text = "Розпізнати RTG знімок";
            this.processImageButton.UseVisualStyleBackColor = true;
            this.processImageButton.Click += new System.EventHandler(this.processImageButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(19, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(359, 50);
            this.button1.TabIndex = 23;
            this.button1.Text = "Додати RTG знімок";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddImageClick);
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost.Location = new System.Drawing.Point(0, 0);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(381, 378);
            this.elementHost.TabIndex = 0;
            this.elementHost.Text = "elementHost2";
            this.elementHost.Child = this.userControl11;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.elementHost_bottom);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 378);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(381, 188);
            this.panel4.TabIndex = 0;
            // 
            // elementHost_bottom
            // 
            this.elementHost_bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost_bottom.Location = new System.Drawing.Point(0, 0);
            this.elementHost_bottom.Name = "elementHost_bottom";
            this.elementHost_bottom.Size = new System.Drawing.Size(381, 188);
            this.elementHost_bottom.TabIndex = 0;
            this.elementHost_bottom.Text = "elementHost1";
            this.elementHost_bottom.Visible = false;
            this.elementHost_bottom.Child = this.toothHelper1;
            // 
            // tcPatientCard
            // 
            this.tcPatientCard.Controls.Add(this.tp1_GeneralInfo);
            this.tcPatientCard.Controls.Add(this.tp1_DentalStatus);
            this.tcPatientCard.Controls.Add(this.tp2_Lines);
            this.tcPatientCard.Controls.Add(this.tp2_AnatomicCharacteristics);
            this.tcPatientCard.Controls.Add(this.tp2_RetensionCharacteristics);
            this.tcPatientCard.Controls.Add(this.tp3_SurgeryCharacteristics);
            this.tcPatientCard.Controls.Add(this.tp3_Recommendations);
            this.tcPatientCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPatientCard.Location = new System.Drawing.Point(396, 61);
            this.tcPatientCard.Name = "tcPatientCard";
            this.tcPatientCard.SelectedIndex = 0;
            this.tcPatientCard.Size = new System.Drawing.Size(636, 572);
            this.tcPatientCard.TabIndex = 7;
            // 
            // tp1_GeneralInfo
            // 
            this.tp1_GeneralInfo.Location = new System.Drawing.Point(4, 22);
            this.tp1_GeneralInfo.Name = "tp1_GeneralInfo";
            this.tp1_GeneralInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tp1_GeneralInfo.Size = new System.Drawing.Size(628, 546);
            this.tp1_GeneralInfo.TabIndex = 0;
            this.tp1_GeneralInfo.Text = "Загальна інформація";
            this.tp1_GeneralInfo.UseVisualStyleBackColor = true;
            // 
            // tp1_DentalStatus
            // 
            this.tp1_DentalStatus.Controls.Add(this.tabControl1);
            this.tp1_DentalStatus.Location = new System.Drawing.Point(4, 22);
            this.tp1_DentalStatus.Name = "tp1_DentalStatus";
            this.tp1_DentalStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tp1_DentalStatus.Size = new System.Drawing.Size(628, 546);
            this.tp1_DentalStatus.TabIndex = 1;
            this.tp1_DentalStatus.Text = "Стоматологічний статус";
            this.tp1_DentalStatus.UseVisualStyleBackColor = true;
            // 
            // tp2_Lines
            // 
            this.tp2_Lines.Location = new System.Drawing.Point(4, 22);
            this.tp2_Lines.Name = "tp2_Lines";
            this.tp2_Lines.Padding = new System.Windows.Forms.Padding(3);
            this.tp2_Lines.Size = new System.Drawing.Size(628, 546);
            this.tp2_Lines.TabIndex = 2;
            this.tp2_Lines.Text = "Анат. структури та лінії";
            this.tp2_Lines.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpDS_General);
            this.tabControl1.Controls.Add(this.tpDS_Pathology);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(622, 540);
            this.tabControl1.TabIndex = 0;
            // 
            // tpDS_General
            // 
            this.tpDS_General.Location = new System.Drawing.Point(4, 22);
            this.tpDS_General.Name = "tpDS_General";
            this.tpDS_General.Padding = new System.Windows.Forms.Padding(3);
            this.tpDS_General.Size = new System.Drawing.Size(614, 514);
            this.tpDS_General.TabIndex = 0;
            this.tpDS_General.Text = "Загальний";
            this.tpDS_General.UseVisualStyleBackColor = true;
            // 
            // tpDS_Pathology
            // 
            this.tpDS_Pathology.Location = new System.Drawing.Point(4, 22);
            this.tpDS_Pathology.Name = "tpDS_Pathology";
            this.tpDS_Pathology.Padding = new System.Windows.Forms.Padding(3);
            this.tpDS_Pathology.Size = new System.Drawing.Size(614, 514);
            this.tpDS_Pathology.TabIndex = 1;
            this.tpDS_Pathology.Text = "Патологія пов\'язана із зубом";
            this.tpDS_Pathology.UseVisualStyleBackColor = true;
            // 
            // tp2_AnatomicCharacteristics
            // 
            this.tp2_AnatomicCharacteristics.Location = new System.Drawing.Point(4, 22);
            this.tp2_AnatomicCharacteristics.Name = "tp2_AnatomicCharacteristics";
            this.tp2_AnatomicCharacteristics.Padding = new System.Windows.Forms.Padding(3);
            this.tp2_AnatomicCharacteristics.Size = new System.Drawing.Size(628, 546);
            this.tp2_AnatomicCharacteristics.TabIndex = 3;
            this.tp2_AnatomicCharacteristics.Text = "Анатомічні характеристики";
            this.tp2_AnatomicCharacteristics.UseVisualStyleBackColor = true;
            // 
            // tp2_RetensionCharacteristics
            // 
            this.tp2_RetensionCharacteristics.Location = new System.Drawing.Point(4, 22);
            this.tp2_RetensionCharacteristics.Name = "tp2_RetensionCharacteristics";
            this.tp2_RetensionCharacteristics.Padding = new System.Windows.Forms.Padding(3);
            this.tp2_RetensionCharacteristics.Size = new System.Drawing.Size(628, 546);
            this.tp2_RetensionCharacteristics.TabIndex = 4;
            this.tp2_RetensionCharacteristics.Text = "Характеристика ретенції";
            this.tp2_RetensionCharacteristics.UseVisualStyleBackColor = true;
            // 
            // tp3_SurgeryCharacteristics
            // 
            this.tp3_SurgeryCharacteristics.Location = new System.Drawing.Point(4, 22);
            this.tp3_SurgeryCharacteristics.Name = "tp3_SurgeryCharacteristics";
            this.tp3_SurgeryCharacteristics.Padding = new System.Windows.Forms.Padding(3);
            this.tp3_SurgeryCharacteristics.Size = new System.Drawing.Size(628, 546);
            this.tp3_SurgeryCharacteristics.TabIndex = 5;
            this.tp3_SurgeryCharacteristics.Text = "Характеристика хірургічного втручання";
            this.tp3_SurgeryCharacteristics.UseVisualStyleBackColor = true;
            // 
            // tp3_Recommendations
            // 
            this.tp3_Recommendations.Location = new System.Drawing.Point(4, 22);
            this.tp3_Recommendations.Name = "tp3_Recommendations";
            this.tp3_Recommendations.Padding = new System.Windows.Forms.Padding(3);
            this.tp3_Recommendations.Size = new System.Drawing.Size(628, 546);
            this.tp3_Recommendations.TabIndex = 6;
            this.tp3_Recommendations.Text = "Рекомендації";
            this.tp3_Recommendations.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 686);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(16, 670);
            this.Name = "MainForm";
            this.Text = "XRay analyzer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tcPatientCard.ResumeLayout(false);
            this.tp1_DentalStatus.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem створитиСправуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem відкритиСправуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закритиСправуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog attachXRayImageDialog;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem допомогаToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ToolStripMenuItem інструкціяToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPatientCard;
        private System.Windows.Forms.ToolStripButton tsbRentgenAnalysis;
        private System.Windows.Forms.ToolStripButton tsbOutputInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button processImageButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private WpfControlLibrary.UserControl1 userControl11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Integration.ElementHost elementHost_bottom;
        private WpfControlLibrary.ToothHelper toothHelper1;
        private System.Windows.Forms.TabControl tcPatientCard;
        private System.Windows.Forms.TabPage tp1_GeneralInfo;
        private System.Windows.Forms.TabPage tp1_DentalStatus;
        private System.Windows.Forms.TabPage tp2_Lines;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpDS_General;
        private System.Windows.Forms.TabPage tpDS_Pathology;
        private System.Windows.Forms.TabPage tp2_AnatomicCharacteristics;
        private System.Windows.Forms.TabPage tp2_RetensionCharacteristics;
        private System.Windows.Forms.TabPage tp3_SurgeryCharacteristics;
        private System.Windows.Forms.TabPage tp3_Recommendations;
    }
}

