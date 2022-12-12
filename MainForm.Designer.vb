<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.tGUIUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.tbNMEAMessages = New System.Windows.Forms.TextBox()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnStoreMessages = New System.Windows.Forms.Button()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mpMap = New System.Windows.Forms.MapControl()
        Me.btnZoomIn = New System.Windows.Forms.Button()
        Me.btnZoomOut = New System.Windows.Forms.Button()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsslWinPosition = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tUpdateTCP = New System.Windows.Forms.Timer(Me.components)
        Me.btnStartTCP = New System.Windows.Forms.Button()
        Me.pgMain = New System.Windows.Forms.PropertyGrid()
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile_EXELocation = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiFile_End = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpMain.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.msMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(12, 37)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(105, 37)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start recording"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'tGUIUpdate
        '
        Me.tGUIUpdate.Enabled = True
        '
        'tbNMEAMessages
        '
        Me.tbNMEAMessages.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNMEAMessages.Location = New System.Drawing.Point(12, 91)
        Me.tbNMEAMessages.Multiline = True
        Me.tbNMEAMessages.Name = "tbNMEAMessages"
        Me.tbNMEAMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbNMEAMessages.Size = New System.Drawing.Size(327, 226)
        Me.tbNMEAMessages.TabIndex = 1
        Me.tbNMEAMessages.WordWrap = False
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(123, 37)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(105, 37)
        Me.btnStop.TabIndex = 2
        Me.btnStop.Text = "Stop recording"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnStoreMessages
        '
        Me.btnStoreMessages.Location = New System.Drawing.Point(234, 37)
        Me.btnStoreMessages.Name = "btnStoreMessages"
        Me.btnStoreMessages.Size = New System.Drawing.Size(105, 37)
        Me.btnStoreMessages.TabIndex = 3
        Me.btnStoreMessages.Text = "Store messages"
        Me.btnStoreMessages.UseVisualStyleBackColor = True
        '
        'dgvMain
        '
        Me.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Value, Me.Type})
        Me.dgvMain.Location = New System.Drawing.Point(345, 48)
        Me.dgvMain.Name = "dgvMain"
        Me.dgvMain.Size = New System.Drawing.Size(415, 269)
        Me.dgvMain.TabIndex = 4
        '
        'Value
        '
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        '
        'Type
        '
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        '
        'mpMap
        '
        Me.mpMap.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mpMap.Cursor = System.Windows.Forms.Cursors.Cross
        Me.mpMap.ErrorColor = System.Drawing.Color.Red
        Me.mpMap.FitToBounds = True
        Me.mpMap.Location = New System.Drawing.Point(12, 403)
        Me.mpMap.Name = "mpMap"
        Me.mpMap.ShowThumbnails = True
        Me.mpMap.Size = New System.Drawing.Size(748, 449)
        Me.mpMap.TabIndex = 5
        Me.mpMap.Text = "Map"
        Me.mpMap.ThumbnailBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.mpMap.ThumbnailForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(176, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.mpMap.ThumbnailText = "Downloading..."
        Me.mpMap.TileImageAttributes = Nothing
        Me.mpMap.ZoomLevel = 5
        '
        'btnZoomIn
        '
        Me.btnZoomIn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnZoomIn.Location = New System.Drawing.Point(3, 223)
        Me.btnZoomIn.Name = "btnZoomIn"
        Me.btnZoomIn.Size = New System.Drawing.Size(151, 43)
        Me.btnZoomIn.TabIndex = 6
        Me.btnZoomIn.Text = "Zoom IN"
        Me.btnZoomIn.UseVisualStyleBackColor = True
        '
        'btnZoomOut
        '
        Me.btnZoomOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnZoomOut.Location = New System.Drawing.Point(160, 223)
        Me.btnZoomOut.Name = "btnZoomOut"
        Me.btnZoomOut.Size = New System.Drawing.Size(152, 43)
        Me.btnZoomOut.TabIndex = 7
        Me.btnZoomOut.Text = "Zoom OUT"
        Me.btnZoomOut.UseVisualStyleBackColor = True
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.Controls.Add(Me.btnZoomIn, 0, 5)
        Me.tlpMain.Controls.Add(Me.btnZoomOut, 1, 5)
        Me.tlpMain.Location = New System.Drawing.Point(766, 48)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 6
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tlpMain.Size = New System.Drawing.Size(315, 269)
        Me.tlpMain.TabIndex = 8
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslWinPosition})
        Me.ssMain.Location = New System.Drawing.Point(0, 875)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1299, 22)
        Me.ssMain.TabIndex = 10
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsslWinPosition
        '
        Me.tsslWinPosition.Name = "tsslWinPosition"
        Me.tsslWinPosition.Size = New System.Drawing.Size(47, 17)
        Me.tsslWinPosition.Text = "Pos: ---"
        '
        'tUpdateTCP
        '
        Me.tUpdateTCP.Enabled = True
        '
        'btnStartTCP
        '
        Me.btnStartTCP.Location = New System.Drawing.Point(1087, 42)
        Me.btnStartTCP.Name = "btnStartTCP"
        Me.btnStartTCP.Size = New System.Drawing.Size(105, 32)
        Me.btnStartTCP.TabIndex = 11
        Me.btnStartTCP.Text = "Start TCP"
        Me.btnStartTCP.UseVisualStyleBackColor = True
        '
        'pgMain
        '
        Me.pgMain.Location = New System.Drawing.Point(769, 403)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(518, 460)
        Me.pgMain.TabIndex = 12
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(1299, 24)
        Me.msMain.TabIndex = 13
        Me.msMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile_EXELocation, Me.ToolStripMenuItem1, Me.tsmiFile_End})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'tsmiFile_EXELocation
        '
        Me.tsmiFile_EXELocation.Name = "tsmiFile_EXELocation"
        Me.tsmiFile_EXELocation.Size = New System.Drawing.Size(180, 22)
        Me.tsmiFile_EXELocation.Text = "Explorer @ EXE path"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(177, 6)
        '
        'tsmiFile_End
        '
        Me.tsmiFile_End.Name = "tsmiFile_End"
        Me.tsmiFile_End.Size = New System.Drawing.Size(180, 22)
        Me.tsmiFile_End.Text = "End"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1299, 897)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.btnStartTCP)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.msMain)
        Me.Controls.Add(Me.tlpMain)
        Me.Controls.Add(Me.mpMap)
        Me.Controls.Add(Me.dgvMain)
        Me.Controls.Add(Me.btnStoreMessages)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.tbNMEAMessages)
        Me.Controls.Add(Me.btnStart)
        Me.MainMenuStrip = Me.msMain
        Me.Name = "MainForm"
        Me.Text = "GPSRecordPlus"
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpMain.ResumeLayout(False)
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStart As Button
    Friend WithEvents tGUIUpdate As Timer
    Friend WithEvents tbNMEAMessages As TextBox
    Friend WithEvents btnStop As Button
    Friend WithEvents btnStoreMessages As Button
    Friend WithEvents dgvMain As DataGridView
    Friend WithEvents Value As DataGridViewTextBoxColumn
    Friend WithEvents Type As DataGridViewTextBoxColumn
    Friend WithEvents mpMap As MapControl
    Friend WithEvents btnZoomIn As Button
    Friend WithEvents btnZoomOut As Button
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsslWinPosition As ToolStripStatusLabel
    Friend WithEvents tUpdateTCP As Timer
    Friend WithEvents btnStartTCP As Button
    Friend WithEvents pgMain As PropertyGrid
    Friend WithEvents msMain As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsmiFile_EXELocation As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents tsmiFile_End As ToolStripMenuItem
End Class
