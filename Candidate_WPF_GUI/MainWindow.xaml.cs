using Candidate_BussinessObjects.Models;
using Candidate_Services;
using System.Security.Principal;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Candidate_WPF_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHRAccountService hRAccountService;
        public MainWindow()
        {
            
            InitializeComponent();
            hRAccountService = new HRAccountService();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Hraccount hrAccount = hRAccountService.GetHraccountByEmail(txtEmail.Text);
            if (hrAccount != null && txtPassword.Password.Equals(hrAccount.Password) && hrAccount.MemberRole == 1) {
                CandidateProfileWindow profileWindow = new CandidateProfileWindow();
                profileWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Bye Bye");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}