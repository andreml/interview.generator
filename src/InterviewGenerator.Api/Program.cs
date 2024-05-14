using FluentValidation.AspNetCore;
using InterviewGenerator.Api.Configuration;
using InterviewGenerator.CrossCutting.InjecaoDependencia;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRepository();
builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddContextConfig();
builder.Configuration.AddJsonFile("appsettings.json").Build();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddMassTransitConfigSender(builder.Configuration);

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ValidateModelStateAttributeCollectionExtension));
});
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors",
                          policy =>
                          {
                              policy.AllowAnyHeader().AllowCredentials().AllowAnyOrigin().AllowAnyMethod().SetIsOriginAllowed((host) => true)
                          });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0.0.1");
    options.RoutePrefix = "swagger";
});

app.UseCors("cors");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Para teste integrado
public partial class Program { }
