using PcBuilderBackend.Application.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace PcBuilderBackend.Infrastructure.Services
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => BC.HashPassword(password, workFactor: 12);
        public bool Verify(string password, string hash) => BC.Verify(password, hash);
    }
}
