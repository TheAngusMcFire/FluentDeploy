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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentDeploy.Components.Docker.DockerApi.Model
{
    /// <summary>
    ///     individual image layer information in response to ImageHistory operation
    /// </summary>
    [DataContract]
    public class HistoryResponseItem : IEquatable<HistoryResponseItem>, IValidatableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HistoryResponseItem" /> class.
        /// </summary>
        [JsonConstructor]
        protected HistoryResponseItem()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HistoryResponseItem" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="created">created (required).</param>
        /// <param name="createdBy">createdBy (required).</param>
        /// <param name="tags">tags (required).</param>
        /// <param name="size">size (required).</param>
        /// <param name="comment">comment (required).</param>
        public HistoryResponseItem(string id = default, long created = default, string createdBy = default,
            List<string> tags = default, long size = default, string comment = default)
        {
            // to ensure "id" is required (not null)
            if (id == null)
                throw new InvalidDataException("id is a required property for HistoryResponseItem and cannot be null");
            Id = id;

            // to ensure "created" is required (not null)
            if (created == null)
                throw new InvalidDataException(
                    "created is a required property for HistoryResponseItem and cannot be null");
            Created = created;

            // to ensure "createdBy" is required (not null)
            if (createdBy == null)
                throw new InvalidDataException(
                    "createdBy is a required property for HistoryResponseItem and cannot be null");
            CreatedBy = createdBy;

            // to ensure "tags" is required (not null)
            if (tags == null)
                throw new InvalidDataException(
                    "tags is a required property for HistoryResponseItem and cannot be null");
            Tags = tags;

            // to ensure "size" is required (not null)
            if (size == null)
                throw new InvalidDataException(
                    "size is a required property for HistoryResponseItem and cannot be null");
            Size = size;

            // to ensure "comment" is required (not null)
            if (comment == null)
                throw new InvalidDataException(
                    "comment is a required property for HistoryResponseItem and cannot be null");
            Comment = comment;
        }

        /// <summary>
        ///     Gets or Sets Id
        /// </summary>
        [DataMember(Name = "Id", EmitDefaultValue = true)]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or Sets Created
        /// </summary>
        [DataMember(Name = "Created", EmitDefaultValue = true)]
        public long Created { get; set; }

        /// <summary>
        ///     Gets or Sets CreatedBy
        /// </summary>
        [DataMember(Name = "CreatedBy", EmitDefaultValue = true)]
        public string CreatedBy { get; set; }

        /// <summary>
        ///     Gets or Sets Tags
        /// </summary>
        [DataMember(Name = "Tags", EmitDefaultValue = true)]
        public List<string> Tags { get; set; }

        /// <summary>
        ///     Gets or Sets Size
        /// </summary>
        [DataMember(Name = "Size", EmitDefaultValue = true)]
        public long Size { get; set; }

        /// <summary>
        ///     Gets or Sets Comment
        /// </summary>
        [DataMember(Name = "Comment", EmitDefaultValue = true)]
        public string Comment { get; set; }

        /// <summary>
        ///     Returns true if HistoryResponseItem instances are equal
        /// </summary>
        /// <param name="input">Instance of HistoryResponseItem to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(HistoryResponseItem input)
        {
            if (input == null)
                return false;

            return
                (
                    Id == input.Id ||
                    Id != null &&
                    Id.Equals(input.Id)
                ) &&
                (
                    Created == input.Created ||
                    Created != null &&
                    Created.Equals(input.Created)
                ) &&
                (
                    CreatedBy == input.CreatedBy ||
                    CreatedBy != null &&
                    CreatedBy.Equals(input.CreatedBy)
                ) &&
                (
                    Tags == input.Tags ||
                    Tags != null &&
                    input.Tags != null &&
                    Tags.SequenceEqual(input.Tags)
                ) &&
                (
                    Size == input.Size ||
                    Size != null &&
                    Size.Equals(input.Size)
                ) &&
                (
                    Comment == input.Comment ||
                    Comment != null &&
                    Comment.Equals(input.Comment)
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
            sb.Append("class HistoryResponseItem {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Created: ").Append(Created).Append("\n");
            sb.Append("  CreatedBy: ").Append(CreatedBy).Append("\n");
            sb.Append("  Tags: ").Append(Tags).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Comment: ").Append(Comment).Append("\n");
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
            return Equals(input as HistoryResponseItem);
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
                if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                if (Created != null)
                    hashCode = hashCode * 59 + Created.GetHashCode();
                if (CreatedBy != null)
                    hashCode = hashCode * 59 + CreatedBy.GetHashCode();
                if (Tags != null)
                    hashCode = hashCode * 59 + Tags.GetHashCode();
                if (Size != null)
                    hashCode = hashCode * 59 + Size.GetHashCode();
                if (Comment != null)
                    hashCode = hashCode * 59 + Comment.GetHashCode();
                return hashCode;
            }
        }
    }
}