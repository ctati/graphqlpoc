using graphqlpoc.Data;
using graphqlpoc.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPooledDbContextFactory<ReferenceTableContext>(
    opt => opt.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"));
builder.Services.AddScoped<ReferenceTableContext>(
    sp => sp.GetRequiredService<IDbContextFactory<ReferenceTableContext>>().CreateDbContext());

builder.Services
    .AddGraphQLServer()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddQueryType<ReferenceTableQuery>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// app.UseRouting();
app.MapControllers();
app.MapGraphQL();

app.Run();
