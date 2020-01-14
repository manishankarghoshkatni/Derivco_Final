using System;
using InventoryUi.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;

namespace InventoryUi.Inventory.Unit
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
                InventoryUi.Models.Unit Unit = new InventoryUi.Models.Unit();
                Unit.UnitName = TxtUnitName.Text;
                Unit.UnitDescription = TxtDescription.Text;
                string json = JsonConvert.SerializeObject(Unit);

                ApiResponse response = Helper.PostToApi("api/Units/Create", json);
                if (response.responseCode == 0)
                {
                    LblMsg.Text = "New Unit created with Unit Id: " + response.data;
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