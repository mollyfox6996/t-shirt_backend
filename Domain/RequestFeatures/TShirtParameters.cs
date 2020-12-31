namespace Domain.RequestFeatures
{
    public class TshirtParameters : RequestParameters
    {
        public TshirtParameters()
        {
            OrderBy = "name";
        }

        public string Category { get; set; }
        public string Gender { get; set; }
        public string Author { get; set; }
    }
}
