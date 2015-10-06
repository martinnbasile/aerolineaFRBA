use Pruebas

create table roles (
	id int primary key,
	descri varchar(30)
)

create table Logins (
	id int identity(1,1) primary key,
	nombre varchar(30),
	passEncrip varchar(256),
	pass varchar(30)
)


insert into roles values (1, 'Admin')
insert into roles values (2, 'Cliente')

Create table usuarios (
	id int identity(1,1) primary key,
	username varchar(30),
	preguntaSecreta varchar(40),
)

insert into usuarios values('martin','quien es puma?')

create table ABM (
	id int identity(1,1) primary key,
	nombre varchar(30),
)

insert into ABM values('Rol');

