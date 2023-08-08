// <copyright file="IfTagHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.STS.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class IfTagHelper : TagHelper
    {
        [HtmlAttributeName("show")]
        public bool Show { get; set; } = true;

        [HtmlAttributeName("hide")]
        public bool Exclude { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Always strip the outer tag name as we never want <if> to render
            output.TagName = null;

            if (this.Show && !this.Exclude)
            {
                return;
            }

            output.SuppressOutput();
        }
    }
}
