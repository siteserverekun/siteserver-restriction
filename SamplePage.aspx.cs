using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SamplePage : System.Web.UI.Page
{
    //public Literal LtlText;
    //public Label Lable1;

    public void Page_Load(object sender, EventArgs e)
    {
        LtlText.Text = "OK";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "Clicked at " + DateTime.Now.ToString();
        LtlText.Text = "Sample";
    }
}