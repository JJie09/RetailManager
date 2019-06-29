CREATE PROCEDURE [dbo].[spUserRegister] 
	@id nvarchar(128),
    @firstName nvarchar(50),
    @lastName nvarchar(50),
    @emailAddress nvarchar(256)
AS
begin
	set nocount on;
	
    insert into [User](Id,FirstName,LastName,EmailAddress)
    values(@id,@firstName,@lastName,@emailAddress)
end