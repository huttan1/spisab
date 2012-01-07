using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ProfIT;

public partial class JobsOverview : BasePage
{
	public int year = DateTime.Now.Year;
    public Object sessionyearObject;
    public int count;
    public int yearcount;
	int displayyear = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       //MamutTest();

		if (Session["Year"] == null)
		{
            Session.Add("Year", year);
		}
        sessionyearObject = Session["Year"];
		if (sessionyearObject.ToString() == "1")
		{
		    sessionyearObject = DateTime.Now.Year;
		}
        displayyear = Convert.ToInt32(sessionyearObject);

        weeknumber.Text = "År " + displayyear;

    }
    
    protected void GetFullOverViewList(List<JobOverviewList> jobOverviewLists, int year)
    {
        string month = "";


        for (int i = 1; i < 13; i++)
        {
            List<Job> jobsthismonth = new List<Job>();
            DateTime dateTime = new DateTime(year, i, 01);
            JobOverviewList jobOverview = new JobOverviewList(dateTime.ToString("MMMM"), jobsthismonth);
            jobOverviewLists.Add(jobOverview);
       }
        
        var list = CheckForJobsFromSession();

        if (list.Count > 0)
        {
            AddExtraJobs(list, jobOverviewLists);
        }

        for (int i = 1; i < jobOverviewLists.Count + 1; i++)
        {
            if (i < 10)
            {
                month = "0" + i;
            }
            else
            {
                month = i.ToString();
            }
            var jobs = SiteUtilities.GetJobsByMonth(year + "-" + month);
            for (int j = 0; j < jobs.Count; j++)
            {
                if (jobs[j].ParentJobID != Guid.Empty)
                {
                    return;
                }
                int startmonth = jobs[j].JobStartDate.Month - 1;
                switch (jobs[j].JobStartDate.Month - 1)
                {
                    case 0:
                        {
                            // Januari
                            jobOverviewLists[0].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 1:
                        {
                            // Februari
                            jobOverviewLists[1].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 2:
                        {
                            // Mars
                            jobOverviewLists[2].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 3:
                        {
                            // April
                            jobOverviewLists[3].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;

                    case 4:
                        {
                            // Maj
                            jobOverviewLists[4].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 5:
                        {
                            // Juni
                            jobOverviewLists[5].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 6:
                        {
                            // Juli
                            jobOverviewLists[6].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 7:
                        {
                            // Augusti
                            jobOverviewLists[7].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 8:
                        {
                            // September
                            jobOverviewLists[8].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    
                    case 9:
                        {
                            // Oktober
                            jobOverviewLists[9].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 10:
                        {
                            // November
                            jobOverviewLists[10].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                    case 11:
                        {
                            // December
                            jobOverviewLists[11].Jobs.Add(jobs[j]);
                            var months = CheckJobLenght(jobs[j]);
                            AddExtraMonths(jobOverviewLists, jobs[j], j, startmonth, months);
                        }
                        break;
                }
            }
        }
            
    }

    private void AddExtraJobs(IEnumerable<Job> list, List<JobOverviewList> jobOverviewLists)
    {
        
        foreach (var job in list)
        {
            if (job.JobEndDate.Year.ToString() == sessionyearObject.ToString())
            {

                if (job.JobEndDate.Month > 0)
                {
                    int startmonth = 0;
                    for (int k = 0; k < job.JobEndDate.Month; k++)
                    {
                        if (startmonth + k == 12)
                            return;
                        jobOverviewLists[startmonth + k].Jobs.Add(job);
                    }


                    for (int i = job.JobEndDate.Month; i < 12; i++)
                    {
                        if (i == 12)
                            return;
                        Job emptyjob = new Job(Guid.Empty, string.Empty, true, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, string.Empty, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, string.Empty, false, false, Guid.Empty, string.Empty, string.Empty, 0, string.Empty);
                        jobOverviewLists[i].Jobs.Add(emptyjob);
                    }
                }
                
            }
        }
    }

    private List<Job> CheckForJobsFromSession()
    {
        List<Job> jobs = new List<Job>();
        year = Convert.ToInt32(sessionyearObject);
        for (int i = 0; i < Session.Count; i++)
        {
            try
            {
                Job job = Session[i] as Job;

                if (job != null)
                {
                    jobs.Add(job);
                }

            }
            catch 
            {
                
            }
        }

        return jobs;

    }

    private void AddExtraMonths(List<JobOverviewList> jobOverviewLists, Job job, int j, int startmonth, int months)
    {
        

            for (int i = 0; i <= startmonth; i++)
            {
                Job emptyjob = new Job(Guid.Empty, string.Empty, true, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, string.Empty, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, string.Empty, false, false, Guid.Empty, string.Empty, string.Empty, 0, string.Empty);
                if (i == startmonth)
                    continue;
                jobOverviewLists[i].Jobs.Add(emptyjob);
            }

            int after = startmonth + months;

            for (int i = after; i < 12; i++)
            {
                if (job.JobEndDate.Month == i + 1)
                {
                    continue;
                }
                Job emptyjob = new Job(Guid.Empty, string.Empty, true, string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, string.Empty, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, string.Empty, false, false, Guid.Empty, string.Empty, string.Empty, 0, string.Empty);
                jobOverviewLists[i].Jobs.Add(emptyjob);
            }

            for (int k = 1; k < months; k++)
            {
                if (startmonth + k == 12)
                    return;
                jobOverviewLists[startmonth + k].Jobs.Add(job);
            }
       
    }

    private int CheckJobLenght(Job job)
    {
        if (job.JobStartDate.Month != job.JobEndDate.Month || job.JobStartDate.Year != job.JobEndDate.Year)
        {
            if (job.JobStartDate.Year == job.JobEndDate.Year)
            {
                return job.JobEndDate.Month - job.JobStartDate.Month + 1;
            }
            if (job.JobStartDate.Year != job.JobEndDate.Year)
            {
                SaveJobToSession(job);
                return 999;
            }
        }

        return 0;
    }

    private void SaveJobToSession(Job job)
    {
        if (!Page.IsPostBack)
        {
        yearcount = Convert.ToInt32(sessionyearObject);
        Session.Add("Job_" + job.JobId, job);
        count++;
        }
       
    }


    protected void NextYearLink(object sender, EventArgs e)
	{
        year = Convert.ToInt32(sessionyearObject);
        // Add One Year
        var newyear = year + 1;

        if (Session["Year"] != null)
        {
            // Delete old value
            Session.Remove("Year");
        }
            // Set new value
            Session.Add("Year", newyear);
		
		Response.Redirect(Request.Url.ToString(), false);

	}

    protected void PreviousYearLink(object sender, EventArgs e)
    {
        year = Convert.ToInt32(sessionyearObject);
        // Add One Year
        var newyear = year - 1;

        if (Session["Year"] != null)
        {
            // Delete old value
            Session.Remove("Year");
        }
        // Set new value
        Session.Add("Year", newyear);

        Response.Redirect(Request.Url.ToString(), false);

    }


    public class JobOverview
    {
        public DateTime JobStart { get; set; }
        public DateTime JobEnd { get; set; }
        public Job Job { get; set; }

        public JobOverview(DateTime jobstart, DateTime jobend, Job job)
        {
            JobStart = jobstart;
            JobEnd = jobend;
            Job = job;
        }
    }

    public class JobOverviewList
    {
        public string Month { get; set; }
        public List<Job> Jobs { get; set; }

        public JobOverviewList(string month, List<Job> jobs)
        {
            Month = month;
            Jobs = jobs;
        }
    }

    protected void GetAllMonths(object sender, EventArgs e)
    {
        if (Session["Year"] == null)
        {
            Session.Add("Year", year);
        }
        sessionyearObject = Session["Year"];

        DateTime DisplayYear = new DateTime(Convert.ToInt16(sessionyearObject), 01, 01);

        Repeater repeater = sender as Repeater;
        List<JobOverviewList> jobOverviewLists = new List<JobOverviewList>();
        GetFullOverViewList(jobOverviewLists, DisplayYear.Year);

        // Sortering
        //foreach (var jobOverviewList in jobOverviewLists)
        //{
        //    jobOverviewList.Jobs.Reverse();
        //}

        repeater.DataSource = jobOverviewLists;
        repeater.DataBind();

    }

    protected void initJobForMonth(object sender, RepeaterItemEventArgs e)
    {


        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            var month = e.Item.FindControl("month") as Label;
            Repeater repeater = e.Item.FindControl("jobs") as Repeater;
            JobOverviewList jobOverviewList = e.Item.DataItem as JobOverviewList;



            if (jobOverviewList != null && repeater != null && month != null)
            {

                month.Text = jobOverviewList.Month;
                repeater.DataSource = jobOverviewList.Jobs;
                repeater.DataBind();
            }

        }
       
    }

    protected void InitJob(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var repeater = e.Item.Parent as Repeater;

            RepeaterItem parent = repeater.Parent as RepeaterItem;

            JobOverviewList jobOverviewList = parent.DataItem as JobOverviewList;
            
            Job job = e.Item.DataItem as Job;

            if (job != null && job.JobName == "")
            {
                var width1 = e.Item.FindControl("width") as HtmlGenericControl;

                //width1.Attributes["Style"] = "visibility:hidden;height:50px;border:none;";
                width1.Attributes["Style"] = "height:50px;border:none;";
                return;
            }

            var width = e.Item.FindControl("width") as HtmlGenericControl;
            SetJobStyle(job, width, jobOverviewList.Month);
           
        }
    }

    private void SetJobStyle(Job job, HtmlGenericControl width, string month)
    {
        string color = "";
        if (job.Type == SiteUtilities.IMPORT)
        {
            color = "red";
        }
        else if (job.Type == SiteUtilities.CONSTRUCTION)
        {
            color = "orange";
        }
        else if (job.Type == SiteUtilities.DESTRUCTION)
        {
            color = "yellow";
        }
        else if (job.Type == SiteUtilities.DESTRUCTIONFINNISHED)
        {
            color = "lightgreen";
        }
        else if (job.Type == SiteUtilities.CONSTRUCTIONFINNISHED)
        {
            color = "green";
        }


        if (job.JobStartDate.ToString("MMMM") == month && job.JobEndDate.ToString("MMMM") != month)
        {
            // This job shall be over the end of the month but not start from begging
                DateTime LastDayOfMonth = new DateTime();
                if (job.JobStartDate.Month < 12)
                {
                    LastDayOfMonth = new DateTime(job.JobStartDate.Year, job.JobStartDate.Month + 1, 01).AddDays(-1);
                    
                }
                else
                {
                    LastDayOfMonth = new DateTime(job.JobStartDate.Year + 1, 01, 01).AddDays(-1);
                }

                double days = LastDayOfMonth.Day - job.JobStartDate.Day;
                double week = days / 7;
                width.Attributes["Style"] = "width:" + GetWidth(Math.Round(week)) + "%;overflow:hidden;height:50px;border:none;background-color:" + color + ";float:right;";
            return;
        }
        if (job.JobEndDate.ToString("MMMM") == month && job.JobStartDate.ToString("MMMM") != month)
        {
                DateTime FirstDayOfMonth = new DateTime(job.JobEndDate.Year, job.JobEndDate.Month, 01);
                double days = job.JobEndDate.Day - FirstDayOfMonth.Day;
                double week = days / 7;
                width.Attributes["Style"] = "width:" + GetWidth(Math.Round(week)) + "%;overflow:hidden;height:50px;border:none;background-color:" + color + ";";
                return;
          
        }
        if (job.JobStartDate.ToString("MMMM") == month && job.JobEndDate.ToString("MMMM") == month)
        {
            DateTime LastDayOfMonth = new DateTime();

            if (job.JobEndDate.Month < 12)
            {
                LastDayOfMonth = new DateTime(job.JobEndDate.Year, job.JobEndDate.Month + 1, 01).AddDays(-1);
            }
            else
            {
                LastDayOfMonth = new DateTime(job.JobEndDate.Year + 1, 01, 01).AddDays(-1);
            }
            double days = LastDayOfMonth.Day - job.JobEndDate.Day;
            double week = days / 7;
            width.Attributes["Style"] = "width:" + GetWidth(Math.Round(week)) + "%;overflow:hidden;height:50px;border:none;background-color:" + color + ";float:right;";
            return;
        }
            width.Attributes["Style"] = "width:" + 100 + "%;overflow:hidden;height:50px;border:none;background-color:" + color + ";";

        return;

    }

    private string GetWidth(double d)
    {
        int temp = Convert.ToInt16(d);

        switch (temp)
        {

            case 0:
                return "20";
            case 1:
                return "25";
            case 2:
                return "50";
            case 3:
                return "75";
            case 4:
                return "100";
        }

        return "100";
    }
}
