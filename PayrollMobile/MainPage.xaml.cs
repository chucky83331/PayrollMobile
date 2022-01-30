using System;
using Xamarin.Forms;

namespace PayrollMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        string shiftType;
        string shiftTime;
        string currentItem;
        decimal rate1 = 25.83m;
        decimal rate2 = 38.745m;
        decimal rate3 = 51.66m;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            CollectionView.ItemsSource = await App.Database.GetShiftAsync();
        }

        // Add To Database
        async void OnButtonClicked(object sender, EventArgs e)
        {
            DateTime? date = datePicker.Date;
            if (date == null)
            {
                this.Title = "No date";
            }
            else
            {
                this.Title = date.Value.ToShortDateString();
            }

            {
                await App.Database.SaveShiftAsync(new Shift
                {
                    WorkDate = Convert.ToDateTime(Title),

                    
                    ShiftType = Convert.ToString(picker1.SelectedItem),
                    ShiftTime = Convert.ToString(picker2.SelectedItem),
                    Rate = Convert.ToDecimal(rateEntry.Text),
                    HrsWork = Convert.ToDecimal(hrsWorkEntry.Text),
                    Diff = Convert.ToDecimal(diffEntry.Text),
                });


                datePicker.Date = DateTime.Now;
                picker1.SelectedItem = Title;
                picker2.SelectedItem = Title;
                rateEntry.Text = string.Empty;
                hrsWorkEntry.Text = string.Empty;
                diffEntry.Text = string.Empty;

                CollectionView.ItemsSource = await
                    App.Database.GetShiftAsync();

                //await Application.Current.MainPage.DisplayAlert("Start", $"{ picker1}\n, { picker2}\n, {picker1.Title}\n {picker2.Title}", "OK");

            }
        }
        // Refresh from List with selected record to edit.
        Shift lastSelection;
        public void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as Shift;

            shiftType = Convert.ToString(lastSelection.ShiftType);
            picker1.Title = shiftType;
            shiftTime = Convert.ToString(lastSelection.ShiftTime);
            picker2.Title = shiftTime;

            datePicker.Date = Convert.ToDateTime(lastSelection.WorkDate);
            rateEntry.Text = Convert.ToString(lastSelection.Rate);
            hrsWorkEntry.Text = Convert.ToString(lastSelection.HrsWork);
            diffEntry.Text = Convert.ToString(lastSelection.Diff);
            totalEntry.Text = Convert.ToString(lastSelection.Total);
            //await Application.Current.MainPage.DisplayAlert("Refresh after Selection", $"{ picker1.SelectedItem}\n, { picker2.SelectedItem}\n, {picker1.Title}\n {picker2.Title}", "OK");
        }
        // Update Record after Change
        async void Button_Clicked(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                if (picker1.SelectedItem != null || picker1.SelectedItem != "")
                {
                    picker1.Title = Convert.ToString(picker1.SelectedItem);
                    //picker1.Items = selectedItem;
                }

                if (picker2.SelectedItem != null || picker2.SelectedItem != "")
                {
                    picker2.Title = Convert.ToString(picker2.SelectedItem);
                    //picker1.Items = selectedItem;
                }

                lastSelection.WorkDate = Convert.ToDateTime(datePicker.Date);
                datePicker.Date = Convert.ToDateTime(lastSelection.WorkDate);
                picker1.Title = (string)picker1.SelectedItem;
                lastSelection.ShiftType = lastSelection.ShiftType;
                picker2.Title = (string)picker2.SelectedItem;
                lastSelection.ShiftTime = lastSelection.ShiftTime;

                lastSelection.Rate = Convert.ToDecimal(rateEntry.Text);
                lastSelection.HrsWork = Convert.ToDecimal(hrsWorkEntry.Text);
                lastSelection.Diff = Convert.ToDecimal(diffEntry.Text);
                //lastSelection.Total = Convert.ToDecimal(totalEntry.Text);

                //await Application.Current.MainPage.DisplayAlert("Update", $"{picker1.Title}\n {picker1.SelectedItem}\n {picker2.Title}\n {picker2.SelectedItem}", "OK");

                datePicker.Date = DateTime.Now;

                shiftType = "Shift Type"; shiftTime = "Shift Time";
                picker1.Title = shiftType;
                picker1.SelectedItem = shiftType;
                picker2.Title = shiftTime;
                picker2.SelectedItem = shiftTime;
                rateEntry.Text = String.Empty;
                hrsWorkEntry.Text = String.Empty;
                diffEntry.Text = String.Empty;
                totalEntry.Text = "0.00";
                {
                }
                await App.Database.UpdateShiftAsync(lastSelection);
                CollectionView.ItemsSource = await
                    App.Database.GetShiftAsync();
                //await Application.Current.MainPage.DisplayAlert("After Update", $"{picker1.Title}\n {picker1.SelectedItem}\n {picker2.Title}\n {picker2.SelectedItem}", "OK");
            }
        }
        // Delete
        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            if (lastSelection != null)
            {
                await App.Database.DeleteShiftAsync(lastSelection);

                CollectionView.ItemsSource = await
                    App.Database.GetShiftAsync();

                datePicker.Date = DateTime.Now;

                shiftType = "Shift Type"; shiftTime = "Shift Time";
                picker1.Title = shiftType;
                picker1.SelectedItem = shiftType;
                picker2.Title = shiftTime;
                picker2.SelectedItem = shiftTime;
                rateEntry.Text = String.Empty;
                hrsWorkEntry.Text = String.Empty;
                diffEntry.Text = String.Empty;
                totalEntry.Text = "0.00";
            }
        }

        private void Picker1_Unfocused(object sender, FocusEventArgs e)
        {
            //await Application.Current.MainPage.DisplayAlert("Picker1 Unfocused", $"{picker1.Title}\n {picker1.SelectedItem}\n {picker1.ItemsSource}", "OK");
            // Get the Rate for the Shift Type Selected and enter in the Rate Text Box.
            if (picker1.Title != null)
            {
                currentItem = Convert.ToString(picker1.SelectedItem);
                if (currentItem == "Regular Hours" || currentItem == "Sick Day" || currentItem == "Vacation Pay" || currentItem == "Stat")
                {
                    rateEntry.Text = Convert.ToString(rate1);
                }
                else if (currentItem == "Stat 1.5" || currentItem == "Over Time 1.5")
                {
                    rateEntry.Text = Convert.ToString(rate2);
                }
                else if (currentItem == "Stat 2.0" || currentItem == "Over Time 2.0")
                {
                    rateEntry.Text = Convert.ToString(rate3);
                }
            }
        }

        //TODO This may be useful to do calculations with SQL Querys
        // Get Subscribed
        //async void Button_Clicked_2(object sender, EventArgs e)
        //{
        //    collectionView.ItemsSource = await
        //            App.Database.QuerySubscribedAsync();
        //}
        //// Get Not Subscribed
        //async void Button_Clicked_3(object sender, EventArgs e)
        //{
        //    collectionView.ItemsSource = await
        //           App.Database.LinqNotSubscribedAsync();
        //}
    }
}