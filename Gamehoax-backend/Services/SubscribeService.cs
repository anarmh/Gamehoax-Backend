﻿using Gamehoax_backend.Data;
using Gamehoax_backend.Models;
using Gamehoax_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamehoax_backend.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly AppDbContext _context;
        public SubscribeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subscribe>> GetAllAsync()
        {
            return await _context.Subscribes.ToListAsync();
        }
    }
}
