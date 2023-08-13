namespace OrderManagement.Api.Models
{
    public class OrderCreateModel
    {
        public Guid BarId { get; set; }
        public int TableId { get; set; }
        public int? UserId { get; set; }
        public List<Guid> OrderedItemIds { get; set; }
        public double OrderTotal { get; set; }
    }
}
