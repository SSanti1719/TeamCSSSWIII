const API_REST_CONFIG = require("../connection/api-rest-config.json");
const REQUEST_PROMISE = require('request-promise')

/**
 * Petición de tipo post a la API REST,
 * función general para cualquier endpoint
 * según la necesidad.
 * 
 * @param {string} prefix 
 * @returns 
 */
async function post(prefix) {

    try {

        let request = {
            method: 'POST',
            url: API_REST_CONFIG.URL + prefix,
            rejectUnauthorized: false,
            requestCert: false,
            agent: false,
            strictSSL: false,
            resolveWithFullResponse: true
        };

        let requestResponse = await REQUEST_PROMISE(request);
        let responseBody = JSON.parse(requestResponse.body);

        if (responseBody.success === 1) {
            return { status: 201, estado: true, mensaje: 'Exitoso', data: responseBody };
        } else {
            return { status: 400, estado: false, mensaje: 'Fallido', data: responseBody };
        }

    } catch (ex) {
        return { status: 500, estado: false, mensaje: ex.message };
    }

}

module.exports = { post }