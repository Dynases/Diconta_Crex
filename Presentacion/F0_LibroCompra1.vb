Imports Logica.AccesoLogica
Imports DevComponents.Editors
Imports DevComponents.DotNetBar.SuperGrid
Imports System.IO
Imports System.Drawing.Printing
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX

Public Class F0_LibroCompra1

#Region "Variables Globales"

    Dim _DuracionSms As Integer = 5
    Dim _DsLV As DataTable
    Public _modulo As SideNavItem
    Public _nameButton As String
    Public _tab As SuperTabItem
    Private inDuracion As Byte = 5

    Dim codReporte As String = "LibCom"


#End Region

#Region "Eventos"

    Private Sub P_LibroVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        P_Inicio()
    End Sub

#End Region


#Region "Metodos"
    Private Sub P_Inicio()

        Me.WindowState = FormWindowState.Maximized
        Me.Text = "L I B R O   D E   C O M P R A S"

        btnNuevo.Visible = False
        btnModificar.Visible = False
        btnEliminar.Visible = False
        btnGrabar.Visible = False
        btnSalir.Visible = False
        btnImprimir.Visible = False

        btnPrimero.Visible = False
        btnAnterior.Visible = False
        btnSiguiente.Visible = False
        btnUltimo.Visible = False

        LblPaginacion.Visible = False
        BubbleBarUsuario.Visible = False


        CpExportarExcel.Visible = False

        P_prArmarCombos()


        P_prArmarGrillas()

        P_ArmarGrilla()
    End Sub

    Private Sub P_prArmarCombos()
        P_prArmarComboAno()
        P_prArmarComboMes()
    End Sub

    Private Sub P_prArmarGrillas()
        P_prArmarGrillaLibroCompra("-1", "-1")
    End Sub

    Private Sub P_prArmarComboAno()
        Dim dt As New DataTable
        dt = L_prCompraComprobanteGeneralAnios()

        With cbAno.DropDownList
            .Columns.Clear()

            .Columns.Add(dt.Columns("anho").ToString).Width = 100
            .Columns(0).Caption = "Año"
            .Columns(0).Visible = True

            .ValueMember = dt.Columns("anho").ToString
            .DisplayMember = dt.Columns("anho").ToString
            .DataSource = dt
            .Refresh()
        End With

        cbAno.SelectedIndex = dt.Rows.Count - 1
    End Sub


    Private Sub P_prArmarComboMes()
        Dim dt As New DataTable

        dt.Columns.Add("nro", Type.GetType("System.Int32"))
        dt.Columns.Add("mes", Type.GetType("System.String"))

        Dim fil As DataRow
        For i = 1 To 12
            fil = dt.NewRow
            fil(0) = i
            fil(1) = MonthName(i)
            dt.Rows.Add(fil)
        Next

        With cbMes.DropDownList
            .Columns.Clear()

            .Columns.Add(dt.Columns("nro").ToString).Width = 60
            .Columns(0).Caption = "Nro."
            .Columns(0).Visible = True

            .Columns.Add(dt.Columns("mes").ToString).Width = 140
            .Columns(1).Caption = "Mes"
            .Columns(1).Visible = True

            .ValueMember = dt.Columns("nro").ToString
            .DisplayMember = dt.Columns("mes").ToString
            .DataSource = dt
            .Refresh()
        End With

        cbMes.SelectedIndex = Month(Now.Date) - 1
    End Sub

    Private Sub P_prArmarGrillaLibroCompra(mes As String, ano As String)
        Dim dt As New DataTable
        dt = L_prCompraComprobanteGeneralLibroCompra(ano, mes, gi_empresaNumi)

        dgjLibroCompra.BoundMode = Janus.Data.BoundMode.Bound
        dgjLibroCompra.DataSource = dt
        dgjLibroCompra.RetrieveStructure()

        'dar formato a las columnas
        With dgjLibroCompra.RootTable.Columns("esp")
            .Caption = "ESP"
            .Width = 40
            ''.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ''.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("row")
            .Caption = "NRO."
            .Width = 80
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcanumi")
            .Visible = False
        End With
        With dgjLibroCompra.RootTable.Columns("fcanit")
            .Caption = "NIT PROVEEDOR"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjLibroCompra.RootTable.Columns("fcarsocial")
            .Caption = "RAZON SOCIAL PROVEEDOR"
            .Width = 200
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjLibroCompra.RootTable.Columns("fcaautoriz")
            .Caption = "CODIGO DE AUTORIZACION"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcanfac")
            .Caption = "NUMERO FACTURA"
            .Width = 100
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcandui")
            .Caption = "NUMERO DUI/DIM"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjLibroCompra.RootTable.Columns("fcafdoc")
            .Caption = "FECHA DE FACTURA/DUI/DIM"
            .Width = 100
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjLibroCompra.RootTable.Columns("fcaitc2")
            .Caption = "IMPORTE TOTAL COMPRA"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaIce")
            .Caption = "IMPORTE ICE"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaIehd")
            .Caption = "IMPORTE IEHD"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaIpj")
            .Caption = "IMPORTE IPJ"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaTasas")
            .Caption = "TASAS"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcanscf")
            .Caption = "NO SUJETO A CREDITO FISCAL"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaExentos")
            .Caption = "IMPORTES EXENTOS"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaGravTasa0")
            .Caption = "IMPORTE COMPRAS GRAVADAS A TASA CERO"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaSubtotal2")
            .Caption = "SUBTOTAL"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With dgjLibroCompra.RootTable.Columns("fcadesc")
            .Caption = "DESCUENTOS/BONIFICACIONES /REBAJAS SUJETAS AL IVA"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaGCard")
            .Caption = "IMPORTE GIFT CARD"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcaibcf2")
            .Caption = "IMPORTE BASE CF"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With dgjLibroCompra.RootTable.Columns("fcacfiscal2")
            .Caption = "CREDITO FISCAL"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With
        With dgjLibroCompra.RootTable.Columns("fcatcom")
            .Caption = "TIPO COMPRA"
            .Width = 100
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjLibroCompra.RootTable.Columns("fcaccont")
            .Caption = "CODIGO DE CONTROL"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjLibroCompra.RootTable.Columns("fcaCdcf")
            .Caption = "CON DERECHO A CREDITO FISCAL"
            .Width = 90
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With
        With dgjLibroCompra.RootTable.Columns("fcaConsolid")
            .Caption = "ESTADO CONSOLIDACION"
            .Width = 120
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With


        'Habilitar Filtradores
        With dgjLibroCompra
            .GroupByBoxVisible = False
            '.FilterRowFormatStyle.BackColor = Color.Blue
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .SelectionMode = SelectionMode.MultipleSelection
            .AlternatingColors = True
            .RecordNavigator = True
        End With
    End Sub

    Private Function P_prExportarExcel() As Boolean
        Dim rutaArchivo As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            rutaArchivo = gs_RutaArchivos + "\Compras\Libro de Compras"
            If (Not Directory.Exists(gs_RutaArchivos + "\Compras\Libro de Compras")) Then
                Directory.CreateDirectory(gs_RutaArchivos + "\Compras\Libro de Compras")
            End If
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = dgjLibroCompra.RowCount
                Dim _columna As Integer = dgjLibroCompra.RootTable.Columns.Count
                Dim _archivo As String = rutaArchivo & "\Libro_Compra_" & "_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In dgjLibroCompra.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                CpExportarExcel.Visible = True
                CpExportarExcel.Minimum = 1
                CpExportarExcel.Maximum = dgjLibroCompra.RowCount
                CpExportarExcel.Value = 1

                For Each _fil As GridEXRow In dgjLibroCompra.GetRows
                    For Each _col As GridEXColumn In dgjLibroCompra.RootTable.Columns
                        If (_col.Visible) Then
                            _linea = _linea & CStr(_fil.Cells(_col.Key).Value) & ";"
                        End If
                    Next
                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing
                    CpExportarExcel.Value += 1
                Next
                _escritor.Close()
                CpExportarExcel.Visible = False
                Try
                    Dim info As New TaskDialogInfo("¿desea abrir el libro de compra?".ToUpper,
                                                   eTaskDialogIcon.Exclamation,
                                                   "pregunta".ToUpper,
                                                   "Desea continuar?".ToUpper,
                                                   eTaskDialogButton.Yes Or eTaskDialogButton.Cancel,
                                                   eTaskDialogBackgroundColor.Blue)
                    Dim result As eTaskDialogResult = TaskDialog.Show(info)
                    If result = eTaskDialogResult.Yes Then
                        Process.Start(_archivo)
                    End If
                    Return True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return False
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function


    Private Sub _prImprimir()
        Dim objrep As New R_LibroCompra
        Dim dt As New DataTable
        If IsNothing(dgjLibroCompra.DataSource) = False Then
            dt = CType(dgjLibroCompra.DataSource, DataTable)
            If dt.Rows.Count > 0 Then

                'ahora lo mando al visualizador
                Dim dtTitulos As DataTable = L_prTitulosAll(codReporte)

                P_Global.Visualizador = New Visualizador
                objrep.SetDataSource(dt)

                objrep.SetParameterValue("periodo", cbMes.Value.ToString + "/" + cbAno.Text)
                objrep.SetParameterValue("ci", dtTitulos.Rows(0).Item("yedesc").ToString)
                objrep.SetParameterValue("nombre", dtTitulos.Rows(1).Item("yedesc").ToString)

                'objrep.SetParameterValue("empresaDesc", gs_empresaDescSistema)
                objrep.SetParameterValue("empresaDesc", gs_empresaDescSistema + " " + gs_empresaDesc.ToUpper)

                objrep.SetParameterValue("empresaNit", gs_empresaNit)
                objrep.SetParameterValue("empresaDirec", gs_empresaDireccion)

                P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
                P_Global.Visualizador.Show() 'Comentar
                P_Global.Visualizador.BringToFront() 'Comentar
            End If
        End If

    End Sub
    Private Function P_ExportarTxt(TipoLibroVenta As String, Separador As String) As Boolean
        Dim _ubicacion As String
        _ubicacion = gs_CarpetaRaiz
        Try
            'DgdLC.PrimaryGrid.Rows.Clear()
            'DgdLC.PrimaryGrid.DataSource = L_prCompraComprobanteGeneralLibroCompra2(cbAno.Value.ToString, cbMes.Value.ToString, gi_empresaNumi)
            'DgdLC.PrimaryGrid.SetActiveRow(CType(DgdLC.PrimaryGrid.ActiveRow, GridRow))
            Dim _stream As Stream
            Dim _escritor As StreamWriter
            Dim _fila As Integer = DgdLC.PrimaryGrid.Rows.Count
            Dim _columna As Integer = DgdLC.PrimaryGrid.Columns.Count
            Dim _archivo As String = _ubicacion & "\LCV_" & Now.Date.Day &
                "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".txt"
            Dim _linea As String = "" 'TipoLibroVenta + "|"
            Dim _filadata = 0, columndata As Int32 = 0
            File.Delete(_archivo)
            _stream = File.OpenWrite(_archivo)
            _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

            Dim nro As Integer = 1
            For Each _fil As GridRow In DgdLC.PrimaryGrid.Rows
                _linea = TipoLibroVenta + Separador + nro.ToString + Separador
                '_linea = nro.ToString + Separador
                For Each _col As GridColumn In DgdLC.PrimaryGrid.Columns
                    If (_col.Visible And Not _col.Name.Equals("factura")) Then
                        _linea = _linea & CStr(_fil.Cells(_col.Name).Value).Trim & Separador
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing
                nro += 1
            Next
            _escritor.Close()
            Try
                If (MessageBox.Show("DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    Process.Start(_archivo)
                End If
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return False
    End Function
    Private Sub P_ArmarGrilla()

        DgdLC.PrimaryGrid.Columns.Clear()
        'Alto de la Fila de Nombres de Columnas
        DgdLC.PrimaryGrid.ColumnHeader.RowHeight = 25

        'Mostrar u Ocultar la Fila de Filtrado
        DgdLC.PrimaryGrid.EnableColumnFiltering = True
        DgdLC.PrimaryGrid.EnableFiltering = True
        DgdLC.PrimaryGrid.EnableRowFiltering = True
        DgdLC.PrimaryGrid.Filter.Visible = True

        'Para Mostrar u Ocultar la Columna de Cabecera de las Filas
        DgdLC.PrimaryGrid.ShowRowHeaders = True

        'Para Mostrar el Indice de la Grilla
        DgdLC.PrimaryGrid.RowHeaderIndexOffset = 1
        DgdLC.PrimaryGrid.ShowRowGridIndex = True

        'Alto de las Filas
        DgdLC.PrimaryGrid.DefaultRowHeight = 22

        'Alternar Colores de las Filas
        DgdLC.PrimaryGrid.UseAlternateRowStyle = True

        'Para permitir o denegar el cambio de tamaño de la Filas
        DgdLC.PrimaryGrid.AllowRowResize = False

        'Para que el Tamaño de las Columnas se pongan automaticamente
        'DgdLCV.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells

        DgdLC.PrimaryGrid.SelectionGranularity = SelectionGranularity.RowWithCellHighlight

        Dim col As GridColumn

        ''Nro
        'col = New GridColumn("Nro")
        'col.HeaderText = "Nro"
        'col.EditorType = GetType(GridTextBoxXEditControl)
        'col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        'col.ReadOnly = True
        'col.Visible = True
        'col.Width = 60
        'DgdLCV.PrimaryGrid.Columns.Add(col)

        'Codigo
        col = New GridColumn("fcanumi")
        col.HeaderText = "Codigo"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = False
        col.Width = 80
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Nit
        col = New GridColumn("fcanit")
        col.HeaderText = "NIT PROVEEDOR"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Razón Social
        col = New GridColumn("fcarsocial")
        col.HeaderText = "RAZON SOCIAL PROVEEDOR"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 200
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Nro Autorizacion
        col = New GridColumn("fcaautoriz")
        col.HeaderText = "CODIGO DE AUTORIZACION"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Número Factura
        col = New GridColumn("fcanfac")
        col.HeaderText = "NUMERO FACTURA"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Dui
        col = New GridColumn("fcandui")
        col.HeaderText = "NUMERO DUI/DIM"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Fecha
        col = New GridColumn("fcafdoc")
        col.HeaderText = "FECHA DE FACTURA/DUI/DIM"
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleCenter
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Importe
        col = New GridColumn("fcaitc2")
        col.HeaderText = "IMPORTE TOTAL COMPRA"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Ice
        col = New GridColumn("fcaIce")
        col.HeaderText = "IMPORTE ICE"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Iehd
        col = New GridColumn("fcaIehd")
        col.HeaderText = "IMPORTE IEHD"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Ipj
        col = New GridColumn("fcaIpj")
        col.HeaderText = "IMPORTE IPJ"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Tasas
        col = New GridColumn("fcaTasas")
        col.HeaderText = "TASAS"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'No SCF
        col = New GridColumn("fcanscf")
        col.HeaderText = "NO SUJETO A CREDITO FISCAL"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)


        'Exentos
        col = New GridColumn("fcaExentos")
        col.HeaderText = "IMPORTES EXENTOS"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Gravado a tasa 0
        col = New GridColumn("fcaGravTasa0")
        col.HeaderText = "IMPORTE COMPRAS GRAVADAS A TASA CERO"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'SubTotal
        col = New GridColumn("fcaSubtotal2")
        col.HeaderText = "SUBTOTAL"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Descuentos
        col = New GridColumn("fcadesc")
        col.HeaderText = "DESCUENTOS/BONIFICACIONES /REBAJAS SUJETAS AL IVA"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Gift Card
        col = New GridColumn("fcaGCard")
        col.HeaderText = "IMPORTE GIFT CARD"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'F
        col = New GridColumn("fcaibcf2")
        col.HeaderText = "IMPORTE BASE CF"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'G
        col = New GridColumn("fcacfiscal2")
        col.HeaderText = "CREDITO FISCAL"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Tipo Compra
        col = New GridColumn("fcatcom")
        col.HeaderText = "TIPO COMPRA"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Codigo de Control
        col = New GridColumn("fcaccont")
        col.HeaderText = "CODIGO DE CONTROL"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)


        'Con derecho a CF
        col = New GridColumn("fcaCdcf")
        col.HeaderText = "CON DERECHO A CREDITO FISCAL"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)


        'Estado
        col = New GridColumn("fcaConsolid")
        col.HeaderText = "ESTADO CONSOLIDACION"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

    End Sub
#End Region

    Private Sub _prSalir()
        _modulo.Select()
        _tab.Close()
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub btGenerar_Click(sender As Object, e As EventArgs) Handles btGenerar.Click
        P_prArmarGrillaLibroCompra(cbMes.Value.ToString, cbAno.Value.ToString)
        DgdLC.PrimaryGrid.Rows.Clear()
        DgdLC.PrimaryGrid.DataSource = L_prCompraComprobanteGeneralLibroCompra2(cbAno.Value.ToString, cbMes.Value.ToString, gi_empresaNumi)
        'DgdLC.PrimaryGrid.DataSource = CType(dgjLibroCompra.DataSource, DataTable)
        DgdLC.PrimaryGrid.SetActiveRow(CType(DgdLC.PrimaryGrid.ActiveRow, GridRow))
        If (dgjLibroCompra.GetRows.Count = 0) Then
            ToastNotification.Show(Me,
                                   "No hay compras para el año y mes seleccionados.".ToUpper,
                                   My.Resources.INFORMATION, inDuracion * 1000,
                                   eToastGlowColor.Blue,
                                   eToastPosition.TopCenter)
        End If
    End Sub

    Private Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        If (P_prExportarExcel()) Then
            ToastNotification.Show(Me, "Exportación de libro de compras exitosa.".ToUpper,
                                       My.Resources.GRABACION_EXITOSA, inDuracion * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.TopCenter)
        Else
            ToastNotification.Show(Me, "Fallo la esporatación de el libro de compras.".ToUpper,
                                       My.Resources.WARNING, inDuracion * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.TopCenter)
        End If
    End Sub

    Private Sub btReporte_Click(sender As Object, e As EventArgs) Handles btReporte.Click
        _prImprimir()
    End Sub
    Private Sub btTxt_Click_1(sender As Object, e As EventArgs) Handles btTxt.Click
        If (P_ExportarTxt("3", "|")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRECIOS EXITOSA..!!!",
                                       My.Resources.OK1, _DuracionSms * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomLeft)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE LISTA DE PRECIOS..!!!",
                                       My.Resources.WARNING, _DuracionSms * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Sub btSalir_Click(sender As Object, e As EventArgs) Handles btSalir.Click
        _prSalir()
    End Sub
End Class