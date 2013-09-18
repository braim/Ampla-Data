﻿using AmplaWeb.Data.AmplaData2008;
using AmplaWeb.Data.Tests;
using NUnit.Framework;

namespace AmplaWeb.Data.Binding.ViewData
{
    [TestFixture]
    public class ViewFiltersCollectionUnitTests : TestFixture
    {        
        [Test]
        public void InitialiseNull()
        {
            ViewFiltersCollection filters = new ViewFiltersCollection();
            filters.Initialise(null);
        }

        [Test]
        public void InitialiseNullFilters()
        {
            GetView view = new GetView {Filters = null};

            ViewFiltersCollection filters = new ViewFiltersCollection();
            filters.Initialise(view);
        }
    }
}