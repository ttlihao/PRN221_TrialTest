using Candidate_BussinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repositories
{
    public interface IJobPostingRepo
    {
        public JobPosting GetJobPosting(string id);
        public Task<IEnumerable<JobPosting>> GetJobPostings();
        public bool CreateJobPosting(JobPosting profile);
        public bool DeleteJobPosting(string profileid);
        public bool UpdateJobPosting(JobPosting profile);
    }
}
