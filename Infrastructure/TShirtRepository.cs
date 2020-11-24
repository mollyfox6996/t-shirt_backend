﻿using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TShirtRepository : ITshirtRepository
    {
        private readonly AppDbContext _context;

        public TShirtRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateTShirtAsync(TShirt shirt)
        {
             await _context.TShirts.AddAsync(shirt);
             _context.SaveChanges();
        }

        public async Task<TShirt> GetTShirtByIdAsync(int id) => await _context.TShirts.Include(p => p.Category).Include(p => p.User).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<TShirt>> GetTShirtListAsync() => await _context.TShirts.Include(p => p.Category).Include(p => p.User).ToListAsync();

        public async Task<IEnumerable<TShirt>> GetTshirtByCurrentUserAsync(string userId) => await _context.TShirts.Where(x => x.UserId == userId).Include(p => p.Category).Include(p => p.User).ToListAsync();

        public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _context.Categories.ToListAsync();

        public async Task<Category> GetCategoryAsync(string name) => await _context.Categories.FirstOrDefaultAsync(p => p.Name == name);

        public async Task<IEnumerable<TShirt>> GetByAuthorAsync(string authorName) => await _context.TShirts.Where(p => p.User.DisplayName == authorName).Include(p => p.Category).Include(p => p.User).ToListAsync();

    }
}