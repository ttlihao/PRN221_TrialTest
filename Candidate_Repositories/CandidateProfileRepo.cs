using Candidate_BussinessObjects.Models;
using Candidate_DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repositories
{
    public class CandidateProfileRepo : ICandidateProfileRepo
    {
        public  async Task<List<CandidateProfile>> SearchCandidatesAsync(string fullname, DateTime? birthday) => await CandidateProfileDAO.Instance.SearchCandidatesAsync(fullname, birthday);
        public async Task<PaginatedList<CandidateProfile>> GetCandidatesAsync(int pageIndex, int pageSize) => await CandidateProfileDAO.Instance.GetCandidatesAsync(pageIndex, pageSize);
        public async Task<bool> AddCandidateProfile(CandidateProfile profile) => await CandidateProfileDAO.Instance.AddCandidateProfile(profile);

        public CandidateProfile GetCandidateProfile(string id) => CandidateProfileDAO.Instance.GetCandidateProfileById(id);

        public List<CandidateProfile> GetCandidateProfiles() => CandidateProfileDAO.Instance.GetCandidateProfiles();

        public bool RemoveCandidateProfile(string profileid) => CandidateProfileDAO.Instance.DeleteCandidateProfile(profileid);
        public bool UpdateCandidateProfile(CandidateProfile profile) => CandidateProfileDAO.Instance.UpdateCandidateProfile(profile);
    }
}
