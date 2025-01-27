using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;

namespace BLL.Services
{
    public class Institute : IInstitute
    {
        DAL.Repositories.Institute _institute = new DAL.Repositories.Institute();
        public List<Model.Institute> GetAllInstitutes()
        {
            return _institute.GetAllInstitutes();
        }
        public Model.Institute GetInstituteById(int id)
        {
            return _institute.GetInstituteById(id);
        }
        public void AddInstitute(Model.Institute institute)
        {
        }
        public void UpdateInstitute(Model.Institute institute)
        {
        }
        public void DeleteInstitute(int id)
        {
        }
    }
}