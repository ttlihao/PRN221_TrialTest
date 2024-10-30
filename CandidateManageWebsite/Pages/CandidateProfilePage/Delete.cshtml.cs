using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Candidate_BussinessObjects.Models;
using Candidate_Services;

namespace CandidateManageWebsite.Pages.CandidateProfilePage
{
    public class DeleteModel : PageModel
    {
        private readonly ICandidateProfileService candidateProfileService;

        public DeleteModel(ICandidateProfileService _candidateProfileService)
        {
            candidateProfileService = _candidateProfileService;
        }

        [BindProperty]
      public CandidateProfile CandidateProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || candidateProfileService.GetCandidateProfiles() == null)
            {
                return NotFound();
            }

            var candidateprofile = candidateProfileService.GetCandidateProfile(id);

            if (candidateprofile == null)
            {
                return NotFound();
            }
            else 
            {
                CandidateProfile = candidateprofile;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || candidateProfileService.GetCandidateProfiles() == null)
            {
                return NotFound();
            }
            var candidateprofile = candidateProfileService.GetCandidateProfile(id);

            if (candidateprofile != null)
            {
                candidateProfileService.RemoveCandidateProfile(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
