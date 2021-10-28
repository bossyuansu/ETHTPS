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

(function(root, factory) {
  if (typeof define === 'function' && define.amd) {
    // AMD.
    define(['expect.js', process.cwd()+'/src/index'], factory);
  } else if (typeof module === 'object' && module.exports) {
    // CommonJS-like environments that support module.exports, like Node.
    factory(require('expect.js'), require(process.cwd()+'/src/index'));
  } else {
    // Browser globals (root is window)
    factory(root.expect, root.EthtpsApi);
  }
}(this, function(expect, EthtpsApi) {
  'use strict';

  var instance;

  beforeEach(function() {
    instance = new EthtpsApi.HomePageViewModel();
  });

  var getProperty = function(object, getter, property) {
    // Use getter method if present; otherwise, get the property directly.
    if (typeof object[getter] === 'function')
      return object[getter]();
    else
      return object[property];
  }

  var setProperty = function(object, setter, property, value) {
    // Use setter method if present; otherwise, set the property directly.
    if (typeof object[setter] === 'function')
      object[setter](value);
    else
      object[property] = value;
  }

  describe('HomePageViewModel', function() {
    it('should create an instance of HomePageViewModel', function() {
      // uncomment below and update the code to test HomePageViewModel
      //var instance = new EthtpsApi.HomePageViewModel();
      //expect(instance).to.be.a(EthtpsApi.HomePageViewModel);
    });

    it('should have the property instantTPS (base name: "instantTPS")', function() {
      // uncomment below and update the code to test the property instantTPS
      //var instance = new EthtpsApi.HomePageViewModel();
      //expect(instance).to.be();
    });

    it('should have the property providerData (base name: "providerData")', function() {
      // uncomment below and update the code to test the property providerData
      //var instance = new EthtpsApi.HomePageViewModel();
      //expect(instance).to.be();
    });

    it('should have the property colorDictionary (base name: "colorDictionary")', function() {
      // uncomment below and update the code to test the property colorDictionary
      //var instance = new EthtpsApi.HomePageViewModel();
      //expect(instance).to.be();
    });

    it('should have the property tpsData (base name: "tpsData")', function() {
      // uncomment below and update the code to test the property tpsData
      //var instance = new EthtpsApi.HomePageViewModel();
      //expect(instance).to.be();
    });

  });

}));
