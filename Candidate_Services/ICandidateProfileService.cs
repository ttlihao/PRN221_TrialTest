using Candidate_BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Services
{
    public interface ICandidateProfileService
    {
        public Task<PaginatedList<CandidateProfile>> ListCandidatesAsync(int pageIndex, int pageSize);
        public CandidateProfile GetCandidateProfile(string id);
        public List<CandidateProfile> GetCandidateProfiles();
        public Task<bool> AddCandidateProfile(CandidateProfile profile);
        public bool RemoveCandidateProfile(string profileid);
        public bool UpdateCandidateProfile(CandidateProfile profile);
        Task<List<CandidateProfile>> SearchCandidatesAsync(string fullname, DateTime? birthday);
    }
}
