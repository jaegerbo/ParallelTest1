Imports System.Threading

Public Class Form1

   Private _Task As Task = Nothing
   Private _previousTime As DateTime = DateTime.Now
   Private _sc As SynchronizationContext = SynchronizationContext.Current
   Private _tokenSource As New CancellationTokenSource
   Private _cancelationtoken As CancellationToken = Nothing

   Private Sub UltraButton1_Click(sender As Object, e As EventArgs) Handles btnStart1.Click
      Try
         _Task = clsRunUiAsync.Run(lblOk, Sub() Test3(), True)
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnStart2_Click(sender As Object, e As EventArgs) Handles btnStart2.Click
      Try
         _cancelationtoken = _tokenSource.Token

         _Task = clsRunUiAsync.RunTest4(lblOk, 4000000, _tokenSource.Token, True)
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnStart3_Click(sender As Object, e As EventArgs) Handles btnStart3.Click
      Try
         _cancelationtoken = _tokenSource.Token

         _Task = clsRunUiAsync.RunTest5(lblOk, 4000000, _tokenSource.Token, True)
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      Try
         If _Task IsNot Nothing AndAlso _Task.IsCompleted = False Then
            _tokenSource.Cancel()
         End If
      Catch ex As Exception
         Stop
      End Try
   End Sub

   'Public Sub updateUi(i As Integer)
   '   Try
   '      Dim timeNow As DateTime = DateTime.Now

   '      If (DateTime.Now - _previousTime).Milliseconds <= 50 Then
   '         Exit Sub
   '      End If

   '      _sc.Post(New SendOrPostCallback(Sub()
   '                                         lblOk.Text = i.ToString
   '                                      End Sub), Nothing)
   '      _previousTime = DateTime.Now
   '   Catch ex As Exception
   '      Stop
   '   End Try
   'End Sub

End Class
