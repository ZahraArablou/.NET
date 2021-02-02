using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
    class Database
    {
        const string ConnectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\heshmat\Documents\dotnet\LibraryManagementSystem\LibraryDB.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn;
        public Database()
        {
            conn = new SqlConnection(ConnectString);
            conn.Open();
        }
        public int loginCheck( String user,string pass)
        {
            int i = 0;
            using(SqlCommand cmd=new SqlCommand("select count(*) from login where username=@U AND password=@P", conn))
            {
                cmd.Parameters.AddWithValue("@U", user);
                cmd.Parameters.AddWithValue("@P", pass);

                i = (int)cmd.ExecuteScalar();
            }
          
          
            return i;
        }// end Login check
       public bool CheckEmailForSignUp(Login l)
        {
            int i = 0;
            
            using (SqlCommand cmd = new SqlCommand("select count(*) from Login where Email=@Email", conn))
            {
                cmd.Parameters.AddWithValue("@Email",l.Email);
                i = (int)cmd.ExecuteScalar();
                if (i > 0)
                    return true; 
            }
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Login (UserName,Password, Email) VALUES(@user,@pass,@email)", conn))
            {
               
                cmd.Parameters.AddWithValue("@user",l.UserName);
                cmd.Parameters.AddWithValue("@pass",l.Password);
                cmd.Parameters.AddWithValue("@email",l.Email);
              
                cmd.ExecuteNonQuery();
            }


            return false;
        }

        public void AddBook(Book book)
        {

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Book (bName,bAuthor, bPubl, bPDate, bPrice, bQuan) VALUES(@bName,@bAuthor, @bPubl, @bPDate, @bPrice, @bQuan)", conn))
            {
                string name = book.bName;
                string author = book.bAuthor;
                string publ = book.bPubl;
                DateTime date = book.bPDate;
                int price = book.bPrice;
                int quan = book.bQuan;
                cmd.Parameters.AddWithValue("@bName", name);
                cmd.Parameters.AddWithValue("@bAuthor", author);
                cmd.Parameters.AddWithValue("@bPubl", publ);
                cmd.Parameters.AddWithValue("@bPDate", date);
                cmd.Parameters.AddWithValue("@bPrice", price);
                cmd.Parameters.AddWithValue("@bQuan", quan);
                cmd.ExecuteNonQuery();
            }
        }//end Add Book
        public List<Book> GetAllBooks()
        {
            List<Book> bookList = new List<Book>();
            using (SqlCommand cmd = new SqlCommand("Select * FROM Book", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    bookList.Add(new Book(reader));
                }

            }

            return bookList;

        }//end get books
        public void DeleteBookFromBorrowBook(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE  FROM BorrowBook WHERE BookId=@bId ", conn))

            {
                cmd.Parameters.AddWithValue("@bId", id);
                cmd.ExecuteNonQuery();//ex
            }
        }//End Delete Book
        public void DeleteBook(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE  FROM Book WHERE bId=@bId ", conn))

            {
                cmd.Parameters.AddWithValue("@bId", id);
                cmd.ExecuteNonQuery();//ex
            }
        }//End Delete Book

        public void UpdateBook(Book book)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Book SET bName=@bName,bAuthor=@bAuthor, bPubl=@bPubl, bPDate=@bPDate, bPrice= @bPrice, bQuan= @bQuan  WHERE bId=@bId ", conn))
            {
                string name = book.bName;
                string author = book.bAuthor;
                string publ = book.bPubl;
                DateTime date = book.bPDate;
                int price = book.bPrice;
                int quan = book.bQuan;
                int id = book.bId;
                cmd.Parameters.AddWithValue("@bId", id);
                cmd.Parameters.AddWithValue("@bName", name);
                cmd.Parameters.AddWithValue("@bAuthor", author);
                cmd.Parameters.AddWithValue("@bPubl", publ);
                cmd.Parameters.AddWithValue("@bPDate", date);
                cmd.Parameters.AddWithValue("@bPrice", price);
                cmd.Parameters.AddWithValue("@bQuan", quan);
                cmd.ExecuteNonQuery();
            }
        }// end Update
        public List<Book> SearchBook(string str)
        {
            List<Book> list = new List<Book>();

            using (SqlCommand cmd = new SqlCommand("Select * FROM Book WHERE bName LIKE '%" + str + "%'", conn))

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    list.Add(new Book(reader));
                }
            }


            return list;
        }

        //***************************Student*********************
        public void AddStudent(Student s)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Student(SName ,Enroll ,Dep ,Sem ,Contact,Email) VALUES(@SName ,@Enroll ,@Dep ,@Sem ,@Contact,@Email)", conn))
            {
                cmd.Parameters.AddWithValue("@SName", s.SName);
                cmd.Parameters.AddWithValue("@Enroll", s.Enroll);
                cmd.Parameters.AddWithValue("@Dep", s.Dep);
                cmd.Parameters.AddWithValue("@Sem", s.Sem);
                cmd.Parameters.AddWithValue("@Contact", s.Contact);
                cmd.Parameters.AddWithValue("@Email", s.Email);
                cmd.ExecuteNonQuery();
            }


        }

        public List<Student> GetAllStudents()
        {
            List<Student> studentList = new List<Student>();
            using (SqlCommand cmd = new SqlCommand("Select * FROM Student", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    studentList.Add(new Student(reader));
                }

            }

            return studentList;

        }//end get books

        public List<Student> SearchStudent(string str)
        {
            List<Student> list = new List<Student>();

            using (SqlCommand cmd = new SqlCommand("Select * FROM Student WHERE Enroll LIKE '%" + str + "%'", conn))

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    list.Add(new Student(reader));
                }
            }


            return list;
        }//end search student
        public void UpdateStudent(Student s)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Student SET SName=@SName,Enroll=@Enroll, Dep=@Dep, Sem=@Sem, Email= @Email, Contact= @Contact  WHERE SId=@SId ", conn))
            {
                int id = s.SId;
                string name = s.SName;
                string enroll = s.Enroll;
                string dep = s.Dep;

                string sem = s.Sem;
                string email = s.Email;//TODO Exceptions
                string contact = s.Contact;//TODO Exceptions
                cmd.Parameters.AddWithValue("@SId", id);
                cmd.Parameters.AddWithValue("@SName", name);
                cmd.Parameters.AddWithValue("@Enroll", enroll);
                cmd.Parameters.AddWithValue("@Dep", dep);
                cmd.Parameters.AddWithValue("@Sem", sem);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Contact", contact);
                cmd.ExecuteNonQuery();
            }
        }// end Update
        public void deleteStudentFromBorrowBook(int id)

        {
            using (SqlCommand cmd = new SqlCommand("DELETE  FROM BorrowBook WHERE StudId=@SId ", conn))

            {
                cmd.Parameters.AddWithValue("@SId", id);
                cmd.ExecuteNonQuery();//ex
            }
        }//End Delete Student

    

        public void DeleteStudent(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE  FROM Student WHERE SId=@SId ", conn))

            {
                cmd.Parameters.AddWithValue("@SId", id);
                cmd.ExecuteNonQuery();//ex
            }
        }//End Delete Student

        public Student GetStudentByEnrollNo(string str)
        {
            //List<Student> list = new List<Student>();
            Student s = null;

            using (SqlCommand cmd = new SqlCommand("Select * FROM Student WHERE Enroll = '" + str + "'", conn))

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    s = new Student(reader);
                }
            }
            return s;
        }//end get student by enroll

        public void AddIssuedBook(int StudId, int bookId, DateTime issueDate)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO BorrowBook(StudId,BookId,IssueDate) VALUES(@StudId ,@BookId ,@IssueDate)", conn))
            {
                cmd.Parameters.AddWithValue("@StudId", StudId);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@IssueDate", issueDate);

                cmd.ExecuteNonQuery();
            }
        }//end of AddIssuedBook
        public int countIssuedBooks(int id)
        {
            int count = 0;
            using (SqlCommand cmd = new SqlCommand("select count(*) FROM BorrowBook  WHERE StudId=@StudId AND ReturnDate is Null", conn))
            {
                cmd.Parameters.AddWithValue("@StudId", id);
                // using (SqlDataReader reader = cmd.ExecuteReader())
                // {

                count = (int)cmd.ExecuteScalar();

                // }
            }
            return count;
        }// end 
        public List<BookIssued> GetBorrowedBook(int sId)
        {
            List<BookIssued> list = new List<BookIssued>();

            using (SqlCommand cmd = new SqlCommand("Select BB.BookId,B.bName , B.bAuthor , B.bPubl , BB.IssueDate FROM BorrowBook as BB INNER JOIN Book as B ON BB.BookId=B.bId WHERE BB.StudId=@id AND BB.ReturnDate is null", conn))
            {
                cmd.Parameters.AddWithValue("@id", sId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("BookId"));
                        string name = reader.GetString(reader.GetOrdinal("bName"));
                        string author = reader.GetString(reader.GetOrdinal("bAuthor"));
                        string publ = reader.GetString(reader.GetOrdinal("bPubl"));
                        DateTime date = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                        BookIssued b = new BookIssued(id, name, author, publ, date);
                        list.Add(b);

                    }
                }
            }
            return list;
        }// end get borrow book
        public void UpdateBorrowBook(DateTime d, int bId)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE BorrowBook SET ReturnDate=@d  WHERE BookId=@bId ", conn))
            {
                cmd.Parameters.AddWithValue("@d", d);
                cmd.Parameters.AddWithValue("bId", bId);
                cmd.ExecuteNonQuery();
            }
        }//end UpdateBorrowBook



        public List<BookDetails> GetAllDetails()
        {
            List<BookDetails> list = new List<BookDetails>();
           
            using (SqlCommand cmd = new SqlCommand("Select S.SName,S.Enroll,B.bName , B.bAuthor , B.bPubl , BB.IssueDate,BB.ReturnDate FROM BorrowBook as BB INNER JOIN Book as B ON BB.BookId=B.bId             INNER JOIN STUDENT AS S ON BB.StudId=S.SId", conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                   
                    while (reader.Read())
                    {
                        Nullable<DateTime> returnDate=null;
                        string sname = reader.GetString(reader.GetOrdinal("SName"));
                        string enrol = reader.GetString(reader.GetOrdinal("Enroll"));
                        string bname = reader.GetString(reader.GetOrdinal("bName"));
                        string author = reader.GetString(reader.GetOrdinal("bAuthor"));
                        string publ = reader.GetString(reader.GetOrdinal("bPubl"));
                        DateTime issueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                        if (reader.IsDBNull(reader.GetOrdinal("ReturnDate")))
                        {
                            returnDate=null;
                        }
                        else
                        {
                             returnDate = reader.GetDateTime(reader.GetOrdinal("ReturnDate"));

                        }
                      
                        list.Add(new BookDetails(sname, enrol, bname, author, issueDate, returnDate));

                    }
                }
            }

                return list;

        }// end GetAllDetails
    }
}