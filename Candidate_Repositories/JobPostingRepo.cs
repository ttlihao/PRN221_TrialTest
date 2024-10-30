using Candidate_BussinessObjects.Models;
using Candidate_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repositories
{
    public class JobPostingRepo : IJobPostingRepo
    {
        public bool CreateJobPosting(JobPosting profile) => JobPostingDAO.Instance.CreateJobPosting(profile);

        public JobPosting GetJobPosting(string id) => JobPostingDAO.Instance.GetJobPostingById(id);

        public async Task<IEnumerable<JobPosting>> GetJobPostings() => await JobPostingDAO.Instance.GetJobPostings();

        public bool DeleteJobPosting(string profileid) => JobPostingDAO.Instance.DeleteJobPosting(profileid);
        public bool UpdateJobPosting(JobPosting jobPosting) => JobPostingDAO.Instance.UpdateJobPosting(jobPosting);
    }
}
