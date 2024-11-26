using BookStore.Data;
using BookStore.Models;

namespace BookStore.Services
{
    public class GenreService
    {
        private readonly BookstoreContext _context;

        public GenreService(BookstoreContext context)
        {
            _context = context;
        }

        public List<Genre> FindAll()
        {
            return _context.Genres.ToList();
        }

       public void Insert(Genre genre)
        {
            _context.Add(genre);
            _context.SaveChanges();
        }
    }

	
	public async Task<List<Genre>> FindAllAsync()
	{
		return await _context.Genres.ToListAsync();
	}

	// POST: Genres/Create
	public async Task InsertAsync(Genre genre)
	{
		_context.Add(genre);
		await _context.SaveChangesAsync();
	}
}
