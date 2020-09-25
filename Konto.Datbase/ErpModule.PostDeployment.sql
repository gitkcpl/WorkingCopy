/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\PostScript\menu.sql
:r .\PostScript\nob.sql
:r .\PostScript\PackageWiseMenu.sql
:r .\PostScript\alterMenu.sql
:r .\PostScript\Statelist.sql
:r .\PostScript\Uom.sql
:r .\PostScript\nop.sql
:r .\PostScript\LedgerGroup.sql
:r .\PostScript\defaultEntry.sql
:r .\PostScript\Acc.sql
:r .\PostScript\Permissions.sql
:r .\PostScript\SysPara.sql
:r .\PostScript\TransType.sql
:r .\PostScript\ListPageScript.sql
:r .\PostScript\SPCollections.sql
:r .\PostScript\ReportTypeList.sql
:r .\PostScript\VoucherTypeInit.sql
:r .\PostScript\Voucher.sql
:r .\PostScript\TempFieldList.sql


