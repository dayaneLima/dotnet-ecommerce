using Produtos.Application.Interfaces;
using Produtos.Domain.Repository;
using Produtos.Application.DTOs;
using AutoMapper;
using Produtos.Domain.Models;
using Produtos.Domain.Exceptions;

namespace Produtos.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<ProdutoRetornoDTO> Inserir(ProdutoDTO produtoDTO)
    {
        var produto = _mapper.Map<Produto>(produtoDTO);
        var validacao = produto.Validar();
        if (!validacao.IsValid) throw new EntityErrorException($"Dados do produto {produtoDTO.Nome} inválido(s)", validacao);

        _produtoRepository.Inserir(produto);
        await _produtoRepository.UnitOfWork.Commit();

        return _mapper.Map<ProdutoRetornoDTO>(produto);
    }

    public async Task<ProdutoRetornoDTO> Atualizar(int id, ProdutoDTO produtoDTO)
    {
        var produtoBanco = await Obter(id);

        if (produtoBanco is null)
            throw new NotFoundException("Produto não encontrado!");

        var produto = _mapper.Map<Produto>(produtoDTO);
        produto.Id = id;

        var validacao = produto.Validar();
        if (!validacao.IsValid) throw new EntityErrorException($"Dados do produto {produtoDTO.Nome} inválido(s)", validacao);

        _produtoRepository.Atualizar(produto);
        await _produtoRepository.UnitOfWork.Commit();

        return _mapper.Map<ProdutoRetornoDTO>(produto);
    }

    public async Task<ProdutoRetornoDTO> Obter(int id)
    {
        var produto = await _produtoRepository.ObterPorId(id);

        if (produto is null)
            throw new NotFoundException("Produto não encontrado!");

        return _mapper.Map<ProdutoRetornoDTO>(produto);
    }

    public async Task Excluir(int id)
    {
        _produtoRepository.Excluir(id);
        var produtoExcluido = await _produtoRepository.UnitOfWork.Commit();
        
        if (!produtoExcluido)
            throw new NotFoundException("Produto não encontrado!");
    }

    public async Task<IEnumerable<ProdutoRetornoDTO>> Listar()
    {
        var produtos = await _produtoRepository.ObterTodos();
        return _mapper.Map<IEnumerable<ProdutoRetornoDTO>>(produtos);
    }
}
  