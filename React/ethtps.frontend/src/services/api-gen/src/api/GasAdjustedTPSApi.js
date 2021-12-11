/**
 * ETHTPS.API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 *
 */


import ApiClient from "../ApiClient";
import DataPoint from '../model/DataPoint';
import { DataResponseModel } from "..";
/**
* GasAdjustedTPS service.
* @module api/GasAdjustedTPSApi
* @version 1.0
*/
export default class GasAdjustedTPSApi {

    /**
    * Constructs a new GasAdjustedTPSApi. 
    * @alias module:api/GasAdjustedTPSApi
    * @class
    * @param {module:ApiClient} [apiClient] Optional API client implementation to use,
    * default to {@link module:ApiClient#instance} if unspecified.
    */
    constructor(apiClient) {
        this.apiClient = apiClient || ApiClient.instance;
    }


    /**
     * Callback function to receive the result of the aPIGasAdjustedTPSGeMonthlyDataByYearGet operation.
     * @callback module:api/GasAdjustedTPSApi~aPIGasAdjustedTPSGeMonthlyDataByYearGetCallback
     * @param {String} error Error message, if any.
     * @param {Object.<String, {String: [DataResponseModel]}>} data The data returned by the service call.
     * @param {String} response The complete HTTP response.
     */

    /**
     * @param {Object} opts Optional parameters
     * @param {String} opts.provider 
     * @param {Number} opts.year 
     * @param {String} opts.network  (default to 'Mainnet')
     * @param {Boolean} opts.includeSidechains  (default to true)
     * @param {module:api/GasAdjustedTPSApi~aPIGasAdjustedTPSGeMonthlyDataByYearGetCallback} callback The callback function, accepting three arguments: error, data, response
     * data is of type: {@link Object.<String, {String: [DataResponseModel]}>}
     */
    aPIGasAdjustedTPSGeMonthlyDataByYearGet(opts, callback) {
      opts = opts || {};
      let postBody = null;

      let pathParams = {
      };
      let queryParams = {
        'provider': opts['provider'],
        'year': opts['year'],
        'network': opts['network'],
        'includeSidechains': opts['includeSidechains']
      };
      let headerParams = {
      };
      let formParams = {
      };

      let authNames = [];
      let contentTypes = [];
      let accepts = ['text/plain', 'application/json', 'text/json'];
      let returnType = {'String': [DataResponseModel]};
      return this.apiClient.callApi(
        '/API/GasAdjustedTPS/GeMonthlyDataByYear', 'GET',
        pathParams, queryParams, headerParams, formParams, postBody,
        authNames, contentTypes, accepts, returnType, null, callback
      );
    }

    /**
     * Callback function to receive the result of the aPIGasAdjustedTPSGetGet operation.
     * @callback module:api/GasAdjustedTPSApi~aPIGasAdjustedTPSGetGetCallback
     * @param {String} error Error message, if any.
     * @param {Object.<String, {String: [DataResponseModel]}>} data The data returned by the service call.
     * @param {String} response The complete HTTP response.
     */

    /**
     * @param {Object} opts Optional parameters
     * @param {String} opts.provider 
     * @param {String} opts.interval 
     * @param {String} opts.network  (default to 'Mainnet')
     * @param {Boolean} opts.includeSidechains  (default to true)
     * @param {module:api/GasAdjustedTPSApi~aPIGasAdjustedTPSGetGetCallback} callback The callback function, accepting three arguments: error, data, response
     * data is of type: {@link Object.<String, {String: [DataResponseModel]}>}
     */
    aPIGasAdjustedTPSGetGet(opts, callback) {
      opts = opts || {};
      let postBody = null;

      let pathParams = {
      };
      let queryParams = {
        'provider': opts['provider'],
        'interval': opts['interval'],
        'network': opts['network'],
        'includeSidechains': opts['includeSidechains']
      };
      let headerParams = {
      };
      let formParams = {
      };

      let authNames = [];
      let contentTypes = [];
      let accepts = ['text/plain', 'application/json', 'text/json'];
      let returnType = {'String': [DataResponseModel]};
      return this.apiClient.callApi(
        '/API/GasAdjustedTPS/Get', 'GET',
        pathParams, queryParams, headerParams, formParams, postBody,
        authNames, contentTypes, accepts, returnType, null, callback
      );
    }

    /**
     * Callback function to receive the result of the aPIGasAdjustedTPSInstantGet operation.
     * @callback module:api/GasAdjustedTPSApi~aPIGasAdjustedTPSInstantGetCallback
     * @param {String} error Error message, if any.
     * @param {Object.<String, {String: [DataPoint]}>} data The data returned by the service call.
     * @param {String} response The complete HTTP response.
     */

    /**
     * @param {Object} opts Optional parameters
     * @param {Boolean} opts.includeSidechains  (default to true)
     * @param {module:api/GasAdjustedTPSApi~aPIGasAdjustedTPSInstantGetCallback} callback The callback function, accepting three arguments: error, data, response
     * data is of type: {@link Object.<String, {String: [DataPoint]}>}
     */
    aPIGasAdjustedTPSInstantGet(opts, callback) {
      opts = opts || {};
      let postBody = null;

      let pathParams = {
      };
      let queryParams = {
        'includeSidechains': opts['includeSidechains']
      };
      let headerParams = {
      };
      let formParams = {
      };

      let authNames = [];
      let contentTypes = [];
      let accepts = ['text/plain', 'application/json', 'text/json'];
      let returnType = {'String': [DataPoint]};
      return this.apiClient.callApi(
        '/API/GasAdjustedTPS/Instant', 'GET',
        pathParams, queryParams, headerParams, formParams, postBody,
        authNames, contentTypes, accepts, returnType, null, callback
      );
    }

    /**
     * Callback function to receive the result of the aPIGasAdjustedTPSMaxGet operation.
     * @callback module:api/GasAdjustedTPSApi~aPIGasAdjustedTPSMaxGetCallback
     * @param {String} error Error message, if any.
     * @param {Object.<String, module:model/{String: DataPoint}>} data The data returned by the service call.
     * @param {String} response The complete HTTP response.
     */

    /**
     * @param {Object} opts Optional parameters
     * @param {String} opts.provider 
     * @param {String} opts.network  (default to 'Mainnet')
     * @param {module:api/GasAdjustedTPSApi~aPIGasAdjustedTPSMaxGetCallback} callback The callback function, accepting three arguments: error, data, response
     * data is of type: {@link Object.<String, module:model/{String: DataPoint}>}
     */
    aPIGasAdjustedTPSMaxGet(opts, callback) {
      opts = opts || {};
      let postBody = null;

      let pathParams = {
      };
      let queryParams = {
        'provider': opts['provider'],
        'network': opts['network']
      };
      let headerParams = {
      };
      let formParams = {
      };

      let authNames = [];
      let contentTypes = [];
      let accepts = ['text/plain', 'application/json', 'text/json'];
      let returnType = {'String': DataPoint};
      return this.apiClient.callApi(
        '/API/GasAdjustedTPS/Max', 'GET',
        pathParams, queryParams, headerParams, formParams, postBody,
        authNames, contentTypes, accepts, returnType, null, callback
      );
    }


}
