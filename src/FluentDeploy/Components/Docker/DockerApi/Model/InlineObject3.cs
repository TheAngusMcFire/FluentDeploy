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
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace FluentDeploy.Components.Docker.DockerApi.Model
{
    /// <summary>
    ///     InlineObject3
    /// </summary>
    [DataContract]
    public class InlineObject3 : IEquatable<InlineObject3>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InlineObject3" /> class.
        /// </summary>
        /// <param name="container">The ID or name of the container to connect to the network..</param>
        /// <param name="endpointConfig">endpointConfig.</param>
        public InlineObject3(string container = default, EndpointSettings endpointConfig = default)
        {
            Container = container;
            EndpointConfig = endpointConfig;
        }

        /// <summary>
        ///     The ID or name of the container to connect to the network.
        /// </summary>
        /// <value>The ID or name of the container to connect to the network.</value>
        [DataMember(Name = "Container", EmitDefaultValue = false)]
        public string Container { get; set; }

        /// <summary>
        ///     Gets or Sets EndpointConfig
        /// </summary>
        [DataMember(Name = "EndpointConfig", EmitDefaultValue = false)]
        public EndpointSettings EndpointConfig { get; set; }

        /// <summary>
        ///     Returns true if InlineObject3 instances are equal
        /// </summary>
        /// <param name="input">Instance of InlineObject3 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InlineObject3 input)
        {
            if (input == null)
                return false;

            return
                (
                    Container == input.Container ||
                    Container != null &&
                    Container.Equals(input.Container)
                ) &&
                (
                    EndpointConfig == input.EndpointConfig ||
                    EndpointConfig != null &&
                    EndpointConfig.Equals(input.EndpointConfig)
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
            sb.Append("class InlineObject3 {\n");
            sb.Append("  Container: ").Append(Container).Append("\n");
            sb.Append("  EndpointConfig: ").Append(EndpointConfig).Append("\n");
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
            return Equals(input as InlineObject3);
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
                if (Container != null)
                    hashCode = hashCode * 59 + Container.GetHashCode();
                if (EndpointConfig != null)
                    hashCode = hashCode * 59 + EndpointConfig.GetHashCode();
                return hashCode;
            }
        }
    }
}