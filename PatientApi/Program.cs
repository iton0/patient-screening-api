using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;
using PatientApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PatientDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PatientApi",
        Description = "Patient API Learning Project",
        Version = "v1"
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient API V1");
    });
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/patients", async (PatientDb db) =>
{
    // 1. Fetch from DB into memory first
    var patients = await db.Patients.ToListAsync();

    // 2. Map them in-memory and return
    return patients.Select(patient => patient.ToDto());
});
app.MapPost("/patient", async (PatientDb db, Patient patient) =>
{
    await db.Patients.AddAsync(patient);
    await db.SaveChangesAsync();
    return Results.Created($"/patient/{patient.Id}", patient);
});
app.MapDelete("/patient/{id}", async (PatientDb db, int id) =>
{
    var patient = await db.Patients.FindAsync(id);
    if (patient is null)
    {
        return Results.NotFound();
    }
    db.Patients.Remove(patient);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
