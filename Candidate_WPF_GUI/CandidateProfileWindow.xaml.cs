using Candidate_BussinessObjects.Models;
using Candidate_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Candidate_WPF_GUI
{
    /// <summary>
    /// Interaction logic for CandidateProfileWindow.xaml
    /// </summary>
    public partial class CandidateProfileWindow : Window
    {
        private ICandidateProfileService candidateProfileService;
        private IJobPostingService jobPostingService;
        private int currentPage = 1;
        private const int pageSize = 3;
        public CandidateProfileWindow()
        {
            candidateProfileService = new CandidateProfileService();
            jobPostingService = new JobPostingService();
            InitializeComponent();

        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updatedCandidate = new CandidateProfile
                {
                    CandidateId = txtCandidateId.Text,
                    Fullname = txtFullName.Text,
                    Birthday = txtBirthDay.SelectedDate.GetValueOrDefault(),
                    ProfileShortDescription = txtDescription.Text,
                    ProfileUrl = txtImageUrl.Text,
                    PostingId = cmbJobPosting.SelectedValue.ToString()
                };

                bool isSuccess = candidateProfileService.UpdateCandidateProfile(updatedCandidate);

                if (isSuccess)
                {
                    MessageBox.Show("Candidate updated successfully!");
                    await LoadCandidatesAsync(currentPage, pageSize);
                }
            }
            catch (ValidationException ex)
            {
                MessageBox.Show($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void dtgCandidateProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgCandidateProfiles.SelectedItem is CandidateProfile selectedCandidate)
            {
                txtCandidateId.Text = selectedCandidate.CandidateId.ToString();
                txtFullName.Text = selectedCandidate.Fullname;
                txtBirthDay.SelectedDate = selectedCandidate.Birthday;
                txtDescription.Text = selectedCandidate.ProfileShortDescription;
                txtImageUrl.Text = selectedCandidate.ProfileUrl;
                cmbJobPosting.SelectedValue = selectedCandidate.PostingId;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCandidatesAsync(currentPage, pageSize);
            cmbJobPosting.ItemsSource = null;
            cmbJobPosting.ItemsSource = await jobPostingService.GetJobPostings();
            cmbJobPosting.DisplayMemberPath = "PostingId"; // What the user sees
            cmbJobPosting.SelectedValuePath = "PostingId"; // What is used internally
        }
        private async Task LoadCandidatesAsync(int pageIndex, int pageSize)
        {
            var paginatedList = await candidateProfileService.ListCandidatesAsync(pageIndex, pageSize);
            this.dtgCandidateProfiles.ItemsSource = paginatedList;
            btnPrevious.IsEnabled = paginatedList.HasPreviousPage;
            btnNext.IsEnabled = paginatedList.HasNextPage;

        }
        private async void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                await LoadCandidatesAsync(currentPage, pageSize);
            }
        }
        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            await LoadCandidatesAsync(currentPage, pageSize);
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var fullname = txtSearchFullName.Text;
            var birthday = dpSearchBirthday.SelectedDate;

            var searchResults = await candidateProfileService.SearchCandidatesAsync(fullname, birthday);
            dtgCandidateProfiles.ItemsSource = searchResults;
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new CandidateProfile model from the input fields
                var newCandidate = new CandidateProfile
                {
                    CandidateId = txtCandidateId.Text,
                    Fullname = txtFullName.Text,
                    Birthday = txtBirthDay.SelectedDate.GetValueOrDefault(),
                    ProfileShortDescription = txtDescription.Text,
                    ProfileUrl = txtImageUrl.Text,
                    PostingId = cmbJobPosting.SelectedValue.ToString()
                };

                bool isSuccess = await candidateProfileService.AddCandidateProfile(newCandidate);

                if (isSuccess)
                {
                    MessageBox.Show("Candidate added successfully!");
                    await LoadCandidatesAsync(currentPage, pageSize); // Refresh the DataGrid
                }
            }
            catch (ValidationException ex)
            {
                MessageBox.Show($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            {
                if (dtgCandidateProfiles.SelectedItem is CandidateProfile selectedCandidate)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this candidate?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isSuccess = candidateProfileService.RemoveCandidateProfile(selectedCandidate.CandidateId);

                        if (isSuccess)
                        {
                            MessageBox.Show("Candidate deleted successfully!");
                            await LoadCandidatesAsync(currentPage, pageSize);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete candidate.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a candidate to delete.");

                }
            }
        }
    }
}
