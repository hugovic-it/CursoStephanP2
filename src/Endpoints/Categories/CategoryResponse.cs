namespace IWantApp.Endpoints.Categories;
//Essa clase funciona como o DTO 
public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
}