using Microsoft.EntityFrameworkCore;
using AutoMapper;

using ApplicantAPI.Data;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.RequestApplicant;
using ApplicantAPI.Dtos.Language;
using ApplicantAPI.Dtos.ComputerSkill;

namespace ApplicantAPI.Repository;

public class RequestApplicantRepository : IRequestApplicantRepository
{

    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;

    public RequestApplicantRepository(DataContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Response<IEnumerable<GetRequestApplicantDto>>> GetAdminRequestApplicants()
    {

        var response = new Response<IEnumerable<GetRequestApplicantDto>>();

        try
        {

            var loadedRequestApplicant = await _dbContext.RequestApplicants
                                                         .Include(r => r.Job).AsSplitQuery()
                                                         .Include(r => r.Experience).AsSplitQuery()
                                                         .Include(r => r.RequestApplicantLanguages).ThenInclude(ra => ra.Language).AsSplitQuery()
                                                         .Include(r => r.RequestApplicantSkills).ThenInclude(ra => ra.ComputerSkill).AsSplitQuery()
                                                         .ToListAsync();

            if (loadedRequestApplicant == null || !loadedRequestApplicant.Any())
            {
                response.Success = false;
                response.Message = "No Request Applicants Found.";

                return response;
            }


            var data = loadedRequestApplicant
                       .Select(req =>
                       {
                           var dto = _mapper.Map<GetRequestApplicantDto>(req);

                           dto.JobName = req.Job.JobName;
                           dto.JobCategoryId = req.Job.JobCategoryId;


                           dto.ExperienceName = req.Experience.ExperienceName;


                           dto.Languages = req.RequestApplicantLanguages.Select(l => new GetLanguageDto { LanguageId = l.LanguageId, LanguageName = l.Language.LanguageName });
                           dto.ComputerSkills = req.RequestApplicantSkills.Select(s => new GetComputerSkillDto { ComputerSkillId = s.ComputerSkillId, SkillName = s.ComputerSkill.SkillName });

                           return dto;
                       });

            response.Data = data;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<Response<GetRequestApplicantDto>> GetAdminRequestApplicantById(int requestId)
    {

        var response = new Response<GetRequestApplicantDto>();

        try
        {

            var loadedRequestApplicant = await _dbContext.RequestApplicants
                                                         .Where(req => req.RequestId == requestId)
                                                         .Include(r => r.Job).AsSplitQuery()
                                                         .Include(r => r.Experience).AsSplitQuery()
                                                         .Include(r => r.RequestApplicantLanguages).ThenInclude(ra => ra.Language).AsSplitQuery()
                                                         .Include(r => r.RequestApplicantSkills).ThenInclude(ra => ra.ComputerSkill).AsSplitQuery()
                                                         .FirstOrDefaultAsync();

            if (loadedRequestApplicant == null)
            {
                response.Success = false;
                response.Message = "No Request Applicant With Such Id Found.";

                return response;
            }



            var data = _mapper.Map<GetRequestApplicantDto>(loadedRequestApplicant);

            data.JobName = loadedRequestApplicant.Job.JobName;
            data.JobCategoryId = loadedRequestApplicant.Job.JobCategoryId;
            data.JobCategoryName = loadedRequestApplicant.Job.JobCategory.CategoryName;

            data.ExperienceName = loadedRequestApplicant.Experience.ExperienceName;


            data.Languages = loadedRequestApplicant.RequestApplicantLanguages.Select(l => new GetLanguageDto { LanguageId = l.LanguageId, LanguageName = l.Language.LanguageName });
            data.ComputerSkills = loadedRequestApplicant.RequestApplicantSkills.Select(s => new GetComputerSkillDto { ComputerSkillId = s.ComputerSkillId, SkillName = s.ComputerSkill.SkillName });


            response.Data = data;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }


    public async Task<Response<IEnumerable<GetRequestApplicantDto>>> GetAllRequestApplicants()
    {

        var response = new Response<IEnumerable<GetRequestApplicantDto>>();

        try
        {

            var loadedRequestApplicant = await _dbContext.RequestApplicants
                                                         .Where(r => r.IsEnd == false)
                                                         .Include(r => r.Job).AsSplitQuery()
                                                         .Include(r => r.Experience).AsSplitQuery()
                                                         .Include(r => r.RequestApplicantLanguages).ThenInclude(ra => ra.Language).AsSplitQuery()
                                                         .Include(r => r.RequestApplicantSkills).ThenInclude(ra => ra.ComputerSkill).AsSplitQuery()
                                                         .ToListAsync();

            if (loadedRequestApplicant == null || !loadedRequestApplicant.Any())
            {
                response.Success = false;
                response.Message = "No Request Applicants Found.";

                return response;
            }


            var data = loadedRequestApplicant
                       .Select(req =>
                       {
                           var dto = _mapper.Map<GetRequestApplicantDto>(req);

                           dto.JobName = req.Job.JobName;
                           dto.JobCategoryId = req.Job.JobCategoryId;


                           dto.ExperienceName = req.Experience.ExperienceName;


                           dto.Languages = req.RequestApplicantLanguages.Select(l => new GetLanguageDto { LanguageId = l.LanguageId, LanguageName = l.Language.LanguageName });
                           dto.ComputerSkills = req.RequestApplicantSkills.Select(s => new GetComputerSkillDto { ComputerSkillId = s.ComputerSkillId, SkillName = s.ComputerSkill.SkillName });

                           return dto;
                       });

            response.Data = data;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }


    public async Task<Response<IEnumerable<GetRequestApplicantDto>>> GetRequestApplicantsByOwnerId(int userId)
    {

        var response = new Response<IEnumerable<GetRequestApplicantDto>>();

        try
        {

            var loadedRequestApplicant = await _dbContext.RequestApplicants
                                                         .Where(r => r.UserId == userId)
                                                         .Include(r => r.Job).AsSplitQuery()
                                                         .Include(r => r.Experience).AsSplitQuery()
                                                         .Include(r => r.RequestApplicantLanguages).ThenInclude(ra => ra.Language).AsSplitQuery()
                                                         .Include(r => r.RequestApplicantSkills).ThenInclude(ra => ra.ComputerSkill).AsSplitQuery()
                                                         .ToListAsync();

            if (loadedRequestApplicant == null || !loadedRequestApplicant.Any())
            {
                response.Success = false;
                response.Message = "No Request Applicants Found.";

                return response;
            }


            var data = loadedRequestApplicant
                       .Select(req =>
                       {
                           var dto = _mapper.Map<GetRequestApplicantDto>(req);

                           dto.JobName = req.Job.JobName;
                           dto.JobCategoryId = req.Job.JobCategoryId;


                           dto.ExperienceName = req.Experience.ExperienceName;


                           dto.Languages = req.RequestApplicantLanguages.Select(l => new GetLanguageDto { LanguageId = l.LanguageId, LanguageName = l.Language.LanguageName });
                           dto.ComputerSkills = req.RequestApplicantSkills.Select(s => new GetComputerSkillDto { ComputerSkillId = s.ComputerSkillId, SkillName = s.ComputerSkill.SkillName });

                           return dto;
                       });

            response.Data = data;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }




    public async Task<Response<GetRequestApplicantDto>> AddRequestApplicant(AddRequestApplicantDto addRequestApplicantDto)
    {
        var response = new Response<GetRequestApplicantDto>();

        RequestApplicant newRequest = _mapper.Map<RequestApplicant>(addRequestApplicantDto);


        var requestJob = await _dbContext.Jobs.Where(j => j.JobId == addRequestApplicantDto.JobId).SingleOrDefaultAsync();

        var requestExperience = await _dbContext.Experiences.Where(e => e.ExperienceId == addRequestApplicantDto.ExperienceId).SingleOrDefaultAsync();

        if (requestJob == null || requestExperience == null)
        {
            response.Success = false;
            response.Message = "No Such Job or Expereience with this Id.";
            return response;
        }


        // ADD NAVIGATION PROPERTY FOR JOB - EXPERIENCE TO HELP LATER
        newRequest.Job = requestJob;
        newRequest.Experience = requestExperience;


        // Loop through the Computer Skills Ids provided in the AddRequestApplicantDto
        foreach (var computerSkillId in addRequestApplicantDto.ComputerSkillsIds)
        {
            //Retrieve the Computer skill from the repository based on the computerSkillId
            var computerSkill = await _dbContext.ComputerSkills
                                                .Where(c => c.ComputerSkillId == computerSkillId)
                                                .FirstOrDefaultAsync();

            if (computerSkill == null)
            {
                continue;
            }

            // Create a new RequestApplicantSkill instance and associate it with the new RequestApplicant
            var requestApplicantSkill = new RequestApplicantSkill
            {
                ComputerSkill = computerSkill,
                ComputerSkillId = computerSkill.ComputerSkillId,
                Request = newRequest,
            };

            // Add the Skill to the Request
            newRequest.RequestApplicantSkills.Add(requestApplicantSkill);
        }



        // Loop through the Language Ids provided in the AddRequestApplicantDto
        foreach (var languageId in addRequestApplicantDto.LanguagesIds)
        {
            //Retrieve the language from the repository based on the languageId
            var language = await _dbContext.Languages
                                           .Where(l => l.LanguageId == languageId)
                                           .FirstOrDefaultAsync();

            if (language == null)
            {
                continue;
            }

            // Create a new RequestApplicantLanguage instance and associate it with the new RequestApplicant
            var requestApplicantLanguage = new RequestApplicantLanguage
            {
                Language = language,
                LanguageId = language.LanguageId,
                Request = newRequest,
            };

            // Add the Language to the Request
            newRequest.RequestApplicantLanguages.Add(requestApplicantLanguage);
        }

        try
        {
            await _dbContext.RequestApplicants.AddAsync(newRequest);
            await _dbContext.SaveChangesAsync();

            var dto = _mapper.Map<GetRequestApplicantDto>(newRequest);

            dto.JobName = newRequest.Job.JobName;
            dto.JobCategoryId = newRequest.Job.JobCategoryId;

            dto.ExperienceName = newRequest.Experience.ExperienceName;


            dto.Languages = newRequest.RequestApplicantLanguages.Select(l => new GetLanguageDto { LanguageId = l.LanguageId, LanguageName = l.Language.LanguageName });
            dto.ComputerSkills = newRequest.RequestApplicantSkills.Select(s => new GetComputerSkillDto { ComputerSkillId = s.ComputerSkillId, SkillName = s.ComputerSkill.SkillName });


            response.Data = dto;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }


    public async Task<Response<GetRequestApplicantDto>> EditRequestApplicant(EditRequestApplicantDto editRequestApplicantDto)
    {
        var response = new Response<GetRequestApplicantDto>();

        var loadedRequestApplicant = await this.GetRequestApplicantEntityById(editRequestApplicantDto.RequestId);

        if (loadedRequestApplicant == null)
        {
            response.Success = false;
            response.Message = "Couldn't find Request Applicant with such Id to Edit";
            return response;
        }

        loadedRequestApplicant = _mapper.Map<EditRequestApplicantDto, RequestApplicant>(editRequestApplicantDto, loadedRequestApplicant);

        var requestJob = await _dbContext.Jobs.Where(j => j.JobId == editRequestApplicantDto.JobId).SingleOrDefaultAsync();

        var requestExperience = await _dbContext.Experiences.Where(e => e.ExperienceId == editRequestApplicantDto.ExperienceId).SingleOrDefaultAsync();

        if (requestJob == null || requestExperience == null)
        {
            response.Success = false;
            response.Message = "No Such Job or Expereience with this Id.";
            return response;
        }


        // ADD NAVIGATION PROPERTY FOR JOB - EXPERIENCE TO HELP LATER
        loadedRequestApplicant.Job = requestJob;
        loadedRequestApplicant.Experience = requestExperience;


        // CLEAR PREVIOUS SKILLS AND LANGUAGES
        loadedRequestApplicant.RequestApplicantSkills.Clear();
        loadedRequestApplicant.RequestApplicantLanguages.Clear();




        // Loop through the Computer Skills Ids provided in the AddRequestApplicantDto
        foreach (var computerSkillId in editRequestApplicantDto.ComputerSkillsIds)
        {
            //Retrieve the Computer skill from the repository based on the computerSkillId
            var computerSkill = await _dbContext.ComputerSkills
                                                .Where(c => c.ComputerSkillId == computerSkillId)
                                                .FirstOrDefaultAsync();

            if (computerSkill == null)
            {
                continue;
            }

            // Create a new RequestApplicantSkill instance and associate it with the new RequestApplicant
            var requestApplicantSkill = new RequestApplicantSkill
            {
                ComputerSkill = computerSkill,
                ComputerSkillId = computerSkill.ComputerSkillId,
                Request = loadedRequestApplicant,
            };

            // Add the Skill to the Request
            loadedRequestApplicant.RequestApplicantSkills.Add(requestApplicantSkill);
        }



        // Loop through the Language Ids provided in the AddRequestApplicantDto
        foreach (var languageId in editRequestApplicantDto.LanguagesIds)
        {
            //Retrieve the language from the repository based on the languageId
            var language = await _dbContext.Languages
                                           .Where(l => l.LanguageId == languageId)
                                           .FirstOrDefaultAsync();

            if (language == null)
            {
                continue;
            }

            // Create a new RequestApplicantLanguage instance and associate it with the new RequestApplicant
            var requestApplicantLanguage = new RequestApplicantLanguage
            {
                Language = language,
                LanguageId = language.LanguageId,
                Request = loadedRequestApplicant,
            };

            // Add the Language to the Request
            loadedRequestApplicant.RequestApplicantLanguages.Add(requestApplicantLanguage);
        }


        try
        {
            await _dbContext.SaveChangesAsync();


            var dto = _mapper.Map<GetRequestApplicantDto>(loadedRequestApplicant);

            dto.JobName = loadedRequestApplicant.Job.JobName;
            dto.JobCategoryId = loadedRequestApplicant.Job.JobCategoryId;

            dto.ExperienceName = loadedRequestApplicant.Experience.ExperienceName;


            dto.Languages = loadedRequestApplicant.RequestApplicantLanguages.Select(l => new GetLanguageDto { LanguageId = l.LanguageId, LanguageName = l.Language.LanguageName });
            dto.ComputerSkills = loadedRequestApplicant.RequestApplicantSkills.Select(s => new GetComputerSkillDto { ComputerSkillId = s.ComputerSkillId, SkillName = s.ComputerSkill.SkillName });

            response.Data = dto;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }


    public async Task<Response<bool>> EditIsEndRequestApplicant(EditIsEndRequestApplicantDto editIsEndRequestApplicantDto)
    {
        var response = new Response<bool>();

        try
        {
            var loadedRequestApplicant = await this.GetRequestApplicantEntityById(editIsEndRequestApplicantDto.RequestId);

            if (loadedRequestApplicant == null)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "No Request Applicant with this Id";

                return response;
            }

            loadedRequestApplicant.IsEnd = editIsEndRequestApplicantDto.IsEnd;

            await _dbContext.SaveChangesAsync();

            response.Data = true;
            response.Message = "Edited Request Applicant IsEnd Succesfully.";
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;

    }


    public async Task<Response<bool>> DeleteRequestApplicantById(int requestId)
    {
        var response = new Response<bool>();

        var loadedRequestApplicant = await GetRequestApplicantEntityById(requestId);

        if (loadedRequestApplicant == null)
        {
            response.Data = false;
            response.Success = false;
            response.Message = "Couldn't Find Request Applicant With Such Id"; ;
            return response;
        }

        try
        {
            _dbContext.RequestApplicants.Remove(loadedRequestApplicant);

            await _dbContext.SaveChangesAsync();

            response.Data = true;
            response.Message = "Successfully Deleted Request Applicant";
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }


    private async Task<RequestApplicant?> GetRequestApplicantEntityById(int id)
    {
        var loadedRequestApplicant = await _dbContext.RequestApplicants
                                                     .Where(req => req.RequestId == id)
                                                     .Include(req => req.RequestApplicantLanguages)
                                                     .Include(req => req.RequestApplicantSkills)
                                                     .FirstOrDefaultAsync();

        return loadedRequestApplicant;
    }
}