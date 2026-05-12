using AutoMapper;
using BookLibrary.Application.Dtos.BookDtos;
using BookLibrary.Application.Mapping;
using BookLibrary.Domain;
using BookLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Application.Services;

public class BookService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BookService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ResultBookDto>> GetAllAsync()
    {

        var books = await _context.Books
            .Include(a=>a.AppUser)
            .ToListAsync();
        var mapping = _mapper.Map<List<ResultBookDto>>(books);
    return mapping;
    
    }
    public async Task AddAsync(CreateBookDto bookDto,string userId)
    {
        var book = _mapper.Map<Book>(bookDto);
        book.UserId = userId;
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();


    }
    public async Task AddBookFromGoogleAsync(CreateBookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
   
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(UpdateBookDto dto,string userId)
    {
        var book = await _context.Books.FindAsync(dto.Id);
        if (book == null) return false;

        if (book.UserId!=userId)
        {
            throw new UnauthorizedAccessException("Bu kitabı güncelleyemezsiniz");
        }

        book.Title= dto.Title;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id,string userId)
    {
var book= await _context.Books.FindAsync(id);
        if (book is null) return false;

        if (book.UserId!=userId)
        {
            throw new UnauthorizedAccessException("Bu kitabı silemezsiniz");
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return true;
    }
}
