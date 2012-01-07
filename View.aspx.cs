using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ProfIT;

public partial class View : BasePage
{
	public int week = 0;

	public Object sessionweekObject;
    public const char DELIMITER_LIST_BOX_SELECTION = '§';
	String repeaterdate = "";
	Label label = new Label();
	Label labelperson = new Label();
	List<DateTime> JobDateList = new List<DateTime>();
	public DateTime dateTime = new DateTime();
	public DateTime dateTimePerson = new DateTime();
	public static string jobname = string.Empty;
	public static string tmpday = string.Empty;
	public static string jobweek = string.Empty;
	public static string orderref = string.Empty;
	public static string title = string.Empty;
	public static string tmp = string.Empty;
	public static string address = string.Empty;
	public static string jobdate = string.Empty;
	public static string active = string.Empty;
	public static string orderid = string.Empty;
	public static string value = string.Empty;
	public static string weekDay = string.Empty;
	public static string personal = string.Empty;
	public static string sessionweek = string.Empty;
	public static Guid PersonsID = new Guid();
    int weektoshow = 0;

	int displayweek = 0;
	private System.Globalization.CultureInfo m_currentCulture = null;
	List<Repeater> days = new List<Repeater>();

    protected void Page_Load(object sender, EventArgs e)
    {
       //MamutTest();

		if (Session["Week"] == null)
		{
			Session.Add("Week", week);
		}

		sessionweekObject = Session["Week"];

		if (sessionweekObject.ToString() == "0")
		{
			sessionweekObject = SiteUtilities.GetWeekNumber(DateTime.Now, 0);
		}

		displayweek = Convert.ToInt32(sessionweekObject);

		weeknumber.Text = "Vecka " + displayweek.ToString();

        
    }


	protected void GetAllJobs(object sender, EventArgs e)
	{
		Panel panel = sender as Panel;
		DateTime now = System.DateTime.Now;
		String daylabel = "";
		DateTime daytime = new DateTime();

		if (Session["Week"] == null)
		{
			Session.Add("Week", week);
		}
		sessionweekObject = Session["Week"];
		if (sessionweekObject.ToString() == "0")
		{
			sessionweekObject = SiteUtilities.GetWeekNumber(DateTime.Now, 0);
		}
        List<JobList> jobLists = new List<JobList>();

        GetDate(now, jobLists);
        for (int i = 0; i < jobLists.Count; i++)
	    {
            
            List<JobPersonList> personInJob = new List<JobPersonList>();
           var tmplist = SiteUtilities.GetJobsByDate(jobLists[i].Date);

            for (int j = 0; j < tmplist.Count; j++)
	        {
                personInJob = SiteUtilities.GetJobPersonal(tmplist[j].JobId);

                for (int k = 0; k < personInJob.Count; k++)
	            {
                    if (personInJob[k] != null)
                    {
                        string names = personInJob[k].Firstname + " " + personInJob[k].Lastname + " ";
                        tmplist[j].PersonsInThisJob += names;
                    }
	            }
	        }

	        jobLists[i].Jobs = tmplist;
	    }

      

	    jobRepeater.DataSource = jobLists;
        jobRepeater.DataBind();

	}

	

    protected void GetDate(DateTime now, List<JobList> jobLists)
    {

        DateTime firstdayofweek = new DateTime();
        DateTime date = new DateTime();
        firstdayofweek = SiteUtilities.GetFirstDayOfWeek(now.Year, displayweek);
        List<Job> jobs = new List<Job>();
        for (int i = 0; i < 5; i++)
        {
            JobList jobList = new JobList(firstdayofweek.AddDays(i), jobs);
            

            jobLists.Add(jobList);
        }

        
        return;

    }


    

    protected void InitCarsList(object sender, EventArgs e)
    {

        try
        {
            CheckBoxList checkBoxList = sender as CheckBoxList;
            List<ListItem> carsLists = new List<ListItem>();
            if (checkBoxList != null && Request.QueryString["JobID"] != null)
            {
                var jobslist = SiteUtilities.GetJobsByJobId(new Guid(Request.QueryString["JobID"]));
                var allcars = SiteUtilities.GetAllCars();
                var existingcars = SiteUtilities.GetCarFromJobID(jobslist[0].JobId);


                foreach (Car existingcar in existingcars)
                {
                    for (int i = 0; i < allcars.Count; i++)
                    {
                        if (existingcar.CarId == allcars[i].CarId)
                        {
                            string name = allcars[i].Name;
                            ListItem item = new ListItem(name, allcars[i].CarId.ToString());
                            item.Selected = true;
                            carsLists.Add(item);
                            allcars.Remove(allcars[i]);
                        }
                    }
                }

                for (int i = 0; i < allcars.Count; i++)
                {
                    string name = allcars[i].Name;
                    ListItem item = new ListItem(name, allcars[i].CarId.ToString());
                    carsLists.Add(item);
                }
            }
            else
            {
                
                var allcars = SiteUtilities.GetAllCars();
                for (int i = 0; i < allcars.Count; i++)
                {
                    string name = allcars[i].Name;
                    ListItem item = new ListItem(name, allcars[i].CarId.ToString());
                    carsLists.Add(item);

                }
            }




            checkBoxList.DataSource = carsLists;
            checkBoxList.DataBind();

            for (int i = 0; i < checkBoxList.Items.Count; i++)
            {
                if (checkBoxList.Items[i].Text == carsLists[i].Text)
                {
                    if (carsLists[i].Selected)
                    {
                        checkBoxList.Items[i].Selected = true;
                        checkBoxList.Items[i].Value = carsLists[i].Value;
                    }
                    else
                    {
                        checkBoxList.Items[i].Value = carsLists[i].Value;
                    }
                }
            }
        }
        catch 
        {
            
        }

    }

    protected void initJobs(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
        {

            var jobday = e.Item.FindControl("day") as Label;
            Repeater repeater = e.Item.FindControl("jobs") as Repeater;
            JobList jobList = e.Item.DataItem as JobList;


            if (jobList != null && repeater != null && jobday != null)
            {
                jobday.Text = jobList.Date.ToString("dddd", new System.Globalization.CultureInfo("sv-SE")) + " " + jobList.Date.ToString("m");
                repeater.DataSource = jobList.Jobs;
                repeater.DataBind();
            }

        }
        
    }

    protected void initJob(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Job job = e.Item.DataItem as Job;
            var jobstarttime = e.Item.FindControl("JobStartTime") as Literal;
            var jobendtime = e.Item.FindControl("JobEndTime") as Literal;
            var jobcar = e.Item.FindControl("JobCar") as Literal;
            var jobphone = e.Item.FindControl("JobPhone") as Literal;
            if (job != null)
            {
                if (jobstarttime != null)
                {
                    jobstarttime.Text = job.JobStartTime.TimeOfDay.ToString();
                }
                if (jobendtime != null)
                {
                    jobendtime.Text = job.JobEndTime.TimeOfDay.ToString();
                }

                if (jobphone != null)
                {
                    jobphone.Text = job.Phone;
                }

                if (jobcar != null)
                {
                    var existingcars = SiteUtilities.GetCarFromJobID(job.JobId);

                    for (int i = 0; i < existingcars.Count; i++)
                    {

                        jobcar.Text += existingcars[i].Name;

                    }
                    
                }

            }

        }



    }
}
