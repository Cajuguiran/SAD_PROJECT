Imports System.Data.OleDb
Module Module1
    Public connStr As String = ("Provider=Microsoft.JET.OLEDB.4.0;Data Source =" & Application.StartupPath & "\Supplier_InfoDB.mdb")
    Public conn As New OleDbConnection(connStr)

    Function connect()
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Return True

    End Function
End Module
