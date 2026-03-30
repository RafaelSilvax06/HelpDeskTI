using Microsoft.EntityFrameworkCore;
using HelpDeskTI.Models;

namespace HelpDeskTI.Data
{
	public AppDbContext : DbContext()
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
    }

	public DbSet<Usuario> Usuario { get; set; }
}
}
