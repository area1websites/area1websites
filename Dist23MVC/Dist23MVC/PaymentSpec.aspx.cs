using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Dist23MVC
{
    public partial class PaymentSpec : System.Web.UI.Page
    {
        Dist23MVC.Models.clsDataGetter dg = new Models.clsDataGetter(ConfigurationManager.ConnectionStrings["Dist23Data"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["currSetupValue"] = 8;
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO PaymentSpecValues(PaymentSetupKey,SpecialValue,SpecialAmount) ";
            sql += "VALUES(" + Session["currSetupValue"].ToString() + ",'" + tbValue.Text + "','" + tbAmount.Text + "')";
            dg.RunCommand(sql);
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string pKey = GridView1.SelectedRow.Cells[1].Text;
            dg.RunCommand("DELETE FROM PaymentSpecValues WHERE pKey=" + pKey);
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }
    }
}