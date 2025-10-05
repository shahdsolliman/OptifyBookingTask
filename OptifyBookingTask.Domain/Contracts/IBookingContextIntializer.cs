using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptifyBookingTask.Domain.Contracts
{
    public interface IBookingContextIntializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
