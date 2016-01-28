IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getGHPLabImagingByPatientID]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[sp_getGHPLabImagingByPatientID]
	PRINT 'Dropped [dbo].[sp_getGHPLabImagingByPatientID]'
END	
GO

PRINT 'Creating [dbo].[sp_getGHPLabImagingByPatientID]'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_getGHPLabImagingByPatientID]  
   @PatientId int
AS 
   BEGIN

      SET  XACT_ABORT  ON

      SET  NOCOUNT  ON

      /*
      *   SSMA informational messages:
      *   M2SS0134: Conversion of following Comment(s) is not supported :  This procedure was converted on Wed Apr 10 11:51:51 2013 using Ispirer .
      *
      */

      SELECT 
         tblghplabimaging.LabImagingId, 
         tblghplabimaging.Date, 
         tblghplabimaging.TestType, 
         tblghplabimaging.RequestingDoctor, 
         tblghplabimaging.AdministeredBy, 
         tblghplabimaging.Reason, 
         tblghplabimaging.Results, 
         tblghplabimaging.History, 
         @PatientId, 
         tblghplabimaging.UserId, 
         tblghplabimaging.UserType, 
         tblghplabimaging.Createddate, 
         tblghplabimaging.Modifieddate, 
         tblghplabimaging.OtherInfo
      FROM dbo.tblghplabimaging
      inner join (select Date, 
         TestType, 
         RequestingDoctor, 
         AdministeredBy, 
         Results,MAX(LabImagingId) as LabImagingId FROM dbo.tblghplabimaging
         group by Date,TestType,RequestingDoctor,AdministeredBy,Results)img
         on tblghplabimaging.LabImagingId= img.LabImagingId
      WHERE tblghplabimaging.PatientId = @PatientId
         ORDER BY tblghplabimaging.LabImagingId
  
end  

GO
PRINT 'Created the procedure sp_getGHPLabImagingByPatientID'
GO  