﻿using System;
using Xamarin.Forms;

namespace LocalNotifications
{
    public partial class MainPage : ContentPage
    {
        INotificationManager notificationManager;
        int notificationNumber = 0;
        //string title;
        //string message;
        public MainPage()
        {
            InitializeComponent();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            //notificationNumber++;
            string title = "Local Notification2222";
            string message = "You have now received notifications!";
            notificationManager.ScheduleNotification(title, message);

        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }
    }
}