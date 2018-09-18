﻿namespace HubSpot.NET.Api.Contact
{
    using HubSpot.NET.Api.Contact.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HubSpotContactPropertyApi : ApiRoutable
    {
        private readonly IHubSpotClient _client;

        public HubSpotContactPropertyApi(IHubSpotClient client)
        {
             MidRoute = "/properties/v1/contacts";
            _client = client;

            AddRoute<ContactPropertyModel>("properties");
        }

        public ContactPropertyModel CreateProperty(ContactPropertyModel entity)
        {
            string path = GetRoute<ContactPropertyModel>();
            return _client.Execute<ContactPropertyModel, ContactPropertyModel>(path, entity, RestSharp.Method.GET);
        }

        public List<ContactPropertyModel> GetProperties()
        {
            return _client.Execute<List<ContactPropertyModel>>(GetRoute<ContactPropertyModel>());
        }
    }
}
