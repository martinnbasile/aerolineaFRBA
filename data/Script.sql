Use GD2C2015
go
create schema MM
go

Create Procedure MM.limpiarBase as
drop Procedure MM.CancelarAeronaveFueraDeServicio
drop Procedure MM.BorrarCiudades
drop view MM.rolPorUsuario
drop Procedure MM.asentarMillas
drop function MM.movimientosMillas
drop Procedure MM.asentarLLegadaAeronave
drop Procedure MM.Loggear
drop Procedure MM.CancelarAeronaveVidaUtil
drop function MM.devuelveIDD
drop function MM.fechaDeHoy
drop function MM.cantidadMillas
drop function MM.devuelveRutaaa
drop Procedure MM.aeronavesSustitutas
drop function MM.devuelveTipoServicio
drop function MM.devuelveNumeroCliente
drop function MM.convertirFecha
drop Procedure MM.actualizarFecha
drop function MM.devuelveViaje3
drop view MM.funcionalidadPorRol
drop view MM.vista_rutas_aereas
drop view MM.vista_aeronaves
Drop table MM.Fecha
Drop table MM.Butacas
Drop table MM.Cambios_Millas
Drop table MM.Cancelaciones
Drop table MM.Intentos_Fallidos
Drop table MM.Inhabilitados
Drop table MM.Millas
Drop table MM.Paquetes
Drop table MM.Pasajes
Drop table MM.Roles_Funcionalidades
Drop table MM.Tarjetas_Credito
Drop table MM.Productos_Milla
Drop table MM.Funcionalidades
Drop table MM.Intentos_login
Drop table MM.KG
Drop table MM.Usuario_rol
Drop table MM.Viajes
Drop table MM.Rutas_Aereas
Drop table MM.Aeronaves 
Drop table MM.Tipos_Servicio 
Drop table MM.Fabricantes 
Drop table MM.Ciudades 
Drop table MM.tarjetas_Credito
Drop table MM.Clientes 
Drop table MM.Usuarios 
Drop table MM.Roles 
go



create Table MM.Ciudades(
Id int identity(1,1) primary key,
Descripcion varchar(70) not null unique)
go
alter table MM.Ciudades
add Estado varchar(50) default 'Habilitado'
go

create Table MM.Fabricantes(
Id int identity(1,1) primary key,
Descripcion varchar(70) not null unique)
go
create Table MM.Tipos_Servicio(
Id int identity(1,1) primary key,
Descripcion varchar (70) not null unique,
Porcentaje float not null)
go
create Table MM.Funcionalidades(
Id int identity(1,1) primary key,
Descripcion varchar(70) not null unique)
go
create Table MM.Roles(
Id int identity(1,1) primary key,
Descripcion varchar(70) not null unique,
Funcionalidad int)
go
Alter table MM.Roles 
add constraint FK_Funcionalidad FOREIGN KEY (Funcionalidad) references MM.Funcionalidades(Id) 
go
create Table MM.Usuarios(
Id int identity(1,1) primary key,
Username varchar(50) not null unique,
Password varchar(256) not null,
Rol int,
Estado varchar(15))
go
Alter table MM.Usuarios
add constraint FK_Rol FOREIGN KEY (Rol) references MM.Roles(Id)
go
create Table MM.Intentos_Fallidos(
Id_User int,
cantidad int)
go
Alter table MM.Intentos_fallidos
add constraint FK_User FOREIGN KEY (Id_User) references MM.Usuarios(Id) 
go
create Table MM.Inhabilitados(
Id_User int)
go
Alter table MM.Inhabilitados
add constraint FK_User_Inahilitado FOREIGN KEY (Id_User) references MM.Usuarios(Id)
go
Create Table MM.Clientes(
Id int identity(1,1) primary key,
DNI numeric(20) unique not null,
Nombre varchar(40) not null,
Apellido varchar(40) not null,
Direccion varchar(60) not null,
Telefono numeric(30) not null,
Mail varchar(50) not null,
Fecha_Nacimiento smalldatetime not null,
Fecha_prox_vencimiento smalldatetime
)
go
Create Table MM.Rutas_Aereas(
Id int identity(1,1) primary key,
Ciudad_Destino int,
Ciudad_Origen int,
Tipo_Servicio int,
Precio_Base float not null,
Precio_Kg float not null)
go
Alter Table MM.Rutas_Aereas
add constraint FK_Ciudad_Origen FOREIGN KEY (Ciudad_Origen) references MM.Ciudades(Id)
go
Alter Table MM.Rutas_Aereas
add constraint FK_Ciudad_Destino FOREIGN KEY (Ciudad_Destino) references MM.Ciudades(Id)
go
Alter Table MM.Rutas_Aereas
add constraint FK_Tipo_Servicio FOREIGN KEY (Tipo_Servicio) references MM.Tipos_Servicio(Id)
go
alter table MM.Usuarios
add Pregunta_Secreta varchar(50) not null
go
alter table MM.Usuarios
add Respuesta varchar(256) not null
go
create table MM.Aeronaves(
matricula varchar(10) primary key,
Fecha_alta smalldatetime not null,
Modelo varchar(30) not null,
Fabricante int,
Tipo_Servicio int,
Baja_Fuera_Servicio varchar(2),
Baja_Vida_Util varchar(2),
Fecha_Fuera_Servicio smalldatetime,
Fecha_Reinicio_Servicio smalldatetime,
Fecha_Baja_Definitiva smalldatetime,
Cantidad_Butacas int not null,
Cantidad_Kg int not null)
go
alter table MM.Aeronaves
add constraint Fabricante foreign key (Fabricante) references MM.Fabricantes(Id)  
go
alter table MM.Aeronaves
add constraint Tipo_Servicio foreign key (Tipo_Servicio) references MM.Tipos_Servicio(Id)
go
create table MM.Butacas(
Matricula varchar(10) not null,
Numero int not null,
Tipo varchar(20) not null,
Piso int not null)
go
alter table MM.Butacas
add constraint Matricula foreign key (Matricula) references MM.Aeronaves(Matricula)
go
Create table MM.Viajes(
Id int identity(1,1) ,
Matricula varchar(10),
Ruta int)
go
alter table MM.Viajes
add constraint Id primary key (Id)
go
alter table MM.Viajes
add constraint Matricula_Avion foreign key (Matricula) references MM.Aeronaves(Matricula)
go
alter table MM.Viajes
add constraint Ruta foreign key (Ruta) references MM.Rutas_Aereas(Id)
go
alter table MM.Rutas_Aereas
add Fecha_Salida datetime not null
go
alter table MM.Rutas_Aereas
add Fecha_Estimada datetime not null
go
alter table MM.Rutas_Aereas
add Fecha_llegada datetime not null
go
create table MM.Paquetes(
Id int identity(1,1) primary key,
Viaje int,
Kg int not null,
Fecha_Compra smalldatetime,
Cliente int)
go
alter table MM.Paquetes
add constraint Viaje2 foreign key (Viaje) references MM.Viajes(Id)
go
alter table MM.Paquetes
add constraint Cliente_Paquete foreign key (Cliente) references MM.Clientes(Id)
go
Create table MM.Pasajes(
Id int identity(1,1) primary key,
Viaje int,
Numero_Butaca int not null,
Fecha_Compra smalldatetime,
Cliente int)
go
alter table MM.Pasajes
add constraint Viaje3 foreign key (Viaje) references MM.Viajes(Id)
go
alter table MM.Pasajes
add constraint Cliente_Pasaje foreign key (Cliente) references MM.Clientes(Id)
go
create table MM.Millas(
Id int identity(1,1),
Cliente int,
Millas int not null)
go
alter table MM.Millas
add constraint Cliente_Millero foreign key (Cliente) references MM.Clientes(Id)
go
alter table MM.Millas
add constraint PK primary key (Id)
go
create table MM.Productos_Milla(
Id int identity(1,1) primary key,
Descripcion varchar not null,
Cantidad int not null,
Millas_Necesarias int not null)
go
create table MM.Cambios_Millas(
Id int identity(1,1) primary key,
Cliente int,
Producto int,
Cantidad int not null,
Fecha_Canje smalldatetime default getdate() )
go
alter table MM.Cambios_Millas
add constraint Cliente_Cambiador foreign key (Cliente) references MM.Clientes(Id)
go
alter table MM.Cambios_Millas
add constraint Producto foreign key (Producto) references MM.Productos_Milla(Id)
go
create table MM.Cancelaciones(
Id int identity(1,1) primary key,
Codigo_Pasaje int,
Codigo_Encomienda int,
Fecha smalldatetime not null,
Motivo varchar(200))
go
alter table MM.Cancelaciones
add constraint Pasaje foreign key (Codigo_Pasaje) references MM.Pasajes(Id)
go
alter table MM.Cancelaciones
add constraint Encomienda foreign key (Codigo_Encomienda) references MM.Paquetes(Id)
go
create table MM.Tarjetas_Credito(
Id int identity(1,1) primary key,
Cliente int,
Numero_Tarjeta numeric(20) not null,
Codigo_Seguridad numeric(10) not null,
Fecha_Vencimiento smalldatetime not null,
Tipo_Tarjeta varchar(30) not null)
go
alter table MM.Tarjetas_Credito
add constraint Cliente_Tarjetero foreign key(Cliente) references MM.Clientes(Id)
go
drop table MM.Butacas
go
create table MM.Butacas(
Viaje int not null,
Nro int not null,
Ubicacion varchar(30))
go
create table MM.Usuario_rol(

cod_usuario int not null foreign key references MM.Usuarios,
cod_rol int not null foreign key references MM.Roles


)
go

insert into MM.Fabricantes (Descripcion)
select distinct Aeronave_Fabricante from gd_esquema.Maestra
go
insert into MM.Tipos_Servicio(Descripcion,Porcentaje) values ('Común',0)
go
insert into MM.Tipos_Servicio(Descripcion,Porcentaje) values ('Ejecutivo',5)
go
insert into MM.Tipos_Servicio(Descripcion,Porcentaje) values ('Cama',10)
go
insert into MM.Tipos_Servicio(Descripcion,Porcentaje) values ('Semi-Cama',15)
go
insert into MM.Tipos_Servicio(Descripcion,Porcentaje) values ('Premium',25)
go
insert into MM.Ciudades(Descripcion)
select distinct Ruta_Ciudad_Destino from gd_esquema.Maestra union
select distinct Ruta_Ciudad_Origen from gd_esquema.Maestra
go
create table MM.Roles_Funcionalidades(
Funcionalidad int,
Rol int)
go
alter table MM.Roles
drop constraint FK_Funcionalidad
go
alter table MM.Roles_funcionalidades
add constraint FK_Funcionalidad FOREIGN KEY(Funcionalidad) references MM.Funcionalidades
go
alter table MM.Roles
drop column Funcionalidad
go
alter table MM.Roles_Funcionalidades
add constraint FK_Roles FOREIGN KEY(Rol) references MM.Roles
go
alter table MM.Productos_Milla
drop column Descripcion
go
alter table MM.Productos_Milla
add Descripcion varchar(100) not null
go
insert into MM.Productos_Milla (Descripcion,Cantidad,Millas_Necesarias)
values ('Auto de Juguete',100,20)
go
insert into MM.Productos_Milla (Descripcion,Cantidad,Millas_Necesarias)
values ('Moto de Juguete',100,15)
go
insert into MM.Productos_Milla (Descripcion,Cantidad,Millas_Necesarias)
values ('Avion de Juguete',100,25)
go
insert into MM.Productos_Milla (Descripcion,Cantidad,Millas_Necesarias)
values ('Set de perfumes',100,200)
go

insert into MM.Funcionalidades(Descripcion) values ('ABM ROL')
go
insert into MM.Funcionalidades(Descripcion) values ('ABM CIUDADES')
go
insert into MM.Funcionalidades(Descripcion) values ('ABM RUTAS')
go
insert into MM.Funcionalidades(Descripcion) values ('ABM AERONAVES')
go
insert into MM.Funcionalidades(Descripcion) values ('GENERAR VIAJE')
go
insert into MM.Funcionalidades(Descripcion) values ('COMPRAS')
go
insert into MM.Funcionalidades(Descripcion) values ('DEVOLUCIONES Y CANCELACIONES')
go
insert into MM.Funcionalidades(Descripcion) values ('CONSULTAR MILLAS')
go
insert into MM.Funcionalidades(Descripcion) values ('CANJEAR MILLAS')
go
insert into MM.Funcionalidades(Descripcion) values ('LISTADO ESTADISTICO')
go
insert into MM.Funcionalidades(Descripcion) values ('REGISTRAR LLEGADA A DESTINO')
go
insert into MM.Roles(Descripcion) values ('Administrador')
go
insert into MM.Roles(Descripcion) values ('Cliente')
go
alter table MM.Clientes add Unique(DNI)
go
insert into MM.Clientes(DNI,Nombre,Apellido,Direccion,Telefono,Mail,Fecha_Nacimiento) 
(select distinct
Cli_Dni,Cli_Nombre,Cli_Apellido,Cli_Dir,Cli_Telefono,Cli_Mail,Cli_Fecha_Nac from gd_esquema.Maestra)
go


alter table MM.Rutas_Aereas
drop column Fecha_Salida
go
alter table MM.Rutas_Aereas
drop column Fecha_Estimada
go
alter table MM.Rutas_Aereas
drop column Fecha_llegada
go
alter table MM.Rutas_Aereas
add Estado int not null 
go
alter table MM.Viajes
add Fecha_Salida date not null
go
alter table MM.Viajes
add Fecha_Estimada_llegada date not null
go
alter table MM.Viajes
add Fecha_llegada date
go 
create table MM.KG(
Viaje int not null,
Kg int not null)
go
alter table MM.KG
add constraint FK_Viajes FOREIGN KEY (Viaje) references MM.Viajes(Id)
go
alter table MM.Butacas
add constraint FK_Viajes_2 FOREIGN KEY (Viaje) references MM.Viajes(Id)
go
insert into MM.Aeronaves
select distinct Aeronave_Matricula,'1999-01-01',Aeronave_Modelo, 
case 
	when Aeronave_Fabricante='Airbus' then 1
	when Aeronave_Fabricante='Bombardier' then 2
	when Aeronave_Fabricante='Embraer' then 3
	when Aeronave_Fabricante='Boeing' then 4
	end,
case
	when Tipo_Servicio='Cama' then 3
	when Tipo_Servicio='Común' then 1
	when Tipo_Servicio='Ejecutivo' then 2 
	when Tipo_Servicio='Semi-Cama' then 4
	when Tipo_Servicio='Premium' then 5
	end,
	'NO','NO',NULL,NULL,NULL,1250,
	Aeronave_KG_Disponibles 
from gd_esquema.Maestra
order by Aeronave_Matricula
go
Create table MM.Intentos_login(
	Id_login int identity(1,1) primary key,
	Codigo_usuario int not null,
	Es_correcto bit not null,
	foreign key (Codigo_usuario) references MM.Usuarios(Id)
)
go
alter table MM.Intentos_Fallidos
drop constraint FK_User
go
drop table MM.Intentos_Fallidos
go
Create table MM.Intentos_fallidos(
	Id_fallido int identity(1,1) primary key,
	Cod_login int not null,
	foreign key (Cod_login) references MM.Intentos_login(Id_login)
)
go
alter table MM.Inhabilitados
add fecha datetime not null
go
alter table MM.Viajes
drop column Fecha_Salida
go
alter table MM.Viajes
drop column Fecha_Estimada_llegada
go
alter table MM.Viajes
drop column Fecha_llegada
go
alter table MM.Viajes
add Fecha_salida datetime not null
go
alter table MM.Viajes
add Fecha_Estimada_llegada datetime not null
go
alter table MM.Viajes
add Fecha_llegada datetime
go
Create FUNCTION MM.devuelveIDD
(
    @descrip varchar(100)
)
RETURNS int
AS
BEGIN
    Declare @id int;
    SELECT @id =  id from MM.Ciudades 
    where Descripcion = @descrip

    RETURN  @id

END
GO
insert into MM.Rutas_Aereas
select MM.devuelveIDD( A.Ruta_Ciudad_Destino),MM.devuelveIDD( a.Ruta_Ciudad_Origen),
case
when a.Tipo_Servicio='Cama' then 3
	when a.Tipo_Servicio='Común' then 1
	when a.Tipo_Servicio='Ejecutivo' then 2 
	when a.Tipo_Servicio='Semi-Cama' then 4
	when a.Tipo_Servicio='Premium' then 5
	end
	AS Tipo
	,b.Ruta_Precio_BasePasaje,A.Ruta_Precio_BaseKG,1
from 
(select distinct Ruta_Ciudad_Destino,Ruta_Ciudad_Origen, Tipo_Servicio,Ruta_Precio_BaseKG
from gd_esquema.Maestra 
where Ruta_Precio_BaseKG != 0.00
group by Ruta_Ciudad_Destino,Ruta_Ciudad_Origen, Tipo_Servicio,Ruta_Precio_BaseKG
)A
inner join
(select distinct Ruta_Ciudad_Destino,Ruta_Ciudad_Origen, Tipo_Servicio,Ruta_Precio_BasePasaje
from gd_esquema.Maestra 
where Ruta_Precio_BasePasaje != 0.00
group by Ruta_Ciudad_Destino,Ruta_Ciudad_Origen, Tipo_Servicio,Ruta_Precio_BasePasaje)B
on a.Ruta_Ciudad_Destino=b.Ruta_Ciudad_Destino
and
a.Ruta_Ciudad_Origen=b.Ruta_Ciudad_Origen
and 
a.Tipo_Servicio=b.Tipo_Servicio
go

create FUNCTION MM.devuelveRutaaa
(
	@ciudad_origen int,
	@ciudad_destino int,
	@Tipo_Servicio int
)
RETURNS int
AS
Begin
	Declare @id int
	select @id = Id from MM.Rutas_Aereas A
	where	@ciudad_origen=A.Ciudad_Origen and
			@ciudad_destino=A.Ciudad_Destino and
			@Tipo_Servicio=A.Tipo_Servicio

			RETURN @id

END
go
create FUNCTION MM.devuelveTipoServicio(
	@Servicio varchar(50)
	)
RETURNS int
AS
Begin
	Declare @id int
	select @id = ID from MM.Tipos_Servicio
	where @Servicio=Descripcion

	RETURN @id

END
go
insert into MM.Viajes
select Aeronave_Matricula,MM.devuelveRutaaa(MM.devuelveIDD(Ruta_Ciudad_Origen),
MM.devuelveIDD(Ruta_Ciudad_Destino),MM.devuelveTipoServicio(Tipo_Servicio)),
FechaSalida,Fecha_LLegada_Estimada,FechaLlegada
from gd_esquema.Maestra
group by Aeronave_Matricula,Ruta_Ciudad_Destino,Ruta_Ciudad_Origen,
FechaSalida,FechaLLegada,Fecha_LLegada_Estimada,Tipo_Servicio
go
Create FUNCTION MM.devuelveNumeroCliente
(
    @DNI numeric(20)
)
RETURNS int
AS
BEGIN
    Declare @id int;
    SELECT @id =  Id from MM.Clientes 
    where DNI = @DNI

    RETURN  @id

END
GO
Create FUNCTION MM.devuelveViaje3
(
    @ruta int,
	@Matricula varchar(10),
	@FechaSalida varchar(100),
	@FechaEstimada varchar(100),
	@FechaLlegada varchar(100)
)
RETURNS int
AS
BEGIN
    Declare @id int;
    SELECT @id =  Id from MM.Viajes 
    where @Matricula=Matricula and
	@ruta=Ruta and @FechaSalida=Fecha_salida
	and @FechaEstimada=Fecha_Estimada_llegada and
	@Fechallegada=Fecha_llegada 

    RETURN  @id

END
GO
insert into MM.Paquetes
select distinct b.Id,a.Paquete_KG,Paquete_FechaCompra,d.Id  from gd_esquema.Maestra as a join MM.Viajes as b on b.Fecha_Estimada_llegada=a.Fecha_LLegada_Estimada 
and b.Fecha_llegada=a.FechaLLegada and b.Fecha_salida=a.FechaSalida and b.Matricula=a.Aeronave_Matricula join MM.Rutas_Aereas 
as c on b.Ruta=c.Id /*and c.Precio_Base=a.Ruta_Precio_BasePasaje*/ and c.Precio_Kg=a.Ruta_Precio_BaseKG join MM.Tipos_Servicio as 
g on  c.Tipo_Servicio=g.Id and g.Descripcion =a.Tipo_Servicio join MM.Clientes as d on d.DNI=a.Cli_Dni join MM.Ciudades as e 
on c.Ciudad_Destino=e.Id and e.Descripcion=a.Ruta_Ciudad_Destino join MM.Ciudades as f on c.Ciudad_Origen=f.Id and 
f.Descripcion=a.Ruta_Ciudad_Origen
where Paquete_KG != 0
group by b.Id,a.Paquete_KG,Paquete_fechaCompra,d.Id

go
drop table MM.KG
go
Alter table MM.Butacas
add Estado varchar(50) not null
go
insert into MM.Butacas
select  b.Id,a.Butaca_Nro,a.Butaca_Tipo,'vendida'  from gd_esquema.Maestra as a join MM.Viajes as b on 
b.Fecha_Estimada_llegada=a.Fecha_LLegada_Estimada and b.Fecha_llegada=a.FechaLLegada and b.Fecha_salida=a.FechaSalida and 
b.Matricula=a.Aeronave_Matricula join MM.Rutas_Aereas as c on b.Ruta=c.Id and c.Precio_Base=a.Ruta_Precio_BasePasaje 
/*and c.Precio_Kg=a.Ruta_Precio_BaseKG */join MM.Tipos_Servicio as g on  c.Tipo_Servicio=g.Id and g.Descripcion =a.Tipo_Servicio  
join MM.Ciudades as e on c.Ciudad_Destino=e.Id and e.Descripcion=a.Ruta_Ciudad_Destino join MM.Ciudades as f 
on c.Ciudad_Origen=f.Id and f.Descripcion=a.Ruta_Ciudad_Origen
where Pasaje_codigo <>0
group by b.Id,Butaca_Nro,a.Butaca_Tipo

go
insert into MM.Pasajes
select distinct b.Id,a.Butaca_Nro,Paquete_FechaCompra,d.Id  from gd_esquema.Maestra as a join MM.Viajes as b on 
b.Fecha_Estimada_llegada=a.Fecha_LLegada_Estimada and b.Fecha_llegada=a.FechaLLegada and b.Fecha_salida=a.FechaSalida and 
b.Matricula=a.Aeronave_Matricula join MM.Rutas_Aereas as c on b.Ruta=c.Id and c.Precio_Base=a.Ruta_Precio_BasePasaje 
/*and c.Precio_Kg=a.Ruta_Precio_BaseKG */join MM.Tipos_Servicio as g on  c.Tipo_Servicio=g.Id and g.Descripcion =a.Tipo_Servicio 
join MM.Clientes as d on d.DNI=a.Cli_Dni join MM.Ciudades as e on c.Ciudad_Destino=e.Id and e.Descripcion=a.Ruta_Ciudad_Destino 
join MM.Ciudades as f on c.Ciudad_Origen=f.Id and f.Descripcion=a.Ruta_Ciudad_Origen
where Pasaje_codigo <>0
group by b.Id,Butaca_Nro,Paquete_fechaCompra,d.Id

go



create view MM.vista_rutas_aereas as
select r.Id as 'Codigo',  c1.descripcion as 'Ciudad origen',c2.descripcion as 'Ciudad destino',t.Descripcion as 'Servicio', r.Precio_Base as 'Precio base',r.Precio_Kg as 'Precio base encomienda'
from MM.Rutas_Aereas r join MM.Ciudades c1 on (r.Ciudad_Origen=c1.Id)
					join MM.Ciudades c2 on (r.Ciudad_Destino=c2.Id)
					join MM.Tipos_Servicio t on (r.Tipo_Servicio=t.Id)
go

create view MM.vista_aeronaves as
select a.Fecha_alta as 'Fecha de alta',  a.Modelo as 'Modelo',a.matricula as 'Matrícula',f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',a.Baja_Fuera_Servicio as 'Baja por fuera de servicio',a.Baja_Vida_Util as 'Baja por vida util',a.Fecha_Fuera_Servicio as 'Fecha de fuera de servicio',a.Fecha_Reinicio_Servicio as 'Fecha de reinicio de servicio',a.Fecha_Baja_Definitiva as 'Fecha de baja definitiva',a.Cantidad_Butacas as 'Cantidad de butacas',a.Cantidad_Kg as 'Cantidad de Kgs disponibles para realizar encomiendas'
from MM.Aeronaves a join MM.Fabricantes f on (a.Fabricante=f.Id)
					join MM.Tipos_Servicio ts on (a.Tipo_Servicio=ts.Id)				
go


create view MM.funcionalidadPorRol as
select f.descripcion as 'Descripcion', r.Descripcion as 'Rol'
from MM.funcionalidades f join MM.Roles_Funcionalidades rf on (f.Id=rf.Funcionalidad) 
					   join MM.Roles r on (r.Id=rf.Rol)
go



insert into MM.usuarios values ('admin','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',1,'OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go

insert into MM.usuarios values ('martinnbasile','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',1,'OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go

insert into MM.usuarios values ('bec','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',1,'OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go

insert into MM.usuarios values ('ale','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',1,'OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go

insert into MM.usuarios values ('fede','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',1,'OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go


Create  trigger MM.CuaandoSeIngresanLoginsIncorrectos
on MM.Intentos_login
for  insert
as
Begin transaction
Declare @num_login int
Declare @Correcto bit 
select Id_login,Es_Correcto into #tablaIncorrectos  from inserted
where Es_Correcto=0

if ((select count(*) from #tablaIncorrectos
)>0)
begin
Declare cursorDeIncorrectos Cursor
for select * from #tablaIncorrectos
open cursorDeIncorrectos
fetch next from cursorDeIncorrectos into @num_login,@Correcto
while(@@FETCH_STATUS=0)
begin
Insert into MM.Intentos_fallidos (Cod_login)values (@num_login)
fetch next from cursorDeIncorrectos into @num_login,@Correcto
end

close cursorDeIncorrectos
deallocate cursorDeIncorrectos
end
drop table #tablaIncorrectos
commit;
go


Create trigger MM.CuandoSeIngresanLoginsCorrectos
on MM.Intentos_login
for  insert
as
Begin transaction
Declare @num_login int
Declare @Correcto bit 
Declare @cantidad int
declare @cod_usuario int

select Id_login,Es_Correcto,Codigo_usuario into #tablaCorrectos  from inserted
where Es_Correcto=1

if ((select count(*) from #tablaCorrectos
)>0)
begin
Declare cursorDeIncorrectos Cursor
for select * from #tablaCorrectos
open cursorDeIncorrectos
fetch next from cursorDeIncorrectos into @num_login,@Correcto,@cod_usuario
while(@@FETCH_STATUS=0)
begin
Delete from MM.Intentos_fallidos where @cod_usuario=(select codigo_usuario from intentos_login i where i.id_login=cod_login)   
fetch next from cursorDeIncorrectos into @num_login,@Correcto,@cod_usuario
end
close cursorDeIncorrectos
deallocate cursorDeIncorrectos
end
drop table #tablaCorrectos

commit;
go

Create trigger MM.CuandoSeIngresaUnTercerLoginFallidoSEInhabilita
on MM.Intentos_fallidos
for  insert
as
Begin transaction
Declare @num_fallido int
Declare @Cod_login int
Declare cursorDeFallidos Cursor
for select * from inserted
open cursorDeFallidos
fetch next from cursorDeFallidos into @num_fallido,@Cod_login
while(@@FETCH_STATUS=0)
begin
if ((select count(*) from MM.Intentos_fallidos,MM.Usuarios,MM.Intentos_login
where MM.Intentos_fallidos.Cod_login=MM.Intentos_login.Id_login and MM.Intentos_login.Codigo_usuario=MM.Usuarios.Id and 
Id=(Select Codigo_usuario from MM.Intentos_login where Id_login=@Cod_login))>2)
update MM.Usuarios
set Estado='inhabilitado'
where Id = (Select Codigo_usuario from MM.Intentos_login where Id_login=@Cod_login)
fetch next from cursorDeFallidos into @num_fallido,@Cod_login

end
close cursorDeFallidos
deallocate cursorDeFallidos
commit;
go	

create table MM.Fecha(
orden int identity(1,1) primary key,
fecha date
)
go
create  function MM.fechaDeHoy()
returns date
as begin

return (select top 1 fecha from MM.Fecha order by orden)
end
go


create function MM.cantidadMillas(@cliente int)
returns int
as
begin
return(
select sum(Millas) from MM.Millas
where Cliente=@cliente
and Fecha<dateadd(year,-1,MM.fechaDeHoy())
)
end
go

CREATE PROCEDURE MM.aeronavesSustitutas @matricula varchar(10),@fechaBaja smalldatetime,@fechaAlta smalldatetime

AS

	DECLARE @Modelo varchar(30)		
	DECLARE @Fabricante int
	DECLARE @Tipo_Servicio int
	
	SELECT @Modelo = Modelo,@Fabricante = Fabricante,@Tipo_Servicio = Tipo_Servicio from MM.Aeronaves a where 
	a.matricula=@matricula 
	
SELECT a.Fecha_alta as 'Fecha de alta',  a.Modelo as 'Modelo',a.matricula as 'Matrícula',f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',a.Baja_Fuera_Servicio as 'Baja por fuera de servicio',a.Baja_Vida_Util as 'Baja por vida util',a.Fecha_Fuera_Servicio as 'Fecha de fuera de servicio',a.Fecha_Reinicio_Servicio as 'Fecha de reinicio de servicio',a.Fecha_Baja_Definitiva as 'Fecha de baja definitiva',a.Cantidad_Butacas as 'Cantidad de butacas',a.Cantidad_Kg as 'Cantidad de Kgs disponibles para realizar encomiendas'
from MM.aeronaves a
join MM.Fabricantes f on (a.Fabricante=f.Id)
join MM.Tipos_Servicio ts on (a.Tipo_Servicio=ts.Id)
where  
	not exists (
		select * from MM.Viajes v
		where (v.Fecha_salida between @fechaBaja and @fechaAlta
		or  v.Fecha_Estimada_llegada between @fechaBaja and @fechaAlta) 
		and a.matricula=v.Matricula
	)
	and a.Modelo=@Modelo
	and a.Fabricante=@Fabricante
	and a.Tipo_Servicio=@Tipo_Servicio
	and a.matricula!=@matricula			 
		 

GO

create function MM.convertirFecha (@fecha varchar(10))
returns Date
as
begin
return (convert(Date,@fecha,103))
end
go
create procedure MM.actualizarFecha @fecha varchar(10) 
as
begin
insert into MM.Fecha(fecha) values(MM.convertirFecha(@fecha))
end 
go


insert into MM.Roles_Funcionalidades(Funcionalidad,Rol) select Id,1 from MM.Funcionalidades;
go

insert into MM.Roles_Funcionalidades(Funcionalidad,Rol) values(6,2);

insert into MM.Roles_Funcionalidades(Funcionalidad,Rol) values(8,2);

insert into MM.Roles_Funcionalidades(Funcionalidad,Rol) values(7,2);
go


create procedure MM.Loggear @username varchar(30),@contra varchar(255)
as
begin transaction set transaction isolation level serializable
if(not Exists (select Id from MM.Usuarios where Username=@username))
begin
	raiserror ('No existe usuario',16,150)
	rollback
	return
	end
if (exists (select id from MM.Usuarios where
Username=@username and Estado='inhabilitado'))
begin
	raiserror ('Usuario inhabilitado',16,150)
	rollback
	return
end 
if (exists (select id from MM.Usuarios where
Username=@username and Password=@contra))
begin
insert into MM.Intentos_login	 select id,1 from MM.Usuarios where
username=@username
end
else
begin
insert into MM.Intentos_login	 select id,0 from MM.Usuarios where
username=@username
raiserror ('Contraseña incorrecta',16,150)
end
commit
go


create procedure MM.CancelarAeronaveVidaUtil
  (
  @matricula varchar(10))
  as
  BEGIN TRAN
  declare
  @dia datetime
  set @dia=getdate()
  delete from MM.Butacas where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Matricula=@matricula)
  delete from MM.Pasajes where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Matricula=@matricula)
  delete from MM.Paquetes where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Matricula=@matricula)
  delete from MM.Viajes where Matricula=@matricula and Fecha_salida>=GETDATE()
  UPDATE MM.Aeronaves set Baja_Vida_Util='SI',Fecha_Baja_Definitiva=GETDATE() where matricula=@matricula 	  

  COMMIT TRAN
  go



create procedure MM.CancelarAeronaveFueraDeServicio
  (
  @matricula varchar(10),
  @hasta datetime)
  as
  BEGIN TRAN
  declare
  @dia datetime
  set @dia=getdate()
  delete from MM.Butacas where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Fecha_salida<=@hasta and Matricula=@matricula)
  delete from MM.Pasajes where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Fecha_salida<=@hasta and Matricula=@matricula)
  delete from MM.Paquetes where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Fecha_salida<=@hasta and Matricula=@matricula)
  delete from MM.Viajes where Matricula=@matricula and Fecha_salida>=GETDATE() and Fecha_salida<=@hasta
  UPDATE MM.Aeronaves set Baja_Fuera_Servicio='SI',Fecha_Fuera_Servicio=GETDATE(), Fecha_Reinicio_Servicio=@hasta where matricula=@matricula
  COMMIT TRAN
  go


create procedure MM.BorrarCiudades(
@Descripcion varchar(100))
as
begin tran
declare @id int
set @id=(select Id from MM.Ciudades where Descripcion=@Descripcion)
update MM.Ciudades set Estado='Deshabilitado' where Id=@id
update MM.Rutas_Aereas set Estado=2 where Ciudad_Destino=@id or Ciudad_Origen=@id

delete from MM.Butacas where Viaje in (select v.Id from MM.Viajes v inner join MM.Rutas_Aereas r
on v.Ruta=r.Id
where r.Estado=2)
delete from MM.Pasajes where Viaje in (select v.Id from MM.Viajes v inner join MM.Rutas_Aereas r
on v.Ruta=r.Id
where r.Estado=2
)
delete from MM.Paquetes where Viaje in (select v.Id from MM.Viajes v inner join MM.Rutas_Aereas r
on v.Ruta=r.Id
where r.Estado=2
)
delete from MM.Viajes where Ruta in (select Id from MM.Rutas_Aereas where Estado=2) and Fecha_salida>= MM.fechaDeHoy()
commit tran
go

create view MM.rolPorUsuario as
select r.descripcion as rol, u.username as usuario from
MM.usuarios u join MM.Usuario_rol ur on (u.id=ur.cod_usuario)
join MM.roles r on (ur.cod_rol=r.Id)
go

/*
create function MM.movimientosMillas(@cliente int)
returns @tablita table(fecha date,cantidad int)
as
begin
insert into @tablita

select Fecha,Millas from MM.Millas
where Cliente=@cliente
and Fecha<dateadd(year,-1,MM.fechaDeHoy())
return
end

GO*/

create trigger MM.actualizarVencimientoCuandoSeAgreganMillas on MM.Millas
for insert 
as
begin transaction
declare cursorMillas Cursor
for
select Cliente from inserted
open cursorMillas
declare @cliente int
Fetch next from cursorMillas into @cliente
while(@@FETCH_STATUS=0)
begin 
update MM.Clientes
set Fecha_prox_vencimiento=dateadd(year,1,MM.fechaDeHoy())
where Id=@cliente
Fetch next from cursorMillas into @cliente
end
close cursorMillas
deallocate cursorMillas
commit 
go
create procedure MM.asentarMillas @viaje int
as
begin transaction
insert into MM.Millas(Cliente,Millas)
select Cliente,r.Precio_Base/10 from MM.Pasajes p join MM.Viajes v on v.Id=p.Viaje and v.Id=@viaje join MM.Rutas_Aereas r 
on r.Id=v.Ruta
union
select p.Cliente,(p.Kg*r.Precio_Kg)/10 from MM.Paquetes p join MM.Viajes v on v.Id=p.Viaje and v.Id=@viaje join MM.Rutas_Aereas 
r on r.Id=v.Ruta 


commit
go


create procedure MM.asentarLLegadaAeronave @avion int,@origen int,@destino int,@horaLlegada varchar(20)
as
begin transaction
declare @hora datetime
declare @viaje int
select @hora=cast(MM.fechaDeHoy() as datetime)+(convert(datetime,@horaLlegada,20))
(select @viaje=v.Id from MM.viajes v join MM.Rutas_Aereas r on v.Ruta=r.Id and r.Ciudad_Destino=@destino and r.Ciudad_Origen=@origen where 
Matricula=@avion and Fecha_Estimada_llegada between -datediff(hour,1,@hora) and dateadd(hour,1,@hora))

if(@viaje is null)
begin	
raiserror ('No deberia estar llegando alli este avion',16,150)
rollback
end
else
begin
update MM.Viajes 
set Fecha_llegada=@hora
where Id=@viaje
exec MM.asentarMillas @viaje
commit
end

go
insert into MM.Millas(Cliente,Millas)
select Cliente,r.Precio_Base/10 from MM.Pasajes p join MM.Viajes v on v.Id=p.Viaje  join MM.Rutas_Aereas r 
on r.Id=v.Ruta
union
select p.Cliente,(p.Kg*r.Precio_Kg)/10 from MM.Paquetes p join MM.Viajes v on v.Id=p.Viaje   join MM.Rutas_Aereas r 
on r.Id=v.Ruta 
go


insert into MM.Usuario_rol values(1,1);
go
insert into MM.Usuario_rol values(2,1);
go
insert into MM.usuario_rol values (3,1)
go
insert into MM.usuario_rol values (4,1)
go
insert into MM.usuario_rol values (5,1)
go
