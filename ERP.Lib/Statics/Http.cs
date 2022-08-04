namespace ERP.BaseLib.Statics
{
    /// <summary>
    /// Contains information about the http-server
    /// </summary>
    public static class Http
    {
        /// <summary>
        /// The urls the server listens on
        /// </summary>
        public static List<string> ServerUrls = new() { "http://localhost:55555/" };
    }
}