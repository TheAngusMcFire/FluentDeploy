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
using System.Text.Json.Serialization;

namespace FluentDeploy.Components.Docker.DockerApi.Model
{
    /// <summary>
    ///     Specification for the restart policy which applies to containers created as part of this service.
    /// </summary>
    [DataContract]
    public class TaskSpecRestartPolicy : IEquatable<TaskSpecRestartPolicy>, IValidatableObject
    {
        /// <summary>
        ///     Condition for restart.
        /// </summary>
        /// <value>Condition for restart.</value>
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public enum ConditionEnum
        {
            /// <summary>
            ///     Enum None for value: none
            /// </summary>
            [EnumMember(Value = "none")] None = 1,

            /// <summary>
            ///     Enum OnFailure for value: on-failure
            /// </summary>
            [EnumMember(Value = "on-failure")] OnFailure = 2,

            /// <summary>
            ///     Enum Any for value: any
            /// </summary>
            [EnumMember(Value = "any")] Any = 3
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskSpecRestartPolicy" /> class.
        /// </summary>
        /// <param name="condition">Condition for restart..</param>
        /// <param name="delay">Delay between restart attempts..</param>
        /// <param name="maxAttempts">
        ///     Maximum attempts to restart a given container before giving up (default value is 0, which is
        ///     ignored).  (default to 0).
        /// </param>
        /// <param name="window">
        ///     Windows is the time window used to evaluate the restart policy (default value is 0, which is
        ///     unbounded).  (default to 0).
        /// </param>
        public TaskSpecRestartPolicy(ConditionEnum? condition = default, long delay = default, long maxAttempts = 0,
            long window = 0)
        {
            Condition = condition;
            Delay = delay;
            // use default value if no "maxAttempts" provided
            if (maxAttempts == null)
                MaxAttempts = 0;
            else
                MaxAttempts = maxAttempts;
            // use default value if no "window" provided
            if (window == null)
                Window = 0;
            else
                Window = window;
        }

        /// <summary>
        ///     Condition for restart.
        /// </summary>
        /// <value>Condition for restart.</value>
        [DataMember(Name = "Condition", EmitDefaultValue = false)]
        public ConditionEnum? Condition { get; set; }


        /// <summary>
        ///     Delay between restart attempts.
        /// </summary>
        /// <value>Delay between restart attempts.</value>
        [DataMember(Name = "Delay", EmitDefaultValue = false)]
        public long Delay { get; set; }

        /// <summary>
        ///     Maximum attempts to restart a given container before giving up (default value is 0, which is ignored).
        /// </summary>
        /// <value>Maximum attempts to restart a given container before giving up (default value is 0, which is ignored). </value>
        [DataMember(Name = "MaxAttempts", EmitDefaultValue = false)]
        public long MaxAttempts { get; set; }

        /// <summary>
        ///     Windows is the time window used to evaluate the restart policy (default value is 0, which is unbounded).
        /// </summary>
        /// <value>Windows is the time window used to evaluate the restart policy (default value is 0, which is unbounded). </value>
        [DataMember(Name = "Window", EmitDefaultValue = false)]
        public long Window { get; set; }

        /// <summary>
        ///     Returns true if TaskSpecRestartPolicy instances are equal
        /// </summary>
        /// <param name="input">Instance of TaskSpecRestartPolicy to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TaskSpecRestartPolicy input)
        {
            if (input == null)
                return false;

            return
                (
                    Condition == input.Condition ||
                    Condition != null &&
                    Condition.Equals(input.Condition)
                ) &&
                (
                    Delay == input.Delay ||
                    Delay != null &&
                    Delay.Equals(input.Delay)
                ) &&
                (
                    MaxAttempts == input.MaxAttempts ||
                    MaxAttempts != null &&
                    MaxAttempts.Equals(input.MaxAttempts)
                ) &&
                (
                    Window == input.Window ||
                    Window != null &&
                    Window.Equals(input.Window)
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
            sb.Append("class TaskSpecRestartPolicy {\n");
            sb.Append("  Condition: ").Append(Condition).Append("\n");
            sb.Append("  Delay: ").Append(Delay).Append("\n");
            sb.Append("  MaxAttempts: ").Append(MaxAttempts).Append("\n");
            sb.Append("  Window: ").Append(Window).Append("\n");
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
            return Equals(input as TaskSpecRestartPolicy);
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
                if (Condition != null)
                    hashCode = hashCode * 59 + Condition.GetHashCode();
                if (Delay != null)
                    hashCode = hashCode * 59 + Delay.GetHashCode();
                if (MaxAttempts != null)
                    hashCode = hashCode * 59 + MaxAttempts.GetHashCode();
                if (Window != null)
                    hashCode = hashCode * 59 + Window.GetHashCode();
                return hashCode;
            }
        }
    }
}