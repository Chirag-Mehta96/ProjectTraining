USE [8Aug_MIPL]
GO
/****** Object:  Schema [Ecomm]    Script Date: 11/13/2018 9:47:22 AM ******/
CREATE SCHEMA [Ecomm]
GO
/****** Object:  StoredProcedure [Ecomm].[usp_ClearCart]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [Ecomm].[usp_ClearCart]
as

truncate table [Ecomm].[ShoppingCart]

GO
/****** Object:  StoredProcedure [Ecomm].[usp_DeleteCategories]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_DeleteCategories]
@Category_Id varchar(15)
AS
   Delete from [Ecomm].[Categories] Where [Category_Id]=@Category_Id

GO
/****** Object:  StoredProcedure [Ecomm].[usp_DeleteCustomer]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_DeleteCustomer]
@flag bit output,-- return 0 for fail,1 for success
@Customer_Id int
AS
BEGIN
 BEGIN TRANSACTION 
 BEGIN TRY
   Delete from [Ecomm].[Customers] Where [Customer_Id]=@Customer_Id set @flag=1; 
   IF @@TRANCOUNT > 0
   BEGIN commit TRANSACTION;
   END
END TRY
BEGIN CATCH
   IF @@TRANCOUNT > 0
    BEGIN rollback TRANSACTION; 
  END
  set @flag=0; 
END CATCH 
END 

GO
/****** Object:  StoredProcedure [Ecomm].[usp_DeleteProductsAdmin]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_DeleteProductsAdmin]
@model_name varchar(20)
AS
Delete from [Ecomm].[Products] Where  [Model_Name]=@model_name;
GO
/****** Object:  StoredProcedure [Ecomm].[usp_DeleteShoppingCart]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [Ecomm].[usp_DeleteShoppingCart]    Script Date: 10/23/2018 2:18:29 PM ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [Ecomm].[usp_DeleteShoppingCart]
@Product_Id varchar(15)
AS
   Delete from Ecomm.ShoppingCart Where Product_Id=@Product_Id

GO
/****** Object:  StoredProcedure [Ecomm].[usp_DisplayCategories]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [Ecomm].[usp_DisplayCategories]
@catid varchar(15)

AS
Select * from [Ecomm].[Categories] where [Category_Id]=@catid
GO
/****** Object:  StoredProcedure [Ecomm].[usp_FetchDetails]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [Ecomm].[usp_FetchDetails]
@ProductId varchar(15)

AS
Select [Model_Name],[Unit_Cost],[P_Description]
from [Ecomm].[Products] where [Product_Id]=@ProductId
GO
/****** Object:  StoredProcedure [Ecomm].[usp_InsertCategories]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_InsertCategories]
@Category_Id varchar(15),
@Category_Name varchar(20),
@C_Description varchar(1000)
AS
Insert into [Ecomm].[Categories](Category_Id,Category_Name,C_Description) Values(@Category_Id,@Category_Name,@C_Description)

GO
/****** Object:  StoredProcedure [Ecomm].[usp_InsertCustomer]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_InsertCustomer]

@Full_Name varchar(30),
@Email varchar(30),
@Password varchar(15),
@Delivery_Address varchar(80)
AS
Insert into [Ecomm].[Customers]([Full_Name],[Email],[Password],[Delivery_Address]) Values(@Full_Name,@Email,@Password,@Delivery_Address)

GO
/****** Object:  StoredProcedure [Ecomm].[usp_InsertProducts]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_InsertProducts]
@Product_id varchar(15),
@category_id varchar(15),
@modelnum varchar(20),
@modelname varchar(20),
@unitcost int,
@description varchar(1000)
AS
Insert into [Ecomm].[Products]
([Product_Id],[Category_Id],[Model_Number],[Model_Name],[Unit_Cost],[P_Description])
 values(@Product_id,@category_id,@modelnum,@modelname ,@unitcost,@description)

GO
/****** Object:  StoredProcedure [Ecomm].[usp_InsertShoppingCart]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_InsertShoppingCart]
@quantity int,
@product_Id varchar(15),
@date_Created date
AS

Insert into [Ecomm].[ShoppingCart]([Quantity],[Product_Id],[Date_Created]) Values(@quantity,@product_Id,@date_Created)
 
 select * from  [Ecomm].[ShoppingCart];
GO
/****** Object:  StoredProcedure [Ecomm].[usp_Login]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Ecomm].[usp_Login]
@email	varchar(30),-- Add the parameters for the stored procedure here
@password varchar(15)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Email],[Password] from [Ecomm].[Customers] where [Email]=@email AND  [Password]=@password;
END

GO
/****** Object:  StoredProcedure [Ecomm].[usp_PopulateCart]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Ecomm].[usp_PopulateCart]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select  p.[Product_Id],p.[Model_Name],p.[Model_Number],c.[Quantity],p.[Unit_Cost],c.[Quantity]*p.[Unit_cost] AS Sub_Total
from [Ecomm].[Products] p JOIN [Ecomm].[ShoppingCart] c 
ON c.[Product_Id]=p.[Product_Id]

END
GO
/****** Object:  StoredProcedure [Ecomm].[usp_Price]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [Ecomm].[usp_Price]
as

 select SUM(Sub_Total) from [Ecomm].[Total]
GO
/****** Object:  StoredProcedure [Ecomm].[usp_SearchProduct]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_SearchProduct]
@flag bit output,-- return 0 for fail,1 for success
@modelname varchar(20)
AS
BEGIN
 BEGIN TRANSACTION 
 BEGIN TRY
   Select * from [Ecomm].[Products] where [Model_Name]=@modelname set @flag=1; 
   IF @@TRANCOUNT > 0
   BEGIN commit TRANSACTION;
   END
END TRY
BEGIN CATCH
   IF @@TRANCOUNT > 0
    BEGIN rollback TRANSACTION; 
  END
  set @flag=0; 
END CATCH 
END 


GO
/****** Object:  StoredProcedure [Ecomm].[usp_SearchProductModelName]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Ecomm].[usp_SearchProductModelName] @modelname varchar(20)
AS
Select * from [Ecomm].[Products] where [Model_Name]=@modelname

GO
/****** Object:  StoredProcedure [Ecomm].[usp_SelectAllCustomerDetails]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_SelectAllCustomerDetails]
AS
 Select * from [Ecomm].[Customers]

GO
/****** Object:  StoredProcedure [Ecomm].[usp_SelectAllProductDetails]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_SelectAllProductDetails]
AS
 Select * from [Ecomm].[Products];
GO
/****** Object:  StoredProcedure [Ecomm].[usp_SelectAllShoppingCartDetails]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_SelectAllShoppingCartDetails]
AS
 Select * from [Ecomm].[ShoppingCart]

GO
/****** Object:  StoredProcedure [Ecomm].[usp_UpdateCategories]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_UpdateCategories]
@Category_Id varchar(15),
@Category_Name varchar(20),
@C_Description varchar(1000)
 AS
	 Update [Ecomm].[Categories] set [Category_Name]=@Category_Name,[C_Description]=@C_Description
	 Where [Category_Id]=@Category_Id
GO
/****** Object:  StoredProcedure [Ecomm].[usp_UpdateCustomer]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_UpdateCustomer]
@flag bit output,-- return 0 for fail,1 for success
@Customer_Id int,
@Full_Name varchar(30),
@Email varchar(30),
@Password varchar(15),
@Delivery_Address varchar(80)
 AS
 BEGIN
  BEGIN TRANSACTION 
   BEGIN TRY
	 Update [Ecomm].[Customers] set [Full_Name]=@Full_Name,[Email]=@Email,[Password]=@Password,[Delivery_Address]=@Delivery_Address
	 Where [Customer_Id]=@Customer_Id
	 set @flag=1; 
	 IF @@TRANCOUNT > 0
	 	 BEGIN commit TRANSACTION;
		 END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN rollback TRANSACTION; 
		END
		set @flag=0;
	END CATCH
END 
GO
/****** Object:  StoredProcedure [Ecomm].[usp_UpdateProduct]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_UpdateProduct]
@Product_id varchar(15),
@category_id varchar(15),
@modelnum varchar(20),
@modelname varchar(20),
@unitcost int,
@description varchar(1000)
 AS
	 Update [Ecomm].[Products] 
	 set [Product_Id] = @Product_id,
		 [Category_Id]=@category_id ,
		 [Model_Number]=@modelnum,
		 
		 [Unit_Cost]=@unitcost ,
		 [P_Description]=@description
	 Where [Model_Name]=@modelname
GO
/****** Object:  StoredProcedure [Ecomm].[usp_UpdateShoppingCart]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Ecomm].[usp_UpdateShoppingCart]
@flag bit output,-- return 0 for fail,1 for success
@record_Id varchar(15),
@cart_Id int,
@quantity tinyint,
@product_Id varchar(15),
@date_Created date
 AS
 BEGIN
  BEGIN TRANSACTION 
   BEGIN TRY
	 Update [Ecomm].[ShoppingCart] 
	 set [Record_Id]=@record_Id,
		 [Quantity]=@quantity,
		 [Product_Id]=@product_Id ,
		 [Date_Created]=@date_Created
	 Where [Cart_Id]=@cart_Id
	 set @flag=1; 
	 IF @@TRANCOUNT > 0
	 	 BEGIN commit TRANSACTION;
		 END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN rollback TRANSACTION; 
		END
		set @flag=0;
	END CATCH
END 


GO
/****** Object:  View [Ecomm].[Total]    Script Date: 11/13/2018 9:47:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create view [Ecomm].[Total]
 as 
 select p.[Product_Id],p.[Model_Name],p.[Model_Number],c.[Quantity],p.[Unit_Cost],c.[Quantity]*p.[Unit_cost] AS Sub_Total
from [Ecomm].[Products] p JOIN [Ecomm].[ShoppingCart] c 
ON c.[Product_Id]=p.[Product_Id]
GO
