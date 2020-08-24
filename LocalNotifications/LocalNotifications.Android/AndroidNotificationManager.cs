﻿using System;
using System.Globalization;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Graphics;
using Android.Icu.Util;
using Android.OS;
using Android.Support.V4.App;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(LocalNotifications.Droid.AndroidNotificationManager))]
namespace LocalNotifications.Droid
{
    public class AndroidNotificationManager : INotificationManager
    {
        const string channelId = "channel name";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";
        const int pendingIntentId = 0;

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        bool channelInitialized = false;
        int messageId = -1;
        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public void Initialize()
        {
            CreateNotificationChannel();
        }

        public NotificationManager getManager()
        {
            return manager;
        }

        public int ScheduleNotification(string title, string message)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            messageId++;

            Intent alarmIntent = new Intent(AndroidApp.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra(TitleKey, title);
            alarmIntent.PutExtra(MessageKey, message);
            alarmIntent.PutExtra("channelId", channelId);

            //PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.OneShot);

            //NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
            //    .SetContentIntent(pendingIntent)
            //    .SetContentTitle(title)
            //    .SetContentText(message)
            //    .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.xamagonBlue))
            //    .SetSmallIcon(Resource.Drawable.xamagonBlue)
            //    .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            //Notification notification = builder.Build();
            //manager.Notify(messageId, notification);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, 0, alarmIntent, PendingIntentFlags.CancelCurrent);

            var alarmManager = (AlarmManager)AndroidApp.Context.GetSystemService(Context.AlarmService);
            alarmManager.Set(AlarmType.RtcWakeup, DateTime.Now.Millisecond + 30000, pendingIntent);
            //alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 2 * 1000, pendingIntent);

            return messageId;
        }

        public int ScheduleNotification2(string title, string message)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            messageId++;

            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.xamagonBlue))
                .SetSmallIcon(Resource.Drawable.xamagonBlue)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            Notification notification = builder.Build();
            manager.Notify(messageId, notification);
            Console.Write("hello");
            return messageId;
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);
        }

        void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
    }
}
