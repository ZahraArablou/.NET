using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Student
    {
        public int SId { get; set; }
        public string SName { get; set; }
        public string Enroll { get; set; }
        public string Dep { get; set; }
        public string Sem { get; set; }
        public string Contact { get; set; }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if(!regex.IsMatch(value))
                {
                    throw new InvalidValueException("Email format is not correct");
                }
                _email = value;   
            }
        }

        public Student(int sId, string sName, string enroll, string dep, string sem, string contact, string email)
        {
            this.SId = sId;
            SName = sName;
            Enroll = enroll;
            Dep = dep;
            Sem = sem;
            Contact = contact;
            Email = email;
        }
        public Student(SqlDataReader reader)
        {
            SId = reader.GetInt32(reader.GetOrdinal("SId"));
            SName = reader.GetString(reader.GetOrdinal("SName"));
            Enroll = reader.GetString(reader.GetOrdinal("Enroll"));
            Dep = reader.GetString(reader.GetOrdinal("Dep"));
            Sem = reader.GetString(reader.GetOrdinal("Sem"));
            Contact = reader.GetString(reader.GetOrdinal("Contact"));
            Email = reader.GetString(reader.GetOrdinal("Email"));
        }
    }
}
