
using IDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace IDP.Infra.Context
{
    public class ShopDBContext : DbContext
    {
        public ShopDBContext(DbContextOptions<ShopDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
