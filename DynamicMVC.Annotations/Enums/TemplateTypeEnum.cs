using System;

namespace DynamicMVC.Annotations.Enums
{
    /// <summary>
    /// Enum specifies what kind of view is being displayed to the page
    /// </summary>
    [Flags]
    public enum TemplateTypeEnum
    {
        /// <summary>
        /// DynamicIndex
        /// </summary>
        Index,
        /// <summary>
        /// DynamicCreate
        /// </summary>
        Create,
        /// <summary>
        /// DynamicEdit
        /// </summary>
        Edit,
        /// <summary>
        /// DynamicDelete
        /// </summary>
        Delete,
        /// <summary>
        /// DynamicDetails
        /// </summary>
        Details
    }
}
