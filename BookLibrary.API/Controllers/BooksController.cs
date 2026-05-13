using BookLibrary.API.Services;
using BookLibrary.Application.Dtos.BookDtos;
using BookLibrary.Application.Services;
using BookLibrary.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace BookLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookService _service;
        private IValidator<CreateBookDto> _validator;
        private GoogleBooksService _googleBooksService;
        private ILogger<BooksController> _logger;



        public BooksController(BookService service, IValidator<CreateBookDto> validator, GoogleBooksService googleBooksService, ILogger<BooksController> logger)
        {
            _service = service;
            _validator = validator;
            _googleBooksService = googleBooksService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            return Ok(await _service.GetAllAsync());
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBookDto request)
        {
            try
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors.Select(a => a.ErrorMessage));
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _service.AddAsync(request, userId!);
                _logger.LogInformation("Book created successfully.Title:{Title}", request.Title);
                return Ok("created");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex," Error occurred while creating book. Title:{Title}", request.Title);
             return StatusCode(500, "An error occurred while creating the book.");
            }
         
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateBookDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _service.UpdateAsync(request, userId);
            if (!result)
                return NotFound("güncellenemedi");
            return Ok("Updated successfully");
        }
        [Authorize]
        [HttpDelete("{id}")]
        //api/books/id
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _service.DeleteAsync(id, userId);
            if (!result) return NotFound("silemezsiniz");
            return Ok("deleted");
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync(string query)
        { 
            var result = await _googleBooksService.SearchBookAsync(query);
            if (result?.Items is null|| !result.Items.Any())
            {
                return Ok(new { 
                Message="Şu anda veri alınamıyor veya sonuç alınamadı",
                Data=new List<object>()
                });
            }
            var books = result?.Items?.Select(item => new
            {
                Id = item.Id ?? string.Empty,
                Title = item.VolumeInfo?.Title ?? string.Empty,
                Subtitle = item.VolumeInfo?.SubTitle ?? string.Empty,
                Authors = item.VolumeInfo?.Authors != null
                ? item.VolumeInfo.Authors.ToList()
                : new List<string>(),
                SelfLink = item.SelfLink ?? string.Empty,
                Kind = result.Kind ?? string.Empty
            }).ToList();
            return Ok(books);
            
        }

        [HttpPost("SaveFromGoogleBook")]
        public async Task<IActionResult> SaveFromGoogleBookAsync(string? type,string query )
        {
          
            var response= await _googleBooksService.SearchByCriteriaBookAsync(type, query);
            if (response?.Items is null || !response.Items.Any())
            {
                return Ok(new { 
                
                Message="Şu anda veri alınamıyor veya sonuç alınamadı",
                Data=new List<object>()
                
                });
            }
            var createBookDto = new CreateBookDto { 
            
            Title=response.Items.FirstOrDefault()?.VolumeInfo?.Title ?? string.Empty,
            Description=response.Items.FirstOrDefault()?.VolumeInfo?.Description ?? string.Empty,
            SubTitle=response.Items.FirstOrDefault()?.VolumeInfo?.SubTitle ?? string.Empty,
            GoogleId= response.Items.FirstOrDefault()?.Id ?? string.Empty,
            Authors=response.Items.FirstOrDefault()?.VolumeInfo?.Authors != null
                ? string.Join(", ", response.Items.FirstOrDefault()?.VolumeInfo?.Authors)
                : string.Empty

            };
            await _service.AddBookFromGoogleAsync(createBookDto);
            return Ok("Book saved successfully");
        }
    }
}
