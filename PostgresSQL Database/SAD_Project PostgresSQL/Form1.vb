Imports System.Data
Imports Npgsql

Public Class Form1
    Dim lv As ListViewItem
    Dim selected As String

    Private Sub ClearTextbox()
        Me.txtID.Text = ""
        Me.DateTimePicker1.Text = ""
        Me.txtItemName.Text = ""
        Me.txtQuantity.Text = ""
        Me.txtPrice.Text = ""

    End Sub

    Private Sub ExecutedNoQuery(query As String)
        openCon()
        cmd = New NpgsqlCommand(query, cn)
        cmd.ExecuteNonQuery()
        cn.Close()

    End Sub
    Private Sub PopListView()
        ListView1.Clear()

        With ListView1
            .View = View.Details
            .GridLines = True
            .Columns.Add("ID", 50)
            .Columns.Add("Item Name", 100)
            .Columns.Add("Date", 100)
            .Columns.Add("Quantity", 50)
            .Columns.Add("Price", 100)

        End With

        openCon()
        sql = "Select * from sis_table"
        cmd = New NpgsqlCommand(sql, cn)
        dr = cmd.ExecuteReader()

        Do While dr.Read() = True
            lv = New ListViewItem(dr("sis_id").ToString)
            lv.SubItems.Add(dr("sis_itemname"))
            lv.SubItems.Add(dr("sis_date"))
            lv.SubItems.Add(dr("sis_quantity"))
            lv.SubItems.Add(dr("sis_price"))
            ListView1.Items.Add(lv)
        Loop
        cn.Close()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopListView()

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If MsgBox("Are you sure to save this record?", vbQuestion + vbYesNo) = vbYes Then
            openCon()
            sql = "INSERT INTO sis_table (sis_id,sis_itemname, sis_date, sis_quantity, sis_price)" _
            & "VALUES ('" & (Me.txtID.Text) & "', '" & (Me.txtItemName.Text) & "', '" & (Me.DateTimePicker1.Text) & "', '" & (Me.txtQuantity.Text) & "', '" & (Me.txtPrice.Text) & "')"
            cmd = New NpgsqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        End If
        PopListView()
        ClearTextbox()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To ListView1.SelectedItems.Count - 1

            selected = ListView1.SelectedItems(i).Text
            openCon()
            sql = "Select * from sis_table where sis_id = '" & selected & "'"
            cmd = New NpgsqlCommand(sql, cn)
            dr = cmd.ExecuteReader

            dr.Read()
            Me.txtID.Text = dr("sis_id")
            Me.txtItemName.Text = dr("sis_itemname")
            Me.DateTimePicker1.Text = dr("sis_date")
            Me.txtQuantity.Text = dr("sis_quantity")
            Me.txtPrice.Text = dr("sis_price")

            cn.Close()
        Next
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If MsgBox("Are you sure to update this record?", vbExclamation + vbYesNo) = vbYes Then
            openCon()
            sql = "UPDATE sis_table SET sis_itemname = '" & (Me.txtItemName.Text) & "', sis_date = '" & (Me.DateTimePicker1.Text) & "', sis_quantity = '" & (Me.txtQuantity.Text) & "', sis_price =  '" & (Me.txtPrice.Text) & "' where sis_id = '" & selected & "'"
            cmd = New NpgsqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
            PopListView()
            ClearTextbox()
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MsgBox("Are you sure to delete this record?", vbExclamation + vbYesNo) = vbYes Then
            Dim query As String = "Delete from sis_table where sis_id = '" & (txtID.Text) & "'"
            ExecutedNoQuery(query)
            PopListView()
            ClearTextbox()
        End If
    End Sub
End Class

