﻿using System;
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
        string differential;
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
            DateTime? date = dateHead.Date;
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
                    Diff = Convert.ToDecimal(picker3.SelectedItem),
                    Rate = Convert.ToDecimal(rateEntry.Text),
                    HrsWork = Convert.ToDecimal(hrsWorkEntry.Text),
                }) ; ;


                dateHead.Date = DateTime.Now;
                picker1.SelectedItem = Title;
                picker2.SelectedItem = Title;
                picker3.SelectedItem = Title;
                rateEntry.Text = string.Empty;
                hrsWorkEntry.Text = string.Empty;

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

            dateHead.Date = Convert.ToDateTime(lastSelection.WorkDate);

            picker1.SelectedItem = lastSelection.ShiftType;
            picker2.SelectedItem = lastSelection.ShiftTime;
            picker3.SelectedItem = lastSelection.Diff;
            int P1 = -1; int P2 = -1; int P3 = -1;
            switch (lastSelection.ShiftType)
            {
                case "Regular Hours": P1 = 0; break;
                case "Stat 1.5": P1 = 1; break;
                case "Stat 2.0": P1 = 2; break;
                case "Over Time 1.5": P1 = 3; break;
                case "Over Time 2.0": P1 = 4; break;
                case "Sick Day": P1 = 5; break;
                case "Vacation Pay": P1 = 6; break;
                case "Day Off": P1 = 7; break;
            }
            switch (lastSelection.ShiftTime) 
            { 
                case "Morning":P2 = 0; break;
                case "Evening": P2 = 1;break;
                case "Night Shift":P2 = 2;break;
            }
            switch (lastSelection.Diff)
            {
                case 0.00m: P3 = 0; break;
                case 0.50m: P3 = 1; break;
                case 0.75m: P3 = 2; break;
            }
            if (picker1.SelectedIndex != -1) 
            { P1 = Convert.ToInt32(picker1.SelectedIndex); }
            picker1.SelectedIndex = P1;
            if (picker2.SelectedIndex != -1)
            { P2 = Convert.ToInt32(picker2.SelectedIndex); }
            picker2.SelectedIndex = P2;
            if (picker3.SelectedIndex != -1)
            { P1 = Convert.ToInt32(picker3.SelectedIndex); }
            picker3.SelectedIndex = P3;

            rateEntry.Text = Convert.ToString(lastSelection.Rate);
            hrsWorkEntry.Text = Convert.ToString(lastSelection.HrsWork);
            totalEntry.Text = Convert.ToString(lastSelection.Total);
        }
        // Update Record after Change
        async void Button_Clicked(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                lastSelection.WorkDate = Convert.ToDateTime(dateHead.Date);
                dateHead.Date = Convert.ToDateTime(lastSelection.WorkDate);

                if (Convert.ToString(picker1.SelectedItem) != "Shift Type")
                {
                    lastSelection.ShiftType = Convert.ToString(picker1.SelectedItem);
                }
                if (Convert.ToString(picker2.SelectedItem) != "Shift Time")
                {
                    lastSelection.ShiftTime = Convert.ToString(picker2.SelectedItem);
                }
                if (Convert.ToString(picker3.SelectedItem) != "Differential")
                {
                    lastSelection.Diff = Convert.ToDecimal(picker3.SelectedItem);
                }
                lastSelection.Rate = Convert.ToDecimal(rateEntry.Text);
                lastSelection.HrsWork = Convert.ToDecimal(hrsWorkEntry.Text);
                //lastSelection.Total = Convert.ToDecimal(totalEntry.Text);

                await App.Database.UpdateShiftAsync(lastSelection);
                CollectionView.ItemsSource = await
                    App.Database.GetShiftAsync();

                dateHead.Date = DateTime.Now;

                picker1.SelectedIndex = -1;
                picker1.Title = "Shift Type";
                picker2.SelectedIndex = -1;
                picker2.Title = "Shift Time";
                picker3.SelectedIndex = -1;
                picker3.Title = "Differential";

                rateEntry.Text = String.Empty;
                hrsWorkEntry.Text = String.Empty;
                totalEntry.Text = "0.00";
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

                dateHead.Date = DateTime.Now;

                shiftType = "Shift Type"; shiftTime = "Shift Time"; differential = "Diff";
                picker1.Title = shiftType;
                picker1.SelectedItem = shiftType;
                picker2.Title = shiftTime;
                picker2.SelectedItem = shiftTime;
                picker3.Title = differential;
                picker3.SelectedItem = differential;
                rateEntry.Text = String.Empty;
                hrsWorkEntry.Text = String.Empty;
                totalEntry.Text = "0.00";
            }
        }
        // Sort on Date Column
        // ===================
        //private void Sort()
        //{
        //    //CollectionView.Sort(CollectionView.ColumnDefinitionsProperty[2],
        //    CollectionView.(CollectionView.ColumnDefinitionsProperty[2],
        //        System.ComponentModel.ListSortDirection.Ascending;
        //}
        private void picker1_Unfocused(object sender, FocusEventArgs e)
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
                else if (currentItem == "Day Off")
                {
                    rateEntry.Text = "0.00";
                }
            }
        }
    }
}