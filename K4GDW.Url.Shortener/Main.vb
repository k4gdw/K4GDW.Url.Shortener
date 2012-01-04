Imports System.Text.RegularExpressions

Public Class Main

	Private LongURL As String

	''' <summary>
	''' Determines whether the specified URL is valid.
	''' </summary>
	''' <param name="url">The URL.</param>
	''' <returns>
	''' <c>true</c> if the specified URL is valid; otherwise, <c>false</c>.
	''' </returns>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/19/2010 at 1:45 PM
	''' </para>
	''' </remarks>
	Private Function IsValidURL(ByVal url As String) As Boolean
		Return Regex.IsMatch(url, "^(https?://)?" & _
	"([\d\w-.]+?\.(a[cdefgilmnoqrstuwz]|b[abdefghijmnorstvwyz]|c[acdfghiklmnoruvxyz]|d[ejkmnoz]|e[ceghrst]|f[ijkmnor]|g[abdefghilmnpqrstuwy]|h[kmnrtu]|i[delmnoqrst]|j[emop]|k[eghimnprwyz]|l[abcikrstuvy]|m[acdghklmnopqrstuvwxyz]|n[acefgilopruz]|om|p[aefghklmnrstwy]|qa|r[eouw]|s[abcdeghijklmnortuvyz]|t[cdfghjkmnoprtvwz]|u[augkmsyz]|v[aceginu]|w[fs]|y[etu]|z[amw]|aero|arpa|biz|com|coop|edu|info|int|gov|mil|museum|name|net|org|pro)(\b|\W(?<!&|=)(?!\.\s|\.{3}).*?))(\s|$)")
	End Function

	Private Sub btnTinyURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTinyURL.Click
		CacheLongURL()
		If txtURL.Text.Contains("http://is.gd") Or txtURL.Text.Contains("http://bit.ly") Or txtURL.Text.Contains("http://is.gd") Then
			txtURL.Text = TinyUrl.GetTinyUrl(LongURL)
		Else
			txtURL.Text = TinyUrl.GetTinyUrl(txtURL.Text)
		End If
		Clipboard.SetText(txtURL.Text)
	End Sub

	Private Sub btnBitly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBitly.Click
		CacheLongURL()
		If txtURL.Text.Contains("http://tinyurl.com") Or txtURL.Text.Contains("http://is.gd") Or txtURL.Text.Contains("http://is.gd") Then
			txtURL.Text = BitlyApi.ShortenUrl(LongURL).ShortUrl
        Else
            If Not txtURL.Text.ToLower.StartsWith("http") Then
                txtURL.Text = "http://" & txtURL.Text
            End If
            txtURL.Text = BitlyApi.ShortenUrl(txtURL.Text).ShortUrl
		End If
		Clipboard.SetText(txtURL.Text)
	End Sub

	Private Sub txtURL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtURL.TextChanged
		If IsValidURL(txtURL.Text) Then
			EnableButtons()
		Else
			DisableButtons()
		End If
	End Sub

	''' <summary>
	''' Caches the long URL.
	''' </summary>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/18/2010 at 3:18 PM
	''' </para>
	''' </remarks>
	Private Sub CacheLongURL()
		If Not txtURL.Text.Contains("http://bit.ly") _
		 And Not txtURL.Text.Contains("http://tinyurl.com") _
		 And Not txtURL.Text.Contains("http://is.gd") Then
			LongURL = txtURL.Text
		End If
	End Sub

	''' <summary>
	''' Enables the buttons.
	''' </summary>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/18/2010 at 3:18 PM
	''' </para>
	''' </remarks>
	Private Sub EnableButtons()
		btnTinyURL.Enabled = True
		btnBitly.Enabled = True
		btnIsGd.Enabled = True
	End Sub

	''' <summary>
	''' Disables the buttons.
	''' </summary>
	''' <remarks>
	''' <para>
	''' Created By:  Bryan Johns<br />
	''' On:  3/18/2010 at 3:18 PM
	''' </para>
	''' </remarks>
	Private Sub DisableButtons()
		btnTinyURL.Enabled = False
		btnBitly.Enabled = False
		btnIsGd.Enabled = False
	End Sub

	Private Sub btnIsGd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIsGd.Click
		CacheLongURL()
		If txtURL.Text.Contains("http://tinyurl.com") Or txtURL.Text.Contains("http://bit.ly") Then
			txtURL.Text = IsGd.GetIsGd(LongURL)
		Else
			txtURL.Text = IsGd.GetIsGd(txtURL.Text)
		End If
		Clipboard.SetText(txtURL.Text)
	End Sub
End Class
