/* BLOB2File / Project Codenamed "Porcupine"
 * Type: Console Application
 * 
 * Assumptions:
 * 1. The BLOBs have a separate column
 * 2. The filenames with their extensions, i.e. full filenames are stored in another column
 * 3. Both columns are in the same table
 * 
 * Authors: Siddharth Mankad, Vivek Shrinivasan
 */

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

            //Sample query to test first 10 files before jumping in and parsing the whole table which could have 1000+ rows
            //string tblFile10 = "SELECT TOP 10 fileName, fileContent FROM dbo.sueFile";

            Console.WriteLine("Project Porcupine - Console Edition");
            
            //Initialize the file counter
            int counter = 1;
            
            //Initializing the SQL Connection
            //Since the SQL Server is local, and Auth is Windows, no credentials are required in the connection string
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXP2008R2;" + "Initial Catalog=mydatabase;" + "Integrated Security=True");

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
            Console.WriteLine("Firing command to the database...\n");
            Console.WriteLine("Attempting to fetch ALL BLOBs...\n");
            try
            {
                SqlDataReader rdr = null;
                SqlCommand com = new SqlCommand(tblFileblob.ToString(), con);
                rdr = com.ExecuteReader();

                //Once the record has been fetched, we will process that
                while (rdr.Read())
                {
                    //Testing the connection with a sample console output of the filenames
                    //Console.WriteLine((rdr["fileName"]).ToString());

                    if(BLOB2File(rdr["fileName"].ToString(),(byte[])rdr["fileContent"]))
                    {
                        //if the file write method is successful, it will return true and the following code will be executed
                        Console.WriteLine(counter.ToString()+". "+rdr["fileName"].ToString() + " written.");
                        counter++;
                    }
                    else
                    {
                        //if the file write method fails, it will return false and the following code will be executed
                        Console.WriteLine(counter.ToString() + ". " + rdr["fileName"].ToString() + " not written.");
                        counter++;
                    }
                }

                //Closing the connection - cleaning up
                try
                {
                    con.Close();
                }
                catch (Exception e)
                {
                    //Any errors will be outputted to the console
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
                //Replace the path with the desired location where you want the files to be written
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
                //Any errors will be outputted to the console
                Console.WriteLine(e.ToString());
            }
            //If there's a problem, i.e. it fails
            return false;
        }
    }
}
