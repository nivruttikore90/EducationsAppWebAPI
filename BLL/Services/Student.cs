using BLL.Interface;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Student : IStudent
    {
        private readonly IConfiguration _configuration;
        DAL.Repositories.Student _student = new DAL.Repositories.Student();
        public Student(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public Task<ActionResult<StudentInfo>> GetStudentInfoById(StudentInfo request)
        //{
        //    return _studService.GetStudentInfoById(request);
        //}
        public List<Model.Student> GetStudentInfo(Model.Student request)
        {
            return _student.GetStudentInfo(request);
        }

        public int InsertStudentInfo(StudentInfo studentInfo)
        {
            return _student.InsertStudentInfo(studentInfo);
        }

        public TransactionRequest<int> SaveInfo(Model.Student request)
        {
            var transactionRequestSuccess = new TransactionRequest<int>();
            int result = _student.SaveInfo(request);
            if (result == 1)
            {
                var rxHeaderSuccess = new RxHeader
                {
                    RxSession = Guid.NewGuid().ToString(),
                    Source = "System",
                    Destination = "API",
                    TransactionIdentifier = Guid.NewGuid().ToString(),
                    TransactionExternalIdentifier = null,
                    TransactionTimestamp = DateTime.UtcNow,
                    TransactionStatus = "Completed",
                    Environment = "Local",
                    Statuses = new List<Status> { }
                };

                transactionRequestSuccess = new TransactionRequest<int>
                {
                    RxHeader = rxHeaderSuccess,
                    RxTransactionBody = 1
                };
            }
            return transactionRequestSuccess;
        }

        public TransactionRequest<int> UpdateStudentInfo(int id, Model.Student request)
        {
            var transactionRequestSuccess = new TransactionRequest<int>();
            int result = _student.UpdateStudentInfo(id, request);
            if (result == 1)
            {
                var rxHeaderSuccess = new RxHeader
                {
                    RxSession = Guid.NewGuid().ToString(),
                    Source = "System",
                    Destination = "API",
                    TransactionIdentifier = Guid.NewGuid().ToString(),
                    TransactionExternalIdentifier = null,
                    TransactionTimestamp = DateTime.UtcNow,
                    TransactionStatus = "Completed",
                    Environment = "Local",
                    Statuses = new List<Status> { }
                };

                transactionRequestSuccess = new TransactionRequest<int>
                {
                    RxHeader = rxHeaderSuccess,
                    RxTransactionBody = 1
                };
            }
            return transactionRequestSuccess;
        }
    }
}
