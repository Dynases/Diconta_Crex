Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports DevComponents.DotNetBar.Controls
Public Class F0_CierreGestion
#Region "Variables Locales"
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim gs_RutaImg As String = ""
    Public Limpiar As Boolean = False  'Bandera para indicar si limpiar todos los datos o mantener datos ya registrados
    Dim _Inter As Integer = 0
#End Region
#Region "Metodos Privados"
    Private Sub _prIniciarTodo()
        Me.Text = "CIERRE DE MES"
        _prMaxLength()
        _prCargarComboLibreria(cbAno, 10, 1)
        CargarGrillaGestion()
        Inhabilitar()
        Dim blah As New Bitmap(New Bitmap(My.Resources.producto), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        btnEliminar.Visible = False
    End Sub
    Private Sub CargarGrillaGestion()
        Dim dt As New DataTable
        dt = L_MostraCierreGestion()
        grGestion.DataSource = dt
        grGestion.RetrieveStructure()
        grGestion.AlternatingColors = True
        With grGestion.RootTable.Columns("Id")
            .Visible = True
        End With
        With grGestion.RootTable.Columns("Anio")
            .Visible = False
        End With
        With grGestion.RootTable.Columns("DescAnio")
            .Caption = "AÑO"
            .Width = 100
            .Visible = False
        End With
        With grGestion.RootTable.Columns("Descripcion")
            .Caption = "Descripcion"
            .Width = 450
            .Visible = True
        End With

        With grGestion.RootTable.Columns("FechaReg")
            .Caption = "FECHA"
            .Width = 120
            .Visible = True
        End With

        With grGestion.RootTable.Columns("Fecha")
            .Visible = False
        End With
        With grGestion.RootTable.Columns("Hora")
            .Visible = False
        End With
        With grGestion.RootTable.Columns("Usuario")
            .Visible = False
        End With
        With grGestion
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
        End With
    End Sub
    Private Sub CargarGrillaDetalle(IdProducto As Integer)
        Dim dt As New DataTable
        dt = L_MostraCierreGestionDeMes(IdProducto)
        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()
        grDetalle.AlternatingColors = True

        With grDetalle.RootTable.Columns("Id")
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("IdCierreGestion")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("Mes")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("DescMes")
            .HeaderAlignment = TextAlignment.Center
            .Caption = "Mes"
            .Width = 70
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("Estado")
            .Caption = "Estado"
            .HeaderAlignment = TextAlignment.Center
            .Width = 300
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("CierreDemes")
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("Seleccion")
            .Caption = "Seleccion"
            .Visible = True
            .Width = 50
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.FontSize = 100
            '.EditType = EditType.CheckBox
            '.ColumnType = ColumnType.CheckBox
            '.CheckBoxFalseValue = False
            '.CheckBoxTrueValue = True
        End With
        With grDetalle
            .GroupByBoxVisible = False
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub
    Public Sub _prMaxLength()
        cbAno.MaxLength = 40
    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaDetalleGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cnnum").Width = 70
            .DropDownList.Columns("cnnum").Caption = "COD"

            .DropDownList.Columns.Add("cndesc1").Width = 200
            .DropDownList.Columns("cndesc1").Caption = "DESCRIPCION"

            .ValueMember = "cnnum"
            .DisplayMember = "cndesc1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub agregarFilaDetalle()
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 1, "ENERO", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 2, "FEBRERO", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 3, "MARZO", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 4, "ABRIL", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 5, "MAYO", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 6, "JUNIO", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 7, "JULIO", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 8, "AGOSTO", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 9, "SEPTIEMBRE", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 10, "OCTUBRE", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 11, "NOVIEMBRE", 1, "ABIERTO", 0)
        CType(grDetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 12, "DICIEMBRE", 1, "ABIERTO", 0)
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grDetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("Id=MAX(Id)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("Id")
        End If
        Return 1
    End Function
#End Region

    Public Sub _prFiltrar()
        Dim _Mpos As Integer
        CargarGrillaGestion()
        If grGestion.RowCount > 0 Then
            _Mpos = 0
            grGestion.Row = _Mpos
        Else
            _LimpiarDatos()
            LblPaginacion.Text = "0/0"
        End If
    End Sub

#Region "METODOS SOBRECARGADOS"

    Public Sub Habilitar()
        cbAno.ReadOnly = False
        tbDescripcion.ReadOnly = False
        btnImprimir.Visible = False
        grDetalle.Enabled = True
    End Sub

    Public Sub Inhabilitar()
        tbId.ReadOnly = True
        tbDescripcion.ReadOnly = True
        cbAno.ReadOnly = True
        btnImprimir.Visible = True
        grDetalle.Enabled = False
    End Sub
    Public Sub habilitarMenu()
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnGrabar.Enabled = True

    End Sub
    Public Sub inHabilitarMenu()
        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnGrabar.Enabled = False

    End Sub
    Public Sub _LimpiarDatos()
        Try
            tbId.Clear()
            tbDescripcion.Clear()
            tbFecha.Value = DateTime.Today
            If (Limpiar = False) Then
                _prSeleccionarCombo(cbAno)
            End If
            CargarGrillaDetalle(0)
            agregarFilaDetalle()
            LimpiarErrores()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub MostrarMensajeOk(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.GRABACION_EXITOSA,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
    Private Sub MostrarMensajeExito(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.checked,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
    Public Sub _prSeleccionarCombo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        If (CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        Else
            mCombo.SelectedIndex = -1
        End If
    End Sub

    Public Sub LimpiarErrores()
        MEP.Clear()
        cbAno.BackColor = Color.White
    End Sub

    Public Sub _PMOGrabarRegistro()
        Dim resultado As Boolean = False
        Try
            Dim nuevo, modificar As Integer
            nuevo = 1
            modificar = 2
            Dim id As Integer = IIf(tbId.Text = String.Empty, 0, tbId.Text)
            'Se define el tipo de evento 
            Dim tipoEvento As Integer = IIf(id = 0, nuevo, modificar)

            Using ts As New Transactions.TransactionScope

                resultado = L_GuardarCierreGestion(id, tbDescripcion.Text, cbAno.Value,
                                                tbFecha.Value, tipoEvento, CType(grDetalle.DataSource, DataTable))
                ts.Complete()
            End Using
            If resultado Then
                Dim mensaje = "Código de Gestión ".ToUpper + id.ToString() + IIf(tipoEvento = 1, " GRABADO", " MODIFICADO") + " con Exito.".ToUpper
                MostrarMensajeOk(mensaje)

                _prFiltrar()
                If tipoEvento = nuevo Then
                    Limpiar = True
                    _LimpiarDatos()
                    habilitarMenu()
                Else
                    Inhabilitar()
                    inHabilitarMenu()
                End If

            Else
                Throw New Exception("Ocurrio un error inesperado, no se Grabo el registro")
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Function _fnActionNuevo() As Boolean
        Return tbId.Text = String.Empty And tbDescripcion.ReadOnly = False
    End Function

    Public Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()
        If cbAno.SelectedIndex < 0 Then
            cbAno.BackColor = Color.Red
            MEP.SetError(cbAno, "Selecciones año de la Gestión!".ToUpper)
            _ok = False
        Else
            cbAno.BackColor = Color.White
            MEP.SetError(cbAno, "")
        End If
        If tbId.Text = String.Empty Then
            If VerificarAnioRepetido(tbFecha.Value.Year.ToString()) Then
                cbAno.BackColor = Color.Red
                MEP.SetError(cbAno, "Existe un cierre con el AÑO Específicado!".ToUpper)
                _ok = False
            Else
                cbAno.BackColor = Color.White
                MEP.SetError(cbAno, "")
            End If
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Sub _PMOMostrarRegistro()
        With grGestion
            tbId.Text = .GetValue("Id").ToString
            tbDescripcion.Text = .GetValue("Descripcion").ToString
            tbFecha.Value = .GetValue("FechaReg")
            cbAno.Value = .GetValue("Anio")
            lbFecha.Text = CType(.GetValue("Fecha"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("Hora").ToString
            lbUsuario.Text = .GetValue("Usuario").ToString

            CargarGrillaDetalle(tbId.Text)
        End With
        LblPaginacion.Text = Str(grGestion.Row + 1) + "/" + grGestion.RowCount.ToString
    End Sub

#End Region
#Region "Eventos"
    Private Sub F0_ProductoMateriaPrima_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        Dim tb As TextBoxX = CType(sender, TextBoxX)
        If tb.Text = String.Empty Then

        Else
            tb.BackColor = Color.White
            MEP.SetError(tb, "")
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _prFiltrar()
            Inhabilitar()
            inHabilitarMenu()
        Else
            _modulo.Select()
            _tab.Close()
            'Me.Close()
        End If
    End Sub


    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDetalle.EditingCell
        If (e.Column.Index = grDetalle.RootTable.Columns("Seleccion").Index) Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        Dim _MPos As Integer
        If grGestion.RowCount > 0 Then
            _MPos = 0
            grGestion.Row = _MPos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grGestion.Row
        If _MPos > 0 And grGestion.RowCount > 0 Then
            _MPos = _MPos - 1
            grGestion.Row = _MPos
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grGestion.Row
        If _pos < grGestion.RowCount - 1 And _pos >= 0 Then
            _pos = grGestion.Row + 1
            grGestion.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grGestion.Row
        If grGestion.RowCount > 0 Then
            _pos = grGestion.RowCount - 1
            grGestion.Row = _pos
        End If
    End Sub

    Private Sub grGestion_SelectionChanged(sender As Object, e As EventArgs) Handles grGestion.SelectionChanged
        If (grGestion.RowCount >= 0 And grGestion.Row >= 0) Then
            _PMOMostrarRegistro()
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _LimpiarDatos()
        Habilitar()
        habilitarMenu()
        cbAno.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Habilitar()
        habilitarMenu()
        cbAno.Focus()
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _PMOValidarCampos() Then
            _PMOGrabarRegistro()
        End If
    End Sub

    Private Sub grGestion_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grGestion.EditingCell
        e.Cancel = True
    End Sub

    Private Sub grDetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellValueChanged
        If (e.Column.Index = grDetalle.RootTable.Columns("Seleccion").Index) Then
            grDetalle.UpdateData()
            For Each fila As DataRow In CType(grDetalle.DataSource, DataTable).Rows
                If fila.Item("Seleccion") = True Then
                    fila.Item("CierreDeMes") = 2
                    fila.Item("Estado") = "CERRADO"
                    'grDetalle.CurrentRow.Cells("CierreDeMes").Value = 2
                Else
                    fila.Item("CierreDeMes") = 1
                    fila.Item("Estado") = "ABIERTO"
                End If
            Next
        End If
    End Sub

    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellEdited
        'For Each fila As DataRow In CType(grDetalle.DataSource, DataTable).Rows
        '    If fila.Item("Seleccion") = True Then
        '        fila.Item("CierreDeMes") = 2
        '        'grDetalle.CurrentRow.Cells("CierreDeMes").Value = 2
        '    Else
        '        fila.Item("CierreDeMes") = 1
        '    End If
        'Next
    End Sub
#End Region
End Class