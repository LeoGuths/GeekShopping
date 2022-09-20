using System.Security.Claims;
using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace GeekShopping.IdentityServer.Initializer; 

public class DbInitializer : IDbInitializer {
    private readonly MySqlContext _context;
    private readonly UserManager<ApplicationUser> _user;
    private readonly RoleManager<IdentityRole> _role;

    public DbInitializer(MySqlContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role) {
        _context = context;
        _user = user;
        _role = role;
    }

    public void Initialize() {
        if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
        _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
        _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();
        
        var admin = new ApplicationUser {
          UserName  = "leonardo-admin",
          Email  = "leonardo-admin@kiefer.com.br",
          EmailConfirmed = true,
          PhoneNumber = "+55 (47) 12345-6789",
          FirstName = "Leonardo",
          LastName = "Admin"
        };
        _user.CreateAsync(admin, "Leonardo123@").GetAwaiter().GetResult();
        _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
        
        var adminClaims = _user.AddClaimsAsync(admin, new Claim[] {
            new(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
            new(JwtClaimTypes.GivenName, admin.FirstName),
            new(JwtClaimTypes.FamilyName, admin.LastName),
            new(JwtClaimTypes.Role, IdentityConfiguration.Admin)
        }).Result;
        
        var client = new ApplicationUser {
            UserName  = "leonardo-client",
            Email  = "leonardo-client@kiefer.com.br",
            EmailConfirmed = true,
            PhoneNumber = "+55 (47) 12345-6789",
            FirstName = "Leonardo",
            LastName = "client"
        };
        _user.CreateAsync(client, "Leonardo123@").GetAwaiter().GetResult();
        _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
        
        var clientClaims = _user.AddClaimsAsync(client, new Claim[] {
            new(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
            new(JwtClaimTypes.GivenName, client.FirstName),
            new(JwtClaimTypes.FamilyName, client.LastName),
            new(JwtClaimTypes.Role, IdentityConfiguration.Client)
        }).Result;
    }
}