using System;
using InventoryUi.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;

namespace InventoryUi.Inventory.Category
{
    public partial class Modify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LblErrorMsg.Visible = false;
            LblMsg.Visible = false;
            if (!IsPostBack)
            {
                if (Request["categoryId"] == null) { Response.Redirect("/Inventory/Category/Search"); }
                else
                {
                    int categoryId = 0;

                    if (int.TryParse(Request["categoryId"], out categoryId))
                    {
                        TxtId.Text = categoryId.ToString();
                        this.SearchByCategoryId(categoryId);
                    }
                }
            }
        }

        private void SearchByCategoryId(int id)
        {
            try
            {
                ApiResponse response = Helper.GetApiResponse("api/Categories/" + id.ToString());
                InventoryUi.Models.Category data = JsonConvert.DeserializeObject<InventoryUi.Models.Category>(response.data.ToString());
                if (response.responseCode == ApiResponse.Success)
                {
                    TxtCategoryName.Text = data.CategoryName;
                    TxtDescription.Text = data.CategoryDescription;
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
            catch (Exception ex)
            {
                TxtCategoryName.Text = "";
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }
        protected void CmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                int categoryId = 0;

                if (int.TryParse(TxtId.Text, out categoryId))
                {
                    InventoryUi.Models.Category category = new InventoryUi.Models.Category();
                    category.CategoryId = categoryId;
                    category.CategoryName = TxtCategoryName.Text;
                    category.CategoryDescription = TxtDescription.Text;
                    string json = JsonConvert.SerializeObject(category);

                    ApiResponse response = Helper.PutToApi("api/Categories/Modify", json);
                    if (response.responseCode == 0)
                    {
                        LblMsg.Text = "Changed saved";
                        LblMsg.Visible = true;
                    }
                    else
                    {
                        LblErrorMsg.Text = "Server Error: " + response.error;
                        LblErrorMsg.Visible = true;
                    }
                }
                else
                {
                    LblErrorMsg.Text = "Please enter proper Id to search";
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