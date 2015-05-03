using PSU_Calculator.DataWorker;
using System;
using System.Collections.Generic;
namespace PSU_Calculator
{
  partial class Form1
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.supportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fAQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.nützlicheLinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.optionenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.datenSpeicherortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lokalerOrdnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.appDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.spracheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deutschToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.suchmaschineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.PnlHeader = new System.Windows.Forms.Panel();
      this.PnlContent = new System.Windows.Forms.Panel();
      this.LinkLabel4 = new System.Windows.Forms.LinkLabel();
      this.Label17 = new System.Windows.Forms.Label();
      this.gbxNetzteile = new System.Windows.Forms.GroupBox();
      this.cbxConectors = new System.Windows.Forms.CheckBox();
      this.radioTestberichte = new System.Windows.Forms.RadioButton();
      this.radioPreisvergleich = new System.Windows.Forms.RadioButton();
      this.cmdCopyToForum = new System.Windows.Forms.Button();
      this.pnlNetzteile = new System.Windows.Forms.FlowLayoutPanel();
      this.gbxVerbrauch = new System.Windows.Forms.GroupBox();
      this.PictureBox1 = new System.Windows.Forms.PictureBox();
      this.Label8 = new System.Windows.Forms.Label();
      this.pgbEffizienz = new System.Windows.Forms.ProgressBar();
      this.PictureBox4 = new System.Windows.Forms.PictureBox();
      this.Label16 = new System.Windows.Forms.Label();
      this.lblVerbrauch = new System.Windows.Forms.Label();
      this.gbxDaten = new System.Windows.Forms.GroupBox();
      this.pnlRow2 = new System.Windows.Forms.Panel();
      this.cmdCopySystem = new System.Windows.Forms.Button();
      this.label9 = new System.Windows.Forms.Label();
      this.cbxPhysx = new System.Windows.Forms.ComboBox();
      this.lblPhysX = new System.Windows.Forms.Label();
      this.chkCFSLI = new System.Windows.Forms.CheckBox();
      this.lblSSD = new System.Windows.Forms.Label();
      this.cbxSSD = new System.Windows.Forms.ComboBox();
      this.chkdualcpu = new System.Windows.Forms.CheckBox();
      this.cbxCpu = new System.Windows.Forms.ComboBox();
      this.lblCPU = new System.Windows.Forms.Label();
      this.lblKuehlung = new System.Windows.Forms.Label();
      this.cbxCooling = new System.Windows.Forms.ComboBox();
      this.lblHDD = new System.Windows.Forms.Label();
      this.cbxHDD = new System.Windows.Forms.ComboBox();
      this.lblLaufwerke = new System.Windows.Forms.Label();
      this.cbxLaufwerke = new System.Windows.Forms.ComboBox();
      this.lblLuefter = new System.Windows.Forms.Label();
      this.cbxFans = new System.Windows.Forms.ComboBox();
      this.lblOC = new System.Windows.Forms.Label();
      this.cbxOverclocking = new System.Windows.Forms.ComboBox();
      this.cbxCPU2 = new System.Windows.Forms.ComboBox();
      this.lblCPU2 = new System.Windows.Forms.Label();
      this.menuStrip1.SuspendLayout();
      this.PnlContent.SuspendLayout();
      this.gbxNetzteile.SuspendLayout();
      this.gbxVerbrauch.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
      this.gbxDaten.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.BackColor = System.Drawing.SystemColors.Window;
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supportToolStripMenuItem,
            this.nützlicheLinksToolStripMenuItem,
            this.optionenToolStripMenuItem,
            this.einstellungenToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(307, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // supportToolStripMenuItem
      // 
      this.supportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.hilfeToolStripMenuItem,
            this.fAQToolStripMenuItem});
      this.supportToolStripMenuItem.Name = "supportToolStripMenuItem";
      this.supportToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
      this.supportToolStripMenuItem.Text = "Support";
      // 
      // infoToolStripMenuItem
      // 
      this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
      this.infoToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
      this.infoToolStripMenuItem.Text = "Info";
      this.infoToolStripMenuItem.Click += new System.EventHandler(this.anzeigeInfo);
      // 
      // hilfeToolStripMenuItem
      // 
      this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
      this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
      this.hilfeToolStripMenuItem.Text = "Hilfe";
      this.hilfeToolStripMenuItem.Click += new System.EventHandler(this.hilfeToolStripMenuItem_Click);
      // 
      // fAQToolStripMenuItem
      // 
      this.fAQToolStripMenuItem.Name = "fAQToolStripMenuItem";
      this.fAQToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
      this.fAQToolStripMenuItem.Text = "FAQ";
      this.fAQToolStripMenuItem.Click += new System.EventHandler(this.anzeigeFAQ);
      // 
      // nützlicheLinksToolStripMenuItem
      // 
      this.nützlicheLinksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem,
            this.singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem,
            this.wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem});
      this.nützlicheLinksToolStripMenuItem.Name = "nützlicheLinksToolStripMenuItem";
      this.nützlicheLinksToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
      this.nützlicheLinksToolStripMenuItem.Text = "Nützliche Links";
      // 
      // allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem
      // 
      this.allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem.Name = "allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem";
      this.allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem.Size = new System.Drawing.Size(454, 22);
      this.allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem.Text = "Allgemeiner Diskussionsthread zu Netzteilen";
      this.allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem.Click += new System.EventHandler(this.AllgemeinerNetzeilDiskusionsthread);
      // 
      // singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem
      // 
      this.singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem.Name = "singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem";
      this.singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem.Size = new System.Drawing.Size(454, 22);
      this.singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem.Text = "Single Rail vs. Multi Rail. Was ist das? Vorteile und Nachteile...";
      this.singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem.Click += new System.EventHandler(this.SRvsMR);
      // 
      // wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem
      // 
      this.wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem.Name = "wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem";
      this.wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem.Size = new System.Drawing.Size(454, 22);
      this.wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem.Text = "Wie man als Laie ein günstiges von einem billigen Netzteil unterscheidet";
      this.wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem.Click += new System.EventHandler(this.guenstigVSbillig);
      // 
      // optionenToolStripMenuItem
      // 
      this.optionenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem,
            this.datenSpeicherortToolStripMenuItem});
      this.optionenToolStripMenuItem.Name = "optionenToolStripMenuItem";
      this.optionenToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
      this.optionenToolStripMenuItem.Text = "Extras";
      // 
      // themeToolStripMenuItem
      // 
      this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
      this.themeToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
      this.themeToolStripMenuItem.Text = "Aktualisiere Preise";
      this.themeToolStripMenuItem.Visible = false;
      this.themeToolStripMenuItem.Click += new System.EventHandler(this.themeToolStripMenuItem_Click);
      // 
      // datenSpeicherortToolStripMenuItem
      // 
      this.datenSpeicherortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lokalerOrdnerToolStripMenuItem,
            this.appDataToolStripMenuItem});
      this.datenSpeicherortToolStripMenuItem.Name = "datenSpeicherortToolStripMenuItem";
      this.datenSpeicherortToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
      this.datenSpeicherortToolStripMenuItem.Text = "Daten Speicherort";
      // 
      // lokalerOrdnerToolStripMenuItem
      // 
      this.lokalerOrdnerToolStripMenuItem.Name = "lokalerOrdnerToolStripMenuItem";
      this.lokalerOrdnerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.lokalerOrdnerToolStripMenuItem.Text = "Lokaler Ordner";
      this.lokalerOrdnerToolStripMenuItem.Click += new System.EventHandler(this.lokalerOrdnerToolStripMenuItem_Click);
      // 
      // appDataToolStripMenuItem
      // 
      this.appDataToolStripMenuItem.Name = "appDataToolStripMenuItem";
      this.appDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.appDataToolStripMenuItem.Text = "AppData";
      this.appDataToolStripMenuItem.Click += new System.EventHandler(this.appDataToolStripMenuItem_Click);
      // 
      // einstellungenToolStripMenuItem
      // 
      this.einstellungenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spracheToolStripMenuItem,
            this.suchmaschineToolStripMenuItem});
      this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
      this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
      this.einstellungenToolStripMenuItem.Text = "Einstellungen";
      // 
      // spracheToolStripMenuItem
      // 
      this.spracheToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deutschToolStripMenuItem});
      this.spracheToolStripMenuItem.Name = "spracheToolStripMenuItem";
      this.spracheToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
      this.spracheToolStripMenuItem.Text = "Sprache";
      // 
      // deutschToolStripMenuItem
      // 
      this.deutschToolStripMenuItem.Name = "deutschToolStripMenuItem";
      this.deutschToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
      this.deutschToolStripMenuItem.Text = "Deutsch";
      this.deutschToolStripMenuItem.Click += new System.EventHandler(this.deutschToolStripMenuItem_Click);
      // 
      // suchmaschineToolStripMenuItem
      // 
      this.suchmaschineToolStripMenuItem.Name = "suchmaschineToolStripMenuItem";
      this.suchmaschineToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
      this.suchmaschineToolStripMenuItem.Text = "Suchmaschine";
      // 
      // PnlHeader
      // 
      this.PnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.PnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(255)))));
      this.PnlHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PnlHeader.BackgroundImage")));
      this.PnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.PnlHeader.Location = new System.Drawing.Point(0, 24);
      this.PnlHeader.Name = "PnlHeader";
      this.PnlHeader.Size = new System.Drawing.Size(882, 70);
      this.PnlHeader.TabIndex = 1;
      // 
      // PnlContent
      // 
      this.PnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.PnlContent.Controls.Add(this.LinkLabel4);
      this.PnlContent.Controls.Add(this.Label17);
      this.PnlContent.Controls.Add(this.gbxNetzteile);
      this.PnlContent.Controls.Add(this.gbxVerbrauch);
      this.PnlContent.Controls.Add(this.gbxDaten);
      this.PnlContent.Location = new System.Drawing.Point(0, 87);
      this.PnlContent.Name = "PnlContent";
      this.PnlContent.Size = new System.Drawing.Size(882, 399);
      this.PnlContent.TabIndex = 2;
      // 
      // LinkLabel4
      // 
      this.LinkLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.LinkLabel4.AutoSize = true;
      this.LinkLabel4.Location = new System.Drawing.Point(12, 367);
      this.LinkLabel4.Name = "LinkLabel4";
      this.LinkLabel4.Size = new System.Drawing.Size(140, 13);
      this.LinkLabel4.TabIndex = 53;
      this.LinkLabel4.TabStop = true;
      this.LinkLabel4.Text = "Fehlende Hardware melden.";
      this.LinkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel4_LinkClicked);
      // 
      // Label17
      // 
      this.Label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.Label17.AutoSize = true;
      this.Label17.BackColor = System.Drawing.Color.Transparent;
      this.Label17.ForeColor = System.Drawing.Color.Red;
      this.Label17.Location = new System.Drawing.Point(400, 361);
      this.Label17.Name = "Label17";
      this.Label17.Size = new System.Drawing.Size(459, 26);
      this.Label17.TabIndex = 49;
      this.Label17.Text = "Nach Jahren verlieren Netzteile an Leistung und die Restwelligkeit nimmt zu, da d" +
    "ie Elkos altern.\r\nIch empfehle alle 5 Jahre ein neues Netzteil zu kaufen.";
      // 
      // gbxNetzteile
      // 
      this.gbxNetzteile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gbxNetzteile.BackColor = System.Drawing.Color.White;
      this.gbxNetzteile.Controls.Add(this.cbxConectors);
      this.gbxNetzteile.Controls.Add(this.radioTestberichte);
      this.gbxNetzteile.Controls.Add(this.radioPreisvergleich);
      this.gbxNetzteile.Controls.Add(this.cmdCopyToForum);
      this.gbxNetzteile.Controls.Add(this.pnlNetzteile);
      this.gbxNetzteile.Location = new System.Drawing.Point(487, 106);
      this.gbxNetzteile.Name = "gbxNetzteile";
      this.gbxNetzteile.Size = new System.Drawing.Size(392, 249);
      this.gbxNetzteile.TabIndex = 32;
      this.gbxNetzteile.TabStop = false;
      this.gbxNetzteile.Text = "Empfehlenswerte Netzteile";
      // 
      // cbxConectors
      // 
      this.cbxConectors.AutoSize = true;
      this.cbxConectors.Checked = true;
      this.cbxConectors.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbxConectors.Location = new System.Drawing.Point(235, 19);
      this.cbxConectors.Name = "cbxConectors";
      this.cbxConectors.Size = new System.Drawing.Size(106, 17);
      this.cbxConectors.TabIndex = 118;
      this.cbxConectors.Text = "Beachte Stecker";
      this.cbxConectors.UseVisualStyleBackColor = true;
      this.cbxConectors.CheckedChanged += new System.EventHandler(this.cbxConectors_CheckedChanged);
      // 
      // radioTestberichte
      // 
      this.radioTestberichte.AutoSize = true;
      this.radioTestberichte.Location = new System.Drawing.Point(123, 19);
      this.radioTestberichte.Name = "radioTestberichte";
      this.radioTestberichte.Size = new System.Drawing.Size(84, 17);
      this.radioTestberichte.TabIndex = 117;
      this.radioTestberichte.Text = "Testberichte";
      this.radioTestberichte.UseVisualStyleBackColor = false;
      this.radioTestberichte.CheckedChanged += new System.EventHandler(this.radioTestberichte_CheckedChanged);
      // 
      // radioPreisvergleich
      // 
      this.radioPreisvergleich.AutoSize = true;
      this.radioPreisvergleich.Checked = true;
      this.radioPreisvergleich.Location = new System.Drawing.Point(17, 19);
      this.radioPreisvergleich.Name = "radioPreisvergleich";
      this.radioPreisvergleich.Size = new System.Drawing.Size(91, 17);
      this.radioPreisvergleich.TabIndex = 116;
      this.radioPreisvergleich.TabStop = true;
      this.radioPreisvergleich.Text = "Preisvergleich";
      this.radioPreisvergleich.UseVisualStyleBackColor = true;
      this.radioPreisvergleich.CheckedChanged += new System.EventHandler(this.radioPreisvergleich_CheckedChanged);
      // 
      // cmdCopyToForum
      // 
      this.cmdCopyToForum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdCopyToForum.Location = new System.Drawing.Point(11, 218);
      this.cmdCopyToForum.Name = "cmdCopyToForum";
      this.cmdCopyToForum.Size = new System.Drawing.Size(375, 23);
      this.cmdCopyToForum.TabIndex = 115;
      this.cmdCopyToForum.Text = "Netzteile in die Zwischenablage Kopieren";
      this.cmdCopyToForum.UseVisualStyleBackColor = true;
      this.cmdCopyToForum.Click += new System.EventHandler(this.cmdCopyToForum_Click);
      // 
      // pnlNetzteile
      // 
      this.pnlNetzteile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlNetzteile.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.pnlNetzteile.Location = new System.Drawing.Point(13, 43);
      this.pnlNetzteile.Name = "pnlNetzteile";
      this.pnlNetzteile.Size = new System.Drawing.Size(370, 175);
      this.pnlNetzteile.TabIndex = 1;
      // 
      // gbxVerbrauch
      // 
      this.gbxVerbrauch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gbxVerbrauch.BackColor = System.Drawing.Color.Transparent;
      this.gbxVerbrauch.Controls.Add(this.PictureBox1);
      this.gbxVerbrauch.Controls.Add(this.Label8);
      this.gbxVerbrauch.Controls.Add(this.pgbEffizienz);
      this.gbxVerbrauch.Controls.Add(this.PictureBox4);
      this.gbxVerbrauch.Controls.Add(this.Label16);
      this.gbxVerbrauch.Controls.Add(this.lblVerbrauch);
      this.gbxVerbrauch.Location = new System.Drawing.Point(487, 3);
      this.gbxVerbrauch.Name = "gbxVerbrauch";
      this.gbxVerbrauch.Size = new System.Drawing.Size(392, 100);
      this.gbxVerbrauch.TabIndex = 30;
      this.gbxVerbrauch.TabStop = false;
      this.gbxVerbrauch.Text = "Verbrauch";
      // 
      // PictureBox1
      // 
      this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.PictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PictureBox1.BackgroundImage")));
      this.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.PictureBox1.Location = new System.Drawing.Point(340, 51);
      this.PictureBox1.Name = "PictureBox1";
      this.PictureBox1.Size = new System.Drawing.Size(48, 46);
      this.PictureBox1.TabIndex = 27;
      this.PictureBox1.TabStop = false;
      // 
      // Label8
      // 
      this.Label8.AutoSize = true;
      this.Label8.BackColor = System.Drawing.Color.Transparent;
      this.Label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Label8.ForeColor = System.Drawing.Color.Black;
      this.Label8.Location = new System.Drawing.Point(290, 25);
      this.Label8.Name = "Label8";
      this.Label8.Size = new System.Drawing.Size(60, 29);
      this.Label8.TabIndex = 26;
      this.Label8.Text = "Watt";
      // 
      // pgbEffizienz
      // 
      this.pgbEffizienz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pgbEffizienz.Location = new System.Drawing.Point(13, 58);
      this.pgbEffizienz.Maximum = 1000;
      this.pgbEffizienz.Name = "pgbEffizienz";
      this.pgbEffizienz.Size = new System.Drawing.Size(321, 10);
      this.pgbEffizienz.TabIndex = 25;
      this.pgbEffizienz.Value = 20;
      // 
      // PictureBox4
      // 
      this.PictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
      this.PictureBox4.Location = new System.Drawing.Point(13, 74);
      this.PictureBox4.Name = "PictureBox4";
      this.PictureBox4.Size = new System.Drawing.Size(321, 15);
      this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.PictureBox4.TabIndex = 24;
      this.PictureBox4.TabStop = false;
      // 
      // Label16
      // 
      this.Label16.AutoSize = true;
      this.Label16.BackColor = System.Drawing.Color.Transparent;
      this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Label16.ForeColor = System.Drawing.Color.Black;
      this.Label16.Location = new System.Drawing.Point(6, 25);
      this.Label16.Name = "Label16";
      this.Label16.Size = new System.Drawing.Size(208, 29);
      this.Label16.TabIndex = 22;
      this.Label16.Text = "Gesamtverbrauch:";
      // 
      // lblVerbrauch
      // 
      this.lblVerbrauch.AutoSize = true;
      this.lblVerbrauch.BackColor = System.Drawing.Color.Transparent;
      this.lblVerbrauch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblVerbrauch.ForeColor = System.Drawing.Color.Black;
      this.lblVerbrauch.Location = new System.Drawing.Point(211, 26);
      this.lblVerbrauch.Name = "lblVerbrauch";
      this.lblVerbrauch.Size = new System.Drawing.Size(39, 29);
      this.lblVerbrauch.TabIndex = 22;
      this.lblVerbrauch.Text = "20";
      // 
      // gbxDaten
      // 
      this.gbxDaten.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.gbxDaten.Controls.Add(this.pnlRow2);
      this.gbxDaten.Controls.Add(this.cmdCopySystem);
      this.gbxDaten.Controls.Add(this.label9);
      this.gbxDaten.Controls.Add(this.cbxPhysx);
      this.gbxDaten.Controls.Add(this.lblPhysX);
      this.gbxDaten.Controls.Add(this.chkCFSLI);
      this.gbxDaten.Controls.Add(this.lblSSD);
      this.gbxDaten.Controls.Add(this.cbxSSD);
      this.gbxDaten.Controls.Add(this.chkdualcpu);
      this.gbxDaten.Controls.Add(this.cbxCpu);
      this.gbxDaten.Controls.Add(this.lblCPU);
      this.gbxDaten.Controls.Add(this.lblKuehlung);
      this.gbxDaten.Controls.Add(this.cbxCooling);
      this.gbxDaten.Controls.Add(this.lblHDD);
      this.gbxDaten.Controls.Add(this.cbxHDD);
      this.gbxDaten.Controls.Add(this.lblLaufwerke);
      this.gbxDaten.Controls.Add(this.cbxLaufwerke);
      this.gbxDaten.Controls.Add(this.lblLuefter);
      this.gbxDaten.Controls.Add(this.cbxFans);
      this.gbxDaten.Controls.Add(this.lblOC);
      this.gbxDaten.Controls.Add(this.cbxOverclocking);
      this.gbxDaten.Controls.Add(this.cbxCPU2);
      this.gbxDaten.Controls.Add(this.lblCPU2);
      this.gbxDaten.Location = new System.Drawing.Point(3, 3);
      this.gbxDaten.Name = "gbxDaten";
      this.gbxDaten.Size = new System.Drawing.Size(478, 352);
      this.gbxDaten.TabIndex = 0;
      this.gbxDaten.TabStop = false;
      this.gbxDaten.Text = "Daten";
      // 
      // pnlRow2
      // 
      this.pnlRow2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlRow2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.pnlRow2.Location = new System.Drawing.Point(270, 74);
      this.pnlRow2.Name = "pnlRow2";
      this.pnlRow2.Size = new System.Drawing.Size(200, 238);
      this.pnlRow2.TabIndex = 123;
      // 
      // cmdCopySystem
      // 
      this.cmdCopySystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdCopySystem.Location = new System.Drawing.Point(294, 318);
      this.cmdCopySystem.Name = "cmdCopySystem";
      this.cmdCopySystem.Size = new System.Drawing.Size(168, 23);
      this.cmdCopySystem.TabIndex = 122;
      this.cmdCopySystem.Text = "Kopiere Informationen";
      this.cmdCopySystem.UseVisualStyleBackColor = true;
      this.cmdCopySystem.Click += new System.EventHandler(this.cmdCopySystem_Click);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.BackColor = System.Drawing.Color.Transparent;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.ForeColor = System.Drawing.Color.Black;
      this.label9.Location = new System.Drawing.Point(273, 14);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(127, 18);
      this.label9.TabIndex = 119;
      this.label9.Text = "OC und High End:";
      // 
      // cbxPhysx
      // 
      this.cbxPhysx.FormattingEnabled = true;
      this.cbxPhysx.Location = new System.Drawing.Point(67, 70);
      this.cbxPhysx.Name = "cbxPhysx";
      this.cbxPhysx.Size = new System.Drawing.Size(193, 21);
      this.cbxPhysx.TabIndex = 118;
      this.cbxPhysx.Text = "Bitte wählen";
      // 
      // lblPhysX
      // 
      this.lblPhysX.AutoSize = true;
      this.lblPhysX.BackColor = System.Drawing.Color.Transparent;
      this.lblPhysX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPhysX.ForeColor = System.Drawing.Color.Black;
      this.lblPhysX.Location = new System.Drawing.Point(9, 74);
      this.lblPhysX.Name = "lblPhysX";
      this.lblPhysX.Size = new System.Drawing.Size(48, 15);
      this.lblPhysX.TabIndex = 117;
      this.lblPhysX.Text = "Phys-X:";
      // 
      // chkCFSLI
      // 
      this.chkCFSLI.AutoSize = true;
      this.chkCFSLI.Location = new System.Drawing.Point(276, 51);
      this.chkCFSLI.Name = "chkCFSLI";
      this.chkCFSLI.Size = new System.Drawing.Size(66, 17);
      this.chkCFSLI.TabIndex = 105;
      this.chkCFSLI.Text = "SLI / CF";
      this.chkCFSLI.UseVisualStyleBackColor = true;
      this.chkCFSLI.CheckedChanged += new System.EventHandler(this.chkCFSLI_CheckedChanged);
      // 
      // lblSSD
      // 
      this.lblSSD.AutoSize = true;
      this.lblSSD.BackColor = System.Drawing.Color.Transparent;
      this.lblSSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSSD.ForeColor = System.Drawing.Color.Black;
      this.lblSSD.Location = new System.Drawing.Point(9, 158);
      this.lblSSD.Name = "lblSSD";
      this.lblSSD.Size = new System.Drawing.Size(41, 15);
      this.lblSSD.TabIndex = 104;
      this.lblSSD.Text = "SSDs:";
      // 
      // cbxSSD
      // 
      this.cbxSSD.FormattingEnabled = true;
      this.cbxSSD.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
      this.cbxSSD.Location = new System.Drawing.Point(52, 155);
      this.cbxSSD.Name = "cbxSSD";
      this.cbxSSD.Size = new System.Drawing.Size(208, 21);
      this.cbxSSD.TabIndex = 103;
      this.cbxSSD.Text = "Bitte wählen";
      // 
      // chkdualcpu
      // 
      this.chkdualcpu.AutoSize = true;
      this.chkdualcpu.Location = new System.Drawing.Point(276, 35);
      this.chkdualcpu.Name = "chkdualcpu";
      this.chkdualcpu.Size = new System.Drawing.Size(137, 17);
      this.chkdualcpu.TabIndex = 101;
      this.chkdualcpu.Text = "Dual Sockel Mainboard";
      this.chkdualcpu.UseVisualStyleBackColor = true;
      this.chkdualcpu.CheckedChanged += new System.EventHandler(this.chkdualcpu_CheckedChanged);
      // 
      // cbxCpu
      // 
      this.cbxCpu.FormattingEnabled = true;
      this.cbxCpu.Location = new System.Drawing.Point(52, 13);
      this.cbxCpu.Name = "cbxCpu";
      this.cbxCpu.Size = new System.Drawing.Size(208, 21);
      this.cbxCpu.TabIndex = 63;
      this.cbxCpu.Text = "Bitte wählen";
      // 
      // lblCPU
      // 
      this.lblCPU.AutoSize = true;
      this.lblCPU.BackColor = System.Drawing.Color.Transparent;
      this.lblCPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCPU.ForeColor = System.Drawing.Color.Black;
      this.lblCPU.Location = new System.Drawing.Point(9, 16);
      this.lblCPU.Name = "lblCPU";
      this.lblCPU.Size = new System.Drawing.Size(35, 15);
      this.lblCPU.TabIndex = 64;
      this.lblCPU.Text = "CPU:";
      // 
      // lblKuehlung
      // 
      this.lblKuehlung.AutoSize = true;
      this.lblKuehlung.BackColor = System.Drawing.Color.Transparent;
      this.lblKuehlung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblKuehlung.ForeColor = System.Drawing.Color.Black;
      this.lblKuehlung.Location = new System.Drawing.Point(9, 102);
      this.lblKuehlung.Name = "lblKuehlung";
      this.lblKuehlung.Size = new System.Drawing.Size(56, 15);
      this.lblKuehlung.TabIndex = 67;
      this.lblKuehlung.Text = "Kühlung:";
      // 
      // cbxCooling
      // 
      this.cbxCooling.FormattingEnabled = true;
      this.cbxCooling.Location = new System.Drawing.Point(67, 99);
      this.cbxCooling.Name = "cbxCooling";
      this.cbxCooling.Size = new System.Drawing.Size(193, 21);
      this.cbxCooling.TabIndex = 68;
      this.cbxCooling.Text = "Bitte wählen";
      // 
      // lblHDD
      // 
      this.lblHDD.AutoSize = true;
      this.lblHDD.BackColor = System.Drawing.Color.Transparent;
      this.lblHDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblHDD.ForeColor = System.Drawing.Color.Black;
      this.lblHDD.Location = new System.Drawing.Point(9, 131);
      this.lblHDD.Name = "lblHDD";
      this.lblHDD.Size = new System.Drawing.Size(70, 15);
      this.lblHDD.TabIndex = 69;
      this.lblHDD.Text = "Festplatten:";
      // 
      // cbxHDD
      // 
      this.cbxHDD.FormattingEnabled = true;
      this.cbxHDD.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
      this.cbxHDD.Location = new System.Drawing.Point(79, 128);
      this.cbxHDD.Name = "cbxHDD";
      this.cbxHDD.Size = new System.Drawing.Size(181, 21);
      this.cbxHDD.TabIndex = 70;
      this.cbxHDD.Text = "Bitte wählen";
      // 
      // lblLaufwerke
      // 
      this.lblLaufwerke.AutoSize = true;
      this.lblLaufwerke.BackColor = System.Drawing.Color.Transparent;
      this.lblLaufwerke.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLaufwerke.ForeColor = System.Drawing.Color.Black;
      this.lblLaufwerke.Location = new System.Drawing.Point(9, 185);
      this.lblLaufwerke.Name = "lblLaufwerke";
      this.lblLaufwerke.Size = new System.Drawing.Size(67, 15);
      this.lblLaufwerke.TabIndex = 71;
      this.lblLaufwerke.Text = "Laufwerke:";
      // 
      // cbxLaufwerke
      // 
      this.cbxLaufwerke.FormattingEnabled = true;
      this.cbxLaufwerke.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
      this.cbxLaufwerke.Location = new System.Drawing.Point(76, 182);
      this.cbxLaufwerke.Name = "cbxLaufwerke";
      this.cbxLaufwerke.Size = new System.Drawing.Size(184, 21);
      this.cbxLaufwerke.TabIndex = 72;
      this.cbxLaufwerke.Text = "Bitte wählen";
      // 
      // lblLuefter
      // 
      this.lblLuefter.AutoSize = true;
      this.lblLuefter.BackColor = System.Drawing.Color.Transparent;
      this.lblLuefter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLuefter.ForeColor = System.Drawing.Color.Black;
      this.lblLuefter.Location = new System.Drawing.Point(9, 212);
      this.lblLuefter.Name = "lblLuefter";
      this.lblLuefter.Size = new System.Drawing.Size(41, 15);
      this.lblLuefter.TabIndex = 73;
      this.lblLuefter.Text = "Lüfter:";
      // 
      // cbxFans
      // 
      this.cbxFans.FormattingEnabled = true;
      this.cbxFans.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
      this.cbxFans.Location = new System.Drawing.Point(52, 209);
      this.cbxFans.Name = "cbxFans";
      this.cbxFans.Size = new System.Drawing.Size(208, 21);
      this.cbxFans.TabIndex = 74;
      this.cbxFans.Text = "Bitte wählen";
      // 
      // lblOC
      // 
      this.lblOC.AutoSize = true;
      this.lblOC.BackColor = System.Drawing.Color.Transparent;
      this.lblOC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOC.ForeColor = System.Drawing.Color.Black;
      this.lblOC.Location = new System.Drawing.Point(9, 239);
      this.lblOC.Name = "lblOC";
      this.lblOC.Size = new System.Drawing.Size(31, 15);
      this.lblOC.TabIndex = 75;
      this.lblOC.Text = "OC?";
      // 
      // cbxOverclocking
      // 
      this.cbxOverclocking.FormattingEnabled = true;
      this.cbxOverclocking.Location = new System.Drawing.Point(42, 236);
      this.cbxOverclocking.Name = "cbxOverclocking";
      this.cbxOverclocking.Size = new System.Drawing.Size(218, 21);
      this.cbxOverclocking.TabIndex = 76;
      this.cbxOverclocking.Text = "Bitte wählen";
      // 
      // cbxCPU2
      // 
      this.cbxCPU2.FormattingEnabled = true;
      this.cbxCPU2.Location = new System.Drawing.Point(53, 263);
      this.cbxCPU2.Name = "cbxCPU2";
      this.cbxCPU2.Size = new System.Drawing.Size(207, 21);
      this.cbxCPU2.TabIndex = 120;
      this.cbxCPU2.Text = "Bitte wählen";
      this.cbxCPU2.Visible = false;
      // 
      // lblCPU2
      // 
      this.lblCPU2.AutoSize = true;
      this.lblCPU2.BackColor = System.Drawing.Color.Transparent;
      this.lblCPU2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCPU2.ForeColor = System.Drawing.Color.Black;
      this.lblCPU2.Location = new System.Drawing.Point(9, 264);
      this.lblCPU2.Name = "lblCPU2";
      this.lblCPU2.Size = new System.Drawing.Size(45, 15);
      this.lblCPU2.TabIndex = 121;
      this.lblCPU2.Text = "CPU 2:";
      this.lblCPU2.Visible = false;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.ClientSize = new System.Drawing.Size(882, 448);
      this.Controls.Add(this.PnlContent);
      this.Controls.Add(this.menuStrip1);
      this.Controls.Add(this.PnlHeader);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.MinimumSize = new System.Drawing.Size(850, 430);
      this.Name = "Form1";
      this.Text = "PSU Calculator";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.PnlContent.ResumeLayout(false);
      this.PnlContent.PerformLayout();
      this.gbxNetzteile.ResumeLayout(false);
      this.gbxNetzteile.PerformLayout();
      this.gbxVerbrauch.ResumeLayout(false);
      this.gbxVerbrauch.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
      this.gbxDaten.ResumeLayout(false);
      this.gbxDaten.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    void cbxCpu_TextChanged(object sender, System.EventArgs e)
    {
      //LoadComboboxen.LoadCPU(this.cbxCpu);

    }


    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem supportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem fAQToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem nützlicheLinksToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optionenToolStripMenuItem;
    private System.Windows.Forms.Panel PnlHeader;
    private System.Windows.Forms.Panel PnlContent;
    internal System.Windows.Forms.GroupBox gbxVerbrauch;
    internal System.Windows.Forms.PictureBox PictureBox1;
    internal System.Windows.Forms.Label Label8;
    internal System.Windows.Forms.ProgressBar pgbEffizienz;
    internal System.Windows.Forms.PictureBox PictureBox4;
    internal System.Windows.Forms.Label Label16;
    internal System.Windows.Forms.Label lblVerbrauch;
    private System.Windows.Forms.GroupBox gbxDaten;
    internal System.Windows.Forms.GroupBox gbxNetzteile;
    internal System.Windows.Forms.LinkLabel LinkLabel4;
    internal System.Windows.Forms.Label Label17;
    internal System.Windows.Forms.Label label9;
    internal System.Windows.Forms.ComboBox cbxPhysx;
    internal System.Windows.Forms.Label lblPhysX;
    internal System.Windows.Forms.CheckBox chkCFSLI;
    internal System.Windows.Forms.Label lblSSD;
    internal System.Windows.Forms.ComboBox cbxSSD;
    internal System.Windows.Forms.CheckBox chkdualcpu;
    internal System.Windows.Forms.ComboBox cbxCpu;
    internal System.Windows.Forms.Label lblCPU;
    internal System.Windows.Forms.Label lblKuehlung;
    internal System.Windows.Forms.ComboBox cbxCooling;
    internal System.Windows.Forms.Label lblHDD;
    internal System.Windows.Forms.ComboBox cbxHDD;
    internal System.Windows.Forms.Label lblLaufwerke;
    internal System.Windows.Forms.ComboBox cbxLaufwerke;
    internal System.Windows.Forms.Label lblLuefter;
    internal System.Windows.Forms.ComboBox cbxFans;
    internal System.Windows.Forms.Label lblOC;
    internal System.Windows.Forms.ComboBox cbxOverclocking;
    internal System.Windows.Forms.ComboBox cbxCPU2;
    internal System.Windows.Forms.Label lblCPU2;
    internal List<System.Windows.Forms.ComboBox> cbxGrakaList = new List<System.Windows.Forms.ComboBox>();
    internal List<System.Windows.Forms.Label> lblGrakaList = new List<System.Windows.Forms.Label>();
    private System.Windows.Forms.ToolStripMenuItem allgemeinerDiskussionsthreadZuNetzteilenToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem singleRailVsMultiRailWasIstDasVorteileUndNachteileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem wieManAlsLaieEinGünstigesVonEinemBilligenNetzteilUnterscheidetToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
    private System.Windows.Forms.Button cmdCopyToForum;
    internal System.Windows.Forms.FlowLayoutPanel pnlNetzteile;
    private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem spracheToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deutschToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem suchmaschineToolStripMenuItem;
    private System.Windows.Forms.RadioButton radioPreisvergleich;
    private System.Windows.Forms.RadioButton radioTestberichte;
    private System.Windows.Forms.Button cmdCopySystem;
    private System.Windows.Forms.CheckBox cbxConectors;
    private System.Windows.Forms.ToolStripMenuItem datenSpeicherortToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem lokalerOrdnerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem appDataToolStripMenuItem;
    private System.Windows.Forms.Panel pnlRow2;

  }
}

