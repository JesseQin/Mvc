// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.Internal;

namespace Microsoft.AspNet.Mvc.ModelBinding.Validation
{
    public class ModelValidationContext
    {
        public ModelValidationContext(
            [NotNull] ModelBindingContext bindingContext,
            [NotNull] ModelExplorer modelExplorer)
            : this(bindingContext.ModelName,
                   bindingContext.BindingSource,
                   bindingContext.OperationBindingContext.ValidatorProvider,
                   bindingContext.ModelState,
                   modelExplorer)
        {
        }

        public ModelValidationContext(
            string rootPrefix,
            BindingSource bindingSource,
            [NotNull] IModelValidatorProvider validatorProvider,
            [NotNull] ModelStateDictionary modelState,
            [NotNull] ModelExplorer modelExplorer)
        {
            ModelState = modelState;
            RootPrefix = rootPrefix;
            ValidatorProvider = validatorProvider;
            ModelExplorer = modelExplorer;
            BindingSource = bindingSource;
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="ModelValidationContext"/> class using the
        /// <paramref name="parentContext" /> and <paramref name="modelExplorer"/>.
        /// </summary>
        /// <param name="parentContext">Existing <see cref="ModelValidationContext"/>.</param>
        /// <param name="modelExplorer"><see cref="ModelExplorer"/> associated with the new
        /// <see cref="ModelValidationContext"/>.</param>
        /// <returns></returns>
        public static ModelValidationContext GetChildValidationContext(
            [NotNull] ModelValidationContext parentContext,
            [NotNull] ModelExplorer modelExplorer)
        {
            return new ModelValidationContext(
                parentContext.RootPrefix,
                modelExplorer.Metadata.BindingSource,
                parentContext.ValidatorProvider,
                parentContext.ModelState,
                modelExplorer);
        }

        public ModelExplorer ModelExplorer { get; }

        public ModelStateDictionary ModelState { get; }

        public string RootPrefix { get; set; }

        public ModelValidationNode RootValidationNode { get; }

        public BindingSource BindingSource { get; set; }

        public IModelValidatorProvider ValidatorProvider { get; }
    }
}
