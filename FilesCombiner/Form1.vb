Imports AdvancedSharpAdbClient
Imports AdvancedSharpAdbClient.Models
Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports System.Text.RegularExpressions
Imports AdvancedSharpAdbClient.Receivers
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Security.Policy

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FolderS1.ShowDialog() = DialogResult.OK AndAlso System.IO.Directory.Exists(FolderS1.SelectedPath) Then
            TextBox1.Text = TextBox1.Text & (vbCrLf & FolderS1.SelectedPath)
        End If
        Dim temp = TextBox1.Text.Split(vbCrLf)
        TextBox1.Text = ""
        For Each t In temp.Distinct().ToArray()
            If Not String.IsNullOrEmpty(t) Then TextBox1.Text = TextBox1.Text & (t & vbCrLf)
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If FolderS2.ShowDialog() = DialogResult.OK Then
            TextBox2.Text = FolderS2.SelectedPath
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For Each temp In TextBox1.Text.Split(vbCrLf)
            temp = temp.Trim()
            If Not String.IsNullOrEmpty(temp) AndAlso System.IO.Directory.Exists(temp) Then
                CopyMove(New IO.DirectoryInfo(temp), TextBox2.Text.Trim())
            End If
        Next

        MsgBox("ok")
    End Sub

    Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter
        If Not e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.None
            Return
        End If
        Dim paths() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
        ' 只有当所有拖入项都是文件夹时，才允许放下
        If paths IsNot Nothing AndAlso paths.Length > 0 AndAlso paths.All(Function(p) IO.Directory.Exists(p)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop
        Dim paths() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
        If paths Is Nothing OrElse paths.Length = 0 Then Return
        ' 再次过滤，确保只保留文件夹
        Dim folders = paths.Where(Function(p) IO.Directory.Exists(p)).ToArray()
        If folders.Length = 0 Then Return
        ' 多行显示，每行一个路径
        TextBox1.Text = TextBox1.Text & vbCrLf & String.Join(Environment.NewLine, folders)
    End Sub

    Private Sub TextBox2_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox2.DragEnter
        If Not e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.None
            Return
        End If
        Dim paths() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
        ' 只要有一个是文件夹，就允许放下（文件会被忽略）
        If paths IsNot Nothing AndAlso paths.Any(Function(p) IO.Directory.Exists(p)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub TextBox2_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox2.DragDrop
        Dim paths() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
        If paths Is Nothing OrElse paths.Length = 0 Then Return

        ' 取第一个文件夹，忽略文件和其余项
        Dim firstFolder = paths.FirstOrDefault(Function(p) IO.Directory.Exists(p))
        If firstFolder IsNot Nothing Then
            TextBox2.Text = firstFolder
        End If
    End Sub

    Public Function GetAfterEffect_Filename(ByRef FileInf As System.IO.FileInfo, Optional AddIndex As String = "")
        Dim nne As String = FileInf.Name.Substring(0, FileInf.Name.Length - FileInf.Extension.Length)
        If nne.StartsWith("thumb_") OrElse nne.StartsWith("image_") Then
            Return nne & AddIndex & ".png"
        ElseIf nne.StartsWith("video_") Then
            Return nne & AddIndex & ".mp4"
        Else
            Return nne & AddIndex
        End If
    End Function

    Public Function IsSameContent(f1 As FileInfo, f2 As FileInfo) As Boolean
        ' 长度不同直接排除，省去读取(弃用，调用前已经比较了文件大小)
        'If f1.Length <> f2.Length Then Return False

        Const bufferSize As Integer = 8192
        Dim buf1(bufferSize - 1) As Byte
        Dim buf2(bufferSize - 1) As Byte

        Using s1 As FileStream = f1.OpenRead()
            Using s2 As FileStream = f2.OpenRead()
                While True
                    Dim n1 = s1.Read(buf1, 0, bufferSize)
                    Dim n2 = s2.Read(buf2, 0, bufferSize)

                    If n1 <> n2 Then Return False
                    If n1 = 0 Then Return True ' 都读到结尾

                    ' 逐字节比较本次读取的块
                    For i = 0 To n1 - 1
                        If buf1(i) <> buf2(i) Then Return False
                    Next
                End While
            End Using
        End Using
        Return Nothing '遇到错误
    End Function

    Public Sub CopyMove(Source As System.IO.DirectoryInfo, Dest As String)
        If Not System.IO.Directory.Exists(Dest) Then
            System.IO.Directory.CreateDirectory(Dest)
        End If
        For Each ff In Source.GetFiles
            Dim aff = GetAfterEffect_Filename(ff)
            If System.IO.File.Exists(Dest & "\" & aff) Then
                Dim aa = New System.IO.FileInfo(Dest & "\" & aff)
                If ff.Length > aa.Length Then
                    My.Computer.FileSystem.DeleteFile(aa.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
                    ff.MoveTo(Dest & "\" & aff)
                End If
            Else
                ff.MoveTo(Dest & "\" & aff)
            End If
        Next
        Dim subdirs = Source.GetDirectories()
        For Each ss In subdirs
            If ss.GetFiles().Length <> 0 OrElse ss.GetDirectories().Length <> 0 Then '忽略空文件夹
                CopyMove(ss, Dest & "\" & ss.Name)
            End If
        Next
    End Sub

    Public Sub CopyCopy(Source As System.IO.DirectoryInfo, Dest As String, RecycleBinDir As System.IO.DirectoryInfo)
        If Not System.IO.Directory.Exists(Dest) Then
            System.IO.Directory.CreateDirectory(Dest)
        End If
        For Each ff In Source.GetFiles
            Dim aff = GetAfterEffect_Filename(ff)
            If System.IO.File.Exists(Dest & "\" & aff) Then '存档文件已经存在，需要判断保留哪个
                Dim aa = New System.IO.FileInfo(Dest & "\" & aff)
                If ff.Length > aa.Length Then '算法选择保留体积更大的文件，往往能够获得更完整的数据
                    '存档文件更小，先回收存档文件aa再复制
                    Dim RecycleFileFullPath = RecycleBinDir.FullName & "\" & aa.Directory.Name & "\" & aa.Name '合成回收文件名
                    '确保父目录存在免得报错
                    Dim targetDir = System.IO.Path.GetDirectoryName(RecycleFileFullPath)
                    If Not System.IO.Directory.Exists(targetDir) Then
                        System.IO.Directory.CreateDirectory(targetDir)
                    End If
                    If System.IO.File.Exists(RecycleFileFullPath) Then '回收站已经有同名文件
                        If IsSameContent(aa, New System.IO.FileInfo(RecycleFileFullPath)) Then '比较文件是否相同
                            aa.Delete() '数据相同，删除
                        Else '数据不同，进一步处理
                            Dim i = 0
                            Dim RecParPath = RecycleBinDir.FullName & "\" & aa.Directory.Name & "\"
                            Dim tempFN = RecParPath & GetAfterEffect_Filename(ff, "_" & i.ToString)
                            While System.IO.File.Exists(tempFN) '回收文件存在(版本)
                                If IsSameContent(aa, New System.IO.FileInfo(tempFN)) Then
                                    i = -1
                                    aa.Delete()
                                    Exit While '找到了相同的回收文件，无需废话直接删，退
                                Else '文件不相同
                                    i += 1
                                    tempFN = RecParPath & GetAfterEffect_Filename(ff, "_" & i.ToString) '更新删除文件名
                                End If
                            End While
                            If i <> -1 Then aa.MoveTo(RecParPath & GetAfterEffect_Filename(ff, "_" & i.ToString))
                        End If
                    Else
                        aa.MoveTo(RecycleFileFullPath)
                    End If
                    ff.CopyTo(Dest & "\" & aff)
                End If '如果存档文件更大则无需复制
            Else '存档文件不存在，直接复制即可
                ff.CopyTo(Dest & "\" & aff)
            End If
        Next
        Dim subdirs = Source.GetDirectories()
        For Each ss In subdirs
            If ss.GetFiles().Length <> 0 OrElse ss.GetDirectories().Length <> 0 Then '忽略空文件夹
                CopyCopy(ss, Dest & "\" & ss.Name, RecycleBinDir)
            End If
        Next
    End Sub


    ''' <summary>
    ''' 查找 adb.exe，范围包括：程序目录、当前工作目录、PATH 环境变量、常见安装目录
    ''' </summary>
    ''' <returns>找到则返回完整路径，否则返回 Nothing</returns>
    Public Function FindAdbPath() As String
        ' ── 1. 优先检查的候选目录（能直接访问到的）────────────────
        Dim candidateDirs As New List(Of String)

        ' 程序自身所在目录
        Dim exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        If Not String.IsNullOrEmpty(exeDir) Then candidateDirs.Add(exeDir)

        ' 当前工作目录
        Dim workDir = Directory.GetCurrentDirectory()
        If Not String.IsNullOrEmpty(workDir) Then candidateDirs.Add(workDir)

        ' 程序目录下的子目录（常见放法：程序目录\adb\ 或 \platform-tools\）
        If Not String.IsNullOrEmpty(exeDir) Then
            candidateDirs.Add(Path.Combine(exeDir, "adb"))
            candidateDirs.Add(Path.Combine(exeDir, "platform-tools"))
            candidateDirs.Add(Path.Combine(exeDir, "tools"))
        End If

        ' 常见安装目录（无需 PATH 也能命中）
        candidateDirs.Add("C:\platform-tools")
        candidateDirs.Add(Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Android", "Sdk", "platform-tools"))
        candidateDirs.Add(Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
        "Android", "Android Studio", "platform-tools"))

        ' ── 2. 先在这些候选目录里找 ────────────────────────────
        For Each Dir0 In candidateDirs
            Dim found = TryFindAdbIn(Dir0)
            If found IsNot Nothing Then Return found
        Next

        ' ── 3. 再遍历系统/用户 PATH ────────────────────────────
        Dim targets = {EnvironmentVariableTarget.Machine, EnvironmentVariableTarget.User}
        For Each target In targets
            Dim pathValue As String = Environment.GetEnvironmentVariable("Path", target)
            If String.IsNullOrEmpty(pathValue) Then Continue For

            Dim dirs = pathValue.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)
            For Each Dir0 In dirs
                Dim found = TryFindAdbIn(Dir0.Trim())
                If found IsNot Nothing Then Return found
            Next
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' 在指定目录下尝试找 adb.exe，找到返回完整路径，否则 Nothing
    ''' </summary>
    Private Function TryFindAdbIn(dir As String) As String
        If String.IsNullOrEmpty(dir) Then Return Nothing
        Try
            Dim fullPath = Path.Combine(dir, "adb.exe")
            If File.Exists(fullPath) Then Return fullPath
        Catch
            ' 路径非法或无权访问，忽略
        End Try
        Return Nothing
    End Function

    Public Function GetUserIdStrings(device As DeviceData) As List(Of String)
        Dim userIds As New List(Of String)
        Try
            Dim adbClient As New AdbClient()
            ' 1. 创建接收器
            Dim receiver As New ConsoleOutputReceiver()
            ' 2. 传入接收器执行命令
            adbClient.ExecuteRemoteCommand("pm list users", device, receiver)
            ' 3. 从接收器取回完整输出
            Dim output As String = receiver.ToString()
            ' 4. 正则提取用户 ID
            Dim matches = Regex.Matches(output, "UserInfo\{(\d+):")
            For Each m As Match In matches
                If m.Groups.Count > 1 Then
                    userIds.Add(m.Groups(1).Value)
                End If
            Next
        Catch ex As Exception
            ADB_Logging(ex.Message & "[错误函数: GetUserIdStrings]")
        End Try
        Return userIds
    End Function

    Public Function GetSubDirectories(device As DeviceData, parentPath As String) As List(Of String)
        Dim subDirs As New List(Of String)
        Dim adbClient As New AdbClient()
        Dim receiver As New ConsoleOutputReceiver()
        Try
            ' -1 强制一行一个，-F 目录加斜杠
            adbClient.ExecuteRemoteCommand($"ls -1F ""{parentPath}""", device, receiver)
            Dim output As String = receiver.ToString()

            ' 统一换行符
            output = output.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)
            Dim lines = output.Split(New Char() {vbLf}, StringSplitOptions.RemoveEmptyEntries)

            For Each line As String In lines
                Dim trimmed As String = line.Trim()
                If trimmed.EndsWith("/") AndAlso trimmed <> "./" AndAlso trimmed <> "../" Then
                    subDirs.Add(trimmed.TrimEnd("/"c))
                End If
            Next
        Catch
        End Try
        Return subDirs
    End Function

    Public Function IsRemotePathAccessible(device As DeviceData, remotePath As String) As Boolean
        Dim adbClient As New AdbClient()
        Dim hasOutput As Boolean = False
        Try
            ' 谓词在每个输出行到达时被调用，只要有输出就标记为 True
            adbClient.ExecuteRemoteCommand(
            $"ls ""{remotePath}""",
            device,
            Function(line As String) As Boolean
                hasOutput = True
                Return False ' 返回 False 表示不停止接收
            End Function
        )
            ' 命令执行成功，且至少有一行输出，说明路径存在且可读
            Return hasOutput
        Catch ex As Exception
            ' 路径不存在、权限拒绝、或其他错误都会走到这里
            Return False
        End Try
    End Function

    Public Function PullDict(RemoteDir As String, DestDir As String) As Boolean
        Dim psi As New ProcessStartInfo()
        Dim adbpath = FindAdbPath()
        If adbpath Is Nothing Then Return False
        psi.FileName = adbpath
        psi.Arguments = $"pull ""{RemoteDir}"" ""{DestDir}"""
        psi.UseShellExecute = False
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True
        psi.CreateNoWindow = True

        Dim proc As Process = Process.Start(psi)
        Dim output As String = proc.StandardOutput.ReadToEnd()
        proc.WaitForExit()

        If proc.ExitCode = 0 Then
            ' 拉取成功
            Return True
        Else
            ' 失败，output 中包含错误信息
            Return False
        End If
    End Function

    Public Function DeleteRemoteDirectory(device As DeviceData, remotePath As String) As Boolean
        Dim adbClient As New AdbClient()
        Dim receiver As New ConsoleOutputReceiver()

        ' 使用 -rf 递归强制删除文件夹及其所有内容
        ' -r 递归删除，-f 强制删除不提示
        adbClient.ExecuteRemoteCommand($"rm -rf ""{remotePath}""", device, receiver)

        ' 检查输出（如果有错误会显示在 receiver 中）
        Dim output As String = receiver.ToString()
        If Not String.IsNullOrWhiteSpace(output) Then
            ' 可能权限不足或其他错误
            Console.WriteLine($"删除时输出: {output}")
            Return False
        End If
        Return True
    End Function

    Private Sub ADB_Logging(Text As String)
        ADB_Log.Text = Now.ToString & " " & Text & vbCrLf & ADB_Log.Text
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Not System.IO.Directory.Exists(TextBox2.Text) Then
            Try
                System.IO.Directory.CreateDirectory(TextBox2.Text)
            Catch
                ADB_Logging("无法访问存档目录，且无法创建该目录")
                Exit Sub
            End Try
        End If
        Dim adbpath = FindAdbPath()
        If adbpath Is Nothing Then ADB_Logging("没找到adb,无法使用")
        'Try
        Dim adbServer As New AdbServer()
        adbServer.StartServer(adbpath, restartServerIfNewer:=True)

        Dim adbClient As New AdbClient()
        Dim devices = adbClient.GetDevices()
        If devices.Count = 0 Then ADB_Logging("没有已连接的设备") : Return
        ADB_Logging("开始尝试拉取数据")
        Dim tempDir As String = Path.Combine(Path.GetTempPath(), "Wechat_status_puller_temp") '创建临时文件夹
        Directory.CreateDirectory(tempDir)
        For Each device In devices '遍历设备进行批量操作
            For Each UserID In GetUserIdStrings(device) '遍历设备的每个用户
                Dim NowPath As String = "/storage/emulated/" & UserID & "/"
                If IsRemotePathAccessible(device, NowPath) And GetSubDirectories(device, NowPath).Count > 0 Then '有权访问
                    NowPath &= "Android/data/com.tencent.mm/MicroMsg/"
                    If IsRemotePathAccessible(device, NowPath) Then
                        Dim SubDicts = GetSubDirectories(device, NowPath)
                        For Each SubDict In SubDicts '遍历微信数据文件夹子文件夹
                            If SubDict.Length = 32 Then '确认长度为32个十六进制，是用户文件夹
                                NowPath &= SubDict & "/textstatus" '微信状态媒体存储位置
                                If IsRemotePathAccessible(device, NowPath) Then '可以访问
                                    If PullDict(NowPath, tempDir) Then '拉取成功
                                        'Try
                                        Dim RecDir = System.IO.Directory.CreateDirectory(tempDir & "\RecycleBinTemp")
                                        CopyCopy(New System.IO.DirectoryInfo(tempDir & "\textstatus"), TextBox2.Text, RecDir)
                                        ADB_Logging("拉取[ " & NowPath & " ]: 成功")
                                        If DelRemoteFile.Checked Then '需要删除手机数据
                                            If DeleteRemoteDirectory(device, NowPath) Then
                                                ADB_Logging("删除[ " & NowPath & " ]: 成功")
                                            Else
                                                ADB_Logging("删除[ " & NowPath & " ]: 失败")
                                            End If
                                        End If
                                        'Catch ex As Exception
                                        'MsgBox(ex.Message)
                                        'Exit Sub
                                        'End Try
                                    End If
                                    '临时textstatus文件夹拉去回收站
                                    If System.IO.Directory.Exists(tempDir & "\textstatus") Then
                                        My.Computer.FileSystem.DeleteDirectory(tempDir & "\textstatus", UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
                                    End If
                                End If
                            End If
                        Next
                    Else '没有微信数据文件夹，跳过
                        Continue For
                    End If
                Else '无权限访问该用户
                    Continue For
                End If
            Next
        Next
        ADB_Logging("清理临时文件")
        My.Computer.FileSystem.DeleteDirectory(tempDir, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
        ADB_Logging("数据拉取完成")


        'Catch ex As Exception
        '   MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Process.Start("https://developer.android.google.cn/tools/releases/platform-tools?hl=zh-cn")
        Catch ex As Exception
            MessageBox.Show($"无法打开链接：{ex.Message}")
        End Try
    End Sub
End Class
