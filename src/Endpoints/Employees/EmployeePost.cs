using IWantApp.Domain.Product;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {   //UserName é o "login", o identificador unico desse usuário
        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email};
        var result = userManager.CreateAsync(user,employeeRequest.Password).Result;

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors.First());
        }

        return Results.Created($"/employees/{user.Id}", user.Id);
    }

}
