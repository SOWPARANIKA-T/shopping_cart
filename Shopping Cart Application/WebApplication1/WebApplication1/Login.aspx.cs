using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Username = txtUname.Text;
            string Userpassword = txtPassword.Text;

            if (Username == "" && Userpassword == "")
            {
                Label1.Text = "Enter Valid Username and Password";
            }
            else if (Username != "" && Userpassword != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=Webapp;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("select * from userReg where UName=@UName and UPassword=@UPassword", con);
                    cmd.Parameters.AddWithValue("@UName", txtUname.Text);
                    cmd.Parameters.AddWithValue("@UPassword", txtPassword.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        Response.Redirect("ProductListPage.aspx");
                    }

                    else
                    {

                        Label1.Visible = true;
                        Label1.Text = "Login Failed! Enter valid username/Password";
                        Label1.ForeColor = System.Drawing.Color.Red;

                    }
                }
                catch(Exception exp)
                {
                    Label1.Text = exp.ToString();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Response.Redirect("RegistrationPage.aspx");
            RegistrationPage reg = new RegistrationPage();
            reg.Visible = true;
        }
    }
}