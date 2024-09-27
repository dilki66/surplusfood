namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class RefResponse<T> where T : class
    {
        public List<T>? ReferenceList { get; set; }
    }
}
