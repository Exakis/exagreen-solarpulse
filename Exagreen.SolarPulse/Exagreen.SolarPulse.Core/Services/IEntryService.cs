using System.Threading.Tasks;
using Exagreen.SolarPulse.Core.Models;

namespace Exagreen.SolarPulse.Core.Services
{
    public interface IEntryService
    {
        Task<SolarPulseEntry> GetLastEntryAsync();
    }
}