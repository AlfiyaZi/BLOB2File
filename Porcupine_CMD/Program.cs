using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Porcupine_CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initializing SQL commands
            string tblFileblob = "SELECT fileName, fileContent FROM dbo.sueFile";
            //string tblFile10 = "SELECT TOP 10 fileName, fileContent FROM dbo.sueFile";

            Console.WriteLine("Project Porcupine - Console Edition");
            
            
            //Initializing the SQL Connection
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXP2008R2;" + "Initial Catalog=ucdportal;" + "Integrated Security=True");

            //Opening up the connection
            try
            {
                Console.WriteLine("Opening connection to the database...");
                con.Open();
                Console.WriteLine("Database connection ready. Awaiting further instructions.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //Get the resultset
            Console.WriteLine("Firing command to database...\n");
            Console.WriteLine("Attempting to fetch ALL BLOBs...\n");
            try
            {
                SqlDataReader rdr = null;
                SqlCommand com = new SqlCommand(tblFileblob.ToString(), con);
                rdr = com.ExecuteReader();

                //Once the records/stream(s) have been fetched, we will start parsing
                while (rdr.Read())
                {
                    //Console.WriteLine((rdr["fileName"]).ToString());
                    if(BLOB2File(rdr["fileName"].ToString(),(byte[])rdr["fileContent"]))
                    {
                        Console.WriteLine(rdr["fileName"].ToString() + " written.");
                    }
                    else
                    {
                        Console.WriteLine(rdr["fileName"].ToString() + " not written.");
                    }
                }

                //Closing the connection
                try
                {
                    con.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();
        }

       
        public static bool BLOB2File(string _fname, byte[] _blob)
        {
            try 
            {
                //Create a new filestream
                FileStream _fs = new FileStream("C:\\Temp\\ucdportaldata\\" + _fname, FileMode.Create, FileAccess.Write);

                //Writing the stream to file
                _fs.Write(_blob, 0, _blob.Length);

                //Closing the stream
                _fs.Close();

                //If it succeeds
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //Seem to be a problem
            return false;
        }
    }
}
