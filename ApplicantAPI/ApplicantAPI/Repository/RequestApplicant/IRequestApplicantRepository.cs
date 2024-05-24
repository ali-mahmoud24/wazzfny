using ApplicantAPI.Dtos.RequestApplicant;
using ApplicantAPI.Models;

namespace ApplicantAPI.Repository;

public interface IRequestApplicantRepository
{
    Task<Response<IEnumerable<GetRequestApplicantDto>>> GetAdminRequestApplicants();

    Task<Response<IEnumerable<GetRequestApplicantDto>>> GetAllRequestApplicants();


    Task<Response<IEnumerable<GetRequestApplicantDto>>> GetRequestApplicantsByOwnerId(int userId);

    Task<Response<GetRequestApplicantDto>> AddRequestApplicant(AddRequestApplicantDto addRequestApplicantDto);
    Task<Response<GetRequestApplicantDto>> EditRequestApplicant(EditRequestApplicantDto editRequestApplicantDto);

    Task<Response<bool>> EditIsEndRequestApplicant(EditIsEndRequestApplicantDto editIsEndRequestApplicantDto);

    Task<Response<bool>> DeleteRequestApplicantById(int requestId);

}
