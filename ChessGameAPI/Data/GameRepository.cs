using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessGameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessGameAPI.Data
{
    /// <summary>
    /// Implementation of the IGameRepository interface.
    /// Responsible for game info.
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context">data context to access</param>
        public GameRepository(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates/Adds an entity of type T to the injected data context.
        /// </summary>
        /// <typeparam name="T">a reference type</typeparam>
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        
        /// <summary>
        /// Deletes an entity of type T from the injected data context.
        /// </summary>
        /// <typeparam name="T">a reference type</typeparam>
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        /// <summary>
        /// Saves all changes asynchronously
        /// </summary>
        /// <returns>an asynchronous operation returning a int, the number of objects written to the underlying data context.</returns>
        public async Task<int> SaveAll()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesForUser(int userId)
        {
            return await _context.Games.Include(g => g.BlackUser).Include(g => g.WhiteUser).Where(x => x.BlackUserId == userId || x.WhiteUserId == userId).ToListAsync();
        }

        public async Task<Game> GetGame(int gameId)
        {
            return await _context.Games.Include(g =>g.Pieces).Include(g => g.WhiteUserId).Include(g => g.BlackUserId).FirstOrDefaultAsync(x => x.Id == gameId);
        }

        public async Task<Piece> GetPiece(int pieceId)
        {
            return await _context.Pieces.FirstOrDefaultAsync(x => x.Id == pieceId);
        }


        
    }
}