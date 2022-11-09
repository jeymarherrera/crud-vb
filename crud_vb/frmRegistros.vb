Imports System.IO 'para los archivos'
Imports System.Security.Policy

Public Class frmRegistros

#Region "Variables Globales"
    Dim identificacion, nombre, apellido, fecha_nac, genero, telefono, direccion As String
    Dim validarCampos, validarID As Boolean
    Dim Fila As String
#End Region

    'FUNCIONES PRINCIPALES'
#Region "Boton Nuevo"
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        LimpiarCampos()
        ActivarBotones()
    End Sub
#End Region

#Region "Boton Agregar"
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        identificacion = txtId.Text
        nombre = txtNombre.Text
        apellido = txtApellido.Text
        fecha_nac = txtFecha.Text
        genero = cbxGenero.Text
        telefono = txtTelefono.Text
        direccion = txtDireccion.Text

        validarCampos = ValidarDatos(identificacion, nombre, apellido, fecha_nac, genero, telefono, direccion)

        If validarCampos = True Then
            validarID = ValidarIdentificacion(identificacion)
            If validarID = True Then
                dgvRegistros.Rows.Add(identificacion, nombre, apellido, fecha_nac, genero, telefono, direccion)
                MsgBox("Contacto registrado exitosamente", MsgBoxStyle.Information, "Informacion")
                LimpiarCampos()
            End If
        End If
    End Sub
#End Region

#Region "Boton Modificar"
    'Boton Modificar'
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        identificacion = txtId.Text
        nombre = txtNombre.Text
        apellido = txtApellido.Text
        fecha_nac = txtFecha.Text
        genero = cbxGenero.Text
        telefono = txtTelefono.Text
        direccion = txtDireccion.Text

        validarCampos = ValidarDatos(identificacion, nombre, apellido, fecha_nac, genero, telefono, direccion)
        Fila = dgvRegistros.CurrentRow.Index
        validarID = True
        If validarCampos = True Then
            If identificacion <> dgvRegistros.Rows(Fila).Cells("ColumnId").Value Then
                validarID = ValidarIdentificacion(identificacion)
            End If

            If validarID = True Then
                dgvRegistros.Rows(Fila).Cells("ColumnId").Value = identificacion
                dgvRegistros.Rows(Fila).Cells("ColumnNombre").Value = nombre
                dgvRegistros.Rows(Fila).Cells("ColumnApellido").Value = apellido
                dgvRegistros.Rows(Fila).Cells("ColumnFecha").Value = fecha_nac
                dgvRegistros.Rows(Fila).Cells("ColumnGenero").Value = genero
                dgvRegistros.Rows(Fila).Cells("ColumnTelefono").Value = telefono
                dgvRegistros.Rows(Fila).Cells("ColumnDireccion").Value = direccion


                MsgBox("Modificacion exitosamente", MsgBoxStyle.Information, "Informacion")
                ActivarBotones()
                LimpiarCampos()
            Else

            End If
        End If
    End Sub

    'Seleccionar fila a modificar en el dgv'
    Private Sub dgvRegistros_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRegistros.CellDoubleClick
        Fila = dgvRegistros.CurrentRow.Index
        txtNombre.Text = dgvRegistros.Rows(Fila).Cells("ColumnNombre").Value
        txtId.Text = dgvRegistros.Rows(Fila).Cells("ColumnID").Value
        txtTelefono.Text = dgvRegistros.Rows(Fila).Cells("ColumnTelefono").Value
        txtDireccion.Text = dgvRegistros.Rows(Fila).Cells("ColumnDireccion").Value
        cbxGenero.Text = dgvRegistros.Rows(Fila).Cells("ColumnGenero").Value
        DesactivarBotones()
    End Sub

#End Region

#Region "Boton Eliminar"
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim confirmacion As String
        confirmacion = MsgBox("Seguro que desea eliminar este contacto?", MsgBoxStyle.YesNo, "Informacion")
        If confirmacion = vbYes Then
            Fila = dgvRegistros.CurrentRow.Index
            dgvRegistros.Rows.Remove(dgvRegistros.Rows(Fila))
            MsgBox("Eliminacion exitosa", MsgBoxStyle.Information, "Informacion")
            ActivarBotones()
            LimpiarCampos()
        End If
    End Sub
#End Region

#Region "Boton Guardar"
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        GuardarDatos()
    End Sub
#End Region

#Region "Boton Salir"
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Dim confirmacion As String
        confirmacion = MsgBox("Antes de salir. Deseas guardar los datos?", MsgBoxStyle.YesNo, "Informacion")
        If confirmacion = vbYes Then
            GuardarDatos()
        End If
        Me.Close()
    End Sub
#End Region

    'FUNCIONES EXTRAS'

#Region "Validar Campos Vacios"
    Function ValidarDatos(id As String, name As String, lastname As String, birth As String, gender As String, phone As String, address As String) As Boolean
        If id = "" Then
            MsgBox("Ingrese la identificacion, no puede ir vacio este campo", MsgBoxStyle.Critical, "Informacion")
            ErrorProviderValidar.SetError(txtId, "Ingrese identificacion")
            validarCampos = False
        ElseIf name = "" Then
            MsgBox("Ingrese el nombre, no puede ir vacio este campo", MsgBoxStyle.Critical, "Informacion")
            ErrorProviderValidar.SetError(txtNombre, "Ingrese nombre")
            validarCampos = False
        ElseIf lastname = "" Then
            MsgBox("Ingrese el apellido, no puede ir vacio este campo", MsgBoxStyle.Critical, "Informacion")
            ErrorProviderValidar.SetError(txtNombre, "Ingrese apellido")
            validarCampos = False
        ElseIf birth = "" Then
            MsgBox("Ingrese la fecha de nacimiento, no puede ir vacio este campo", MsgBoxStyle.Critical, "Informacion")
            ErrorProviderValidar.SetError(txtNombre, "Ingrese fecha de nacimiento")
            validarCampos = False
        ElseIf gender = "" Then
            MsgBox("Seleccione el genero, no puede ir sin seleccion", MsgBoxStyle.Critical, "Informacion")
            ErrorProviderValidar.SetError(cbxGenero, "Seleccione genero")
            validarCampos = False
        ElseIf phone = "" Then
            MsgBox("Ingrese el telefono, no puede ir vacio este campo", MsgBoxStyle.Critical, "Informacion")
            ErrorProviderValidar.SetError(txtTelefono, "Ingrese telefono")
            validarCampos = False
        ElseIf address = "" Then
            MsgBox("Ingrese la direccion, no puede ir vacio este campo", MsgBoxStyle.Critical, "Informacion")
            ErrorProviderValidar.SetError(txtDireccion, "Ingrese direccion")
            validarCampos = False
        Else
            validarCampos = True
            ErrorProviderValidar.SetError(txtId, "")
            ErrorProviderValidar.SetError(txtNombre, "")
            ErrorProviderValidar.SetError(txtApellido, "")
            ErrorProviderValidar.SetError(txtFecha, "")
            ErrorProviderValidar.SetError(cbxGenero, "")
            ErrorProviderValidar.SetError(txtTelefono, "")
            ErrorProviderValidar.SetError(txtDireccion, "")
        End If
        Return validarCampos
    End Function

#End Region

#Region "Validar Contacto Existente"
    Function ValidarIdentificacion(id As String) As Boolean
        validarID = True
        For y = 0 To dgvRegistros.RowCount - 1
            If id = dgvRegistros.Rows(y).Cells("ColumnId").Value Then
                MsgBox("Contacto Existente", MsgBoxStyle.Critical, "Informacion")
                ErrorProviderValidar.SetError(txtId, "Ingrese una identificacion nueva")
                validarID = False
                Exit For
            Else
                ErrorProviderValidar.SetError(txtId, "")
                validarID = True
            End If
        Next
        Return validarID
    End Function
#End Region

#Region "Limpiar Campos"
    Private Sub LimpiarCampos()
        txtId.Text = ""
        txtNombre.Text = ""
        txtApellido.Text = ""
        txtFecha.Text = ""
        txtTelefono.Text = ""
        txtDireccion.Text = ""
    End Sub
#End Region

#Region "Activar y Desactivar botones"
    Private Sub ActivarBotones()
        btnAgregar.Enabled = True
        btnGuardar.Enabled = True
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
    End Sub

    Private Sub DesactivarBotones()
        btnAgregar.Enabled = False
        btnGuardar.Enabled = False
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
    End Sub
#End Region

#Region "Escribir Archivo"
    Private Sub GuardarDatos()
        Dim archivo_escritura As StreamWriter
        Dim linea As String
        archivo_escritura = New StreamWriter("./datos.txt")
        With dgvRegistros
            For y = 0 To .RowCount - 1
                linea = .Rows(y).Cells("ColumnId").Value & ";" &
                    .Rows(y).Cells("ColumnNombre").Value & ";" &
                    .Rows(y).Cells("ColumnApellido").Value & ";" &
                    .Rows(y).Cells("ColumnFecha").Value & ";" &
                    .Rows(y).Cells("ColumnGenero").Value & ";" &
                    .Rows(y).Cells("ColumnTelefono").Value & ";" &
                    .Rows(y).Cells("ColumnDireccion").Value & ";"
                archivo_escritura.WriteLine(linea)
            Next
            MsgBox("Datos guardados exitosamente", MsgBoxStyle.Information, "Informacion")
        End With
        archivo_escritura.Close()
    End Sub
#End Region

#Region "Leer Archivo"
    Private Sub frmRegistros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim archivo_lectura As StreamReader
        archivo_lectura = New StreamReader("./datos.txt")
        While Not archivo_lectura.EndOfStream
            Dim cadena As String = archivo_lectura.ReadLine
            Dim lectura As String() = cadena.Split(New Char() {";"})
            dgvRegistros.Rows.Add(lectura)
        End While
        archivo_lectura.Close()
    End Sub
#End Region

End Class
