using Pedidos.Application.DTOs;
using Refit;

namespace Pedidos.Application.Integrations;

public  interface  IProdutoIntegrationService
{
    [Get("/v1/produtos")]
    Task<IEnumerable<ProdutoRetornoDTO>> Listar(string ids);
}