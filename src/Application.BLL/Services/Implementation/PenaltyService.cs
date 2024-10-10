using Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    public class PenaltyService : IPenaltyService
    {
        public Task<IEnumerable<ReadPenaltyDto>> AllPenalties()
        {
            throw new NotImplementedException();
        }

        public Task DeletePenalty(Guid PenaltyId)
        {
            throw new NotImplementedException();
        }

        public Task<ReadPenaltyDto> GetLByPenaltiesByMember(Guid MemberId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPenaltyPaid(Guid PenaltyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePenalty(UpdatePenaltyDto updatePenaltyto)
        {
            throw new NotImplementedException();
        }
    }
}
