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

public partial class Jobs : BasePage
{
	public int week = 0;

	public Object sessionweekObject;
    public const char DELIMITER_LIST_BOX_SELECTION = '§';
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
    public Guid JobID = Guid.Empty;
    public DateTime now = new DateTime();
	int displayweek = 0;
    public int CurrentYear = 2011;
    public int CurrentWeek = 0;
    public DateTime FirstDayofWeek;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["JobID"] != null)
        {
            JobID = new Guid(Request.QueryString["JobID"]);

            var job = SiteUtilities.GetJobsByJobId(JobID);

            if (job.Count > 0)
            {
                sessionweekObject = SiteUtilities.GetWeekNumber(job[0].JobStartDate, 0);
                displayweek = Convert.ToInt16(sessionweekObject);
                if (Session["Week"] == null)
                {
                    Session.Add("Week", week);
                }
                CurrentYear = job[0].JobStartDate.Year;
                CurrentWeek = Convert.ToInt16(sessionweekObject);
                now = job[0].JobStartDate;
                Session["Week"] = sessionweekObject;

                if (job[0].JobStartDate.ToString() == "9999-12-31 00:00:00")
                {
                    FirstDayofWeek = SiteUtilities.GetFirstDayOfWeek(DateTime.Now.Year, SiteUtilities.GetWeekNumber(DateTime.Now, 0));
                }
                else
                {
                    FirstDayofWeek = SiteUtilities.GetFirstDayOfWeek(CurrentYear, CurrentWeek);    
                }
            }
        }
        else
        {
            if (Session["CurrentWeekFirstDay"] != null)
            {
                FirstDayofWeek = Convert.ToDateTime(Session["CurrentWeekFirstDay"]);
            }
            else
            {
                FirstDayofWeek = SiteUtilities.GetFirstDayOfWeek(DateTime.Now.Year, SiteUtilities.GetWeekNumber(DateTime.Now, 0));
            }
        }


        displayweek = SiteUtilities.GetWeekNumber(FirstDayofWeek, 0);
        weeknumber.Text = "Vecka " + displayweek.ToString() + " År: " + FirstDayofWeek.Year;
        Job(sender, e);
    }

    protected void GetAllJobs(object sender, EventArgs e)
    {
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

        GetDate(FirstDayofWeek, jobLists);
        for (int i = 0; i < jobLists.Count; i++)
        {
            //List<Job> tmplist = new List<Job>();
            var personInJob = new List<JobPersonList>();

            var tmplist = SiteUtilities.GetJobsByDate(jobLists[i].Date);

            for (int j = 0; j < tmplist.Count; j++)
            {
                personInJob = SiteUtilities.GetJobPersonal(tmplist[j].JobId);

                for (int k = 0; k < personInJob.Count; k++)
                {
                    if (personInJob[k] != null)
                    {
                        if (personInJob[k].Active)
                        {
                            string names = personInJob[k].Firstname + " " + personInJob[k].Lastname.Substring(0,1) + " ";
                            tmplist[j].PersonsInThisJob += names;
                        }
                    }
                }
            }

            jobLists[i].Jobs = tmplist;
        }
        jobRepeater.DataSource = jobLists;
        jobRepeater.DataBind();

    }

    protected void initJobs(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
            var jobday = e.Item.FindControl("day") as Label;
            var personal = e.Item.FindControl("personal") as Label;
            Repeater repeater = e.Item.FindControl("jobs") as Repeater;
            JobList jobList = e.Item.DataItem as JobList;
            List<Person> persons = new List<Person>();

            if (jobList != null && repeater != null && jobday != null)
            {
                if (personal != null)
                {
                   persons = SiteUtilities.GetAvaliablePersonal(jobList.Date);
                    for (int i = 0; i < persons.Count; i++)
                    {
                        if (persons[i].Active)
                        {
                            string name = persons[i].Firstname + " " + persons[i].Lastname.Substring(0,1) + " ";
                            personal.Text += name;
                        }
                    }
                }
                jobday.Text = jobList.Date.ToString("dddd", new System.Globalization.CultureInfo("sv-SE")) + " " + jobList.Date.ToString("m");
                repeater.DataSource = jobList.Jobs;
                repeater.DataBind();
            }

        }

    }


	protected void GetAllNewJobs(object sender, EventArgs e)
	{
		Panel panel = sender as Panel;
		String daylabel = "";
		if (Session["Week"] == null)
		{
			Session.Add("Week", week);
		}
		sessionweekObject = Session["Week"];
		if (sessionweekObject.ToString() == "0")
		{
			sessionweekObject = SiteUtilities.GetWeekNumber(DateTime.Now, 0);
		}

        if (panel != null && panel.ID == "NoDate")
        {
            List<JobPersonList> personInJob = new List<JobPersonList>();
            var jobslist = SiteUtilities.GetJobsByDate(DateTime.MaxValue);

            for (int i = 0; i < jobslist.Count; i++)
            {
                personInJob = SiteUtilities.GetJobPersonal(jobslist[i].JobId);

                for (int j = 0; j < personInJob.Count; j++)
                {
                    if (personInJob[j] != null)
                    {
                        if (personInJob[j].Active)
                        {
                            string names = personInJob[j].Firstname + " " + personInJob[j].Lastname + " ";
                            jobslist[i].PersonsInThisJob += names;
                        }
                    }
                }
            }

            NoDayRepeater.DataSource = jobslist;
            NoDayRepeater.DataBind();
            NoDateLabel.Text = daylabel;
        }
	}


	protected void JobDateInit(object sender, EventArgs e)
	{

		if (Request.QueryString["JobID"] != null)
		{
		    try
		    {

                Guid jobid = new Guid(Request.QueryString["JobID"]);

                var jobslist = SiteUtilities.GetJobsByJobId(jobid);
                var tmplist =  SiteUtilities.GetCloneJobsByJobId(jobslist[0].JobId);


                if (tmplist.Count == 0 && jobslist[0].ParentJobID == Guid.Empty)
                {
                    if (jobslist[0].Type == SiteUtilities.CONSTRUCTION)
                    Button1.Visible = false;
                }

                if (jobslist[0].ParentJobID == Guid.Empty)
                {
                    
                }
                else if (jobslist[0].ParentJobID != Guid.Empty)
                {
                    if (jobslist[0].Type == SiteUtilities.CONSTRUCTION || jobslist[0].Type == SiteUtilities.DESTRUCTION)
                    {
                        JobDate.Visible = false;
                        JobDateEnd.Visible = false;
                        JobDateSet.Visible = false;
                        FreeWeeksOfRental.Visible = false;
                        JobName.Enabled = false;
                        JobAddress.Enabled = false;
                    }
                }
                else if (jobslist[0].Type == SiteUtilities.CONSTRUCTIONFINNISHED || jobslist[0].Type == SiteUtilities.DESTRUCTIONFINNISHED)
                {
                    JobDate.Visible = false;
                    JobDateEnd.Visible = false;
                    JobDateSet.Visible = false;
                    FreeWeeksOfRental.Visible = false;
                    JobName.Enabled = false;
                    JobAddress.Enabled = false;
                }
                
                    if (jobslist[0].JobStartDate.ToShortDateString() == "9999-12-31")
                    {
                        JobDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        if (jobslist[0].JobEndDate.ToShortDateString() == "9999-12-31")
                        {
                            JobDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                    }
                    else
                    {
                        JobDate.Text = jobslist[0].JobStartDate.ToShortDateString();
                        if (jobslist[0].JobEndDate.ToShortDateString() != "9999-12-31")
                        {
                            JobDateEnd.Text = jobslist[0].JobEndDate.ToShortDateString();
                        }
                    }
                

                JobContact.Text = jobslist[0].ContactName;
                JobName.Text = jobslist[0].JobName;
                JobAddress.Text = jobslist[0].Address;
                JobComments.Text = jobslist[0].Comments;

                FreeWeekOfRental();
		    }
		    catch
		    {
		        
		        
		    }
			
		}

	}

    protected void GetDate(DateTime firstDayofWeek, List<JobList> jobLists)
    {
        
        List<Job> jobs = new List<Job>();
        for (int i = 0; i < 5; i++)
        {
            JobList jobList = new JobList(firstDayofWeek.AddDays(i), jobs);


            jobLists.Add(jobList);
        }


        return;

    }

	protected void ButtonSend_Save(object sender, EventArgs e)
    {
        
        List<Job> job = new List<Job>();
	    List<Car> jobcars = new List<Car>();
        List<Person> personInJob = new List<Person>();
        var jobslist = SiteUtilities.GetJobsByJobId(new Guid(Request.QueryString["JobID"]));
     
        foreach (string personIDString in c_hiddenSelectedItems.Value.Split(new Char[] { DELIMITER_LIST_BOX_SELECTION }))
        {
            if (personIDString.Length > 15)
            {
               personInJob.Add(SiteUtilities.GetPersonByID(new Guid(personIDString)));
            }

        }
	   
        double starttime = 0;
        double endtime = 0;
	    bool fullday = false;
        starttime = Convert.ToDouble(StartTime.SelectedItem.Text.Substring(0, 2));
        endtime = Convert.ToDouble(EndTime.SelectedItem.Text.Substring(0, 2));

	    DateTime startJobTime = Convert.ToDateTime(StartTime.SelectedItem.Text);
        DateTime endJobTime = Convert.ToDateTime(EndTime.SelectedItem.Text);

        if (StartTime.SelectedItem.Text.Substring(3) == "30")
        {
            starttime = starttime + 0.5;
        }
        if (EndTime.SelectedItem.Text.Substring(3) == "30")
        {
            endtime = endtime + 0.5;
        }

        if (endtime - starttime >= 9)
        {
            fullday = true;

        }

        int RentalDays = Convert.ToInt16(FreeWeeksOfRental.SelectedItem.Value) * 7;
        DateTime tmp = Convert.ToDateTime(JobDateEnd.Text);
        TimeSpan timeSpan = new TimeSpan(RentalDays, 0, 0, 0);

        DateTime RentalDateEnd = tmp.Add((timeSpan));

	    try
	    {
            DateTime JobStart = Convert.ToDateTime(JobDate.Text);
            DateTime JobEnd = Convert.ToDateTime(JobDateEnd.Text);

            if (JobStart != JobEnd)
            {
                if (jobslist[0].Type == SiteUtilities.IMPORT)
                {
                    jobslist[0].Type = SiteUtilities.CONSTRUCTION;
                }

                int days = JobEnd.DayOfYear - JobStart.DayOfYear;

                for (int i = 0; i < days; i++)
                {
                    DateTime JobDateStart = Convert.ToDateTime(JobDate.Text);

                    DateTime newdadate = JobDateStart.AddDays(i + 1);
                    var tmpjobs = SiteUtilities.GetJobsByDate(newdadate);
                    if (tmpjobs.Count == 0)
                    {
                        Job extra = new Job(Guid.NewGuid(), JobName.Text, true, jobslist[0].OrderID, JobDateStart.AddDays(i + 1), JobDateStart.AddDays(i + 1), JobContact.Text, JobAddress.Text, JobComments.Text, Convert.ToDateTime(JobDateEnd.Text), RentalDateEnd, startJobTime, endJobTime, string.Empty, fullday, false, jobslist[0].JobId, string.Empty, JobContactPhone.Text, jobslist[0].Type, string.Empty);
                        job.Add(extra);
                    }
                    else if (tmpjobs.Count > 0 && !JobNewDatePanel.Visible)
                    {
                        Job extra = new Job(Guid.NewGuid(), JobName.Text, true, jobslist[0].OrderID, JobDateStart.AddDays(i + 1), JobDateStart.AddDays(i + 1), JobContact.Text, JobAddress.Text, JobComments.Text, Convert.ToDateTime(JobDateEnd.Text), RentalDateEnd, startJobTime, endJobTime, string.Empty, fullday, false, jobslist[0].JobId, string.Empty, JobContactPhone.Text, jobslist[0].Type, string.Empty);
                        job.Add(extra);
                    }
                  
                }
            }

            if (job.Count > 0)
            {
                DateTime JobDateStart = Convert.ToDateTime(JobDate.Text);
                Job currentjob = new Job(new Guid(Request.QueryString["JobID"]), JobName.Text, true, jobslist[0].OrderID, JobDateStart, Convert.ToDateTime(JobDateEnd.Text), JobContact.Text, JobAddress.Text, JobComments.Text, Convert.ToDateTime(JobDateEnd.Text), RentalDateEnd, startJobTime, endJobTime, string.Empty, fullday, false, Guid.Empty, string.Empty, JobContactPhone.Text, jobslist[0].Type, JobDateStart.Month.ToString());
                job.Add(currentjob);
            }
            else
            {

                if (JobDateEnd.Visible && JobDate.Visible)
                {
                    DateTime JobDateStart = Convert.ToDateTime(JobDate.Text);
                    Job currentjob = new Job(new Guid(Request.QueryString["JobID"]), JobName.Text, true, jobslist[0].OrderID, JobDateStart, Convert.ToDateTime(JobDateEnd.Text), JobContact.Text, JobAddress.Text, JobComments.Text, Convert.ToDateTime(JobDateEnd.Text), RentalDateEnd, startJobTime, endJobTime, string.Empty, fullday, false, Guid.Empty, string.Empty, JobContactPhone.Text, jobslist[0].Type, JobDateStart.Month.ToString());
                    job.Add(currentjob);
                }
                else
                {
                    Job currentjob = new Job(new Guid(Request.QueryString["JobID"]), JobName.Text, true, jobslist[0].OrderID, jobslist[0].JobStartDate, jobslist[0].JobEndDate, JobContact.Text, JobAddress.Text, JobComments.Text, jobslist[0].RentStartDate, jobslist[0].RentEndDate, startJobTime, endJobTime, string.Empty, fullday, false, jobslist[0].ParentJobID, string.Empty, JobContactPhone.Text, jobslist[0].Type, string.Empty);
                    job.Add(currentjob);
                }
            }
        
	    }
	    catch
	    {
	        
	    }
        for (int i = 0; i < CarsCheckBoxList.Items.Count; i++)
        {
            if (CarsCheckBoxList.Items[i].Selected)
            {
                Guid carid = new Guid(CarsCheckBoxList.Items[i].Value);

                Car car = new Car(carid, new Guid(Request.QueryString["JobID"]), CarsCheckBoxList.Items[i].Text, string.Empty);
                
                jobcars.Add(car);
            }
        }

        if (JobNewDatePanel.Visible)
        {
            DateTime JobStart = Convert.ToDateTime(JobNewDate.Text);

            SiteUtilities.JobComplete(new Guid(Request.QueryString["JobID"]), JobStart);
            SiteUtilities.DeleteOldSession(job, Session);
            Response.Redirect(Request.Url.ToString());
        }
        else
        {
            SiteUtilities.DeleteOldSession(job, Session);
            SiteUtilities.UpdateOrCreateJob(new Guid(Request.QueryString["JobID"]), job, personInJob, jobcars);

            Response.Redirect(Request.Url.ToString());
        }
	}


	protected void NextWeekLink(object sender, EventArgs e)
	{

	    DateTime NextWeekFirstDay = FirstDayofWeek.AddDays(7);
        string url;
        if (Session["CurrentWeekFirstDay"] != null)
		{
            Session.Add("CurrentWeekFirstDay", NextWeekFirstDay);
		}
		else
        {
            Session["CurrentWeekFirstDay"] = NextWeekFirstDay;
        }
		Response.Redirect("http://" + Request.Url.Host + "/jobs.aspx", false);

	}

	protected void PreviousWeekLink(object sender, EventArgs e)
	{
	    DateTime PreviousWeekFirstDay = FirstDayofWeek.AddDays(-7);
        string url;
        if (Session["CurrentWeekFirstDay"] != null)
        {
            Session.Add("CurrentWeekFirstDay", PreviousWeekFirstDay);
        }
        else
        {
            Session["CurrentWeekFirstDay"] = PreviousWeekFirstDay;
        }
        Response.Redirect("http://" + Request.Url.Host + "/jobs.aspx", false);
	}
    protected void Job(object sender, EventArgs e)
    {
        if (Request.QueryString["JobID"] == null)
        {
            EditArea.Visible = false;
            return;
        }

        JobNewDate.Text = DateTime.Now.ToShortDateString();
        InitButtons();
        if (!Page.IsPostBack)
        {
            try
            {
                InitMembershipInfo(c_listBoxSelectedItems, c_listBoxExistingItems);
                if (Request.QueryString["JobID"] != null)
                {
                    string jobdatetime = "";
                    
                    TextBox textBox = sender as TextBox;
                    var jobslist = SiteUtilities.GetJobsByJobId(new Guid(Request.QueryString["JobID"]));

                    if (jobslist.Count > 0)
                    {
                        JobContactPhone.Text = jobslist[0].Phone;

                        if (jobslist[0].JobStartTime.Hour.ToString() != "23")
                        {
                            ListItem starttime =
                                StartTime.Items.FindByText(jobslist[0].JobStartTime.ToShortTimeString().Substring(0, 5)) as ListItem;
                            starttime.Selected = true;
                        }
                        else
                        {
                            StartTime.Items[0].Selected = true;
                        }


                        if (jobslist[0].JobEndTime.Hour.ToString() != "23")
                        {
                            ListItem endtime =
                                EndTime.Items.FindByText(jobslist[0].JobEndTime.ToShortTimeString().Substring(0, 5)) as ListItem;
                            endtime.Selected = true;
                        }
                        else
                        {
                            EndTime.Items[18].Selected = true;
                        }

                    }

                }
            }
            catch
            {
                
            }

            
        }
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

    protected void FreeWeekOfRental()
    {
        if (Request.QueryString["JobID"] != null)
        {
            string jobdatetime = "";

            var jobslist = SiteUtilities.GetJobsByJobId(new Guid(Request.QueryString["JobID"]));

            if (jobslist[0].ParentJobID != Guid.Empty)
            {
                Guid tmp = jobslist[0].ParentJobID;
                jobslist.Clear();
                jobslist = SiteUtilities.GetJobsByJobId(tmp);
            }


            if (jobslist[0].RentStartDate.ToShortDateString() != "9999-12-31" && jobslist[0].RentEndDate.ToShortDateString() != "9999-12-31")
            {

                TimeSpan dateTime = jobslist[0].RentEndDate - jobslist[0].RentStartDate;

                int weeks = Convert.ToInt32(dateTime.TotalDays) /7;
                

                ListItem freeweek = FreeWeeksOfRental.Items.FindByValue(weeks.ToString()) as ListItem;
                freeweek.Selected = true;
            }
            else
            {
            for (int i = 0; i < FreeWeeksOfRental.Items.Count; i++)
            {
                FreeWeeksOfRental.Items[i].Selected = false;
            }
            ListItem freeweek = FreeWeeksOfRental.Items.FindByText("4 veckor") as ListItem;
            freeweek.Selected = true;
            }

            if (FreeWeeksOfRental != null)
            {

                RentalEndDate.Text = jobslist[0].RentEndDate.ToShortDateString();
            }
        }
    }


    /// <summary>
    /// Inits all the buttons
    /// </summary>
    private void InitButtons()
    {
        #region -- InitButtons --
        c_buttonAdd.Attributes.Add("Value", "Lägg till");
        c_buttonAdd.Attributes.Add("OnClick", "javascript:MoveItem('" + c_listBoxExistingItems.ClientID + "','" + c_listBoxSelectedItems.ClientID + "');StoreSelectedItems('" + c_listBoxSelectedItems.ClientID + "', '" + c_hiddenSelectedItems.ClientID + "', '" + DELIMITER_LIST_BOX_SELECTION + "');");
        c_buttonRemove.Attributes.Add("Value", "Ta bort");
        c_buttonRemove.Attributes.Add("OnClick", "javascript:MoveItem('" + c_listBoxSelectedItems.ClientID + "','" + c_listBoxExistingItems.ClientID + "');StoreSelectedItems('" + c_listBoxSelectedItems.ClientID + "', '" + c_hiddenSelectedItems.ClientID + "', '" + DELIMITER_LIST_BOX_SELECTION + "');");
        #endregion
    }

    public void InitMembershipInfo(ListBox listBoxSelectedItems, ListBox listBoxExistingItems)
    {
        try
        {
            DataTable tableSelectedItems = new DataTable();
            DataTable tableExistingItems = new DataTable();
            DataRow dataRow;
            Hashtable hashSelected = new Hashtable();
            List<Person> personlist = new List<Person>();
            List<JobPersonList> persononJoblist = new List<JobPersonList>();
            Collection<Person> persons = new Collection<Person>();
            personlist = SiteUtilities.GetAllPersonal();

            if (Request.QueryString["JobID"] != null)
            {

               persononJoblist = SiteUtilities.GetJobPersonal(new Guid(Request.QueryString["JobID"]));

                if (personlist != null)
                {

                    if (personlist.Count > 0)
                    {
                        tableSelectedItems.Columns.Add(new DataColumn("Name", typeof(string)));
                        tableSelectedItems.Columns.Add(new DataColumn("Id", typeof(Guid)));
                        foreach (JobPersonList person in persononJoblist)
                        {
                            dataRow = tableSelectedItems.NewRow();
                            dataRow[0] = person.Firstname + " " + person.Lastname;
                            dataRow[1] = person.PersonId;
                            tableSelectedItems.Rows.Add(dataRow);
                            hashSelected.Add(person.PersonId, "isSelected");
                            c_hiddenSelectedItems.Value += person.PersonId.ToString() +
                                                           DELIMITER_LIST_BOX_SELECTION;
                        }
                        listBoxSelectedItems.DataTextField = "Name";
                        listBoxSelectedItems.DataValueField = "Id";
                        listBoxSelectedItems.DataSource = tableSelectedItems;
                        listBoxSelectedItems.DataBind();
                    }

                }




                // Existing Items, (all the persons that the job is not allready added to)
                tableExistingItems.Columns.Add(new DataColumn("Name", typeof(string)));
                tableExistingItems.Columns.Add(new DataColumn("Id", typeof(Guid)));
                foreach (Person person in personlist)
                {
                    AddPersons(tableExistingItems, hashSelected, person);
                }
                listBoxExistingItems.DataTextField = "Name";
                listBoxExistingItems.DataValueField = "Id";
                listBoxExistingItems.DataSource = tableExistingItems;
                listBoxExistingItems.DataBind();

            }
        }
        catch
        {
            
        }

        
    }

    private static void AddPersons(DataTable tableExistingItems, Hashtable hashSelected, Person person)
    {
        if (!hashSelected.ContainsKey(person.PersonId)) // Check hash if the user is one the members of the job 
        {
            DataRow dataRow = tableExistingItems.NewRow();
            dataRow[0] = person.Firstname + " " + person.Lastname;
            dataRow[1] = person.PersonId;
            tableExistingItems.Rows.Add(dataRow);
        }

    }

    protected void InitFinnished(object sender, EventArgs e)
    {
        CheckBox checkBox = sender as CheckBox;

        if (checkBox.Checked)
        {
            JobNewDatePanel.Visible = true;
        }
        else
        {
            JobNewDatePanel.Visible = false;
        }
    }

    protected void InitJob(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Job job = e.Item.DataItem as Job;

            if (job != null)
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
                    var width = e.Item.FindControl("width") as HtmlGenericControl;
                    
                    width.Attributes["Style"] = "width:" + 100 + "%;overflow:hidden;border:none;height:235px;background-color:" + color + ";";
                    
                }

            }
        }
    
}
