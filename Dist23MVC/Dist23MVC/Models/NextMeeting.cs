using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Dist23MVC.Models
{
    public class NextMeeting
    {
        [Key]
        public int pKey { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string type { get; set; }
        public string topic { get; set; }
        public string aaGroup { get; set; }
        public string location { get; set; }
        public string city { get; set; }
        public string Address { get; set; }
        public string hours { get; set; }
        public string minutes { get; set; }
        public int DistKey { get; set; }


        public NextMeeting()
        {
            using (Dist23Data db = new Dist23Data())
            {
                NextMeeting nextMeeting;
                string connString = db.Database.Connection.ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "sp_NextMeeting";
                        cmd.Parameters.AddWithValue("@DistKey", GlobalVariables.DistKey);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                    this.pKey = Convert.ToInt32(reader[0]);
                                    this.Day = reader[1].ToString();
                                    this.Time = reader[2].ToString();
                                    this.type = reader[3].ToString();
                                    this.topic = reader[4].ToString();
                                    this.aaGroup = reader[5].ToString();
                                    this.location = reader[6].ToString();
                                    this.Address = reader[7].ToString();
                                    this.city = reader[8].ToString();
                                    this.hours = reader[9].ToString();
                                    this.minutes = reader[10].ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}