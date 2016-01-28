IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetPatientImmunization]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[sp_GetPatientImmunization]
	PRINT 'Dropped [dbo].[sp_GetPatientImmunization]'
END	
GO

PRINT 'Creating [dbo].[sp_GetPatientImmunization]'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetPatientImmunization]  
   @ImmunizationID int,
   @PatientID int
AS 
   BEGIN

      SET  XACT_ABORT  ON

      SET  NOCOUNT  ON

      IF @ImmunizationID > 0
         SELECT 
            tblpatientimmunization.ImmunizationID, 
            tblpatientimmunization.ImmunizationType, 
            tblpatientimmunization.AdministrationDate, 
            tblpatientimmunization.Note, 
            tblpatientimmunization.AdverseEvent, 
            tblpatientimmunization.PatientID
         FROM dbo.tblpatientimmunization
         inner join (select ImmunizationType,AdministrationDate,MAX(ImmunizationID) as ImmunizationID
          FROM dbo.tblpatientimmunization
          group by ImmunizationType,AdministrationDate)I 
          on tblpatientimmunization.ImmunizationID=I.ImmunizationID
         
         WHERE tblpatientimmunization.ImmunizationID = @ImmunizationID
         order by AdministrationDate desc
      ELSE 
         SELECT 
            tblpatientimmunization.ImmunizationID, 
            tblpatientimmunization.ImmunizationType, 
            tblpatientimmunization.AdministrationDate, 
            tblpatientimmunization.Note, 
            tblpatientimmunization.AdverseEvent, 
            tblpatientimmunization.PatientID, 
            tblpatientimmunization.ModifiedDate
         FROM dbo.tblpatientimmunization
         inner join (select ImmunizationType,AdministrationDate,MAX(ImmunizationID) as ImmunizationID
          FROM dbo.tblpatientimmunization
          group by ImmunizationType,AdministrationDate)I 
          on tblpatientimmunization.ImmunizationID=I.ImmunizationID
         
         WHERE tblpatientimmunization.PatientID = @PatientID
         order by AdministrationDate desc
end  

GO
PRINT 'Created the procedure sp_GetPatientImmunization'
GO  