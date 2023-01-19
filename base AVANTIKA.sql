CREATE DATABASE OSilvaAVANTIKA
GO 
USE OSilvaAVANTIKA
GO 
CREATE TABLE Cliente
(
	IdCliente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(20) NOT NULL,
	ApellidoPaterno VARCHAR(20) NOT NULL,
	ApellidoMaterno VARCHAR(20) NOT NULL,
	RFC VARCHAR(13) NOT NULL
)
GO
CREATE TABLE Lugar
(
	IdLugar INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(60) NOT NULL,
	Calle VARCHAR(60) NOT NULL,
	Numero VARCHAR(5) NOT NULL,
	Localidad VARCHAR(20) NOT NULL,
	Capacidad INT NOT NULL
)
GO
CREATE TABLE Evento
(
	IdEvento INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(20) NOT NULL,
	IdLugar INT NOT NULL,
	FechaHora DATETIME NOT NULL,
	Precio DECIMAL NOT NULL
	FOREIGN KEY (IdLugar) REFERENCES Lugar(IdLugar)
)
GO
CREATE TABLE Venta
(
	IdVenta INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	IdCliente INT NOT NULL,
	IdEvento INT NOT NULL,
	IdLugar INT NOT NULL,
	NumeroAsiento INT NOT NULL,
	FechaOperacion DATETIME NOT NULL,
	--BoletosAdq INT NOT NULL
	FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
	FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento),
	FOREIGN KEY (IdLugar) REFERENCES Lugar(IdLugar)
)
GO
INSERT INTO Cliente VALUES('Miguel','Juarez','Aguilar','LLLLDDDDDDXXX')
GO
INSERT INTO Cliente VALUES('Hidalgo','Pichardo','Romero','LLLLDDDDDDXXX')
GO
INSERT INTO Cliente VALUES('Jose','Luna','Buendia','LLLLDDDDDDXXX')
GO
INSERT INTO Cliente VALUES('Angel','De la Rosa','Aviles','LLLLDDDDDDXXX')
GO
INSERT INTO Cliente VALUES('Kevin','Sanchez','Urtado','LLLLDDDDDDXXX')
GO
INSERT INTO Lugar VALUES('Palacio de los deportes','Insurgentes','123','Iztapalapa',100)
GO
INSERT INTO Lugar VALUES('Granja el terrenal','el templo','15','Amecameca',60)
GO
INSERT INTO Lugar VALUES('Karma','Av. Miraflores','8','Chalco',100)
GO
INSERT INTO Lugar VALUES('Los Zapotes','Cuahutemoc Norte','12','Cuahutemoc centro',100)
GO
INSERT INTO Lugar VALUES('Rancho no escondido','Tequila','23','Jalisco',100)
GO
INSERT INTO Evento VALUES('Melatica',1,GETDATE(),3000)
GO
INSERT INTO Evento VALUES('El Tree',2,GETDATE(),400)
GO
INSERT INTO Evento VALUES('Gansos Rosas',3,GETDATE(),1650)
GO
INSERT INTO Evento VALUES('Los Vitles',4,GETDATE(),8500)
GO
INSERT INTO Evento VALUES('El Ranstein',5,GETDATE(),7896)
GO
INSERT INTO Venta VALUES(1,1,1,4,SYSDATETIME())
GO
INSERT INTO Venta VALUES(2,2,2,18,SYSDATETIME())
GO
INSERT INTO Venta VALUES(3,3,3,11,SYSDATETIME())
GO
INSERT INTO Venta VALUES(4,4,4,1,SYSDATETIME())
GO
INSERT INTO Venta VALUES(5,5,5,6,SYSDATETIME())
GO

CREATE PROCEDURE addEvento
	--@IdEvento INT,
	@Nombre VARCHAR(20),
	@IdLugar INT,
	@FechaHora VARCHAR(30),
	@Precio DECIMAL
AS
	INSERT INTO Evento 
	VALUES
	(
	--@IdEvento,
	@Nombre,
	@IdLugar,
	CONVERT(VARCHAR,@FechaHora,103),
	@Precio
	)
GO
CREATE PROCEDURE GetAllEvento
AS
	SELECT 
	IdEvento, 
	Evento.Nombre, 
	Lugar.IdLugar, 
	Lugar.Nombre, 
	FechaHora, 
	Precio
	FROM Evento
	INNER JOIN Lugar ON Evento.IdLugar = Lugar.IdLugar
GO
CREATE PROCEDURE GetByIdEvento 
	@IdEvento INT
AS
	SELECT 
	IdEvento, 
	Evento.Nombre, 
	Lugar.IdLugar, 
	Lugar.Nombre, 
	FechaHora, 
	Precio
	FROM Evento
	INNER JOIN Lugar ON Evento.IdLugar = Lugar.IdLugar
	WHERE IdEvento = @IdEvento
GO
CREATE PROCEDURE UpdateEvento
	@IdEvento INT,
	@Nombre VARCHAR(20),
	@IdLugar INT,
	@FechaHora VARCHAR(30),
	@Precio DECIMAL
AS
	UPDATE Evento
	--SET IdEvento = @IdEvento, 
	SET
	Nombre = @Nombre, 
	IdLugar = @IdLugar, 
	FechaHora = CONVERT(VARCHAR,@FechaHora,103),
	Precio = @Precio
	WHERE IdEvento = @IdEvento
GO
CREATE PROCEDURE DeleteEvento
	@IdEvento INT
AS
	DELETE FROM Evento
	WHERE IdEvento = @IdEvento
GO
