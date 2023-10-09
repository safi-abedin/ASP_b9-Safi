namespace EventPractice
{
    public class EmailEventArgs
    {
       
        public bool IsSuccessful { get; internal set; }
        public DateTime CompletionTime { get; internal set; }
    }
}