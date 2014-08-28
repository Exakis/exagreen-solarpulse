using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exagreen.SolarPulse.Core.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace Exagreen.SolarPulse.Core.Services
{
    public class EntryService : MobileBaseService<SolarPulseEntry>, IEntryService
    {
        public EntryService(IMobileServiceClient mobileServiceClient)
            : base(mobileServiceClient)
        {
        }

        //public Task<List<SolarPulseEntry>> GetLast3MonthAsync()
        //{

        //}

        public async Task<SolarPulseEntry> GetLastEntryAsync()
        {
            var result = await base.GetTable().OrderByDescending(d=>d.Timestamp).Take(1).ToListAsync();
            return result.First();
        }
    }
}