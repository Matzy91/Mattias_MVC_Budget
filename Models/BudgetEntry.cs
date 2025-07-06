namespace MyEconomy.Models
{
    public class BudgetEntry
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    public class BudgetViewModel
    {
        public List<BudgetEntry> Entries { get; set; } = [];
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}