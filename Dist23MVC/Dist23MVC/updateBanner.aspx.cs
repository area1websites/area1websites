using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dist23MVC.Models;
using System.Configuration;
using System.Data.SqlClient;


namespace Dist23MVC
{
    public partial class updateBanner : System.Web.UI.Page
    {

        clsDataGetter dg = new clsDataGetter(ConfigurationManager.ConnectionStrings["Dist23Data"].ConnectionString);
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dr = dg.GetDataReader("SELECT TOP(1) * FROM SiteConfig WHERE DistKey=" + Session["DistKey"].ToString());
                if (dr.Read())
                {
                    tbBannerTitle.Text = dr["BannerTitle"].ToString();
                    tbBannerSubTitle.Text = dr["BannerSubTitle"].ToString();
                    tbHotlinePh.Text = dr["HotlinePh"].ToString();
                    tbAltHotline.Text = dr["AltHotline"].ToString();
                    tbAltHotlineMsg.Text = dr["AltHotlineMsg"].ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE SiteConfig SET BannerTitle='" + tbBannerTitle.Text + "',";
            sql += "BannerSubTitle='" + tbBannerSubTitle.Text + "',";
            sql += "HotlinePh='" + tbHotlinePh.Text + "',";
            sql += "AltHotline='" + tbAltHotline.Text + "',";
            sql += "AltHotlineMsg='" + tbAltHotlineMsg.Text + "'";
            sql += " WHERE DistKey=" + Session["DistKey"].ToString();
            dg.RunCommand(sql);
        }
    }
}

