using FluentValidation.AspNetCore;
using InterviewGenerator.Api.Configuration;
using InterviewGenerator.CrossCutting.InjecaoDependencia;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRepository();
builder.Services.AddValidators();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddContextConfig();
builder.Configuration.AddJsonFile("appsettings.json").Build();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddMassTransitConfigSender(builder.Configuration);

///*Mass Transit*/
//var configuration = builder.Configuration; //traz os dados configuration
//var servidor = configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
//var usuario = configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
//var senha = configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;

///*UsingInMemory - bom pra teste unitário*/
//builder.Services.AddMassTransit((x => {
//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host(servidor, "/", h =>
//        {
//            h.Username(usuario);
//            h.Password(senha);
//        });

//        cfg.ConfigureEndpoints(context);
//    });
//}));

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ValidateModelStateAttributeCollectionExtension));
});
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0.0.1");
        options.RoutePrefix = "swagger";
    });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Para teste integrado
public partial class Program { }
