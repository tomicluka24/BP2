CREATE FUNCTION f_avg_ppg
() RETURNS DECIMAL
AS
	BEGIN
	DECLARE
	@return_value AS DECIMAL;
	SELECT @return_value = avg(PpgKrila) FROM Kosarkas_Krilo;
	RETURN @return_value;
END;declare @ret_val decimal;
exec @ret_val = f_avg_ppg
select @ret_val;

select * from Kosarkas_Krilo;