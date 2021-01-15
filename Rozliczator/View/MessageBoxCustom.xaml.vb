Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Public Class MessageBoxCustom
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        TextBox.Text = "powinien tu być tekst"
    End Sub
    Sub New(_message As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TextBox.Text = _message

    End Sub


    Private Sub okButton_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub MessageBoxDeactivated(sender As Object, e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception

        End Try

    End Sub
End Class
