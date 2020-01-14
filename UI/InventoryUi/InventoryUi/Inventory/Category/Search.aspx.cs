using System;
using System.Web.UI.WebControls;
using InventoryUi.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;
using System.Data;

namespace InventoryUi.Inventory.Category
{
    public partial class Search : System.Web.UI.Page
    {
        void BindGrid(string categoryName = "")
        {

            DataTable dt = null;

            try
            {
                ApiResponse response = Helper.GetCategories(categoryName);

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Models.Category[] data = JsonConvert.DeserializeObject<InventoryUi.Models.Category[]>(response.data.ToString());
                    dt = Helper.CreateDataTable(data);
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtId.Text = "";
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtId.Text = "";
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }

            CategoryGridView.DataSource = dt;
            CategoryGridView.DataBind();
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            LblErrorMsg.Visible = false;
            LblMsg.Visible = false;

            if (!IsPostBack)
            {
                this.BindGrid();
            }

            TxtId.Attributes.Add("onkeypress", "return isNumberKey(event)");
        }

        protected void CmdSearchById_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtId.Text == string.Empty)
                {
                    this.BindGrid(""); //Show all Categories when Id field is empty
                }
                else {
                    int id = 0;
                    if (!int.TryParse(TxtId.Text, out id))
                    {
                        LblErrorMsg.Text = "Please enter proper Id to search";
                        LblErrorMsg.Visible = true;
                    }
                    else
                    {
                        ApiResponse response = Helper.GetApiResponse("api/Categories/" + id.ToString());
                        InventoryUi.Models.Category data = JsonConvert.DeserializeObject<InventoryUi.Models.Category>(response.data.ToString());
                        if (response.responseCode == ApiResponse.Success)
                        {
                            TxtCategoryName.Text = data.CategoryName;
                            TxtDescription.Text = data.CategoryDescription;

                            InventoryUi.Models.Category[] arCategory = new InventoryUi.Models.Category[1];
                            arCategory[0] = data;
                            DataTable dt = Helper.CreateDataTable(arCategory);
                            CategoryGridView.DataSource = dt;
                            CategoryGridView.DataBind();
                        }
                        else if (response.responseCode == ApiResponse.NoDataFound)
                        {
                            TxtCategoryName.Text = "";
                            TxtDescription.Text = "";
                            LblErrorMsg.Text = "No Data Found";
                            LblErrorMsg.Visible = true;
                        }
                        else if (response.responseCode == ApiResponse.Exception)
                        {
                            TxtCategoryName.Text = "";
                            TxtDescription.Text = "";
                            LblErrorMsg.Text = "Api Error: " + response.error;
                            LblErrorMsg.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TxtCategoryName.Text = "";
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        protected void CmdSearchByName_Click(object sender, EventArgs e)
        {
            this.BindGrid(TxtCategoryName.Text);
        }

        protected void CategoryGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int CategoryId = Convert.ToInt32(CategoryGridView.DataKeys[e.NewEditIndex].Value.ToString());
            Response.Redirect("/Inventory/Category/Modify?categoryId=" + CategoryId.ToString());
        }

        protected void CategoryGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                int CategoryId = Convert.ToInt32(CategoryGridView.DataKeys[e.RowIndex].Value.ToString());

                string actionUrl = "api/Categories/Delete";

                ApiResponse response = Helper.DeleteRequestToApi(actionUrl, CategoryId);

                if (response.responseCode == ApiResponse.Success)
                {
                    LblMsg.Text = response.data.ToString();
                    LblMsg.Visible = true;
                    this.BindGrid();
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }
    }
}