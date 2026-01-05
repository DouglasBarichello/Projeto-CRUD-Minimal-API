using Microsoft.Extensions.Caching.Distributed;
using Minimal_Api.Model;
using System.Text.Json;

namespace Minimal_Api.View
{
    public static class Endpoints
    {
        // Define o mapeamento modular de endpoints
        public static void MapEndpointsRoutes(this IEndpointRouteBuilder app)
        {
            // Agrupamento de rotas para garantir consistência.
            var group = app.MapGroup("/api/usuario");


            // Persistência de estado: Serializa e armazena o novo usuário no cache distribuído
            group.MapPost("/", async (Usuario usuario, IDistributedCache cache) =>
            {
                var usuarioJson = JsonSerializer.Serialize(usuario);

                await cache.SetStringAsync(usuario.Id.ToString(), usuarioJson);
                // Retorno 201 Created seguindo os padrões de maturidade de Richardson
                return Results.Created($"/api/usuario/exibir/{usuario.Id}", usuarioJson);

            });

            // Recuperação de dados: Implementa o padrão de busca por chave única.
            group.MapGet("/exibir/{id}", async (int id, IDistributedCache cache) =>
            {
                var data = await cache.GetStringAsync(id.ToString());

                if (string.IsNullOrEmpty(data))
                {
                    return Results.NotFound(new { mensagem = "Usuário não encontrado" });
                }

                var usuarioEncontrado = JsonSerializer.Deserialize<Usuario>(data);

                return Results.Ok(usuarioEncontrado);

            });

            // Consistência no Update: Valida a existência antes de sobrescrever o estado no cache
            group.MapPut("/atualizar/{id}", async (int id, Usuario usuarioNovo, IDistributedCache cache) =>
            {
                var data = await cache.GetStringAsync(id.ToString());

                if (String.IsNullOrEmpty(data))
                {
                    return Results.NotFound(new { mensagem = "Usuário não encontrado." });
                }

                // Sobrescreve o registro existente com a nova representação do recurso
                var usuarioEncontrado = JsonSerializer.Serialize<Usuario>(usuarioNovo);
                await cache.SetStringAsync(id.ToString(), usuarioEncontrado);

                return Results.Ok(usuarioNovo);
            });

            // Delete de Recurso: Garante a limpeza do estado no cache  em segundo plano.
            group.MapDelete("/delete/{id}", async (int id, IDistributedCache cache) =>
            {
                var data = await cache.GetStringAsync(id.ToString());

                if (String.IsNullOrEmpty(data))
                {
                    return Results.NotFound(new { mensagem = "Usuário não encontrado" });
                }

                var usuarioEncontrado = JsonSerializer.Deserialize<Usuario>(data);
                await cache.RemoveAsync(usuarioEncontrado.Id.ToString());
                // Retorno 204 NoContent.
                return Results.NoContent();

            });

            
        }

    }
}



