﻿ Create  PROCEDURE [dbo].[GenerateSerialNumberSp]
    @voucherid INT,
    @FiscalYearId INT,
    @CompanyId INT,
	@BranchId INT
AS
BEGIN

    DECLARE @TranName VARCHAR(20);
    SELECT @TranName = 'GenerateSerialNumber';

    BEGIN TRANSACTION @TranName;
	
    BEGIN TRY

    IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.Voucher tblSerialSchema
            WHERE (tblSerialSchema.Id =   @voucherid)
        )
        BEGIN
            RAISERROR('Serial Number Schema not exists!', 16, 1);
        END;

        /*  Constants   */
        DECLARE @SerialValueTag VARCHAR(3) = '{#}';
        DECLARE @YearTag VARCHAR(4) = '{yy}';
        DECLARE @Year2Tag VARCHAR(6) = '{yyyy}';
        DECLARE @MonthTag VARCHAR(4) = '{mm}';
        DECLARE @DayTag VARCHAR(4) = '{dd}';
        DECLARE @FiscalYearTag VARCHAR(4) = '{fy}';
        DECLARE @CompanyTag VARCHAR(4) = '{co}';

        /*  Variables   */
        DECLARE @SerialNumber NVARCHAR(100);
        DECLARE @SerialValue BIGINT;
        DECLARE @SchemaId INT;
        DECLARE @IncrementValue INT;
        DECLARE @InitialValue INT;
		 DECLARE @Year DATETIME2;
        --Declare @FiscalYearId int

        DECLARE @LeadingZerosLength NVARCHAR(50);
        DECLARE @ExpiryDate DATETIME2 = NULL;
        DECLARE @MaxValue BIGINT;
        DECLARE @IsCycle BIT;
        DECLARE @LastSerial BIGINT;

        SELECT @SchemaId = tblSerialSchema.VoucherId,
               @IncrementValue = ISNULL(NULLIF(tblSerialSchema.Increment, 0), 1),
               @InitialValue = ISNULL(tblSerialSchema.StartFrom, 0),
               @SerialNumber = ISNULL(tblSerialSchema.Serial_Mask, @SerialValueTag),
               @LeadingZerosLength = ISNULL(tblSerialSchema.VchWidth, 0),
               @MaxValue = tblSerialSchema.Max_Value,
             	 @LastSerial= case when isnull(tblSerialSchema.Last_Serial,0) <> 0 then tblSerialSchema.Last_Serial  end,
               @IsCycle = ISNULL(tblSerialSchema.FyReset, 0)
          FROM dbo.VchSetup tblSerialSchema
        WHERE (tblSerialSchema.VoucherId =@voucherid) and 
		(tblSerialSchema.CompId =@CompanyId);
		


        IF (@ExpiryDate IS NOT NULL)
        BEGIN
            IF (DATEDIFF(MINUTE, GETDATE(), @ExpiryDate) <= 0)
            BEGIN
                RAISERROR('Schema is expired!', 16, 1);
            END;
        END;

        SELECT @SerialValue
            = ISNULL(MIN(Serial_Value), ISNULL(@LastSerial, @InitialValue - @IncrementValue) + @IncrementValue)
        FROM SerialNumbersShelf
        WHERE Is_Hold = 0
              AND VoucherId = @voucherid
              AND YearId = @FiscalYearId and CompanyId=@CompanyId and BranchId=@BranchId

          IF (@MaxValue IS NOT NULL and @MaxValue !=0)
        BEGIN
            IF (@SerialValue > @MaxValue AND @IsCycle = 1)
            BEGIN
                SET @SerialValue = @InitialValue;
            END;
			else if(@SerialValue <= @MaxValue )
			Begin 
			SET @SerialValue = @SerialValue;
			END;
            ELSE
            BEGIN
                RAISERROR('Max value exceeded', 16, 1);
            END;
        END;
 
        IF NOT EXISTS
        (
            SELECT 1
            FROM SerialNumbersShelf
            WHERE VoucherId = @voucherid
                  AND Serial_Value = @SerialValue
                  AND YearId = @FiscalYearId and CompanyId=@CompanyId and BranchId=@branchid
        )
        BEGIN
            INSERT INTO SerialNumbersShelf
            (
                VoucherId,
                Serial_Value,
                Is_Hold,
                YearId,CompanyId,BranchId
            )
            VALUES
            (@voucherid, @SerialValue, 1, @FiscalYearId,@CompanyId,@BranchId);
        END;
        ELSE
        BEGIN
            UPDATE SerialNumbersShelf
            SET Is_Hold = 1
            WHERE VoucherId = @voucherid
                  AND Serial_Value = @SerialValue
                  AND YearId = @FiscalYearId
				   AND CompanyId = @CompanyId
				  AND BranchId=@BranchId;
        END;


        IF (LEN(@SerialValue) > @LeadingZerosLength)
        BEGIN
            SET @LeadingZerosLength = LEN(@SerialValue);
        END;

        /* Serial */
		
        IF (PATINDEX('%' + @SerialValueTag + '%', @SerialNumber) > 0 )
        BEGIN
            SET @SerialNumber
                = REPLACE(
                             @SerialNumber,
                             @SerialValueTag,
                             RIGHT(REPLICATE('0', @LeadingZerosLength) + CONVERT(NVARCHAR, @SerialValue), @LeadingZerosLength)
                         );
        END;
        /* Year */
		if(@IsCycle = 1)
		begin
		select @year=TDate  from FinYear where Id=@FiscalYearId
		end
		else
		begin
		select @year=FDate from FinYear where Id=@FiscalYearId
		end
        IF ((PATINDEX('%' + @YearTag + '%', @SerialNumber) > 0))
        BEGIN
		 SET @SerialNumber = REPLACE(@SerialNumber, @YearTag, RIGHT(YEAR(@year), 2));
           -- SET @SerialNumber = REPLACE(@SerialNumber, @YearTag, RIGHT(YEAR(GETDATE()), 2));
        END;
        IF (PATINDEX('%' + @Year2Tag + '%', @SerialNumber) > 0)
        BEGIN
            SET @SerialNumber = REPLACE(@SerialNumber, @Year2Tag, YEAR(@year));
        END;
        /* Month */
        IF (PATINDEX('%' + @MonthTag + '%', @SerialNumber) > 0)
        BEGIN
            SET @SerialNumber = REPLACE(@SerialNumber, @MonthTag, MONTH(GETDATE()));
        END;
        /* Day */
        IF (PATINDEX('%' + @DayTag + '%', @SerialNumber) > 0)
        BEGIN
            SET @SerialNumber = REPLACE(@SerialNumber, @DayTag, DAY(GETDATE()));
        END;
        /* Fiscal Year */
        IF (PATINDEX('%' + @FiscalYearTag + '%', @SerialNumber) > 0)
        BEGIN
            DECLARE @FiscalYear NVARCHAR(10) = '';
            SELECT @FiscalYear = YearCode
            FROM dbo.FinYear
            WHERE (
                      @FiscalYearId IS NOT NULL
                      AND Id = @FiscalYearId
                  );
            SET @SerialNumber = REPLACE(@SerialNumber, @FiscalYearTag, @FiscalYear);
        END;
        /* Compamy */
        IF (PATINDEX('%' + @CompanyTag + '%', @SerialNumber) > 0)
        BEGIN
            DECLARE @CompanyName NVARCHAR(10) = '';
            SELECT @CompanyName = SortName
            FROM dbo.Company
            WHERE (
                      @CompanyId IS NOT NULL
                      AND Id = @CompanyId
                  );
            SET @SerialNumber = REPLACE(@SerialNumber, @CompanyTag, @CompanyName);
        END;

        UPDATE dbo.VchSetup
        SET Last_Serial = @SerialValue
        WHERE VoucherId = @voucherid and CompId=@CompanyId;

        SELECT @SerialNumber SerialNumber,
               @SerialValue SerialValue;

    END TRY

BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION @TranName;

        DECLARE @ErrorMessage NVARCHAR(4000);
        SELECT @ErrorMessage = N'Cannot Generate Serial Number, ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);

    END CATCH;


    IF @@TRANCOUNT > 0
        COMMIT TRANSACTION @TranName;

END;