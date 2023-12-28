var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/* option.ReturnHttpNotAcceptable is used for content negotiation that means if the server accept on the xml and if the
 * server does not satisfy this request, then HTTP 406 Not Acceptable status code.
 * AddXmlDataContractSerializerFormatters method is used to configure the MVC (Model-View-Controller) framework to include 
 * formatters for XML serialization using the DataContractSerializer.
 * i.e, After adding this line, your API can handle requests and responses in both JSON and XML formats based on the client's 
 * preferences specified in the "Accept" header of the HTTP request.
 */
builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = true;
}
    ).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
