Public Class WykresSubKonta
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New KontaCSEGViewModel

    Public Sub New()

        MyBase.New()

        Try
            Me.InitializeComponent()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        ' Insert code required on object creation below this point.

        Try
            VM = Startup.VMLocator.VMKontaCSEG
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub


End Class
