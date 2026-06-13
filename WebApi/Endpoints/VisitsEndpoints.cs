using Application.DTOs.Visit;
using Application.UseCases.Visits;

namespace WebApi.Endpoints
{
    public static class VisitsEndpoints
    {
        public static void MapVisitsEndpoints(this IEndpointRouteBuilder app)
        {
           var group = app.MapGroup("/api/visits").WithTags("Visits");

            group.MapGet("/", async (GetAllVisitsUseCase useCase) =>
            {
                try
                {
                    var visits = await useCase.ExecuteAsync();
                    return Results.Ok(visits);
                }
                catch (Exception e)
                {
                    return Results.InternalServerError(e.Message);
                }
            }).WithName("GetAllVisits")
            .WithSummary("Obtener todas las visitas")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/entry", async (RegisterEntryDto dto, RegisterEntryUseCase useCase) =>
            {
                try
                {
                    var visit = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/visits/{visit.Id}",visit);
                }
                catch (InvalidOperationException e)
                {
                    return Results.BadRequest(new { error = e.Message });
                }
                catch(ArgumentException e)
                {
                    return Results.BadRequest(new { error = e.Message });
                }
                catch (Exception e)
                {
                    return Results.InternalServerError(e.Message);
                }
            }).WithName("RegisterEntry")
            .WithSummary("Registrar entrada de una persona (por personId o Code)")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapGet("/active", async (GetActiveVisitsUseCase useCase) =>
            {
                try
                {
                    var visits = await useCase.ExecuteAsync();
                    return Results.Ok(visits);
                }
                
                catch (Exception e)
                {
                    return Results.InternalServerError(e.Message);
                }
            }).WithName("GetActiveVisits")
            .WithDescription("Obtener todas las visitas activas (personas dentro)")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/person/{personId:guid}", async (Guid personId, GetVisitsByPersonUseCase useCase) =>
            {
                try
                {
                    var visits = await useCase.ExecuteAsync(personId);
                    return Results.Ok(visits);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("GetVisitsByPerson")
            .WithDescription("Obtener historial de visitas de una persona")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/exit", async (RegisterExitDto dto, RegisterExitUseCase useCase) =>
            {
                try
                {
                    var visit = await useCase.ExecuteAsync(dto);
                    return Results.Ok(visit);
                }
                catch (InvalidOperationException e)
                {
                    return Results.BadRequest(new { error = e.Message });
                }
                catch (ArgumentException e)
                {
                    return Results.BadRequest(new { error = e.Message });
                }
                catch (Exception e)
                {
                    return Results.InternalServerError(e.Message);
                }
            }).WithName("RegisterExit")
            .WithDescription("Registra la salida de una persona(por visitId o Code")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
