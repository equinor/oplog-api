﻿namespace Oplog.Core.Commands
{
    public class CreateLogResult
    {
        public int LogId { get; set; }
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public CreateLogResult LogCreated(int logId)
        {
            ResultType = ResultType.Success;
            Message = "New log created";
            LogId = logId;
            return this;
        }
    }
}
