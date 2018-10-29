using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;


namespace DotvvmWeb.Views.Docs.Controls.businesspack.GridView.sample2
{
    public class ViewModel : DotvvmViewModelBase
    {
        public bool ShowHeaderWhenNoData { get; set; }
        public BusinessPackDataSet<Customer> Customers { get; set; }

        public override Task Init()
        {
            Customers = new BusinessPackDataSet<Customer> 
            {
                OnLoadingData = GetData,
                SortingOptions =
                {
                    SortExpression = nameof(Customer.Id)
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
                numbers.Add(new Customer { Id = i + 1, Name = $"Customer {i + 1}" });
            }
            return numbers.AsQueryable();
        }
    }
}