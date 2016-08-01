//----------------------------------------------------------------------- 
// <copyright file="ServiceConfigInfo.cs" company="No Company"> 
//     No Company. All rights reserved. 
// </copyright> 
//----------------------------------------------------------------------- 
namespace DAL.Configuration
{
    using System;
    using System.Net;

    /// <summary>
    /// Data for any service
    /// </summary>
    [Serializable]
    public class ServiceConfigInfo
    {
        public IPEndPoint IpEndPoint { get; set; }
        public string ServiceType { get; set; }
        public string Path { get; set; }
    }
}
