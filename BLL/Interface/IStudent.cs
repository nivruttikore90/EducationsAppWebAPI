using Model;
using Model.Core;

namespace BLL.Interface
{
    public interface IStudent
    {
        int InsertStudentInfo(StudentInfo studentInfo);
        TransactionRequest<int> SaveInfo(Student request);
        List<Student> GetStudentInfo(Student request);
        TransactionRequest<int> UpdateStudentInfo(int id, Student request);
    }
}
