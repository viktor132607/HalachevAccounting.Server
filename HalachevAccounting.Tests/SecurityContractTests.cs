using System.Reflection;
using HalachevAccounting.Api.Controllers;
using HalachevAccounting.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace HalachevAccounting.Tests;

public sealed class SecurityContractTests
{
    [Fact]
    public void ContactManagementEndpointsRequireAdmin()
    {
        Type type = typeof(ContactRequestsController);

        foreach (string methodName in new[] { "GetAll", "GetById", "Update" })
        {
            MethodInfo method = type.GetMethod(methodName)!;
            AuthorizeAttribute? authorize = method.GetCustomAttribute<AuthorizeAttribute>();
            Assert.NotNull(authorize);
            Assert.Equal("Admin", authorize!.Roles);
        }
    }

    [Fact]
    public void PublicContactSubmissionIsRateLimited()
    {
        MethodInfo method = typeof(ContactRequestsController).GetMethod("Create")!;
        Assert.NotNull(method.GetCustomAttribute<AllowAnonymousAttribute>());
        EnableRateLimitingAttribute? rate = method.GetCustomAttribute<EnableRateLimitingAttribute>();
        Assert.NotNull(rate);
        Assert.Equal("contact", rate!.PolicyName);
    }

    [Fact]
    public void AuthenticationMutationEndpointsAreRateLimited()
    {
        Type type = typeof(AuthController);
        foreach (string methodName in new[] { "Register", "Login", "ForgotPassword", "ResetPassword", "ChangePassword" })
        {
            EnableRateLimitingAttribute? rate =
                type.GetMethod(methodName)!.GetCustomAttribute<EnableRateLimitingAttribute>();
            Assert.NotNull(rate);
            Assert.Equal("auth", rate!.PolicyName);
        }
    }

    [Fact]
    public void BackupEndpointsRequireAdmin()
    {
        AuthorizeAttribute? authorize =
            typeof(DatabaseBackupController).GetCustomAttribute<AuthorizeAttribute>();
        Assert.NotNull(authorize);
        Assert.Equal("Admin", authorize!.Roles);
    }
}
