using Common;
using Model;
using Model.Core;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    public class Student
    {
        //public async Task<TransactionRequest<StudentInfo>> GetStudentInfoById(StudentInfo studentInfo)
        //{
        //    TransactionRequest<StudentInfo> student = new TransactionRequest<StudentInfo>();

        //    string insertQuery = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";
        //    SqlParameter[] param = {
        //        DatabaseHelper.CreateParameter("@iUserId", studentInfo.UserId),
        //        DatabaseHelper.CreateParameter("@iPassword", studentInfo.Password)
        //    };

        //    // Execute the query using ExecuteNonQuery
        //    int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery, param);
        //    return student;
        //}

        public List<Model.Student> GetStudentInfo(Model.Student request)
        {
            var students = new List<Model.Student>();

            try
            {
                string query = "uspGetStudentInfo";
                SqlParameter[] param = { };

                using (SqlDataReader rdr = DatabaseHelper.ExecuteReader(query, param))
                {
                    while (rdr.Read())
                    {
                        var student = new Model.Student
                        {
                            Id = rdr["Id"] as int?,
                            Name = rdr["Name"] as string,
                            Address = rdr["Address"] as string,
                            PhoneNumber = rdr["PhoneNumber"] as string,
                            Email = rdr["Email"] as string
                        };

                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching student info: {ex.Message}");
            }
            return students;
        }

        public int InsertStudentInfo(StudentInfo studentInfo)
        {
            string insertQuery = "INSERT INTO trn_student_old (first_name) VALUES (@iFirstName)";
            SqlParameter[] param = {
                DatabaseHelper.CreateParameter("@iFirstName", studentInfo.FirstName)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery, param);
            return rowsAffected;
        }
        public int SaveInfo(Model.Student student)
        {
            SqlParameter[] param = {
            DatabaseHelper.CreateParameter("@name", student.Name),             // Assuming the Name is a string
            DatabaseHelper.CreateParameter("@address", student.Address ?? (object)DBNull.Value), // Nullable, defaults to DBNull if null
            DatabaseHelper.CreateParameter("@phoneNumber", student.PhoneNumber ?? (object)DBNull.Value),
            DatabaseHelper.CreateParameter("@email", student.Email ?? (object)DBNull.Value)
            };

            int rowsAffected = DatabaseHelper.ExecuteQuery("uspInsertStudent", param, "INSERT", true);  // Assuming ExecuteQuery can handle the query type
            return rowsAffected;
        }
        public int UpdateStudentInfo(int id, Model.Student student)
        {
            SqlParameter[] param = {
            DatabaseHelper.CreateParameter("@id", id),
            DatabaseHelper.CreateParameter("@name", student.Name),
            DatabaseHelper.CreateParameter("@address", student.Address ?? (object)DBNull.Value),
            DatabaseHelper.CreateParameter("@phoneNumber", student.PhoneNumber ?? (object)DBNull.Value),
            DatabaseHelper.CreateParameter("@email", student.Email ?? (object)DBNull.Value)
            };

            int rowsAffected = DatabaseHelper.ExecuteQuery("uspUpdateStudent", param, "UPDATE", true);
            return rowsAffected;
        }
    }
}
