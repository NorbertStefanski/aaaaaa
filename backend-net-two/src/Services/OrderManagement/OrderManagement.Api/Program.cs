using Api;
using AppLogic.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Api.Filters;
using OrderManagement.AppLogic;
using OrderManagement.AppLogic.Events;
using OrderManagement.Domain;
using OrderManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<IBarRepository, BarDbRepository>();
builder.Services.AddScoped<IOrderRepository, OrderDbRepository>();
builder.Services.AddScoped<IOrderFactory, Order.Factory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICocktailRepository, CocktailDbRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemDbRepository>();
builder.Services.AddScoped<OrderPlacedIntegrationEvent>();
builder.Services.AddScoped<OrderManagementDbInitializer>();


builder.Services.AddSingleton(provider => new ApplicationExceptionFilterAttribute(provider.GetRequiredService<ILogger<ApplicationExceptionFilterAttribute>>()));
builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ApplicationExceptionFilterAttribute>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRabbitMQEventBus(configuration);
builder.Services.AddScoped<BarReceivedEventHandler>();
builder.Services.AddScoped<BarDeleteEventHandler>();
builder.Services.AddScoped<MenuItemReceivedEventHandler>();
builder.Services.AddScoped<MenuItemDeleteEventHandler>();
builder.Services.AddScoped<CocktailReceivedEventHandler>();
builder.Services.AddScoped<CocktailUpdatedEventHandler>();
builder.Services.AddScoped<CocktailDeletedEventHandler>();

builder.Services.AddDbContext<OrderManagementContext>(options =>
{
    string connectionString = configuration["ConnectionString"];
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 15,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });
    //options.UseSqlServer(connectionString);
#if DEBUG
    options.UseLoggerFactory(LoggerFactory.Create(loggingBuilder => loggingBuilder.AddDebug()));
    options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        string identityUrl = builder.Configuration.GetValue<string>("Urls:IdentityUrl");
        options.Authority = identityUrl;
        options.Audience = "ordermanagement";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false
        };
    });

//var readPolicy = new AuthorizationPolicyBuilder()
//    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//    .RequireAuthenticatedUser()
//    .RequireClaim("scope", "ordermanagement.read")
//    .Build();

//builder.Services.AddSingleton(provider => new ApplicationExceptionFilterAttribute(provider.GetRequiredService<ILogger<ApplicationExceptionFilterAttribute>>()));
//builder.Services.AddControllers(options =>
//{
//    options.Filters.AddService<ApplicationExceptionFilterAttribute>();
//    options.Filters.Add(new AuthorizeFilter(readPolicy));
//});

var writePolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .RequireClaim("scope", "manage")
    .Build();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("write", writePolicy);
});

var app = builder.Build();

IServiceScope startUpScope = app.Services.CreateScope();
var initializer = startUpScope.ServiceProvider.GetRequiredService<OrderManagementDbInitializer>();
initializer.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AddEventBusSubscriptions(app);

app.Run();

void AddEventBusSubscriptions(IApplicationBuilder app)
{
    IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

    eventBus.Subscribe<BarReceivedIntegrationEvent, BarReceivedEventHandler>();
    eventBus.Subscribe<BarDeleteIntegrationEvent, BarDeleteEventHandler>();
    eventBus.Subscribe<MenuItemReceivedIntegrationEvent, MenuItemReceivedEventHandler>();
    eventBus.Subscribe<MenuItemDeleteIntegrationEvent, MenuItemDeleteEventHandler>();
    eventBus.Subscribe<CocktailReceivedIntegrationEvent, CocktailReceivedEventHandler>();
    eventBus.Subscribe<CocktailReceivedIntegrationEvent, CocktailUpdatedEventHandler>();
    eventBus.Subscribe<CocktailDeletedIntegrationEvent, CocktailDeletedEventHandler>();
}
