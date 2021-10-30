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

import ApiClient from '../ApiClient';
import ProviderInfo from './ProviderInfo';
import TPSDataPoint from './TPSDataPoint';
import TPSResponseModel from './TPSResponseModel';

/**
 * The HomePageViewModel model module.
 * @module model/HomePageViewModel
 * @version 1.0
 */
class HomePageViewModel {
    /**
     * Constructs a new <code>HomePageViewModel</code>.
     * @alias module:model/HomePageViewModel
     */
    constructor() { 
        
        HomePageViewModel.initialize(this);
    }

    /**
     * Initializes the fields of this object.
     * This method is used by the constructors of any subclasses, in order to implement multiple inheritance (mix-ins).
     * Only for internal use.
     */
    static initialize(obj) { 
    }

    /**
     * Constructs a <code>HomePageViewModel</code> from a plain JavaScript object, optionally creating a new instance.
     * Copies all relevant properties from <code>data</code> to <code>obj</code> if supplied or a new instance if not.
     * @param {Object} data The plain JavaScript object bearing properties of interest.
     * @param {module:model/HomePageViewModel} obj Optional instance to populate.
     * @return {module:model/HomePageViewModel} The populated <code>HomePageViewModel</code> instance.
     */
    static constructFromObject(data, obj) {
        if (data) {
            obj = obj || new HomePageViewModel();

            if (data.hasOwnProperty('instantTPS')) {
                obj['instantTPS'] = ApiClient.convertToType(data['instantTPS'], {'String': [TPSDataPoint]});
            }
            if (data.hasOwnProperty('providerData')) {
                obj['providerData'] = ApiClient.convertToType(data['providerData'], [ProviderInfo]);
            }
            if (data.hasOwnProperty('colorDictionary')) {
                obj['colorDictionary'] = ApiClient.convertToType(data['colorDictionary'], {'String': 'String'});
            }
            if (data.hasOwnProperty('tpsData')) {
                obj['tpsData'] = ApiClient.convertToType(data['tpsData'], {'String': {'String': [TPSResponseModel]}});
            }
        }
        return obj;
    }


}

/**
 * @member {Object.<String, Array.<module:model/TPSDataPoint>>} instantTPS
 */
HomePageViewModel.prototype['instantTPS'] = undefined;

/**
 * @member {Array.<module:model/ProviderInfo>} providerData
 */
HomePageViewModel.prototype['providerData'] = undefined;

/**
 * @member {Object.<String, String>} colorDictionary
 */
HomePageViewModel.prototype['colorDictionary'] = undefined;

/**
 * @member {Object.<String, Object.<String, Array.<module:model/TPSResponseModel>>>} tpsData
 */
HomePageViewModel.prototype['tpsData'] = undefined;






export default HomePageViewModel;

