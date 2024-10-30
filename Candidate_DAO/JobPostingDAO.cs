using Candidate_BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_DAO
{
    public class JobPostingDAO
    {
        private CandidateManagementContext _context;
        private static JobPostingDAO instance;

        public JobPostingDAO()
        {
            _context = new CandidateManagementContext();
        }
        public static JobPostingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JobPostingDAO();
                }
                return instance;
            }
        }
        public JobPosting GetJobPostingById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _context.JobPostings.FirstOrDefault(c => c.PostingId == id);
            }
            return null;
        }
        public async Task<IEnumerable<JobPosting>> GetJobPostings()
        {
            return await _context.JobPostings.AsNoTracking().ToListAsync();
        }
        public bool DeleteJobPosting(string id)
        {
            var jobPosting = _context.JobPostings.FirstOrDefault(c => c.PostingId == id);
            if (jobPosting != null)
            {
                _context.JobPostings.Remove(jobPosting);
                _context.SaveChanges(); 
                return true;
            }
            return false;
        }
        public bool CreateJobPosting(JobPosting jobPosting)
        {
            if (jobPosting == null)
            {
                return false;
            }

            _context.JobPostings.Add(jobPosting);
            _context.SaveChanges(); // Save changes to the database

            return true;
        }
        public bool UpdateJobPosting(JobPosting jobPostingDTO)
        {
            if (jobPostingDTO == null || string.IsNullOrEmpty(jobPostingDTO.PostingId))
            {
                return false;
            }

            var jobPosting = _context.JobPostings.FirstOrDefault(c => c.PostingId == jobPostingDTO.PostingId);
            if (jobPosting == null)
            {
                return false;
            }

            jobPosting.JobPostingTitle = jobPostingDTO.JobPostingTitle;
            jobPosting.Description = jobPostingDTO.Description;
            jobPosting.PostedDate = jobPostingDTO.PostedDate;

            _context.JobPostings.Update(jobPosting);
            _context.SaveChanges(); // Save changes to the database

            return true;
        }



    }
}
