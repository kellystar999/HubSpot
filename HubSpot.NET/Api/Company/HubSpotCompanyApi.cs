using System;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Company
{
    public class HubSpotCompanyApi : IHubSpotCompanyApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotCompanyApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a Company entity
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Create<T>(T entity) where T : CompanyHubSpotModel, new()
        {
            var path = $"{entity.RouteBasePath}/companies";

            return _client.Execute<T>(path, entity, Method.POST);
        }

        /// <summary>
        /// Gets a specific company by it's ID
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="companyId">The ID</param>
        /// <returns>The company entity</returns>
        public T GetById<T>(long companyId) where T : CompanyHubSpotModel, new()
        {
            var path =  $"{new T().RouteBasePath}/companies/{companyId}";

            return _client.Execute<T>(path);
        }

        /// <summary>
        /// Gets a company by domain name
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="domain">Domain name to search for</param>
        /// <param name="options">Set of search options</param>
        /// <returns>The company entity</returns>
        public T GetByDomain<T>(string domain, CompanySearchByDomain options = null) where T : CompanySearchResultModel, new()
        {
            if (options == null)
            {
                options = new CompanySearchByDomain();
            }

            var path =  $"{new CompanyHubSpotModel().RouteBasePath}/domains/{domain}/companies";

            var data = _client.ExecuteList<T>(path, options, Method.POST);

            return data;
        }

        /// <summary>
        /// Updates a given company entity, any changed properties are updated
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="entity">The company entity</param>
        /// <returns>The updated company entity</returns>
        public T Update<T>(T entity) where T : CompanyHubSpotModel, new()
        {
            if (entity.Id < 1)
            {
                throw new ArgumentException("Company entity must have an id set!");
            }

            var path = $"{entity.RouteBasePath}/companies/{entity.Id}";

            var data = _client.Execute<T>(path, entity, Method.PUT);

            return data;
        }

        /// <summary>
        /// Deletes the given company
        /// </summary>
        /// <param name="companyId">ID of the company</param>
        public void Delete(long companyId)
        {
            var path = $"{new CompanyHubSpotModel().RouteBasePath}/companies/{companyId}";

            _client.Execute(path, method: Method.DELETE);
        }
    }
}