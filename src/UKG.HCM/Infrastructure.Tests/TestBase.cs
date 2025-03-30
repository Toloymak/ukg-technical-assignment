using AutoFixture;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using UKG.HCM.Infrastructure.Contexts;

namespace UKG.HCM.Infrastructure.Tests;

using static Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId;

public abstract class TestBase : IDisposable
{
	protected IFixture Fixture { get; } = new Fixture();
		
	protected AppDbContext DbContext => _context ??= CreateContext();
	private AppDbContext? _context;

	[SetUp]
	public void BaseSetup()
		=> _context = null; // Setup context every time to have clean DB

	private AppDbContext CreateContext()
	{
		var connectionString = new SqlConnectionStringBuilder { DataSource = ":memory:"}.ToString();
		var connection = new SqliteConnection(connectionString);
		connection.Open();

		var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection);
			
		// Disable unsupported transactions for SQLite
		optionsBuilder.ConfigureWarnings(x => x.Ignore(AmbientTransactionWarning));
			
		// Enable for query debugging
		// optionsBuilder.UseLoggerFactory(LoggerFactory);
			
		var context = new AppDbContext(optionsBuilder.Options);
		context.Database.EnsureCreated();
			
		return context;
	}

	public void Dispose()
	{
		_context?.Dispose();
		GC.SuppressFinalize(this);
	}
}