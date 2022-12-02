namespace ProjectTasksCosmosApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

public abstract class AuthorizedControllerBase : ControllerBase
{
    private string[] _scopeRequiredByApi;

    public AuthorizedControllerBase(IConfiguration configuration) : base()
    {
        _scopeRequiredByApi = new string[] { configuration["AppAuth:PermissionsScope"]! };
    }

    protected string[] ScopeRequiredByApi
    {
        get => _scopeRequiredByApi;
    }

    protected void VerifyUserPermissions()
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(ScopeRequiredByApi);
    }
}
