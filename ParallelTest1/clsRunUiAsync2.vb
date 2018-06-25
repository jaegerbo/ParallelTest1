Public Class clsRunUiAsync2

   Public Shared Function Run(mainControl As Control, Aktion As [Delegate], Optional showWaitControl As Boolean = True) As Task
      ' Zweck:    für das gegebene mainControl die gegebene Aktion asynchron durchführen
      Try
         If mainControl Is Nothing Then
            Return Nothing
         End If

         'Return Task.Factory.StartNew(Sub()
         '                                Dim waitControl As Infragistics.Win.Misc.UltraLabel = Nothing
         '                                If showWaitControl = True Then
         '                                   waitControl = New Infragistics.Win.Misc.UltraLabel
         '                                End If

         '                                mainControl.BeginInvoke(Sub() waitControlEinrichten(mainControl, waitControl))
         '                                mainControl.BeginInvoke(Aktion)
         '                                mainControl.BeginInvoke(Sub() fertig(mainControl, waitControl))
         '                             End Sub)

         Dim waitControl As Infragistics.Win.Misc.UltraLabel = Nothing
         If showWaitControl = True Then
            waitControl = New Infragistics.Win.Misc.UltraLabel
         End If

         mainControl.BeginInvoke(Sub() waitControlEinrichten(mainControl, waitControl))
         mainControl.BeginInvoke(Aktion)
         mainControl.BeginInvoke(Sub() fertig(mainControl, waitControl))

      Catch ex As Exception
         Stop
      End Try
      Return Nothing
   End Function

   Private Shared Sub waitControlEinrichten(mainControl As Control, waitControl As Infragistics.Win.Misc.UltraLabel)
      ' Zweck:    Wenn ein waitControl angezeigt werden soll, wird es hier erzeugt
      Try
         If waitControl IsNot Nothing Then
            mainControl.Visible = False
            With waitControl
               .Top = mainControl.Top
               .Left = mainControl.Left
               .Width = mainControl.Width
               .Height = mainControl.Height
               .Text = "wird geladen"
               .Appearance.TextHAlign = Infragistics.Win.HAlign.Center
               .Appearance.TextVAlign = Infragistics.Win.VAlign.Middle
               .Appearance.FontData.SizeInPoints = CSng(.Width / 20)
               .Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True
            End With
            mainControl.Parent.Controls.Add(waitControl)
            Application.DoEvents()
         End If
      Catch ex As Exception
         Stop
      End Try
   End Sub
   Private Shared Sub fertig(mainControl As Control, waitControl As Control)
      ' Zweck:    Wenn ein waitControl vorhanden war, wird es wieder entfernt
      Try
         If waitControl IsNot Nothing Then
            mainControl.Visible = True
            mainControl.Parent.Controls.Remove(waitControl)
            waitControl.Dispose()
            Application.DoEvents()
         End If
      Catch ex As Exception
         Stop
      End Try
   End Sub


End Class
