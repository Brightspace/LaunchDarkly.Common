﻿using System;
using Newtonsoft.Json.Linq;
using LaunchDarkly.Client;

namespace LaunchDarkly.Common
{
    internal abstract class EventFactory
    {
        internal static EventFactory Default { get; } = new DefaultEventFactory();

        internal abstract long GetTimestamp();

        internal FeatureRequestEvent NewFeatureRequestEvent(IFlagEventProperties flag, User user,
            int? variation, JToken value, JToken defaultVal)
        {
            return new FeatureRequestEvent(GetTimestamp(), flag.Key, user, variation, value, defaultVal,
                flag.Version, null, flag.TrackEvents, flag.DebugEventsUntilDate, false);
        }

        internal FeatureRequestEvent NewDefaultFeatureRequestEvent(IFlagEventProperties flag, User user,
            JToken defaultVal)
        {
            return new FeatureRequestEvent(GetTimestamp(), flag.Key, user, null, defaultVal, defaultVal,
                flag.Version, null, flag.TrackEvents, flag.DebugEventsUntilDate, false);
        }

        internal FeatureRequestEvent NewUnknownFeatureRequestEvent(string key, User user,
            JToken defaultVal)
        {
            return new FeatureRequestEvent(GetTimestamp(), key, user, null, defaultVal, defaultVal,
                null, null, false, null, false);
        }

        internal FeatureRequestEvent NewPrerequisiteFeatureRequestEvent(IFlagEventProperties prereqFlag, User user,
            int? variation, JToken value, IFlagEventProperties prereqOf)
        {
            return new FeatureRequestEvent(GetTimestamp(), prereqFlag.Key, user, variation, value, null,
                prereqFlag.Version, prereqOf.Key, prereqFlag.TrackEvents, prereqFlag.DebugEventsUntilDate, false);
        }

        internal FeatureRequestEvent NewDebugEvent(FeatureRequestEvent from)
        {
            return new FeatureRequestEvent(from.CreationDate, from.Key, from.User, from.Variation, from.Value, from.Default,
                from.Version, from.PrereqOf, from.TrackEvents, from.DebugEventsUntilDate, true);
        }

        internal CustomEvent NewCustomEvent(string key, User user, string data)
        {
            return new CustomEvent(GetTimestamp(), key, user, data);
        }

        internal IdentifyEvent NewIdentifyEvent(User user)
        {
            return new IdentifyEvent(GetTimestamp(), user);
        }
    }

    internal class DefaultEventFactory : EventFactory
    {
        override internal long GetTimestamp()
        {
            return Util.GetUnixTimestampMillis(DateTime.UtcNow);
        }
    }
}
