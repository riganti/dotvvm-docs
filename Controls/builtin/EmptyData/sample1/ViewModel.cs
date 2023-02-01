using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using Newtonsoft.Json;

namespace DotvvmWeb.Views.Docs.Controls.builtin.EmptyData.sample1
{
    public class ViewModel : DotvvmViewModelBase
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }


    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConstructor]
        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
