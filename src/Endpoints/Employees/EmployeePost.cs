using IWantApp.Domain.Product;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {   //UserName é o "login", o identificador unico desse usuário
        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email};
        var result = userManager.CreateAsync(user,employeeRequest.Password).Result;
        if (!result.Succeeded){
            return Results.BadRequest(result.Errors.First());
        }
        //var resultadoDoClaim = contexto.Adicionar(usuárioDesejado,NovoClaim(chave/tipo,valor))
        var claimResult = 
            userManager.AddClaimAsync(user, new Claim("EmployeeCode",employeeRequest.EmployeeCode)).Result;
        if (!claimResult.Succeeded)
        {
            return Results.BadRequest(claimResult.Errors.First());
        }

        //agora, o claim do nome
        claimResult = 
            userManager.AddClaimAsync(user, new Claim("Name", employeeRequest.Name)).Result;
        if (!claimResult.Succeeded)
        {
            return Results.BadRequest(claimResult.Errors.First());
        }

        return Results.Created($"/employees/{user.Id}", user.Id);
    }
}
