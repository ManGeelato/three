namespace ThreeSixty.Common
{
    public static class Enums
    {
        public enum BatchStatus
        {
            None = -1,
            TransferStarted = 0,
            TransferCompleted = 1,
            TransferFailed = 2,
            ExtractionSucceeded = 3,
            ExtractionFailed = 4
        }


        public enum SubmissionStatus
        {
            None = -1,
            Created = 0,
            Completed = 1,
            PartiallyCompleted = 2,
            Failed = 3,
        }

        public enum SubmissionType
        {
            None = -1,
            Batch = 0,
            Api = 1,
        }

        public enum IngestionType
        {
            None = -1,
            MetaData = 0,
            Incident = 1,
        }

        public enum FileIngestionStatus
        {
            None = -1,
            IngestionStarted = 0,
            IngestionCompleted = 1,
            IngestionFailed = 2
        }


        public enum IngestionStatus
        {
            None = -1,
            IngestionStarted = 0,
            IngestionCompleted = 1,
            IngestionFailed = 2
        }

        public enum IngestionStatusReason
        {
            None = -1,
            InvalidXml = 0,
            InvalidJson = 1
        }

        public enum FileType
        {
            None = -1,
            Incident = 0,
            MetaData = 1,
        }
        
        public enum MethodType
        {
            Post = 0,
            Get = 1
        }

        public enum IncidentStatus
        {
            Pending = 0,
            Assigned = 1,
            InProgress = 2,
            Closed = 3,
            Cancelled = 4,
        }

        public enum IncidentStatusReason
        {
            Pending = 0,
            Assigned = 1,
            InProgress = 2,
            Closed = 3,
            Cancelled = 4,
        }
    }
}
