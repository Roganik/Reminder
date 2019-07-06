namespace Storage.Entities
{
    public class Message
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string FullText { get; set; }
        public MessageProcessingStatus Status { get; set; }
        
        public string ReminderText { get; set; }
        public string TimeText { get; set; }
        public string ParsedTimeText { get; set; }
        
        public User User { get; set; }
    }

    public enum MessageProcessingStatus
    {
        Unknown = 0,
        UnableToParse = 1,
        Scheduled = 2,
        Delivered = 3,
    }
}