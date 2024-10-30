using Candidate_BussinessObjects.Models;
using Candidate_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Services
{
    public class HRAccountService : IHRAccountService
    {

        private IHRAccountRepo HRAccountRepo; 
        public HRAccountService() { 
        HRAccountRepo = new HRAccountRepo();    
        }   
        public Hraccount GetHraccountByEmail(string email) => HRAccountRepo.GetHraccountByEmail(email);

        public List<Hraccount> GetHraccounts() => HRAccountRepo.GetHraccounts();
    }
}
