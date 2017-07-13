namespace Rio.SME.Domain.Filters
{
    public class Filter
    {
        public Filter()
        {
            Skip = 0;
            Take = int.MaxValue;
            TotalRecords = 0;
            Order = Order.Ascending;
        }

        public int Skip { get; set; }
        public int Take { get; set; }
        public int TotalRecords { get; set; }
        public Order Order { get; set; }
        public bool EnablePaging { get { return Take != int.MaxValue; } }

    }

    public enum Order
    {
        Ascending,
        Descending
    }
}
