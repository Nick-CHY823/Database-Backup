<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lb_Time = New System.Windows.Forms.Label()
        Me.tb_BackupInfo = New System.Windows.Forms.TextBox()
        Me.bt_start_bk = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(303, 55)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Daily Backup" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lb_Time
        '
        Me.lb_Time.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_Time.Location = New System.Drawing.Point(32, 95)
        Me.lb_Time.Name = "lb_Time"
        Me.lb_Time.Size = New System.Drawing.Size(256, 37)
        Me.lb_Time.TabIndex = 5
        Me.lb_Time.Text = "Time"
        Me.lb_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tb_BackupInfo
        '
        Me.tb_BackupInfo.CausesValidation = False
        Me.tb_BackupInfo.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.tb_BackupInfo.Location = New System.Drawing.Point(12, 154)
        Me.tb_BackupInfo.Multiline = True
        Me.tb_BackupInfo.Name = "tb_BackupInfo"
        Me.tb_BackupInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tb_BackupInfo.Size = New System.Drawing.Size(302, 215)
        Me.tb_BackupInfo.TabIndex = 6
        Me.tb_BackupInfo.WordWrap = False
        '
        'bt_start_bk
        '
        Me.bt_start_bk.BackColor = System.Drawing.Color.RoyalBlue
        Me.bt_start_bk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bt_start_bk.ForeColor = System.Drawing.Color.Black
        Me.bt_start_bk.Location = New System.Drawing.Point(12, 375)
        Me.bt_start_bk.Name = "bt_start_bk"
        Me.bt_start_bk.Size = New System.Drawing.Size(302, 44)
        Me.bt_start_bk.TabIndex = 7
        Me.bt_start_bk.Text = "Execute"
        Me.bt_start_bk.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(60, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Azure
        Me.ClientSize = New System.Drawing.Size(334, 431)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.bt_start_bk)
        Me.Controls.Add(Me.tb_BackupInfo)
        Me.Controls.Add(Me.lb_Time)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Form1"
        Me.Text = "Automatic DB Backup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lb_Time As System.Windows.Forms.Label
    Friend WithEvents tb_BackupInfo As System.Windows.Forms.TextBox
    Friend WithEvents bt_start_bk As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
