using Microsoft.AspNetCore.Components;

namespace PayRemind.Data
{
    public class SwipeItemNotification
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ElementReference ElementReference { get; set; }
        public double StartX { get; set; }
        public bool IsPaid { get; set; }
        public string DateNotification { get; set; } = string.Empty;
        public decimal Mount { get; set; } = 0;
    }
}
