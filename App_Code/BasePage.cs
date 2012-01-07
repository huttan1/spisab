using System;
using System.IO;
using System.Collections.Generic;
using System.Web;

public class BasePage : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        // If there is not an explicitly set page title, then set it automatically
        if (string.IsNullOrEmpty(Page.Title) || string.Compare(Page.Title, "Untitled Page", true) == 0)
        {
            // Set the page's Title based on the site map
            SiteMapNode currentNode = SiteMap.CurrentNode;

            string pageTitle = string.Empty;

            if (currentNode != null)
            {
                // The current page exists in the site map... build up the title based on the site map info
                pageTitle = currentNode.Title;

                // Walk up the site map to the root, prepending each node's title to the page title
                currentNode = currentNode.ParentNode;
                while (currentNode != null)
                {
                    pageTitle = string.Concat(currentNode.Title, " : ", pageTitle);

                    currentNode = currentNode.ParentNode;
                }
            }
            else
            {
                // This page is not in the site map, use the filename
                pageTitle = Path.GetFileNameWithoutExtension(Request.PhysicalPath);
            }

            // Set the page title
            Page.Title = pageTitle;
        }


        base.OnLoad(e);
    }

    protected virtual void DisplayAlert(string message)
    {
        ClientScript.RegisterStartupScript(
                    this.GetType(),
                    Guid.NewGuid().ToString(),
                    string.Format("alert('{0}');", message.Replace("'", @"\'")),
                    true
                );
    }
}