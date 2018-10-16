USE [MDXHelperDb]
GO
	SET NOCOUNT ON 
	;

	DROP TABLE IF EXISTS #TempTab
	;

	SELECT
		[ProjectName]	=	pr.[ProjectName]
	,	[CubeName]		=	cb.[Name]
	,	[ObjectName]	=	mm.[Name]
	,	[ObjectCode]	=	mm.[FullCode]
	,	[VISIBLE]		=	mm.[Visible]	
	,	[MEASGRP]		=	mm.[MeasureGroup]	
	,	[DISFOLD]		=	mm.[DisplayFolder]	
	,	[FORMATS]		=	mm.[FormatString]	
	,	[NONEMPT]		=	mm.[NonEmptyBehavior]
	,	[rn]			=	ROW_NUMBER() OVER (	PARTITION BY	mm.[MeasureGroup]	
												ORDER BY		mm.[Name]
												)
	INTO #TempTab
	FROM 
				[dbo].[MdxMembers]		AS mm
	INNER JOIN	[dbo].[ObjCalcScripts]	AS cs	ON mm.ObjCalcScriptId	= cs.Id
	INNER JOIN	[dbo].[ObjCubes]		AS cb	ON cs.CubeId			= cb.Id
	INNER JOIN	[dbo].[Projects]		AS pr	ON cb.ProjectId			= pr.Id
	WHERE 1=1
	AND pr.[ProjectName]	=	'AWorksDW'			--	'ContosoDCDemo'
	AND cb.[Name]			=	'Adventure Works'	--	'OperationA'
	AND mm.[IsMeasure]		=	1
	ORDER BY 
		mm.[MeasureGroup],
		mm.[Name]
	;

-----------------------------------------------------------------------------------------------------------------------------------

	DECLARE @NewScript NVARCHAR(MAX);

	SELECT 
		@NewScript		=	REPLACE(REPLACE(mdx,'&#x0D;',CHAR(13)),'&amp;','&')
	FROM
	(
		SELECT
		
			CASE 
				WHEN [rn] = 1
				THEN	CHAR(13) + '-- ' + ISNULL(MEASGRP, 'MeasureGroup Not Set') + CHAR(13) + 
						'----------------------------------------------------------------------------------------------' + CHAR(13) 
				ELSE ''
				END + CHAR(13) +
			'CREATE MEMBER CURRENTCUBE.' + [ObjectName] +
			CHAR(13) + 'AS (' + [ObjectCode] + ')'
	
			+ CASE WHEN NULLIF(FORMATS,	'') IS NULL THEN '' ELSE CHAR(13) + ',	FORMAT_STRING			 = '	+	FORMATS END
			+ CASE WHEN NULLIF(NONEMPT,	'') IS NULL THEN '' ELSE CHAR(13) + ',	NON_EMPTY_BEHAVIOR		 = '	+	NONEMPT END
			+ CASE WHEN NULLIF(VISIBLE,	'') IS NULL THEN '' ELSE CHAR(13) + ',	VISIBLE					 = '	+	CAST(ISNULL(VISIBLE,0) AS VARCHAR(1)) END
			+ CASE WHEN NULLIF(DISFOLD,	'') IS NULL THEN '' ELSE CHAR(13) + ',	DISPLAY_FOLDER			 = '	+	''''	+	DISFOLD + '''' END
			+ CASE WHEN NULLIF(MEASGRP,	'') IS NULL THEN '' ELSE CHAR(13) + ',	ASSOCIATED_MEASURE_GROUP = '	+	MEASGRP END
	
			+ CHAR(13) +';'
			+ CHAR(13)   
		FROM #TempTab
		WHERE 1=1
		ORDER BY MEASGRP, ObjectName
		FOR XML PATH('')
	) AS m(mdx)

	PRINT @NewScript