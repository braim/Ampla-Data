﻿using System;
using AmplaData.Binding.ModelData;

namespace AmplaData.Binding.Mapping
{
    public class ValidatedModelFieldMapping : ModelFieldMapping
    {
        private readonly Func<string, bool> validateFunc;

        public ValidatedModelFieldMapping(string name, Func<string, bool> validateFunc) : base(name, typeof(string))
        {
            this.validateFunc = validateFunc;
        }

        public override bool TryResolveValue<TModel>(IModelProperties<TModel> modelProperties, TModel model, out string value)
        {
            string baseValue;
            if (base.TryResolveValue(modelProperties, model, out baseValue))
            {
                if (validateFunc(baseValue))
                {
                    value = baseValue;
                    return true;
                }
            }

            value = null;
            return false;
        }
    }
}