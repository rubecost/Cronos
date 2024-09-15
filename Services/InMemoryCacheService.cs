using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace Cronos.Services
{
    public class InMemoryCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public InMemoryCacheService()
        {
            // Inicializa o MemoryCache
            _cache = new MemoryCache(new MemoryCacheOptions());

            // Define as opções do cache (expira após 30 minutos)
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10) // Se acessado, renova por mais 10 minutos
            };
        }

        // Método genérico para adicionar dados ao cache
        public void AdicionarAoCache<T>(string chave, T valor)
        {
            _cache.Set(chave, valor, _cacheOptions);
        }

        // Método genérico para obter dados do cache
        public T ObterDoCache<T>(string chave)
        {
            return _cache.TryGetValue(chave, out T valor) ? valor : default;
        }

        // Verifica se a chave existe no cache
        public bool ExisteNoCache(string chave)
        {
            return _cache.TryGetValue(chave, out _);
        }

        // Remove um item do cache
        public void RemoverDoCache(string chave)
        {
            _cache.Remove(chave);
        }

        // Atualiza um item no cache (ou adiciona se não existir)
        public void AtualizarCache<T>(string chave, T valor)
        {
            if (ExisteNoCache(chave))
            {
                RemoverDoCache(chave);
            }
            AdicionarAoCache(chave, valor);
        }

        // Método específico para cache de páginas
        public class DataCacheService<T>
        {
            private readonly Dictionary<int, List<T>> _cachedPages = new();

            // Cacheia uma página específica
            public void CachePage(int pageNumber, List<T> items)
            {
                if (!_cachedPages.ContainsKey(pageNumber))
                {
                    _cachedPages[pageNumber] = items;
                }
            }

            // Obtém uma página do cache
            public List<T>? GetCachedPage(int pageNumber)
            {
                _cachedPages.TryGetValue(pageNumber, out var items);
                return items;
            }
        }
    }
}
