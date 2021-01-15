Imports MySql.Data.MySqlClient

Public Class Credentials
    Public Shared FtpUser As String
    Public Shared DbUser As String
    Public Shared txtPassword As String

    Public Shared Function CheckCredentials(login As String, password As String) As Boolean
        Dim llogin As String = login.ToLower()

        Select Case llogin
            Case "postrowski", "ostrowski", "po"
                FtpUser = "postrowski_rozliczator@cseg.pl"
                DbUser = "cseg_postrowski"
            Case "ppawlowski", "pawlowski", "pp", "pawłowski", "ppawłowski"
                FtpUser = "ppawlowski_rozliczator@cseg.pl"
                DbUser = "cseg_pawlowski"
            Case "mbabka", "babka", "mm", "mb", "bąbka", "mariusz"
                FtpUser = "mbabka_rozliczator@cseg.pl"
                DbUser = "cseg_mbabka"
            Case Else
                Return False
        End Select

        txtPassword = password

        If CheckDataBase() Then
            Return True
        Else
            Return False

        End If

    End Function

    Public Shared Function CheckDataBase() As Boolean
        Dim cs As String = "server=s64.hekko.net.pl;database=cseg_dane; user = " & Credentials.DbUser &
        ";port=3306;password= " & Credentials.txtPassword
        Using con As New MySqlConnection(cs)
            Try
                con.Open()
                If con.State = System.Data.ConnectionState.Open Then

                    con.Close()
                    Return True
                Else
                    Return False

                End If
            Catch ex As Exception

                Return False
            End Try

        End Using
    End Function
End Class
