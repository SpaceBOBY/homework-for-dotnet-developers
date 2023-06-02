namespace OrderManager.Exceptions
{
    public class OrderPlacementException : Exception
    {
        public OrderPlacementException()
        {
        }

        public OrderPlacementException(string message)
            : base(message)
        {
        }

        public OrderPlacementException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
