using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saml;

namespace WIF_Claims
{
    public partial class SiteMaster : MasterPage
    {
        public static string samlCertificate = ConfigurationManager.AppSettings["samlCertificate"].ToString();
        public static string str_samlEndpoint = ConfigurationManager.AppSettings["samlEndpoint"].ToString();
        public static string str_issuer = ConfigurationManager.AppSettings["issuer"].ToString();
        public static string str_redirect = ConfigurationManager.AppSettings["redirect"].ToString();

        public static string str_adminGroup = ConfigurationManager.AppSettings["AdminGroup"].ToString();
        public static string str_accesstype = ConfigurationManager.AppSettings["accessType"].ToString();
        protected void Page_Init(object sender, EventArgs e)
        {

            if (str_accesstype == "SAML")
            {
                string SAMLresponse = (string)Session["SAMLresponse"];
                Response samlResponse = new Response(samlCertificate, SAMLresponse);
                Session["claims"] = samlResponse.GetGroupClaims();

                    var userIdentity = new ClaimsIdentity("Identity");
                    userIdentity.Label = "Identity";
                    userIdentity.AddClaim(new Claim(ClaimTypes.Name, samlResponse.GetFirstName()));
                    userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, samlResponse.GetNameID()));
                    userIdentity.AddClaim(new Claim(ClaimTypes.Country, "PL"));
                    userIdentity.AddClaim(new Claim(ClaimTypes.Email, samlResponse.GetEmail()));

                try
                {
                    foreach (var claim in samlResponse.UserClaims)
                    {
                        userIdentity.AddClaim(new Claim(claim.ClaimType, claim.Value));
                    }
                }
                catch
                {

                }
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);
            // Set current principal
            Thread.CurrentPrincipal = claimsPrincipal;

            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            // Get the claims values
            string name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            string sid = identity.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            //isAuthenticated ??
            bool isAuthenticated = identity.Identity.IsAuthenticated;
            Literal1.Text += ("isAuthenticated:{0}", isAuthenticated).ToString() + "<br />";


            //List of claims values
            Literal1.Text += ("<hr />Claims:") + "<br />";
            foreach (var claim in identity.Claims)
            {
                Literal1.Text += ("Claim:{0} Value:{1}", claim.Type, claim.Value) + "<br />";
            }

            Literal1.Text += ("Roles:") + "<br />";
            var isAdmin = identity.IsInRole("Admin");
            Literal1.Text += ("isAdmin:{0}", isAdmin) + "<br />";
            var isSuperAdmin = identity.IsInRole("SuperAdmin");
            Literal1.Text += ("isSuperAdmin:{0}", isSuperAdmin) + "<br />";
            var isUser = identity.IsInRole("User");
            Literal1.Text += ("isUser:{0}", isUser) + "<br />";

                username.Text = name;

            } else // Windows 
            {

            }
        }
        
        public Boolean IsInAdminGroup
        {
            get
            {
                bool isAdmin = false;
                if (str_accesstype == "SAML")
                {
                    var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                    isAdmin = identity.IsInRole(str_adminGroup);
                }
                else
                {
                    isAdmin = HttpContext.Current.User.IsInRole(str_adminGroup);
                }
                return isAdmin;
            }

        }

        static bool ExactMatch(string input, string match)

        {

            return Regex.IsMatch(input, string.Format(@"\b{0}\b", Regex.Escape(match)));

        }

        public string getUserName
        {
            get
            {
                string username = "";
                if (str_accesstype == "SAML")
                {
                    var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                    username = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                } else
                {
                    username = HttpContext.Current.User.Identity.Name;
                }
                return username;
            }
        }

        public string GetEmail
        {
            get
            {
                string mailaddress = "";
                if (str_accesstype == "SAML")
                {
                    var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                    mailaddress = identity.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
                }
                else
                {
                    mailaddress = HttpContext.Current.User.Identity.Name;
                }
                return mailaddress;
            }
        }
    }


}