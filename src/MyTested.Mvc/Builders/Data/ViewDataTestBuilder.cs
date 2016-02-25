﻿namespace MyTested.Mvc.Builders.Data
{
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;

    public class ViewDataTestBuilder : BaseDataProviderTestBuilder, IAndViewDataTestBuilder
    {
        private const string ViewDataName = "view data";
        
        public ViewDataTestBuilder(ControllerTestContext testContext)
            :base(testContext, ViewDataName)
        {
        }

        public IAndViewDataTestBuilder ContainingEntry(string key)
        {
            this.ValidateContainingEntry(key);
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntry<TEntry>(TEntry value)
        {
            this.ValidateContainingEntry(value);
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntryOfType<TEntry>()
        {
            this.ValidateContainingEntryOfType<TEntry>();
            return this;
        }
        
        public IAndViewDataTestBuilder ContainingEntryOfType<TEntry>(string key)
        {
            this.ValidateContainingEntryOfType<TEntry>(key);
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntry(string key, object value)
        {
            this.ValidateContainingEntry(key, value);
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntries(object entries)
            => this.ContainingEntries(new RouteValueDictionary(entries));

        public IViewDataTestBuilder AndAlso()
        {
            return this;
        }

        public IAndViewDataTestBuilder ContainingEntries(IDictionary<string, object> entries)
        {
            this.ValidateContainingEntries(entries);
            return this;
        }

        protected override IDictionary<string, object> GetDataProvider()
        {
            return this.TestContext.ControllerAs<Controller>().ViewData;
        }
    }
}
