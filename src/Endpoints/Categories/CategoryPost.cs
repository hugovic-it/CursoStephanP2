using IWantApp.Domain.Product;
using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category(categoryRequest.Name, "Test", "Test");

        if (!category.IsValid)
        {
            return Results.BadRequest(category.Notifications);
        }
        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categoires/{category.Id}", category.Id);
    }

}
