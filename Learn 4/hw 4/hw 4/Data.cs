using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_4
{
    internal class Data
    {
        private string conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
        
        public DataTable GetStudents()
        {
            DataTable dt = new DataTable();
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "select * from Students";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetTeachers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection db = new SqlConnection(conn))
            { 
                string sql = "select * from Teachers";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetClassrooms()
        {
            DataTable dt = new DataTable();
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "select * from Classrooms";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetSubjects()
        {
            DataTable dt = new DataTable();
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "select * from Subjects";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                adapter.Fill(dt);
            }
            return dt;
        }

        public void UpdateStudent(DataTable modStudent)
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                //db.Open();
                string sql = "select * from Students";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                adapter.Update(modStudent);
            }
        }

        public void UpdateTeacher(DataTable modTeacher)
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                //db.Open();
                string sql = "select * from Teachers";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                adapter.Update(modTeacher);
            }
        }

        public void UpdateClassroom(DataTable modClassrooms)
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                //db.Open();
                string sql = "select * from Classrooms";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                adapter.Update(modClassrooms);
            }
        }

        public void UpdateSubject(DataTable modSubject)
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                //db.Open();
                string sql = "select * from Subjects";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                adapter.Update(modSubject);
            }
        }
    }
}
