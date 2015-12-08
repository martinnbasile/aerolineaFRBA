Use GD2C2015
go
--create index unNombrePonelee on gd_esquema.Maestra(cli_dni)
--go
create schema MM
go

create  procedure mm.nuevaCancelacion @motivo varchar(200)
as
insert into mm.Cancelaciones(Motivo,Fecha) values (@motivo,mm.fechaDeHoy())
go


create  function mm.ultimacancelacion() returns int
as
begin 
declare @a int
select @a=max(Cod_CAncelacion) from mm.Cancelaciones
return @a
end

go
create procedure mm.cancelarPasajesYPaquetesConViajeInhabilitado 
as
begin transaction
set transaction isolation level serializable

exec mm.nuevaCancelacion 'baja de viaje'

declare @canc int
set @canc=mm.ultimacancelacion()

select 
p.Id into #tempPas from
pasajes p join viajes v on p.Viaje=v.Id and v.estado<>'habilitado'

update mm.Pasajes
set cod_cancelacion=@canc
where Id in (select * from #tempPas)
drop table #tempPas


select 
p.Id into #tempPaq from
paquetes p join viajes v on p.Viaje=v.Id and v.estado<>'habilitado'
update mm.Paquetes
set cod_cancelacion=@canc
where Id in (select * from #tempPaq)

drop table #tempPaq


commit

go

create procedure mm.inhabilitarViajesConRutasInhabilitadas
as
begin transaction

delete b  from mm.Rutas_aereas r join mm.viajes v on v.ruta=r.id  join mm.butacas b on b.viaje=v.id
where r.Estado=2


 
select v.id into #temporal from mm.viajes v join mm.rutas_aereas r on v.ruta=r.id and r.Estado=2


update mm.Viajes
set estado='inhabilitado'
where Id in(select * from #temporal)
drop table #temporal
exec mm.cancelarPasajesYPaquetesConViajeInhabilitado
commit
go

Create Procedure MM.limpiarBase as

drop procedure mm.eliminarruta
drop procedure mm.cancelacionPaquete
drop procedure mm.cancelacionPasaje
drop procedure mm.nuevaCancelacion
drop function mm.ultimacancelacion
drop procedure mm.crearAeronave
drop procedure mm.inhabilitarViajesConRutasInhabilitadas
drop procedure mm.cancelarPasajesYPaquetesConViajeInhabilitado
drop procedure mm.crearModeloAvion
drop function mm.semestre
drop function mm.maximosMilleros
drop function mm.DestinosMasVendidosPasajes
drop function mm.DestinosMasCancelados
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
drop function MM.AeronavesMasDiasFueraServicio
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
drop table mm.compras
drop table mm.TC
drop function mm.ultimacompra
drop procedure mm.nuevaCompra
Drop table MM.Roles_Funcionalidades
Drop table MM.Productos_Milla
Drop table MM.Intentos_login
Drop table MM.Usuario_rol
Drop table MM.Viajes
Drop table MM.Rutas_Aereas
Drop table MM.Ciudades 
Drop table MM.Clientes_Repetidos 
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
drop procedure mm.generarViaje
drop function mm.aeronavesDisponibles
drop procedure mm.limpiarBase
DROP FUNCTION MM.BUTACASDISPONIBLES
DROP FUNCTION MM.VIAJESDISPONIBLES
drop function MM.paquetesCancelables
drop function MM.pasajesCancelables
drop procedure mm.ingresarCompraPaquete
drop procedure mm.ingresarCompraPasaje
drop procedure mm.asentarCompra
drop function mm.DestinosAeronavesMenosButacasVendidos
drop procedure mm.ingresarTC
drop function mm.modelosValidos

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

Create  table MM.Viajes(
Id int identity(1,1) primary key ,
Matricula varchar(10) foreign key references MM.Aeronaves(Matricula) on update cascade, 
estado varchar(15) default 'habilitado',
Ruta int,
constraint Ruta foreign key (Ruta) references MM.Rutas_Aereas(Id),
kgDisponibles int,
butacasDisponibles int,
Fecha_Salida date not null,
Fecha_Estimada_llegada date,
Fecha_llegada date,

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
precio_paquete float,
cod_compra int,
cod_cancelacion int)
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
precioPasaje float,
cod_compra int,
cod_cancelacion int)
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
check (Cantidad>=0),
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
fecha datetime,
total float,
medioPago varchar(10)
 


)
go
create table MM.Cancelaciones(
Cod_CAncelacion int identity(100000,1) primary key,

Fecha smalldatetime not null,
Motivo varchar(200)
)

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
insert into MM.Tipos_Servicio(Descripcion,Porcentaje) select distinct Tipo_Servicio,round( Pasaje_Precio/Ruta_Precio_BasePasaje,2) from gd_esquema.Maestra where Pasaje_Precio<>0
go

insert into MM.Ciudades(Descripcion)
select distinct Ruta_Ciudad_Destino from gd_esquema.Maestra union
select distinct Ruta_Ciudad_Origen from gd_esquema.Maestra
go


insert into mm.modeloAvion(Modelo_descripcion,Kg,fabricante,tipoServicio)
select distinct Aeronave_Modelo,Aeronave_KG_Disponibles,f.Id,s.Id from gd_esquema.Maestra as m join mm.Fabricantes as f on f.Descripcion=m.Aeronave_Fabricante join mm.Tipos_Servicio as s on s.Descripcion=m.Tipo_Servicio 
go
insert into mm.Butacas_Avion (butacaNum,butacaPiso,butacaTipo,modeloAvion)
select distinct  ma.Butaca_Nro,ma.Butaca_Piso,ma.Butaca_Tipo,mo.id
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
where Cli_Dni<>23718649
group by Cli_Dni,Cli_Nombre,Cli_Apellido,Cli_Dir,Cli_Telefono,Cli_Mail,Cli_Fecha_Nac)
go

Create  Table MM.Clientes_Repetidos(
DNI numeric(20) not null,
Nombre varchar(40) not null,
Apellido varchar(40) not null,
Direccion varchar(60) not null,
Telefono numeric(30) not null,
Mail varchar(50) not null,
Fecha_Nacimiento smalldatetime not null,
Fecha_prox_vencimiento smalldatetime
)
go



insert into MM.Clientes_Repetidos(DNI,Nombre,Apellido,Direccion,Telefono,Mail,Fecha_Nacimiento,Fecha_prox_vencimiento) 
(select 
Cli_Dni,Cli_Nombre,Cli_Apellido,Cli_Dir,Cli_Telefono,Cli_Mail,Cli_Fecha_Nac,dateadd(year,1,max(FechaLLegada)) from gd_esquema.Maestra
where Cli_Dni=23718649
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
add Estado int not null default 1
go

--alter table mm.rutas_aereras add default 1
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

insert into MM.Viajes(Matricula,ruta,fECHA_SALIDA,Fecha_Estimada_llegada,fecha_llegada,kgdisponibles,butacasdisponibles)
select Aeronave_Matricula,r.Id,
FechaSalida,Fecha_LLegada_Estimada,FechaLlegada,Aeronave_KG_Disponibles-sum(paquete_KG),0
from gd_esquema.Maestra g join MM.Rutas_Aereas as r on g.Ruta_Codigo=r.Ruta_Codigo join MM.Tipos_Servicio t on t.Id=r.Tipo_Servicio and g.Tipo_Servicio=t.Descripcion join mm.Ciudades o on g.Ruta_Ciudad_Origen=o.Descripcion and r.Ciudad_Origen=o.Id join mm.Ciudades c on c.Descripcion=g.Ruta_Ciudad_Destino and r.Ciudad_Destino=c.Id
group by Aeronave_Matricula,
FechaSalida,FechaLLegada,Fecha_LLegada_Estimada,r.Id,Aeronave_KG_Disponibles
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

insert into MM.Pasajes(Viaje,Numero_Butaca,Fecha_Compra,Cliente,PrecioPasaje)
select distinct b.Id,a.Butaca_Nro,Pasaje_FechaCompra,d.Id,Pasaje_Precio  from gd_esquema.Maestra as a join MM.Viajes as b on 
b.Fecha_Estimada_llegada=a.Fecha_LLegada_Estimada and b.Fecha_llegada=a.FechaLLegada and b.Fecha_salida=a.FechaSalida and 
b.Matricula=a.Aeronave_Matricula join MM.Rutas_Aereas as c on b.Ruta=c.Id and c.Precio_Base=a.Ruta_Precio_BasePasaje 
/*and c.Precio_Kg=a.Ruta_Precio_BaseKG */join MM.Tipos_Servicio as g on  c.Tipo_Servicio=g.Id and g.Descripcion =a.Tipo_Servicio 
join MM.Clientes as d on d.DNI=a.Cli_Dni join MM.Ciudades as e on c.Ciudad_Destino=e.Id and e.Descripcion=a.Ruta_Ciudad_Destino 
join MM.Ciudades as f on c.Ciudad_Origen=f.Id and f.Descripcion=a.Ruta_Ciudad_Origen
where Pasaje_codigo <>0
group by b.Id,Butaca_Nro,Pasaje_fechaCompra,d.Id,Pasaje_Precio

insert into MM.Paquetes(Viaje,Kg,Fecha_Compra,Cliente,precio_Paquete)
select distinct b.Id,a.Paquete_KG,Paquete_FechaCompra,d.Id,Paquete_Precio  from gd_esquema.Maestra as a join MM.Viajes as b on b.Fecha_Estimada_llegada=a.Fecha_LLegada_Estimada 
and b.Fecha_llegada=a.FechaLLegada and b.Fecha_salida=a.FechaSalida and b.Matricula=a.Aeronave_Matricula join MM.Rutas_Aereas 
as c on b.Ruta=c.Id /*and c.Precio_Base=a.Ruta_Precio_BasePasaje*/ and c.Precio_Kg=a.Ruta_Precio_BaseKG join MM.Tipos_Servicio as 
g on  c.Tipo_Servicio=g.Id and g.Descripcion =a.Tipo_Servicio join MM.Clientes as d on d.DNI=a.Cli_Dni join MM.Ciudades as e 
on c.Ciudad_Destino=e.Id and e.Descripcion=a.Ruta_Ciudad_Destino join MM.Ciudades as f on c.Ciudad_Origen=f.Id and 
f.Descripcion=a.Ruta_Ciudad_Origen
where Paquete_KG != 0
group by b.Id,a.Paquete_KG,Paquete_fechaCompra,d.Id,Paquete_Precio

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
select a.Fecha_alta as 'Fecha de alta',  mo.Modelo_descripcion as 'Modelo',a.matricula as 'Matrícula',f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',a.fecha_baja_fuera_servicio as 'Fecha de fuera de servicio',a.fecha_alta_fuera_servicio as 'Fecha de reinicio de servicio',a.Fecha_Baja_Definitiva as 'Fecha de baja definitiva',mo.Kg as 'Cantidad de Kgs disponibles para realizar encomiendas', Count(DISTINCT ba.butacaPiso) as 'Cantidad de pisos',COUNT(ba.id) as 'Cantidad de asientos'
from MM.Aeronaves a join mm.modeloAvion mo on mo.id=a.modelo join MM.Fabricantes f on (mo.Fabricante=f.Id)
					join MM.Tipos_Servicio ts on (mo.tipoServicio=ts.Id)
					join MM.Butacas_Avion ba on (mo.id=ba.modeloAvion)				
where a.Fecha_Baja_Definitiva is null
					 group by Modelo_descripcion,f.Descripcion,ts.Descripcion,mo.Kg,a.fecha_alta,a.matricula,a.fecha_baja_fuera_servicio,a.fecha_alta_fuera_servicio,a.fecha_baja_definitiva
go


create view MM.vista_modelos as
select mo.id, mo.Modelo_descripcion as 'Modelo',f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',mo.Kg as 'Cantidad de Kgs disponibles para realizar encomiendas', Count(DISTINCT ba.butacaPiso) as 'Cantidad de pisos',COUNT(ba.id) as 'Cantidad de asientos'
from mm.modeloAvion mo join MM.Fabricantes f on (mo.Fabricante=f.Id)
					   join MM.Tipos_Servicio ts on (mo.tipoServicio=ts.Id)	
					   join MM.Butacas_Avion ba on (mo.id=ba.modeloAvion)	
					   group by mo.id,mo.Modelo_descripcion,f.Descripcion,ts.Descripcion,mo.Kg		
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

create function MM.fechaDeHoy()
returns date
as begin

return (select top 1 fecha from MM.Fecha order by orden desc)
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
		or  v.Fecha_Estimada_llegada between @fechaBaja and @fechaAlta or @fechaAlta between v.Fecha_llegada and v.Fecha_Estimada_llegada) 
		and a.matricula=v.Matricula
	)
	and a.fecha_baja_definitiva is null
	and (((a.fecha_baja_fuera_servicio not between @fechaBaja and @fechaAlta) and (a.fecha_alta_fuera_servicio not between @fechaBaja and @fechaAlta) and @fechaBaja not between a.fecha_baja_fuera_servicio and a.fecha_alta_fuera_servicio) or ((a.fecha_baja_fuera_servicio is null) and (a.fecha_alta_fuera_servicio is null)) )
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


create procedure MM.actualizarFecha @fecha varchar(10) 
as
begin
if exists(select fecha from mm.fecha)
begin
update MM.Fecha set fecha=(MM.convertirFecha(@fecha))
end
else
begin 
insert into mm.fecha (fecha) values(MM.convertirFecha(@fecha))
end

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

  
  
  delete b from MM.Butacas b join mm.viajes as v on v.id=b.viaje 
where v.Fecha_salida>=mm.fechaDeHoy() and v.Matricula=@matricula

  update MM.Viajes 
  set estado='inhabilitado'
  where Matricula=@matricula and Fecha_salida>=mm.fechaDeHoy()
  
exec cancelarPasajesYPaquetesConViajeInhabilitado


  UPDATE MM.Aeronaves set fecha_baja_definitiva=mm.fechaDeHoy() where matricula=@matricula 	  
  
  COMMIT TRAN
  go

  create index choto on mm.viajes(Matricula)
  go
  create index chotos on mm.viajes(Fecha_salida)
  go


create procedure MM.CancelarAeronaveFueraDeServicio(@matricula varchar(10), @hasta datetime)
  as
  BEGIN TRAN
  declare

  @dia datetime
  set @dia=mm.fechaDeHoy()

  delete from MM.Butacas where Viaje in (select Id from MM.Viajes
  where Fecha_salida>=@dia and Fecha_salida<=@hasta and Matricula=@matricula)
update   MM.Viajes
set estado='inhabilitado'
 where Matricula=@matricula and Fecha_salida>=@dia and Fecha_salida<=@hasta

exec cancelarPasajesYPaquetesConViajeInhabilitado
  UPDATE MM.Aeronaves set fecha_baja_fuera_servicio=@dia, Fecha_alta_fuera_servicio=@hasta
  where matricula=@matricula
  COMMIT TRAN
  go

create   procedure MM.BorrarCiudades(
@Descripcion varchar(100))
as
begin tran
declare @id int
select @id=(select Id from MM.Ciudades where Descripcion= @Descripcion)
update MM.Ciudades set Estado='Deshabilitado' where Id= @id
update MM.Rutas_Aereas set Estado=2 where Ciudad_Destino=@id or Ciudad_Origen=@id
exec mm.inhabilitarViajesConRutasInhabilitadas
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
select Cliente,cast(pas.precioPasaje as int)/10,MM.fechaDeHoy(),'COMPRA PASAJE' from MM.Pasajes pas join MM.Viajes v on v.Id=pas.Viaje and v.Id=@viaje left join mm.Cancelaciones j on pas.cod_cancelacion=j.Cod_CAncelacion where j.Cod_CAncelacion is null-- where pas.Id not in (select Codigo_Pasaje from MM.Cancelaciones)
union
select paq.Cliente,cast(paq.precio_paquete as int)/10,MM.fechaDeHoy(),'COMPRA PAQUETE' from MM.Paquetes paq join MM.Viajes v on v.Id=paq.Viaje and v.Id=@viaje  left join mm.Cancelaciones j on paq.cod_cancelacion=j.Cod_CAncelacion where j.Cod_CAncelacion is null --where paq.Id not in (select Codigo_Encomienda from MM.Cancelaciones) 


commit
go


create  procedure MM.asentarLLegadaAeronave @avion varchar(10),@origenString varchar(50),@destinoString varchar(50),@horaLlegada varchar(20)
as
begin transaction
declare @hora datetime
declare @viaje int
declare @origen int
declare @destino int

select @origen=id from mm.ciudades where Descripcion=@origenString
select @destino=id from mm.ciudades where Descripcion=@destinoString
declare @horas int
declare @minutos int
declare @llegada datetime

set @llegada=(convert(datetime,@horaLlegada,20))
set @horas=datepart(hour,@llegada)
set @minutos=datepart(minute,@llegada)
set @hora=cast(MM.fechaDeHoy() as datetime)
set @hora=dateadd(hour,@horas,@hora)
(select @viaje=v.Id from MM.viajes v join MM.Rutas_Aereas r on v.Ruta=r.Id and r.Ciudad_Destino=@destino and r.Ciudad_Origen=@origen 
where 
Matricula=@avion and Fecha_Estimada_llegada between dateadd(hour,-1,@hora) and dateadd(hour,1,@hora) and v.estado='habilitado') 
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
	COMMIT
	end try

	begin catch
			raiserror ('No hay cantidad suficiente en stock',10,16)
			rollback
	end catch

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
(Description varchar(100),cantidad int)
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
select top 5 d.Descripcion,count(a.Id)
from MM.Pasajes a right join MM.Viajes b on a.Viaje=b.Id right join  mm.Rutas_Aereas c on b.Ruta=c.Id  and b.Fecha_salida 
between @anio+@desde and @anio+@hasta  right join MM.Ciudades d on c.Ciudad_Destino=d.Id
 
group by d.Descripcion 
order by count(a.Id) desc
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
create function mm.AeronavesMasDiasFueraServicio
(@semestre int, @anio char(4))

returns @table table (Description varchar(50),dias int)
as
begin

insert into @table
select top 5 a.matricula,count(b.aeronave)
from  mm.aeronaves a left join mm.logBajasAeronaves b on a.Matricula=b.aeronave
where ((year(fechabaja)=@anio and mm.semestre(FechaBaja)=@semestre) or b.aeronave is null)
group by a.MAtricula
order by count(*) desc

return
end

go

create function mm.DestinosMasCancelados
(@semestre int,
@anio char(4))
returns @table table
(Description varchar(100),
cantidad int)
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

select top 5 e.Descripcion,count(b.cod_cancelacion)
from MM.Pasajes b right join MM.Viajes c  on b.Viaje=c.Id  and  b.cod_cancelacion is not null   right join MM.Rutas_Aereas d on c.Ruta=d.Id and 
		  c.Fecha_salida 
between @anio+@desde and @anio+@hasta  right join MM.Ciudades e on d.Ciudad_Destino=e.Id
	
		
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
@tablita table(dni numeric(20), nombre varchar(30), apellido varchar(30),cantMillas int)
as
begin
insert into @tablita 
select top 5 c.dni,c.nombre, c.apellido, sum(isnull(m.millas,0))

from mm.millas m right join mm.clientes c on (m.cliente=c.id) and m.millas>0 and year(m.Fecha_movimiento)=@anio and (1+(month(m.Fecha_movimiento)-1)/6)=@semestre
 
group by c.nombre,c.apellido,c.dni


order by sum(isnull(m.millas,0)) DESC
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

create   trigger mm.actualizarTablaLog on MM.Aeronaves
after update
as
begin
	
	insert into mm.logBajasAeronaves (aeronave,fechaBaja,fechaAlta)
	Select i.matricula, i.fecha_baja_fuera_servicio, i.fecha_alta_fuera_servicio
	from inserted i join deleted d on (i.matricula=d.matricula and i.fecha_alta_fuera_servicio>=mm.fechaDeHoy() and d.fecha_alta_fuera_servicio<mm.fechaDeHoy())
	
	declare unCursor cursor for
	select id, fecha_alta_fuera_servicio 
	from mm.logBajasAeronaves l join inserted i on 
	(l.aeronave=i.matricula and l.fechaBaja=i.fecha_baja_fuera_servicio
	and l.fechaBaja<i.fecha_baja_fuera_servicio)
	declare @id int
	declare @fechaR DateTime

	open unCursor

	fetch next from unCursor into @id,@fechaR
	while (@@fetch_status=0)
	begin
		update mm.logBajasAeronaves
		set fechaAlta = @fechaR
		where id = @id
	fetch next from unCursor into @id,@fechaR
	
	end
	close unCursor
	deallocate unCursor
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


create procedure mm.crearRuta @destino varchar(30),@origen varchar(30),@servicio varchar(20),@basePasaje int,@baseKg int
as insert into mm.Rutas_Aereas(Ciudad_Destino,Ciudad_Origen,Tipo_Servicio,Precio_Base,Precio_Kg)
select d.Id,o.Id,t.Id,@basePasaje,@baseKg from mm.Tipos_Servicio as t,MM.Ciudades as o,mm.Ciudades as d
where t.Descripcion=@servicio and o.Descripcion=@origen and d.Descripcion=@destino

go


create procedure mm.actualizarRuta @id int,@destino varchar(30),@origen varchar(30),@servicio varchar(20),@basePasaje int,@baseKg int
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
set @llegada=convert(datetime,@fechaLlegada,20)
set @salida=convert(datetime,@fechaSalida,20)
declare @kg int
declare @but int
select @but=count(*),@kg=mo.Kg from mm.aeronaves a join mm.modeloAvion mo on mo.id=a.modelo and a.matricula=@matricula join mm.Butacas_Avion b on b.modeloAvion=mo.id
group by mo.Kg
insert into mm.Viajes(Matricula,Ruta,Fecha_salida,Fecha_Estimada_llegada,KgDisponibles,ButacasDisponibles) values (@matricula,@ruta,@salida,@llegada,@kg,@but)
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
set @llegada=convert(datetime,@fechaLlegada,20)
set @salida=convert(datetime,@fechaSalida,20)

insert into @tabla
select a.matricula  from mm.aeronaves a left join mm.Viajes v on v.Matricula=a.matricula  and ((v.Fecha_Estimada_llegada between @salida and @llegada  ) or (@salida between v.Fecha_salida and v.Fecha_Estimada_llegada) or (v.Fecha_salida between @salida and @llegada  ) and v.estado='habilitado' )left join mm.modeloAvion m on m.id=a.modelo left join mm.Tipos_Servicio t on t.Id=m.tipoServicio
where t.Descripcion=@TipoServicio  
and (a.fecha_baja_definitiva >@llegada or a.fecha_baja_definitiva is null)
group by a.Matricula,v.id
having v.id is  null

return 
end
go


create trigger mm.decrementarButacas on mm.Pasajes
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
create trigger mm.decrementarKg on mm.Paquetes
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
tipoServicio varchar(15),
precioPasaje float,
precioPaquete float)
as
begin

declare @llegada datetime
set @llegada=convert(date,@fecha,20)
insert into @jaja

select v.Id,butacasDisponibles,kgDisponibles,t.Descripcion,r.Precio_Base*t.Porcentaje,r.Precio_Kg from mm.viajes v join mm.Rutas_Aereas r on r.Id=v.Ruta join mm.Tipos_Servicio t on t.Id=r.Tipo_servicio join mm.Ciudades d on d.id=r.Ciudad_Destino join mm.Ciudades o on o.Id=r.Ciudad_Origen
where datepart(dayofyear,v.fecha_salida) = datepart(dayofyear,@llegada) and year(v.Fecha_salida)=year(@llegada) and d.Descripcion=@destino and o.Descripcion=@origen
and (kgDisponibles>0 or butacasDisponibles>0) and v.estado='habilitado' 
 return 
 end

 go


create procedure mm.ingresarCompraPasaje @viaje int,@dni int,@butaca int,@codigoCompra int,@precio float --vamos a tener que agregar un codigo de compra
as 
begin
declare @cliente int
set @cliente=(select max(Id) from mm.clientes where DNI=@dni)

insert into mm.pasajes(Viaje,Numero_Butaca,Fecha_Compra,Cliente,cod_compra,precioPasaje) values (@viaje,@butaca,mm.fechaDeHoy(),@cliente,@codigoCompra,@precio)

end
go

create procedure mm.ingresarCompraPaquete @viaje int,@dni int,@kg int,@compra int,@precio float
as
begin
declare @cliente int
set @cliente=(select max(Id) from mm.clientes where DNI=@dni)

insert into mm.paquetes(viaje,kg,Fecha_Compra,Cliente,cod_compra,Precio_paquete) values(@viaje,@kg,mm.fechaDeHoy(),@cliente,@compra,@precio)
end
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

create  function mm.ultimacompra() returns int
as
begin 
declare @a int
select @a=max(cod_compra) from mm.compras
return @a
end

go

create procedure mm.nuevaCompra as
insert into mm.compras(fecha) values(mm.fechaDeHoy())

go

/*
create procedure MM.vista_pasajes_cancelables (@idCompra int)
AS
BEGIN TRAN
select 
pc.cod_pasaje as 'Codigo de pasaje', ra.Precio_Base as 'Precio',c1.Descripcion as 'Origen',c2.Descripcion as 'Destino',
v.Fecha_salida as 'Fecha salida',v.Fecha_llegada as 'Fecha llegada',pc.butaca_nro as 'Numero de butaca',
ts.Descripcion as 'Tipo de servicio',ba.butacaTipo as 'Tipo de Asiento'
from MM.pasajesCancelables(@idCompra) pc
join MM.Viajes v on pc.Viaje=v.Id 
join MM.Rutas_Aereas ra on v.Ruta=ra.Id
join MM.Butacas bu on bu.Nro=pc.butaca_nro
join MM.Butacas_Avion ba on bu.Nro=ba.butacaNum 
join MM.Tipos_Servicio ts on ra.Tipo_Servicio=ts.Id
join MM.Ciudades c1 on ra.Ciudad_Origen=c1.Id
join MM.Ciudades c2 on ra.Ciudad_Destino=c2.Id
COMMIT TRAN

go

create PROCEDURE MM.vista_paquetes_cancelables @idCompra int
AS
BEGIN TRAN
select
pc.cod_paquete as 'Codigo de paquete',pc.Kg as 'Kilogramos',ra.Precio_Kg 'Precio por Kg', pc.kg * ra.Precio_Kg as 'Precio total' ,
c1.Descripcion as 'Origen',c2.Descripcion as 'Destino',
v.Fecha_salida as 'Fecha salida',v.Fecha_llegada as 'Fecha llegada'
from MM.paquetesCancelables(@idCompra) pc
join MM.Viajes v on pc.Viaje=v.Id 
join MM.Rutas_Aereas ra on v.Ruta=ra.Id 
join MM.Ciudades c1 on ra.Ciudad_Origen=c1.Id
join MM.Ciudades c2 on ra.Ciudad_Destino=c2.Id
COMMIT TRAN

go
*/
/*

CREATE procedure MM.cancelarCompraPasaje @codigoCompra int,@butaca int, @motivo varchar
AS
BEGIN TRANSACTION
	DECLARE @idPasaje int		
	SELECT @idPasaje = id from MM.pasajes p where p.cod_compra=@codigoCompra and p.Numero_Butaca=@butaca
	INSERT INTO MM.Cancelaciones (Codigo_Pasaje,Fecha,Motivo) values (@idPasaje,MM.fechaDeHoy(),@motivo)
COMMIT TRANSACTION

go
CREATE procedure MM.cancelarCompraPaquete @codigoCompra int, @motivo varchar
AS
BEGIN TRANSACTION
	DECLARE @idPaquete int		
	SELECT @idPaquete = id from MM.Paquetes p where p.cod_compra=@codigoCompra 
	INSERT INTO MM.Cancelaciones (Codigo_Encomienda,Fecha,Motivo) values (@idPaquete,MM.fechaDeHoy(),@motivo)
COMMIT TRANSACTION

go
*/

create function mm.paquetesCancelables (@codCompra int)
returns @mitabla table(
cod_paquete int,
viaje int,
kg int,
precioPaquete float)
as 
begin
insert into @mitabla
select Id,viaje,kg,precio_paquete from mm.paquetes
where cod_compra=@codCompra and cod_cancelacion is null
return 
end

go
create function mm.pasajesCancelables (@codCompra int)
returns @mitabla table(
cod_pasaje int,
viaje int,
butaca_nro int,
precioPasaje float)
as 
begin
insert into @mitabla
select Id,viaje,Numero_Butaca,precioPasaje from mm.pasajes
where cod_compra=@codCompra and cod_cancelacion is null
return 
end

go

create procedure mm.cancelacionPaquete @codPaquete int,@codCancelacion int
as
declare @viaje int
declare @kg int
update mm.Paquetes
set cod_cancelacion=@codCancelacion,@viaje=viaje,@kg=kg
where Id=@codPaquete

update mm.Viajes set kgDisponibles=kgDisponibles+@kg where Id=@viaje
go


create procedure mm.cancelacionPasaje @codPasaje int,@codCancelacion int
as
declare @viaje int
declare @num int
update mm.Pasajes
set cod_cancelacion=@codCancelacion,@viaje=viaje,@num=numero_butaca
where Id=@codPasaje
update mm.Viajes
set butacasDisponibles=butacasDisponibles+1
where Id=@viaje
update mm.Butacas
set Estado='Libre'
where Viaje=@viaje and Nro=@num
go


create trigger mm.noPuedeHaber2PasajesAlMismoTiempo on mm.Pasajes
instead of insert
as
begin transaction
if(exists(select v1.Id 
from mm.Pasajes as p1 
 
join mm.Viajes v1 on p1.viaje=v1.Id 
join inserted p2 on p2.cliente=p1.Cliente  
join mm.viajes as v2 on v2.Id=p2.viaje 
and
(v1.Fecha_salida between v2.Fecha_salida and v2.Fecha_Estimada_llegada or v1.Fecha_Estimada_llegada between v2.Fecha_salida and v2.Fecha_Estimada_llegada or (v2.Fecha_salida between v1.Fecha_salida and v1.Fecha_Estimada_llegada))
and p1.cod_cancelacion is null)) 
begin 
	raiserror ('Un Pasajero no puede estar haciendo 2 viajes a la vez',16,150)
	rollback
end
else

begin
insert into mm.Pasajes(Cliente,cod_cancelacion,cod_compra,Fecha_Compra,Numero_Butaca,precioPasaje,Viaje)
select Cliente,cod_cancelacion,cod_compra,Fecha_Compra,Numero_Butaca,precioPasaje,viaje from inserted
commit
end
go





create procedure mm.ingresarTC @nro numeric(18),@cod int,@anio int,@mes int
as
begin

if(exists(select NRO_TC from mm.TC where NRO_TC=@nro))
return
else insert into mm.TC values(@nro,@cod,@anio,@mes)

end
go

create procedure mm.asentarCompra @codCompra int, @dni int, @total float, @medioPago varchar(10)
as

begin

update mm.compras
set cliente=(select id from mm.clientes where dni=@dni),
total=@total,
medioPago=@medioPago

where cod_compra=@codCompra
end
go


--alter table gd_esquema.Maestra add id int identity(1,1) primary key

/*
select a.Cli_Dni,count(distinct b.cli_apellido) from gd_esquema.maestra as a  join gd_esquema.maestra as b on a.cli_dni=b.cli_dni and b.id<=a.id

group by a.Cli_Dni
 order by 2 desc
 */

 create function [MM].[DestinosAeronavesMenosButacasVendidos]
(@semestre int,
@anio char(4))
returns @table table
(Description varchar(100),
vacias int)
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

select top 5 D.Descripcion,sum(isnull(b.butacasDisponibles,0)) 
as vacias
from
MM.Viajes B right join
MM.Rutas_Aereas C on B.Ruta=C.Id  right join
MM.Ciudades D on C.Ciudad_Destino=D.Id 
and b.Fecha_salida
between @anio+@desde and @anio+@hasta

group by D.Descripcion
order by sum(isnull(B.butacasDisponibles,0)) desc
return
end

GO

create  trigger mm.noPuedeHaber2ViajesAlMismoTiempo on mm.Viajes
instead of insert
as
begin transaction
set transaction isolation level serializable
if(exists(select v1.Id 
 
from mm.Viajes v1 

join inserted as v2 on v2.Matricula=v1.Matricula
and
(v1.Fecha_salida between v2.Fecha_salida and v2.Fecha_Estimada_llegada or v1.Fecha_Estimada_llegada between v2.Fecha_salida and v2.Fecha_Estimada_llegada or v2.Fecha_salida between v1.Fecha_salida and v1.Fecha_Estimada_llegada)
))
begin 
	raiserror ('Un avion no puede estar haciendo 2 viajes a la vez',16,150)
	rollback
end
else
begin
insert into mm.viajes(mATRICULA,ruta,kgDisponibles,butacasDisponibles,Fecha_salida,fecha_llegada,fecha_estimada_llegada) select mATRICULA,ruta,kgDisponibles,butacasDisponibles,Fecha_salida,fecha_llegada,fecha_estimada_llegada from inserted
commit
end
go


create procedure mm.eliminarRuta @ruta int
as
begin transaction
update MM.Rutas_Aereas set Estado=2 where Id=@ruta
exec mm.inhabilitarViajesConRutasInhabilitadas
commit

go

create function [MM].[modelosValidos](@matricula varchar(10))
returns @tabla table
(modelo int)
as
begin

declare @modeloAeronave int
set @modeloAeronave= (select a.modelo from mm.aeronaves a where a.matricula=@matricula)
declare @cantidadMinimaKg int
set @cantidadMinimaKg = (select ma.Kg from mm.Aeronaves a INNER JOIN mm.modeloAvion ma on a.modelo=ma.id where a.matricula=@matricula)
declare @cantidadMinimaPasillo  int
set @cantidadMinimaPasillo = (select count(ba.butacaTipo) from mm.Aeronaves a INNER JOIN mm.modeloAvion ma on a.modelo=ma.id INNER JOIN mm.Butacas_Avion ba on ba.modeloAvion=ma.id where a.matricula=@matricula and ba.butacaTipo='Pasillo')
declare @cantidadMinimaVentanilla int
set @cantidadMinimaVentanilla= (select count(ba.butacaTipo) from mm.Aeronaves a INNER JOIN mm.modeloAvion ma on a.modelo=ma.id INNER JOIN mm.Butacas_Avion ba on ba.modeloAvion=ma.id where a.matricula=@matricula and ba.butacaTipo='Ventanilla')
declare @tipoDeServicio int
set @tipoDeServicio= (select ma.tipoServicio from mm.Aeronaves a INNER JOIN mm.modeloAvion ma on a.modelo=ma.id where a.matricula=@matricula)



declare @tablaPasillo table (modelo int,cantidadPasillo int)
insert into @tablaPasillo
select mo.id,count(mo.id) from mm.modeloAvion mo
INNER JOIN
MM.Butacas_Avion ba on (mo.id=ba.modeloAvion)
where ba.butacaTipo='Pasillo'
group by mo.id

declare @tablaVentanilla table (modelo int,cantidadVentanilla int)
insert into @tablaVentanilla
select mo.id,count(mo.id) from mm.modeloAvion mo
INNER JOIN
MM.Butacas_Avion ba on (mo.id=ba.modeloAvion)
where ba.butacaTipo='Ventanilla'
group by mo.id

insert into @tabla
select tp.modelo from @tablaPasillo tp
INNER JOIN
@tablaVentanilla tv on tp.modelo=tv.modelo
INNER JOIN 
MM.modeloAvion on tp.modelo=modeloAvion.id
where tp.cantidadPasillo>=@cantidadMinimaPasillo 
and tv.cantidadVentanilla>=@cantidadMinimaVentanilla 
and modeloAvion.Kg>=@cantidadMinimaKg
and modeloAvion.tipoServicio=@tipoDeServicio
and modeloAvion.id!=@modeloAeronave
return 
end

go







