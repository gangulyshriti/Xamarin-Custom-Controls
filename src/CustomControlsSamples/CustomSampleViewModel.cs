﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace CustomControlsSamples
{
    public class RandomObject
    {
        public string RandomProperty1 { get; set; }
        public string RandomProperty2 { get; set; }
        public string RandomProperty3 { get; set; }
        public string RandomProperty4 { get; set; }
    }

    public class CustomSampleViewModel : BaseViewModel
    {
        private int _loadingCount = 1;

        private ICommand _selectedItemCommand;
        private ICommand _reloadItemCommand;

        public ICommand SelectedItemCommand => _selectedItemCommand ?? (_selectedItemCommand = new Command((selectedItem) => Select((RandomObject)selectedItem)));
        public ICommand ReloadItemCommand => _reloadItemCommand ?? (_reloadItemCommand = new Command(async () => await LoadData(true)));

        public ObservableRangeCollection<RandomObject> Items { get; } = new ObservableRangeCollection<RandomObject>();

        public string SelectedValue { get; set; } = "No selection";

        public CustomSampleViewModel()
        {

        }

        public async Task LoadData(bool isReloading = false)
        {
            IsBusy = true;

            if (isReloading)
                _loadingCount++;

            try
            {
                var items = new List<RandomObject>
            {
                new RandomObject{ RandomProperty1=$"randomValue{_loadingCount}1", RandomProperty2="randomValue12", RandomProperty3 = "randomValue13",  RandomProperty4 = "randomValue14"},
                new RandomObject{ RandomProperty1=$"randomValue{_loadingCount+1}1", RandomProperty2="randomValue22", RandomProperty3 = "randomValue23",  RandomProperty4 = "randomValue24"},
                new RandomObject{ RandomProperty1=$"randomValue{_loadingCount+2}1", RandomProperty2="randomValue32", RandomProperty3 = "randomValue33",  RandomProperty4 = "randomValue34"},
                new RandomObject{ RandomProperty1=$"randomValue{_loadingCount+3}1", RandomProperty2="randomValue42", RandomProperty3 = "randomValue43",  RandomProperty4 = "randomValue44"},
            };

                await Task.Delay(5000);

                Items.ReplaceRange(items);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Select(RandomObject selectedItem)
        {
            SelectedValue = $"Selected item {Items.IndexOf(selectedItem)}: value1 = {selectedItem.RandomProperty1}, value2 = {selectedItem.RandomProperty2}, etc...";

            OnPropertyChanged(nameof(SelectedValue));
        }
    }
}
