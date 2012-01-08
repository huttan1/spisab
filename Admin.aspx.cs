using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.App_Code;
using ProfIT;

public partial class Admin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void InitMamut(object sender, EventArgs e)
    {
        Button button = sender as Button;

        if (button.ID == "job")
        {
            var jobs = GetJobs();

            if (jobs == null)
            {
                errorLabel.Text = "Problem vid import";
                errorLabel.Visible = true;
            }
            else if (jobs.Count == 0)
            {
                errorLabel.Text = "Inga nya jobb fanns att hämta";
                errorLabel.Visible = true;
            }
            if (jobs != null && jobs.Count > 0)
            {
                List<Car> cars = new List<Car>();
                List<Person> persons = new List<Person>();
                SiteUtilities.UpdateOrCreateJob(Guid.Empty, jobs, persons, cars);

                count.Text = jobs.Count.ToString();
                repeater.DataSource = jobs;
                repeater.DataBind();
                ResultJob.Visible = true;
                errorLabel.Visible = false;
            }
        }
        else if (button.ID == "emp")
        {
            var persons = GetPersons();

            if (persons == null || persons.Count == 0)
            {
                errorLabel.Text = "Error with import";
                errorLabel.Visible = true;
            }
            else if (persons.Count > 0)
            {
                countperson.Text = persons.Count.ToString();
                repeaterperson.DataSource = persons;
                repeaterperson.DataBind();
                ResultPerson.Visible = true;
                errorLabel.Visible = false;
            }

        }
    }

    public List<Person> GetPersons()
    {
        List<Person> persons = new List<Person>();
        return Mamut.GetPersonsFromMamut(persons, Request);
    }

    private List<Job> GetJobs()
    {
        Exception exception = new Exception();
        return MamutV2.GetOrdersFromMamut(exception, Request);
        //return Mamut.GetOrdersFromMamut(exception, Request);
    }
}
