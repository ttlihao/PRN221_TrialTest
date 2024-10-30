using Candidate_BussinessObjects.Models;
using Candidate_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Services
{
    public class JobPostingService : IJobPostingService
    {
        private IJobPostingRepo JobPostingRepo;
        public JobPostingService()
        {
            JobPostingRepo = new JobPostingRepo();
        }
        public bool CreateJobPosting(JobPosting profile)
        {
            return JobPostingRepo.CreateJobPosting(profile);
        }

        public JobPosting GetJobPosting(string id)
        {
            return JobPostingRepo.GetJobPosting(id);
        }

        public async Task<IEnumerable<JobPosting>> GetJobPostings()
        {
            return await JobPostingRepo.GetJobPostings();
        }

        public bool DeleteJobPosting(string profileid)
        {
            return JobPostingRepo.DeleteJobPosting(profileid);
        }

        public bool UpdateJobPosting(JobPosting profile)
        {
            return JobPostingRepo.UpdateJobPosting(profile);
        }
    }
}
