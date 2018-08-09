<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
   Inherits System.Windows.Forms.Form

   'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
   <System.Diagnostics.DebuggerNonUserCode()>
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Wird vom Windows Form-Designer benötigt.
   Private components As System.ComponentModel.IContainer

   'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
   'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
   'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
   <System.Diagnostics.DebuggerStepThrough()>
   Private Sub InitializeComponent()
      Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
      Me.btnStart1 = New Infragistics.Win.Misc.UltraButton()
      Me.lblOk = New Infragistics.Win.Misc.UltraLabel()
      Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
      Me.btnStart2 = New Infragistics.Win.Misc.UltraButton()
      Me.btnStart3 = New Infragistics.Win.Misc.UltraButton()
      Me.btnStart2MitReturn = New Infragistics.Win.Misc.UltraButton()
      Me.btnStart4 = New Infragistics.Win.Misc.UltraButton()
      Me.SuspendLayout()
      '
      'btnStart1
      '
      Me.btnStart1.Location = New System.Drawing.Point(27, 31)
      Me.btnStart1.Name = "btnStart1"
      Me.btnStart1.Size = New System.Drawing.Size(191, 49)
      Me.btnStart1.TabIndex = 0
      Me.btnStart1.Text = "Start"
      '
      'lblOk
      '
      Me.lblOk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Appearance1.BackColor = System.Drawing.Color.LightBlue
      Appearance1.FontData.BoldAsString = "True"
      Appearance1.FontData.SizeInPoints = 12.0!
      Appearance1.TextHAlignAsString = "Center"
      Appearance1.TextVAlignAsString = "Middle"
      Me.lblOk.Appearance = Appearance1
      Me.lblOk.Location = New System.Drawing.Point(302, 33)
      Me.lblOk.Name = "lblOk"
      Me.lblOk.Size = New System.Drawing.Size(691, 387)
      Me.lblOk.TabIndex = 1
      Me.lblOk.Text = "in Ruhe"
      '
      'btnCancel
      '
      Me.btnCancel.Location = New System.Drawing.Point(27, 486)
      Me.btnCancel.Name = "btnCancel"
      Me.btnCancel.Size = New System.Drawing.Size(191, 49)
      Me.btnCancel.TabIndex = 2
      Me.btnCancel.Text = "abbrechen"
      '
      'btnStart2
      '
      Me.btnStart2.Location = New System.Drawing.Point(27, 112)
      Me.btnStart2.Name = "btnStart2"
      Me.btnStart2.Size = New System.Drawing.Size(191, 49)
      Me.btnStart2.TabIndex = 3
      Me.btnStart2.Text = "Start 2 (abbrechbar)"
      '
      'btnStart3
      '
      Me.btnStart3.Location = New System.Drawing.Point(27, 282)
      Me.btnStart3.Name = "btnStart3"
      Me.btnStart3.Size = New System.Drawing.Size(191, 49)
      Me.btnStart3.TabIndex = 4
      Me.btnStart3.Text = "Start 3 (mit Zwischenstatus)"
      '
      'btnStart2MitReturn
      '
      Me.btnStart2MitReturn.Location = New System.Drawing.Point(27, 195)
      Me.btnStart2MitReturn.Name = "btnStart2MitReturn"
      Me.btnStart2MitReturn.Size = New System.Drawing.Size(191, 49)
      Me.btnStart2MitReturn.TabIndex = 5
      Me.btnStart2MitReturn.Text = "Start 2 (abbrechbar mit Return)"
      '
      'btnStart4
      '
      Me.btnStart4.Location = New System.Drawing.Point(27, 371)
      Me.btnStart4.Name = "btnStart4"
      Me.btnStart4.Size = New System.Drawing.Size(191, 49)
      Me.btnStart4.TabIndex = 6
      Me.btnStart4.Text = "Start 4 (mit Aufgabemanager)"
      '
      'Form1
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(1048, 568)
      Me.Controls.Add(Me.btnStart4)
      Me.Controls.Add(Me.btnStart2MitReturn)
      Me.Controls.Add(Me.btnStart3)
      Me.Controls.Add(Me.btnStart2)
      Me.Controls.Add(Me.btnCancel)
      Me.Controls.Add(Me.lblOk)
      Me.Controls.Add(Me.btnStart1)
      Me.Name = "Form1"
      Me.Text = "Form1"
      Me.ResumeLayout(False)

   End Sub

   Friend WithEvents btnStart1 As Infragistics.Win.Misc.UltraButton
   Friend WithEvents lblOk As Infragistics.Win.Misc.UltraLabel
   Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
   Friend WithEvents btnStart2 As Infragistics.Win.Misc.UltraButton
   Friend WithEvents btnStart3 As Infragistics.Win.Misc.UltraButton
   Friend WithEvents btnStart2MitReturn As Infragistics.Win.Misc.UltraButton
   Friend WithEvents btnStart4 As Infragistics.Win.Misc.UltraButton
End Class
