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
    ///     CredentialSpec for managed service account (Windows only)
    /// </summary>
    [DataContract]
    public class
        TaskSpecContainerSpecPrivilegesCredentialSpec : IEquatable<TaskSpecContainerSpecPrivilegesCredentialSpec>,
            IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskSpecContainerSpecPrivilegesCredentialSpec" /> class.
        /// </summary>
        /// <param name="config">
        ///     Load credential spec from a Swarm Config with the given ID. The specified config must also be
        ///     present in the Configs field with the Runtime property set.  &lt;p&gt;&lt;br /&gt;&lt;/p&gt;   &gt; **Note**:
        ///     &#x60;CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry&#x60;, &gt; and &#x60;CredentialSpec.Config&#x60;
        ///     are mutually exclusive. .
        /// </param>
        /// <param name="file">
        ///     Load credential spec from this file. The file is read by the daemon, and must be present in the
        ///     &#x60;CredentialSpecs&#x60; subdirectory in the docker data directory, which defaults to &#x60;
        ///     C:\\ProgramData\\Docker\\&#x60; on Windows.  For example, specifying &#x60;spec.json&#x60; loads &#x60;
        ///     C:\\ProgramData\\Docker\\CredentialSpecs\\spec.json&#x60;.  &lt;p&gt;&lt;br /&gt;&lt;/p&gt;  &gt; **Note**: &#x60;
        ///     CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry&#x60;, &gt; and &#x60;CredentialSpec.Config&#x60; are
        ///     mutually exclusive. .
        /// </param>
        /// <param name="registry">
        ///     Load credential spec from this value in the Windows registry. The specified registry value must
        ///     be located in:  &#x60;HKLM\\SOFTWARE\\Microsoft\\Windows
        ///     NT\\CurrentVersion\\Virtualization\\Containers\\CredentialSpecs&#x60;  &lt;p&gt;&lt;br /&gt;&lt;/p&gt;   &gt;
        ///     **Note**: &#x60;CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry&#x60;, &gt; and &#x60;
        ///     CredentialSpec.Config&#x60; are mutually exclusive. .
        /// </param>
        public TaskSpecContainerSpecPrivilegesCredentialSpec(string config = default, string file = default,
            string registry = default)
        {
            Config = config;
            File = file;
            Registry = registry;
        }

        /// <summary>
        ///     Load credential spec from a Swarm Config with the given ID. The specified config must also be present in the
        ///     Configs field with the Runtime property set.  &lt;p&gt;&lt;br /&gt;&lt;/p&gt;   &gt; **Note**: &#x60;
        ///     CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry&#x60;, &gt; and &#x60;CredentialSpec.Config&#x60; are
        ///     mutually exclusive.
        /// </summary>
        /// <value>
        ///     Load credential spec from a Swarm Config with the given ID. The specified config must also be present in the
        ///     Configs field with the Runtime property set.  &lt;p&gt;&lt;br /&gt;&lt;/p&gt;   &gt; **Note**: &#x60;
        ///     CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry&#x60;, &gt; and &#x60;CredentialSpec.Config&#x60; are
        ///     mutually exclusive.
        /// </value>
        [DataMember(Name = "Config", EmitDefaultValue = false)]
        public string Config { get; set; }

        /// <summary>
        ///     Load credential spec from this file. The file is read by the daemon, and must be present in the &#x60;
        ///     CredentialSpecs&#x60; subdirectory in the docker data directory, which defaults to &#x60;C:\\ProgramData\\Docker\\
        ///     &#x60; on Windows.  For example, specifying &#x60;spec.json&#x60; loads &#x60;
        ///     C:\\ProgramData\\Docker\\CredentialSpecs\\spec.json&#x60;.  &lt;p&gt;&lt;br /&gt;&lt;/p&gt;  &gt; **Note**: &#x60;
        ///     CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry&#x60;, &gt; and &#x60;CredentialSpec.Config&#x60; are
        ///     mutually exclusive.
        /// </summary>
        /// <value>
        ///     Load credential spec from this file. The file is read by the daemon, and must be present in the &#x60;
        ///     CredentialSpecs&#x60; subdirectory in the docker data directory, which defaults to &#x60;C:\\ProgramData\\Docker\\
        ///     &#x60; on Windows.  For example, specifying &#x60;spec.json&#x60; loads &#x60;
        ///     C:\\ProgramData\\Docker\\CredentialSpecs\\spec.json&#x60;.  &lt;p&gt;&lt;br /&gt;&lt;/p&gt;  &gt; **Note**: &#x60;
        ///     CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry&#x60;, &gt; and &#x60;CredentialSpec.Config&#x60; are
        ///     mutually exclusive.
        /// </value>
        [DataMember(Name = "File", EmitDefaultValue = false)]
        public string File { get; set; }

        /// <summary>
        ///     Load credential spec from this value in the Windows registry. The specified registry value must be located in:
        ///     &#x60;HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Virtualization\\Containers\\CredentialSpecs&#x60;
        ///     &lt;p&gt;&lt;br /&gt;&lt;/p&gt;   &gt; **Note**: &#x60;CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry
        ///     &#x60;, &gt; and &#x60;CredentialSpec.Config&#x60; are mutually exclusive.
        /// </summary>
        /// <value>
        ///     Load credential spec from this value in the Windows registry. The specified registry value must be located in:
        ///     &#x60;HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Virtualization\\Containers\\CredentialSpecs&#x60;
        ///     &lt;p&gt;&lt;br /&gt;&lt;/p&gt;   &gt; **Note**: &#x60;CredentialSpec.File&#x60;, &#x60;CredentialSpec.Registry
        ///     &#x60;, &gt; and &#x60;CredentialSpec.Config&#x60; are mutually exclusive.
        /// </value>
        [DataMember(Name = "Registry", EmitDefaultValue = false)]
        public string Registry { get; set; }

        /// <summary>
        ///     Returns true if TaskSpecContainerSpecPrivilegesCredentialSpec instances are equal
        /// </summary>
        /// <param name="input">Instance of TaskSpecContainerSpecPrivilegesCredentialSpec to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TaskSpecContainerSpecPrivilegesCredentialSpec input)
        {
            if (input == null)
                return false;

            return
                (
                    Config == input.Config ||
                    Config != null &&
                    Config.Equals(input.Config)
                ) &&
                (
                    File == input.File ||
                    File != null &&
                    File.Equals(input.File)
                ) &&
                (
                    Registry == input.Registry ||
                    Registry != null &&
                    Registry.Equals(input.Registry)
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
            sb.Append("class TaskSpecContainerSpecPrivilegesCredentialSpec {\n");
            sb.Append("  Config: ").Append(Config).Append("\n");
            sb.Append("  File: ").Append(File).Append("\n");
            sb.Append("  Registry: ").Append(Registry).Append("\n");
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
            return Equals(input as TaskSpecContainerSpecPrivilegesCredentialSpec);
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
                if (Config != null)
                    hashCode = hashCode * 59 + Config.GetHashCode();
                if (File != null)
                    hashCode = hashCode * 59 + File.GetHashCode();
                if (Registry != null)
                    hashCode = hashCode * 59 + Registry.GetHashCode();
                return hashCode;
            }
        }
    }
}