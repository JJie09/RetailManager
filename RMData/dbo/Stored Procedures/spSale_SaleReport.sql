﻿CREATE PROCEDURE [dbo].[spSale_SaleReport]
AS
BEGIN
	set nocount on

	select [s].[SaleDate], [s].[SubTotal], [s].[Tax], [s].[Total], u.EmailAddress, u.FirstName, u.LastName
	from dbo.Sale s
	inner join dbo.[User] u on s.CashierId = u.Id;

END
