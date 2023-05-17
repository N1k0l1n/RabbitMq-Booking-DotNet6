namespace FormulaAirline.API.Services
{
    public interface IMessageProducer
    {
        //Method Responsable for sending a Message
        public void SendMessage<T>(T message);
    }
}
