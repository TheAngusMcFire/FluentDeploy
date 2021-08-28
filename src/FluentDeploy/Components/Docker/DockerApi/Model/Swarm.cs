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
    ///     Swarm
    /// </summary>
    [DataContract]
    public class Swarm : IEquatable<Swarm>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Swarm" /> class.
        /// </summary>
        /// <param name="iD">The ID of the swarm..</param>
        /// <param name="version">version.</param>
        /// <param name="createdAt">
        ///     Date and time at which the swarm was initialised in [RFC
        ///     3339](https://www.ietf.org/rfc/rfc3339.txt) format with nano-seconds. .
        /// </param>
        /// <param name="updatedAt">
        ///     Date and time at which the swarm was last updated in [RFC
        ///     3339](https://www.ietf.org/rfc/rfc3339.txt) format with nano-seconds. .
        /// </param>
        /// <param name="spec">spec.</param>
        /// <param name="tLSInfo">tLSInfo.</param>
        /// <param name="rootRotationInProgress">Whether there is currently a root CA rotation in progress for the swarm .</param>
        /// <param name="dataPathPort">
        ///     DataPathPort specifies the data path port number for data traffic. Acceptable port range is
        ///     1024 to 49151. If no port is set or is set to 0, the default port (4789) is used. .
        /// </param>
        /// <param name="defaultAddrPool">Default Address Pool specifies default subnet pools for global scope networks. .</param>
        /// <param name="subnetSize">SubnetSize specifies the subnet size of the networks created from the default subnet pool. .</param>
        /// <param name="joinTokens">joinTokens.</param>
        public Swarm(string iD = default, ObjectVersion version = default, string createdAt = default,
            string updatedAt = default, SwarmSpec spec = default, TLSInfo tLSInfo = default,
            bool rootRotationInProgress = default, int dataPathPort = default, List<string> defaultAddrPool = default,
            int subnetSize = default, JoinTokens joinTokens = default)
        {
            ID = iD;
            _Version = version;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Spec = spec;
            TLSInfo = tLSInfo;
            RootRotationInProgress = rootRotationInProgress;
            DataPathPort = dataPathPort;
            DefaultAddrPool = defaultAddrPool;
            SubnetSize = subnetSize;
            JoinTokens = joinTokens;
        }

        /// <summary>
        ///     The ID of the swarm.
        /// </summary>
        /// <value>The ID of the swarm.</value>
        [DataMember(Name = "ID", EmitDefaultValue = false)]
        public string ID { get; set; }

        /// <summary>
        ///     Gets or Sets _Version
        /// </summary>
        [DataMember(Name = "Version", EmitDefaultValue = false)]
        public ObjectVersion _Version { get; set; }

        /// <summary>
        ///     Date and time at which the swarm was initialised in [RFC 3339](https://www.ietf.org/rfc/rfc3339.txt) format with
        ///     nano-seconds.
        /// </summary>
        /// <value>
        ///     Date and time at which the swarm was initialised in [RFC 3339](https://www.ietf.org/rfc/rfc3339.txt) format with
        ///     nano-seconds.
        /// </value>
        [DataMember(Name = "CreatedAt", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     Date and time at which the swarm was last updated in [RFC 3339](https://www.ietf.org/rfc/rfc3339.txt) format with
        ///     nano-seconds.
        /// </summary>
        /// <value>
        ///     Date and time at which the swarm was last updated in [RFC 3339](https://www.ietf.org/rfc/rfc3339.txt) format
        ///     with nano-seconds.
        /// </value>
        [DataMember(Name = "UpdatedAt", EmitDefaultValue = false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        ///     Gets or Sets Spec
        /// </summary>
        [DataMember(Name = "Spec", EmitDefaultValue = false)]
        public SwarmSpec Spec { get; set; }

        /// <summary>
        ///     Gets or Sets TLSInfo
        /// </summary>
        [DataMember(Name = "TLSInfo", EmitDefaultValue = false)]
        public TLSInfo TLSInfo { get; set; }

        /// <summary>
        ///     Whether there is currently a root CA rotation in progress for the swarm
        /// </summary>
        /// <value>Whether there is currently a root CA rotation in progress for the swarm </value>
        [DataMember(Name = "RootRotationInProgress", EmitDefaultValue = false)]
        public bool RootRotationInProgress { get; set; }

        /// <summary>
        ///     DataPathPort specifies the data path port number for data traffic. Acceptable port range is 1024 to 49151. If no
        ///     port is set or is set to 0, the default port (4789) is used.
        /// </summary>
        /// <value>
        ///     DataPathPort specifies the data path port number for data traffic. Acceptable port range is 1024 to 49151. If no
        ///     port is set or is set to 0, the default port (4789) is used.
        /// </value>
        [DataMember(Name = "DataPathPort", EmitDefaultValue = false)]
        public int DataPathPort { get; set; }

        /// <summary>
        ///     Default Address Pool specifies default subnet pools for global scope networks.
        /// </summary>
        /// <value>Default Address Pool specifies default subnet pools for global scope networks. </value>
        [DataMember(Name = "DefaultAddrPool", EmitDefaultValue = false)]
        public List<string> DefaultAddrPool { get; set; }

        /// <summary>
        ///     SubnetSize specifies the subnet size of the networks created from the default subnet pool.
        /// </summary>
        /// <value>SubnetSize specifies the subnet size of the networks created from the default subnet pool. </value>
        [DataMember(Name = "SubnetSize", EmitDefaultValue = false)]
        public int SubnetSize { get; set; }

        /// <summary>
        ///     Gets or Sets JoinTokens
        /// </summary>
        [DataMember(Name = "JoinTokens", EmitDefaultValue = false)]
        public JoinTokens JoinTokens { get; set; }

        /// <summary>
        ///     Returns true if Swarm instances are equal
        /// </summary>
        /// <param name="input">Instance of Swarm to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Swarm input)
        {
            if (input == null)
                return false;

            return
                (
                    ID == input.ID ||
                    ID != null &&
                    ID.Equals(input.ID)
                ) &&
                (
                    _Version == input._Version ||
                    _Version != null &&
                    _Version.Equals(input._Version)
                ) &&
                (
                    CreatedAt == input.CreatedAt ||
                    CreatedAt != null &&
                    CreatedAt.Equals(input.CreatedAt)
                ) &&
                (
                    UpdatedAt == input.UpdatedAt ||
                    UpdatedAt != null &&
                    UpdatedAt.Equals(input.UpdatedAt)
                ) &&
                (
                    Spec == input.Spec ||
                    Spec != null &&
                    Spec.Equals(input.Spec)
                ) &&
                (
                    TLSInfo == input.TLSInfo ||
                    TLSInfo != null &&
                    TLSInfo.Equals(input.TLSInfo)
                ) &&
                (
                    RootRotationInProgress == input.RootRotationInProgress ||
                    RootRotationInProgress != null &&
                    RootRotationInProgress.Equals(input.RootRotationInProgress)
                ) &&
                (
                    DataPathPort == input.DataPathPort ||
                    DataPathPort != null &&
                    DataPathPort.Equals(input.DataPathPort)
                ) &&
                (
                    DefaultAddrPool == input.DefaultAddrPool ||
                    DefaultAddrPool != null &&
                    input.DefaultAddrPool != null &&
                    DefaultAddrPool.SequenceEqual(input.DefaultAddrPool)
                ) &&
                (
                    SubnetSize == input.SubnetSize ||
                    SubnetSize != null &&
                    SubnetSize.Equals(input.SubnetSize)
                ) &&
                (
                    JoinTokens == input.JoinTokens ||
                    JoinTokens != null &&
                    JoinTokens.Equals(input.JoinTokens)
                );
        }

        /// <summary>
        ///     To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // SubnetSize (int) maximum
            if (SubnetSize > 29)
                yield return new ValidationResult(
                    "Invalid value for SubnetSize, must be a value less than or equal to 29.", new[] {"SubnetSize"});
        }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Swarm {\n");
            sb.Append("  ID: ").Append(ID).Append("\n");
            sb.Append("  _Version: ").Append(_Version).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
            sb.Append("  Spec: ").Append(Spec).Append("\n");
            sb.Append("  TLSInfo: ").Append(TLSInfo).Append("\n");
            sb.Append("  RootRotationInProgress: ").Append(RootRotationInProgress).Append("\n");
            sb.Append("  DataPathPort: ").Append(DataPathPort).Append("\n");
            sb.Append("  DefaultAddrPool: ").Append(DefaultAddrPool).Append("\n");
            sb.Append("  SubnetSize: ").Append(SubnetSize).Append("\n");
            sb.Append("  JoinTokens: ").Append(JoinTokens).Append("\n");
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
            return Equals(input as Swarm);
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
                if (ID != null)
                    hashCode = hashCode * 59 + ID.GetHashCode();
                if (_Version != null)
                    hashCode = hashCode * 59 + _Version.GetHashCode();
                if (CreatedAt != null)
                    hashCode = hashCode * 59 + CreatedAt.GetHashCode();
                if (UpdatedAt != null)
                    hashCode = hashCode * 59 + UpdatedAt.GetHashCode();
                if (Spec != null)
                    hashCode = hashCode * 59 + Spec.GetHashCode();
                if (TLSInfo != null)
                    hashCode = hashCode * 59 + TLSInfo.GetHashCode();
                if (RootRotationInProgress != null)
                    hashCode = hashCode * 59 + RootRotationInProgress.GetHashCode();
                if (DataPathPort != null)
                    hashCode = hashCode * 59 + DataPathPort.GetHashCode();
                if (DefaultAddrPool != null)
                    hashCode = hashCode * 59 + DefaultAddrPool.GetHashCode();
                if (SubnetSize != null)
                    hashCode = hashCode * 59 + SubnetSize.GetHashCode();
                if (JoinTokens != null)
                    hashCode = hashCode * 59 + JoinTokens.GetHashCode();
                return hashCode;
            }
        }
    }
}