
Imports System.Data.SQLite
Imports System.IO
Imports System.Windows


Class MainWindow
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New ViewModelMain
    Sub New()

        ' This call is required by the designer.
        'Show Login dialog before initialization. To dont initialize datagrid without data
        Dim win = New LoginScreenView
        win.ShowDialog()


        If win.DialogResult.HasValue And win.DialogResult.Value Then
            'initialize database if login OK
            Startup.MainDataBaseModel = New DataBaseModel(Startup.VMLocator)
        Else
            'Exit aplication
            Environment.Exit(0)
        End If

        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMMain
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub MenuItem_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
