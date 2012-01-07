using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// The ReviewSiteMapProvider class generates a site map based on the genres and books in the database. For more information on creating and
/// using a custom site map provider, see:
///    * http://aspnet.4guysfromrolla.com/articles/020106-1.aspx
///    * http://msdn.microsoft.com/en-us/magazine/cc163657.aspx
/// </summary>
public class ReviewSiteMapProvider : StaticSiteMapProvider
{
    private object lockObj = new object();
    private string connectionStringName = null;
    private SiteMapNode root = null;
    public static SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
    {
        if (string.IsNullOrEmpty(attributes["connectionStringName"]))
            throw new ProviderException("Missing provider attribute connectionStringName");

        connectionStringName = attributes["connectionStringName"];
        
        attributes.Remove("connectionStringName");

        base.Initialize(name, attributes);
    }

    public override SiteMapNode BuildSiteMap()
    {
        lock (lockObj)
        {
            // If we've already constructed the site map, return it
            if (root != null)
                return root;
            else
                base.Clear();

            // Otherwise, build the site map... first, add the Home node
            root = new SiteMapNode(this, "root", "~/Default.aspx", "Home");
            base.AddNode(root);

            // Get the genres and books
            string connString = sqlConnection.ConnectionString;
            string currentGenreName = string.Empty;
            SiteMapNode currentGenreNode = null;

            //using (SqlConnection myConnection = new SqlConnection(connString))
            //{
            //    SqlCommand myCommand = new SqlCommand("usp_GenresAndBooksForSiteMap", myConnection);
            //    myCommand.CommandType = CommandType.StoredProcedure;

            //    myConnection.Open();
            //    SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                
            //    while (myReader.Read())
            //    {
            //        string genre = myReader["Genre"].ToString();
            //        string genreId = myReader["GenreId"].ToString();
            //        string title = myReader["Title"].ToString();
            //        string bookId = myReader["BookId"].ToString();

            //        if (string.Compare(genre, currentGenreName) != 0)
            //        {
            //            // This is a new genre, add the genre site map node
            //            currentGenreNode = new SiteMapNode(this,
            //                                               genreId,
            //                                               string.Format("~/Genre.aspx?ID={0}", genreId),
            //                                               genre);

            //            currentGenreName = genre;

            //            base.AddNode(currentGenreNode, root);
            //        }

            //        // Add the book review site map node
            //        SiteMapNode bookNode = new SiteMapNode(this,
            //                                               bookId,
            //                                               string.Format("~/Review.aspx?ID={0}", bookId),
            //                                               title);

            //        base.AddNode(bookNode, currentGenreNode);
            //    }
                
            //    myReader.Close();
            //    myConnection.Close();
            //}

            //// Add the About node as a child of Home
            //SiteMapNode about = new SiteMapNode(this, "about", "~/About.aspx", "About");
            //base.AddNode(about, root);

            //// Finally, add the Admin node and its children nodes
            //SiteMapNode admin = new SiteMapNode(this, "admin", "~/Admin/Default.aspx", "Admin");
            //SiteMapNode manageGenres = new SiteMapNode(this, "admin-genres", "~/Admin/ManageGenres.aspx", "Manage Genres");
            //SiteMapNode manageAuthors = new SiteMapNode(this, "admin-authors", "~/Admin/ManageAuthors.aspx", "Manage Authors");
            //SiteMapNode manageReviews = new SiteMapNode(this, "admin-reviews", "~/Admin/ManageReviews.aspx", "Manage Reviews");

            //base.AddNode(admin, root);
            //base.AddNode(manageGenres, admin);
            //base.AddNode(manageAuthors, admin);
            //base.AddNode(manageReviews, admin);

            //// Return the site map's root
            return root;
        }
    }

    protected override SiteMapNode GetRootNodeCore()
    {
        return BuildSiteMap();
    }

    public void RefreshSiteMap()
    {
        lock (lockObj)
        {
            root = null;
        }
    }
}
