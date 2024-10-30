using Candidate_BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_DAO
{
    public class CandidateProfileDAO
    {
        private CandidateManagementContext _context;
        private static CandidateProfileDAO instance;

        public CandidateProfileDAO()
        {
            _context = new CandidateManagementContext();
        }
        public static CandidateProfileDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CandidateProfileDAO();
                }
                return instance;
            }
        }
        public Task<PaginatedList<CandidateProfile>> GetCandidatesAsync(int pageIndex, int pageSize)
        {
            var query = _context.CandidateProfiles.Include(x => x.Posting).AsNoTracking(); 
            return PaginatedList<CandidateProfile>.CreateAsync(query, pageIndex, pageSize);
        }

        public async Task<List<CandidateProfile>> SearchCandidatesAsync(string fullname, DateTime? birthday)
        {
            var query = _context.CandidateProfiles.AsQueryable();

            if (!string.IsNullOrEmpty(fullname))
            {
                query = query.Where(c => c.Fullname.Contains(fullname));
            }

            if (birthday.HasValue)
            {
                query = query.Where(c => c.Birthday == birthday.Value);
            }

            return await query.ToListAsync();
        }


        public CandidateProfile GetCandidateProfileById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _context.CandidateProfiles.Include(x => x.Posting).FirstOrDefault(c => c.CandidateId == id);
            }
            return null;
        }
        public List<CandidateProfile> GetCandidateProfiles()
        {
            return _context.CandidateProfiles.Include(x => x.Posting).ToList();
        }
        public async Task<bool> AddCandidateProfile(CandidateProfile candidateProfile)
        {
            _context.CandidateProfiles.Add(candidateProfile);
            var changes =  await _context.SaveChangesAsync();
            return changes > 0;
        }


        public bool DeleteCandidateProfile(string id)
        {
            bool isSuccess = false;
            try
            {
                if (id != null)
                {

                    _context.CandidateProfiles.Remove(_context.CandidateProfiles.First(c => c.CandidateId == id));
                    _context.SaveChanges();
                    isSuccess = true;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return isSuccess;
        }
        public bool UpdateCandidateProfile(CandidateProfile profile)
        {
            bool isSuccess = false;
            var existingEntity = _context.CandidateProfiles.Local.FirstOrDefault(e => e.CandidateId == profile.CandidateId);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            _context.CandidateProfiles.Attach(profile);
            try
            {
                if (profile != null)
                {
                    _context.Attach<CandidateProfile>(profile).State
                        = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return isSuccess;
    }
    }
}
