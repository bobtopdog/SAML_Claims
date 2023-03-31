using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIF_Claims
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<SessionData> gv = new List<SessionData>();
                Dictionary<string, HttpSessionState> sessionData =
                  (Dictionary<string, HttpSessionState>)Application["s"];

                if (sessionData != null)
                {
                    foreach (KeyValuePair<string, HttpSessionState> item in sessionData)
                    {
                        gv.Add(new SessionData()
                        {
                            SessionId = item.Key,
                            SessionKey = null,
                            SessionValue = null
                        });
                        if (item.Value != null && item.Value.Count > 0)
                        {
                            foreach (string key in item.Value.Keys)
                            {
                                gv.Add(new SessionData()
                                {
                                    SessionId = item.Key,
                                    SessionKey = key,
                                    SessionValue = item.Value[key] != null ?
                                   item.Value[key].ToString() : string.Empty
                                });
                            }
                        }
                    }
                    sessionValuesGrid.DataSource = gv;
                    sessionValuesGrid.DataBind();
                }
            }
            catch { }
        }
    }
    public class SessionData
    {
        public string SessionId { get; set; }
        public string SessionKey { get; set; }
        public string SessionValue { get; set; }
    }
}