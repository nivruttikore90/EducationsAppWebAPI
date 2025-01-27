using Model;

namespace BLL.Interface
{
    public interface IInstitute
    {
        List<Institute> GetAllInstitutes();
        Institute GetInstituteById(int id);
        void AddInstitute(Institute institute);
        void UpdateInstitute(Institute institute);
        void DeleteInstitute(int id);

    }
}
