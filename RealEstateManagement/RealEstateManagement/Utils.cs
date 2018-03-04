using System.Web.UI;

namespace RealEstateManagement
{
    public class Utils
    {
        public static void ShowMessage( Page page, string msg )
        {
            string script = "alert(\"" + msg + "\");";
            ScriptManager.RegisterClientScriptBlock( page, page.GetType(),
                                  "ServerControlScript", script, true );
        }
    }
}