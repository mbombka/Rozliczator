Imports System.Net
Imports System.IO
Public Class FtpModel
    ' #TODO implement login page
    Private UsernameFtp As String = Credentials.FtpUser
    Private PasswordFtp As String = Credentials.txtPassword
    Private FtpCredentials = New NetworkCredential(UsernameFtp, PasswordFtp)

    Public Function DeleteFromFTP(_filePath As String) As Boolean
        Dim _FTPPath = "ftp://cseg.pl/" + _filePath
        Dim wrDelete As FtpWebRequest = DirectCast(WebRequest.Create(_FTPPath), FtpWebRequest) 'direct cast is just fancy equivalent of =
        'Specify Username & Password'
        wrDelete.Credentials = FtpCredentials
        wrDelete.Method = WebRequestMethods.Ftp.DeleteFile

        Try
            Using response As FtpWebResponse = DirectCast(wrDelete.GetResponse(), FtpWebResponse)
                Return True
            End Using

        Catch ex As WebException
            Dim response As FtpWebResponse = DirectCast(ex.Response, FtpWebResponse)
            'folder does not exist
            MessageBox.Show("kierowniku, ten tego, nie można usunąć pliku.")
            Return False
        End Try

    End Function

    Public Sub DownloadFromFTP(_fileSavePath As String, _FtpFilePath As String)
        Dim _FTPPath = "ftp://cseg.pl/" + _FtpFilePath
        Dim _FileName = Path.GetFileName(_FtpFilePath)
        Dim wrDownload As FtpWebRequest = DirectCast(WebRequest.Create(_FTPPath), FtpWebRequest) 'direct cast is just fancy equivalent of =
        'Specify Username & Password'
        wrDownload.Credentials = FtpCredentials
        wrDownload.Method = WebRequestMethods.Ftp.DownloadFile

        Try
            Using response As FtpWebResponse = DirectCast(wrDownload.GetResponse(), FtpWebResponse)

                Dim responseStream As System.IO.Stream = response.GetResponseStream()
                Dim fs As New System.IO.FileStream(_fileSavePath, System.IO.FileMode.Create)
                responseStream.CopyTo(fs)
                responseStream.Close()
            End Using

        Catch ex As WebException
            Dim response As FtpWebResponse = DirectCast(ex.Response, FtpWebResponse)
            'couldnt download
            MessageBox.Show("kierowniku, ten tego, nie można zapisać.")

        End Try

    End Sub

    Public Function UploadToFTP(_filePath As String, _CatalogName As String) As Boolean

        Dim _FTPCatalogPath = "ftp://cseg.pl/" + _CatalogName
        Dim _FileName = Path.GetFileName(_filePath)
        Dim FTPFilePath = _FTPCatalogPath + _FileName

        'first check if directory exist
        If CheckIfDirectoryExist(_FTPCatalogPath) Then
            'Start Upload Process'
            Try
                UploadFileFTP(_filePath, FTPFilePath)
                Return True
            Catch ex As Exception
                Return False
            End Try
            ' if directory doesnt exist then first create one 
        ElseIf CreateFtpDirectory(_FTPCatalogPath) Then
            'Start Upload Process'
            Try
                UploadFileFTP(_filePath, FTPFilePath)
                Return True
            Catch ex As Exception
                Return False
            End Try
        Else
            MessageBox.Show("obsługa FTP nie bangla")
            Return False
        End If
    End Function

    Private Sub UploadFileFTP(_FilePath As String, _FtpFilePath As String)
        'Locate File And Store It In Byte Array'
        Dim btfile() As Byte = File.ReadAllBytes(_FilePath)

        'prepare web request for uploading file
        Dim wrUpload As FtpWebRequest = DirectCast(WebRequest.Create(_FtpFilePath), FtpWebRequest) 'direct cast is just fancy equivalent of =
        'Specify Username & Password'
        wrUpload.Credentials = FtpCredentials

        wrUpload.Method = WebRequestMethods.Ftp.UploadFile
        'Get File'
        Dim strFile As Stream = wrUpload.GetRequestStream()

        'Upload Each Byte'
        strFile.Write(btfile, 0, btfile.Length)

        'Close'
        strFile.Close()

        'Free Memory'
        strFile.Dispose()
    End Sub

    Private Function CreateFtpDirectory(_DiretoryName As String) As Boolean
        'prepare request for creating diretory
        Dim wrCreateDirectory = DirectCast(WebRequest.Create(_DiretoryName), FtpWebRequest)
        wrCreateDirectory.Credentials = FtpCredentials
        wrCreateDirectory.Method = WebRequestMethods.Ftp.MakeDirectory
        Try
            Using response As FtpWebResponse = DirectCast(wrCreateDirectory.GetResponse(), FtpWebResponse)

                Return True
            End Using

        Catch ex As WebException
            Dim response As FtpWebResponse = DirectCast(ex.Response, FtpWebResponse)
            'folder does not exist
            MessageBox.Show("kierowniku, ten tego, katalogu utworzyć nie można..")
            Return False
        End Try
    End Function

    Private Function CheckIfDirectoryExist(_DirectoryName As String) As Boolean
        'prepare request for checking if directoy exist
        Dim wrDirectoryExist = DirectCast(WebRequest.Create(_DirectoryName), FtpWebRequest)
        wrDirectoryExist.Credentials = FtpCredentials
        wrDirectoryExist.Method = WebRequestMethods.Ftp.ListDirectory
        Try
            Using response As FtpWebResponse = DirectCast(wrDirectoryExist.GetResponse(), FtpWebResponse)
                ' Folder exists
                Return True
            End Using

        Catch ex As WebException
            Dim response As FtpWebResponse = DirectCast(ex.Response, FtpWebResponse)
            'folder does not exist
            Return False
        End Try
    End Function

End Class
