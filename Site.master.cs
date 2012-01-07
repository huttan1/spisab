using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Site : System.Web.UI.MasterPage
{
    SiteMapNode currentNode = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        currentNode = SiteMap.CurrentNode;

        // See if we are viewing the home node
       // if (currentNode != null && currentNode.ParentNode == null)
           // liHome.Attributes["class"] = "highlight";
    }

    protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            SiteMapNode nodeInRepeater = e.Item.DataItem as SiteMapNode;

            if (currentNode != null && nodeInRepeater != null)
            {
                bool selectNode = false;

                if (currentNode.Equals(nodeInRepeater))
                    selectNode = true;
                else if (currentNode.ParentNode != null && currentNode.ParentNode.Equals(nodeInRepeater))
                    selectNode = true;

                if (selectNode)
                {
                    HtmlGenericControl li = e.Item.FindControl("liNode") as HtmlGenericControl;
                    if (li != null)
                        li.Attributes["class"] = "highlight";
                }
            }
        }
    }
}
