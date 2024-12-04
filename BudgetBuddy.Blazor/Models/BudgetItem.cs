using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BudgetBuddy.Models;

public class BudgetItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    [Required(ErrorMessage = "Inkomstkälla är obligatoriskt.")]
    public string Name { get; set; }
    
    [JsonPropertyName("amount")]
    [Range(1, double.MaxValue, ErrorMessage = "Beloppet måste vara större än 0.")]
    public decimal Amount { get; set; }
    
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }
    
    [JsonPropertyName("date")]
    public DateTime Date { get; set; } = DateTime.Now;
    
    [JsonPropertyName("isIncome")]
    public bool IsIncome { get; set; }
}