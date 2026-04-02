using Microsoft.EntityFrameworkCore;
using HelpDeskTI.Models;

namespace HelpDeskTI.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Usuario> Usuarios { get; set; }
	}
}
