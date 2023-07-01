namespace App
{
    public class CurrencyModel
    {
        public string inputValue { get; set; } = string.Empty;
        public double resultValue { get; set; }
        public string originCurrency { get; set; } = string.Empty;
        public string finalCurrency { get; set; } = string.Empty;
    }
}