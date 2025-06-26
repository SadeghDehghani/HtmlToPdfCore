namespace HtmlToPdf.Models
{
    public class InvoiceItem
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int Total => Quantity * UnitPrice;
    }

    public class InvoiceViewModel
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public int TotalAmount => Items.Sum(i => i.Total);
    }

}
