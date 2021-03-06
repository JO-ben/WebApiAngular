using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;

        }
        
        /**
        *Metody Add i Delete nie potrzebuja byc asynchroniczce poniewaz tak naprawde podczas ich wykonywania NIE WYSYŁAMY
        *zadnego zapytania/ani niczego do bazy danych, jest to po prostu lokalne zapisanie w pamieci dopoki nie zrobimy 
        *SaveAll(), wtedy defacto zapisujemy do bazy i musimy dzialac async 
        */
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        { //chcemy zeby odpowiedz z bazy zwracala tez zdjecie danego uzytkownika dlatego dalismy include
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        { //chcemy zeby odpowiedz z bazy zwracala tez zdjecia glowne uzytkowników dlatego dalismy include
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}