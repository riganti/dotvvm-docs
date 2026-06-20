using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.builtin.EmptyData.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }


    public record Customer(int Id, string Name);
}
