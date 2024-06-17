namespace NET.MEM.EvalForm
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.debugBorderLabel = new System.Windows.Forms.Label();
            this.inputGroup = new System.Windows.Forms.GroupBox();
            this.inputSamplingFrequencyText = new System.Windows.Forms.TextBox();
            this.inputSamplingFrequencyLabel = new System.Windows.Forms.Label();
            this.inputGenerateButton = new System.Windows.Forms.Button();
            this.inputFrequenciesText = new System.Windows.Forms.TextBox();
            this.inputGenerateModeRadio = new System.Windows.Forms.RadioButton();
            this.inputChooseCsvFileButton = new System.Windows.Forms.Button();
            this.inputCsvFilePathText = new System.Windows.Forms.TextBox();
            this.inputCsvModeRadio = new System.Windows.Forms.RadioButton();
            this.inputDataGrid = new System.Windows.Forms.DataGridView();
            this.inputDataGridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.inputDataGridSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputSignalPlotPanel = new System.Windows.Forms.Panel();
            this.resultTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.psdEnableXLogscaleCheck = new System.Windows.Forms.CheckBox();
            this.psdMaxFrequencyText = new System.Windows.Forms.TextBox();
            this.psdSymbolLabel = new System.Windows.Forms.Label();
            this.psdMinFrequensyText = new System.Windows.Forms.TextBox();
            this.psdFrequencyLabel = new System.Windows.Forms.Label();
            this.memPSDPlotPanel = new System.Windows.Forms.Panel();
            this.memResultList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.memResultLabel = new System.Windows.Forms.Label();
            this.memExecuteButton = new System.Windows.Forms.Button();
            this.maximumDegreeText = new System.Windows.Forms.TextBox();
            this.maximumDegreeLabel = new System.Windows.Forms.Label();
            this.resultPowerSpectrumDensityPage = new System.Windows.Forms.TabPage();
            this.powerSpectrumDesityDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.memDegreeDataGrid = new System.Windows.Forms.DataGridView();
            this.mainMenuStrip.SuspendLayout();
            this.inputGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGrid)).BeginInit();
            this.inputDataGridContextMenu.SuspendLayout();
            this.resultTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.resultPowerSpectrumDensityPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerSpectrumDesityDataGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memDegreeDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1184, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.fileExitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(67, 20);
            this.fileMenu.Text = "ファイル(&F)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // fileExitMenuItem
            // 
            this.fileExitMenuItem.Name = "fileExitMenuItem";
            this.fileExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.fileExitMenuItem.Size = new System.Drawing.Size(155, 22);
            this.fileExitMenuItem.Text = "終了(&X)";
            this.fileExitMenuItem.Click += new System.EventHandler(this.OnFileExitMenuClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 799);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // debugBorderLabel
            // 
            this.debugBorderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugBorderLabel.BackColor = System.Drawing.Color.Red;
            this.debugBorderLabel.Location = new System.Drawing.Point(0, 28);
            this.debugBorderLabel.Name = "debugBorderLabel";
            this.debugBorderLabel.Size = new System.Drawing.Size(1185, 1);
            this.debugBorderLabel.TabIndex = 2;
            // 
            // inputGroup
            // 
            this.inputGroup.Controls.Add(this.inputSamplingFrequencyText);
            this.inputGroup.Controls.Add(this.inputSamplingFrequencyLabel);
            this.inputGroup.Controls.Add(this.inputGenerateButton);
            this.inputGroup.Controls.Add(this.inputFrequenciesText);
            this.inputGroup.Controls.Add(this.inputGenerateModeRadio);
            this.inputGroup.Controls.Add(this.inputChooseCsvFileButton);
            this.inputGroup.Controls.Add(this.inputCsvFilePathText);
            this.inputGroup.Controls.Add(this.inputCsvModeRadio);
            this.inputGroup.Location = new System.Drawing.Point(12, 32);
            this.inputGroup.Name = "inputGroup";
            this.inputGroup.Size = new System.Drawing.Size(328, 198);
            this.inputGroup.TabIndex = 3;
            this.inputGroup.TabStop = false;
            this.inputGroup.Text = "入力(&I)";
            // 
            // inputSamplingFrequencyText
            // 
            this.inputSamplingFrequencyText.Location = new System.Drawing.Point(144, 20);
            this.inputSamplingFrequencyText.Name = "inputSamplingFrequencyText";
            this.inputSamplingFrequencyText.Size = new System.Drawing.Size(178, 25);
            this.inputSamplingFrequencyText.TabIndex = 1;
            this.inputSamplingFrequencyText.Text = "1000";
            // 
            // inputSamplingFrequencyLabel
            // 
            this.inputSamplingFrequencyLabel.AutoSize = true;
            this.inputSamplingFrequencyLabel.Location = new System.Drawing.Point(6, 23);
            this.inputSamplingFrequencyLabel.Name = "inputSamplingFrequencyLabel";
            this.inputSamplingFrequencyLabel.Size = new System.Drawing.Size(122, 18);
            this.inputSamplingFrequencyLabel.TabIndex = 0;
            this.inputSamplingFrequencyLabel.Text = "サンプリング周期(&R)";
            // 
            // inputGenerateButton
            // 
            this.inputGenerateButton.Location = new System.Drawing.Point(226, 156);
            this.inputGenerateButton.Name = "inputGenerateButton";
            this.inputGenerateButton.Size = new System.Drawing.Size(96, 28);
            this.inputGenerateButton.TabIndex = 7;
            this.inputGenerateButton.Text = "生成(&G)";
            this.inputGenerateButton.UseVisualStyleBackColor = true;
            this.inputGenerateButton.Click += new System.EventHandler(this.OnInputGenerateClick);
            // 
            // inputFrequenciesText
            // 
            this.inputFrequenciesText.Location = new System.Drawing.Point(144, 125);
            this.inputFrequenciesText.Name = "inputFrequenciesText";
            this.inputFrequenciesText.Size = new System.Drawing.Size(178, 25);
            this.inputFrequenciesText.TabIndex = 6;
            this.inputFrequenciesText.Text = "20,50,55";
            // 
            // inputGenerateModeRadio
            // 
            this.inputGenerateModeRadio.AutoSize = true;
            this.inputGenerateModeRadio.Checked = true;
            this.inputGenerateModeRadio.Location = new System.Drawing.Point(9, 126);
            this.inputGenerateModeRadio.Name = "inputGenerateModeRadio";
            this.inputGenerateModeRadio.Size = new System.Drawing.Size(103, 22);
            this.inputGenerateModeRadio.TabIndex = 5;
            this.inputGenerateModeRadio.TabStop = true;
            this.inputGenerateModeRadio.Text = "周波数指定(&F)";
            this.inputGenerateModeRadio.UseVisualStyleBackColor = true;
            this.inputGenerateModeRadio.CheckedChanged += new System.EventHandler(this.OnInputSelectionModeCheckedChanged);
            // 
            // inputChooseCsvFileButton
            // 
            this.inputChooseCsvFileButton.Enabled = false;
            this.inputChooseCsvFileButton.Location = new System.Drawing.Point(226, 59);
            this.inputChooseCsvFileButton.Name = "inputChooseCsvFileButton";
            this.inputChooseCsvFileButton.Size = new System.Drawing.Size(96, 28);
            this.inputChooseCsvFileButton.TabIndex = 3;
            this.inputChooseCsvFileButton.Text = "選択(&S)";
            this.inputChooseCsvFileButton.UseVisualStyleBackColor = true;
            this.inputChooseCsvFileButton.Click += new System.EventHandler(this.OnInputChooseCsvFileClick);
            // 
            // inputCsvFilePathText
            // 
            this.inputCsvFilePathText.BackColor = System.Drawing.SystemColors.Control;
            this.inputCsvFilePathText.ForeColor = System.Drawing.Color.Blue;
            this.inputCsvFilePathText.Location = new System.Drawing.Point(36, 90);
            this.inputCsvFilePathText.Name = "inputCsvFilePathText";
            this.inputCsvFilePathText.ReadOnly = true;
            this.inputCsvFilePathText.Size = new System.Drawing.Size(286, 25);
            this.inputCsvFilePathText.TabIndex = 4;
            // 
            // inputCsvModeRadio
            // 
            this.inputCsvModeRadio.AutoSize = true;
            this.inputCsvModeRadio.Location = new System.Drawing.Point(9, 62);
            this.inputCsvModeRadio.Name = "inputCsvModeRadio";
            this.inputCsvModeRadio.Size = new System.Drawing.Size(144, 22);
            this.inputCsvModeRadio.TabIndex = 2;
            this.inputCsvModeRadio.Text = "CSV ファイル指定(&C)";
            this.inputCsvModeRadio.UseVisualStyleBackColor = true;
            this.inputCsvModeRadio.CheckedChanged += new System.EventHandler(this.OnInputSelectionModeCheckedChanged);
            // 
            // inputDataGrid
            // 
            this.inputDataGrid.AllowUserToAddRows = false;
            this.inputDataGrid.AllowUserToDeleteRows = false;
            this.inputDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputDataGrid.ContextMenuStrip = this.inputDataGridContextMenu;
            this.inputDataGrid.Location = new System.Drawing.Point(12, 236);
            this.inputDataGrid.Name = "inputDataGrid";
            this.inputDataGrid.ReadOnly = true;
            this.inputDataGrid.RowTemplate.Height = 21;
            this.inputDataGrid.Size = new System.Drawing.Size(328, 187);
            this.inputDataGrid.TabIndex = 4;
            // 
            // inputDataGridContextMenu
            // 
            this.inputDataGridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputDataGridSaveMenuItem});
            this.inputDataGridContextMenu.Name = "inputDataGridContextMenu";
            this.inputDataGridContextMenu.Size = new System.Drawing.Size(113, 26);
            // 
            // inputDataGridSaveMenuItem
            // 
            this.inputDataGridSaveMenuItem.Name = "inputDataGridSaveMenuItem";
            this.inputDataGridSaveMenuItem.Size = new System.Drawing.Size(112, 22);
            this.inputDataGridSaveMenuItem.Text = "保存(&S)";
            this.inputDataGridSaveMenuItem.Click += new System.EventHandler(this.OnInputDataSaveMenuClick);
            // 
            // inputSignalPlotPanel
            // 
            this.inputSignalPlotPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.inputSignalPlotPanel.Location = new System.Drawing.Point(12, 429);
            this.inputSignalPlotPanel.Name = "inputSignalPlotPanel";
            this.inputSignalPlotPanel.Size = new System.Drawing.Size(328, 370);
            this.inputSignalPlotPanel.TabIndex = 5;
            // 
            // resultTabControl
            // 
            this.resultTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultTabControl.Controls.Add(this.tabPage1);
            this.resultTabControl.Controls.Add(this.resultPowerSpectrumDensityPage);
            this.resultTabControl.Controls.Add(this.tabPage2);
            this.resultTabControl.Location = new System.Drawing.Point(346, 32);
            this.resultTabControl.Name = "resultTabControl";
            this.resultTabControl.SelectedIndex = 0;
            this.resultTabControl.Size = new System.Drawing.Size(826, 764);
            this.resultTabControl.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psdEnableXLogscaleCheck);
            this.tabPage1.Controls.Add(this.psdMaxFrequencyText);
            this.tabPage1.Controls.Add(this.psdSymbolLabel);
            this.tabPage1.Controls.Add(this.psdMinFrequensyText);
            this.tabPage1.Controls.Add(this.psdFrequencyLabel);
            this.tabPage1.Controls.Add(this.memPSDPlotPanel);
            this.tabPage1.Controls.Add(this.memResultList);
            this.tabPage1.Controls.Add(this.memResultLabel);
            this.tabPage1.Controls.Add(this.memExecuteButton);
            this.tabPage1.Controls.Add(this.maximumDegreeText);
            this.tabPage1.Controls.Add(this.maximumDegreeLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(818, 733);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "#";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // psdEnableXLogscaleCheck
            // 
            this.psdEnableXLogscaleCheck.AutoSize = true;
            this.psdEnableXLogscaleCheck.Checked = true;
            this.psdEnableXLogscaleCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.psdEnableXLogscaleCheck.Location = new System.Drawing.Point(9, 96);
            this.psdEnableXLogscaleCheck.Name = "psdEnableXLogscaleCheck";
            this.psdEnableXLogscaleCheck.Size = new System.Drawing.Size(152, 22);
            this.psdEnableXLogscaleCheck.TabIndex = 6;
            this.psdEnableXLogscaleCheck.Text = "ログスケールで描画(&L)";
            this.psdEnableXLogscaleCheck.UseVisualStyleBackColor = true;
            // 
            // psdMaxFrequencyText
            // 
            this.psdMaxFrequencyText.Location = new System.Drawing.Point(204, 50);
            this.psdMaxFrequencyText.Name = "psdMaxFrequencyText";
            this.psdMaxFrequencyText.Size = new System.Drawing.Size(64, 25);
            this.psdMaxFrequencyText.TabIndex = 5;
            this.psdMaxFrequencyText.Text = "200";
            // 
            // psdSymbolLabel
            // 
            this.psdSymbolLabel.AutoSize = true;
            this.psdSymbolLabel.Location = new System.Drawing.Point(178, 53);
            this.psdSymbolLabel.Name = "psdSymbolLabel";
            this.psdSymbolLabel.Size = new System.Drawing.Size(20, 18);
            this.psdSymbolLabel.TabIndex = 4;
            this.psdSymbolLabel.Text = "～";
            // 
            // psdMinFrequensyText
            // 
            this.psdMinFrequensyText.Location = new System.Drawing.Point(108, 50);
            this.psdMinFrequensyText.Name = "psdMinFrequensyText";
            this.psdMinFrequensyText.Size = new System.Drawing.Size(64, 25);
            this.psdMinFrequensyText.TabIndex = 3;
            this.psdMinFrequensyText.Text = "0";
            // 
            // psdFrequencyLabel
            // 
            this.psdFrequencyLabel.AutoSize = true;
            this.psdFrequencyLabel.Location = new System.Drawing.Point(6, 55);
            this.psdFrequencyLabel.Name = "psdFrequencyLabel";
            this.psdFrequencyLabel.Size = new System.Drawing.Size(68, 18);
            this.psdFrequencyLabel.TabIndex = 2;
            this.psdFrequencyLabel.Text = "周波数範囲";
            // 
            // memPSDPlotPanel
            // 
            this.memPSDPlotPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memPSDPlotPanel.Location = new System.Drawing.Point(6, 151);
            this.memPSDPlotPanel.Name = "memPSDPlotPanel";
            this.memPSDPlotPanel.Size = new System.Drawing.Size(806, 576);
            this.memPSDPlotPanel.TabIndex = 10;
            // 
            // memResultList
            // 
            this.memResultList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memResultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.memResultList.Font = new System.Drawing.Font("メイリオ", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.memResultList.FullRowSelect = true;
            this.memResultList.GridLines = true;
            this.memResultList.HideSelection = false;
            this.memResultList.Location = new System.Drawing.Point(274, 24);
            this.memResultList.MultiSelect = false;
            this.memResultList.Name = "memResultList";
            this.memResultList.Size = new System.Drawing.Size(538, 121);
            this.memResultList.TabIndex = 9;
            this.memResultList.UseCompatibleStateImageBehavior = false;
            this.memResultList.View = System.Windows.Forms.View.Details;
            this.memResultList.VirtualMode = true;
            this.memResultList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.OnMEMRetrieveVirtualItem);
            this.memResultList.SelectedIndexChanged += new System.EventHandler(this.OnMEMSelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "次数";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Q";
            this.columnHeader2.Width = 313;
            // 
            // memResultLabel
            // 
            this.memResultLabel.AutoSize = true;
            this.memResultLabel.Location = new System.Drawing.Point(274, 3);
            this.memResultLabel.Name = "memResultLabel";
            this.memResultLabel.Size = new System.Drawing.Size(32, 18);
            this.memResultLabel.TabIndex = 8;
            this.memResultLabel.Text = "結果";
            // 
            // memExecuteButton
            // 
            this.memExecuteButton.Location = new System.Drawing.Point(172, 93);
            this.memExecuteButton.Name = "memExecuteButton";
            this.memExecuteButton.Size = new System.Drawing.Size(96, 28);
            this.memExecuteButton.TabIndex = 7;
            this.memExecuteButton.Text = "実行(&E)";
            this.memExecuteButton.UseVisualStyleBackColor = true;
            this.memExecuteButton.Click += new System.EventHandler(this.OnMEMExecuteClick);
            // 
            // maximumDegreeText
            // 
            this.maximumDegreeText.Location = new System.Drawing.Point(108, 16);
            this.maximumDegreeText.Name = "maximumDegreeText";
            this.maximumDegreeText.Size = new System.Drawing.Size(160, 25);
            this.maximumDegreeText.TabIndex = 1;
            this.maximumDegreeText.Text = "256";
            // 
            // maximumDegreeLabel
            // 
            this.maximumDegreeLabel.AutoSize = true;
            this.maximumDegreeLabel.Location = new System.Drawing.Point(6, 19);
            this.maximumDegreeLabel.Name = "maximumDegreeLabel";
            this.maximumDegreeLabel.Size = new System.Drawing.Size(56, 18);
            this.maximumDegreeLabel.TabIndex = 0;
            this.maximumDegreeLabel.Text = "最大次数";
            // 
            // resultPowerSpectrumDensityPage
            // 
            this.resultPowerSpectrumDensityPage.Controls.Add(this.powerSpectrumDesityDataGrid);
            this.resultPowerSpectrumDensityPage.Location = new System.Drawing.Point(4, 27);
            this.resultPowerSpectrumDensityPage.Name = "resultPowerSpectrumDensityPage";
            this.resultPowerSpectrumDensityPage.Padding = new System.Windows.Forms.Padding(3);
            this.resultPowerSpectrumDensityPage.Size = new System.Drawing.Size(818, 733);
            this.resultPowerSpectrumDensityPage.TabIndex = 1;
            this.resultPowerSpectrumDensityPage.Text = "スペクトル密度";
            this.resultPowerSpectrumDensityPage.UseVisualStyleBackColor = true;
            // 
            // powerSpectrumDesityDataGrid
            // 
            this.powerSpectrumDesityDataGrid.AllowUserToAddRows = false;
            this.powerSpectrumDesityDataGrid.AllowUserToDeleteRows = false;
            this.powerSpectrumDesityDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.powerSpectrumDesityDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.powerSpectrumDesityDataGrid.Location = new System.Drawing.Point(3, 3);
            this.powerSpectrumDesityDataGrid.Name = "powerSpectrumDesityDataGrid";
            this.powerSpectrumDesityDataGrid.ReadOnly = true;
            this.powerSpectrumDesityDataGrid.RowTemplate.Height = 21;
            this.powerSpectrumDesityDataGrid.Size = new System.Drawing.Size(812, 727);
            this.powerSpectrumDesityDataGrid.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.memDegreeDataGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(818, 733);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "次数";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // memDegreeDataGrid
            // 
            this.memDegreeDataGrid.AllowUserToAddRows = false;
            this.memDegreeDataGrid.AllowUserToDeleteRows = false;
            this.memDegreeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memDegreeDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memDegreeDataGrid.Location = new System.Drawing.Point(3, 3);
            this.memDegreeDataGrid.Name = "memDegreeDataGrid";
            this.memDegreeDataGrid.ReadOnly = true;
            this.memDegreeDataGrid.RowTemplate.Height = 21;
            this.memDegreeDataGrid.Size = new System.Drawing.Size(812, 727);
            this.memDegreeDataGrid.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 821);
            this.Controls.Add(this.resultTabControl);
            this.Controls.Add(this.inputSignalPlotPanel);
            this.Controls.Add(this.inputDataGrid);
            this.Controls.Add(this.inputGroup);
            this.Controls.Add(this.debugBorderLabel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "最大エントロピー法 検証";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.inputGroup.ResumeLayout(false);
            this.inputGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGrid)).EndInit();
            this.inputDataGridContextMenu.ResumeLayout(false);
            this.resultTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.resultPowerSpectrumDensityPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.powerSpectrumDesityDataGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memDegreeDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Label debugBorderLabel;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
        private System.Windows.Forms.GroupBox inputGroup;
        private System.Windows.Forms.Button inputGenerateButton;
        private System.Windows.Forms.TextBox inputFrequenciesText;
        private System.Windows.Forms.RadioButton inputGenerateModeRadio;
        private System.Windows.Forms.Button inputChooseCsvFileButton;
        private System.Windows.Forms.TextBox inputCsvFilePathText;
        private System.Windows.Forms.RadioButton inputCsvModeRadio;
        private System.Windows.Forms.TextBox inputSamplingFrequencyText;
        private System.Windows.Forms.Label inputSamplingFrequencyLabel;
        private System.Windows.Forms.DataGridView inputDataGrid;
        private System.Windows.Forms.Panel inputSignalPlotPanel;
        private System.Windows.Forms.TabControl resultTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage resultPowerSpectrumDensityPage;
        private System.Windows.Forms.Button memExecuteButton;
        private System.Windows.Forms.TextBox maximumDegreeText;
        private System.Windows.Forms.Label maximumDegreeLabel;
        private System.Windows.Forms.ListView memResultList;
        private System.Windows.Forms.Label memResultLabel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel memPSDPlotPanel;
        private System.Windows.Forms.DataGridView powerSpectrumDesityDataGrid;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView memDegreeDataGrid;
        private System.Windows.Forms.TextBox psdMaxFrequencyText;
        private System.Windows.Forms.Label psdSymbolLabel;
        private System.Windows.Forms.TextBox psdMinFrequensyText;
        private System.Windows.Forms.Label psdFrequencyLabel;
        private System.Windows.Forms.CheckBox psdEnableXLogscaleCheck;
        private System.Windows.Forms.ContextMenuStrip inputDataGridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem inputDataGridSaveMenuItem;
    }
}
