namespace EventPractice
{
    public class ProccessEventArgs
    {
        public DateTime CompletionTime { get; internal set; }
        public bool IsSuccessful { get; internal set; }
    }
}