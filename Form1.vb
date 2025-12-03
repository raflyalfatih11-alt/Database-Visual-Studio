Imports Microsoft.Data.SqlClient

Imports System.Data
Public Class Form1
    Private MyDatAdp As SqlDataAdapter
    Private MyCmdBld As SqlCommandBuilder
    Private MyDataTbl As New DataTable
    Private MyCn As New SqlConnection()
    Private MyRowPosition As Integer = 0
    Private strJK As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' JANGAN panggil FormClosed() manual
        'Me.FormClosed()

        MyCn.ConnectionString =
            "Data Source=(localdb)\MSSQLLocalDB;" &
             "AttachDbFilename=C:\Users\User\source\repos\database visual\Database1.mdf;" &
             "Integrated Security=True;"


        MyCn.Open()

        MyDatAdp = New SqlDataAdapter("SELECT * FROM dbo.mahasiswa", MyCn)
        MyCmdBld = New SqlCommandBuilder(MyDatAdp)
        MyDatAdp.Fill(MyDataTbl)

        If MyDataTbl.Rows.Count > 0 Then
            Dim MyDataRow As DataRow = MyDataTbl.Rows(0)

            TextBox1.Text = MyDataRow("NPM").ToString()
            TextBox2.Text = MyDataRow("Nama").ToString()
            ComboBox1.Text = MyDataRow("JK").ToString()
        End If

        showRecords()

    End Sub

    Private Sub showRecords()
        If MyDataTbl.Rows.Count = 0 Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            ComboBox1.Text = ""
            Exit Sub
        End If

        TextBox1.Text = MyDataTbl.Rows(MyRowPosition)("NPM").ToString()
        TextBox2.Text = MyDataTbl.Rows(MyRowPosition)("Nama").ToString()
        ComboBox1.Text = MyDataTbl.Rows(MyRowPosition)("JK").ToString()
    End Sub

    ' FIRST RECORD
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MyRowPosition = 0
        showRecords()
    End Sub

    ' PREVIOUS RECORD
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MyRowPosition > 0 Then
            MyRowPosition -= 1
            showRecords()
        End If
    End Sub

    ' NEXT RECORD
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MyRowPosition < MyDataTbl.Rows.Count - 1 Then
            MyRowPosition += 1
            showRecords()
        End If
    End Sub

    ' LAST RECORD
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MyDataTbl.Rows.Count > 0 Then
            MyRowPosition = MyDataTbl.Rows.Count - 1
            showRecords()
        End If
    End Sub

    ' ADD NEW
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim MyNewRow As DataRow = MyDataTbl.NewRow()
        MyDataTbl.Rows.Add(MyNewRow)
        MyRowPosition = MyDataTbl.Rows.Count - 1
        showRecords()
    End Sub

    ' DELETE
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MyDataTbl.Rows.Count > 0 Then
            MyDataTbl.Rows(MyRowPosition).Delete()
            MyDatAdp.Update(MyDataTbl)
            MyRowPosition = 0
            showRecords()
        End If
    End Sub

    ' UPDATE
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If MyDataTbl.Rows.Count > 0 Then
            MyDataTbl.Rows(MyRowPosition)("NPM") = TextBox1.Text
            MyDataTbl.Rows(MyRowPosition)("Nama") = TextBox2.Text
            MyDataTbl.Rows(MyRowPosition)("JK") = ComboBox1.Text
            MyDatAdp.Update(MyDataTbl)
        End If

        If TextBox1.Text.Trim() = "" Then
            MessageBox.Show("NPM wajib diisi!", "Validasi Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub ' Keluar dari sub sebelum mencoba Update
        End If

        If MyDataTbl.Rows.Count > 0 Then
            MyDataTbl.Rows(MyRowPosition)("NPM") = TextBox1.Text
            ' ... kode update lainnya
            MyDatAdp.Update(MyDataTbl)
        End If

    End Sub

End Class
