﻿using System;

namespace AdronCore.Tools.Notification.NotifyPlatform
{
    public class DotnetNotify : Notify
    {
        public static void HandleDotNetNotification(string message, NotificationType type)
        {
            LogToFile(message, type.ToString());

            switch (type)
            {
                case NotificationType.Info:
                    Console.WriteLine($"[INFO] {message}");
                    break;
                case NotificationType.Warning:
                    Console.WriteLine($"[WARNING] {message}");
                    break;
                case NotificationType.Error:
                    Console.WriteLine($"[ERROR] {message}");
                    break;
                case NotificationType.Success:
                    Console.WriteLine($"[Succes] {message}");
                    break;
                default:
                    throw new ArgumentException("Invalid declaration type");
            }
        }
    }
}
