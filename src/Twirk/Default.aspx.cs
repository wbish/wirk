using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WiRK.TwirkIt
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			btnRunSimulations.Attributes.Add("onclick", "return ValidateSimulate();");
		}

		protected void btnRunSimulations_OnClick(object sender, EventArgs e)
		{
		}
	}
}