﻿using GlobalResources;
using System;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Web.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Web.Models
{
    public class DeviceJobModel
    {
        public DeviceJobModel(JobResponse jobResponse)
        {
            Status = jobResponse.Status;
            JobId = jobResponse.JobId;
            QueryCondition = jobResponse.QueryCondition;
            DeviceCount = ConvertNullValue(jobResponse.DeviceJobStatistics?.DeviceCount);
            SucceededCount = ConvertNullValue(jobResponse.DeviceJobStatistics?.SucceededCount);
            FailedCount = ConvertNullValue(jobResponse.DeviceJobStatistics?.FailedCount);
            PendingCount = ConvertNullValue(jobResponse.DeviceJobStatistics?.PendingCount);
            RunningCount = ConvertNullValue(jobResponse.DeviceJobStatistics?.RunningCount);
            OperationType = jobResponse.Type.LocalizedString();
            StartTime = jobResponse.StartTimeUtc;
            EndTime = jobResponse.EndTimeUtc;
            CloudToDeviceMethod = jobResponse.CloudToDeviceMethod;
            UpdateTwin = jobResponse.UpdateTwin;
        }

        public DeviceJobModel(string jobResponseJsonString)
        {
            var wrappedJobResponse = JsonConvert.DeserializeObject<JobResponseWrapper>(jobResponseJsonString);
            Status = wrappedJobResponse.Status;
            JobId = wrappedJobResponse.JobId;
            QueryCondition = wrappedJobResponse.QueryCondition;
            DeviceCount = ConvertNullValue(wrappedJobResponse.DeviceJobStatistics?.DeviceCount);
            SucceededCount = ConvertNullValue(wrappedJobResponse.DeviceJobStatistics?.SucceededCount);
            FailedCount = ConvertNullValue(wrappedJobResponse.DeviceJobStatistics?.FailedCount);
            PendingCount = ConvertNullValue(wrappedJobResponse.DeviceJobStatistics?.PendingCount);
            RunningCount = ConvertNullValue(wrappedJobResponse.DeviceJobStatistics?.RunningCount);
            OperationType = wrappedJobResponse.Type.LocalizedString();
            StartTime = wrappedJobResponse.StartTime;
            EndTime = wrappedJobResponse.EndTime;
        }

        public string JobId { get; set; }
        public string JobName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public JobStatus Status { get; set; }
        public string QueryName { get; set; }
        public string QueryCondition { get; set; }
        public string OperationType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string DeviceCount { get; set; }
        public string SucceededCount { get; set; }
        public string FailedCount { get; set; }
        public string PendingCount { get; set; }
        public string RunningCount { get; set; }
        public string DeviceId { get; set; }
        public Twin UpdateTwin { get; set; }
        public CloudToDeviceMethod CloudToDeviceMethod { get; set; }

        private string ConvertNullValue(int? value)
        {
            return value == null ? Strings.NotApplicableValue : value.ToString();
        }

        /// <summary>
        /// This is a wrapper class to fix the problem of null value for 
        /// StartTime and EndTime property of JobResponse returned by JobClient.
        /// </summary>
        class JobResponseWrapper : JobResponse
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }
    }
}