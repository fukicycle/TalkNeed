namespace Chat.SampleApp.Models
{
    public class ModelBase
    {
        public ModelBase(
            Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
