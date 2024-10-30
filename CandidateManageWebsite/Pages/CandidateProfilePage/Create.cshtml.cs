using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Candidate_BussinessObjects.Models;
using Candidate_Services;

namespace CandidateManageWebsite.Pages.CandidateProfilePage
{
    public class CreateModel : PageModel
    {
        private readonly IJobPostingService jobPostingService;
        private readonly ICandidateProfileService candidateProfileService;

        public CreateModel(IJobPostingService _jobPostingService, ICandidateProfileService _candidateProfileService)
        {
            jobPostingService = _jobPostingService; 
            candidateProfileService = _candidateProfileService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
        ViewData["PostingId"] = new SelectList(await jobPostingService.GetJobPostings(), "PostingId", "PostingId");
            return Page();
        }

        [BindProperty]
        public CandidateProfile CandidateProfile { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || candidateProfileService.GetCandidateProfiles() == null || CandidateProfile == null)
            {
                return Page();
            }
          candidateProfileService.AddCandidateProfile(CandidateProfile);

            return RedirectToPage("./Index");
        }
    }
}
