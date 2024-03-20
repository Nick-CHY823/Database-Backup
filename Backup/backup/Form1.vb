Imports Newtonsoft.Json
Imports System.IO
Imports System.Timers
Imports System.Net.Mail
Imports System.Text


Public Class Form1

    Private backupTimer As New Timer()
    Private timeUpdateTimer As New Timer()
    Private previousBackupInfo As String = ""


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Set the interval to 24 hours (24 * 60 * 60 * 1000 milliseconds)
        backupTimer.Interval = 24 * 60 * 60 * 1000
        'backupTimer.Interval = 60 * 60 * 1000
        'Add the event handler for the Tick event
        AddHandler backupTimer.Elapsed, AddressOf BackupTimer_Tick
        'Start the timer
        backupTimer.Start()

        timeUpdateTimer.Interval = 1000 ' 1sec
        AddHandler timeUpdateTimer.Elapsed, AddressOf TimeUpdateTimer_Tick
        timeUpdateTimer.Start()

    End Sub


    Private Sub TimeUpdateTimer_Tick(sender As Object, e As ElapsedEventArgs)
        'display time and refresh 1 sec
        Me.Invoke(Sub()
                      lb_Time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                  End Sub)
    End Sub


    Private Sub BackupTimer_Tick(sender As Object, e As ElapsedEventArgs)
        Try
            'Read JSON file content
            Dim jsonFilePath As String = Path.Combine(Application.StartupPath, "bk_db.json")
            Dim json As String = File.ReadAllText(jsonFilePath)


            'Convert JSON data to ServerInfo list
            Dim serverInfoList As List(Of ServerInfo) = JsonConvert.DeserializeObject(Of List(Of ServerInfo))(json)

            Dim backupInfo As New StringBuilder()
            Dim emailstr As New StringBuilder()
            For Each serverInfo As ServerInfo In serverInfoList
                For Each databaseInfo As DatabaseInfo In serverInfo.Databases
                    Try
                        Dim processStartInfo As New ProcessStartInfo()
                        processStartInfo.FileName = Path.Combine(Application.StartupPath, "mysqldump")
                        'processStartInfo.FileName = "C:\Program Files\MySQL\MySQL Server 5.7\bin\mysqldump"

                        'Back up the entire database if no table is specified
                        'If a table is specified, add it to the backup command
                        Dim backupCommand As String = "--user=" & serverInfo.User & " --password=" & serverInfo.Password & " --host=" & serverInfo.Server & " " & databaseInfo.Name


                        If databaseInfo.Tables IsNot Nothing AndAlso databaseInfo.Tables.Count > 0 Then
                            backupCommand = backupCommand & " --tables " & String.Join(" ", databaseInfo.Tables)
                        End If

                        processStartInfo.Arguments = backupCommand

                        'Set ProcessStartInfo object to execute an external program (mysqldump) without interference from an external shell
                        processStartInfo.UseShellExecute = False
                        processStartInfo.RedirectStandardOutput = True

                        Dim process As New Process()
                        process.StartInfo = processStartInfo
                        process.Start()

                        'DATE FILENAME PATH
                        Dim currentDate As String = DateTime.Now.ToString("yyyyMMdd")
                        Dim backupFolder As String = Path.Combine(databaseInfo.BkPath, "Dump-" & currentDate)
                        Dim backupFileName As String = databaseInfo.Name & "-" & currentDate & ".sql"
                        'If databaseInfo.Tables IsNot Nothing AndAlso databaseInfo.Tables.Count > 0 Then
                        '    backupFileName = databaseInfo.Name & "-" & String.Join("-", databaseInfo.Tables) & "-" & currentDate & ".sql"
                        'Else
                        '    backupFileName = databaseInfo.Name & "-" & currentDate & ".sql"
                        'End If

                        Dim backupPath As String = Path.Combine(backupFolder, backupFileName)

                        'If the directory not exist, create it
                        If Not Directory.Exists(backupFolder) Then
                            Directory.CreateDirectory(backupFolder)
                        End If

                        'buffer
                        Dim bufferSize As Integer = 16384
                        Using outputFile As New StreamWriter(backupPath)
                            Using inputStream As New StreamReader(process.StandardOutput.BaseStream)
                                Dim buffer(bufferSize - 1) As Char
                                Dim bytesRead As Integer
                                Do
                                    bytesRead = inputStream.Read(buffer, 0, bufferSize)
                                    If bytesRead > 0 Then
                                        outputFile.Write(buffer, 0, bytesRead)
                                    End If
                                Loop While bytesRead > 0
                            End Using
                        End Using

                        process.WaitForExit()

                        'successful display
                        Dim successInfo As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") & databaseInfo.Name & " backup successful"

                        Me.Invoke(Sub()
                                      previousBackupInfo &= successInfo & Environment.NewLine ' Append to previous info
                                      tb_BackupInfo.AppendText(successInfo & Environment.NewLine) ' Display in TextBox
                                  End Sub)

                        'Add success message to backupInfo
                        backupInfo.AppendLine(successInfo)
                        emailstr.Append(successInfo & "\n")
                    Catch ex As Exception

                        'failed display
                        Dim failureInfo As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") & databaseInfo.Name & " backup failed-" & ex.Message

                        Me.Invoke(Sub()
                                      previousBackupInfo &= failureInfo & Environment.NewLine ' Append to previous info
                                      tb_BackupInfo.AppendText(failureInfo & Environment.NewLine) ' Display in TextBox
                                  End Sub)

                        Dim errorFilePath As String = Path.Combine(Application.StartupPath, "error_log.txt")

                        If Not File.Exists(errorFilePath) Then
                            Using fs As FileStream = File.Create(errorFilePath)
                            End Using
                        End If

                        Using writer As New StreamWriter(errorFilePath, True)
                            writer.WriteLine(failureInfo)
                        End Using
                        'Add fail message to backupInfo
                        backupInfo.AppendLine(failureInfo)
                        emailstr.Append(failureInfo & "\n")
                    End Try
                Next
            Next

            'Send email with all the backup information
            SendEmail("Backup Report", emailstr.ToString())
        Catch ex As Exception
            MessageBox.Show("Backup failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub SendEmail(subject As String, body As String)
        Try
            Dim pbstrWOdbconn As String = "Driver={MySQL ODBC 5.1 Driver};Server=meit.amtopp.com;uid=amtoppme;pwd=Network01;database=amtopp"
            Dim pbWOdbconn As New Odbc.OdbcConnection(pbstrWOdbconn)
            Dim SQLCommand As New Odbc.OdbcCommand '(strselinitiation_approval, pbWOdbconn)
            SQLCommand.Connection = pbWOdbconn
            pbWOdbconn.Open()
            SQLCommand.CommandText = "INSERT INTO email_header (indatetime, subject, body, tocopyid, attach, flag) VALUES (now(), '" & subject & "','" & body & "', 'F', 'F', 'I')"
            SQLCommand.ExecuteNonQuery()
            SQLCommand.CommandText = "SELECT LAST_INSERT_ID()"
            Dim insertedId As Integer = Convert.ToInt32(SQLCommand.ExecuteScalar())
            SQLCommand.CommandText = "INSERT INTO email_touserid (email_header, touserid, type) VALUES (" & insertedId & ", 'nchen@amjk.inteplast.com', 'M')"
            SQLCommand.ExecuteNonQuery()
            SQLCommand.CommandText = "INSERT INTO email_touserid (email_header, touserid, type) VALUES (" & insertedId & ", 'samyu@amjk.inteplast.com', 'M')"
            SQLCommand.ExecuteNonQuery()
            pbWOdbconn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), System.Reflection.MethodInfo.GetCurrentMethod.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub bt_start_bk_Click(sender As System.Object, e As System.EventArgs) Handles bt_start_bk.Click

        backupTimer.Stop()

        BackupTimer_Tick(Nothing, Nothing)

        backupTimer.Start()
    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub
End Class


Public Class ServerInfo
    Public Property Server As String
    Public Property User As String
    Public Property Password As String
    Public Property Databases As List(Of DatabaseInfo)
End Class


Public Class DatabaseInfo
    Public Property Name As String
    Public Property Tables As List(Of String)
    Public Property BkPath As String
End Class
