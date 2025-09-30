
using FormulatrixB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FormulatrixB.Database
{
	public class AppDBContext : DbContext
	{

		public AppDBContext(DbContextOptions options) : base(options)
		{
			ChangeTracker.LazyLoadingEnabled = true;
		}

		public required DbSet<Item> Items { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override async Task<int> SaveChangesAsync(bool success, CancellationToken cancellationToken = default)
		{
			return await base.SaveChangesAsync(success, cancellationToken);
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}