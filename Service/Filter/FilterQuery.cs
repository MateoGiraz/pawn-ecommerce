namespace Service.Filter.ConcreteFilter;

public class FilterQuery
{
    public IdFilterCriteria? CategoryId { get; set; }
    public IdFilterCriteria? BrandId { get; set; }
    public StringFilterCriteria? Name { get; set; }
}