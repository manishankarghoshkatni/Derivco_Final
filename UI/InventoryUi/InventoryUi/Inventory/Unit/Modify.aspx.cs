using System;
using InventoryUi.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;


namespace InventoryUi.Inventory.Unit
{
    public partial class Modify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LblErrorMsg.Visible = false;
            LblMsg.Visible = false;
            if (!IsPostBack)
            {
                if (Request["unitId"] == null) { Response.Redirect("/Inventory/Unit/Search"); }
                else
                {
                    int unitId = 0;

                    if (int.TryParse(Request["unitId"], out unitId))
                    {
                        TxtId.Text = unitId.ToString();
                        this.SearchByUnitId(unitId);
                    }
                }
            }
        }

        private void SearchByUnitId(int id)
        {
            try
            {
                ApiResponse response = Helper.GetApiResponse("api/Units/" + id.ToString());
                InventoryUi.Models.Unit data = JsonConvert.DeserializeObject<InventoryUi.Models.Unit>(response.data.ToString());
                if (response.responseCode == ApiResponse.Success)
                {
                    TxtUnitName.Text = data.UnitName;
                    TxtDescription.Text = data.UnitDescription;
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtUnitName.Text = "";
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtUnitName.Text = "";
                    TxtDescription.Text = "";
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtUnitName.Text = "";
                TxtDescription.Text = "";
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }
        protected void CmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                int unitId = 0;

                if (int.TryParse(TxtId.Text, out unitId))
                {
                    InventoryUi.Models.Unit unit = new InventoryUi.Models.Unit();
                    unit.UnitId = unitId;
                    unit.UnitName = TxtUnitName.Text;
                    unit.UnitDescription = TxtDescription.Text;
                    string json = JsonConvert.SerializeObject(unit);

                    ApiResponse response = Helper.PutToApi("api/Units/Modify", json);
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