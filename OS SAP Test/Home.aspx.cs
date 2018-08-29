using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;
namespace OS_SAP_Test
{
    public partial class Home : System.Web.UI.Page
    {
        SqlConnection conn_sql = new SqlConnection(@"Data Source=MICHAEL;Initial Catalog=ASPCRUD;Integrated Security=true");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (conn_sql.State == ConnectionState.Closed)
            {
                conn_sql.Open();
            }
            string filePath = ConfigurationManager.AppSettings["FilePath"].ToString();
            string filename = string.Empty;
            string new_filePath = "";
            if(fileUpload.HasFile)
            {
                try
                {
                    string[] allowedFile = { ".XLS", ".XLSX" };
                    string FileExt = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
                    bool isValidFile = allowedFile.Contains(FileExt);
                    if(!isValidFile)
                    {
                        lblMessage.Text = FileExt;
                    }
                    else
                    {
                        filename = Path.GetFileName(Server.MapPath(fileUpload.FileName));
                        fileUpload.SaveAs(Server.MapPath(filePath) + filename);
                        new_filePath = Server.MapPath(filePath) + filename;
                        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + new_filePath+";Extended Properties='Excel 12.0 XML;';");
                        con.Open();
                        //excel from storage
                        OleDbCommand ExcelCommand = new OleDbCommand("SELECT * FROM [Sheet3$]",con);
                        OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                        DataSet ExcelDataSet = new DataSet();
                        ExcelAdapter.Fill(ExcelDataSet);
                        gvStorage.DataSource = ExcelDataSet.Tables[0];
                        gvStorage.DataBind();
                        //insert to database storage
                        try
                        {
                            for (int i = 0; i < gvStorage.Rows.Count; i++)
                            {
                                string storage_location = gvStorage.Rows[i].Cells[0].Text;
                                string warehouse = gvStorage.Rows[i].Cells[1].Text;
                                if (warehouse.Trim() == "&nbsp;")
                                {
                                    warehouse = string.Empty;
                                }
                                SqlCommand cmd = new SqlCommand("insert into storage(storage_location,warehouse) values(@Storage_location,@Warehouse)", conn_sql);
                                cmd.Parameters.AddWithValue("@Storage_location", storage_location);
                                cmd.Parameters.AddWithValue("@Warehouse", warehouse);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch(Exception ex)
                        {
                            lblMessage.Text += ex.Message; 
                        };
                        if (ExcelDataSet.Tables.Count == 0)
                        {
                            
                            lblMessage.Text += "Storage Failed";
                        }
                        else
                        {
                            lblMessage.Text += "Storage Success! ";
                        }
                        //excel from detail_cs
                        ExcelCommand = new OleDbCommand("SELECT * FROM [Sheet1$]", con);
                        ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                        ExcelDataSet = new DataSet();
                        ExcelAdapter.Fill(ExcelDataSet);
                        //deleting rows if all cell are empty
                        
                        for(int i=5;i<24;i++)
                        {
                            ExcelDataSet.Tables[0].Rows.RemoveAt(i);
                        }
                        GridView1.DataSource = ExcelDataSet.Tables[0];
                        GridView1.DataBind();
                        //insert to database detail_cs
                        try
                        {
                            for (int k = 1; k < GridView1.Rows.Count; k++)
                            {
                                string exception = GridView1.Rows[k].Cells[0].Text;
                                string material = GridView1.Rows[k].Cells[1].Text;
                                string plant = GridView1.Rows[k].Cells[2].Text;
                                string storage_location = GridView1.Rows[k].Cells[3].Text;
                                string material_description = GridView1.Rows[k].Cells[4].Text;
                                string base_unit_of_measure = GridView1.Rows[k].Cells[5].Text;
                                string batch = GridView1.Rows[k].Cells[6].Text;
                                string unrestricted = GridView1.Rows[k].Cells[7].Text;
                                string in_quality_insp = GridView1.Rows[k].Cells[8].Text;
                                string blocked = GridView1.Rows[k].Cells[9].Text;
                                string total_stock = GridView1.Rows[k].Cells[10].Text;
                                string market = GridView1.Rows[k].Cells[11].Text;
                                string week = GridView1.Rows[k].Cells[12].Text;
                                string year = GridView1.Rows[k].Cells[13].Text;
                                string warehouse = GridView1.Rows[k].Cells[14].Text;
                                if (exception.Trim() == "&nbsp;")
                                {
                                    exception = string.Empty;
                                }
                                if (material.Trim() == "&nbsp;")
                                {
                                    material = string.Empty;
                                }
                                if (plant.Trim() == "&nbsp;")
                                {
                                    plant = string.Empty;
                                }
                                if (material_description.Trim() == "&nbsp;")
                                {
                                    material_description = string.Empty;
                                }
                                if (base_unit_of_measure.Trim() == "&nbsp;")
                                {
                                    base_unit_of_measure = string.Empty;
                                }
                                if (batch.Trim() == "&nbsp;")
                                {
                                    batch = string.Empty;
                                }
                                if (market.Trim() == "&nbsp;")
                                {
                                    market = string.Empty;
                                }
                                if (year.Trim() == "&nbsp;")
                                {
                                    year = string.Empty;
                                }
                                if(storage_location != string.Empty)
                                {
                                    SqlCommand cmd = new SqlCommand("insert into detail_cs(exception,material,plant,storage_location,material_description,base_unit_of_measure,batch,unrestricted,in_quality,blocked,total_stock,market,week,year,warehouse) values(@Exception,@Material,@Plant,@Storage_Location,@Material_Description,@Base_Unit_Of_Measure,@Batch,@Unrestricted,@In_Quality,@Blocked,@Total_Stock,@Market,@Week,@Year,@Warehouse)", conn_sql);
                                    cmd.Parameters.AddWithValue("@Exception", exception);
                                    cmd.Parameters.AddWithValue("@Material", material);
                                    cmd.Parameters.AddWithValue("@Plant", plant);
                                    cmd.Parameters.AddWithValue("@Storage_Location", storage_location);
                                    cmd.Parameters.AddWithValue("@Material_Description", material_description);
                                    cmd.Parameters.AddWithValue("@Base_Unit_Of_Measure", base_unit_of_measure);
                                    cmd.Parameters.AddWithValue("@Batch", batch);
                                    cmd.Parameters.AddWithValue("@Unrestricted", Convert.ToInt32(unrestricted));
                                    cmd.Parameters.AddWithValue("@In_Quality", Convert.ToInt32(in_quality_insp));
                                    cmd.Parameters.AddWithValue("@Blocked", Convert.ToInt32(blocked));
                                    cmd.Parameters.AddWithValue("@Total_Stock", Convert.ToInt32(total_stock));
                                    cmd.Parameters.AddWithValue("@Market", market);
                                    cmd.Parameters.AddWithValue("@Week", Convert.ToInt32(week));
                                    cmd.Parameters.AddWithValue("@Year", year);
                                    cmd.Parameters.AddWithValue("@Warehouse", warehouse);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            

                        }
                        catch (Exception ex)
                        {
                            Label1.Text += ex.Message;
                        };
                        if (ExcelDataSet.Tables.Count == 0)
                        {
                            lblMessage.Text += "Detail_CS Failed!!";
                        }
                        else
                        {
                            lblMessage.Text += "Detail_CS Success!!";
                        }

                        con.Close();
                    }
                }  
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }

        protected void gvStorage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}