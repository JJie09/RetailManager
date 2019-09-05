using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData
    {
        //public List<ProductModel> GetProducts()
        //{
        //    SqlDataAccess sql = new SqlDataAccess();
        //    var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");
        //    return output;
        //}
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            decimal taxRate = ConfigHelper.GetTaxRate() / 100;

            saleInfo.SaleDetails.ForEach((product) =>
                {
                    SaleDetailDBModel detail = new SaleDetailDBModel
                    {
                        ProductId = product.ProductId,
                        Quantity = product.Quantity
                    };
                    var productInfo = products.GetProductById(product.ProductId);
                    if (productInfo == null)
                    {
                        throw new Exception($"The product Id of {product.ProductId} could not be found in database");
                    }
                    detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;
                    if (productInfo.IsTaxable)
                        detail.Tax = detail.PurchasePrice * taxRate;

                    details.Add(detail);
                });

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId,
            };
            sale.Total = sale.SubTotal + sale.Tax;




            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    sql.StartTransaction("RMData");
                    sql.SaveDataInTransaction<SaleDBModel>("dbo.spSale_Insert", sale);
                    sale.Id = sql.LoadDataInTransaction<int, dynamic>("spSale_Lookup", new { CashierId = sale.CashierId, SaleDate = sale.SaleDate }).FirstOrDefault();
                    details.ForEach((item) =>
                    {
                        item.SaleId = sale.Id;
                        sql.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
                    });
                    sql.CommitTransaction();
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }




        }
    }
}
