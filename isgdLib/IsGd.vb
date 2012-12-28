Imports System.IO
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web

Public Class IsGd

	''' <summary>
	''' Gets the shortened Url from is.gd.
	''' </summary>
	''' <param name="longUrl">The long URL.</param>
	''' <returns>Task{System.String}.</returns>
	''' <exception cref="System.ApplicationException"></exception>
	Public Shared Async Function ShortenUrl(ByVal longUrl As String) As Task(Of String)
		Try
			If Not longUrl.ToLower().StartsWith("http") AndAlso Not longUrl.ToLower().StartsWith("ftp") Then
				longUrl = "http://" + longUrl
			End If
			If longUrl.Length <= 18 Then
				Return longUrl
			End If
			Dim req As WebRequest = WebRequest.Create(String.Format("http://is.gd/create.php?url={0}&format=simple", longUrl))
			Using rsp As WebResponse = Await req.GetResponseAsync()
				Dim txt As String = longUrl
				Using rdr As StreamReader = New StreamReader(rsp.GetResponseStream())
					txt = rdr.ReadToEnd()
				End Using
				Return txt
			End Using
		Catch ex As WebException
			Throw New ApplicationException("There was an error communicating with the Is.Gd service.", ex)
		Catch ex As Exception
			Throw New ApplicationException("There was an error getting the shortened longUrl.", ex)
		End Try
	End Function

End Class
