using ApplicantAPI.Dtos.Language;
using ApplicantAPI.Models;

namespace ApplicantAPI.Repository;

public interface ILanguageRepository
{
    Task<Response<IEnumerable<GetLanguageDto>>> GetAllLanguages();
    Task<Response<GetLanguageDto>> AddLanguage(Language newLanguage);

    Task<Response<int>> GetLanguagesCount();
}
