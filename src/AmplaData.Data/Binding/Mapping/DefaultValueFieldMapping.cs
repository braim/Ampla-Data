﻿using System;
using AmplaData.Data.Binding.ModelData;

namespace AmplaData.Data.Binding.Mapping
{
    public class DefaultValueFieldMapping : ModelFieldMapping
    {
        private readonly Func<string> defaultValue;

        public DefaultValueFieldMapping(string name, Func<string> defaultValue) : base(name)
        {
            this.defaultValue = defaultValue;
        }

        public override bool TryResolveValue<TModel>(IModelProperties<TModel> modelProperties, TModel model, out string value)
        {
            string baseValue;
            if (base.TryResolveValue(modelProperties, model, out baseValue))
            {
                if (!modelProperties.IsDefaultValue(model, Name))
                {
                    value = baseValue;
                    return true;
                }
            }

            value = defaultValue();
            return true;
        }
    }
}