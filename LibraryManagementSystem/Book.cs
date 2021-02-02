using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Book
    {
       public int bId { get; set; }
        public string bName { get; set; }
        public string bAuthor { get; set; }
        public string bPubl { get; set; }
        public DateTime bPDate { get; set; }
        public int bPrice { get; set; }
        public int bQuan { get; set; }

        public Book(int bId, string bName, string bAuthor, string bPubl, DateTime bPDate, int bPrice, int bQuan)
        {
            this.bId = bId;
            this.bName = bName;
            this.bAuthor = bAuthor;
            this.bPubl = bPubl;
            this.bPDate = bPDate;
            this.bPrice = bPrice;
            this.bQuan = bQuan;
        }
        public Book(SqlDataReader reader)
        {
            bId = reader.GetInt32(reader.GetOrdinal("bId"));
            bName = reader.GetString(reader.GetOrdinal("bName"));
            bAuthor = reader.GetString(reader.GetOrdinal("bAuthor"));
            bPubl = reader.GetString(reader.GetOrdinal("bPubl"));
            bPDate = reader.GetDateTime(reader.GetOrdinal("bPDate"));
            bPrice= reader.GetInt32(reader.GetOrdinal("bPrice"));
            bQuan = reader.GetInt32(reader.GetOrdinal("bQuan"));
        }
    }
    public class BookIssued
    {
        public int bId { get; set; }
        public string bName { get; set; }
        public string bAuthor { get; set; }
        public string bPubl { get; set; }
        public DateTime IssueDate {get;set;}

        public BookIssued(int bId, string bName, string bAuthor, string bPubl, DateTime issueDate)
        {
            this.bId = bId;
            this.bName = bName;
            this.bAuthor = bAuthor;
            this.bPubl = bPubl;
            IssueDate = issueDate;
        }
  }
    public class BookDetails
    {
        public string SName { get; set; }
        public string SEnroll { get; set; }
        public string BName { get; set; }
        public string BAuthor { get; set; }
        public DateTime IssueDate { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }

        public BookDetails(string sName, string sEnroll, string bName, string bAuthor, DateTime issueDate, Nullable<DateTime> returnDate)
        {
            SName = sName;
            SEnroll = sEnroll;
            BName = bName;
            BAuthor = bAuthor;
            IssueDate = issueDate;
            ReturnDate = returnDate;
        }
    }
}
