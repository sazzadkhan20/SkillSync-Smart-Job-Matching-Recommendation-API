using BLL.DomainLogic.Calculators;
using BLL.DTOs;
using BLL.Helpers;
using Common;
using DAL;
using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JobApplicationService
    {
        private readonly DataAccessFactory _dataAccess;
        private readonly ISkillMatchCalculator _skillMatch;
        private readonly IEmailService _emailService;
        public JobApplicationService(DataAccessFactory dataAccess,ISkillMatchCalculator skillMatch,IEmailService emailService)
        {
            _dataAccess = dataAccess;
            _skillMatch = skillMatch;
            _emailService = emailService;
        }
        public async Task<JobApplicationDetailsDTO> GetAsync(int id)
        {
            var data = await _dataAccess.JobApplicationData().GetAsync(id);
            var result = MapperConfig.GetMapper().Map<JobApplicationDetailsDTO>(data);
            return result;
        }

        public async Task<List<JobApplicationDetailsDTO>> GetAllAsync()
        {
            var data = await _dataAccess.JobApplicationData().GetAllAsync();
            var result = MapperConfig.GetMapper().Map<List<JobApplicationDetailsDTO>>(data);
            return result;
        }

        public async Task<bool> AddAsync(JobApplicationDTO application)
        {
            var candidateData = await _dataAccess.CandidateData().GetAsync(application.CandidateId);
            var jobData = await _dataAccess.JobPostData().GetAsync(application.JobId);
            if (candidateData == null)
            {
                throw new Exception("Invalid Candidate id");
            }
            if (jobData == null)
            {
                throw new Exception("Invalid Job id");
            }
            var checkExistingApplication = await _dataAccess.JobApplicationFeature().GetByCandiadteAndJobId(application.CandidateId, application.JobId);
            if (checkExistingApplication != null)
                throw new Exception("Application already exists for this candidate and job");
            int skillMatchPErcentage = _skillMatch.Calculate(candidateData, jobData);
            var data = MapperConfig.GetMapper().Map<JobApplication>(application);
            data.SkillMatchPercent = skillMatchPErcentage;
            var result = await _dataAccess.JobApplicationData().AddAsync(data);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _dataAccess.JobApplicationData().GetAsync(id);
            if (data == null)
            {
                throw new Exception("Job Application Not Found");
            }
            var result = await _dataAccess.JobApplicationData().DeleteAsync(data);
            return result;
        }

        public async Task<bool> UpdateAsync(JobApplicationDTO application, int id)
        {
            var existingData = await _dataAccess.JobApplicationData().GetAsync(id);
            if (existingData == null)
            {
                throw new Exception("Job Application Not Found");
            }
            var data = MapperConfig.GetMapper().Map<JobApplication>(application);
            var result = await _dataAccess.JobApplicationData().UpdateAsync(data);
            return result;
        }

        // Notify Selected Candidates
        public async Task<int> NotifyCandidatesAsync(int jobId, CandidateNotifyRequestDTO notify)
        {
            // Fetch filtered candidates
            var applicants = await _dataAccess.JobApplicationFeature()
                .GetSelectedCandidate(jobId, notify.MinimumSkillMatch, notify.DecisionType);

            if (applicants == null || applicants.Count == 0)
            {
                throw new Exception("No candidates found matching the criteria");
            }

            int emailCount = 0;

            foreach (var applicant in applicants)
            {
                // Prepare email subject
                string subject = notify.DecisionType.Equals("Hired")
                    ? "🎉 Congratulations! You are hired at SkillSync!"
                    : "📩 Interview Invitation from SkillSync";

                string body = notify.DecisionType.Equals("Interview")
                    ? $@"
                <h2 style='color:green;'>Congratulations {applicant.FullName}!</h2>
                <p>You have been <strong>selected</strong> for the position <em>{applicant.CandidateType}</em> at <strong>SkillSync</strong>.</p>
                <p>We are excited to have you join our team. Our HR team will contact you soon for next steps.</p>
                <p>Best regards,<br/><strong>SkillSync Team</strong></p>"
                    : $@"
                <h2 style='color:blue;'>Hello {applicant.FullName},</h2>
                <p>You have been <strong>shortlisted for an interview</strong> for the position <em>{applicant.CandidateType}</em> at <strong>SkillSync</strong>.</p>
                <p>Please check your email for the interview schedule or contact our HR team for details.</p>
                <p>Best regards,<br/><strong>SkillSync Team</strong></p>";

                await _emailService.SendBulkEmailAsync(applicant.Email, subject, body);

                emailCount++;
            }

            return emailCount;
        }


    }
}
