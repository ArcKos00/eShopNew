using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.Models.Requests;
using MVC.ViewModels;

namespace MVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IOptions<AppSettings> _options;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = options;
        }

        public async Task<Catalog> GetCatalogItems(int page, int take, int? anomaly, int? type, int? meet)
        {
            var filters = new Dictionary<TypeFilter, int>();

            if (anomaly.HasValue)
            {
                filters.Add(TypeFilter.Anomaly, anomaly.Value);
            }

            if (type.HasValue)
            {
                filters.Add(TypeFilter.Type, type.Value);
            }

            if (meet.HasValue)
            {
                filters.Add(TypeFilter.Meets, meet.Value);
            }

            var result = await _httpClient.SendAsync<Catalog, PaginatedItemRequest<TypeFilter>>(
                $"{_options.Value.CatalogUrl}/items",
                HttpMethod.Post,
                new PaginatedItemRequest<TypeFilter>()
                {
                    PageIndex = page,
                    PageSize = take,
                    Filter = filters
                });

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetAnomalies()
        {
            var result = await _httpClient.SendAsync<List<CatalogAnomaly>, object>($"{_options.Value.CatalogUrl}/getanomalies", HttpMethod.Post, null);
            return result.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();
        }

        public async Task<IEnumerable<SelectListItem>> GetMeets()
        {
            var result = await _httpClient.SendAsync<List<CatalogMeets>, object>($"{_options.Value.CatalogUrl}/getmeets", HttpMethod.Post, null);
            return result.Select(s => new SelectListItem()
            {
                Text = s.Meets,
                Value = s.Id.ToString()
            }).ToList();
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            var result = await _httpClient.SendAsync<List<CatalogType>, object>($"{_options.Value.CatalogUrl}/gettypes", HttpMethod.Post, null);
            return result.Select(s => new SelectListItem()
            {
                Text= s.Name,
                Value = s.Id.ToString()
            }).ToList();
        }
    }
}
