Public Class LoginScreenView
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        txtUsername.Text = My.Settings.MemoUsername
        txtPassword.Password = My.Settings.MemoPassword
        checkboxPamietaj.IsChecked = My.Settings.MemoCheckboxPamietaj
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Cancel(sender As Object, e As RoutedEventArgs)
        DialogResult = False
    End Sub

    Private Sub Login(sender As Object, e As RoutedEventArgs)

        'DialogResult = True
        If Credentials.CheckCredentials(txtUsername.Text, txtPassword.Password) Then
            If checkboxPamietaj.IsChecked Then
                My.Settings.MemoUsername = txtUsername.Text
                My.Settings.MemoPassword = txtPassword.Password
                My.Settings.MemoCheckboxPamietaj = checkboxPamietaj.IsChecked
                My.Settings.Save()
            End If
            DialogResult = True
        Else
            InfoTextBlock.Text = "Niepoprawne dane logowania lub brak sieci"
        End If

    End Sub

    Private Sub checkboxPamietaj_Checked(sender As Object, e As RoutedEventArgs) Handles checkboxPamietaj.Unchecked
        My.Settings.MemoUsername = ""
        My.Settings.MemoPassword = ""
        My.Settings.MemoCheckboxPamietaj = False
        My.Settings.Save()
    End Sub
End Class
