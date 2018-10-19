using System;
using System.Collections.Generic;
using System.Linq;
using PowerTeam.Model;
using PowerTeam.DAL.Interface;

namespace PowerTeam.DAL.Reponsitory
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private const int UserId = 1;
        private readonly PTDBContext _context;
        public UserRepository(PTDBContext ptDBContext) : base(ptDBContext)
        {
            _context = ptDBContext;
        }

        private IEnumerable<User> GetUsers()
        {
            return from a in _context.Users select a;
        }

        public User GetUser()
        {
            return GetUsers().FirstOrDefault(a => a.Guid.Equals(UserId));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
