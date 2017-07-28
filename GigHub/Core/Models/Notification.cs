﻿using System;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        public Gig Gig { get; private set; }

        protected Notification()
        { }

        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException(nameof(gig));

            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig gig, DateTime dateTime, string venue)
        {
            var notification = new Notification(NotificationType.GigUpdated, gig)
            {
                OriginalDateTime = dateTime,
                OriginalVenue = venue
            };

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }

    }
}