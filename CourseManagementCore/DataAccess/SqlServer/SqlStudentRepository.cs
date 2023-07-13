using CourseManagementCore.Domain.Abstraction.Interfaces;
using CourseManagementCore.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace CourseManagementCore.DataAccess.SqlServer
{
    public class SqlStudentRepository : IStudentRepository
    {
        string _connectionString;
        public SqlStudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Student student)
        {
            throw new NotImplementedException();
        }

        public bool Check(int id)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Student Get(int id)
        {
            Student student = new Student();

            using var connection = new SqlConnection(_connectionString);

            connection.Open();

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select* from Students";

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                student.Add(new Student()
                {
                    Id = Convert.ToInt16(reader["Id"]),
                    Surname = reader["Surname"].ToString(),
                    Name = reader["Name"].ToString(),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                    IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
                    Email = Convert.ToString(reader["Email"]),
                    PhoneNumber = Convert.ToString(reader["PhoneNumber"])
                });

            }
            connection.Close();
            return student;

        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using var connection = new SqlConnection(_connectionString);

            connection.Open();

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select* from Students";

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new Student()
                {
                    Id = Convert.ToInt16(reader["Id"]),
                    Surname = reader["Surname"].ToString(),
                    Name = reader["Name"].ToString(),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                    IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
                    Email = Convert.ToString(reader["Email"]),
                    PhoneNumber = Convert.ToString(reader["PhoneNumber"])
                });

            }
            connection.Close();
            return students;

        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
