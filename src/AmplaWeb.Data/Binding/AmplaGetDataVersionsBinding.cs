﻿using System;
using System.Collections.Generic;
using AmplaWeb.Data.Binding.History;
using AmplaWeb.Data.Binding.ModelData;
using AmplaWeb.Data.Binding.ViewData;
using AmplaWeb.Data.Records;

namespace AmplaWeb.Data.Binding
{
    public class AmplaGetDataVersionsBinding<TModel> : IAmplaBinding where TModel : class, new()
    {
        private readonly AmplaRecord amplaRecord;
        private readonly AmplaAuditRecord auditRecord;
        private readonly TModel currentModel;
        private readonly ModelVersions versions;
        private readonly IModelProperties<TModel> modelProperties;
        private IAmplaViewProperties viewProperties;

        public AmplaGetDataVersionsBinding(AmplaRecord amplaRecord, AmplaAuditRecord auditRecord, TModel currentModel, ModelVersions versions, IModelProperties<TModel> modelProperties, IAmplaViewProperties viewProperties)
        {
            this.amplaRecord = amplaRecord;
            this.auditRecord = auditRecord;
            this.currentModel = currentModel;
            this.versions = versions;
            this.modelProperties = modelProperties;
            this.viewProperties = viewProperties;
        }

        public bool Bind()
        {
            versions.ModelName = modelProperties.GetModelName();

            AmplaRecordHistory history = new AmplaRecordHistory(amplaRecord, auditRecord);
            List<AmplaRecordChanges> recordChanges = history.Reconstruct();

            ModelVersion<TModel>[] modelVersions = new ModelVersion<TModel>[recordChanges.Count];

            TModel clonedModel = modelProperties.CloneModel(currentModel);

            for (int i = 0; i < modelVersions.Length; i++)
            {
                AmplaRecordChanges recordChange = recordChanges[i];
                bool isCurrent = i == (modelVersions.Length - 1);
                modelVersions[i] = new ModelVersion<TModel>(isCurrent, clonedModel)
                    {
                        Version = i + 1,
                        User = recordChange.User,
                        VersionDate = recordChange.VersionDateTime,
                        Operation = recordChange.Operation,
                        Display = recordChange.Display
                    };
            }

            TModel current = currentModel;
            for (int i = modelVersions.Length - 1; i >= 0; i--)
            {
                var modelVersion = modelVersions[i];
                modelVersion.Model = modelProperties.CloneModel(current);
                AmplaRecordChanges recordChange = recordChanges[i];

                foreach (var field in recordChange.Changes)
                {
                    string currentValue;
                    if (modelProperties.TryGetPropertyValue(current, field.Name, out currentValue))
                    {
                        if (StringComparer.InvariantCulture.Compare(currentValue, field.EditedValue) == 0)
                        {
                            modelProperties.TrySetValueFromString(current, field.Name, field.OriginalValue);
                        }
                    }
                }
            }

            foreach (var modelVersion in modelVersions)
            {
                versions.Versions.Add(modelVersion);
            }
            return true;
        }

        public bool Validate()
        {
            return auditRecord != null && (!Equals(currentModel, null));
        }
    }
}