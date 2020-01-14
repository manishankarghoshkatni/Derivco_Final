using System;
using System.Web.UI.WebControls;
using InventoryUi.Models;
using Newtonsoft.Json;
using InventoryUi.Shared;
using System.Data;

namespace InventoryUi.Inventory.Product
{
    public partial class Search : System.Web.UI.Page
    {
        private ApiResponse GetProductByName(string productName = "")
        {
            ApiResponse response = null;
            try
            {
                string actionUrl = "api/Products";
                productName = productName.Trim();
                if (productName.Length > 0)
                {
                    actionUrl += "/" + productName;
                }

                response = Helper.GetApiResponse(actionUrl);

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Models.Product[] data = JsonConvert.DeserializeObject<InventoryUi.Models.Product[]>(response.data.ToString());
                    response.data = Helper.CreateDataTable(data);
                }
            }
            catch(Exception ex)
            {
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }

            return response;
        }

        private ApiResponse GetProductByCategoryId(int categoryId)
        {
            ApiResponse response = null;
            try
            {
                string actionUrl = "api/Products/Category/" + categoryId.ToString();

                response = Helper.GetApiResponse(actionUrl);

                if (response.responseCode == ApiResponse.Success)
                {
                    InventoryUi.Models.Product[] data = JsonConvert.DeserializeObject<InventoryUi.Models.Product[]>(response.data.ToString());
                    response.data = Helper.CreateDataTable(data);
                }
            }
            catch (Exception ex)
            {
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }

            return response;
        }

        void BindGrid(ApiResponse response)
        {

            DataTable dt = null;

            try
            {

                if (response.responseCode == ApiResponse.Success)
                {
                    dt = response.data as DataTable;
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }

            ProductGridView.DataSource = dt;
            ProductGridView.DataBind();

        }

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
            this.BindGrid(this.GetProductByName(""));
            TxtId.Attributes.Add("onkeypress", "return isNumberKey(event)");
        }

        private void ClearFields()
        {
            TxtId.Text = string.Empty;
            TxtProductName.Text = string.Empty;
            TxtDescription.Text = string.Empty;
            CboCategory.SelectedIndex = -1;
            CboUnit.SelectedIndex = -1;
            CboCurrency.SelectedIndex = -1;
            TxtPrice.Text = string.Empty;
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

                    InventoryUi.Models.Product[] arProduct = new InventoryUi.Models.Product[1];
                    arProduct[0] = data;
                    response.data = Helper.CreateDataTable(arProduct);
                    this.BindGrid(response);
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    this.ClearFields();
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    this.ClearFields();
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                this.ClearFields();
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        protected void CmdSearchById_Click(object sender, EventArgs e)
        {
            if (TxtId.Text == string.Empty)
            {
                this.BindGrid(this.GetProductByName("")); // Show all products when id is empty
            }
            else
            {
                int id = 0;
                if (!int.TryParse(TxtId.Text, out id))
                {
                    LblErrorMsg.Text = "Please enter proper Id to search";
                    LblErrorMsg.Visible = true;
                }
                else
                {
                    this.SearchById(id);
                    TxtId.Text = id.ToString();
                }
            }
        }

        protected void ProductGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int productId = Convert.ToInt32(ProductGridView.DataKeys[e.NewEditIndex].Value.ToString());
            Response.Redirect("/Inventory/Product/Modify?productId=" + productId.ToString());
        }

        protected void ProductGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 2)
            {
                e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:200px");
            }
        }

        protected void ProductGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int productId = Convert.ToInt32(ProductGridView.DataKeys[e.RowIndex].Value.ToString());

                string actionUrl = "api/Products/Delete";

                ApiResponse response = Helper.DeleteRequestToApi(actionUrl, productId);

                if (response.responseCode == ApiResponse.Success)
                {
                    LblMsg.Text = response.data.ToString();
                    LblMsg.Visible = true;
                    this.BindGrid(this.GetProductByName(""));
                }
                else if (response.responseCode == ApiResponse.NoDataFound)
                {
                    this.ClearFields();
                    LblErrorMsg.Text = "No Data Found";
                    LblErrorMsg.Visible = true;
                }
                else if (response.responseCode == ApiResponse.Exception)
                {
                    this.ClearFields();
                    LblErrorMsg.Text = "Api Error: " + response.error;
                    LblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                this.ClearFields();
                LblErrorMsg.Text = "Page Error: " + ex.Message;
                LblErrorMsg.Visible = true;
            }
        }

        private void ShowForProductName(string productName)
        {
            this.BindGrid(this.GetProductByName(productName));
            this.ClearFields();
            TxtProductName.Text = productName;
        }
        protected void CmdSearchByName_Click(object sender, EventArgs e)
        {
            this.ShowForProductName(TxtProductName.Text);
        }

        protected void CmdSearchByCategory_Click(object sender, EventArgs e)
        {
            int categoryId = int.Parse(CboCategory.SelectedValue);
            if (categoryId == 0)
            {
                this.BindGrid(this.GetProductByName("")); // if no category selected, show all products
            }
            else
            {
                this.BindGrid(this.GetProductByCategoryId(int.Parse(CboCategory.SelectedValue)));
            }
            this.ClearFields();
            CboCategory.SelectedValue = categoryId.ToString();
        }

        protected void ProductGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(e.NewPageIndex > -1)
            {
                ProductGridView.PageIndex = e.NewPageIndex;
                ProductGridView.DataBind();
            }         
        }

        protected void CmdShowAll_Click(object sender, EventArgs e)
        {
            this.ShowForProductName("");
        }
    }
}