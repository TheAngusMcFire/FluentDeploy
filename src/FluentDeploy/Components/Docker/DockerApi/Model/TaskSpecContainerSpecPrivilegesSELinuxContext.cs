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
    ///     SELinux labels of the container
    /// </summary>
    [DataContract]
    public class
        TaskSpecContainerSpecPrivilegesSELinuxContext : IEquatable<TaskSpecContainerSpecPrivilegesSELinuxContext>,
            IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskSpecContainerSpecPrivilegesSELinuxContext" /> class.
        /// </summary>
        /// <param name="disable">Disable SELinux.</param>
        /// <param name="user">SELinux user label.</param>
        /// <param name="role">SELinux role label.</param>
        /// <param name="type">SELinux type label.</param>
        /// <param name="level">SELinux level label.</param>
        public TaskSpecContainerSpecPrivilegesSELinuxContext(bool disable = default, string user = default,
            string role = default, string type = default, string level = default)
        {
            Disable = disable;
            User = user;
            Role = role;
            Type = type;
            Level = level;
        }

        /// <summary>
        ///     Disable SELinux
        /// </summary>
        /// <value>Disable SELinux</value>
        [DataMember(Name = "Disable", EmitDefaultValue = false)]
        public bool Disable { get; set; }

        /// <summary>
        ///     SELinux user label
        /// </summary>
        /// <value>SELinux user label</value>
        [DataMember(Name = "User", EmitDefaultValue = false)]
        public string User { get; set; }

        /// <summary>
        ///     SELinux role label
        /// </summary>
        /// <value>SELinux role label</value>
        [DataMember(Name = "Role", EmitDefaultValue = false)]
        public string Role { get; set; }

        /// <summary>
        ///     SELinux type label
        /// </summary>
        /// <value>SELinux type label</value>
        [DataMember(Name = "Type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        ///     SELinux level label
        /// </summary>
        /// <value>SELinux level label</value>
        [DataMember(Name = "Level", EmitDefaultValue = false)]
        public string Level { get; set; }

        /// <summary>
        ///     Returns true if TaskSpecContainerSpecPrivilegesSELinuxContext instances are equal
        /// </summary>
        /// <param name="input">Instance of TaskSpecContainerSpecPrivilegesSELinuxContext to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TaskSpecContainerSpecPrivilegesSELinuxContext input)
        {
            if (input == null)
                return false;

            return
                (
                    Disable == input.Disable ||
                    Disable != null &&
                    Disable.Equals(input.Disable)
                ) &&
                (
                    User == input.User ||
                    User != null &&
                    User.Equals(input.User)
                ) &&
                (
                    Role == input.Role ||
                    Role != null &&
                    Role.Equals(input.Role)
                ) &&
                (
                    Type == input.Type ||
                    Type != null &&
                    Type.Equals(input.Type)
                ) &&
                (
                    Level == input.Level ||
                    Level != null &&
                    Level.Equals(input.Level)
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
            sb.Append("class TaskSpecContainerSpecPrivilegesSELinuxContext {\n");
            sb.Append("  Disable: ").Append(Disable).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  Role: ").Append(Role).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Level: ").Append(Level).Append("\n");
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
            return Equals(input as TaskSpecContainerSpecPrivilegesSELinuxContext);
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
                if (Disable != null)
                    hashCode = hashCode * 59 + Disable.GetHashCode();
                if (User != null)
                    hashCode = hashCode * 59 + User.GetHashCode();
                if (Role != null)
                    hashCode = hashCode * 59 + Role.GetHashCode();
                if (Type != null)
                    hashCode = hashCode * 59 + Type.GetHashCode();
                if (Level != null)
                    hashCode = hashCode * 59 + Level.GetHashCode();
                return hashCode;
            }
        }
    }
}