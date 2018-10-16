USE [master]
GO
	
	--	DROP LS
	-------------------------------------------------------------------------

	EXEC sp_dropserver 
		@server		=	N'LinkedServerDCDemo', 
		@droplogins	=	'droplogins'
	GO

	--	CREATE LS
	-------------------------------------------------------------------------

	EXEC sp_addlinkedserver 
		@server		=	N'LinkedServerDCDemo', 
		@srvproduct	=	N'MSOLAP', 
		@provider	=	N'MSOLAP', 
		@datasrc	=	N'.\KOKOSZMD', 
		@catalog	=	N'ContosoDCDemo'
	GO

	--	GET DATA TO #TBLs
	-------------------------------------------------------------------------

	DROP TABLE IF EXISTS #MEMBERS	
	DROP TABLE IF EXISTS #MEASURES

	SELECT
		CAST([CUBE_NAME]			AS NVARCHAR(1000))	AS [CUBE_NAME]			
	,	CAST([MEMBER_UNIQUE_NAME]	AS NVARCHAR(1000))	AS [MEMBER_UNIQUE_NAME]	
	,	CAST([EXPRESSION]			AS NVARCHAR(1000))	AS [EXPRESSION]		
	,	CAST([MEMBER_TYPE]			AS NVARCHAR(1000))	AS [MEMBER_TYPE]		
	INTO #MEMBERS	
	FROM OPENQUERY(
	[LinkedServerDCDemo],
	'	SELECT
			[CUBE_NAME]
		,	[MEMBER_UNIQUE_NAME]
		,	[EXPRESSION]	
		,	[MEMBER_TYPE]	
		FROM $system.MDSCHEMA_MEMBERS
		WHERE [MEMBER_TYPE]= 4 OR [MEMBER_TYPE]= 3
		'
	)

	SELECT
		CAST([CUBE_NAME]				AS NVARCHAR(1000))	AS [CUBE_NAME]
	,	CAST([MEASURE_UNIQUE_NAME]		AS NVARCHAR(1000))	AS [MEASURE_UNIQUE_NAME]
	,	CAST([MEASURE_IS_VISIBLE]		AS INT)				AS [MEASURE_IS_VISIBLE]
	,	CAST([MEASUREGROUP_NAME]		AS NVARCHAR(1000))	AS [MEASUREGROUP_NAME]
	,	CAST([MEASURE_DISPLAY_FOLDER]	AS NVARCHAR(1000))	AS [MEASURE_DISPLAY_FOLDER]
	,	CAST([DEFAULT_FORMAT_STRING]	AS NVARCHAR(1000))	AS [DEFAULT_FORMAT_STRING]
	INTO #MEASURES
	FROM OPENQUERY(
	[LinkedServerDCDemo],
	'	SELECT
			[CUBE_NAME]
		,	[MEASURE_UNIQUE_NAME]
		,	[MEASURE_IS_VISIBLE]
		,	[MEASUREGROUP_NAME]
		,	[MEASURE_DISPLAY_FOLDER]
		,	[DEFAULT_FORMAT_STRING]
		FROM $SYSTEM.MDSCHEMA_MEASURES
		'
	)

	--	ObjectId	CubeName	ObjectName	ObjectType	ObjectCode	VISIBLE	MEASGRP	DISFOLD	NONEMPT	FORMATS
	-------------------------------------------------------------------------

	SELECT
			[CubeName]		=	mb.[CUBE_NAME]
		,	[ObjectType]	=	CASE mb.[MEMBER_TYPE]
									WHEN 4 THEN 'CM'
									WHEN 3 THEN 'PM'
									END
		,	[ObjectName]	=	mb.[MEMBER_UNIQUE_NAME]	
		,	[ObjectCode]	=	mb.[EXPRESSION]
		,	[VISIBLE]		=	mr.[MEASURE_IS_VISIBLE]
		,	[MEASGRP]		=	ISNULL(mr.[MEASUREGROUP_NAME]			,'')
		,	[DISFOLD]		=	ISNULL(mr.[MEASURE_DISPLAY_FOLDER]		,'')
		,	[FORMATS]		=	ISNULL(mr.[DEFAULT_FORMAT_STRING]		,'')
		,	[NONEMPT]		=	''
	FROM		 #MEMBERS	AS mb
	inner JOIN	#MEASURES	AS mr	ON	mb.[CUBE_NAME] = mr.[CUBE_NAME]
									AND mb.[MEMBER_UNIQUE_NAME] = mr.[MEASURE_UNIQUE_NAME]
	WHERE 1=1
	AND mb.[CUBE_NAME] NOT LIKE '$%'
	ORDER BY 
			[CubeName]		
		,	[ObjectName]	
		

-----------------------------------------------------------------------

	SELECT * 
	FROM #MEMBERS	AS mb	
	WHERE [MEMBER_UNIQUE_NAME]	LIKE '%test%'

	SELECT * 
	FROM #MEASURES	AS mr	
	WHERE [MEASURE_UNIQUE_NAME] LIKE '%test%'
							