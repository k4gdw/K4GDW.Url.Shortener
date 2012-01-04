<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.btnBitly = New System.Windows.Forms.Button
        Me.btnTinyURL = New System.Windows.Forms.Button
        Me.btnIsGd = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "URL"
        '
        'txtURL
        '
        Me.txtURL.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtURL.Location = New System.Drawing.Point(16, 30)
        Me.txtURL.Multiline = True
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(240, 75)
        Me.txtURL.TabIndex = 1
        '
        'btnBitly
        '
        Me.btnBitly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBitly.Enabled = False
        Me.btnBitly.Location = New System.Drawing.Point(16, 112)
        Me.btnBitly.Name = "btnBitly"
        Me.btnBitly.Size = New System.Drawing.Size(75, 23)
        Me.btnBitly.TabIndex = 2
        Me.btnBitly.Text = "Bit.ly"
        Me.btnBitly.UseVisualStyleBackColor = True
        '
        'btnTinyURL
        '
        Me.btnTinyURL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTinyURL.Enabled = False
        Me.btnTinyURL.Location = New System.Drawing.Point(181, 112)
        Me.btnTinyURL.Name = "btnTinyURL"
        Me.btnTinyURL.Size = New System.Drawing.Size(75, 23)
        Me.btnTinyURL.TabIndex = 3
        Me.btnTinyURL.Text = "TinyURL"
        Me.btnTinyURL.UseVisualStyleBackColor = True
        '
        'btnIsGd
        '
        Me.btnIsGd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnIsGd.Enabled = False
        Me.btnIsGd.Location = New System.Drawing.Point(99, 112)
        Me.btnIsGd.Name = "btnIsGd"
        Me.btnIsGd.Size = New System.Drawing.Size(75, 23)
        Me.btnIsGd.TabIndex = 4
        Me.btnIsGd.Text = "Is.gd"
        Me.btnIsGd.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(268, 148)
        Me.Controls.Add(Me.btnIsGd)
        Me.Controls.Add(Me.btnTinyURL)
        Me.Controls.Add(Me.btnBitly)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(500, 371)
        Me.MinimumSize = New System.Drawing.Size(276, 175)
        Me.Name = "Main"
        Me.Text = "K4GDW URL Shortener"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents txtURL As System.Windows.Forms.TextBox
	Friend WithEvents btnBitly As System.Windows.Forms.Button
	Friend WithEvents btnTinyURL As System.Windows.Forms.Button
	Friend WithEvents btnIsGd As System.Windows.Forms.Button

End Class
