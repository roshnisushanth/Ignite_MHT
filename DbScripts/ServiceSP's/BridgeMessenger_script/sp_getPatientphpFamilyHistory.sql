IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientphpFamilyHistory]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[sp_getPatientphpFamilyHistory]
	PRINT 'Dropped [dbo].[sp_getPatientphpFamilyHistory]'
END	
GO

PRINT 'Creating [dbo].[sp_getPatientphpFamilyHistory]'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_getPatientphpFamilyHistory]  
   @PatientID int,
   @PatientFamilyHistoryID int

AS 
   BEGIN

      SET  XACT_ABORT  ON

      SET  NOCOUNT  ON

      IF @PatientFamilyHistoryID > 0
         SELECT
            FH.PatientFamilyHistoryID, 
            FH.ConditionName, 
            FH.OnsetDate, 
            FH.PatientID, 
            FH.CreatedDate, 
            FH.ModifiedDate, 
            FH.ConditionCheck, 
            FH.OtherInfo, 
            FH.RelationShip, 
            FH.Note
         FROM dbo.tblpatientphpfamilyhistory  AS FH
         inner join (select distinct ConditionName,OnsetDate,RelationShip,MAX(PatientFamilyHistoryID) as PatientFamilyHistoryID
         FROM dbo.tblpatientphpfamilyhistory group by ConditionName,OnsetDate,RelationShip)PFH 
         on  FH.PatientFamilyHistoryID= PFH.PatientFamilyHistoryID 
         
         WHERE FH.PatientFamilyHistoryID = 0
        
      ELSE 
         SELECT 
            FH.PatientFamilyHistoryID, 
            FH.ConditionName, 
            FH.OnsetDate, 
            FH.CreatedDate, 
            FH.ModifiedDate, 
            FH.PatientID, 
            FH.ConditionCheck, 
            FH.OtherInfo, 
            FH.RelationShip, 
            FH.Note
         FROM dbo.tblpatientphpfamilyhistory  AS FH
         inner join (select distinct ConditionName,OnsetDate,RelationShip,MAX(PatientFamilyHistoryID) as PatientFamilyHistoryID
         FROM dbo.tblpatientphpfamilyhistory group by ConditionName,OnsetDate,RelationShip)PFH 
         on  FH.PatientFamilyHistoryID= PFH.PatientFamilyHistoryID 
         
         WHERE FH.PatientID = @PatientID
         

   END

GO
PRINT 'Created the procedure sp_getPatientphpFamilyHistory'
GO  