Imports System.Net
Imports System.Text
Imports System.Threading

Public Class Form1

    Dim TheWord As String
    Dim web As WebClient
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not My.Computer.Clipboard.GetText = Nothing Then
            TheWord = My.Computer.Clipboard.GetText
            TextBox2.Text = TheWord
        Else
            TextBox2.Text = "Copy any word"
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Clipboard.Clear()
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Location = New Point(Screen.PrimaryScreen.WorkingArea.Width - Me.Width, Screen.PrimaryScreen.WorkingArea.Height - Me.Height)
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox1.Text = TranslatorAPI(TheWord)
    End Sub

    Private Sub SW()
        Try
            Me.WindowState = FormWindowState.Normal
            Thread.Sleep(5000)
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Function TranslatorAPI(ByVal word As String) As String
        Try
            web = New WebClient

            web.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36")
            web.Headers.Add("Content-type: application/x-www-form-urlencoded")
            web.Headers.Add("Accept: */*")
            web.Headers.Add("Accept-Language: ar,en-US;q=0.9,en;q=0.8")
            web.Encoding = Encoding.UTF8
            web.Encoding = UTF8Encoding.UTF8

            Dim RfText As String = web.UploadString("http://www.bing.com/ttranslate?&category=&IG=DABE083CD44343EDAB328CF3C42CD746&IID=translator.5034.2", "&text=" & word & "&from=en&to=ar")

            Dim rf = Split(RfText, "translationResponse"":""") : rf = Split(rf(1), """}")

            Dim tr As New Thread(AddressOf SW) : tr.Start()


            Return rf(0).Replace("\r\n", vbCrLf)
        Catch ex As Exception
            Return ("Copy any word")
        End Try
    End Function




End Class
