/*
 * Docker Engine API
 *
 * The Engine API is an HTTP API served by Docker Engine. It is the API the Docker client uses to communicate with the Engine, so everything the Docker client can do can be done with the API.  Most of the client's commands map directly to API endpoints (e.g. `docker ps` is `GET /containers/json`). The notable exception is running containers, which consists of several API calls.  # Errors  The API uses standard HTTP status codes to indicate the success or failure of the API call. The body of the response will be JSON in the following format:  ``` {   \"message\": \"page not found\" } ```  # Versioning  The API is usually changed in each release, so API calls are versioned to ensure that clients don't break. To lock to a specific version of the API, you prefix the URL with its version, for example, call `/v1.30/info` to use the v1.30 version of the `/info` endpoint. If the API version specified in the URL is not supported by the daemon, a HTTP `400 Bad Request` error message is returned.  If you omit the version-prefix, the current version of the API (v1.40) is used. For example, calling `/info` is the same as calling `/v1.40/info`. Using the API without a version-prefix is deprecated and will be removed in a future release.  Engine releases in the near future should support this version of the API, so your client will continue to work even if it is talking to a newer Engine.  The API uses an open schema model, which means server may add extra properties to responses. Likewise, the server will ignore any extra query parameters and request body properties. When you write clients, you need to ignore additional properties in responses to ensure they do not break when talking to newer daemons.   # Authentication  Authentication for registries is handled client side. The client has to send authentication details to various endpoints that need to communicate with registries, such as `POST /images/(name)/push`. These are sent as `X-Registry-Auth` header as a [base64url encoded](https://tools.ietf.org/html/rfc4648#section-5) (JSON) string with the following structure:  ``` {   \"username\": \"string\",   \"password\": \"string\",   \"email\": \"string\",   \"serveraddress\": \"string\" } ```  The `serveraddress` is a domain/IP without a protocol. Throughout this structure, double quotes are required.  If you have already got an identity token from the [`/auth` endpoint](#operation/SystemAuth), you can just pass this instead of credentials:  ``` {   \"identitytoken\": \"9cbaf023786cd7...\" } ``` 
 *
 * The version of the OpenAPI document: 1.40
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace FluentDeploy.Components.Docker.DockerApi.Model
{
    /// <summary>
    ///     Available plugins per type.  &lt;p&gt;&lt;br /&gt;&lt;/p&gt;  &gt; **Note**: Only unmanaged (V1) plugins are
    ///     included in this list. &gt; V1 plugins are \&quot;lazily\&quot; loaded, and are not returned in this list &gt; if
    ///     there is no resource using the plugin.
    /// </summary>
    [DataContract]
    public class PluginsInfo : IEquatable<PluginsInfo>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PluginsInfo" /> class.
        /// </summary>
        /// <param name="volume">Names of available volume-drivers, and network-driver plugins..</param>
        /// <param name="network">Names of available network-drivers, and network-driver plugins..</param>
        /// <param name="authorization">Names of available authorization plugins..</param>
        /// <param name="log">Names of available logging-drivers, and logging-driver plugins..</param>
        public PluginsInfo(List<string> volume = default, List<string> network = default,
            List<string> authorization = default, List<string> log = default)
        {
            Volume = volume;
            Network = network;
            Authorization = authorization;
            Log = log;
        }

        /// <summary>
        ///     Names of available volume-drivers, and network-driver plugins.
        /// </summary>
        /// <value>Names of available volume-drivers, and network-driver plugins.</value>
        [DataMember(Name = "Volume", EmitDefaultValue = false)]
        public List<string> Volume { get; set; }

        /// <summary>
        ///     Names of available network-drivers, and network-driver plugins.
        /// </summary>
        /// <value>Names of available network-drivers, and network-driver plugins.</value>
        [DataMember(Name = "Network", EmitDefaultValue = false)]
        public List<string> Network { get; set; }

        /// <summary>
        ///     Names of available authorization plugins.
        /// </summary>
        /// <value>Names of available authorization plugins.</value>
        [DataMember(Name = "Authorization", EmitDefaultValue = false)]
        public List<string> Authorization { get; set; }

        /// <summary>
        ///     Names of available logging-drivers, and logging-driver plugins.
        /// </summary>
        /// <value>Names of available logging-drivers, and logging-driver plugins.</value>
        [DataMember(Name = "Log", EmitDefaultValue = false)]
        public List<string> Log { get; set; }

        /// <summary>
        ///     Returns true if PluginsInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of PluginsInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PluginsInfo input)
        {
            if (input == null)
                return false;

            return
                (
                    Volume == input.Volume ||
                    Volume != null &&
                    input.Volume != null &&
                    Volume.SequenceEqual(input.Volume)
                ) &&
                (
                    Network == input.Network ||
                    Network != null &&
                    input.Network != null &&
                    Network.SequenceEqual(input.Network)
                ) &&
                (
                    Authorization == input.Authorization ||
                    Authorization != null &&
                    input.Authorization != null &&
                    Authorization.SequenceEqual(input.Authorization)
                ) &&
                (
                    Log == input.Log ||
                    Log != null &&
                    input.Log != null &&
                    Log.SequenceEqual(input.Log)
                );
        }

        /// <summary>
        ///     To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PluginsInfo {\n");
            sb.Append("  Volume: ").Append(Volume).Append("\n");
            sb.Append("  Network: ").Append(Network).Append("\n");
            sb.Append("  Authorization: ").Append(Authorization).Append("\n");
            sb.Append("  Log: ").Append(Log).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        ///     Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        ///     Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return Equals(input as PluginsInfo);
        }

        /// <summary>
        ///     Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                if (Volume != null)
                    hashCode = hashCode * 59 + Volume.GetHashCode();
                if (Network != null)
                    hashCode = hashCode * 59 + Network.GetHashCode();
                if (Authorization != null)
                    hashCode = hashCode * 59 + Authorization.GetHashCode();
                if (Log != null)
                    hashCode = hashCode * 59 + Log.GetHashCode();
                return hashCode;
            }
        }
    }
}