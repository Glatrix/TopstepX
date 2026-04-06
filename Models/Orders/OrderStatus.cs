namespace TopstepX.Models.Orders
{
    public enum OrderStatus
    {
        None = 0,
        Open = 1,
        Filled = 2,
        Cancelled = 3,
        Expired = 4,
        Rejected = 5,
        Pending = 6,
        PendingCancellation = 7,
        Suspended = 8
    }
}
