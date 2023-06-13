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
	imagem		VARCHAR(MAX)	NOT NULL
)
GO

CREATE TABLE Categorias
(
	idCategoria	INT				PRIMARY KEY		IDENTITY,
	nome		VARCHAR(20)		NOT NULL,
	imagem		VARCHAR(MAX)	NULL
)
GO

CREATE TABLE Receitas
(
	idReceita		INT				PRIMARY KEY		IDENTITY,
	nome			VARCHAR(50)		NOT NULL,
	imagem			VARCHAR(MAX)	NULL,
	ingredientes	VARCHAR(MAX)	NOT NULL,
	modoPreparo		VARCHAR(MAX)	NOT NULL,
	categoriaId		INT				NOT NULL,
	usuarioId		INT				NOT NULL,
	dataPostagem	DATETIME		NOT NULL,
	FOREIGN KEY	(categoriaId)		REFERENCES Categorias(idCategoria),
	FOREIGN KEY	(usuarioId)			REFERENCES Usuarios(idUsuario)

)
GO

CREATE TABLE Seguidores
(
	usuarioId	INT				NOT NULL,	
	seguidorId	INT				NOT NULL,
	PRIMARY KEY	(usuarioId, seguidorId),
	FOREIGN KEY	(usuarioId)		REFERENCES Usuarios(idUsuario),
	FOREIGN KEY (seguidorId)	REFERENCES Usuarios(idUsuario) 
)
GO

CREATE TABLE Comentarios
(
	idComentario INT			PRIMARY KEY		IDENTITY,
	texto		 VARCHAR(MAX)	NULL,
	usuarioId	 INT			NOT NULL,
	receitaId	 INT			NOT NULL,
	FOREIGN KEY (usuarioId)		REFERENCES Usuarios(idUsuario),
	FOREIGN KEY (receitaId)		REFERENCES Receitas(idReceita)
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

--INSERTS
INSERT INTO Categorias(nome, imagem) VALUES ('Bebidas','bebidas.jpg'),
											('Sobremesas','sobremesas.jpg'),
											('Refeições','refeicoes.jpg'),
											('Aperitivos','aperitivos.jpg'),
											('Churrasco','churrasco.jpg'),
											('Acompanhamentos','acompanhamentos.jpg'),
											('Salgados','salgados.jpg')
GO

INSERT INTO Usuarios(nome, email, senha, idade, imagem) VALUES 	('Igor', 'igor@igor.com', '123', 20, 'foto.jpg'),
																('Deizy', 'deizy@deizy.com', '123', 24, 'foto2.jpg')
GO

INSERT INTO Receitas(nome, imagem, ingredientes, modoPreparo, categoriaId, usuarioId, dataPostagem) 
VALUES ('Brigadeiro Vegano', 
		'brigadeiro-vegano.jpg', 
		'1 porção de Leite Condensado Vegano (fica pronto em 3 minutos), 2 colheres (sopa) de cacau em pó de boa qualidade, 1/4 de xícara de chocolate 70% picado (ou outro chocolate em barra), pitada de sal',
		'Adicione todos os ingredientes em uma panela e cozinhe em fogo médio até obter uma mistura bem homogênea que desgruda do fundo da panela. Retire do fogo, coloque em um recipiente e deixe esfriar. Ele irá engrossar por conta e deve ficar no ponto de enrolar. Para enrolar, unte suas mãos com óleo de coco ou creme vegetal de sua preferência.',
		2,
		1,
		CONVERT(DATE, GETDATE()))
GO		
