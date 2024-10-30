using Candidate_BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_DAO
{
    public class HRAccountDAO
    {
        private CandidateManagementContext context;
        private static HRAccountDAO instance;

        public HRAccountDAO()
        {
            context = new CandidateManagementContext();
        }

        public static HRAccountDAO Instance { get
            {
                if (instance == null) { 
                instance = new HRAccountDAO();
                }
                return instance;
            }
        }
        public Hraccount GetHraccountByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return context.Hraccounts.FirstOrDefault(c => c.Email == email);
            }
            return null; 
        }
        public List<Hraccount> GetHraccounts()
        {
            return context.Hraccounts.ToList();

        } 
    }
}
