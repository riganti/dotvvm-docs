using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;


namespace DotvvmWeb.Views.Docs.Controls.businesspack.GridView.sample6
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool IsEditing { get; set; }
        public BusinessPackDataSet<Customer> Customers { get; set; }

        public override Task Init()
        {
            Customers = new BusinessPackDataSet<Customer> 
            {
                OnLoadingData = GetData,
                RowEditOptions = new RowEditOptions {
                    PrimaryKeyPropertyName = nameof(Customer.Id),
                    EditRowId = -1
                },
                SortingOptions =
                {
                    SortExpression = nameof(Customer.Id)
                }
            };

            return base.Init();
        }

        public void EditCustomer(Customer customer)
        {
            Customers.RowEditOptions.EditRowId = customer.Id;
            IsEditing = true;
        }

        public void UpdateCustomer(Customer customer)
        {
            // Submit customer changes to your database..
            CancelEdit();
        }

        private void CancelEdit()
        {
            Customers.RowEditOptions.EditRowId = -1;
            IsEditing = false;
        }

        public void CancelEditCustomer()
        {
            CancelEdit();
            Customers.RequestRefresh(true);
        }

        public GridViewDataSetLoadedData<Customer> GetData(IGridViewDataSetLoadOptions gridViewDataSetOptions)
        {
            var queryable = GetQueryable(15);
            return queryable.GetDataFromQueryable(gridViewDataSetOptions);
        }

        private IQueryable<Customer> GetQueryable(int size)
        {
            var numbers = new List<Customer>();
            for (var i = 0; i < size; i++)
            {
                numbers.Add(new Customer { Id = i + 1, Name = $"Customer {i + 1}", BirthDate = DateTime.Now.AddYears(-i), Orders = i });
            }
            return numbers.AsQueryable();
        }
    }
}