
	DROP TABLE IF EXISTS #LP
	DROP TABLE IF EXISTS #RP

	----------------------------------------------------------------------------------

	SELECT mm.*	
	INTO #LP
	FROM
				[dbo].[MdxMembers]		AS mm
	INNER JOIN	[dbo].[ObjCalcScripts]	AS cs	ON mm.ObjCalcScriptId	= cs.Id
	INNER JOIN	[dbo].[ObjCubes]		AS cb	ON cs.CubeId			= cb.Id
	INNER JOIN	[dbo].[Projects]		AS pr	ON cb.ProjectId			= pr.Id
	WHERE 1=1
	AND pr.ProjectName	= 'ContosoDCDemo'
	AND cb.[Name]		= 'OperationA'
	
	SELECT mm.*	
	INTO #RP
	FROM
				[dbo].[MdxMembers]		AS mm
	INNER JOIN	[dbo].[ObjCalcScripts]	AS cs	ON mm.ObjCalcScriptId	= cs.Id
	INNER JOIN	[dbo].[ObjCubes]		AS cb	ON cs.CubeId			= cb.Id
	INNER JOIN	[dbo].[Projects]		AS pr	ON cb.ProjectId			= pr.Id
	WHERE 1=1
	AND pr.ProjectName	= 'ContosoDCDemo'
	AND cb.[Name]		= 'OperationB'

	----------------------------------------------------------------------------------

	SELECT 
		[Name]	=	COALESCE(l.[Name], r.[Name])
	,	[L_IsMeasure]	=	l.[IsMeasure]
	,	[R_IsMeasure]	=	r.[IsMeasure]
	,	[Diff_Exis]		=	CASE 
								WHEN r.[Name] IS NULL THEN 1
								WHEN l.[Name] IS NULL THEN 2
								ELSE 0
								END
	,	[Diff_Code]		=	CASE WHEN ISNULL(l.[FullCode], '')			<> ISNULL(r.[FullCode]		, '')	THEN 1 ELSE 0 END
	,	[Diff_Visi]		=	CASE WHEN ISNULL(l.[Visible], '')			<> ISNULL(r.[Visible]		, '')	THEN 1 ELSE 0 END
	,	[Diff_MGrp]		=	CASE WHEN ISNULL(l.[MeasureGroup], '')		<> ISNULL(r.[MeasureGroup]	, '')	THEN 1 ELSE 0 END
	,	[Diff_DiFo]		=	CASE WHEN ISNULL(l.[DisplayFolder], '')		<> ISNULL(r.[DisplayFolder]	, '')	THEN 1 ELSE 0 END
	,	[L_ObjectCode]	=	l.[FullCode]
	,	[R_ObjectCode]	=	r.[FullCode]
	,	[L_VISIBLE]		=	l.[Visible]
	,	[R_VISIBLE]		=	r.[Visible]
	,	[L_MEASGRP]		=	l.[MeasureGroup]
	,	[R_MEASGRP]		=	r.[MeasureGroup]
	,	[L_DISFOLD]		=	l.[DisplayFolder]
	,	[R_DISFOLD]		=	r.[DisplayFolder]
	FROM			#LP	AS l
	FULL OUTER JOIN #RP	AS r	ON	l.[Name]	= r.[Name]
	WHERE
		ISNULL(l.[FullCode]			, '')	<> ISNULL(r.[FullCode]		, '')
	OR	ISNULL(l.[Visible]			, '')	<> ISNULL(r.[Visible]		, '')
	OR	ISNULL(l.[MeasureGroup]		, '')	<> ISNULL(r.[MeasureGroup]	, '')
	OR	ISNULL(l.[DisplayFolder]	, '')	<> ISNULL(r.[DisplayFolder]	, '')
	ORDER BY 
		COALESCE(l.[IsMeasure], r.[IsMeasure]) DESC
	,	COALESCE(l.[Name], r.[Name])
