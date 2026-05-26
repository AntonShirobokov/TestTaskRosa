using Microsoft.EntityFrameworkCore;
using TestTaskRosa.Data;
using TestTaskRosa.Models;

namespace TestTaskRosa.Services
{
    public class RequestService
    {
        private readonly AppDbContext _db;

        public RequestService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Request> Create(Request request)
        {
            var cutoff = DateTime.UtcNow.AddMinutes(-5);
            var isDuplicate = await _db.Requests.AnyAsync(r => 
                r.EmployeeName == request.EmployeeName && 
                r.CertificateType == request.CertificateType && 
                r.Reason == request.Reason &&
                r.CreatedAt > cutoff);

            if (isDuplicate)
                throw new InvalidOperationException("Подобный запрос уже был отправлен недавно. Пожалуйста, подождите.");

            request.Id = Guid.NewGuid();
            request.Status = RequestStatus.Created;
            request.CreatedAt = DateTime.UtcNow;

            _db.Requests.Add(request);
            await _db.SaveChangesAsync();
            return request;
        }

        public async Task<List<Request>> GetAll()
        {
            return await _db.Requests.OrderByDescending(r => r.CreatedAt).ToListAsync();
        }

        public async Task<List<Request>> GetByEmployee(string employeeName)
        {
            return await _db.Requests
                            .Where(r => r.EmployeeName.ToLower() == employeeName.ToLower())
                            .OrderByDescending(r => r.CreatedAt)
                            .ToListAsync();
        }

        public async Task<Request?> GetById(Guid id)
        {
            return await _db.Requests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateStatus(Guid id, RequestStatus status)
        {
            var request = await GetById(id);

            if (request == null)
                throw new Exception("Request not found");

            request.Status = status;
            await _db.SaveChangesAsync();
        }
    }
}
