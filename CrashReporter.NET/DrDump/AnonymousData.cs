using System;

namespace CrashReporterDotNET.DrDump
{
    internal class AnonymousData
    {
        public Exception Exception { get; set; }
        public string ToEmail { get; set; }
        public Guid? ApplicationID { get; set; }
    }
}
