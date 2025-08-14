create database sebasTiendaMovil
go
use sebasTiendaMovil
go
Alter authorization on database::sebasTiendaMovil to sa
go

set dateformat dmy
set language spanish
go


create table TipoUsuario(
idTipoUsuario int not null,
NOMBRE nvarchar(50) not null,
constraint PK_TipoUsuario primary key(idTipoUsuario)
)
go



create table Cliente(
idCliente int identity(1,1) not null,
idTipoUsuario int not null,
NOMBRE nvarchar(max)not null,
APELLIDOS nvarchar(max)not null,
CORREO_ELECTRONICO nvarchar(max)not null,
[PASSWORD] nvarchar(max)not null,
NUMERO_VERIFICACION nvarchar(6)null,
constraint PK_Cliente primary key (idCliente),
constraint FK_Cliente_TipoUsuario foreign key (idTipoUsuario)
references Cliente (idCliente)
)
go


-----------------------------------------
create table Domicilio(
idDomicilio int identity(1,1) not null,
idCliente int not null,
DIRECCION nvarchar(max)not null,
constraint PK_Domicilio primary key (idDomicilio),
constraint FK_Domicilio_Cliente foreign key (idCliente)
references Cliente (idCliente)
)
go



create table Pedido(
CODIGO_PEDIDO nvarchar(6) not null,
idCliente int not null,
ESTADO int not null,
TOTAL numeric(10)not null,
DIRECCION nvarchar(max)not null,
constraint PK_Pedido primary key (CODIGO_PEDIDO),
constraint FK_Pedido_Cliente foreign key (idCliente)
references Cliente (idCliente)
)
go



create table Proveedor(
idProveedor int identity(1,1) not null,
NOMBRE nvarchar(max)not null,
TELEFONO nvarchar(max)not null,
constraint PK_Proveedor primary key (idProveedor)
)
go



create table Categoria(
idCategoria int identity(1,1) not null,
NOMBRE nvarchar(max)not null,
constraint PK_Categoria primary key (idCategoria)
)
go



create table Descuento(
idDescuento int identity(1,1) not null,
DESCUENTO int not null,
FECHA_INICIO datetime not null,
FECHA_FIN datetime not null,
constraint PK_Descuento primary key (idDescuento)
)
go



create table Producto(
idProducto int identity(1,1) not null,
CODIGO nvarchar(9) not null,
NOMBRE nvarchar(max)not null,
DESCRIPCION nvarchar(max)not null,
STOCK int not null,
PRECIO numeric(10)not null,
idProveedor int not null,
idCategoria int not null,
idDescuento int not null,
constraint PK_Producto primary key (idProducto),
constraint UK_ unique (CODIGO),
constraint FK_Producto_Proveedor foreign key (idProveedor)
references Proveedor (idProveedor),
constraint FK_Producto_Categoria foreign key (idCategoria)
references Categoria (idCategoria),
constraint FK_Producto_Descuento foreign key (idDescuento)
references Descuento (idDescuento)
)
go

--drop table Imagen


create table Imagen(
idImagen int identity(1,1) not null,
idProducto int not null,
IMAGEN_NOMBRE varchar(max) not null,
IMAGENURL varbinary(max) not null,
constraint PK_Imagen primary key (idImagen),
constraint FK_Imagen_Producto foreign key (idProducto)
references Producto (idProducto)
)
go


-------------------------------------
--------------------------------------
create table Carrito(
idCarrito int identity(1,1) not null,
idCliente int not null,
idProducto int not null,
CODIGO nvarchar(9) not null,
CANTIDAD int not null,
constraint PK_Carrito primary key (idCarrito),
constraint FK_Carrito_Cliente foreign key (idCliente)
references Cliente (idCliente),
constraint FK_Carrito_Producto foreign key (idProducto)
references Producto (idProducto),
)
go

create table DetallePedido(
idDetallePedido int identity(1,1) not null,
CODIGO_PEDIDO nvarchar(6) not null,
idProducto int not null,
CANTIDAD int not null,
PRECIO numeric(10) not null,
constraint PK_DetallePedido primary key (idDetallePedido),
constraint FK_DetallePedido_Pedido foreign key (CODIGO_PEDIDO)
references Pedido (CODIGO_PEDIDO),
constraint FK_DetallePedido_Producto foreign key (idProducto)
references Producto (idProducto),
)
go




--procedures
-- VOLVER A CORRER ESTE
CREATE PROCEDURE [dbo].[SP_REGISTRARSE]
(
@NOMBRE nvarchar(max),
@APELLIDOS nvarchar(max),
@CORREO_ELECTRONICO nvarchar(max),
@PASSWORD nvarchar(max),
@TIPO_USUARIO int,
	
	

	 @IDCLIENTE INT OUTPUT, --IDRETURN pero con otro nombre
  @NOMBRE_RETURN NVARCHAR(max) OUTPUT,
  @APELLIDOS_RETURN NVARCHAR(max) OUTPUT,
  @TIPO_USUARIO_RETURN int OUTPUT,

	@ERRORID int output,
	@ERRORDESCRIPCION nvarchar(max) output
)
AS
BEGIN
 SET @IDCLIENTE  = 0; --IDRETURN pero con otro nombre
 SET @NOMBRE_RETURN = @NOMBRE;
 SET @APELLIDOS_RETURN = @APELLIDOS;
 SET @TIPO_USUARIO_RETURN = @TIPO_USUARIO;
	BEGIN TRY
	IF EXISTS (SELECT * FROM Cliente WHERE CORREO_ELECTRONICO =@CORREO_ELECTRONICO) -- el correo est  registrada?
		-- Si, el correo si est  registrada. Devolver error.
		BEGIN
			SET @IDCLIENTE = -1;
			SET @ERRORID = 2; --correo ya registrada
			SET @ERRORDESCRIPCION = 'ERROR DESDE BD: CORREO YA REGISTRADO';
		END
	ELSE
		-- No,  el correo no est  registrada.
		-- TODO BIEN! El correo no est  registrados
				BEGIN
					
					INSERT INTO Cliente 
					(
						[NOMBRE],
						[APELLIDOS],
						[CORREO_ELECTRONICO],
						[PASSWORD],
						[idTipoUsuario]
					)
				VALUES
					(
						@NOMBRE,
						@APELLIDOS,
						@CORREO_ELECTRONICO,
						@PASSWORD,
						1
					);

					set @IDCLIENTE = scope_identity();
			END
		

	END TRY
	
	BEGIN CATCH
		set @IDCLIENTE = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
		
	END CATCH
END
GO


CREATE PROCEDURE [dbo].[SP_INGRESAR_CATEGORIA]
(
@NOMBRE nvarchar(max),

	@IDRETURN int output,
	@ERRORID int output,
	@ERRORDESCRIPCION nvarchar(max) output
)
AS
BEGIN
	BEGIN TRY
	IF EXISTS (SELECT * FROM Categoria WHERE NOMBRE = @NOMBRE) -- La categoria ya esta registrada?
		-- Si, la categoria si est  registrada. Devolver error.
		BEGIN
			SET @IDRETURN = -1;
			SET @ERRORID = 3; --Categoria ya registrada
			SET @ERRORDESCRIPCION = 'ERROR DESDE BD: CATEGORIA YA REGISTRADA';
		END
	ELSE
		-- No, la el correo no est  registrada.
		-- TODO BIEN! El correo no est  registrados
				BEGIN
					
					INSERT INTO Categoria 
					(
						[NOMBRE]
					)
				VALUES
					(
						@NOMBRE
					);

					set @idReturn = scope_identity();
			END
		

	END TRY
	
	BEGIN CATCH
		set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
		
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[SP_INGRESAR_PROVEEDOR]
(
@NOMBRE nvarchar(max),
@TELEFONO nvarchar(MAX),

	@IDRETURN int output,
	@ERRORID int output,
	@ERRORDESCRIPCION nvarchar(max) output
)
AS
BEGIN
	BEGIN TRY
	IF EXISTS (SELECT * FROM Proveedor WHERE NOMBRE = @NOMBRE) -- El proveedor ya esta registrada?
		-- Si, el proveedor si est  registrado. Devolver error.
		BEGIN
			SET @IDRETURN = -1;
			SET @ERRORID = 4; --Proveedor ya registrado
			SET @ERRORDESCRIPCION = 'ERROR DESDE BD: PROVEEDOR YA REGISTRADO';
		END
	ELSE
		-- No, la el correo no est  registrada.
		-- TODO BIEN! El correo no est  registrados
				BEGIN
					
					INSERT INTO Proveedor 
					(
						[NOMBRE],
						[TELEFONO]
					)
				VALUES
					(
						@NOMBRE,
						@TELEFONO
					);

					set @idReturn = scope_identity();
			END
		

	END TRY
	
	BEGIN CATCH
		set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
		
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[SP_INGRESAR_DESCUENTO]
(
@DESCUENTO int,
@FECHA_INICIO datetime,
@FECHA_FIN datetime,

	@IDRETURN int output,
	@ERRORID int output,
	@ERRORDESCRIPCION nvarchar(max) output
)
AS
BEGIN
	BEGIN TRY
	IF EXISTS (SELECT * FROM Descuento WHERE DESCUENTO = @DESCUENTO) -- El Descuento ya esta registrada?
		-- Si, el Descuento si est  registrado. Devolver error.
		BEGIN
			SET @IDRETURN = -1;
			SET @ERRORID = 5; --Descuento ya registrado
			SET @ERRORDESCRIPCION = 'ERROR DESDE BD: DESCUENTO YA REGISTRADO';
		END
	ELSE
		-- No, la el Descuento no est  registrada.
		-- TODO BIEN! El Descuento no est  registrados
				BEGIN
					
					INSERT INTO Descuento 
					(
						[DESCUENTO],
						[FECHA_INICIO],
						[FECHA_FIN]
					)
				VALUES
					(
						@DESCUENTO,
						@FECHA_INICIO,
						@FECHA_FIN 
					);

					set @idReturn = scope_identity();
			END
		

	END TRY
	
	BEGIN CATCH
		set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
		
	END CATCH
END
GO


CREATE PROCEDURE [dbo].[SP_INGRESAR_IMAGEN](
@IDPRODUCTO nvarchar(max),
@IMAGEN_NOMBRE  varchar(max),
@IMAGENURL varbinary(max),
@IDRETURN int output,
@ERRORID int output,
@ERRORDESCRIPCION nvarchar(max) output
)
AS 
BEGIN
	BEGIN TRY
	IF NOT EXISTS (SELECT * FROM Producto WHERE idProducto = @IDPRODUCTO)
	--Si el Producto no existe. Devolver error.
		BEGIN 
		SET @IDRETURN = -1;
		SET @ERRORID = 1; --El producto no existe
		SET @ERRORDESCRIPCION = 'ERROR DESDE BD: EL PRODUCTO NO EXISTE';
		END
		ELSE
		--El producto existe 
		BEGIN
			
			INSERT INTO Imagen
			(
			[idProducto],
			[IMAGEN_NOMBRE],
			[IMAGENURL]
			)
			VALUES
			(
			@IDPRODUCTO,
			@IMAGEN_NOMBRE,
			@IMAGENURL
			);

			set @idReturn = scope_identity();

		END


	END TRY

	BEGIN CATCH
		set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[SP_INGRESAR_PRODUCTO](
@CODIGO nvarchar(9),
@NOMBRE nvarchar(max),
@DESCRIPCION nvarchar(max),
@STOCK int,
@PRECIO numeric(10),
@NOMBRE_PROVEEDOR nvarchar(max),
@NOMBRE_CATEGORIA  nvarchar(max),

@IDRETURN int output,
@ERRORID int output,
@ERRORDESCRIPCION nvarchar(max) output
)
AS 
BEGIN

DECLARE @IDPROVEEDOR INT;
DECLARE @IDCATEGORIA INT;


	BEGIN TRY
	IF NOT EXISTS (SELECT * FROM Proveedor WHERE NOMBRE = @NOMBRE_PROVEEDOR)
	--Si el Proveedor no existe. Devolver error.
		BEGIN 
		SET @IDRETURN = -1;
		SET @ERRORID = 7; --El producto no existe
		SET @ERRORDESCRIPCION = 'ERROR DESDE BD: EL PROVEEDOR NO EXISTE';
		END
	ELSE IF NOT EXISTS (SELECT * FROM Categoria WHERE NOMBRE = @NOMBRE_CATEGORIA)
		BEGIN 
		SET @IDRETURN = -1;
		SET @ERRORID = 6; --La categoria no existe
		SET @ERRORDESCRIPCION = 'ERROR DESDE BD: LA CATEGORIA NO EXISTE';
		END
	ELSE IF EXISTS (SELECT * FROM Producto WHERE CODIGO = @CODIGO)
		BEGIN 
		SET @IDRETURN = -1;
		SET @ERRORID = 6; --EL CODIGO DEL PRODUCTO YA EXISTE
		SET @ERRORDESCRIPCION = 'ERROR DESDE BD: EL CODIGO DEL PRODUCTO YA EXISTE';
		END
	ELSE 

		--El producto existe 
		BEGIN
			SELECT @IDPROVEEDOR = idProveedor FROM Proveedor WHERE NOMBRE = @NOMBRE_PROVEEDOR;
			
			SELECT @IDCATEGORIA = idCategoria FROM Categoria WHERE NOMBRE = @NOMBRE_CATEGORIA;

			INSERT INTO Producto
			(
			[CODIGO],
			[NOMBRE],
			[DESCRIPCION],
			[STOCK],
			[PRECIO],
			[idProveedor],
			[idCategoria],
			[idDescuento]
			)
			VALUES
			(
			@CODIGO,
			@NOMBRE,
			@DESCRIPCION,
			@STOCK,
			@PRECIO,
			@IDPROVEEDOR,
			@IDCATEGORIA,
			1
			);

			set @idReturn = scope_identity();

		END


	END TRY

	BEGIN CATCH
		set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[SP_INGRESAR_DOMICILIO](
@IDCLIENTE int,
@DIRECCION nvarchar(max),

@IDRETURN int output,
@ERRORID int output,
@ERRORDESCRIPCION nvarchar(max) output
)
AS 
BEGIN

	BEGIN TRY
	IF NOT EXISTS (SELECT * FROM Cliente WHERE idCliente = @IDCLIENTE)
	--Si el cliente no existe. Devolver error.
		BEGIN 
		SET @IDRETURN = -1;
		SET @ERRORID = 9; --El cliente no existe
		SET @ERRORDESCRIPCION = 'ERROR DESDE BD: EL CLIENTE NO EXISTE';
		END
		ELSE
		--El cliente existe 
		BEGIN
			

			INSERT INTO Domicilio
			(
			[idCliente],
			[DIRECCION]
			)
			VALUES
			(
			@IDCLIENTE,
			@DIRECCION
			);

			set @idReturn = scope_identity();

		END


	END TRY

	BEGIN CATCH
		set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[SP_INGRESAR_CARRITO](
@IDCLIENTE int,
@IDPRODUCTO int,
@CANTIDAD int,

@IDRETURN int output,
@ERRORID int output,
@ERRORDESCRIPCION nvarchar(max) output
)
AS 
DECLARE @CODIGO NVARCHAR(9) = '';
BEGIN
  
	BEGIN TRY
	
		IF @CANTIDAD > (SELECT STOCK FROM Producto WHERE idProducto = @IDPRODUCTO)
	
		BEGIN 
		SET @IDRETURN = -1;
		SET @ERRORID = 11; --No hay stock del producto
		SET @ERRORDESCRIPCION = 'ERROR DESDE BD: NO HAY STOCK DEL PRODUCTO';
		END
		ELSE
		 
		BEGIN

		SELECT @CODIGO = CODIGO FROM Producto where idProducto = @IDPRODUCTO

		
		INSERT INTO Carrito(
		[idCliente],
		[idProducto],
		[CODIGO],
		[CANTIDAD]
		)VALUES(
		@IDCLIENTE,
		@IDPRODUCTO,
		@CODIGO,
		@CANTIDAD
		)
			set @idReturn = scope_identity();
		END
		


	END TRY

	BEGIN CATCH
		set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
	END CATCH
END
GO



CREATE PROCEDURE [dbo].[SP_INGRESAR_PEDIDO]( 
@CODIGO_PEDIDO nvarchar(6),
@IDCLIENTE int,
@TOTAL numeric(10),
@DIRECCION nvarchar(max),

@IDRETURN int output,
@CODIGO_PEDIDO_RETURN nvarchar(6),
@ERRORID int output,
@ERRORDESCRIPCION nvarchar(max) output
)
AS 
BEGIN

	BEGIN TRY
	
		IF EXISTS (SELECT * FROM Pedido WHERE CODIGO_PEDIDO = @CODIGO_PEDIDO)
	--Si el codigo ya existe. Devolver error.
		BEGIN 
		SET @IDRETURN = -1;
		SET @ERRORID = 12; --El CODIGO PEDIDO YA EXISTENTE.
		SET @ERRORDESCRIPCION = 'ERROR DESDE BD: CODIGO PEDIDO YA EXISTENTE';
		END
		ELSE IF NOT EXISTS (SELECT * FROM Cliente WHERE idCliente = @IDCLIENTE)
	--Si el cliente no existe. Devolver error.
		BEGIN 
		SET @IDRETURN = -1;
		SET @ERRORID = 9; --El cliente no existe
		SET @ERRORDESCRIPCION = 'ERROR DESDE BD: EL CLIENTE NO EXISTE';
		END

		ELSE
		--TODO BIEN
		BEGIN
			

			INSERT INTO Pedido
			(
			[CODIGO_PEDIDO],
			[idCliente],
			[ESTADO],
			[TOTAL],
			[DIRECCION]
			)
			VALUES
			(
			@CODIGO_PEDIDO,
			@IDCLIENTE,
			1,
			@TOTAL,
			@DIRECCION
			);

			select @idReturn = idCliente from Pedido where CODIGO_PEDIDO = @CODIGO_PEDIDO;
			set @CODIGO_PEDIDO_RETURN = @CODIGO_PEDIDO;

		END


	END TRY

	BEGIN CATCH
		set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
	END CATCH
END
GO



CREATE PROCEDURE [dbo].[SP_LOGIN](
@CORREO_ELECTRONICO nvarchar(max),
@PASSWORD nvarchar(max),

  @IDCLIENTE INT OUTPUT, --IDRETURN pero con otro nombre
  @NOMBRE NVARCHAR(max) OUTPUT,
  @APELLIDOS NVARCHAR(max) OUTPUT,
  @TIPO_USUARIO int OUTPUT,

  @ERRORID int OUTPUT,
  @ERRORDESCRIPCION nvarchar(max) OUTPUT

)AS
BEGIN

	SET @IDCLIENTE = 0;
	SET @NOMBRE = '';
	SET @APELLIDOS = '';
	SET @TIPO_USUARIO = 0;
	
		IF NOT EXISTS(SELECT * FROM Cliente WHERE CORREO_ELECTRONICO = @CORREO_ELECTRONICO)
			BEGIN
			SET @IDCLIENTE = -1; --aca cumple con la misma funcion
			SET @ERRORID = 9;
			SET @ERRORDESCRIPCION = 'El cliente no existe';
			END
		ELSE IF NOT EXISTS(SELECT * FROM Cliente WHERE CORREO_ELECTRONICO = @CORREO_ELECTRONICO AND [PASSWORD] = @PASSWORD)
			BEGIN
			SET @IDCLIENTE = -1;
			SET @ERRORID = 13;
			SET @ERRORDESCRIPCION = 'PASSWORD INCORRECTA';
			END

		ELSE
			BEGIN

			SELECT @IDCLIENTE = C.idCliente , @NOMBRE = C.NOMBRE , @APELLIDOS = C.APELLIDOS, @TIPO_USUARIO = C.idTipoUsuario 
			FROM Cliente C
			WHERE CORREO_ELECTRONICO = @CORREO_ELECTRONICO AND [PASSWORD] = @PASSWORD
	
			END
END
GO


CREATE PROCEDURE [dbo].[SP_OBTENER_DOMICILIO_CLIENTE]
@IDCLIENTE int
AS
BEGIN
   
		SELECT 
			idDomicilio,
			DIRECCION
		FROM [Domicilio]
		WHERE idCliente = @IDCLIENTE
		
END
GO
--EMPEZAR AQUI
CREATE PROCEDURE [dbo].[SP_OBTENER_PROVEEDORES]
AS
BEGIN
		SELECT 
		idProveedor,
		NOMBRE,
		TELEFONO
		FROM Proveedor
END
GO

CREATE PROCEDURE [dbo].[SP_OBTENER_IMAGENES_PRODUCTO]
@IDPRODUCTO int
AS
BEGIN
		SELECT 
		idImagen,
		idProducto,
		IMAGENURL,
		IMAGEN_NOMBRE
		FROM Imagen
		WHERE idProducto = @IDPRODUCTO
END
GO

CREATE PROCEDURE [dbo].[SP_OBTENER_CARRITOS]
@IDCLIENTE int
AS
BEGIN
		SELECT 
		C.idCarrito,
		C.idCliente,
		C.idProducto,
		C.CODIGO,
		P.NOMBRE,
		C.CANTIDAD
		FROM Carrito C
		INNER JOIN Producto P ON C.idProducto = P.idProducto
		WHERE C.idCliente = @IDCLIENTE;
END
GO
 

CREATE PROCEDURE [dbo].[SP_OBTENER_CATEGORIAS]
AS
BEGIN
		SELECT 
		idCategoria,
		NOMBRE
		FROM Categoria
END
GO

CREATE PROCEDURE [dbo].[SP_OBTENER_DESCUENTOS]
AS
BEGIN
		SELECT 
		idDescuento,
		FECHA_INICIO,
		FECHA_FIN
		FROM Descuento
END
GO

CREATE PROCEDURE [dbo].[SP_OBTENER_PRODUCTOS]
AS
BEGIN
		SELECT 
		P.idProducto,
		P.CODIGO,
		P.NOMBRE,
		P.DESCRIPCION,
		P.STOCK,
		P.PRECIO,
		R.NOMBRE AS NOMBRE_PROVEEDOR,
		C.NOMBRE AS NOMBRE_CATEGORIA,
		D.DESCUENTO
		FROM Producto P
		INNER JOIN Proveedor R ON P.idProveedor = R.idProveedor
		INNER JOIN Categoria C ON P.idCategoria = C.idCategoria
		INNER JOIN Descuento D on P.idDescuento = D.idDescuento
END
go

CREATE PROCEDURE [dbo].[SP_OBTENER_PEDIDOS]
AS
BEGIN
		SELECT 
		P.idCliente,
		P.CODIGO_PEDIDO,
		P.ESTADO,
		P.TOTAL,
		P.DIRECCION
		FROM Pedido P
END
GO

CREATE PROCEDURE [dbo].[SP_OBTENER_DETALLES_PEDIDO]
@CODIGO_PEDIDO nvarchar(9)
AS
BEGIN
		SELECT 
		D.CANTIDAD,
		D.PRECIO,
		P.NOMBRE,
		P.DESCRIPCION
		FROM DetallePedido D
		INNER JOIN Producto P ON D.idProducto = P.idProducto
		WHERE D.CODIGO_PEDIDO = @CODIGO_PEDIDO
END
GO


CREATE PROCEDURE [dbo].[SP_SELECCIONAR_PRODUCTOS]
@CODIGO NVARCHAR(9),

@IDPRODUCTO_RETURN INT OUTPUT,
@CODIGO_RETURN NVARCHAR(9) OUTPUT,
@NOMBRE_PRODUCTO NVARCHAR(MAX) OUTPUT,
@DESCRIPCION NVARCHAR(MAX) OUTPUT,
@STOCK INT OUTPUT,
@PRECIO DECIMAL OUTPUT,
@NOMBRE_PROVEEDOR NVARCHAR(MAX) OUTPUT,
@NOMBRE_CATEGORIA NVARCHAR(MAX) OUTPUT,
@DESCUENTO INT OUTPUT,

@ERRORID int OUTPUT,
@ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN

SET @IDPRODUCTO_RETURN = 0;
SET @CODIGO_RETURN = '';
SET @NOMBRE_PRODUCTO = '';
SET @DESCRIPCION  = '';
SET @STOCK = 0;
SET @PRECIO = 0;
SET @NOMBRE_PROVEEDOR = '';
SET @NOMBRE_CATEGORIA ='';
SET @DESCUENTO = 0;

		IF NOT EXISTS(SELECT * FROM Producto WHERE CODIGO = @CODIGO)
			BEGIN
			SET @IDPRODUCTO_RETURN = -1; --aca cumple con la misma funcion
			SET @ERRORID = 14;
			SET @ERRORDESCRIPCION = 'CODIGO PRODUCTO NO EXISTE';
			END

		ELSE
			BEGIN

			SELECT 
			@IDPRODUCTO_RETURN = P.idProducto,
			@CODIGO_RETURN = P.CODIGO,
			@NOMBRE_PRODUCTO = P.NOMBRE,
			@DESCRIPCION = P.DESCRIPCION,
			@STOCK = P.STOCK,
			@PRECIO = P.PRECIO,
			@NOMBRE_PROVEEDOR = R.NOMBRE,
			@NOMBRE_CATEGORIA = C.NOMBRE,
			@DESCUENTO = D.DESCUENTO
			FROM Producto P
			INNER JOIN Proveedor R ON P.idProveedor = R.idProveedor
			INNER JOIN Categoria C ON P.idCategoria = C.idCategoria
			INNER JOIN Descuento D on P.idDescuento = D.idDescuento
			WHERE CODIGO = @CODIGO
	
			END
END
GO

-- sirve para validar 1 x 1 los porductos del pedido
CREATE PROCEDURE [dbo].[SP_SELECCIONAR_PRODUCTO_CARRITOS]
@IDPRODUCTO INT,

@IDPRODUCTO_RETURN INT OUTPUT,
@NOMBRE_PRODUCTO NVARCHAR(MAX) OUTPUT,
@STOCK INT OUTPUT,
@PRECIO DECIMAL OUTPUT,
@DESCUENTO INT OUTPUT,

@ERRORID int OUTPUT,
@ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN

SET @IDPRODUCTO_RETURN = 0;
SET @NOMBRE_PRODUCTO = '';
SET @STOCK = 0;
SET @PRECIO = 0;
SET @DESCUENTO = 0;

		IF NOT EXISTS(SELECT * FROM Producto WHERE idProducto = @IDPRODUCTO)
			BEGIN
			SET @IDPRODUCTO_RETURN = -1; --aca cumple con la misma funcion
			SET @ERRORID = 15;
			SET @ERRORDESCRIPCION = 'PRODUCTO NO EXISTE';
			END

		ELSE
			BEGIN

			SELECT 
			@IDPRODUCTO_RETURN = P.idProducto,
			@NOMBRE_PRODUCTO = P.NOMBRE,
			@STOCK = P.STOCK,
			@PRECIO = P.PRECIO,
			@DESCUENTO = D.DESCUENTO
			FROM Producto P
			INNER JOIN Descuento D on P.idDescuento = D.idDescuento
			WHERE idProducto = @IDPRODUCTO
	
			END
END
GO

--nuevos procedure mas dure q proce
CREATE PROCEDURE [dbo].[SP_ELIMINAR_CARRITO]
@IDCARRITO INT,

@IDCARRITO_RETURN INT OUTPUT,


@ERRORID int OUTPUT,
@ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN

SET @IDCARRITO_RETURN = 0;


		IF NOT EXISTS(SELECT * FROM Carrito WHERE idCarrito = @IDCARRITO)
			BEGIN
			SET @IDCARRITO_RETURN = -1; -- carrito no existe
			SET @ERRORID = 16;
			SET @ERRORDESCRIPCION = 'CARRITO NO EXITE';
			END

		ELSE
			BEGIN

			delete
			FROM Carrito
			WHERE idCarrito = @IDCARRITO
	
			SET @IDCARRITO_RETURN = 1

			END
END
GO

CREATE PROCEDURE [dbo].[SP_OBTENER_PRODUCTOS_SEGUN_CATEGORIA]
@IDCATEGORIA INT,

@IDRETURN INT OUTPUT,
@ERRORID INT OUTPUT,
@ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
SET @IDRETURN = 0;

		IF NOT EXISTS(SELECT * FROM Categoria WHERE idCategoria = @IDCATEGORIA)
			BEGIN
			SET @IDRETURN = -1; -- Categoria no existe
			SET @ERRORID = 6;
			SET @ERRORDESCRIPCION = 'CATEGORIA NO EXITE';
			END

		ELSE
		BEGIN
			SELECT 
			P.idProducto,
			P.CODIGO,
			P.NOMBRE,
			P.DESCRIPCION,
			P.STOCK,
			P.PRECIO,
			R.NOMBRE AS NOMBRE_PROVEEDOR,
			C.NOMBRE AS NOMBRE_CATEGORIA,
			D.DESCUENTO
			FROM Producto P
			INNER JOIN Proveedor R ON P.idProveedor = R.idProveedor
			INNER JOIN Categoria C ON P.idCategoria = C.idCategoria
			INNER JOIN Descuento D on P.idDescuento = D.idDescuento
			WHERE P.idCategoria = @IDCATEGORIA

			SET @IDRETURN = @IDCATEGORIA;

		END


END
go



CREATE PROCEDURE [dbo].[SP_AGREGAR_CODIGO_CLIENTE]
@IDCLIENTE INT,
@NUMERO_VERIFICACION nvarchar(6),

@IDCLIENTE_RETURN INT OUTPUT,


@ERRORID int OUTPUT,
@ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN

SET @IDCLIENTE_RETURN = 0;

	BEGIN TRY

		IF NOT EXISTS(SELECT * FROM Cliente WHERE idCliente = @IDCLIENTE)
			BEGIN
			SET @IDCLIENTE_RETURN = -1; -- carrito no existe
			SET @ERRORID = 9;
			SET @ERRORDESCRIPCION = 'CLIENTE NO EXITE';
			END

		ELSE
			BEGIN

			UPDATE Cliente
			SET NUMERO_VERIFICACION = @NUMERO_VERIFICACION
			WHERE idCliente = @IDCLIENTE
	
			set @IDCLIENTE_RETURN = @IDCLIENTE;

			END
	END TRY 
	
	BEGIN CATCH
		set @IDCLIENTE_RETURN = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
	END CATCH
END
GO


--AGREGUE ESTOS


CREATE PROCEDURE [dbo].[SP_OBTENER_PEDIDOS_CLIENTE]
@IdCliente int
AS
BEGIN
		SELECT 
		P.idCliente,
		P.CODIGO_PEDIDO,
		P.ESTADO,
		P.TOTAL,
		P.DIRECCION
		FROM Pedido P
		WHERE idCliente = @IdCliente
END
GO



CREATE PROCEDURE [dbo].[SP_OBTENER_DETALLE_PEDIDOS_CLIENTE]
@CodigoPedido NVARCHAR(6)
AS
BEGIN
		SELECT 
		D.idDetallePedido,
		D.CODIGO_PEDIDO,
		D.idProducto,
		P.NOMBRE,
		D.CANTIDAD,
		D.PRECIO
		FROM DetallePedido D
		INNER JOIN Producto P ON  D.idProducto = p.idProducto
		WHERE CODIGO_PEDIDO = @CodigoPedido
END
GO

drop PROCEDURE [dbo].[SP_OBTENER_CLIENTES]
go

CREATE PROCEDURE [dbo].[SP_OBTENER_CLIENTE]
@IdCliente INT,

@CORREO_RETURN NVARCHAR(max) OUTPUT
AS
BEGIN
   
        -- Consultar todos los registros de la tabla Cliente
        SELECT 
			@CORREO_RETURN = CORREO_ELECTRONICO
        FROM 
            [Cliente] 
			WHERE idCliente = @IdCliente;
			
END
GO

--HASTA AQUI





--somos
CREATE TRIGGER TR_AfterInsertPedido
ON Pedido
AFTER INSERT
AS
BEGIN
    DECLARE @IDCLIENTE int ;
    DECLARE @CODIGO_PEDIDO nvarchar(6);
	DECLARE @IDPRODUCTO int;
	DECLARE @PRECIO DECIMAL(10);
	DECLARE @DESCUENTO int;
	DECLARE @PRECIO_FINAL int;
	DECLARE @CANTIDAD int
    -- Obtener el idCliente y idPedido del nuevo pedido insertado
    SELECT @IDCLIENTE = idCliente, @CODIGO_PEDIDO = CODIGO_PEDIDO FROM inserted;

	DECLARE CarritoCursor CURSOR FOR
    SELECT idProducto, CANTIDAD
    FROM Carrito
    WHERE idCliente = @IDCLIENTE;

	 OPEN CarritoCursor;
    FETCH NEXT FROM CarritoCursor INTO @IDPRODUCTO, @CANTIDAD;

	 WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Obtener el precio y descuento del producto actual
        SELECT 
            @PRECIO = P.PRECIO,
            @DESCUENTO = D.DESCUENTO
        FROM 
            Producto P
        INNER JOIN 
            Descuento D ON P.idDescuento = D.idDescuento
        WHERE 
            P.idProducto = @IDPRODUCTO;

        -- Calcular el precio final con el descuento aplicado
        SET @PRECIO_FINAL = (@PRECIO - (@PRECIO * (@DESCUENTO / 100.0))) * @CANTIDAD;

        -- Insertar los detalles del pedido en DetallePedido
        INSERT INTO DetallePedido (CODIGO_PEDIDO, idProducto, CANTIDAD, PRECIO)
        VALUES (@CODIGO_PEDIDO, @IDPRODUCTO, @CANTIDAD, @PRECIO_FINAL);
		
		

		--restar al stock del producto la cantidad vendida
		UPDATE Producto
		SET STOCK = STOCK - @CANTIDAD
		WHERE idProducto = @idProducto;



        -- Obtener el siguiente producto del carrito
        FETCH NEXT FROM CarritoCursor INTO @IDPRODUCTO, @CANTIDAD;
    END;

	    CLOSE CarritoCursor;
    DEALLOCATE CarritoCursor;

        -- Borrar todos los carritos del cliente despu s de la inserci n en DetallePedido
        DELETE FROM Carrito WHERE idCliente = @IDCLIENTE;
    
END
GO


Insert into TipoUsuario(idTipoUsuario,NOMBRE)
Values(1,'Inactivo'),(2,'Activo'),(3,'Admin')
go

insert into Categoria (NOMBRE)
values ('moda'),('emos'),('termos')
go

insert into Proveedor (NOMBRE,TELEFONO)
values ('Teletica','88888888'),('Metalco','88884444')
go

INSERT INTO Descuento (DESCUENTO, FECHA_INICIO, FECHA_FIN) 
VALUES 
    (0, '01-01-2024', '31-12-2024'),
    (10, '01-01-2024', '31-12-2024'),
    (20, '01-01-2024', '31-12-2024'),
    (30, '01-01-2024', '31-12-2024'),
    (40, '01-01-2024', '31-12-2024'),
    (50, '01-01-2024', '31-12-2024'),
    (60, '01-01-2024', '31-12-2024'),
    (70, '01-01-2024', '31-12-2024'),
    (80, '01-01-2024', '31-12-2024'),
    (90, '01-01-2024', '31-12-2024'),
    (100, '01-01-2024', '31-12-2024');
	go


	
	INSERT INTO Producto (CODIGO, NOMBRE, DESCRIPCION, STOCK, PRECIO, idProveedor, idCategoria, idDescuento) 
VALUES 
    ('PROD0001', 'Producto A', 'Descripción del Producto A', 50, 100, 1, 1, 5);
	go
INSERT INTO Producto (CODIGO, NOMBRE, DESCRIPCION, STOCK, PRECIO, idProveedor, idCategoria, idDescuento) 
VALUES 
    ('PROD0002', 'Producto B', 'Descripción del Producto B', 30, 150, 2, 2, 2);
	go

	INSERT INTO Imagen (idProducto, IMAGEN_NOMBRE, IMAGENURL) 
VALUES 
    (2, 'producto1_imagen', 0x --aqui va tu imagen
	);
	go

	select * from Cliente
	delete from cliente where CORREO_ELECTRONICO = 'gacoro30@gmail.com'
	select * from DetallePedido
	select * from DetallePedido where CODIGO_PEDIDO ='c8qjmn'