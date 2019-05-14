'created by Rui Santos, http://randomnerdtutorials.wordpress.com
'Control a servo motor with Visual Basic 
'2013

Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form1

    Shared _continue As Boolean
    Shared _serialPort As SerialPort

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SerialPort1.Close()
        SerialPort1.PortName = "com6" 'you need to check which com port your arduino is using, and change them if you need
        SerialPort1.BaudRate = 9600
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = System.Text.Encoding.Default
    End Sub
    Private Sub btn90L_Click_1(sender As System.Object, e As System.EventArgs) Handles btn90L.Click
        SerialPort1.Open()
        SerialPort1.Write("0")
        SerialPort1.Close()
    End Sub

    Private Sub btn90R_Click(sender As System.Object, e As System.EventArgs) Handles btn90R.Click
        SerialPort1.Open()
        SerialPort1.Write("1")
        SerialPort1.Close()
    End Sub

    Private Sub btn180L_Click(sender As System.Object, e As System.EventArgs) Handles btn180L.Click
        SerialPort1.Open()
        SerialPort1.Write("2")
        SerialPort1.Close()
    End Sub

    Private Sub btn180R_Click(sender As System.Object, e As System.EventArgs) Handles btn180R.Click
        SerialPort1.Open()
        SerialPort1.Write("3")
        SerialPort1.Close()
    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub

End Class