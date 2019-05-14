<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.btn90L = New System.Windows.Forms.Button()
        Me.btn90R = New System.Windows.Forms.Button()
        Me.btn180L = New System.Windows.Forms.Button()
        Me.btn180R = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn90L
        '
        Me.btn90L.Location = New System.Drawing.Point(23, 87)
        Me.btn90L.Name = "btn90L"
        Me.btn90L.Size = New System.Drawing.Size(105, 47)
        Me.btn90L.TabIndex = 0
        Me.btn90L.Text = "Rotate 90º Left"
        Me.btn90L.UseVisualStyleBackColor = True
        '
        'btn90R
        '
        Me.btn90R.Location = New System.Drawing.Point(160, 87)
        Me.btn90R.Name = "btn90R"
        Me.btn90R.Size = New System.Drawing.Size(100, 47)
        Me.btn90R.TabIndex = 1
        Me.btn90R.Text = "Rotate 90º Right"
        Me.btn90R.UseVisualStyleBackColor = True
        '
        'btn180L
        '
        Me.btn180L.Location = New System.Drawing.Point(23, 166)
        Me.btn180L.Name = "btn180L"
        Me.btn180L.Size = New System.Drawing.Size(105, 47)
        Me.btn180L.TabIndex = 2
        Me.btn180L.Text = "Rotate 180º Left"
        Me.btn180L.UseVisualStyleBackColor = True
        '
        'btn180R
        '
        Me.btn180R.Location = New System.Drawing.Point(160, 166)
        Me.btn180R.Name = "btn180R"
        Me.btn180R.Size = New System.Drawing.Size(100, 47)
        Me.btn180R.TabIndex = 3
        Me.btn180R.Text = "Rotate 180º Right"
        Me.btn180R.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(218, 39)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Arduino Servo"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(23, 87)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 47)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "btn90L"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(277, 237)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn180R)
        Me.Controls.Add(Me.btn180L)
        Me.Controls.Add(Me.btn90R)
        Me.Controls.Add(Me.btn90L)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents btn90L As System.Windows.Forms.Button
    Friend WithEvents btn90R As System.Windows.Forms.Button
    Friend WithEvents btn180L As System.Windows.Forms.Button
    Friend WithEvents btn180R As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
