﻿namespace AI_Social_Platform.Data
{
    using Models;
    using Models.Publication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class ASPDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ASPDbContext(DbContextOptions<ASPDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<State> States { get; set; } = null!;
        public DbSet<School> Schools { get; set; } = null!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<Publication> Publications { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Media> MediaFiles { get; set; } = null!;
        public DbSet<UserSchool> UserSchools { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(ASPDbContext)) ??
                                      Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}
