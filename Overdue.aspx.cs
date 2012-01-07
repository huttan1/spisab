using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ASP.App_Code;
using ProfIT;

public partial class Overdue : BasePage
{
	public int week = 0;

	public Object sessionweekObject;
    int displayweek = 0;
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

    protected void initOverdue(object sender, EventArgs e)
    {
        Repeater repeater = sender as Repeater;
        
        var jobs = SiteUtilities.GetRentalOverDue(DateTime.Now);
        repeater.DataSource = jobs;
        repeater.DataBind();
    }

    protected void initJob(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Job job = e.Item.DataItem as Job;
            var jobstarttime = e.Item.FindControl("JobStartTime") as Literal;
            var jobendtime = e.Item.FindControl("JobEndTime") as Literal;
            var info = e.Item.FindControl("info") as Literal;
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

                if (info != null)
                {
                    DateTime now = DateTime.Now;

                    TimeSpan daysover = now - job.RentEndDate;

                    info.Text = "Slutdatum: " + job.RentEndDate.ToShortDateString() + "</br>" + "Dagar över tid: " +  daysover.Days.ToString();
                }

            }

        }



    }
}
