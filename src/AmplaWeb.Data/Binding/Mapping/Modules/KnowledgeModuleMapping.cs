﻿
using AmplaWeb.Data.AmplaData2008;

namespace AmplaWeb.Data.Binding.Mapping.Modules
{
    public class KnowledgeModuleMapping : StandardModuleMapping
    {
        public KnowledgeModuleMapping()
        {
            AddSpecialMapping("SampleDateTime", () => new DefaultValueFieldMapping("Sample Period", Iso8601UtcNow));
            AddRequiredMapping("SampleDateTime", () => new DefaultValueFieldMapping("Sample Period", Iso8601UtcNow));

            AddSupportedOperation(ViewAllowedOperations.AddRecord);
            AddSupportedOperation(ViewAllowedOperations.DeleteRecord);
            AddSupportedOperation(ViewAllowedOperations.ModifyRecord);

            AddSupportedOperation(ViewAllowedOperations.ConfirmRecord);
            AddSupportedOperation(ViewAllowedOperations.UnconfirmRecord);
        }
    }
}