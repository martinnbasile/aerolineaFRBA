Use GD2C2015
go
create schema MM
go
Create Procedure MM.limpiarBase as

drop procedure mm.crearAeronave
drop procedure mm.crearModeloAvion
drop function mm.semestre
drop function mm.maximosMilleros
drop function mm.DestinosMasVendidosPasajes
drop function mm.DestinosMasCancelados
drop function mm.AeronavesMasDiasFueraServicio
drop Procedure MM.CancelarAeronaveFueraDeServicio
drop Procedure MM.BorrarCiudades
drop view MM.rolPorUsuario
drop view MM.vista_modelos
drop Procedure MM.asentarMillas
drop Procedure MM.asentarLLegadaAeronave
drop Procedure MM.Loggear
drop Procedure MM.CancelarAeronaveVidaUtil
drop Procedure MM.agregarFuncionalidadesRol
drop Procedure MM.darDeBajaRol
drop function MM.devuelveIDD
drop function MM.fechaDeHoy
drop procedure mm.crearRuta
drop procedure mm.actualizarRuta
drop function MM.devuelveRutaaa
drop Procedure MM.aeronavesSustitutas
drop function MM.devuelveTipoServicio
drop function MM.devuelveNumeroCliente
drop function MM.convertirFecha
drop Procedure MM.actualizarFecha
drop Procedure MM.DestinosMasVendidosPasajes
drop Procedure MM.AeronavesMasDiasFueraServicio
drop Procedure MM.registrarCanje
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
Drop table MM.Productos_Milla
Drop table MM.Intentos_login
Drop table MM.Usuario_rol
Drop table MM.Viajes
Drop table MM.Rutas_Aereas
Drop table MM.Ciudades 
Drop table MM.tarjetas_Credito
Drop table MM.Clientes 
Drop table MM.Usuarios 
Drop table MM.Roles 
drop table MM.Funcionalidades
drop table MM.Fabricantes
drop table MM.Tipos_Servicio
drop table MM.logBajasAeronaves
drop table MM.Butacas_Avion
drop table MM.Aeronaves
drop table MM.modeloAvion
drop function MM.top5ClientesConMasMillas
drop function MM.millasClienteEnUnPeriodo
drop function MM.top5LugaresConMasPasajes
drop procedure mm.generarViaje
drop function mm.aeronavesDisponibles
drop procedure mm.limpiarBase
DROP FUNCTION MM.BUTACASDISPONIBLES
DROP FUNCTION MM.VIAJESDISPONIBLES
drop procedure mm.ingresarCompraPaquete
drop procedure mm.ingresarCompraPasaje

drop schema MM


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
Estado varchar(15))
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
Ruta_Codigo int ,
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
create table mm.modeloAvion(
id int identity(1,1) primary key,
Modelo_descripcion varchar(10),
Kg int,
fabricante varchar(20),
tipoServicio varchar(20)
)
go

create table mm.Butacas_Avion(
id  int identity(1,1) primary key ,
butacaNum int,
butacaPiso int,
butacaTipo varchar(30),
modeloAvion int foreign key references mm.ModeloAvion(id)
)
go

create table mm.aeronaves(
matricula varchar(10) primary key,
fecha_alta datetime,
modelo int foreign key references mm.ModeloAvion(id),
fecha_baja_definitiva datetime,
fecha_baja_fuera_servicio datetime,
fecha_alta_fuera_servicio datetime,
)
go


Create table MM.Viajes(
Id int identity(1,1) primary key ,
Matricula varchar(10) foreign key references MM.Aeronaves(Matricula),
Ruta int,
constraint Ruta foreign key (Ruta) references MM.Rutas_Aereas(Id),
kgDisponibles int,
butacasDisponibles int,
check(kgdisponibles>=0),
check(butacasdisponibles>=0)
)
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
Cliente int,
cod_compra int)
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
Cliente int,
cod_compra int)
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
Millas int not null,
Fecha_movimiento datetime,
Descripcion varchar(20))
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
check (Cantidad>0),
Millas_Necesarias int not null)
go
create table MM.Cambios_Millas(
Id int identity(1,1) primary key,
Cliente int,
Producto int,
Cantidad int not null,
Fecha_Canje smalldatetime  )
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
create table MM.Butacas(
Viaje int not null,
Nro int not null,
Ubicacion varchar(30),
Estado varchar(50) not null,
FOREIGN KEY (Viaje) references MM.Viajes(Id)
)
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


insert into mm.modeloAvion(Modelo_descripcion,Kg,fabricante,tipoServicio)
select distinct Aeronave_Modelo,Aeronave_KG_Disponibles,f.Id,s.Id from gd_esquema.Maestra as m join mm.Fabricantes as f on f.Descripcion=m.Aeronave_Fabricante join mm.Tipos_Servicio as s on s.Descripcion=m.Tipo_Servicio 
go

insert into mm.Butacas_Avion (butacaNum,butacaPiso,butacaTipo,modeloAvion)
select ma.Butaca_Nro,ma.Butaca_Piso,ma.Butaca_Tipo,mo.id
from gd_esquema.Maestra ma join mm.modeloAvion mo on (ma.Aeronave_KG_Disponibles=mo.Kg and ma.Aeronave_Modelo=mo.Modelo_descripcion and ma.Butaca_Nro<>0) join mm.Fabricantes f on f.Id=mo.fabricante and f.Descripcion=ma.Aeronave_Fabricante join mm.Tipos_Servicio t on t.Id=mo.tipoServicio and t.Descripcion=ma.Tipo_Servicio
go

insert into MM.Aeronaves (matricula,fecha_alta,modelo)
select distinct Aeronave_Matricula,'1999-01-01', mo.id 
from gd_esquema.Maestra ma join mm.modeloAvion mo on (ma.Aeronave_KG_Disponibles=mo.Kg and ma.Aeronave_Modelo=mo.Modelo_descripcion ) join mm.Fabricantes f on f.Id=mo.fabricante and f.Descripcion=ma.Aeronave_Fabricante join mm.Tipos_Servicio t on t.Id=mo.tipoServicio and t.Descripcion=ma.Tipo_Servicio
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
insert into MM.Clientes(DNI,Nombre,Apellido,Direccion,Telefono,Mail,Fecha_Nacimiento,Fecha_prox_vencimiento) 
(select 
Cli_Dni,Cli_Nombre,Cli_Apellido,Cli_Dir,Cli_Telefono,Cli_Mail,Cli_Fecha_Nac,dateadd(year,1,max(FechaLLegada)) from gd_esquema.Maestra
group by Cli_Dni,Cli_Nombre,Cli_Apellido,Cli_Dir,Cli_Telefono,Cli_Mail,Cli_Fecha_Nac)
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
add Estado int not null default 'Habilitado'
go
alter table MM.Viajes
add Fecha_Salida date not null
go
alter table MM.Viajes
add Fecha_Estimada_llegada date
alter table MM.Viajes
add Fecha_llegada date

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

insert into MM.Rutas_Aereas(Ruta_Codigo,Ciudad_Destino,Ciudad_Origen,Precio_Base,Precio_Kg,Estado,Tipo_Servicio)
select e.Ruta_Codigo,d.Id,o.Id,max(e.Ruta_Precio_BasePasaje),max(e.Ruta_Precio_BaseKG),1,t.Id from gd_esquema.Maestra as e join mm.ciudades as d on e.Ruta_Ciudad_Destino=d.Descripcion join  mm.Ciudades as o on e.Ruta_Ciudad_Origen=o.Descripcion join mm.Tipos_Servicio t on t.Descripcion=e.Tipo_Servicio
group by d.Id,o.Id,e.Ruta_Codigo,t.Id

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

insert into MM.Viajes(Matricula,ruta,fECHA_SALIDA,Fecha_Estimada_llegada,fecha_llegada)
select Aeronave_Matricula,r.Id,
FechaSalida,Fecha_LLegada_Estimada,FechaLlegada
from gd_esquema.Maestra g join MM.Rutas_Aereas as r on g.Ruta_Codigo=r.Ruta_Codigo join MM.Tipos_Servicio t on t.Id=r.Tipo_Servicio and g.Tipo_Servicio=t.Descripcion join mm.Ciudades o on g.Ruta_Ciudad_Origen=o.Descripcion and r.Ciudad_Origen=o.Id join mm.Ciudades c on c.Descripcion=g.Ruta_Ciudad_Destino and r.Ciudad_Destino=c.Id
group by Aeronave_Matricula,
FechaSalida,FechaLLegada,Fecha_LLegada_Estimada,r.Id
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

insert into MM.Pasajes(Viaje,Numero_Butaca,Fecha_Compra,Cliente)
select distinct b.Id,a.Butaca_Nro,Pasaje_FechaCompra,d.Id  from gd_esquema.Maestra as a join MM.Viajes as b on 
b.Fecha_Estimada_llegada=a.Fecha_LLegada_Estimada and b.Fecha_llegada=a.FechaLLegada and b.Fecha_salida=a.FechaSalida and 
b.Matricula=a.Aeronave_Matricula join MM.Rutas_Aereas as c on b.Ruta=c.Id and c.Precio_Base=a.Ruta_Precio_BasePasaje 
/*and c.Precio_Kg=a.Ruta_Precio_BaseKG */join MM.Tipos_Servicio as g on  c.Tipo_Servicio=g.Id and g.Descripcion =a.Tipo_Servicio 
join MM.Clientes as d on d.DNI=a.Cli_Dni join MM.Ciudades as e on c.Ciudad_Destino=e.Id and e.Descripcion=a.Ruta_Ciudad_Destino 
join MM.Ciudades as f on c.Ciudad_Origen=f.Id and f.Descripcion=a.Ruta_Ciudad_Origen
where Pasaje_codigo <>0
group by b.Id,Butaca_Nro,Pasaje_fechaCompra,d.Id

insert into MM.Paquetes(Viaje,Kg,Fecha_Compra,Cliente)
select distinct b.Id,a.Paquete_KG,Paquete_FechaCompra,d.Id  from gd_esquema.Maestra as a join MM.Viajes as b on b.Fecha_Estimada_llegada=a.Fecha_LLegada_Estimada 
and b.Fecha_llegada=a.FechaLLegada and b.Fecha_salida=a.FechaSalida and b.Matricula=a.Aeronave_Matricula join MM.Rutas_Aereas 
as c on b.Ruta=c.Id /*and c.Precio_Base=a.Ruta_Precio_BasePasaje*/ and c.Precio_Kg=a.Ruta_Precio_BaseKG join MM.Tipos_Servicio as 
g on  c.Tipo_Servicio=g.Id and g.Descripcion =a.Tipo_Servicio join MM.Clientes as d on d.DNI=a.Cli_Dni join MM.Ciudades as e 
on c.Ciudad_Destino=e.Id and e.Descripcion=a.Ruta_Ciudad_Destino join MM.Ciudades as f on c.Ciudad_Origen=f.Id and 
f.Descripcion=a.Ruta_Ciudad_Origen
where Paquete_KG != 0
group by b.Id,a.Paquete_KG,Paquete_fechaCompra,d.Id

go
/*
insert into MM.Butacas
select  b.Id,a.Butaca_Nro,a.Butaca_Tipo,'vendida'  from gd_esquema.Maestra as a join MM.Viajes as b on 
b.Fecha_Estimada_llegada=a.Fecha_LLegada_Estimada and b.Fecha_llegada=a.FechaLLegada and b.Fecha_salida=a.FechaSalida and 
b.Matricula=a.Aeronave_Matricula join MM.Rutas_Aereas as c on b.Ruta=c.Id and c.Precio_Base=a.Ruta_Precio_BasePasaje 
/*and c.Precio_Kg=a.Ruta_Precio_BaseKG */join MM.Tipos_Servicio as g on  c.Tipo_Servicio=g.Id and g.Descripcion =a.Tipo_Servicio  
join MM.Ciudades as e on c.Ciudad_Destino=e.Id and e.Descripcion=a.Ruta_Ciudad_Destino join MM.Ciudades as f 
on c.Ciudad_Origen=f.Id and f.Descripcion=a.Ruta_Ciudad_Origen
where Pasaje_codigo <>0
group by b.Id,Butaca_Nro,a.Butaca_Tipo

*/

go

create view MM.vista_rutas_aereas as
select r.Id as 'Codigo',  c1.descripcion as 'Ciudad origen',c2.descripcion as 'Ciudad destino',t.Descripcion as 'Servicio', r.Precio_Base as 'Precio base',r.Precio_Kg as 'Precio base encomienda'
from MM.Rutas_Aereas r join MM.Ciudades c1 on (r.Ciudad_Origen=c1.Id)
					join MM.Ciudades c2 on (r.Ciudad_Destino=c2.Id)
					join MM.Tipos_Servicio t on (r.Tipo_Servicio=t.Id) and r.Estado<>2
go

create view MM.vista_aeronaves as
select a.Fecha_alta as 'Fecha de alta',  mo.Modelo_descripcion as 'Modelo',a.matricula as 'Matrícula',f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',a.fecha_baja_fuera_servicio as 'Fecha de fuera de servicio',a.fecha_alta_fuera_servicio as 'Fecha de reinicio de servicio',a.Fecha_Baja_Definitiva as 'Fecha de baja definitiva',mo.Kg as 'Cantidad de Kgs disponibles para realizar encomiendas'
from MM.Aeronaves a join mm.modeloAvion mo on mo.id=a.modelo join MM.Fabricantes f on (mo.Fabricante=f.Id)
					join MM.Tipos_Servicio ts on (mo.tipoServicio=ts.Id)				
go


create view MM.vista_modelos as
select mo.Modelo_descripcion as 'Modelo',f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',mo.Kg as 'Cantidad de Kgs disponibles para realizar encomiendas', Count(DISTINCT ba.butacaPiso) as 'Cantidad de pisos',COUNT(ba.id) as 'Cantidad de asientos'
from mm.modeloAvion mo join MM.Fabricantes f on (mo.Fabricante=f.Id)
					   join MM.Tipos_Servicio ts on (mo.tipoServicio=ts.Id)	
					   join MM.Butacas_Avion ba on (mo.id=ba.modeloAvion)	
					   group by Modelo_descripcion,f.Descripcion,ts.Descripcion,mo.Kg		
go


create view MM.funcionalidadPorRol as
select f.descripcion as 'Descripcion', r.Descripcion as 'Rol'
from MM.funcionalidades f join MM.Roles_Funcionalidades rf on (f.Id=rf.Funcionalidad) 
					   join MM.Roles r on (r.Id=rf.Rol)
go



insert into MM.usuarios values ('admin','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7','OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go

insert into MM.usuarios values ('martinnbasile','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7','OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go

insert into MM.usuarios values ('bec','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7','OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go

insert into MM.usuarios values ('ale','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7','OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
go

insert into MM.usuarios values ('fede','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7','OK','Sos Dios?','1ea442a134b2a184bd5d40104401f2a37fbc09ccf3f4bc9da161c6099be3691d')
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

CREATE PROCEDURE MM.aeronavesSustitutas @matricula varchar(10),@fechaBaja smalldatetime,@fechaAlta smalldatetime


AS

	DECLARE @Modelo int		
	SELECT @Modelo = Modelo from MM.Aeronaves a where a.matricula=@matricula 
	
	

SELECT a.Fecha_alta as 'Fecha de alta',  mo.Modelo_descripcion as 'Modelo',a.matricula as 'Matrícula',
f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',
a.fecha_baja_fuera_servicio as 'Fecha de fuera de servicio',a.fecha_alta_fuera_servicio as 'Fecha de reinicio de servicio',
a.Fecha_Baja_Definitiva as 'Fecha de baja definitiva',mo.Kg as 'Cantidad de Kgs disponibles para realizar encomiendas',
Count(DISTINCT ba.butacaPiso) as 'Cantidad de pisos',COUNT(ba.id) as 'Cantidad de asientos'
from MM.aeronaves a
join mm.modeloAvion mo on (a.modelo=mo.id)
join MM.Fabricantes f on (mo.fabricante=f.Id)
join MM.Tipos_Servicio ts on (mo.TipoServicio=ts.Id)
join MM.Butacas_Avion ba on (mo.id=ba.modeloAvion)	
where  
	not exists (
		select * from MM.Viajes v
		where (v.Fecha_salida between @fechaBaja and @fechaAlta
		or  v.Fecha_Estimada_llegada between @fechaBaja and @fechaAlta) 
		and a.matricula=v.Matricula
	)
	and a.fecha_baja_definitiva is null
	and (((a.fecha_baja_fuera_servicio not between @fechaBaja and @fechaAlta) and (a.fecha_alta_fuera_servicio not between @fechaBaja and @fechaAlta)) or ((a.fecha_baja_fuera_servicio is null) and (a.fecha_alta_fuera_servicio is null)))
	and a.Modelo=@Modelo
	and a.matricula!=@matricula			 
	 group by Modelo_descripcion,f.Descripcion,ts.Descripcion,mo.Kg,
	 a.fecha_alta,a.modelo,a.matricula,a.fecha_baja_fuera_servicio,
	 fecha_alta_fuera_servicio,a.fecha_baja_definitiva

go

create function MM.convertirFecha (@fecha varchar(10))
returns Date
as
begin
return ( convert(Date,@fecha,103))
end
go
create   procedure MM.actualizarFecha @fecha varchar(10) 
as
begin
insert into MM.Fecha(fecha) values(MM.convertirFecha(@fecha))

insert into mm.Millas(Cliente,Millas,Fecha_movimiento,Descripcion) 

select c.Id,-sum(m.Millas),MM.convertirFecha(@fecha),'VENCIMIENTO' from mm.Clientes c join mm.Millas m on m.Cliente=c.Id
where c.Fecha_prox_vencimiento<MM.convertirFecha(@fecha) 

group by c.Id

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


create procedure MM.CancelarAeronaveVidaUtil (@matricula varchar(10))
  as
  BEGIN TRAN

  
  
  delete from MM.Butacas where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=mm.fechaDeHoy() and Matricula=@matricula)

  delete from MM.Pasajes where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=mm.fechaDeHoy() and Matricula=@matricula)

  delete from MM.Paquetes where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=mm.fechaDeHoy() and Matricula=@matricula)

  delete from MM.Viajes where Matricula=@matricula and Fecha_salida>=mm.fechaDeHoy()

  UPDATE MM.Aeronaves set fecha_baja_definitiva=mm.fechaDeHoy() where matricula=@matricula 	  
  
  COMMIT TRAN
  go



create procedure MM.CancelarAeronaveFueraDeServicio(@matricula varchar(10), @hasta datetime)
  as
  BEGIN TRAN
  declare

  @dia datetime
  set @dia=mm.fechaDeHoy()

  delete from MM.Butacas where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Fecha_salida<=@hasta and Matricula=@matricula)

  delete from MM.Pasajes where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Fecha_salida<=@hasta and Matricula=@matricula)

  delete from MM.Paquetes where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Fecha_salida<=@hasta and Matricula=@matricula)

  delete from MM.Viajes where Matricula=@matricula and Fecha_salida>=@dia and Fecha_salida<=@hasta

  UPDATE MM.Aeronaves set fecha_baja_fuera_servicio=@dia, Fecha_alta_fuera_servicio=@hasta
  where matricula=@matricula
  COMMIT TRAN
  go

create  procedure MM.BorrarCiudades(
@Descripcion varchar(100))
as
begin tran
declare @id int
select @id=(select Id from MM.Ciudades where Descripcion= @Descripcion)
update MM.Ciudades set Estado='Deshabilitado' where Id= @id
update MM.Rutas_Aereas set Estado=2 where Ciudad_Destino=10/*@id */or Ciudad_Origen=@id
delete b  from mm.Rutas_aereas r join mm.viajes v on v.ruta=r.id  join mm.butacas b on b.viaje=v.id
where r.Estado=2

delete p  from mm.Rutas_aereas r join mm.viajes v on v.ruta=r.id join mm.pasajes p on p.viaje=v.id 
where r.Estado=2

delete w  from mm.Rutas_aereas r join mm.viajes v on v.ruta=r.id join  mm.paquetes w on w.viaje=v.id 
where r.Estado=2

delete v  from mm.Rutas_aereas r join mm.viajes v on v.ruta=r.id 
where r.Estado=2
commit tran
go

create view MM.rolPorUsuario as
select r.descripcion as rol, u.username as usuario from
MM.usuarios u join MM.Usuario_rol ur on (u.id=ur.cod_usuario)
join MM.roles r on (ur.cod_rol=r.Id) 
go
create procedure MM.asentarMillas @viaje int
as
begin transaction
insert into MM.Millas(Cliente,Millas,Fecha_movimiento,Descripcion)
select Cliente,r.Precio_Base/10,MM.fechaDeHoy(),'COMPRA PASAJE' from MM.Pasajes p join MM.Viajes v on v.Id=p.Viaje and v.Id=@viaje join MM.Rutas_Aereas r 
on r.Id=v.Ruta
union
select p.Cliente,(p.Kg*r.Precio_Kg)/10,MM.fechaDeHoy(),'COMPRA PAQUETE' from MM.Paquetes p join MM.Viajes v on v.Id=p.Viaje and v.Id=@viaje join MM.Rutas_Aereas 
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
delete from MM.Butacas where Viaje=@viaje
commit
end

go

insert into MM.Millas(Cliente,Millas,Fecha_movimiento,Descripcion)
select Cliente,r.Precio_Base/10,v.Fecha_llegada,'COMPRA PASAJE' from MM.Pasajes p join MM.Viajes v on v.Id=p.Viaje  join MM.Rutas_Aereas r 
on r.Id=v.Ruta
union
select p.Cliente,(p.Kg*r.Precio_Kg)/10,v.Fecha_llegada,'COMPRA PAQUETE' from MM.Paquetes p join MM.Viajes v on v.Id=p.Viaje   join MM.Rutas_Aereas r 
on r.Id=v.Ruta 
go
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

CREATE PROCEDURE [MM].[registrarCanje] @numCli int,@cantidad int,@descripcion varchar (30)
AS

			
	DECLARE @idProducto int
	DECLARE @cantidadActual int
	declare @millasCosto int

	SELECT @idProducto = id, @cantidadActual=Cantidad,@millasCosto=Millas_necesarias  from MM.Productos_Milla a where 
	a.Descripcion=@descripcion
	 

BEGIN TRANSACTION
	
	Insert into mm.millas(cliente,Millas,Fecha_movimiento,Descripcion)
	values (@numCli,-(@cantidad*@millasCosto),getdate(),'Canje de millas')
		
	Begin try
	UPDATE MM.Productos_Milla
	SET Cantidad=@cantidadActual-@cantidad
	WHERE Descripcion=@descripcion
	end try

	begin catch
			raiserror ('No hay cantidad suficiente en stock',10,16)
			rollback
	end catch

COMMIT
GO

alter table MM.Roles
add Estado varchar(20) default 'Habilitado'
go
update MM.roles
set Estado = 'Habilitado'
go

CREATE PROCEDURE [MM].[darDeBajaRol]
@rol VARCHAR (50)
AS
	DECLARE @idRol int		
	SELECT @idRol = id from MM.Roles a where 
	a.Descripcion=@rol
BEGIN TRANSACTION
update MM.roles
set Estado='Deshabilitado'
where Descripcion = @rol
delete from MM.Usuario_rol where cod_rol=@idRol
COMMIT

GO
create function mm.DestinosMasVendidosPasajes
(@semestre int,
@anio char(4))
returns @table table
(Description varchar(100))
as
begin
declare @desde char(4)
declare @hasta char(4)
if @semestre=1
begin
set @desde='0101'
set @hasta='0530'
end
if @semestre=2
begin
set @desde='0601'
set @hasta='1231'
end
insert into @table  
select top 5 d.Descripcion
from MM.Pasajes a,MM.Viajes b, mm.Rutas_Aereas c, MM.Ciudades d
where a.Viaje=b.Id and b.Ruta=c.Ciudad_Destino and c.Ciudad_Destino=d.Id and b.Fecha_salida 
between @anio+@desde and @anio+@hasta 
group by d.Descripcion 
order by count(*) desc
return
end
GO
create function mm.semestre(@fecha date)
returns int
as 
begin
declare @puto int
set @puto=1+(month(@fecha)-1)/6

return @puto
end
go
create  function mm.AeronavesMasDiasFueraServicio
(@semestre int, @anio char(4))

returns @table table (Description varchar(50))
as
begin

insert into @table
select top 5 aeronave
from  mm.logBajasAeronaves
where (year(fechabaja)=@anio and mm.semestre(FechaBaja)=@semestre)
group by aeronave
order by count(*) desc

return
end

go

create function mm.DestinosMasCancelados
(@semestre int,
@anio char(4))
returns @table table
(Description varchar(100))
as
begin
declare @desde char(4)
declare @hasta char(4)
if @semestre=1
begin
set @desde='0101'
set @hasta='0530'
end
if @semestre=2
begin
set @desde='0601'
set @hasta='1231'
end
insert into @table  

select top 5 e.Descripcion
from MM.Cancelaciones a,MM.Pasajes b, MM.Viajes c, MM.Rutas_Aereas d, MM.Ciudades e
where	a.Codigo_Pasaje=b.Id and
		b.Viaje=c.Id and
		c.Ruta=d.Id and
		d.Ciudad_Destino=e.Id 
		and c.Fecha_salida 
between @anio+@desde and @anio+@hasta 
group by e.Descripcion 
order by count(*) desc
return
end
GO



CREATE PROCEDURE MM.agregarFuncionalidadesRol @rol varchar(70) 
AS
	DECLARE @idRol int		
	SELECT @idRol = id from MM.Roles a where 
	a.Descripcion=@rol
BEGIN TRANSACTION
	DELETE from MM.Roles_Funcionalidades
	where Rol=@idRol
	INSERT INTO MM.Roles_Funcionalidades(Funcionalidad,Rol)
	select MM.Funcionalidades.Id,@idRol from MM.Funcionalidades,#tablaTemporal as f where funcionalidad=MM.Funcionalidades.Descripcion
	DROP table #tablaTemporal
COMMIT
GO


create index indiceMillero on mm.clientes (nombre,apellido)
go

create function mm.maximosMilleros(@semestre int,@anio int) returns
@tablita table(nombre varchar(30), apellido varchar(30),cantMillas int)
as
begin
insert into @tablita 
select top 5 c.nombre, c.apellido, sum(m.millas)

from mm.millas m join mm.clientes c on (m.cliente=c.id)
where m.millas>0 and year(m.Fecha_movimiento)=@anio and (1+(month(m.Fecha_movimiento)-1)/6)=@semestre
group by c.nombre,c.apellido


order by sum(m.millas) DESC
return
end
go

create table mm.logBajasAeronaves(
id int identity(1,1),
aeronave varchar(10) foreign key references mm.aeronaves(matricula),
fechaBaja DateTime,
fechaAlta DateTime
)

create index aeronavesBajas on mm.logBajasAeronaves (aeronave)
go

create trigger mm.actualizarTablaLog on MM.Aeronaves
after update
as
begin
	
	insert into mm.logBajasAeronaves (aeronave,fechaBaja,fechaAlta)
	Select i.matricula, i.fecha_baja_fuera_servicio, i.fecha_alta_fuera_servicio
	from inserted i join deleted d on (i.matricula=d.matricula and i.fecha_alta_fuera_servicio>=mm.fechaDeHoy() and d.fecha_alta_fuera_servicio<mm.fechaDeHoy())
	
	declare miCursor cursor for
	select id, fecha_alta_fuera_servicio 
	from mm.logBajasAeronaves l join inserted i on 
	(l.aeronave=i.matricula and l.fechaBaja=i.fecha_baja_fuera_servicio
	and l.fechaBaja<i.fecha_baja_fuera_servicio)
	declare @id int
	declare @fechaR DateTime

	open miCursor

	fetch next from miCursor into @id,@fechaR
	while (@@fetch_status=0)
	begin
		update mm.logBajasAeronaves
		set fechaAlta = @fechaR
		where id = @id
	end
end
go

create   procedure  mm.crearModeloAvion @modeloDescripcion varchar(20),@Kg int,@fabricante int,@tipoServicio int,@cantPisos int,@cantButacasPiso int
as
begin transaction
insert into MM.modeloAvion(Modelo_descripcion,fabricante,Kg,tipoServicio) values(@modeloDescripcion,@fabricante,@Kg,@tipoServicio)
select @Kg=max(Id) from mm.modeloAvion
select @fabricante=@cantButacasPiso
while(@cantPisos>0)
begin
while(@cantButacasPiso>0)
begin
if(@cantButacasPiso%2=0)
insert into mm.Butacas_Avion(modeloAvion,butacaNum,butacaPiso,butacaTipo) values (@Kg,@fabricante*(@cantPisos-1)+@cantButacasPiso,@cantPisos,'Pasillo')
else

insert into mm.Butacas_Avion(modeloAvion,butacaNum,butacaPiso,butacaTipo) values (@Kg,@fabricante*(@cantPisos-1)+@cantButacasPiso,@cantPisos,'Ventanilla')
set @cantButacasPiso=@cantButacasPiso-1
end
set @cantButacasPiso=@fabricante
set @cantPisos=@cantPisos-1
end 
commit
go



create procedure mm.crearAeronave @matricula varchar(10),@id_Modelo int
as
insert into mm.aeronaves(matricula,modelo,fecha_alta) values(@matricula,@id_Modelo,mm.fechaDeHoy())

go


create procedure mm.crearRuta @destino varchar(30),@origen varchar(30),@servicio varchar(10),@basePasaje int,@baseKg int
as insert into mm.Rutas_Aereas(Ciudad_Destino,Ciudad_Origen,Tipo_Servicio,Precio_Base,Precio_Kg)
select d.Id,o.Id,t.Id,@basePasaje,@baseKg from mm.Tipos_Servicio as t,MM.Ciudades as o,mm.Ciudades as d
where t.Descripcion=@servicio and o.Descripcion=@origen and d.Descripcion=@destino

go


create procedure mm.actualizarRuta @id int,@destino varchar(30),@origen varchar(30),@servicio varchar(10),@basePasaje int,@baseKg int
as
begin
declare @dest int
declare @ori int
set @ori=(select Id from ciudades where Descripcion=@origen)
set @dest=(select Id from ciudades where Descripcion=@destino)
update  mm.Rutas_Aereas 
set Ciudad_Destino=@dest, Ciudad_Origen=@ori,Tipo_Servicio=(select Id from Tipos_Servicio where Descripcion=@servicio),Precio_Base=@basePasaje,Precio_Kg=@baseKg
where Id=@id  
end
go
create procedure mm.generarViaje @matricula varchar(10),@ruta int,@fechaSalida varchar(15),@fechaLlegada varchar(15)--FORMATO DE FECHAS aaaa-mm-dd hh:mi:ss(24h)
as
begin
declare @salida datetime
declare @llegada datetime
set @llegada=convert(date,@fechaLlegada,20)
set @salida=convert(date,@fechaSalida,20)
declare @kg int
declare @but int
select @but=count(*),@kg=mo.Kg from mm.aeronaves a join mm.modeloAvion mo on mo.id=a.modelo and a.matricula=@matricula join mm.Butacas_Avion b on b.modeloAvion=mo.id
group by mo.Kg
insert into mm.Viajes(Matricula,Ruta,Fecha_salida,Fecha_llegada,KgDisponibles,ButacasDisponibles) values (@matricula,@ruta,@salida,@llegada,@kg,@but)
declare @a int
select @a=max(Id) from mm.Viajes
insert into mm.Butacas (Viaje,Nro,Ubicacion,Estado)
select @a,b.butacaNum,b.butacaTipo,'Libre' from mm.Butacas_Avion b join mm.modeloAvion m on m.id=b.modeloAvion join mm.aeronaves a on a.modelo=m.Id and a.matricula=@matricula

end
go

create function mm.aeronavesDisponibles(@fechaSalida varchar(15),@fechaLlegada varchar(15),@TipoServicio varchar(15))
returns @tabla table
(matricula varchar(10))
as
begin

declare @salida datetime
declare @llegada datetime
set @llegada=convert(date,@fechaLlegada,20)
set @salida=convert(date,@fechaSalida,20)

insert into @tabla
select a.matricula  from mm.aeronaves a join mm.Viajes v on v.Matricula=a.matricula join modeloAvion m on m.id=a.modelo join mm.Tipos_Servicio t on t.Id=m.tipoServicio
where t.Descripcion=@TipoServicio and not((v.Fecha_llegada between @salida and @llegada  ) or (v.Fecha_salida between @salida and @llegada  ) )
and a.fecha_baja_definitiva is not null
group by a.Matricula

return 
end
go


create trigger decrementarButacas on mm.Pasajes
for insert
as
begin transaction
declare micursor cursor for
select viaje,Numero_Butaca from inserted 
declare @viaje int
declare @butaca int
open micursor
fetch next from micursor into @viaje,@butaca
while @@FETCH_STATUS=0
begin
update mm.viajes
set butacasdisponibles=butacasdisponibles-1
where id=@viaje

update mm.butacas
set Estado='Vendida'
where Viaje=@viaje and Nro=@butaca

fetch next from micursor into @viaje,@butaca

end
close micursor
deallocate micursor
commit
go
create trigger decrementarKg on mm.Paquetes
for insert
as
begin transaction
declare micursor cursor for
select viaje,sum(Kg) from inserted 
group by viaje
declare @viaje int
declare @cantidad int
open micursor
fetch next from micursor into @viaje,@cantidad
while @@FETCH_STATUS=0
begin
update mm.viajes
set kgdisponibles=kgdisponibles-@cantidad
where id=@viaje

fetch next from micursor into @viaje,@cantidad

end
close micursor
deallocate micursor
commit


go

create  function mm.viajesDisponibles(@fecha varchar(15),@origen varchar(30),@destino varchar(30))
returns @jaja table
(
idViaje int,
butacasLibres int,
kgLibres int,
tipoServicio varchar(15))
as
begin

declare @llegada datetime
set @llegada=convert(date,@fecha,20)
insert into @jaja

select v.Id,butacasDisponibles,kgDisponibles,t.Descripcion from mm.viajes v join mm.Rutas_Aereas r on r.Id=v.Ruta join mm.Tipos_Servicio t on t.Id=r.Tipo_servicio join mm.Ciudades d on d.id=r.Ciudad_Destino join mm.Ciudades o on o.Id=r.Ciudad_Origen
where datepart(dayofyear,v.fecha_salida) = datepart(dayofyear,@llegada) and year(v.Fecha_salida)=year(@llegada) and d.Descripcion=@destino and o.Descripcion=@origen
and (kgDisponibles>0 or butacasDisponibles>0) 
 return 
 end

 go


create procedure mm.ingresarCompraPasaje @viaje int,@cliente int,@butaca int,@codigoCompra int --vamos a tener que agregar un codigo de compra
as 

insert into mm.pasajes(Viaje,Numero_Butaca,Fecha_Compra,Cliente,cod_compra) values (@viaje,@butaca,mm.fechaDeHoy(),@cliente,@codigoCompra)


go

create procedure mm.ingresarCompraPaquete @viaje int,@cliente int,@kg int,@compra int
as 
insert into mm.paquetes(viaje,kg,Fecha_Compra,Cliente,cod_compra) values(@viaje,@kg,mm.fechaDeHoy(),@cliente,@compra)
go

create function mm.butacasDisponibles(@viaje int) 
returns 
@jaja table
(nroButaca int,
tipoButaca varchar(20))
as
begin
insert into @jaja
select Nro,b.Ubicacion from mm.Butacas b where Estado<>'Vendida' and viaje=@viaje
return
end


go

create  table mm.TC(
NRO_TC numeric(18) primary key,
cod_seg int,
anio_venc int,
mes_venc int 


)
go

create table mm.compras(
cod_compra int identity(1000000,1) primary key,
cliente int foreign key references mm.clientes,
TC numeric(18) foreign key references mm.TC


)
go

create  function mm.ultimacompra() returns int
as
begin 
declare @a int
select @a=max(cod_compra) from mm.compras
return @a
end

go

create procedure mm.nuevaCoompra
as
insert into mm.compras(cliente) values(null)

go

