using Application.UseCases.Persons;
using Application.UseCases.Visits;
using Data;
using Data.Repositories;
using Domain;
using Domain.Abstractions;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//se hace asi para poder cambiar el PersonRepository por otro repositorio sin afectar el resto del codigo, se hace la inyeccion de dependencias
//por ejemplo si queremos usar sql ahora y luego queremos cambiar a mongoDB, solo cambiamos el repositorio y el resto del codigo no se ve afectado, esta en Data/Repositories/PersonRepository.cs

/*Metodo 1 inyeccion normal*/
//builder.Services.AddScoped<IRepository<personEntity, Guid>, PersonRepository>();
//builder.Services.AddScoped<ICodeRepository<personEntity>, PersonRepository>();



//conexion a db 
var conectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

/*Metodo 2 inyeccion encamsuldad (clase en data DependencyInjections)*/
builder.Services.AddData(conectionString);

//agregar todos los casos de uso a la inyeccion de dependencias
builder.Services.AddScoped<CreatePersonUseCase>();
builder.Services.AddScoped<GetPersonByIdUseCase>();
builder.Services.AddScoped<DeletePersonUseCase>();
builder.Services.AddScoped<UpdatePersonUseCase>();
builder.Services.AddScoped<GetAllPersonsUseCase>();
builder.Services.AddScoped<GetPersonByCodeUseCase>();

//los metodos de visit
builder.Services.AddScoped<GetActiveVisitsUseCase>();
builder.Services.AddScoped<GetAllVisitsUseCase>();
builder.Services.AddScoped<GetVisitsByPersonUseCase>();
builder.Services.AddScoped<RegisterEntryUseCase>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPersonsEndpoints(); //es lo que se creo en Endpoints/PersonsEndpoints.cs, es lo que se encarga de mapear las rutas y los casos de uso a los endpoints, es una forma de organizar el codigo y no tener todo en el Program.cs
app.MapVisitsEndpoints();

app.Run();

//ejemplo de extension method, se hace asi para agregar funcionalidades a una clase sin tener que modificar la clase original,
//en este caso se agrega el metodo Hi a la clase string, se hace con el keyword this y el tipo de dato al que se le va a agregar
//la funcionalidad, en este caso string, y luego se puede usar el metodo Hi como si fuera un metodo normal de la clase string
string name = "juan";
Console.WriteLine(name.Hi());

static class StringExtensions
{
    public static string Hi(this string str)
    {
        return "Hola" + str;
    }
}
