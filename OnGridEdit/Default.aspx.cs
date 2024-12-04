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
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EMPID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Company");
            dt.Columns.Add("Department");
            dt.Columns.Add("Designation");
            dt.Columns.Add("Salary");
            return dt;
        }

        private void LoadDataFromDatabase(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT EMPID, Name, Company, Department, Designation, Salary, CompanyID, DepartmentID   FROM [dbo].[Emp]";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
        }

        private void BindGrid(DataTable dt)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();


            if (GridView1.Rows.Count > 0)
            {

                GridViewRow newGridViewRow = GridView1.Rows[GridView1.Rows.Count - 1];
                CheckBox chkSelect = (CheckBox)newGridViewRow.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    chkSelect.Checked = true;
                }
            }
            else
            {


                if (dt != null)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    GridViewRow emptyRow = GridView1.Rows[GridView1.Rows.Count - 1];
                    CheckBox chkSelect = (CheckBox)emptyRow.FindControl("chkSelect");
                    if (chkSelect != null)
                    {
                        chkSelect.Checked = false;
                    }
                }
            }


        }

        private void AddEmptyRow(DataTable dt)
        {
            DataRow newRow = dt.NewRow();
            newRow["Name"] = string.Empty;
            newRow["Company"] = string.Empty;
            newRow["Department"] = string.Empty;
            newRow["Designation"] = string.Empty;
            newRow["Salary"] = string.Empty;
            dt.Rows.Add(newRow);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["DataTable"];

            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtName = (TextBox)row.FindControl("txtName");
                TextBox txtCname = (TextBox)row.FindControl("txtCname");
                TextBox txtDepartment = (TextBox)row.FindControl("txtDepartment");
                TextBox txtDesignation = (TextBox)row.FindControl("txtDesignation");
                TextBox txtSalary = (TextBox)row.FindControl("txtSalary");

                if (txtName != null && txtCname != null && txtDepartment != null && txtDesignation != null && txtSalary != null)
                {
                    dt.Rows[row.RowIndex]["Name"] = txtName.Text;
                    dt.Rows[row.RowIndex]["Company"] = txtCname.Text;
                    dt.Rows[row.RowIndex]["Department"] = txtDepartment.Text;
                    dt.Rows[row.RowIndex]["Designation"] = txtDesignation.Text;
                    dt.Rows[row.RowIndex]["Salary"] = txtSalary.Text;
                }
            }

            AddEmptyRow(dt);

            Session["DataTable"] = dt;
            BindGrid(dt);
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtID = (TextBox)row.FindControl("txtID");
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

                if (txtID != null && string.IsNullOrEmpty(txtID.Text) && chkSelect != null)
                {
                    chkSelect.Checked = true;
                }
            }
        }

    }
}