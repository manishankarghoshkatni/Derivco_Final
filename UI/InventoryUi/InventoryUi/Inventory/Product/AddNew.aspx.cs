using System;
using System.Web.UI.WebControls;
using InventoryUi.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;
using System.Data;

namespace InventoryUi.Inventory.Product
{
    public partial class AddNew : System.Web.UI.Page
    {
        void LoadCategories(string categoryName = "")
        {
            ApiResponse response = Helper.GetCategories(categoryName);

            try
            {

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Models.Category[] data = JsonConvert.DeserializeObject<InventoryUi.Models.Category[]>(response.data.ToString());
                    using (DataTable dt = Helper.CreateDataTable(data, ""))
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
                    using (DataTable dt = Helper.CreateDataTable(data, ""))
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
        protected void Page_Load(object sender, EventArgs e)
        {
            LblErrorMsg.Visible = false;
            LblMsg.Visible = false;

            if (!IsPostBack)
            {
                this.LoadCategories("");
                this.LoadUnits("");
                this.LoadCurrencies();
            }

            TxtPrice.Attributes.Add("onkeypress", "return isNumberOrDotKey(event)");
            TxtPrice.Attributes.Add("onblur", "return RoundOffNumber(event, 2)");
        }

        protected void CmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal price = 0;
                if (!decimal.TryParse(TxtPrice.Text, out price))
                {
                    LblErrorMsg.Text = "Invalis Price value !";
                    LblErrorMsg.Visible = true;
                }
                else
                {
                    InventoryUi.Models.Product product = new InventoryUi.Models.Product();

                    product.ProductId = 0;
                    product.ProductName = TxtProductName.Text;
                    product.ProductDescription = TxtDescription.Text.Replace("\r\n", "<br />");

                    product.CategoryId = int.Parse(CboCategory.SelectedValue);
                    product.Price = price;
                    product.Currency = CboCurrency.SelectedValue;
                    product.UnitId = int.Parse(CboUnit.SelectedValue);

                    string json = JsonConvert.SerializeObject(product);

                    ApiResponse response = Helper.PostToApi("api/Products/Create", json);
                    if (response.responseCode == 0)
                    {
                        LblMsg.Text = "New product created with Product Id: " + response.data.ToString();
                        LblMsg.Visible = true;
                    }
                    else
                    {
                        LblErrorMsg.Text = "Server Error: " + response.error;
                        LblErrorMsg.Visible = true;
                    }
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