using Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Graphics;

using LocalNotifications;
using LocalNotifications.Droid;
using AndroidApp = Android.App.Application;

using Xamarin.Forms;

[BroadcastReceiver]
public class AlarmReceiver : BroadcastReceiver
{
    private int z = 0;
    private int i;


    public override void OnReceive(Context context, Intent intent)
    {

        var title = intent.GetStringExtra("title");
        var message = intent.GetStringExtra("messsage");
        var channelId = intent.GetStringExtra("channelId");


        //var resultIntent = new Intent(context, typeof(MainActivity));


        //PendingIntent pending = PendingIntent.GetActivities(context, 0,
        //    new Intent[] { resultIntent },
        //    PendingIntentFlags.OneShot);


        ////////
        //NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, "kkk")
        //    .SetContentIntent(pending)
        //    .SetContentTitle(title)
        //    .SetContentText(message)
        //    .SetSmallIcon(Android.Resource.Drawable.SymDefAppIcon)
        //    .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

        ///////


       // var notification = builder.Build();

        DateTime now = DateTime.Now;
        var notificationID = now.Millisecond;
        AndroidNotificationManager manager = (AndroidNotificationManager) DependencyService.Get<INotificationManager>();
        
        Console.WriteLine("AlarmReceiver 222");
        manager.ScheduleNotification2(title, message);

    }

}
//[BroadcastReceiver]
//public class AlarmReceiver : BroadcastReceiver
//{
//    public override void OnReceive(Context context, Intent intent)
//    {
//        var message = intent.GetStringExtra("message");
//        var title = intent.GetStringExtra("title");

//        var resultIntent = new Intent(context, typeof(MainActivity));
//        resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

//        var pending = PendingIntent.GetActivity(context, 0,
//            resultIntent,
//            PendingIntentFlags.CancelCurrent);

//        var builder =
//            new Notification.Builder(context)
//                .SetContentTitle(title)
//                .SetContentText(message)
//                .SetDefaults(NotificationDefaults.All);

//        builder.SetContentIntent(pending);

//        var notification = builder.Build();

//        var manager = NotificationManager.FromContext(context);
//        DateTime now = DateTime.Now;
//        var notificationID = now.Millisecond;

//        manager.Notify(notificationID, notification);
//        //manager.Notify(1337, notification);
//    }
//}