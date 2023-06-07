USE [master]
GO

DROP DATABASE IF EXISTS VeganoBD
GO

CREATE DATABASE VeganoBD;
GO

USE VeganoBD
GO

CREATE TABLE Usuarios
(
	idUsuario	INT				PRIMARY KEY		IDENTITY,
	nome		VARCHAR(100)	NOT NULL,
	email		VARCHAR(100)	NOT NULL,
	senha		VARCHAR(20)		NOT NULL,
	idade		INT				NOT NULL,
	imagem		VARBINARY(MAX)	NOT NULL
)
GO

CREATE TABLE Categorias
(
	idCategoria	INT				PRIMARY KEY		IDENTITY,
	nome		VARCHAR(20)		NOT NULL
)
GO

CREATE TABLE Receitas
(
	idReceita		INT				PRIMARY KEY		IDENTITY,
	nome			VARCHAR(50)		NOT NULL,
	imagem			VARCHAR(200)	NULL,
	descricao		VARCHAR(1000)	NOT NULL,
	categoriaId		INT				NOT NULL,
	dataPostagem	DATETIME		NOT NULL,
	FOREIGN KEY	(categoriaId)	references Categorias(idCategoria)

)
GO

CREATE TABLE Seguidores
(
	usuarioId	INT				NOT NULL,	
	seguidorId	INT				NOT NULL,
	PRIMARY KEY	(usuarioId, seguidorId),
	FOREIGN KEY	(usuarioId)		references Usuarios(idUsuario),
	FOREIGN KEY (seguidorId)	references Usuarios(idUsuario) 
)
GO

CREATE TABLE Comentarios
(
	idComentario INT			PRIMARY KEY		IDENTITY,
	texto		 VARCHAR(MAX)	NULL,
	usuarioId	 INT			NOT NULL,
	receitaId	 INT			NOT NULL,
	FOREIGN KEY (usuarioId)		references Usuarios(idUsuario),
	FOREIGN KEY (receitaId)		references Receitas(idReceita)
)
GO

CREATE TABLE Favoritos
(
	receitaId	INT				NOT NULL,
	usuarioId	INT				NOT NULL,
	PRIMARY KEY (receitaId, usuarioId),
	FOREIGN KEY (receitaId)		references Receitas(idReceita),
	FOREIGN KEY (usuarioId)		references Usuarios(idUsuario)
)
GO

INSERT INTO Categorias VALUES	('Sobremesas'),
								('Sucos'),
								('Refeições')
GO
