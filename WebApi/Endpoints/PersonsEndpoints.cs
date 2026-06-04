using Application.DTOs.Persons;
using Application.UseCases.Persons;
using System.Runtime.InteropServices;

namespace WebApi.Endpoints
{
    public static class PersonsEndpoints //debe ser static para poder extender
    {
        public static void MapPersonsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/persons").WithTags("Persons");

            group.MapGet("/{id:guid}", async (Guid id, GetPersonByIdUseCase useCase
                ) =>
            {
                try
                {
                    var person = await useCase.ExecuteAsync(id);
                    return Results.Ok(person);
                }
                catch(InvalidOperationException ex)
                {
                    return Results.NotFound(new { error= ex.Message});
                }
            }
            ).WithName("GetPersonById")
            .WithSummary("Obtener una persona por su id")
            /*.Produces(200) */.Produces(StatusCodes.Status200OK)
            .Produces(404);

            group.MapPost("/", async (CreatePersonDto dto, CreatePersonUseCase useCase
                ) =>
            {
                try
                {
                    var person = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/persons/{person.Id}", person);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }
            ).WithName("CreatePerson")
            .WithSummary("Crea una nueva persona")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/", async (GetAllPersonsUseCase useCase) =>
            {
                try
                {
                    var personas = await useCase.ExecuteAsync();
                    return Results.Ok(personas);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("GetAllPersons")
            .WithSummary("Obtener todas las personas")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/{id:guid}", async (Guid id, UpdatePersonDto dto, UpdatePersonUseCase useCase) =>
            {
                if (id != dto.Id)
                {
                    return Results.BadRequest("Las ids no corresponden");
                }

                try
                {
                    var person = await useCase.ExecuteAsync(dto);
                    return Results.Ok(person);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new {error = ex.Message});
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch(Exception e)
                {
                    return Results.InternalServerError(e.Message);
                }
            }). WithName("UpdatePerson")
            .WithDescription("Actualiza una persona existente")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id:guid}", async (Guid id, DeletePersonUseCase useCase) =>
            {
                try
                {
                    await useCase.ExecuteAsync(id);
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new {error=ex.Message});
                }
                catch (Exception e)
                {
                    return Results.InternalServerError(e.Message);
                }
            }).WithName("DeletePerson")
            .WithDescription("Elimina una persona")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/code/{code}", async (string code, GetPersonByCodeUseCase useCase) =>
            {
                try
                {
                    var person = await useCase.ExecuteAsync(code);
                    return Results.Ok(person);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            })
                .WithName("GetPersonByCode")
                .WithDescription("Obtiene una persona por su codigo");
        }

    }
}
