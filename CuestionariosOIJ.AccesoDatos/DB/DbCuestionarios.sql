USE [master]
GO
/****** Object:  Database [DataBaseCuestionarios]    Script Date: 11/11/2023 18:02:16 ******/
CREATE DATABASE [DataBaseCuestionarios]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataBaseCuestionarios', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DataBaseCuestionarios.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DataBaseCuestionarios_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DataBaseCuestionarios_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DataBaseCuestionarios] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataBaseCuestionarios].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataBaseCuestionarios] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET ARITHABORT OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DataBaseCuestionarios] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DataBaseCuestionarios] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DataBaseCuestionarios] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DataBaseCuestionarios] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET RECOVERY FULL 
GO
ALTER DATABASE [DataBaseCuestionarios] SET  MULTI_USER 
GO
ALTER DATABASE [DataBaseCuestionarios] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DataBaseCuestionarios] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DataBaseCuestionarios] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DataBaseCuestionarios] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DataBaseCuestionarios] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DataBaseCuestionarios] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DataBaseCuestionarios] SET QUERY_STORE = OFF
GO
USE [DataBaseCuestionarios]
GO
/****** Object:  Schema [Administracion]    Script Date: 11/11/2023 18:02:16 ******/
CREATE SCHEMA [Administracion]
GO
/****** Object:  Schema [Cuestionarios]    Script Date: 11/11/2023 18:02:16 ******/
CREATE SCHEMA [Cuestionarios]
GO
/****** Object:  UserDefinedFunction [dbo].[fc_check_pregunta_unica]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fc_check_pregunta_unica] (
    @TextoPregunta VARCHAR(200),
    @CuestionarioId INT
)
RETURNS BIT
AS
BEGIN
    DECLARE @result BIT;

    IF (EXISTS (
        SELECT 1
        FROM Cuestionarios.Pregunta
        WHERE TextoPregunta = @TextoPregunta AND CuestionarioID = @CuestionarioId AND Eliminado = 0
    ))
    BEGIN
        SET @result = 0;
    END
    ELSE
    BEGIN
        SET @result = 1;
    END

    RETURN @result;
END;
GO
/****** Object:  Table [Administracion].[Oficina]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Administracion].[Oficina](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Categoria]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Categoria](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [uq_nombre_unico] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Cuestionario]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Cuestionario](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Descripcion] [varchar](300) NULL,
	[Activo] [bit] NOT NULL,
	[FechaVencimiento] [datetime] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaUltimaModificacion] [datetime] NOT NULL,
	[TipoCuestionarioID] [int] NOT NULL,
	[OficinaID] [int] NOT NULL,
	[Eliminado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [uq_nombre_cuestionario] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Justiicacion_Respuesta]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Justiicacion_Respuesta](
	[RespuestaID] [int] NOT NULL,
	[Justificacion] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RespuestaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[OpcionRespuesta]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[OpcionRespuesta](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PreguntaID] [int] NOT NULL,
	[TextoOpcion] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Pregunta]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Pregunta](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TextoPregunta] [varchar](200) NOT NULL,
	[Posicion] [int] NOT NULL,
	[Etiqueta] [varchar](100) NULL,
	[Justificacion] [bit] NOT NULL,
	[Obligatoria] [bit] NOT NULL,
	[CategoriaID] [int] NULL,
	[SubcategoriaID] [int] NULL,
	[TipoPreguntaID] [int] NOT NULL,
	[CuestionarioID] [int] NOT NULL,
	[Eliminado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Respuesta]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Respuesta](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TextoRespuesta] [varchar](300) NOT NULL,
	[FechaRespondida] [datetime] NOT NULL,
	[FechaEliminada] [datetime] NULL,
	[PreguntaID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Respuesta_OpcionRespuesta]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Respuesta_OpcionRespuesta](
	[RespuestaID] [int] NOT NULL,
	[OpcionRespuestaID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RespuestaID] ASC,
	[OpcionRespuestaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Revisador_Cuestionario]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Revisador_Cuestionario](
	[Revisador] [varchar](150) NOT NULL,
	[CuestionarioID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Revisador] ASC,
	[CuestionarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Subcategoria]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Subcategoria](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[CategoriaID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [uq_nombre_subcategoria] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[TipoCuestionario]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[TipoCuestionario](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[TipoPregunta]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[TipoPregunta](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Cuestionarios].[Usuario_Respuesta]    Script Date: 11/11/2023 18:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuestionarios].[Usuario_Respuesta](
	[RespuestaID] [int] NOT NULL,
	[Usuario] [varchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RespuestaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Cuestionarios].[Cuestionario] ADD  DEFAULT ((0)) FOR [Eliminado]
GO
ALTER TABLE [Cuestionarios].[Pregunta] ADD  DEFAULT ((0)) FOR [Eliminado]
GO
ALTER TABLE [Cuestionarios].[Cuestionario]  WITH CHECK ADD FOREIGN KEY([OficinaID])
REFERENCES [Administracion].[Oficina] ([ID])
GO
ALTER TABLE [Cuestionarios].[Cuestionario]  WITH CHECK ADD FOREIGN KEY([TipoCuestionarioID])
REFERENCES [Cuestionarios].[TipoCuestionario] ([ID])
GO
ALTER TABLE [Cuestionarios].[Justiicacion_Respuesta]  WITH CHECK ADD FOREIGN KEY([RespuestaID])
REFERENCES [Cuestionarios].[Respuesta] ([ID])
GO
ALTER TABLE [Cuestionarios].[OpcionRespuesta]  WITH CHECK ADD FOREIGN KEY([PreguntaID])
REFERENCES [Cuestionarios].[Pregunta] ([ID])
GO
ALTER TABLE [Cuestionarios].[Pregunta]  WITH CHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [Cuestionarios].[Categoria] ([ID])
GO
ALTER TABLE [Cuestionarios].[Pregunta]  WITH CHECK ADD FOREIGN KEY([CuestionarioID])
REFERENCES [Cuestionarios].[Cuestionario] ([ID])
GO
ALTER TABLE [Cuestionarios].[Pregunta]  WITH CHECK ADD FOREIGN KEY([SubcategoriaID])
REFERENCES [Cuestionarios].[Subcategoria] ([ID])
GO
ALTER TABLE [Cuestionarios].[Pregunta]  WITH CHECK ADD FOREIGN KEY([TipoPreguntaID])
REFERENCES [Cuestionarios].[TipoPregunta] ([ID])
GO
ALTER TABLE [Cuestionarios].[Respuesta]  WITH CHECK ADD FOREIGN KEY([PreguntaID])
REFERENCES [Cuestionarios].[Pregunta] ([ID])
GO
ALTER TABLE [Cuestionarios].[Respuesta_OpcionRespuesta]  WITH CHECK ADD FOREIGN KEY([OpcionRespuestaID])
REFERENCES [Cuestionarios].[OpcionRespuesta] ([ID])
GO
ALTER TABLE [Cuestionarios].[Respuesta_OpcionRespuesta]  WITH CHECK ADD FOREIGN KEY([RespuestaID])
REFERENCES [Cuestionarios].[Respuesta] ([ID])
GO
ALTER TABLE [Cuestionarios].[Revisador_Cuestionario]  WITH CHECK ADD FOREIGN KEY([CuestionarioID])
REFERENCES [Cuestionarios].[Cuestionario] ([ID])
GO
ALTER TABLE [Cuestionarios].[Subcategoria]  WITH CHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [Cuestionarios].[Categoria] ([ID])
GO
ALTER TABLE [Cuestionarios].[Usuario_Respuesta]  WITH CHECK ADD FOREIGN KEY([RespuestaID])
REFERENCES [Cuestionarios].[Respuesta] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[sp_actualizar_cuestionario]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_actualizar_cuestionario]
	@ID int,
	@Nombre varchar(200),
	@Descripcion varchar(300),
	@Activo bit,
	@FechaVencimiento datetime,
	@TipoCuestionarioID int
AS 
BEGIN
	UPDATE [Cuestionarios].[Cuestionario]
	   SET 
		  [Nombre] = @Nombre
		  ,[Descripcion] = @Descripcion
		  ,[Activo] = @Activo
		  ,[FechaVencimiento] = @FechaVencimiento
		  ,[FechaUltimaModificacion] = GETDATE()
		  ,[TipoCuestionarioID] = @TipoCuestionarioID
	 WHERE [ID] = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_agregar_opcion_escogida]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_agregar_opcion_escogida]
	@respuestaId INT, 
	@opcionId INT
AS BEGIN
	INSERT INTO Cuestionarios.[Respuesta_OpcionRespuesta]
	(RespuestaID, OpcionRespuestaID)
	VALUES (@respuestaId, @opcionId);
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_insertar_cuestionario]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_insertar_cuestionario]
	@Codigo VARCHAR(20),
	@Nombre VARCHAR(200),
	@Descripcion VARCHAR(300),
	@Activo BIT,
	@FechaVencimiento DATETIME,
	@TipoCuestionarioID INT,
	@OficinaID INT
AS
BEGIN
	INSERT INTO Cuestionarios.Cuestionario
	(Codigo, Nombre, Descripcion, Activo, FechaVencimiento, FechaCreacion, FechaUltimaModificacion, TipoCuestionarioID, OficinaID, Eliminado)
	VALUES
	(@Codigo, @Nombre, @Descripcion, @Activo, @FechaVencimiento, GETDATE(), GETDATE(), @TipoCuestionarioID, @OficinaID, 0);

END
GO
/****** Object:  StoredProcedure [dbo].[sp_insertar_pregunta]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_insertar_pregunta]
	@TextoPregunta VARCHAR(200),
	@Posicion INT,
	@Etiqueta VARCHAR(100),
	@Justificacion BIT, 
	@Obligatoria BIT,
	@CategoriaID INT,
	@SubcategoriaID INT,
	@TipoPreguntaID INT,
	@CuestionarioID INT
AS
BEGIN
	
	INSERT INTO [Cuestionarios].[Pregunta]
			   ([TextoPregunta]
			   ,[Posicion]
			   ,[Etiqueta]
			   ,[Justificacion]
			   ,[Obligatoria]
			   ,[CategoriaID]
			   ,[SubcategoriaID]
			   ,[TipoPreguntaID]
			   ,[CuestionarioID]
			   ,[Eliminado])
		 VALUES
			   (@TextoPregunta
			   ,@Posicion
			   ,@Etiqueta
			   ,@Justificacion
			   ,@Obligatoria
			   ,@CategoriaID
			   ,@SubcategoriaID
			   ,@TipoPreguntaID
			   ,@CuestionarioID
			   ,0);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_obtener_fechas_eliminacion_por_cuestionario]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_obtener_fechas_eliminacion_por_cuestionario]
    @CuestionarioID INT
AS
BEGIN
    SELECT 
		FORMAT(R.FechaEliminada, 'dd') AS Dia,
		FORMAT(R.FechaEliminada, 'MM') AS Mes,
		FORMAT(R.FechaEliminada, 'yyyy') AS Anio
    FROM [Cuestionarios].[Respuesta] R
    INNER JOIN [Cuestionarios].[Pregunta] P ON R.PreguntaID = P.ID
    WHERE P.CuestionarioID = @CuestionarioID AND P.Eliminado = 0
    GROUP BY
		FORMAT(R.FechaEliminada, 'dd'),
		FORMAT(R.FechaEliminada, 'MM'),
		FORMAT(R.FechaEliminada, 'yyyy')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_reporte_escala]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_reporte_escala]
	@preguntaId INT,
	@periodo DATETIME
AS BEGIN
	IF @periodo IS NULL
	BEGIN
		SELECT
			SUM(CAST(TextoRespuesta AS DECIMAL(10,2)))/COUNT(*) AS Promedio,
			COUNT(*) AS Respuestas
		FROM Cuestionarios.Respuesta R
		WHERE R.PreguntaID = @preguntaId
			AND R.FechaEliminada IS NULL
	END
	ELSE
	BEGIN
		SELECT
			SUM(CAST(TextoRespuesta AS DECIMAL(10,2)))/COUNT(*) AS Promedio,
			COUNT(*) AS Respuestas
		FROM Cuestionarios.Respuesta R
		WHERE R.PreguntaID = @preguntaId
			AND FORMAT(R.FechaEliminada, 'dd/MM/yyyy') = @periodo
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_reporte_respuestas_escala]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_reporte_respuestas_escala]
	@preguntaId INT,
	@periodo DATETIME
AS BEGIN
	DECLARE @valores TABLE(numero int);
	INSERT INTO @valores VALUES (0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10);

	IF @periodo IS NULL
	BEGIN
		SELECT 
			V.numero AS "Valor",
			COUNT(R.TextoRespuesta) AS "Respuestas"
		FROM @valores V
			LEFT JOIN (	SELECT * 
						FROM Cuestionarios.Respuesta 
						WHERE PreguntaID = @preguntaId
							AND FechaEliminada IS NULL) R 
				ON CAST(R.TextoRespuesta AS INT) = V.numero
		GROUP BY v.numero
		ORDER BY V.numero
	END
	ELSE
	BEGIN
		SELECT 
			V.numero AS "Valor",
			COUNT(R.TextoRespuesta) AS "Respuestas"
		FROM @valores V
			LEFT JOIN (	SELECT * 
						FROM Cuestionarios.Respuesta 
						WHERE PreguntaID = @preguntaId
							AND FORMAT(FechaEliminada, 'dd/MM/yyyy') = @periodo) R 
				ON CAST(R.TextoRespuesta AS INT) = V.numero
		GROUP BY v.numero
		ORDER BY V.numero
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_reporte_seleccion_multiple]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_reporte_seleccion_multiple]
	@preguntaId INT,
	@periodo DATETIME
AS BEGIN
	DECLARE @totalRespuestas INT = (
		SELECT COUNT(R.ID)
		FROM Cuestionarios.Respuesta R
		WHERE R.PreguntaID = @preguntaId
	);

	IF @periodo IS NULL 
	BEGIN
		SELECT
			O.TextoOpcion,
			COUNT(RO.RespuestaID) AS "Elecciones",
			@totalRespuestas AS "TotalRespuestas"
		FROM Cuestionarios.OpcionRespuesta O
			LEFT JOIN Cuestionarios.Respuesta_OpcionRespuesta RO ON RO.OpcionRespuestaID = O.ID
				LEFT JOIN Cuestionarios.Respuesta R On R.ID = RO.RespuestaID
		WHERE O.PreguntaID = @preguntaId  
			AND R.FechaEliminada IS NULL
		GROUP BY O.TextoOpcion
	END
	ELSE
	BEGIN
		SELECT
			O.TextoOpcion,
			COUNT(RO.RespuestaID) AS "Elecciones",
			@totalRespuestas AS "TotalRespuestas"
		FROM Cuestionarios.OpcionRespuesta O
			LEFT JOIN Cuestionarios.Respuesta_OpcionRespuesta RO ON RO.OpcionRespuestaID = O.ID
				LEFT JOIN Cuestionarios.Respuesta R On R.ID = RO.RespuestaID
		WHERE O.PreguntaID = @preguntaId  
			AND FORMAT(R.FechaEliminada, 'dd/MM/yyyy') = @periodo
		GROUP BY O.TextoOpcion
	END;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_reporte_seleccion_unica]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_reporte_seleccion_unica]
	@preguntaId INT,
	@periodo DATETIME
AS BEGIN
	DECLARE @totalRespuestas INT = (
		SELECT COUNT(OpcionRespuestaID) 
		FROM Cuestionarios.Respuesta_OpcionRespuesta R
			JOIN Cuestionarios.OpcionRespuesta O ON O.ID = R.OpcionRespuestaID
		WHERE O.PreguntaID = @preguntaID
		);

	IF @periodo IS NULL
	BEGIN
		select 
			ORes.TextoOpcion,
			COUNT(ORes.ID) AS Elecciones,
			CAST(CAST(COUNT(ORes.ID)AS DECIMAL(10,2))/@totalRespuestas *100 AS DECIMAL(5,2)) AS "Porcentaje"
		from Cuestionarios.OpcionRespuesta ORes
			JOIN Cuestionarios.Respuesta_OpcionRespuesta ROR ON ROR.OpcionRespuestaID = ORes.ID
				JOIN Cuestionarios.Respuesta R ON R.ID = ROR.RespuestaID
		Where ORes.PreguntaID = @preguntaID
			AND R.FechaEliminada IS NULL
		GROUP BY ORes.TextoOpcion
	END
	ELSE
	BEGIN
		select 
			ORes.TextoOpcion,
			COUNT(ORes.ID) AS Elecciones,
			CAST(CAST(COUNT(ORes.ID)AS DECIMAL(10,2))/@totalRespuestas *100 AS DECIMAL(5,2)) AS "Porcentaje"
		from Cuestionarios.OpcionRespuesta ORes
			JOIN Cuestionarios.Respuesta_OpcionRespuesta ROR ON ROR.OpcionRespuestaID = ORes.ID
				JOIN Cuestionarios.Respuesta R ON R.ID = ROR.RespuestaID
		Where ORes.PreguntaID = @preguntaID
			AND FORMAT(R.FechaEliminada, 'dd/MM/yyyy') = @periodo
		GROUP BY ORes.TextoOpcion
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_reporte_texto]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_reporte_texto]
	@preguntaId INT,
	@periodo DATETIME
AS BEGIN
	IF @periodo IS NULL 
	BEGIN
		SELECT 
			CASE 	
				WHEN UR.Usuario IS NULL THEN 'Anónimo'
				ELSE UR.Usuario
			END AS Usuario,
			R.TextoRespuesta 
		FROM Cuestionarios.Respuesta R
			LEFT JOIN Cuestionarios.Usuario_Respuesta UR ON UR.RespuestaID = R.ID
		WHERE R.PreguntaID = @preguntaId
			AND R.FechaEliminada IS NULL;
	END
	ELSE
	BEGIN
		SELECT 
			CASE 	
				WHEN UR.Usuario IS NULL THEN 'Anónimo'
				ELSE UR.Usuario
			END AS Usuario,
			R.TextoRespuesta 
		FROM Cuestionarios.Respuesta R
			LEFT JOIN Cuestionarios.Usuario_Respuesta UR ON UR.RespuestaID = R.ID
		WHERE R.PreguntaID = @preguntaId
			AND FORMAT(R.FechaEliminada, 'dd/MM/yyyy') = @periodo;
	END;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_reporte_verdadero_falso]    Script Date: 11/11/2023 18:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_reporte_verdadero_falso]
	@preguntaId INT,
	@periodo DATETIME
AS BEGIN
	DECLARE @totalRespuestas INT = (
		SELECT COUNT(R.ID)
		FROM Cuestionarios.Respuesta R
		WHERE R.PreguntaID = @preguntaId
	);
	IF @periodo IS NULL
	BEGIN
		SELECT
			R.TextoRespuesta,
			COUNT(R.TextoRespuesta) AS "Respuestas",
			CAST(CAST(COUNT(R.TextoRespuesta) AS decimal(10,2))/@totalRespuestas*100 AS decimal(5,2)) AS "Porcentaje"
		FROM Cuestionarios.Respuesta R
		WHERE R.PreguntaID = @preguntaId
			AND R.FechaEliminada IS NULL
		GROUP BY R.TextoRespuesta
	END
	ELSE
	BEGIN
		SELECT
			R.TextoRespuesta,
			COUNT(R.TextoRespuesta) AS "Respuestas",
			CAST(CAST(COUNT(R.TextoRespuesta) AS decimal(10,2))/@totalRespuestas*100 AS decimal(5,2)) AS "Porcentaje"
		FROM Cuestionarios.Respuesta R
		WHERE R.PreguntaID = @preguntaId
			AND FORMAT(R.FechaEliminada, 'dd/MM/yyyy') = @periodo
		GROUP BY R.TextoRespuesta
	END
END
GO
USE [master]
GO
ALTER DATABASE [DataBaseCuestionarios] SET  READ_WRITE 
GO
