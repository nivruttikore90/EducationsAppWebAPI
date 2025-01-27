using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Institute
    {
        public List<Model.Institute> GetAllInstitutes()
        {
            var institutes = new List<Model.Institute>();

            Model.Institute institute = null;
            string query = "SELECT * FROM Institutes";
            SqlParameter[] param = { };
            var reader = DatabaseHelper.ExecuteReader(query, param);
            if (reader.Read())
            {
                while (reader.Read())
                {
                    institutes.Add(new Model.Institute
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Location = reader["Location"].ToString(),
                        Established = (DateTime)reader["Established"]
                    });
                }
            }
            return institutes;
        }

        public Model.Institute GetInstituteById(int id)
        {
            Model.Institute institute = null;
            string query = "SELECT * FROM Institutes WHERE Id = @Id";
            SqlParameter[] param = {
                DatabaseHelper.CreateParameter("@Id", id),
                DatabaseHelper.CreateParameter("@Location", institute.Location),
                DatabaseHelper.CreateParameter("@Established", institute.Established)
            };
            var reader = DatabaseHelper.ExecuteReader(query, param);
            if (reader.Read())
            {
                institute = new Model.Institute
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Location = reader["Location"].ToString(),
                    Established = (DateTime)reader["Established"]
                };
            }
            return institute;
        }

        public void AddInstitute(Model.Institute institute)
        {
            string query = "INSERT INTO Institutes (Name, Location, Established) VALUES (@Name, @Location, @Established)";
            SqlParameter[] param = {
                DatabaseHelper.CreateParameter("@Name", institute.Name),
                DatabaseHelper.CreateParameter("@Location", institute.Location),
                DatabaseHelper.CreateParameter("@Established", institute.Established)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, param);
        }

        public void UpdateInstitute(Model.Institute institute)
        {
            string query = "UPDATE Institutes SET Name = @Name, Location = @Location, Established = @Established WHERE Id = @Id";
            SqlParameter[] param = {
                DatabaseHelper.CreateParameter("@Id", institute.Id),
                DatabaseHelper.CreateParameter("@Name", institute.Name),
                DatabaseHelper.CreateParameter("@Location", institute.Location),
                DatabaseHelper.CreateParameter("@Established", institute.Established)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, param);
        }

        public void DeleteInstitute(int id)
        {
            string query = "DELETE FROM Institutes WHERE Id = @Id";
            SqlParameter[] param = {
                DatabaseHelper.CreateParameter("@Id", id)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, param);
        }
    }
}
