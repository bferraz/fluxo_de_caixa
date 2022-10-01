namespace Core.Bus
{
    public abstract class Message
    {
        public string MessageType { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
