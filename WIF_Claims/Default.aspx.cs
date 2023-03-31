using Saml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIF_Claims
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {




            Label1.Text = Master.getUserName; // (string)Session["username"];
            Label2.Text = (string)Session["details"];
            //Label3.Text = (string)Session["firstName"];
            //Label4.Text = (string)Session["lastName"];
            //Label5.Text = (string)Session["eMail"];
            //Label6.Text = (string)Session["claims"];
            //Label7.Text = (string)Session["nameID"];
            //Label8.Text = Master.IsInAdminGroup.ToString();

            //if (Master.IsInAdminGroup)
            //{
            //    PlaceHolder1.Visible = true;
            //} else
            //{
            //    PlaceHolder1.Visible = false;
            //}


            Label9.Text = Master.IsInAdminGroup.ToString();


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           

        }
        
    }
}