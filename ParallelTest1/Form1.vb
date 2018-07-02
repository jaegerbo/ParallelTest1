Imports System.Threading

Public Class Form1

   Private _Task As Task = Nothing
   Private _previousTime As DateTime = DateTime.Now
   Private _sc As SynchronizationContext = SynchronizationContext.Current
   Private _tokenSource As New CancellationTokenSource
   Private _cancelationtoken As CancellationToken = Nothing

   Private Sub btnStart1_Click(sender As Object, e As EventArgs) Handles btnStart1.Click
      Try
         clsAsync.Run(lblOk, Sub() Test1(4), "wird geladen")
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnStart2_Click(sender As Object, e As EventArgs) Handles btnStart2.Click
      Try
         Debug.WriteLine($"Start2 gestartet um {Now.ToLongTimeString}")
         _tokenSource.Cancel()
         _tokenSource = New CancellationTokenSource
         _cancelationtoken = Nothing
         _cancelationtoken = _tokenSource.Token

         'clsAsync.RunWithCancelation(lblOk, 5, _cancelationtoken, $"wird geladen {Now.ToLongTimeString}")

         Dim i As Integer = clsAsync.RunWithCancelation(lblOk, AddressOf Test3, $"wird geladen {Now.ToLongTimeString}").Result
         lblOk.Text = i.ToString

      Catch ex As Exception
         Stop
      End Try
      Debug.WriteLine($"Start2 beendet um {Now.ToLongTimeString}")
   End Sub
   Private Sub btnStart2MitReturn_Click(sender As Object, e As EventArgs) Handles btnStart2MitReturn.Click
      Try
         _tokenSource.Cancel()
         _cancelationtoken = _tokenSource.Token

         clsAsync.RunWithCancelationAndReturn(lblOk, 5, _cancelationtoken, "wird geladen")
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnStart3_Click(sender As Object, e As EventArgs) Handles btnStart3.Click
      Try
         _tokenSource.Cancel()
         _cancelationtoken = _tokenSource.Token

         _Task = clsAsync.RunTest5(lblOk, 4000000, _tokenSource.Token, True)
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      Try
         Debug.WriteLine($"aktueller Prozess gecancelt um {Now.ToLongTimeString}")
         _tokenSource.Cancel()
      Catch ex As Exception
         Stop
      End Try
   End Sub

End Class
