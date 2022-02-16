using AutoMapper;
using TopicsApi.AutomapperProfiles;
using Microsoft.Extensions.Http;
using TopicsApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//// Configuration of the backing services...
builder.Services.AddControllers();
//.ConfigureApiBehaviorOptions(options =>
//{
//    var builtInFactory = options.InvalidModelStateResponseFactory;

//    options.InvalidModelStateResponseFactory = context =>
//    {
//        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();

//        // Perform logging here.
//        // ...

//        // Invoke the default behavior, which produces a ValidationProblemDetails response.
//        // To produce a custom response, return a different implementation of IActionResult instead.
//        return builtInFactory(context);
//    };
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(config =>
{
    config.AddDefaultPolicy(pol =>
    {
        pol.AllowAnyOrigin();
        pol.AllowAnyMethod();
        pol.AllowAnyHeader(); // you can't allow this AND AllowCredentials.
    });
});

builder.Services.AddTransient<ILookupOnCallDevelopers, RpcDeveloperLookup>();


builder.Services.AddHttpClient<OnCallApiHttpClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("on-call-developer-api"));

    client.DefaultRequestHeaders.Add("User-Agent", "Topics Api");
    client.DefaultRequestHeaders.Add("Acccept", "application/json");
}).AddPolicyHandler(
    HttpPolicies.GetDefaultRetryPolicy())
    .AddPolicyHandler(
    HttpPolicies.GetCircuitBreakerPolicy()
   );
var mapperConfig = new MapperConfiguration(opts =>
{
    opts.AddProfile<TopicsProfile>();
    opts.AddProfile<ResourcesProfile>();
});

builder.Services.AddSingleton<MapperConfiguration>(mapperConfig);
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);
builder.Services.AddScoped<IProvideTopicsData, EfSqlTopicsData>();
builder.Services.AddScoped<IProvideResourcesData, EfSqlResourcesData>();
// The TopicsDataContext is set up as a Scoped service. You can inject it into your controllers, services, and stuff.
builder.Services.AddDbContext<TopicsDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("topics"));
});

// Building the actual application 
var app = builder.Build();

app.UseCors(); // OPTIONS request from browsers, by usuing the service we set up above.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run(); // blocking


