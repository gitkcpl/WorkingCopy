CREATE FUNCTION [dbo].[keyMonthName] (@iMonth int)

RETURNS varchar(25)
AS
BEGIN

if @imonth=1
	return	 'Januray'
else if @imonth = 2
	return	 'February'
else if @imonth = 3
	return	 'March'
else if @imonth = 4
	return	 'April'
else if @imonth = 5
	return	 'May'
else if @imonth = 6
	return	 'June'
else if @imonth = 7
	return	 'July'
else if @imonth = 8
	return	 'August'
else if @imonth = 9
	return	 'September'
else if @imonth = 10
	return	 'October'
else if @imonth = 11
	return	 'November'
else if @imonth = 12
	return	 'December'

return '-'	

	
	
	
	



  /* Function body */
END

GO

