using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace GridView_Editing_With_SP
{
    public partial class StudentDetailsWIthProfilePhoto : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(ReadCS.ConStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if(FileUpload1.HasFiles)
            {
                HttpPostedFile selectedfile = FileUpload1.PostedFile;
                string fileExtension=Path.GetExtension(selectedfile.FileName);  
                if(fileExtension==".jpg" || fileExtension==".bmp" || fileExtension==".png")
                {
                    string imgName = selectedfile.FileName;
                    string folderPath = Server.MapPath("~/Images/");
                    if(!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    selectedfile.SaveAs(folderPath+imgName);
                    imgPhoto.ImageUrl = "~/Images/"+imgName;
                    BinaryReader br = new BinaryReader(selectedfile.InputStream);
                    byte[] imgData=br.ReadBytes(selectedfile.ContentLength);
                    Session["PhotoName"] = imgName;
                    Session["PhotoBinary"] = imgData;                        
                }
                else
                {
                    Response.Write("<script> alert('Supported image file formates are .jpg, .bmp .png only')</script>");
                }

            }
            else
            {
                Response.Write("<script> alert('Please select an image file to upload')</script>");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        private void ClearData()
        {
            txtId.Text = txtName.Text = txtClass.Text = txtFees.Text = lblMsgs.Text = imgPhoto.ImageUrl = "";
            Session["PhotoName"] = Session["PhotoBinary"] = null;
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "sp_selectRecord";
                cmd.Parameters.AddWithValue("@Sid",txtId.Text);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read()) 
                {                    
                    txtName.Text = dr["Name"].ToString();
                    txtClass.Text= dr["Class"].ToString();
                    txtFees.Text= dr["Fees"].ToString();
                    if (dr["PhotoName"]!=DBNull.Value)
                    {
                        imgPhoto.ImageUrl = "~/Images/" + dr["PhotoName"];
                        Session["PhotoName"] = dr["PhotoName"].ToString();
                    }
                    else
                    {
                        imgPhoto.ImageUrl = ""; Session["PhotoName"] = null;
                    }
                    if(dr["PhotoBinary"] != DBNull.Value)
                    {
                        Session["PhtoBinary"] = (byte[])dr["PhotoBinary"];
                    }
                    else
                    {
                        Session["PhtoBinary"] = null;
                    }
                }
                else
                {
                    Response.Write("<script> alert('No Student exist with given ID')</script>");
                    ClearData();
                }
            }
            catch(Exception ex)
            {
                lblMsgs.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "sp_Save_Student";
                AddParameters();
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Record inserted into the table')</script>");
                ClearData();
            }
            catch (Exception ex)
            {
                lblMsgs.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        private void AddParameters()
        {
            cmd.Parameters.AddWithValue("@Sid",txtId.Text);
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Class", txtClass.Text);
            cmd.Parameters.AddWithValue("@Fees ", txtFees.Text);
            if (Session["PhotoName"]!=null)
            {
                cmd.Parameters.AddWithValue("@PhotoName", Session["PhotoName"].ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@PhotoName",DBNull.Value);
                cmd.Parameters["@PhotoName"].SqlDbType = SqlDbType.VarChar;
            }
            if(Session["PhotoBinary"] != null)
            {
                cmd.Parameters.AddWithValue("@PhotoBinary", (byte[])Session["PhotoBinary"]);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PhotoBinary", DBNull.Value);
                cmd.Parameters["@PhotoBinary"].SqlDbType = SqlDbType.VarBinary;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "sp_Update_Student";
                AddParameters();
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Record Updated into the table')</script>");
                ClearData();
            }
            catch (Exception ex)
            {
                lblMsgs.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText ="sp_Delete_Student";
                cmd.Parameters.AddWithValue("@Sid", txtId.Text);
                //AddParameters();
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Record Deleted into the table')</script>");
                ClearData();
            }
            catch (Exception ex)
            {
                lblMsgs.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}