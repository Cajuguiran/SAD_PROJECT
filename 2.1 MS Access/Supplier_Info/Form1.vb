Imports System.Data.OleDb
Public Class Form1
    Dim da As New OleDbDataAdapter
    Dim dset As New DataSet
    Dim comm As OleDbCommand
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        da = New OleDbDataAdapter("Select * from tbl_SuppInfo", conn)
        dset = New DataSet
        da.Fill(dset, "tbl_SuppInfo")
        DataGridView1.DataSource = dset.Tables("tbl_SuppInfo").DefaultView
    End Sub
    Function populate()
        da = New OleDbDataAdapter("Select * from tbl_SuppInfo", conn)
        dset = New DataSet
        da.Fill(dset, "tbl_SuppInfo")
        DataGridView1.DataSource = dset.Tables("tbl_SuppInfo").DefaultView
        Return True
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim comm As OleDbCommand
        connect()

        comm = New OleDbCommand
        comm.Connection = conn
        comm.CommandText = "Insert into tbl_SuppInfo values('" & txtID.Text & "', '" & txtFirst.Text & "', '" & txtLast.Text & "', '" & txtMid.Text & "', '" & txtAddress.Text & "', '" & txtContact.Text & "', '" & txtEmail.Text & "')"
        comm.ExecuteNonQuery()

        MessageBox.Show("Successfully Added!")
        populate()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtID.Clear()
        txtFirst.Clear()
        txtLast.Clear()
        txtMid.Clear()
        txtAddress.Clear()
        txtContact.Clear()
        txtEmail.Clear()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        txtID.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
        txtFirst.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
        txtLast.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
        txtMid.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
        txtAddress.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
        txtContact.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString
        txtEmail.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value.ToString
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        connect()
        comm = New OleDbCommand
        comm.Connection = conn
        comm.CommandText = "Delete from tbl_SuppInfo where ID='" & txtID.Text & "'"
        comm.ExecuteNonQuery()

        MessageBox.Show("Successfully Deleted!")
        populate()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        connect()
        comm = New OleDbCommand
        comm.Connection = conn
        comm.CommandText = "Update tbl_SuppInfo set FirstName='" & txtFirst.Text & "', LastName='" & txtLast.Text & "', Middle='" & txtMid.Text & "', Address='" & txtAddress.Text & "', Contact='" & txtContact.Text & "', Email='" & txtEmail.Text & "' where ID='" & txtID.Text & "'"
        comm.ExecuteNonQuery()
        populate()

    End Sub
End Class
