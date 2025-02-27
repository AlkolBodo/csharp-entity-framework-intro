﻿using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class AuthorRepo : IAuthorRepo
    {
        DataContext _db;

        public AuthorRepo(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _db.Authors
                .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .ThenInclude(b => b.Publisher)
                .OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            var entity = await _db.Authors
                .Include (a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .ThenInclude(b => b.Publisher)
                .FirstAsync(a => a.Id == id);
            return entity;
        }

    }
}
