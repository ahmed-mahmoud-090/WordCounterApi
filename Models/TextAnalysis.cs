public class TextAnalysis
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int WordCount { get; set; }
    public int CharacterCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
