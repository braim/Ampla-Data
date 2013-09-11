﻿using System;
using System.Collections.Generic;
using AmplaWeb.Data.AmplaData2008;
using AmplaWeb.Data.Attributes;
using AmplaWeb.Data.Tests;
using NUnit.Framework;

namespace AmplaWeb.Data.Binding.ModelData
{
    [TestFixture]
    public class ModelPropertiesUnitTests : TestFixture
    {
        [AmplaLocation(Location="Enterprise.Site.Area.Simple")]
        [AmplaModule(Module="Production")]
        public class SimpleModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Value { get; set; }
        }

        public class EmptyModel
        {
        }

        [Test]
        public void TestSimpleModel()
        {
            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();
            Assert.That(modelProperties.Location, Is.EqualTo("Enterprise.Site.Area.Simple"));
            Assert.That(modelProperties.Module, Is.EqualTo(AmplaModules.Production));

            IList<string> properties = modelProperties.GetProperties();
            Assert.That(properties, Contains.Item("Id").And.Contains("Name").And.Contains("Value"));
            Assert.That(properties.Count, Is.EqualTo(3));
        }

        [Test]
        public void ConstructorThrowsWhenNoAttributes()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()=>new ModelProperties<EmptyModel>());
            Assert.That(exception.Message, Is.StringContaining(typeof(AmplaLocationAttribute).Name));
            Assert.That(exception.Message, Is.StringContaining(typeof(AmplaModuleAttribute).Name));
        }

        [Test]
        public void TryGetPropertyWithDefaultValues()
        {
            SimpleModel model = new SimpleModel();

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            AssertPropertyGetValue(modelProperties, model, "Id", "0");
            AssertPropertyGetValue(modelProperties, model, "Name", null);
            AssertPropertyGetValue(modelProperties, model, "Value", "0");
        }

        [Test]
        public void TryGetPropertyWithValidValues()
        {
            SimpleModel model = new SimpleModel {Id = 100, Name = "Ampla", Value = 1.234};

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            AssertPropertyGetValue(modelProperties, model, "Id", "100");
            AssertPropertyGetValue(modelProperties, model, "Name", "Ampla");
            AssertPropertyGetValue(modelProperties, model, "Value", "1.234");
        }

        [Test]
        public void TryGetPropertyWithEmptyValues()
        {
            SimpleModel model = new SimpleModel { Id = 0, Name = "", Value = 0D };

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            AssertPropertyGetValue(modelProperties, model, "Id", "0");
            AssertPropertyGetValue(modelProperties, model, "Name", "");
            AssertPropertyGetValue(modelProperties, model, "Value", "0");
        }

        [Test]
        public void TryGetPropertyWithInvalidProperty()
        {
            SimpleModel model = new SimpleModel();

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            AssertNotPropertyGetValue(modelProperties, model, "InvalidId");
            AssertNotPropertyGetValue(modelProperties, model, "InvalidName");
            AssertNotPropertyGetValue(modelProperties, model, "InvalidValue");
        }

        [Test]
        public void TrySetPropertyWithValidValues()
        {
            SimpleModel model = new SimpleModel();

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            AssertPropertySetValue(modelProperties, model, "Id", "100");
            AssertPropertySetValue(modelProperties, model, "Name", "Ampla");
            AssertPropertySetValue(modelProperties, model, "Value", "1.234");

            Assert.That(model.Id, Is.EqualTo(100));
            Assert.That(model.Name, Is.EqualTo("Ampla"));
            Assert.That(model.Value, Is.EqualTo(1.234D));
        }

        [Test]
        public void TrySetPropertyWithEmptyValues()
        {
            SimpleModel model = new SimpleModel { Id = 1, Name = "Name", Value = 1.234};

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            AssertPropertySetValue(modelProperties, model, "Id", "0");
            AssertPropertySetValue(modelProperties, model, "Name", "");
            AssertPropertySetValue(modelProperties, model, "Value", "0");

            Assert.That(model.Id, Is.EqualTo(0));
            Assert.That(model.Name, Is.EqualTo(""));
            Assert.That(model.Value, Is.EqualTo(0));
        }

        [Test]
        public void TrySetPropertyWithNullValues()
        {
            SimpleModel model = new SimpleModel { Id = 1, Name = "Name", Value = 1.234 };

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            AssertPropertyNotSetValue(modelProperties, model, "Id", null);
            AssertPropertySetValue(modelProperties, model, "Name", null);
            AssertPropertyNotSetValue(modelProperties, model, "Value", null);

            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo(null));
            Assert.That(model.Value, Is.EqualTo(1.234));
        }

        [Test]
        public void RoundTripProperties()
        {
            SimpleModel model = new SimpleModel { Id = 100, Name = "Ampla", Value = 1.234 }; 
            SimpleModel newModel = new SimpleModel();

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            string id;
            string name;
            string value;

            modelProperties.TryGetPropertyValue(model, "Id", out id);
            modelProperties.TryGetPropertyValue(model, "Name", out name);
            modelProperties.TryGetPropertyValue(model, "Value", out value);

            modelProperties.TrySetValueFromString(newModel, "Id", id);
            modelProperties.TrySetValueFromString(newModel, "Name", name);
            modelProperties.TrySetValueFromString(newModel, "Value", value);

            Assert.That(newModel.Id, Is.EqualTo(100));
            Assert.That(newModel.Name, Is.EqualTo("Ampla"));
            Assert.That(newModel.Value, Is.EqualTo(1.234D));
        }

        [Test]
        public void RoundTripNullProperties()
        {
            SimpleModel model = new SimpleModel { Id = 1, Name = null, Value = 0 };
            SimpleModel newModel = new SimpleModel();

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            string id;
            string name;
            string value;

            modelProperties.TryGetPropertyValue(model, "Id", out id);
            modelProperties.TryGetPropertyValue(model, "Name", out name);
            modelProperties.TryGetPropertyValue(model, "Value", out value);

            modelProperties.TrySetValueFromString(newModel, "Id", id);
            modelProperties.TrySetValueFromString(newModel, "Name", name);
            modelProperties.TrySetValueFromString(newModel, "Value", value);

            Assert.That(newModel.Id, Is.EqualTo(1));
            Assert.That(newModel.Name, Is.EqualTo(null));
            Assert.That(newModel.Value, Is.EqualTo(0));
        }

        [Test]
        public void RoundTripEmptyProperties()
        {
            SimpleModel model = new SimpleModel { Id = 1, Name = string.Empty, Value = 0 };
            SimpleModel newModel = new SimpleModel();

            ModelProperties<SimpleModel> modelProperties = new ModelProperties<SimpleModel>();

            string id;
            string name;
            string value;

            modelProperties.TryGetPropertyValue(model, "Id", out id);
            modelProperties.TryGetPropertyValue(model, "Name", out name);
            modelProperties.TryGetPropertyValue(model, "Value", out value);

            modelProperties.TrySetValueFromString(newModel, "Id", id);
            modelProperties.TrySetValueFromString(newModel, "Name", name);
            modelProperties.TrySetValueFromString(newModel, "Value", value);

            Assert.That(newModel.Id, Is.EqualTo(1));
            Assert.That(newModel.Name, Is.EqualTo(string.Empty));
            Assert.That(newModel.Value, Is.EqualTo(0));
        }

        private void AssertPropertyGetValue<TModel>(ModelProperties<TModel> modelProperties, TModel model, string property, string expected) where TModel : new()
        {
            string value;
            bool result = modelProperties.TryGetPropertyValue(model, property, out value);

            Assert.That(result, Is.True, "Unexpected Result for {0}", property);
            Assert.That(value, Is.EqualTo(expected), "TryGetPropertyValue('{0}')", property);
        }

        private void AssertPropertySetValue<TModel>(ModelProperties<TModel> modelProperties, TModel model, string property, string value) where TModel : new()
        {
            bool result = modelProperties.TrySetValueFromString(model, property, value);
            Assert.That(result, Is.True, "Unexpected Result for {0}", property);
        }

        private void AssertPropertyNotSetValue<TModel>(ModelProperties<TModel> modelProperties, TModel model, string property, string value) where TModel : new()
        {
            bool result = modelProperties.TrySetValueFromString(model, property, value);
            Assert.That(result, Is.False, "Unexpected Result for {0}", property);
        }

        private void AssertNotPropertyGetValue<TModel>(ModelProperties<TModel> modelProperties, TModel model, string property) where TModel : new()
        {
            string value;
            bool result = modelProperties.TryGetPropertyValue(model, property, out value);

            Assert.That(result, Is.False, "Unexpected Result for {0}", property);
        }

    }
}