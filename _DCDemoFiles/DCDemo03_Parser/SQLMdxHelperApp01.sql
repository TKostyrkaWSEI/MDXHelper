USE [MDXHelperDb]
GO

-------------------------------------------------------------

	SELECT *
	FROM [dbo].[Projects]
	WHERE [ProjectName] = 'AWorksDW'

	SELECT *
	FROM [dbo].[ObjCubes]

	SELECT *, LEN(CalculationScriptText)
	FROM [dbo].[ObjCalcScripts]

-------------------------------------------------------------

	SELECT
		pr.ProjectName,
		cb.[Name],
		COUNT(*) AS [cnt]
	FROM 
				[dbo].[MdxMembers]		AS mm
	INNER JOIN	[dbo].[ObjCalcScripts]	AS cs	ON mm.ObjCalcScriptId	= cs.Id
	INNER JOIN	[dbo].[ObjCubes]		AS cb	ON cs.CubeId			= cb.Id
	INNER JOIN	[dbo].[Projects]		AS pr	ON cb.ProjectId			= pr.Id
	WHERE 1=1
	AND cs.IsActive = 1
	GROUP BY 
		pr.ProjectName,
		cb.[Name]

	SELECT *
	FROM [dbo].[MdxMembers]		AS mm