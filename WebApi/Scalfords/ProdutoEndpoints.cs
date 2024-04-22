using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;

namespace WebApi.Scalfords
{
    public static class ProdutoEndpoints
    {
        public static void MapProdutoEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Produto").WithTags(nameof(Produto));

            group.MapGet("/", async (WebApiContext db) =>
            {
                return await db.Produto.ToListAsync();
            })
            .WithName("GetAllProdutos")
            .WithOpenApi();

            group.MapGet("/{id}", async Task<Results<Ok<Produto>, NotFound>> (Guid id, WebApiContext db) =>
            {
                return await db.Produto.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.Id == id)
                    is Produto model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetProdutoById")
            .WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Produto produto, WebApiContext db) =>
            {
                var affected = await db.Produto
                    .Where(model => model.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                      .SetProperty(m => m.Id, produto.Id)
                      .SetProperty(m => m.Nome, produto.Nome)
                      .SetProperty(m => m.Preco, produto.Preco)
                      .SetProperty(m => m.Estoque, produto.Estoque)
                      );
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateProduto")
            .WithOpenApi();

            group.MapPost("/", async (Produto produto, WebApiContext db) =>
            {
                db.Produto.Add(produto);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Produto/{produto.Id}", produto);
            })
            .WithName("CreateProduto")
            .WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, WebApiContext db) =>
            {
                var affected = await db.Produto
                    .Where(model => model.Id == id)
                    .ExecuteDeleteAsync();
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteProduto")
            .WithOpenApi();
        }
    }
}

}
