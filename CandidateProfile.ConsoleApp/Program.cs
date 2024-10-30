// Seeusing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Candidate_Services
{
    class Program
    {
        private ICandidateProfileService profileService;
        private IHRAccountService hraccountService;
        private IJobPostingService jobPostingService;
        public Program(ICandidateProfileService _profileService, IHRAccountService hraccountService, IJobPostingService jobPostingService)
        {
            profileService = _profileService;
            this.hraccountService = hraccountService;
            this.jobPostingService = jobPostingService; 
        }
        static void Main(string[] args)
        {
            ICandidateProfileService profileService = new CandidateProfileService();
            IJobPostingService jobPostingService = new JobPostingService();
            IHRAccountService hRAccountService = new HRAccountService();    
            Program program = new Program(profileService, hRAccountService, jobPostingService);

            var CanList = program.profileService.GetCandidateProfiles();
            var option = new JsonSerializerOptions { WriteIndented = true };
            string CanadidateProfileData = JsonSerializer.Serialize(CanList, option);
            string filePath = Path.Combine("C:\\Users\\thien.phan\\source\\repos\\PRN221PE_FA22_TrialTest_PhanTranBaoThien\\CandidateProfile.JsonWPF", "candidates.json");
            File.WriteAllText(filePath, jsonData);
            Console.WriteLine($"JSON data has been written to {filePath}");
        }
    }
}
