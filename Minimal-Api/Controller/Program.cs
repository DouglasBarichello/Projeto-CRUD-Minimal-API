using Minimal_Api.View;


//Incializa o builder da aplicação Web da API
var builder = WebApplication.CreateBuilder(args);

//Habilita Serviço de Memoria Cache
builder.Services.AddDistributedMemoryCache();

//Habilita Metodos para Endpoints da API
builder.Services.AddEndpointsApiExplorer();

//Habilita os Serviços WebApplication
var app = builder.Build();

//Chama as Rota dos Endpoint
app.MapEndpointsRoutes();

//Redireciona para rota Https/Htpp
app.UseHttpsRedirection();


app.Run();