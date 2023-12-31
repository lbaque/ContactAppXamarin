using Api.Contact.Context;
using Api.Contact.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(descriptions =>
    {
        return descriptions.First();
    });

    c.SwaggerDoc("1.0", new OpenApiInfo { Title = "Api.Contact", Version = "1.0" });

    c.OperationFilter<Infrastructure.Swagger.RemoveVersionFromParameter>();
    c.DocumentFilter<Infrastructure.Swagger.ReplaceVersionWithExactValueInPath>();


});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IVouchersWrapper, VouchersWrapper>();

builder.Services.AddDbContext<ApiContactContext>(options =>
{


    string cnn = Configuration.GetConnectionString("ApiContactContext");

    options.UseSqlServer(cnn,
    sqloptions =>
    {
        sqloptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);

    });


});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(x =>
               x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/1.0/swagger.json", "Api.Contact v1.0"));
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<ApiContactContext>();
app.Run();
