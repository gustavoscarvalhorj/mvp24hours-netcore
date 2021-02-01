﻿//=====================================================================================
// Developed by Kallebe Lins (kallebe.santos@outlook.com)
// Teacher, Architect, Consultant and Project Leader
// Virtual Card: https://www.linkedin.com/in/kallebelins
//=====================================================================================
// Reproduction or sharing is free!
//=====================================================================================
using Mvp24Hours.Core.Contract.Infrastructure.Contexts;
using Mvp24Hours.Core.Enums;
using Mvp24Hours.Core.ValueObjects.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Mvp24Hours.Infrastructure.Contexts
{
    /// <summary>
    /// <see cref="Mvp24Hours.Core.Contract.Infrastructure.Contexts.INotificationContext"/>
    /// </summary>
    public class NotificationContext : INotificationContext
    {
        #region [ Members ]
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();
        public bool HasErrorNotifications => _notifications.Any(x => x.Type == MessageType.Error);
        #endregion

        #region [ Ctor ]
        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }
        #endregion

        #region [ Methods ]
        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }
        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }
        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
        #endregion
    }
}
