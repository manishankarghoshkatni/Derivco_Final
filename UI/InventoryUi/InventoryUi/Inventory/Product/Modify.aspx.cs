using System;
using System.Web.UI.WebControls;
using InventoryUi.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;
using System.Data;

namespace InventoryUi.Inventory.Product
{
    public partial class Modify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LblErrorMsg.Visible = false;
            LblMsg.Visible = false;
            if (!IsPostBack)
            {
                if (Request["productId"] == null)
                {
                    Response.Redirect("/Inventory/Product/Search");
                }
                else 
                {
                    LblErrorMsg.Visible = false;
                    LblMsg.Visible = false;

                    this.LoadCategories("");
                    this.LoadUnits("");
                    this.LoadCurrencies();

                    int productId = 0;

                    if (int.TryParse(Request["productId"], out productId))
                    {
                        TxtId.Text = productId.ToString();
                        this.SearchById(productId);
                    }
                }
                TxtPrice.Attributes.Add("onkeypress", "return isNumberOrDotKey(event)");
                TxtPrice.Attributes.Add("onblur", "return RoundOffNumber(event, 2)");
            }
        }

        void LoadCategories(string categoryName = "")
        {
            ApiResponse response = Helper.GetCategories(categoryName);

            try
            {

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Models.Category[] data = JsonConvert.DeserializeObject<InventoryUi.Models.Category[]>(response.data.ToString());
                    using (DataTable dt = Helper.CreateDataTable(data, "Select"))
                    {
                        CboCategory.DataSource = dt;
                        CboCategory.DataTextField = "CategoryName";
                        CboCategory.DataValueField = "CategoryId";
                        CboCategory.DataBind();
                    }
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtDescription.Text = string.Empty;
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtDescription.Text = string.Empty;
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtDescription.Text = string.Empty;
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        void LoadUnits(string unitName = "")
        {
            ApiResponse response = Helper.GetUnits(unitName);

            try
            {
                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Models.Unit[] data = JsonConvert.DeserializeObject<InventoryUi.Models.Unit[]>(response.data.ToString());
                    using (DataTable dt = Helper.CreateDataTable(data, "Select"))
                    {
                        CboUnit.DataSource = dt;
                        CboUnit.DataTextField = "UnitName";
                        CboUnit.DataValueField = "UnitId";
                        CboUnit.DataBind();
                    }
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtDescription.Text = string.Empty;
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtDescription.Text = string.Empty;
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtDescription.Text = string.Empty;
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        private void LoadCurrencies()
        {
            string[] currencies = Helper.GetCurrencies();
            if (currencies != null)
            {
                foreach (string currency in currencies)
                {
                    CboCurrency.Items.Add(new ListItem(currency, currency));
                }
            }
        }

        private void SearchById(int id)
        {
            try
            {
                ApiResponse response = Helper.GetApiResponse("api/Products/" + id.ToString());
                InventoryUi.Models.Product data = JsonConvert.DeserializeObject<InventoryUi.Models.Product>(response.data.ToString());
                if (response.responseCode == ApiResponse.Success)
                {
                    TxtProductName.Text = data.ProductName;
                    TxtDescription.Text = data.ProductDescription.Replace("<br />", "\r\n");
                    CboCategory.SelectedValue = data.CategoryId.ToString();
                    CboUnit.SelectedValue = data.UnitId.ToString();
                    TxtPrice.Text = data.Price.ToString();
                    CboCurrency.SelectedValue = data.Currency;
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    TxtProductName.Text = string.Empty;
                    TxtDescription.Text = string.Empty;
                    CboCategory.SelectedIndex = -1;
                    CboUnit.SelectedIndex = -1;
                    CboCurrency.SelectedIndex = -1;
                    TxtPrice.Text = string.Empty;
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    TxtProductName.Text = string.Empty;
                    TxtDescription.Text = string.Empty;
                    CboCategory.SelectedIndex = -1;
                    CboUnit.SelectedIndex = -1;
                    CboCurrency.SelectedIndex = -1;
                    TxtPrice.Text = string.Empty;
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                TxtProductName.Text = string.Empty;
                TxtDescription.Text = string.Empty;
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        protected void CmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                int productId = 0; decimal price = 0;
                if(!decimal.TryParse(TxtPrice.Text, out price))
                {
                    LblErrorMsg.Text = "Invalis Price value !";
                    LblErrorMsg.Visible = true;
                }
                else if (int.TryParse(TxtId.Text, out productId))
                {
                    InventoryUi.Models.Product product = new InventoryUi.Models.Product();

                    product.ProductId = productId;
                    product.ProductName = TxtProductName.Text;
                    product.ProductDescription = TxtDescription.Text.Replace("\r\n", "<br />");

                    product.CategoryId = int.Parse(CboCategory.SelectedValue);
                    product.Price = price;
                    product.Currency = CboCurrency.SelectedValue;
                    product.UnitId = int.Parse(CboUnit.SelectedValue);

                    string json = JsonConvert.SerializeObject(product);

                    ApiResponse response = Helper.PutToApi("api/Products/Modify", json);
                    if (response.responseCode == 0)
                    {
                        LblMsg.Text = "Changes saved";
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