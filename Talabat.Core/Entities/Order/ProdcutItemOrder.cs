namespace Talabat.Core.Entities.Order
{
    public class ProdcutItemOrder
    {
        public ProdcutItemOrder(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }
        public ProdcutItemOrder()
        {
            
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}