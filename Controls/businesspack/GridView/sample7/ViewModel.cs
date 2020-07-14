using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.GridView.sample7
{
    public class ViewModel : DotvvmViewModelBase
    {
        public string NewCustomerName { get; set; }
        public DateTime NewCustomerBirthday { get; set; } = DateTime.Now;
        public bool IsInserting { get; set; }
        public BusinessPackDataSet<Customer> Customers { get; set; } = new BusinessPackDataSet<Customer>
        {
            PagingOptions = {
                    PageSize = 10
                },
            RowEditOptions = new RowEditOptions
            {
                PrimaryKeyPropertyName = nameof(Customer.Id),
                EditRowId = -1
            }
        };

        public override Task PreRender()
        {
            if (Customers.IsRefreshRequired)
            {
                Customers.LoadFromQueryable(GetQueryable(15));
            }

            return base.PreRender();
        }

        public void InsertNewCustomer()
        {
            Customers.RowInsertOptions.InsertedRow = new Customer
            {
                Id = Customers.Items.Max(c => c.Id) + 1,
                Name = NewCustomerName,
                BirthDate = NewCustomerBirthday,
                Orders = 0
            };
            IsInserting = true;
        }

        public void CancelInsertNewCustomer()
        {
            Customers.RowInsertOptions.InsertedRow = null;
            IsInserting = false;
        }

        public void SaveNewCustomer()
        {
            // Save inserted item to database
            Customers.Items.Add(Customers.RowInsertOptions.InsertedRow);
            CancelInsertNewCustomer();
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
