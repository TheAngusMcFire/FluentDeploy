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
    ///     change item in response to ContainerChanges operation
    /// </summary>
    [DataContract]
    public class ContainerChangeResponseItem : IEquatable<ContainerChangeResponseItem>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerChangeResponseItem" /> class.
        /// </summary>
        [JsonConstructor]
        protected ContainerChangeResponseItem()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerChangeResponseItem" /> class.
        /// </summary>
        /// <param name="path">Path to file that has changed (required).</param>
        /// <param name="kind">Kind of change (required).</param>
        public ContainerChangeResponseItem(string path = default, int kind = default)
        {
            // to ensure "path" is required (not null)
            if (path == null)
                throw new InvalidDataException(
                    "path is a required property for ContainerChangeResponseItem and cannot be null");
            Path = path;

            // to ensure "kind" is required (not null)
            if (kind == null)
                throw new InvalidDataException(
                    "kind is a required property for ContainerChangeResponseItem and cannot be null");
            Kind = kind;
        }

        /// <summary>
        ///     Path to file that has changed
        /// </summary>
        /// <value>Path to file that has changed</value>
        [DataMember(Name = "Path", EmitDefaultValue = true)]
        public string Path { get; set; }

        /// <summary>
        ///     Kind of change
        /// </summary>
        /// <value>Kind of change</value>
        [DataMember(Name = "Kind", EmitDefaultValue = true)]
        public int Kind { get; set; }

        /// <summary>
        ///     Returns true if ContainerChangeResponseItem instances are equal
        /// </summary>
        /// <param name="input">Instance of ContainerChangeResponseItem to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ContainerChangeResponseItem input)
        {
            if (input == null)
                return false;

            return
                (
                    Path == input.Path ||
                    Path != null &&
                    Path.Equals(input.Path)
                ) &&
                (
                    Kind == input.Kind ||
                    Kind != null &&
                    Kind.Equals(input.Kind)
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
            sb.Append("class ContainerChangeResponseItem {\n");
            sb.Append("  Path: ").Append(Path).Append("\n");
            sb.Append("  Kind: ").Append(Kind).Append("\n");
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
            return Equals(input as ContainerChangeResponseItem);
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
                if (Path != null)
                    hashCode = hashCode * 59 + Path.GetHashCode();
                if (Kind != null)
                    hashCode = hashCode * 59 + Kind.GetHashCode();
                return hashCode;
            }
        }
    }
}