using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;


namespace DotvvmWeb.Views.Docs.Controls.businesspack.GridView.sample10
{
    public class ViewModel : DotvvmViewModelBase
    {
        public GridViewUserSettings UserSettings { get; set; }
        public BusinessPackDataSet<Customer> Customers { get; set; }

        public override Task Init()
        {
            Customers = new BusinessPackDataSet<Customer> {
                OnLoadingData = GetData,
                SortingOptions =
                {
                    SortExpression = nameof(Customer.Id)
                }
            };

            UserSettings = new GridViewUserSettings {
                EnableUserSettings = true,
                ColumnsSettings = new List<GridViewColumnSetting> {
                    new GridViewColumnSetting {
                        ColumnName = "CustomerId",
                        DisplayOrder = 0,
                        ColumnWidth = 50
                    },
                    new GridViewColumnSetting {
                        ColumnName = "CustomerName",
                        DisplayOrder = 1,
                        ColumnWidth = 400
                    },
                    new GridViewColumnSetting {
                        ColumnName = "CustomerBirthdate",
                        DisplayOrder = 2
                    },
                    new GridViewColumnSetting {
                        ColumnName = "CustomerOrders",
                        DisplayOrder = 3
                    }
                }
            };

            return base.Init();
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