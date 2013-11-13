﻿using AmplaData.Data.AmplaData2008;
using AmplaData.Data.Knowledge;
using NUnit.Framework;

namespace AmplaData.Data.Binding.Mapping.Modules
{
    [TestFixture]
    public class KnowledgeModuleMappingUnitTests : ModuleMappingTestFixture
    {
        public KnowledgeModuleMappingUnitTests() : base(KnowledgeViews.StandardView, () => new KnowledgeModuleMapping())
        {
        }

        [Test]
        public void IdField()
        {
            CheckField<IdFieldMapping>("Id", "Id", true, false);
        }
        
        [Test]
        public void LocationField()
        {
            CheckField<ReadOnlyFieldMapping>("ObjectId", "Location", true, false);
        }

        [Test]
        public void SamplePeriod()
        {
            CheckField<DefaultValueFieldMapping>("SampleDateTime", "Sample Period", true, true);
        }

        [Test]
        public void SupportedOperations()
        {
            CheckAllowedOperations(
                ViewAllowedOperations.AddRecord,
                ViewAllowedOperations.ConfirmRecord,
                ViewAllowedOperations.DeleteRecord,
                ViewAllowedOperations.ModifyRecord,
                ViewAllowedOperations.UnconfirmRecord,
                ViewAllowedOperations.ViewRecord);
        }
    }
}