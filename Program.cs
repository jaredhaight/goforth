using System.Text.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

app.MapGet("/{name}", async Task<IResult> (string name) =>
{
    string fileName = "links.json";
    using FileStream openStream = File.OpenRead(fileName);                
    List<Link>? links = await JsonSerializer.DeserializeAsync<List<Link>>(openStream);
    Link? link = links?.Find(l => (l.Name).Equals(name, StringComparison.InvariantCultureIgnoreCase));

    if (link is null)
    {
        return Results.NotFound("Not Found");
    }
    else 
    {
        return Results.Redirect(link.Url, false, false);
    }
});

app.Run();

record Link(string Name, string Url);