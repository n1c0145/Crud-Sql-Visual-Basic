Imports System.Data.SqlClient
Public Class Form1
    Private Sub AgregarProducto(sender As Object, e As EventArgs) Handles Button1.Click
        Dim producto As New Class1
        producto.id = CInt(Me.TextBox1.Text)
        producto.descripcion = Me.TextBox2.Text
        producto.precio = CDbl(Me.TextBox3.Text)
        Agregar(producto)
        CargarDatos()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

    Function Agregar(valor As Class1)

        Dim conexion As New SqlConnection(My.Settings.CONEXION_SQLSERVER)
        conexion.Open()
        Dim consulta As String
        consulta = "Insert into productos(id, descripcion, precio) values (" & valor.id & ", '" & valor.descripcion & "'," & valor.precio & ")"
        Dim comandoSql As New SqlCommand(consulta, conexion)
        comandoSql.ExecuteNonQuery()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        MsgBox("Dato Agregado")

    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ProductosTableAdapter.Fill(Me.ProductosDataSet.productos)

    End Sub

    Function CargarDatos()
        Dim conexion As New SqlConnection(My.Settings.CONEXION_SQLSERVER)
        Dim consulta As String
        consulta = "Select * from productos"
        Dim comandoSql As New SqlCommand(consulta, conexion)
        Dim adapter As New SqlDataAdapter(comandoSql)
        Dim datosTablaProducto As New DataSet
        adapter.Fill(datosTablaProducto, "PRODUCTOS")
        Me.DataGridView1.DataSource = datosTablaProducto.Tables("PRODUCTOS")

    End Function
    Function eliminar(valor As Class1)

        Dim conexion As New SqlConnection(My.Settings.CONEXION_SQLSERVER)
        conexion.Open()
        Dim delete As String
        delete = "delete from productos where id=" & TextBox1.Text & " "
        'delete = "delete from productos where id=" TextBox1.Text " "
        Dim comandoSql As New SqlCommand(delete, conexion)

        comandoSql.ExecuteNonQuery()
        TextBox1.Text = ""
        MsgBox("Dato Eliminado")
    End Function
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim producto As New Class1
        producto.id = CInt(Me.TextBox1.Text)
        eliminar(producto)
        CargarDatos()
    End Sub


    Function editar(valor As Class1)

        Dim conexion As New SqlConnection(My.Settings.CONEXION_SQLSERVER)
        conexion.Open()
        Dim update As String
        update = "UPDATE productos set descripcion = @descripcion , precio = @precio WHERE id=@id"

        Dim comandoSql As New SqlCommand(update, conexion)
        comandoSql.Parameters.AddWithValue("@id", TextBox1.Text)
        comandoSql.Parameters.AddWithValue("@descripcion", TextBox2.Text)
        comandoSql.Parameters.AddWithValue("@precio", TextBox3.Text)

        comandoSql.ExecuteNonQuery()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        MsgBox("Dato Actualizado")
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim producto As New Class1
        producto.id = CInt(Me.TextBox1.Text)
        editar(producto)
        CargarDatos()

    End Sub
End Class
