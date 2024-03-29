﻿public class ViewModel : DotvvmViewModelBase
{
    public int IndexAdvanced { get; set; } = 1;
    public List<AccordionModel> Accordions { get; set; }
    public ViewModel()
    {
        Accordions = new List<AccordionModel>()
        {
            new AccordionModel()
            {
                Header = "Item 1",
                Text = "Text 1"
            },
            new AccordionModel()
            {
                Header = "Item 2",
                Text = "Text 2"
            }
        };
    }
    public void AddItem()
    {
        var itemNumber = Accordions.Count + 1;
        Accordions.Add(
            new AccordionModel()
            {
                Header = $"Item {itemNumber}",
                Text = $"Text  {itemNumber}"
            });
        IndexAdvanced = Accordions.Count - 1;
    }

    public class AccordionModel
    {
        public string Header { get; set; }
        public string Text { get; set; }
    }
}