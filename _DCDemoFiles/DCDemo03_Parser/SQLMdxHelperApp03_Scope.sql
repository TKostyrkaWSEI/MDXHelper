USE [MDXHelperDb]
GO
	
SELECT *
FROM
			[dbo].[MdxScopes]		AS ms
INNER JOIN	[dbo].[ObjCalcScripts]	AS cs	ON ms.ObjCalcScriptId	= cs.Id
INNER JOIN	[dbo].[ObjCubes]		AS cb	ON cs.CubeId			= cb.Id
INNER JOIN	[dbo].[Projects]		AS pr	ON cb.ProjectId			= pr.Id

--------------------------------------------------------

DECLARE @scopecode VARCHAR(MAX)

SELECT @scopecode = Code
FROM
			[dbo].[MdxScopes]		AS ms
INNER JOIN	[dbo].[ObjCalcScripts]	AS cs	ON ms.ObjCalcScriptId	= cs.Id
INNER JOIN	[dbo].[ObjCubes]		AS cb	ON cs.CubeId			= cb.Id
INNER JOIN	[dbo].[Projects]		AS pr	ON cb.ProjectId			= pr.Id
ORDER BY ms.Id
OFFSET 1 ROW FETCH NEXT 1 ROW ONLY
;

PRINT @scopecode