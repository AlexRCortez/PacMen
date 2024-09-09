namespace PacMen.BL.Models
{
    public class Score
    {
        public Guid Id { get; set; }
        public int Scores { get; set; }
        public DateTime Date { get; set; }
        public int Level { get; set; }
    }
}
