Use GD2C2015

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
Rol int)
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
Mail varchar(50) unique not null,
Fecha_Nacimiento smalldatetime not null,
Usuario int)
go
Alter table Clientes
add constraint FK_User_Cliente FOREIGN KEY (Usuario) references Usuarios(Id)
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
alter table Clientes
drop constraint UQ__Clientes__2724B2D1B1E33E98
go
begin transaction
Declare cursorCliente Cursor for
select distinct Cli_Nombre,Cli_Apellido,Cli_Dni,Cli_Dir,
Cli_Telefono,Cli_Mail,Cli_Fecha_Nac
from gd_esquema.Maestra
Declare @nombre varchar(30)
Declare @apellido varchar(30)
Declare @Dni int
Declare @Dir varchar(100) 
Declare @telefono numeric(12)
Declare @mail varchar(50)
Declare @fnac date
open cursorCliente
fetch next from cursorCliente into @nombre,@apellido,@Dni,@Dir,
@telefono,@mail,@fnac
while @@FETCH_STATUS=0
begin
Insert into Usuarios(Username,Password,Rol,Pregunta_Secreta,Respuesta) values(@Dni,'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',1,'Sos Dios?','Algo a ver')
insert into usuarios values ('admin','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',1,'Sos Dios?','Algo a ver')
Insert into Clientes(Usuario,Nombre,Apellido,DNI,Mail,Telefono,Direccion,Fecha_nacimiento) values((Select MAX(Id)from Usuarios),@nombre,@apellido,@Dni,@mail,@telefono,@Dir,@fnac)
fetch next from cursorCliente into @nombre,@apellido,@Dni,@Dir,
@telefono,@mail,@fnac
end
close cursorCliente
deallocate cursorCliente
commit
go

create view vista_rutas_aereas as
select r.Id as 'Codigo',  c1.descripcion as 'Ciudad origen',c2.descripcion as 'Ciudad destino',t.Descripcion as 'Servicio', r.Precio_Base as 'Precio base',r.Precio_Kg as 'Precio base encomienda'
from Rutas_Aereas r join Ciudades c1 on (r.Ciudad_Origen=c1.Id)
					join Ciudades c2 on (r.Ciudad_Destino=c2.Id)
					join Tipos_Servicio t on (r.Tipo_Servicio=t.Id)
go
