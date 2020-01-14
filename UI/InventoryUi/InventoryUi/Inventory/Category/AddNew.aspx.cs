using System;
using InventoryUi.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;

namespace InventoryUi.Inventory.Category
{
    public partial class AddNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LblErrorMsg.Visible = false;
            LblMsg.Visible = false;
        }

        protected void CmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                InventoryUi.Models.Category category = new InventoryUi.Models.Category();
                category.CategoryName = TxtCategoryName.Text;
                category.CategoryDescription = TxtDescription.Text;
                string json = JsonConvert.SerializeObject(category);

                ApiResponse response = Helper.PostToApi("api/Categories/Create", json);
                if (response.responseCode == 0)
                {
                    LblMsg.Text = "New category created with Category Id: " + response.data;
                    LblMsg.Visible = true;
                }
                else
                {
                    LblErrorMsg.Text = "Server Error: " + response.error;
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