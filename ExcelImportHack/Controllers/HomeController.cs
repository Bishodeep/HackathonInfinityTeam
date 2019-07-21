using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace ExcelImportHack.Controllers
{
    public class HomeController : Controller
    {
        int x;
        int y;
         static SqlConnection con = new SqlConnection("Data Source=LAPTOP-13OTI979;Integrated Security=true;Database=Test2");
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        public ActionResult ImportDataTables()
        {
            try
            {
                string cs_file_path = @"C:\Users\hp\Desktop\Tables.csv";
                DataTable csvData = GetDataTabletFromCSVFile(cs_file_path);
                x = csvData.Rows.Count;
                bool stat = true;
                foreach (DataRow item in csvData.Rows)
                {
                    var name = item["Name"].ToString();
                    var tableName = item["ListName"].ToString();
                    if (name != "" && tableName != "")
                    {
                        if (stat)
                        {
                            CreateTables(tableName);

                            stat = false;
                        }
                        StoreDatainTables(tableName, name);
                    }
                    else
                    {
                        stat = true;
                    }
                }
            }
            catch (Exception e)
            {

                
            }
           
            return View();
        }
        public static void CreateTables(string tableName)
        {
            try
            {
                var query = "CREATE TABLE " + tableName + " ( Id INT PRIMARY KEY IDENTITY(1, 1), Name NVARCHAR(MAX))";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

               
            }

           
        }
        public static void StoreDatainTables(string tableName, string name)
        {
            try
            {
                var query = "INSERT INTO " + tableName + "(Name) Values ( '" + name + " ')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

                
            }
           
        }
        public ActionResult ImportData()
       {
            try
            {
                string cs_file_path = @"C:\Users\hp\Desktop\MainData3.csv";
                DataTable csvData = GetDataTabletFromCSVFile(cs_file_path);
                x = csvData.Rows.Count;
                var yx = csvData.Columns.Count;

                //int i = 0;
                CreateTable("UrbanData");

                var tests = csvData.Rows[0][0].ToString();
                var tester = csvData.Columns[0].ToString();
                var test1 = csvData.Rows[1][0].ToString();

                int c = 0;
                bool stat = true;
                var testquery = "";
                for(int i = 0; i < csvData.Columns.Count; i++)
                {
                    c++;
                    var test = csvData.Columns[i].ToString().Replace(" ", "_");
                    var test2 = test.Replace("?", "_");
                    var test4 = test2.Replace("-", "_");
                    var test5 = test4.Replace(",", "_");
                    var test6 = test5.Replace("(", "_");
                    var test7 = test6.Replace(")", "_");
                    var test8 = test7.Replace("<", "_");
                    var test9 = test8.Replace(">", "_");
                    var test10 = test9.Replace(".", "_");
                    var test11 = test10.Replace("&", "_");
                    var test12 = test11.Replace("–", "_");
                    var test13 = test12.Replace("’", "_"); 
                    
                    var test3 = test13.Replace("/", "_");
                    //create column



                    //AlterTable("UrbanData", test3);
                    var query = "ALTER TABLE UrbanData ADD " + test3 + " INT ";
                    testquery = testquery + query;
                   
                }
                SqlCommand cmd = new SqlCommand(testquery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string name = "";
                string value = "";
                int k = 0;
                stat = true;

                for (int i = 0; i < csvData.Rows.Count; i++)
                {
                    bool check = true;
                    for (int j = 0; j < csvData.Columns.Count; j++)
                    {
                        var test = csvData.Columns[j].ToString().Replace(" ", "_");
                        var test2 = test.Replace("?", "_");
                        var test4 = test2.Replace("-", "_");
                        var test5 = test4.Replace(",", "_");
                        var test6 = test5.Replace("(", "_");
                        var test7 = test6.Replace(")", "_");
                        var test8 = test7.Replace("<", "_");
                        var test9 = test8.Replace(">", "_");
                        var test10 = test9.Replace(".", "_");
                        var test11 = test10.Replace("&", "_");
                        var test12 = test11.Replace("–", "_");
                        var test13 = test12.Replace("’", "_");
                        var test3 = test13.Replace("/", "_");

                   
                   
                        if (stat)
                        {
                            //foreign key
                            AlterTableForeignKey("UrbanData", test3, csvData.Rows[i][j].ToString());

                            
                        }
                        else
                        {
                            //front row back column
                            if(check == true)
                            {
                                name = test3;
                                value = csvData.Rows[i][j].ToString();
                                check = false;
                            }
                            else
                            {
                                name = name + ',' + test3;
                                if(csvData.Rows[i][j].ToString() == "")
                                {
                                    value = value + ",null";
                                }
                                else
                                {
                                    value = value + ',' + csvData.Rows[i][j].ToString();
                                }
                                
                            }
                            

                            
                        }

                    }
                    if (stat == false)
                    {
                        StoreData("UrbanData", name, value);
                    }
                   
                    stat = false;
                }
            }
            catch (Exception e)
            {

               
            }


            return View();
       }
        public static void CreateTable(string tableName)
        {
            try
            {
                var query = "CREATE TABLE " + tableName + " ( Id INT PRIMARY KEY IDENTITY(1, 1))";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

               
            }

            
        }
        public static void AlterTable(string tableName,string columnName)
        {

            try
            {
                var query = "ALTER TABLE " + tableName + " ADD " + columnName + " INT";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }
          
        }
        public static void AlterTableForeignKey(string tableName, string columnName,string foreignTable)
        {
            try
            {
                var query = "ALTER TABLE " + tableName + " ADD FOREIGN KEY (" + columnName + ") REFERENCES " + foreignTable + "(Id)";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

              
            }

            
        }

        public static void StoreData(string tableName, string columnName, string value)
        {
            try
            {
                var query = "";
                if (value != "")
                {
                     query = "INSERT INTO " + tableName + "(" + columnName + ")Values(" + value + ")";
                    
                }
                else
                {
                     query = "INSERT INTO " + tableName + "(" + columnName + ")Values(null)";
                    
                }
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

               
            }
         
        }



        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)

        {

            DataTable csvData = new DataTable();

            try

            {

                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))

                {

                    csvReader.SetDelimiters(new string[] { "," });

                    csvReader.HasFieldsEnclosedInQuotes = true;

                    string[] colFields = csvReader.ReadFields();
              
                    foreach (string column in colFields)

                    {

                        DataColumn datecolumn = new DataColumn(column);

                        datecolumn.AllowDBNull = true;

                        csvData.Columns.Add(datecolumn);
                       
                    }
               
                    while (!csvReader.EndOfData)

                    {

                        string[] fieldData = csvReader.ReadFields();

                        //Making empty value as null

                        for (int i = 0; i < fieldData.Length; i++)

                        {

                            if (fieldData[i] == "")

                            {

                                fieldData[i] = null;

                            }

                        }

                        csvData.Rows.Add(fieldData);

                    }

                }

            }

            catch (Exception ex)

            {

            }

            return csvData;

        }

    }
}