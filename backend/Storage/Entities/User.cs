using System;

namespace Storage.Entities
{
    public class User
    {
        public int ID { get; set; }
        public int TelegramID { get; set; }
        public object TimeZone => throw new NotImplementedException(); 
    }
}