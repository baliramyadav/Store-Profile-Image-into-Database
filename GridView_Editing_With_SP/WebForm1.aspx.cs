using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace GridView_Editing_With_SP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataSet ds;
        int Count;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(ReadCS.ConStr);            
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            if(!IsPostBack)
            {
                showdata();
            }            
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {                
                cmd.CommandText= "sp_Customer_Insert";                 
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Balance", txtBalance.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@Status", cbStatus.Checked);
                cmd.Parameters.Add("@Custid", SqlDbType.Int).Direction = ParameterDirection.Output;
                con.Open(); 
                if (cmd.ExecuteNonQuery() > 0)
                {
                    Response.Write("<script>alert('Record Inserted successfully into table.')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Failed Inserting the record in table.')</script>");
                }
                txtId.Text=cmd.Parameters["@Custid"].Value.ToString();
                clrControl();
                showdata();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public void clrControl()
        {
            txtId.Text = txtName.Text = txtBalance.Text = txtCity.Text = string.Empty;
            cbStatus.Checked = false;
        }

        public DataSet Customer_Select(int? CustId, bool? Status)
        {            
            try
            {
                cmd.CommandText = "sp_Customer_Select";                
                cmd.Parameters.Clear();
                if(CustId !=null && Status==null)
                    cmd.Parameters.AddWithValue("@Custid", CustId);
                else if(CustId == null && Status != null)
                    cmd.Parameters.AddWithValue("@Status", Status);
                else if(CustId != null && Status != null)
                {
                    cmd.Parameters.AddWithValue("@Custid", CustId);
                    cmd.Parameters.AddWithValue("@Status", Status);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds,"tblCustomer");
                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }           
        }
        public void showdata()
        {
            GridView1.DataSource = Customer_Select(null, true);
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            showdata();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            showdata();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int Custid = int.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text);
            string Name = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            decimal Balance = decimal.Parse(((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
            string City = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            try
            {
                cmd.CommandText = "sp_Customer_Update";                
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Custid", Convert.ToInt32(Custid));
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Balance", Balance);
                cmd.Parameters.AddWithValue("@City", City);
                con.Open();                 
                if (cmd.ExecuteNonQuery() > 0)
                {
                    GridView1.EditIndex = -1;
                    showdata();
                }
                else
                {
                    Response.Write("<script>alert('Failed updating the record in table.')</script>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                con.Close();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Custid = int.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text);
                cmd.CommandText = "sp_Customer_Delete";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Custid", Convert.ToInt32(Custid));                
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    GridView1.EditIndex = -1;
                    showdata();
                }
                else
                {
                    Response.Write("<script>alert('Failed deleting the record from table.')</script>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}