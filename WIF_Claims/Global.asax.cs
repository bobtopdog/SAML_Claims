using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Saml;

namespace WIF_Claims
{
    public class Global : HttpApplication
    {
        public static string samlCertificate = ConfigurationManager.AppSettings["samlCertificate"].ToString();
        public static string str_samlEndpoint = ConfigurationManager.AppSettings["samlEndpoint"].ToString();
        public static string str_issuer = ConfigurationManager.AppSettings["issuer"].ToString();
        public static string str_redirect = ConfigurationManager.AppSettings["redirect"].ToString();
        public static string str_accesstype = ConfigurationManager.AppSettings["accessType"].ToString();
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Dictionary<string, HttpSessionState> sessionData =
               new Dictionary<string, HttpSessionState>();
            Application["s"] = sessionData;

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs  
        }
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started  
            Dictionary<string, HttpSessionState> sessionData = (Dictionary<string, HttpSessionState>)Application["s"];

            if (sessionData.Keys.Contains(HttpContext.Current.Session.SessionID))
            {
                sessionData.Remove(HttpContext.Current.Session.SessionID);
                sessionData.Add(HttpContext.Current.Session.SessionID,
                                HttpContext.Current.Session);
            }
            else
            {
                sessionData.Add(HttpContext.Current.Session.SessionID,
                                HttpContext.Current.Session);
            }
            Application["s"] = sessionData;

            if (str_accesstype == "SAML")
            {
                string SAMLresponse = Request.Form["SAMLResponse"];

                if (SAMLresponse != null)
                {
                    Response samlResponse = new Response(samlCertificate, SAMLresponse);

                    if (samlResponse.IsValid())
                    {

                        Session["IsValid"] = true;
                        Session["SAMLresponse"] = SAMLresponse;

                        //var userIdentity = new ClaimsIdentity("Identity");
                        //userIdentity.Label = "Identity";
                        //userIdentity.AddClaim(new Claim(ClaimTypes.Name, samlResponse.GetFirstName()));
                        //userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1"));
                        //userIdentity.AddClaim(new Claim(ClaimTypes.Country, "PL"));
                        //userIdentity.AddClaim(new Claim(ClaimTypes.Email, samlResponse.GetEmail()));
                        //userIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                        //userIdentity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                        //var identity = new ClaimsPrincipal(userIdentity);

                        ////   string plainName = identity.FindFirst(a => a.ValueType("Name").Value;

                        ////isAuthenticated ??
                        //bool isAuthenticated = identity.Identity.IsAuthenticated;
                        //// Console.WriteLine("isAuthenticated:{0}", isAuthenticated);


                        ////List of claims values
                        //Console.WriteLine("Claims:");
                        //foreach (var claim in identity.Claims)
                        //{
                        //    Console.WriteLine("Claim:{0} Value:{1}", claim.Type, claim.Value);
                        //}

                        //Console.WriteLine("Roles:");
                        //var isAdmin = identity.IsInRole("Admin");
                        //Console.WriteLine("isAdmin:{0}", isAdmin);
                        //var isSuperAdmin = identity.IsInRole("SuperAdmin");
                        //Console.WriteLine("isSuperAdmin:{0}", isSuperAdmin);
                        //var isUser = identity.IsInRole("User");
                        //Console.WriteLine("isUser:{0}", isUser);






                        ////Session["username"] = "user " + samlResponse.GetNameID();
                        ////Session["firstName"]  = samlResponse.GetFirstName();
                        ////Session["lastName"] = samlResponse.GetLastName();
                        ////Session["eMail"] = samlResponse.GetEmail();
                        ///
                        //Session["claims"] = samlResponse.GetGroupClaims();
                        //   string myClaims = "";
                        //   foreach (var claim in samlResponse.Claims)
                        //   {
                        //       myClaims += claim.ClaimType + " " + claim.Value + "<br />";
                        //   }
                        ////Session["nameID"] = samlResponse.GetNameID();
                    }
                    else
                    {
                        Session["IsValid"] = false;
                        Session["SAMLresponse"] = "";

                    }

                    StringBuilder s = new StringBuilder();
                    foreach (string key in Request.Form.Keys)
                    {
                        s.AppendLine(key + ": " + Request.Form[key]);
                    }
                    string formData = s.ToString();

                    Session["details"] = formData;

                }
                else
                {
                    var samlEndpoint = str_samlEndpoint;

                    var request = new AuthRequest(
                        str_issuer, //TODO: put your app's "entity ID" here
                        str_redirect //TODO: put Assertion Consumer URL (where the provider should redirect users after authenticating)
                        );

                    string url = request.GetRedirectUrl(samlEndpoint);

                    Response.Redirect(url);

                }

            }
            else
            {
                // Code for Windows Aurhentication  
                Session["IsValid"] = true;
            }


        }
        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.  
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer  
            // or SQLServer, the event is not raised.  
        }



    }
}