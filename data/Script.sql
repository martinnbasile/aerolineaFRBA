Use GD2C2015
go
drop procedure limpiarBase
go
Create Procedure limpiarBase as
Drop table Butacas
Drop table Cambios_Millas
Drop table Cancelaciones
Drop table Intentos_Fallidos
Drop table Inhabilitados
Drop table Millas
Drop table Paquetes
Drop table Pasajes
Drop table Roles_Funcionalidades
Drop table Tarjetas_Credito
Drop table Productos_Milla
Drop table Funcionalidades
Drop table Intentos_login
Drop table KG
Drop table Usuario_rol
Drop table Viajes
Drop table Rutas_Aereas
Drop table Aeronaves 
Drop table Tipos_Servicio 
Drop table Fabricantes 
Drop table Ciudades 
Drop table tarjetas_Credito
Drop table Clientes 
Drop table Usuarios 
Drop table Roles 
go

exec limpiarBase
go

create Table Ciudades(
Id int identity(1,1) primary key,
Descripcion varchar(70) not null unique)
go
create Table Fabricantes(
Id int identity(1,1) primary key,
Descripcion varchar(70) not null unique)
go
create Table Tipos_Servicio(
Id int identity(1,1) primary key,
Descripcion varchar (70) not null unique,
Porcentaje float not null)
go
create Table Funcionalidades(
Id int identity(1,1) primary key,
Descripcion varchar(70) not null unique)
go
create Table Roles(
Id int identity(1,1) primary key,
Descripcion varchar(70) not null unique,
Funcionalidad int)
go
Alter table Roles 
add constraint FK_Funcionalidad FOREIGN KEY (Funcionalidad) references Funcionalidades(Id) 
go
create Table Usuarios(
Id int identity(1,1) primary key,
Username varchar(50) not null unique,
Password varchar(256) not null,
Rol int,
Estado varchar(10))
go
Alter table Usuarios
add constraint FK_Rol FOREIGN KEY (Rol) references Roles(Id)
go
create Table Intentos_Fallidos(
Id_User int,
cantidad int)
go
Alter table Intentos_fallidos
add constraint FK_User FOREIGN KEY (Id_User) references Usuarios(Id) 
go
create Table Inhabilitados(
Id_User int)
go
Alter table Inhabilitados
add constraint FK_User_Inahilitado FOREIGN KEY (Id_User) references Usuarios(Id)
go
Create Table Clientes(
Id int identity(1,1) primary key,
DNI numeric(20) unique not null,
Nombre varchar(40) not null,
Apellido varchar(40) not null,
Direccion varchar(60) not null,
Telefono numeric(30) not null,
Mail varchar(50) not null,
Fecha_Nacimiento smalldatetime not null,
)
go
Create Table Rutas_Aereas(
Id int identity(1,1) primary key,
Ciudad_Destino int,
Ciudad_Origen int,
Tipo_Servicio int,
Precio_Base float not null,
Precio_Kg float not null)
go
Alter Table Rutas_Aereas
add constraint FK_Ciudad_Origen FOREIGN KEY (Ciudad_Origen) references Ciudades(Id)
go
Alter Table Rutas_Aereas
add constraint FK_Ciudad_Destino FOREIGN KEY (Ciudad_Destino) references Ciudades(Id)
go
Alter Table Rutas_Aereas
add constraint FK_Tipo_Servicio FOREIGN KEY (Tipo_Servicio) references Tipos_Servicio(Id)
go
alter table Usuarios
add Pregunta_Secreta varchar(50) not null
go
alter table Usuarios
add Respuesta varchar(256) not null
go
create table Aeronaves(
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
alter table Aeronaves
add constraint Fabricante foreign key (Fabricante) references Fabricantes(Id)  
go
alter table Aeronaves
add constraint Tipo_Servicio foreign key (Tipo_Servicio) references Tipos_Servicio(Id)
go
create table Butacas(
Matricula varchar(10) not null,
Numero int not null,
Tipo varchar(20) not null,
Piso int not null)
go
alter table Butacas
add constraint Matricula foreign key (Matricula) references Aeronaves(Matricula)
go
Create table Viajes(
Id int identity(1,1) ,
Matricula varchar(10),
Ruta int)
go
alter table Viajes
add constraint Id primary key (Id)
go
alter table Viajes
add constraint Matricula_Avion foreign key (Matricula) references Aeronaves(Matricula)
go
alter table Viajes
add constraint Ruta foreign key (Ruta) references Rutas_Aereas(Id)
go
alter table Rutas_Aereas
add Fecha_Salida datetime not null
go
alter table Rutas_Aereas
add Fecha_Estimada datetime not null
go
alter table Rutas_Aereas
add Fecha_llegada datetime not null
go
create table Paquetes(
Id int identity(1,1) primary key,
Viaje int,
Kg int not null,
Fecha_Compra smalldatetime,
Cliente int)
go
alter table Paquetes
add constraint Viaje2 foreign key (Viaje) references Viajes(Id)
go
alter table Paquetes
add constraint Cliente_Paquete foreign key (Cliente) references Clientes(Id)
go
Create table Pasajes(
Id int identity(1,1) primary key,
Viaje int,
Numero_Butaca int not null,
Fecha_Compra smalldatetime,
Cliente int)
go
alter table Pasajes
add constraint Viaje3 foreign key (Viaje) references Viajes(Id)
go
alter table Pasajes
add constraint Cliente_Pasaje foreign key (Cliente) references Clientes(Id)
go
create table Millas(
Id int identity(1,1),
Cliente int,
Millas int not null)
go
alter table Millas
add constraint Cliente_Millero foreign key (Cliente) references Clientes(Id)
go
alter table Millas
add constraint PK primary key (Id)
go
alter table Millas
add Fecha smalldatetime not null default getdate()
go
create table Productos_Milla(
Id int identity(1,1) primary key,
Descripcion varchar not null,
Cantidad int not null,
Millas_Necesarias int not null)
go
create table Cambios_Millas(
Id int identity(1,1) primary key,
Cliente int,
Producto int,
Cantidad int not null,
Fecha_Canje smalldatetime default getdate() )
go
alter table Cambios_Millas
add constraint Cliente_Cambiador foreign key (Cliente) references Clientes(Id)
go
alter table Cambios_Millas
add constraint Producto foreign key (Producto) references Productos_Milla(Id)
go
create table Cancelaciones(
Id int identity(1,1) primary key,
Codigo_Pasaje int,
Codigo_Encomienda int,
Fecha smalldatetime not null,
Motivo varchar(200))
go
alter table Cancelaciones
add constraint Pasaje foreign key (Codigo_Pasaje) references Pasajes(Id)
go
alter table Cancelaciones
add constraint Encomienda foreign key (Codigo_Encomienda) references Paquetes(Id)
go
create table Tarjetas_Credito(
Id int identity(1,1) primary key,
Cliente int,
Numero_Tarjeta numeric(20) not null,
Codigo_Seguridad numeric(10) not null,
Fecha_Vencimiento smalldatetime not null,
Tipo_Tarjeta varchar(30) not null)
go
alter table Tarjetas_Credito
add constraint Cliente_Tarjetero foreign key(Cliente) references Clientes(Id)
go
drop table Butacas
go
create table Butacas(
Viaje int not null,
Nro int not null,
Ubicacion varchar(30))
go
create table Usuario_rol(

cod_usuario int not null foreign key references Usuarios,
cod_rol int not null foreign key references Roles


)
go

insert into Fabricantes (Descripcion)
select distinct Aeronave_Fabricante from gd_esquema.Maestra
go
insert into Tipos_Servicio(Descripcion,Porcentaje) values ('Común',0)
go
insert into Tipos_Servicio(Descripcion,Porcentaje) values ('Ejecutivo',5)
go
insert into Tipos_Servicio(Descripcion,Porcentaje) values ('Cama',10)
go
insert into Tipos_Servicio(Descripcion,Porcentaje) values ('Semi-Cama',15)
go
insert into Tipos_Servicio(Descripcion,Porcentaje) values ('Premium',25)
go
insert into Ciudades(Descripcion)
select distinct Ruta_Ciudad_Destino from gd_esquema.Maestra union
select distinct Ruta_Ciudad_Origen from gd_esquema.Maestra
go
create table Roles_Funcionalidades(
Funcionalidad int,
Rol int)
go
alter table Roles
drop constraint FK_Funcionalidad
go
alter table Roles_funcionalidades
add constraint FK_Funcionalidad FOREIGN KEY(Funcionalidad) references Funcionalidades
go
alter table Roles
drop column Funcionalidad
go
alter table Roles_Funcionalidades
add constraint FK_Roles FOREIGN KEY(Rol) references Roles
go
alter table Productos_Milla
drop column Descripcion
go
alter table Productos_Milla
add Descripcion varchar(100) not null
go
insert into Productos_Milla (Descripcion,Cantidad,Millas_Necesarias)
values ('Auto de Juguete',100,20)
go
insert into Productos_Milla (Descripcion,Cantidad,Millas_Necesarias)
values ('Moto de Juguete',100,15)
go
insert into Productos_Milla (Descripcion,Cantidad,Millas_Necesarias)
values ('Avion de Juguete',100,25)
go
insert into Productos_Milla (Descripcion,Cantidad,Millas_Necesarias)
values ('Set de perfumes',100,200)
go

insert into Funcionalidades(Descripcion) values ('ABM ROL')
go
insert into Funcionalidades(Descripcion) values ('ABM CIUDADES')
go
insert into Funcionalidades(Descripcion) values ('ABM RUTAS')
go
insert into Funcionalidades(Descripcion) values ('ABM AERONAVES')
go
insert into Funcionalidades(Descripcion) values ('GENERAR VIAJE')
go
insert into Funcionalidades(Descripcion) values ('COMPRAS')
go
insert into Funcionalidades(Descripcion) values ('DEVOLUCIONES Y CANCELACIONES')
go
insert into Funcionalidades(Descripcion) values ('CONSULTAR MILLAS')
go
insert into Funcionalidades(Descripcion) values ('CANJEAR MILLAS')
go
insert into Funcionalidades(Descripcion) values ('LISTADO ESTADISTICO')
go
insert into Funcionalidades(Descripcion) values ('REGISTRAR LLEGADA A DESTINO')
go
insert into Roles(Descripcion) values ('Administrador')
go
insert into Roles(Descripcion) values ('Cliente')
go
alter table Clientes add Unique(DNI)
go
insert into Clientes(DNI,Nombre,Apellido,Direccion,Telefono,Mail,Fecha_Nacimiento) 
(select distinct
Cli_Dni,Cli_Nombre,Cli_Apellido,Cli_Dir,Cli_Telefono,Cli_Mail,Cli_Fecha_Nac from gd_esquema.Maestra)
go

insert into usuarios values ('admin','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',1,'OK','Sos Dios?','Algo a ver')
go
alter table Rutas_Aereas
drop column Fecha_Salida
go
alter table Rutas_Aereas
drop column Fecha_Estimada
go
alter table Rutas_Aereas
drop column Fecha_llegada
go
alter table Rutas_Aereas
add Estado int not null 
go
alter table Viajes
add Fecha_Salida date not null
go
alter table Viajes
add Fecha_Estimada_llegada date not null
go
alter table Viajes
add Fecha_llegada date
go 
create table KG(
Viaje int not null,
Kg int not null)
go
alter table KG
add constraint FK_Viajes FOREIGN KEY (Viaje) references Viajes(Id)
go
alter table Butacas
add constraint FK_Viajes_2 FOREIGN KEY (Viaje) references Viajes(Id)
go
insert into Aeronaves
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
Create table Intentos_login(
	Id_login int identity(1,1) primary key,
	Codigo_usuario int not null,
	Es_correcto bit not null,
	foreign key (Codigo_usuario) references Usuarios(Id)
)
go
alter table Intentos_Fallidos
drop constraint FK_User
go
drop table Intentos_Fallidos
go
Create table Intentos_fallidos(
	Id_fallido int identity(1,1) primary key,
	Cod_login int not null,
	foreign key (Cod_login) references Intentos_login(Id_login)
)
go
alter table Inhabilitados
add fecha datetime not null
go
alter table Viajes
drop column Fecha_Salida
go
alter table Viajes
drop column Fecha_Estimada_llegada
go
alter table Viajes
drop column Fecha_llegada
go
alter table Viajes
add Fecha_salida datetime not null
go
alter table Viajes
add Fecha_Estimada_llegada datetime not null
go
alter table Viajes
add Fecha_llegada datetime
go
Create FUNCTION devuelveIDD
(
    @descrip varchar(100)
)
RETURNS int
AS
BEGIN
    Declare @id int;
    SELECT @id =  id from Ciudades 
    where Descripcion = @descrip

    RETURN  @id

END
GO
insert into Rutas_Aereas
select dbo.devuelveIDD( A.Ruta_Ciudad_Destino),dbo.devuelveIDD( a.Ruta_Ciudad_Origen),
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

create FUNCTION devuelveRutaaa
(
	@ciudad_origen int,
	@ciudad_destino int,
	@Tipo_Servicio int
)
RETURNS int
AS
Begin
	Declare @id int
	select @id = Id from Rutas_Aereas A
	where	@ciudad_origen=A.Ciudad_Origen and
			@ciudad_destino=A.Ciudad_Destino and
			@Tipo_Servicio=A.Tipo_Servicio

			RETURN @id

END
go
create FUNCTION devuelveTipoServicio(
	@Servicio varchar(50)
	)
RETURNS int
AS
Begin
	Declare @id int
	select @id = ID from Tipos_Servicio
	where @Servicio=Descripcion

	RETURN @id

END
go
insert into Viajes
select Aeronave_Matricula,dbo.devuelveRutaaa(dbo.devuelveIDD(Ruta_Ciudad_Origen),
dbo.devuelveIDD(Ruta_Ciudad_Destino),dbo.devuelveTipoServicio(Tipo_Servicio)),
FechaSalida,Fecha_LLegada_Estimada,FechaLlegada
from gd_esquema.Maestra
group by Aeronave_Matricula,Ruta_Ciudad_Destino,Ruta_Ciudad_Origen,
FechaSalida,FechaLLegada,Fecha_LLegada_Estimada,Tipo_Servicio
go
Create FUNCTION devuelveNumeroCliente
(
    @DNI numeric(20)
)
RETURNS int
AS
BEGIN
    Declare @id int;
    SELECT @id =  Id from Clientes 
    where DNI = @DNI

    RETURN  @id

END
GO
Create FUNCTION devuelveViaje3
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
    SELECT @id =  Id from Viajes 
    where @Matricula=Matricula and
	@ruta=Ruta and @FechaSalida=Fecha_salida
	and @FechaEstimada=Fecha_Estimada_llegada and
	@Fechallegada=Fecha_llegada 

    RETURN  @id

END
GO
insert into Paquetes
select dbo.devuelveViaje3(dbo.devuelveRutaaa(dbo.devuelveIDD(Ruta_Ciudad_Origen),
dbo.devuelveIDD(Ruta_Ciudad_Destino),dbo.devuelveTipoServicio(Tipo_Servicio)),Aeronave_Matricula,
FechaSalida,Fecha_Llegada_Estimada,FechaLlegada),Paquete_Kg,Paquete_FechaCompra,dbo.devuelveNumeroCliente(Cli_Dni)
from gd_esquema.maestra
where Paquete_KG != 0
group by Tipo_Servicio,Fecha_llegada_estimada,Cli_Dni,Paquete_KG,Paquete_FechaCompra
,Ruta_Ciudad_Origen,Ruta_Ciudad_Destino,Aeronave_Matricula,FechaSalida,FechaLlegada
go
drop table KG
go
Alter table Butacas
add Estado varchar(50) not null
go
insert into Butacas
select distinct dbo.devuelveViaje3(dbo.devuelveRutaaa(dbo.devuelveIDD(Ruta_Ciudad_Origen),
dbo.devuelveIDD(Ruta_Ciudad_Destino),dbo.devuelveTipoServicio(Tipo_Servicio)),Aeronave_Matricula,
FechaSalida,Fecha_Llegada_Estimada,FechaLlegada),Butaca_Nro,Butaca_Tipo,'Vendida' 
from gd_esquema.maestra
where Pasaje_codigo !=0
group by Ruta_ciudad_origen,Ruta_Ciudad_destino,Tipo_Servicio,Aeronave_matricula,FechaSalida,
Fecha_llegada_estimada,FechaLlegada,Butaca_Nro,Butaca_tipo
go
insert into Pasajes
select distinct dbo.devuelveViaje3(dbo.devuelveRutaaa(dbo.devuelveIDD(Ruta_Ciudad_Origen),
dbo.devuelveIDD(Ruta_Ciudad_Destino),dbo.devuelveTipoServicio(Tipo_Servicio)),Aeronave_Matricula,
FechaSalida,Fecha_Llegada_Estimada,FechaLlegada),Butaca_Nro,Paquete_FechaCompra,dbo.devuelveNumeroCliente(Cli_dni) 
from gd_esquema.maestra
where Pasaje_codigo !=0
group by Ruta_Ciudad_Origen,Ruta_Ciudad_Destino,Tipo_servicio,Aeronave_Matricula,FechaSalida,
Fecha_llegada_Estimada,Fechallegada,Butaca_Nro,Paquete_fechaCompra,Cli_Dni

go

create view vista_rutas_aereas as
select r.Id as 'Codigo',  c1.descripcion as 'Ciudad origen',c2.descripcion as 'Ciudad destino',t.Descripcion as 'Servicio', r.Precio_Base as 'Precio base',r.Precio_Kg as 'Precio base encomienda'
from Rutas_Aereas r join Ciudades c1 on (r.Ciudad_Origen=c1.Id)
					join Ciudades c2 on (r.Ciudad_Destino=c2.Id)
					join Tipos_Servicio t on (r.Tipo_Servicio=t.Id)
go

create view vista_aeronaves as
select a.Fecha_alta as 'Fecha de alta',  a.Modelo as 'Modelo',a.matricula as 'Matrícula',f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',a.Baja_Fuera_Servicio as 'Baja por fuera de servicio',a.Baja_Vida_Util as 'Baja por vida util',a.Fecha_Fuera_Servicio as 'Fecha de fuera de servicio',a.Fecha_Reinicio_Servicio as 'Fecha de reinicio de servicio',a.Fecha_Baja_Definitiva as 'Fecha de baja definitiva',a.Cantidad_Butacas as 'Cantidad de butacas',a.Cantidad_Kg as 'Cantidad de Kgs disponibles para realizar encomiendas'
from Aeronaves a join Fabricantes f on (a.Fabricante=f.Id)
					join Tipos_Servicio ts on (a.Tipo_Servicio=ts.Id)				
go


create view funcionalidadPorRol as
select f.descripcion as 'Descripcion', r.Descripcion as 'Rol'
from funcionalidades f join Roles_Funcionalidades rf on (f.Id=rf.Funcionalidad) 
					   join Roles r on (r.Id=rf.Rol)
go


insert into Usuarios
values ('martinnb','62c66a7a5dd70c3146618063c344e531e6d4b59e379808443ce962b3abd63c5a',1,'OK','Sos dios?','no')
go

Create  trigger CuaandoSeIngresanLoginsIncorrectos
on Intentos_login
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
Insert into Intentos_fallidos (Cod_login)values (@num_login)
fetch next from cursorDeIncorrectos into @num_login,@Correcto
end

close cursorDeIncorrectos
deallocate cursorDeIncorrectos
end
drop table #tablaIncorrectos
commit;
go


Create trigger CuandoSeIngresanLoginsCorrectos
on Intentos_login
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
Delete from Intentos_fallidos where @cod_usuario=(select codigo_usuario from intentos_login i where i.id_login=cod_login)   
fetch next from cursorDeIncorrectos into @num_login,@Correcto,@cod_usuario
end
close cursorDeIncorrectos
deallocate cursorDeIncorrectos
end
drop table #tablaCorrectos

commit;
go


Create trigger CuandoSeIngresaUnTercerLoginFallidoSEInhabilita
on Intentos_fallidos
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
if ((select count(*) from Intentos_fallidos,Usuario,Intentos_login
where Intentos_fallidos.Cod_login=Intentos_login.Id_login and Intentos_login.Codigo_usuario=Usuario.Id_usuario and id_usuario=(Select Codigo_usuario from Intentos_login where Id_login=@Cod_login))>2)
update Usuario
set Estado='inhabilitado'
where Id_usuario = (Select Codigo_usuario from Intentos_login where Id_login=@Cod_login)
fetch next from cursorDeFallidos into @num_fallido,@Cod_login

end
close cursorDeFallidos
deallocate cursorDeFallidos
commit;
go	
create table Fecha(
fecha date
)
go
create function fechaDeHoy()
returns date
as begin

return (select max(fecha) from Fecha)
end
go


create function cantidadMillas(@cliente int)
returns int
as
begin
return(
select sum(Cantidad) from Millas
where Cliente=@cliente
and Fecha_Canje<dateadd(year,-1,fechaDeHoy())
)
end
go

create function movimientosMillas(@cliente int)
returns @tablita table(
fecha date,
cantidad int
)
as
begin
insert into @tablita

select Fecha_Canje,Cantidad from Millas
where Cliente=@cliente
and Fecha_Canje<dateadd(year,-1,fechaDeHoy())
return
end

GO

CREATE PROCEDURE aeronavesSustitutas @matricula varchar(10),@fechaBaja smalldatetime,@fechaAlta smalldatetime

AS

	DECLARE @Modelo varchar(30)		
	DECLARE @Fabricante int
	DECLARE @Tipo_Servicio int
	
	SELECT @Modelo = Modelo from Aeronaves a where a.matricula=@matricula 
	SELECT @Fabricante = Fabricante from Aeronaves a where a.matricula=@matricula
	SELECT @Tipo_Servicio = Tipo_Servicio from Aeronaves a where a.matricula=@matricula

SELECT a.Fecha_alta as 'Fecha de alta',  a.Modelo as 'Modelo',a.matricula as 'Matrícula',f.Descripcion as 'Fabricante', ts.Descripcion as 'Tipo de servicio',a.Baja_Fuera_Servicio as 'Baja por fuera de servicio',a.Baja_Vida_Util as 'Baja por vida util',a.Fecha_Fuera_Servicio as 'Fecha de fuera de servicio',a.Fecha_Reinicio_Servicio as 'Fecha de reinicio de servicio',a.Fecha_Baja_Definitiva as 'Fecha de baja definitiva',a.Cantidad_Butacas as 'Cantidad de butacas',a.Cantidad_Kg as 'Cantidad de Kgs disponibles para realizar encomiendas'
from aeronaves a
join Fabricantes f on (a.Fabricante=f.Id)
join Tipos_Servicio ts on (a.Tipo_Servicio=ts.Id)
where  
	not exists (
		select * from Viajes v
		where (v.Fecha_salida between @fechaBaja and @fechaAlta
		or  v.Fecha_Estimada_llegada between @fechaBaja and @fechaAlta) 
		and a.matricula=v.Matricula
	)
	and a.Modelo=@Modelo
	and a.Fabricante=@Fabricante
	and a.Tipo_Servicio=@Tipo_Servicio
	and a.matricula!=@matricula			 
		 

GO


