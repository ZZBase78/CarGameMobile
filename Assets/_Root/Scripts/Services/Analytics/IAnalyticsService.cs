using System;
using System.Collections.Generic;

namespace Services.Analytics
{
    internal interface IAnalyticsService
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, Dictionary<string, object> eventData);
        UnityEngine.Analytics.AnalyticsResult Transaction(string productId, Decimal amount, string currency);
    }
}
