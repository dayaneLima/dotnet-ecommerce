using Refit;
using Pedidos.Application.DTOs;

namespace Pedidos.Application.Integrations;

public  interface  IProdutoIntegrationService
{
    [Get("/v1/produtos")]
    Task<IEnumerable<ProdutoDTO>> Listar(string ids);
}