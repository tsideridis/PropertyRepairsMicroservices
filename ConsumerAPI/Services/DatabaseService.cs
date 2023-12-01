using ConsumerAPI.Context;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace ConsumerAPI.Services
{
    public class DatabaseService
    {
        private readonly RepairRequestsContext _context;

        public DatabaseService(RepairRequestsContext context)
        {
            _context = context;
        }

        public async Task SaveRepairRequest(RepairRequest request)
        {
            try
            {
                _context.RepairRequests.Add(request);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw; 
            }
        }

        public async Task<IEnumerable<RepairRequest>> GetProcessedRequestsAsync()
        {
            return await _context.RepairRequests.ToListAsync();
        }
    }
}
