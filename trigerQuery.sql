CREATE TRIGGER [Trigger]
ON [dbo].[Kosarkas_Krilo]
instead of update
AS
BEGIN
RAISERROR ('no update on table projekat', -- Message text.
16, -- Severity.
1);
END