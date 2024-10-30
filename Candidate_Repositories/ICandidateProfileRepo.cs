
using Candidate_BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repositories
{
    public interface ICandidateProfileRepo
    {
        public Task<List<CandidateProfile>> SearchCandidatesAsync(string fullname, DateTime? birthday);
        public Task<PaginatedList<CandidateProfile>> GetCandidatesAsync(int pageIndex, int pageSize);
        public CandidateProfile GetCandidateProfile( string id);
        public List<CandidateProfile> GetCandidateProfiles();
        public Task<bool> AddCandidateProfile (CandidateProfile profile);
        public bool RemoveCandidateProfile (string profileid);
        public bool UpdateCandidateProfile(CandidateProfile profile);   
    }
}
