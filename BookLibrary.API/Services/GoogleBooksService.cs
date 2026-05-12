
using BookLibrary.API.Models;
using BookLibrary.Application.Dtos.BookDtos;
using BookLibrary.Application.Services;
using BookLibrary.Domain;
using BookLibrary.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace BookLibrary.API.Services
{
    public class GoogleBooksService
    {
        private readonly HttpClient _httpClient;
     
        public GoogleBooksService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<BookResponse> SearchBookAsync(string query)
        {
           var encodedQuery = Uri.EscapeDataString(query);  
            var url = $"https://www.googleapis.com/books/v1/volumes?q={encodedQuery}";
            var response = await _httpClient.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return new BookResponse();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<BookResponse>(json, options);
            return data ?? new BookResponse();
        }
        public async Task<BookResponse> SearchByCriteriaBookAsync(string? type,string query)      {
            var url = "";
            var encodedQuery = Uri.EscapeDataString(query);
            if (!string.IsNullOrWhiteSpace(type))
            {
                url = $"https://www.googleapis.com/books/v1/volumes?q={type}:{encodedQuery}";
            }
            else
            {

                url = $"https://www.googleapis.com/books/v1/volumes?q={encodedQuery}";
            }
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return new BookResponse();
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<BookResponse>(json, options);
            return data ?? new BookResponse();
        }
    }
}
