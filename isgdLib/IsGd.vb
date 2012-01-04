Imports System.IO
Imports System.Net

Public Class IsGd
	''' <summary>
	''' Gets the IsGD URL.
	''' </summary>
	''' <param name="URL">The URL.</param>
	''' <returns></returns>
	''' <remarks>
	''' Created: 6/30/2009 at 12:12 PM
	''' By: bjohns.
	''' </remarks>
	Public Shared Function GetIsGd(ByVal URL As String) As String
		Try
			If Not URL.ToLower().StartsWith("http") AndAlso Not URL.ToLower().StartsWith("ftp") Then
				URL = "http://" + URL
			End If
            If URL.Length <= 18 Then
                Return URL
            End If
			Dim req As WebRequest = WebRequest.Create("http://is.gd/api.php?longurl=" + URL)
			Dim rsp As WebResponse = req.GetResponse()
			Dim txt As String
			Using rdr As StreamReader = New StreamReader(rsp.GetResponseStream())
				txt = rdr.ReadToEnd()
			End Using
			Return txt
		Catch ex As Net.WebException
			Throw New ApplicationException("There was an error communicating with the Is.Gd service.", ex)
		Catch ex As Exception
			Throw New ApplicationException("There was an error getting the shortened URL.", ex)
		End Try
	End Function
End Class
