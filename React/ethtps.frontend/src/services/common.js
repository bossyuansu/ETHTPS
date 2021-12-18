import { GeneralApi, GPSApi, TPSApi, GasAdjustedTPSApi, StatusApi } from './api-gen/src/index';
import ApiClient from './api-gen/src/ApiClient';
import InstantDataService from './InstantDataService';

export const client = new ApiClient('http://localhost:10202/');
export const globalGeneralApi = new GeneralApi(client);
export const globalGPSApi = new GPSApi(client);
export const globalTPSApi = new TPSApi(client);
export const globalGasAdjustedTPSApi = new GasAdjustedTPSApi(client);
export const globalStatusApi = new StatusApi(client);
export const globalInstantDataService = new InstantDataService(); 
export const formatModeName = function(mode) {
    if (mode !== "gasAdjustedTPS"){
      return mode.toUpperCase();
    }
    else{
      return "gas-adjusted TPS"
    }
  }

export const formatSmoothingName = function(smoothing){
    smoothing = smoothing.replace("One", "1")
    .replace("Minute", "m")
    .replace("Hour", "h")
    .replace("Day", "d")
    .replace("Week", "w")
    .replace("Month", "mo")
    return smoothing;
}

export const capitalizeFirstLetter = function(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
  }

export const to2DecimalPlaces = function(num){
    return Math.round((num + Number.EPSILON) * 100) / 100
 }