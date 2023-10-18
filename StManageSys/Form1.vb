Imports System.Data.SqlClient

Public Class Form1

    Dim conn As New SqlConnection("Data Source=TOSEI-012\SQLEXPRESS; Initial Catalog=StudentsDB; Integrated Security=True")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub ExecuteQuery(ByVal query As String)
        Dim cmd As New SqlCommand(query, conn)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim insertquery As String = " INSERT INTO StInfoTbl(FIRSTNAME, LASTNAME, ADDRESS) VALUES(
        '" & TextBoxFName.Text & "' ,
        '" & TextBoxLName.Text & "' ,
        '" & TextBoxAddress.Text & "' 
        ) "
        ExecuteQuery(insertquery)
        MessageBox.Show("Record inserted suceessfully", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ClearEntry()
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim cmd As New SqlCommand("SELECT * FROM StInfoTbl WHERE ID=@ID", conn)
        cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = TextBoxSearch.Text
        Dim DataAdapter As New SqlDataAdapter(cmd)
        Dim ShowDataOnTable As New DataTable
        DataAdapter.Fill(ShowDataOnTable)

        If ShowDataOnTable.Rows.Count > 0 Then
            TextBoxFName.Text = ShowDataOnTable.Rows(0)(1).ToString
            TextBoxLName.Text = ShowDataOnTable.Rows(0)(2).ToString
            TextBoxAddress.Text = ShowDataOnTable.Rows(0)(3).ToString
        Else
            MessageBox.Show("No record found", "No record", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim updatequery As String = "UPDATE StInfoTbl SET 

        FIRSTNAME= '" & TextBoxFName.Text & "', 
        LASTNAME = '" & TextBoxLName.Text & "',
        ADDRESS = '" & TextBoxAddress.Text & "'
        WHERE ID = '" & TextBoxSearch.Text & "'"
        ExecuteQuery(updatequery)
        MessageBox.Show("Record updated successfuly ", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ClearEntry()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim deletequery As String = "DELETE FROM StInfoTbl WHERE ID= '" & TextBoxSearch.Text & "'"
        ExecuteQuery(deletequery)
        MessageBox.Show("Record deleted successfuly ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ClearEntry()
    End Sub

    Public Sub ClearEntry()
        TextBoxFName.Clear()
        TextBoxLName.Clear()
        TextBoxAddress.Clear()
    End Sub


End Class
