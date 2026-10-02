<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.FolderS1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FolderS2 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DelRemoteFile = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ADB_Log = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.AllowDrop = True
        Me.TextBox1.Location = New System.Drawing.Point(7, 30)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(797, 283)
        Me.TextBox1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(7, 319)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(797, 52)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "选择本地转换源文件夹"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 574)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(818, 57)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "选取文件夹"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.AllowDrop = True
        Me.TextBox2.Location = New System.Drawing.Point(72, 533)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(751, 35)
        Me.TextBox2.TabIndex = 2
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(7, 377)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(797, 52)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "本地转换(移动并重命名)"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'FolderS1
        '
        Me.FolderS1.ShowNewFolderButton = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(250, 24)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "本地转换的原始文件夹"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 536)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 24)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "存档"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(7, 372)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(797, 60)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "ADB快捷拉取并转换(pull不删除手机数据)"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(826, 519)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DelRemoteFile)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Button4)
        Me.TabPage1.Location = New System.Drawing.Point(8, 39)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(810, 472)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ADB快捷拉取"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DelRemoteFile
        '
        Me.DelRemoteFile.AutoSize = True
        Me.DelRemoteFile.Location = New System.Drawing.Point(7, 438)
        Me.DelRemoteFile.Name = "DelRemoteFile"
        Me.DelRemoteFile.Size = New System.Drawing.Size(378, 28)
        Me.DelRemoteFile.TabIndex = 10
        Me.DelRemoteFile.Text = "数据拉取完成后删除手机端数据"
        Me.DelRemoteFile.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ADB_Log)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(789, 360)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "日志"
        '
        'ADB_Log
        '
        Me.ADB_Log.Location = New System.Drawing.Point(6, 29)
        Me.ADB_Log.Multiline = True
        Me.ADB_Log.Name = "ADB_Log"
        Me.ADB_Log.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ADB_Log.Size = New System.Drawing.Size(773, 325)
        Me.ADB_Log.TabIndex = 8
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.TextBox1)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.Button3)
        Me.TabPage2.Location = New System.Drawing.Point(8, 39)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(810, 472)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "本地转换"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TextBox4)
        Me.TabPage3.Controls.Add(Me.Label4)
        Me.TabPage3.Controls.Add(Me.LinkLabel1)
        Me.TabPage3.Controls.Add(Me.TextBox3)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Location = New System.Drawing.Point(8, 39)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(810, 472)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "关于"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(238, 24)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "ADB工具官方下载地址"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(10, 39)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(794, 35)
        Me.TextBox3.TabIndex = 1
        Me.TextBox3.Text = "https://developer.android.google.cn/tools/releases/platform-tools?hl=zh-cn"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(250, 12)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(58, 24)
        Me.LinkLabel1.TabIndex = 2
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "访问"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 24)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "关于此工具"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(10, 138)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(794, 328)
        Me.TextBox4.TabIndex = 4
        Me.TextBox4.Text = resources.GetString("TextBox4.Text")
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(837, 643)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox2)
        Me.Name = "Form1"
        Me.Text = "textstatus文件夹操作"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents FolderS1 As FolderBrowserDialog
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents FolderS2 As FolderBrowserDialog
    Friend WithEvents Button4 As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ADB_Log As TextBox
    Friend WithEvents DelRemoteFile As CheckBox
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox4 As TextBox
End Class
