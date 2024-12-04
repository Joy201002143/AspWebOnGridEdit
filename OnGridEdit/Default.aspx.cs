using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnGridEdit
{
    public partial class _Default : Page
    {
        private const string ConnectionString = "Server=DESKTOP-JHB8AON;Database=EmpLoyees;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DataTable"] = null;

                if (Session["DataTable"] == null)
                {
                    DataTable dt = CreateDataTable();
                    LoadDataFromDatabase(dt);
                    Session["DataTable"] = dt;
                    BindGrid(dt);
                }

                DataTable dts = (DataTable)Session["DataTable"];
                AddEmptyRow(dts);
                Session["DataTable"] = dts;
                BindGrid(dts);
            }
        }
        private DataTable GetCompanies()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT CompanyID, CompanyName FROM [dbo].[Companies]";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        private DataTable GetDepartments()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT DepartmentID, DepartmentName FROM [dbo].[Departments]";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        private void BindDropdowns(GridViewRow row)
        {
            // Bind Company Dropdown
            DropDownList ddlCompany = (DropDownList)row.FindControl("ddlCompany");
            ddlCompany.DataSource = GetCompanies();
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("Select Company", ""));

            // Bind Department Dropdown
            DropDownList ddlDepartment = (DropDownList)row.FindControl("ddlDepartment");
            ddlDepartment.DataSource = GetDepartments();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BindDropdowns(e.Row);

                // Pre-select dropdown values based on data
                DropDownList ddlCompany = (DropDownList)e.Row.FindControl("ddlCompany");
                DropDownList ddlDepartment = (DropDownList)e.Row.FindControl("ddlDepartment");

                if (ddlCompany != null && DataBinder.Eval(e.Row.DataItem, "CompanyID") != null)
                {
                    ddlCompany.SelectedValue = DataBinder.Eval(e.Row.DataItem, "CompanyID").ToString();
                }

                if (ddlDepartment != null && DataBinder.Eval(e.Row.DataItem, "DepartmentID") != null)
                {
                    ddlDepartment.SelectedValue = DataBinder.Eval(e.Row.DataItem, "DepartmentID").ToString();
                }
            }
        }
        
    }
}