using System.Net;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using WebAppAd6;
using WebAppAd6.CustomAuthorize;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ForwardedHeadersOptions>(options => {
    options.KnownProxies.Add(IPAddress.Parse("192.168.50.146"));
});

var appSettingsSection = builder.Configuration.GetSection("AppSettings");

builder.Services.Configure<AppSettings>(appSettingsSection);
builder.Services.AddTransient<AppSettings>();

var appSettings = appSettingsSection.Get<AppSettings>();

builder.Services.AddTransient<IAuthorizationHandler, CustomAuthorizeHandler>();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate(options => {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            //options.EnableLdap("MILF.CO");
        }
    });
builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = options.DefaultPolicy;

    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder();
    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

    foreach (var domainName in appSettings.AvailableUsers) {
        options.AddPolicy(
                domainName,
                policy => {
                    policy.Requirements.Add(
                        new CustomAuthorizeRequirement(domainName));
                }
            );
    }

});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions {
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
