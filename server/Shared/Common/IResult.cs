using System;
using System.Net;

namespace Langscape.Shared
{
    /// <summary>
    /// A common class for all server responses
    /// </summary>
    /// <typeparam name="T">The type of returned data</typeparam>
    public interface IResult<T>
    {
        /// <summary>
        /// Server messages including failing or extra info
        /// </summary>
        string Title { get; }

        /// <summary>
        /// True if the request succeeded
        /// </summary>
        bool Succeeded { get; }

        /// <summary>
        /// The data sent from the server
        /// </summary>
        T Data { get; }

        /// <summary>
        /// Exception on failure if any (only for development)
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// Response status code
        /// </summary>
        HttpStatusCode Code { get; }
    }
}