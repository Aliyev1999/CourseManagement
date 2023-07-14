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
            using var connection = new SqlConnection(_connectionString);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"INSERT INTO Students
                                   VALUES (@Name, @Surname, @Email, @PhoneNumber, @BirthDate, @IsDeleted);";


            //command.Parameters.AddRange(new[]SqlParameter {
            //    new SqlParameter("Name",student.Name),
            //    new SqlParameter("Surname", student.Surname),
            //    new SqlParameter("Email", student.Email),
            //    new SqlParameter("PhoneNumber", student.PhoneNumber),
            //    new SqlParameter("BirthDate", student.BirthDate),
            //    new SqlParameter("IsDeleted", student.IsDeleted)
            //  });

        }

        public bool Check(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select count(*) c from Students";

            int count = (int)command.ExecuteScalar();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }

            return count > 0;
        }

        public int Count()
        {
            using var connection = new SqlConnection(_connectionString);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select count(*) c from Students";

            int count = (int)command.ExecuteScalar();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }

            return count;
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "delete from Students where Id = @Id";
            command.Parameters.Add(new SqlParameter("Id", id));

            command.ExecuteNonQuery();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public Student Get(int id)
        {
            Student student = new Student();

            using var connection = new SqlConnection(_connectionString);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select* from Students where Id = @Id";

            command.Parameters.Add(new SqlParameter("Id", id));

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                student = new Student()
                {
                    Id = Convert.ToInt16(reader["Id"]),
                    Surname = reader["Surname"].ToString(),
                    Name = reader["Name"].ToString(),
                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                    IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
                    Email = Convert.ToString(reader["Email"]),
                    PhoneNumber = Convert.ToString(reader["PhoneNumber"])
                };

            }
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
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
