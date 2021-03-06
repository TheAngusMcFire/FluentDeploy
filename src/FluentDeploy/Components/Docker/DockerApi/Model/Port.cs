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
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentDeploy.Components.Docker.DockerApi.Model
{
    /// <summary>
    ///     An open port on a container
    /// </summary>
    [DataContract]
    public class Port : IEquatable<Port>, IValidatableObject
    {
        /// <summary>
        ///     Defines Type
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public enum TypeEnum
        {
            /// <summary>
            ///     Enum Tcp for value: tcp
            /// </summary>
            [EnumMember(Value = "tcp")] Tcp = 1,

            /// <summary>
            ///     Enum Udp for value: udp
            /// </summary>
            [EnumMember(Value = "udp")] Udp = 2,

            /// <summary>
            ///     Enum Sctp for value: sctp
            /// </summary>
            [EnumMember(Value = "sctp")] Sctp = 3
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Port" /> class.
        /// </summary>
        [JsonConstructor]
        protected Port()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Port" /> class.
        /// </summary>
        /// <param name="iP">Host IP address that the container&#39;s port is mapped to.</param>
        /// <param name="privatePort">Port on the container (required).</param>
        /// <param name="publicPort">Port exposed on the host.</param>
        /// <param name="type">type (required).</param>
        public Port(string iP = default, int privatePort = default, int publicPort = default, TypeEnum type = default)
        {
            // to ensure "privatePort" is required (not null)
            if (privatePort == null)
                throw new InvalidDataException("privatePort is a required property for Port and cannot be null");
            PrivatePort = privatePort;

            // to ensure "type" is required (not null)
            if (type == null)
                throw new InvalidDataException("type is a required property for Port and cannot be null");
            Type = type;

            IP = iP;
            PublicPort = publicPort;
        }

        /// <summary>
        ///     Gets or Sets Type
        /// </summary>
        [DataMember(Name = "Type", EmitDefaultValue = true)]
        public TypeEnum Type { get; set; }

        /// <summary>
        ///     Host IP address that the container&#39;s port is mapped to
        /// </summary>
        /// <value>Host IP address that the container&#39;s port is mapped to</value>
        [DataMember(Name = "IP", EmitDefaultValue = false)]
        public string IP { get; set; }

        /// <summary>
        ///     Port on the container
        /// </summary>
        /// <value>Port on the container</value>
        [DataMember(Name = "PrivatePort", EmitDefaultValue = true)]
        public int PrivatePort { get; set; }

        /// <summary>
        ///     Port exposed on the host
        /// </summary>
        /// <value>Port exposed on the host</value>
        [DataMember(Name = "PublicPort", EmitDefaultValue = false)]
        public int PublicPort { get; set; }

        /// <summary>
        ///     Returns true if Port instances are equal
        /// </summary>
        /// <param name="input">Instance of Port to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Port input)
        {
            if (input == null)
                return false;

            return
                (
                    IP == input.IP ||
                    IP != null &&
                    IP.Equals(input.IP)
                ) &&
                (
                    PrivatePort == input.PrivatePort ||
                    PrivatePort != null &&
                    PrivatePort.Equals(input.PrivatePort)
                ) &&
                (
                    PublicPort == input.PublicPort ||
                    PublicPort != null &&
                    PublicPort.Equals(input.PublicPort)
                ) &&
                (
                    Type == input.Type ||
                    Type != null &&
                    Type.Equals(input.Type)
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
            sb.Append("class Port {\n");
            sb.Append("  IP: ").Append(IP).Append("\n");
            sb.Append("  PrivatePort: ").Append(PrivatePort).Append("\n");
            sb.Append("  PublicPort: ").Append(PublicPort).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return Equals(input as Port);
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
                if (IP != null)
                    hashCode = hashCode * 59 + IP.GetHashCode();
                if (PrivatePort != null)
                    hashCode = hashCode * 59 + PrivatePort.GetHashCode();
                if (PublicPort != null)
                    hashCode = hashCode * 59 + PublicPort.GetHashCode();
                if (Type != null)
                    hashCode = hashCode * 59 + Type.GetHashCode();
                return hashCode;
            }
        }
    }
}